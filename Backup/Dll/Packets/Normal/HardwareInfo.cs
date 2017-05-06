using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0xD9 )]
	public class HardwareInfo : Packet
	{
		private ushort m_PacketSize;
		private byte m_ClientType;
		private uint m_IstanceID;
		private uint m_OSMajor;
		private uint m_OSMinor;
		private uint m_OSRevision;
		private byte m_CPUManuf;
		private uint m_CPUFamily;
		private uint m_CPUModel;
		private uint m_ClockSpeed;
		private byte m_CPUQuantity;
		private uint m_PhysicalMemory;
		private uint m_ScreenWidth;
		private uint m_ScreenHeight;
		private uint m_ScreenDepth;
		private ushort m_DirectXMajor;
		private ushort m_DirectXMinor;
		private string m_VideoDescription;
		private uint m_VideoVendorID;
		private uint m_VideoDeviceID;
		private uint m_VideoMemory;
		private byte m_Distribution;
		private byte m_ClientsRunning;
		private byte m_ClientsInstalled;
		private byte m_PartialInstalled;
		private string m_Language;
		private string m_Unknown;

		//private string m_Unk;
		#region Props
		[PacketProp( 0, "0x{0:X}" )]
		public ushort PacketSize { get { return m_PacketSize; } }

		[PacketProp( 1 )]
		public byte ClientType { get { return m_ClientType; } }

		[PacketProp( 2, "0x{0:X}" )]
		public uint IstanceID { get { return m_IstanceID; } }

		[PacketProp( 3 )]
		public uint OSMajor { get { return m_OSMajor; } }

		[PacketProp( 4 )]
		public uint OSMinor { get { return m_OSMinor; } }

		[PacketProp( 5 )]
		public uint OSRevision { get { return m_OSRevision; } }

		[PacketProp( 6 )]
		public byte CPUManuf { get { return m_CPUManuf; } }

		[PacketProp( 7 )]
		public uint CPUFamily { get { return m_CPUFamily; } }

		[PacketProp( 8 )]
		public uint CPUModel { get { return m_CPUModel; } }

		[PacketProp( 9 )]
		public uint CPUClockSpeed { get { return m_ClockSpeed; } }

		[PacketProp( 10 )]
		public byte CPUQuantity { get { return m_CPUQuantity; } }

		[PacketProp( 11 )]
		public uint PhysicalMemory { get { return m_PhysicalMemory; } }

		[PacketProp( 12 )]
		public uint ScreenWidth { get { return m_ScreenWidth; } }

		[PacketProp( 13 )]
		public uint ScreenHeight { get { return m_ScreenHeight; } }

		[PacketProp( 14 )]
		public uint ScreenDepth { get { return m_ScreenDepth; } }

		[PacketProp( 15 )]
		public ushort DirectXMajor { get { return m_DirectXMajor; } }

		[PacketProp( 16 )]
		public ushort DirectXMinor { get { return m_DirectXMinor; } }

		[PacketProp( 17 )]
		public string VideoDescription { get { return m_VideoDescription; } }

		[PacketProp( 18 )]
		public uint VideoVendorID { get { return m_VideoVendorID; } }

		[PacketProp( 19 )]
		public uint VideoDeviceID { get { return m_VideoDeviceID; } }

		[PacketProp( 20 )]
		public uint VideoMemory { get { return m_VideoMemory; } }

		[PacketProp( 21 )]
		public byte Distribution { get { return m_Distribution; } }

		[PacketProp( 22 )]
		public byte ClientsRunning { get { return m_ClientsRunning; } }

		[PacketProp( 23 )]
		public byte ClientsInstalled { get { return m_ClientsInstalled; } }

		[PacketProp( 24 )]
		public byte PartialInstalled { get { return m_PartialInstalled; } }

		[PacketProp( 25 )]
		public string Language { get { return m_Language; } }

		[PacketProp( 26 )]
		public string Unknown { get { return m_Unknown; } }
		#endregion

		public HardwareInfo( PacketReader reader, bool send ) : base( reader, send )
		{
			m_PacketSize = reader.ReadUInt16();
			m_ClientType = reader.ReadByte();
			m_IstanceID = reader.ReadUInt32();
			m_OSMajor = reader.ReadUInt32();
			m_OSMinor = reader.ReadUInt32();
			m_OSRevision = reader.ReadUInt32();
			m_CPUManuf = reader.ReadByte();
			m_CPUFamily = reader.ReadUInt32();
			m_CPUModel = reader.ReadUInt32();
			m_ClockSpeed = reader.ReadUInt32();
			m_CPUQuantity = reader.ReadByte();
			m_PhysicalMemory = reader.ReadUInt32();
			m_ScreenWidth = reader.ReadUInt32();
			m_ScreenHeight = reader.ReadUInt32();
			m_ScreenDepth = reader.ReadUInt32();
			m_DirectXMajor = reader.ReadUInt16();
			m_DirectXMinor = reader.ReadUInt16();
			m_VideoDescription = reader.ReadASCIIString( 64 );
			m_VideoVendorID = reader.ReadUInt32();
			m_VideoDeviceID = reader.ReadUInt32();
			m_VideoMemory = reader.ReadUInt32();
			m_Distribution = reader.ReadByte();
			m_ClientsRunning = reader.ReadByte();
			m_ClientsInstalled = reader.ReadByte();
			m_PartialInstalled = reader.ReadByte();
			m_Language = reader.ReadASCIIString( 4 );
			m_Unknown = reader.ReadASCIIString( 64 );
		}
	}
}