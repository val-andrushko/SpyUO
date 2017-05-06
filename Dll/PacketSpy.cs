using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SpyUO
{
	public delegate void PacketHandler( byte[] data, bool send );
	public delegate void ProcessTerminatedHandler();

	public class PacketSpy : IDisposable
	{
		private abstract class PacketSpyStarter
		{
			public static void Start( PacketSpy packetSpy, string path )
			{
				PacketSpyStarter starter = new PathStarter( packetSpy, path );
				Start( starter );
			}

			public static void Start( PacketSpy packetSpy, Process process )
			{
				PacketSpyStarter starter = new ProcessStarter( packetSpy, process );
				Start( starter );
			}

			private static void Start( PacketSpyStarter starter )
			{
				Thread thread = new Thread( new ThreadStart( starter.Start ) );
				thread.Start();
			}

			private PacketSpy m_PacketSpy;

			public PacketSpy PacketSpy { get { return m_PacketSpy; } }

			public PacketSpyStarter( PacketSpy packetSpy )
			{
				m_PacketSpy = packetSpy;
			}

			public abstract void Start();

			private class PathStarter : PacketSpyStarter
			{
				string m_Path;

				public PathStarter( PacketSpy packetSpy, string path ) : base( packetSpy )
				{
					m_Path = path;
				}

				public override void Start()
				{
					PacketSpy.Init( m_Path );
					PacketSpy.MainLoop();
				}
			}

			private class ProcessStarter : PacketSpyStarter
			{
				private Process m_Process;

				public ProcessStarter( PacketSpy packetSpy, Process process ) : base( packetSpy )
				{
					m_Process = process;
				}

				public override void Start()
				{
					PacketSpy.Init( m_Process );
					PacketSpy.MainLoop();
				}
			}
		}

		private Process m_Process;
		private IntPtr m_hProcess;
		private bool m_Enhanced;
		private AddressAndRegisters m_Send;
		private byte m_OrSCode;
		private AddressAndRegisters m_Recv;
		private byte m_OrRCode;
		private PacketHandler m_PacketHandler;

		private NativeMethods.CONTEXT m_ContextBuffer;
		private NativeMethods.WOW64_CONTEXT m_X64ContextBuffer;
		private NativeMethods.DEBUG_EVENT_EXCEPTION m_DEventBuffer;

		private bool m_ToStop;
		private ManualResetEvent m_Stopped;

		private bool SafeToStop
		{
			get	{ lock ( this ) return m_ToStop; }
			set	{ lock ( this )	m_ToStop = value; }
		}

		private bool ProcessTerminated { get { return m_Process.HasExited; } }

		public event ProcessTerminatedHandler OnProcessTerminated;

		public PacketSpy( bool enhanced, AddressAndRegisters send, AddressAndRegisters recv, PacketHandler packetHandler )
		{
			m_Enhanced = enhanced;
			m_Send = send;
			m_Recv = recv;
			m_PacketHandler = packetHandler;

			m_ContextBuffer = new NativeMethods.CONTEXT();
			m_ContextBuffer.ContextFlags = NativeMethods.ContextFlags.CONTEXT_CONTROL | NativeMethods.ContextFlags.CONTEXT_INTEGER;
			m_X64ContextBuffer = new NativeMethods.WOW64_CONTEXT();
			m_X64ContextBuffer.ContextFlags = NativeMethods.ContextFlags.CONTEXT_CONTROL | NativeMethods.ContextFlags.CONTEXT_INTEGER;
			m_DEventBuffer = new NativeMethods.DEBUG_EVENT_EXCEPTION();

			m_ToStop = false;
			m_Stopped = new ManualResetEvent( true );
		}

		public void Spy( string path )
		{
			PacketSpyStarter.Start( this, path );
		}

		public void Spy( Process process )
		{
			PacketSpyStarter.Start( this, process );
		}

		private void Init( string path )
		{
			string pathDir = Path.GetDirectoryName( path );

			NativeMethods.STARTUPINFO startupInfo = new NativeMethods.STARTUPINFO();
			NativeMethods.PROCESS_INFORMATION processInfo;

			if ( !NativeMethods.CreateProcess( path, null, IntPtr.Zero, IntPtr.Zero, false,
				NativeMethods.CreationFlag.DEBUG_PROCESS, IntPtr.Zero, pathDir, ref startupInfo, out processInfo ) )
				throw new Win32Exception();

			NativeMethods.CloseHandle( processInfo.hThread );

			m_Process = Process.GetProcessById( (int)processInfo.dwProcessId );
			m_hProcess = processInfo.hProcess;

			InitBreakpoints();
		}

		private void Init( Process process )
		{
			uint id = (uint)process.Id;

			m_Process = process;

			m_hProcess = NativeMethods.OpenProcess( NativeMethods.DesiredAccessProcess.PROCESS_ALL_ACCESS, false, id );
			if ( m_hProcess == IntPtr.Zero )
				throw new Win32Exception();

			if ( !NativeMethods.DebugActiveProcess( id ) )
				throw new Win32Exception();

			InitBreakpoints();
		}

		private void InitBreakpoints()
		{
			m_OrSCode = AddBreakpoint( m_Send.Address );
			m_OrRCode = AddBreakpoint( m_Recv.Address );
		}

		private static readonly byte[] BreakCode = { 0xCC };

		private byte AddBreakpoint( uint address )
		{
			byte[] orOpCode = ReadProcessMemory( address, 1 );

			WriteProcessMemory( address, BreakCode );

			return orOpCode[0];
		}

		private void RemoveBreakpoints()
		{
			WriteProcessMemory( m_Send.Address, new byte[] { m_OrSCode } );
			WriteProcessMemory( m_Recv.Address, new byte[] { m_OrRCode } );
		}

		private void MainLoop()
		{
			m_Stopped.Reset();

			try
			{
				while ( !SafeToStop && !ProcessTerminated )
				{
					if ( NativeMethods.WaitForDebugEvent( ref m_DEventBuffer, 1000 ) )
					{
						if ( m_DEventBuffer.dwDebugEventCode == NativeMethods.DebugEventCode.EXCEPTION_DEBUG_EVENT )
						{
							ulong address = (ulong)m_DEventBuffer.u.Exception.ExceptionRecord.ExceptionAddress.ToInt64();

							if ( address == m_Send.Address )
							{
								SpySendPacket( m_DEventBuffer.dwThreadId );
								continue;
							}
							else if ( address == m_Recv.Address )
							{
								SpyRecvPacket( m_DEventBuffer.dwThreadId );
								continue;
							}
						}

						ContinueDebugEvent( m_DEventBuffer.dwThreadId );
					}
				}
			}
			finally
			{
				EndSpy();

				if ( ProcessTerminated && OnProcessTerminated != null )
					OnProcessTerminated();
			}
		}

		private void SpySendPacket( uint threadId )
		{
			SpyPacket( threadId, true );
		}

		private void SpyRecvPacket( uint threadId )
		{
			SpyPacket( threadId, false );
		}

		private void SpyPacket( uint threadId, bool send )
		{
			IntPtr hThread = NativeMethods.OpenThread( NativeMethods.DesiredAccessThread.THREAD_GET_CONTEXT | NativeMethods.DesiredAccessThread.THREAD_SET_CONTEXT | NativeMethods.DesiredAccessThread.THREAD_QUERY_INFORMATION, false, threadId );

			GetThreadContext( hThread );

			AddressAndRegisters ar = send ? m_Send : m_Recv;

			uint dataAddress = GetContextRegister( ar.AddressRegister );
			uint dataLength = GetContextRegister( ar.LengthRegister ) & 0xFFFF;

			byte[] data = null;
			
			if ( m_Enhanced )
			{
				data = ReadProcessMemory( dataAddress + 4, 8 );

				using ( MemoryStream stream = new MemoryStream( data ) )
				{
					using ( BinaryReader reader = new BinaryReader( stream ) )
					{
						uint start = reader.ReadUInt32();
						uint length = reader.ReadUInt32() - start;
						
						data = ReadProcessMemory( start, length );
					}
				}
			}
			else
				data = ReadProcessMemory( dataAddress, dataLength );

			#region Breakpoint Recovery

			WriteProcessMemory( ar.Address, new byte[] { send ? m_OrSCode : m_OrRCode } );

			if ( SystemInfo.IsX64 )
			{
				m_X64ContextBuffer.Eip--;
				m_X64ContextBuffer.EFlags |= 0x100; // Single step
			}
			else
			{
				m_ContextBuffer.Eip--;
				m_ContextBuffer.EFlags |= 0x100; // Single step
			}

			SetThreadContext( hThread );
			ContinueDebugEvent( threadId );

			if ( !NativeMethods.WaitForDebugEvent( ref m_DEventBuffer, 2500 ) )
				throw new Win32Exception();

			WriteProcessMemory( ar.Address, BreakCode );

			GetThreadContext( hThread );

			if ( SystemInfo.IsX64 )
				m_X64ContextBuffer.EFlags &= ~0x100u; //  End single step
			else
				m_ContextBuffer.EFlags &= ~0x100u; // End single step

			SetThreadContext( hThread );

			#endregion

			NativeMethods.CloseHandle( hThread );

			ContinueDebugEvent( threadId );

			if ( data != null )
				m_PacketHandler( data, send );
		}

		private byte[] ReadProcessMemory( uint address, uint length )
		{
			byte[] buffer = new byte[length];
			IntPtr ptrAddr = new IntPtr( address );
			uint read;

			NativeMethods.ReadProcessMemory( m_hProcess, ptrAddr, buffer, length, out read );

			if ( read != length )
				throw new Win32Exception();

			return buffer;
		}

		private void WriteProcessMemory( uint address, byte[] data )
		{
			uint length = (uint)data.Length;
			IntPtr ptrAddr = new IntPtr( address );
			uint written;

			NativeMethods.WriteProcessMemory( m_hProcess, ptrAddr, data, length, out written );

			if ( written != length )
				throw new Win32Exception();

			NativeMethods.FlushInstructionCache( m_hProcess, ptrAddr, length );
		}

		private void ContinueDebugEvent( uint threadId )
		{
			if ( !NativeMethods.ContinueDebugEvent( (uint)m_Process.Id, threadId, NativeMethods.ContinueStatus.DBG_CONTINUE ) )
				throw new Win32Exception();
		}

		private void GetThreadContext( IntPtr hThread )
		{
			if ( SystemInfo.IsX64 )
			{
				if ( !NativeMethods.Wow64GetThreadContext( hThread, ref m_X64ContextBuffer ) )
					throw new Win32Exception();
			}
			else
			{
				if ( !NativeMethods.GetThreadContext( hThread, ref m_ContextBuffer ) )
					throw new Win32Exception();
			}
		}

		private void SetThreadContext( IntPtr hThread )
		{
			if ( SystemInfo.IsX64 )
			{
				if ( !NativeMethods.Wow64SetThreadContext( hThread, ref m_X64ContextBuffer ) )
					throw new Win32Exception();
			}
			else
			{
				if ( !NativeMethods.SetThreadContext( hThread, ref m_ContextBuffer ) )
					throw new Win32Exception();
			}
		}

		private uint GetContextRegister( Register register )
		{
			if ( SystemInfo.IsX64 )
			{
				switch ( register )
				{
					case Register.Eax: return m_X64ContextBuffer.Eax;
					case Register.Ebp: return m_X64ContextBuffer.Ebp;
					case Register.Ebx: return m_X64ContextBuffer.Ebx;
					case Register.Ecx: return m_X64ContextBuffer.Ecx;
					case Register.Edi: return m_X64ContextBuffer.Edi;
					case Register.Edx: return m_X64ContextBuffer.Edx;
					case Register.Esi: return m_X64ContextBuffer.Esi;
					case Register.Esp: return m_X64ContextBuffer.Esp;

					default: throw new ArgumentException();
				}
			}
			else
			{
				switch ( register )
				{
					case Register.Eax: return m_ContextBuffer.Eax;
					case Register.Ebp: return m_ContextBuffer.Ebp;
					case Register.Ebx: return m_ContextBuffer.Ebx;
					case Register.Ecx: return m_ContextBuffer.Ecx;
					case Register.Edi: return m_ContextBuffer.Edi;
					case Register.Edx: return m_ContextBuffer.Edx;
					case Register.Esi: return m_ContextBuffer.Esi;
					case Register.Esp: return m_ContextBuffer.Esp;

					default: throw new ArgumentException();
				}
			}
		}

		public void Dispose()
		{
			if ( !SafeToStop )
			{
				SafeToStop = true;

				m_Stopped.WaitOne();
				m_Stopped.Close();
			}
		}

		private void EndSpy()
		{
			try
			{
				RemoveBreakpoints();
				NativeMethods.DebugActiveProcessStop( (uint)m_Process.Id );
				NativeMethods.CloseHandle( m_hProcess );
			}
			catch { }

			m_Stopped.Set();
		}
	}
}