using System;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;

using Ultima;

namespace SpyUO.Packets
{
	[PacketInfo( 0x54 )]
	public class PlaySound : Packet
	{
		private SoundPlayer m_Player;
		private UOSound m_Sound;

		private ushort m_SoundId;
		private byte m_SoundMode;
		private Point3D m_Position;
		private short m_Unknown;

		[PacketProp( 0, "0x{0:X}" )]
		public ushort SoundId { get { return m_SoundId; } }

		[PacketProp( 1 )]
		public byte SoundMode { get { return m_SoundMode; } }

		[PacketProp( 2 )]
		public Point3D Position { get { return m_Position; } }

		[PacketProp( 3, "0x{0:X}" )]
		public short Unknown { get { return m_Unknown; } }

		[PacketProp( 4 )]
		public string Name
		{
			get
			{
				if ( m_Sound != null )
					return m_Sound.Name;

				return String.Empty;
			}
		}

		public PlaySound( PacketReader reader, bool send ) : base( reader, send )
		{
			m_SoundMode = reader.ReadByte();
			m_SoundId = reader.ReadUInt16();
			m_Unknown = reader.ReadInt16();
			m_Position = new Point3D( reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16() );

			try
			{
				if ( m_SoundId != 0xFFFF )
				{
					m_Sound = LoadSound( m_SoundId );

					if ( m_Sound != null )
						m_Player = new SoundPlayer( m_Sound .WAVEStream );
				}
			}
			catch
			{
				// TODO logging
			}
		}

		public override void AddContextMenuItems( ToolStripItemCollection list )
		{
			if ( m_Sound != null )
			{
				ToolStripButton button = new ToolStripButton( "Play" );
				button.Click +=new EventHandler( Play_Click );
				list.Add( button );
			}
		}

		private void Play_Click( object sender, EventArgs e )
		{
			if ( m_Player != null )
			{
				m_Player.Stop();
				m_Player.Stream.Position = 0;
				m_Player.Play();
			}
		}

		private static Dictionary<int, UOSound> m_Buffer = new Dictionary<int,UOSound>();

		public static UOSound LoadSound( int id )
		{
			if ( m_Buffer.ContainsKey( id ) )
				return m_Buffer[ id ];

			UOSound sound = Sounds.GetSound( id );
			m_Buffer.Add( id, sound );
			return sound;
		}
	}
}