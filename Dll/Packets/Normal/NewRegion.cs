using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x58 )]
	public class NewRegion : Packet
	{
		private string m_Name;
		private uint m_Unknown;
		private short m_StartX;
		private short m_StartY;
		private short m_Width;
		private short m_Height;
		private short m_StartZ;
		private short m_EndZ;
		private string m_Description;
		private short m_Sound;
		private short m_Music;
		private short m_NightSound;
		private byte m_Dungeon;
		private short m_Light;
		
		[PacketProp( 0 )]
		public string Name { get { return m_Name; } }

		[PacketProp( 1 )]
		public uint Unknown { get { return m_Unknown; } }

		[PacketProp( 2 )]
		public short StartX { get { return m_StartX; } }

		[PacketProp( 3 )]
		public short StartY { get { return m_StartY; } }

		[PacketProp( 4 )]
		public short Width { get { return m_Width; } }

		[PacketProp( 5 )]
		public short Height { get { return m_Height; } }

		[PacketProp( 6 )]
		public short StartZ { get { return m_StartZ; } }

		[PacketProp( 7 )]
		public short EndZ { get { return m_EndZ; } }

		[PacketProp( 8 )]
		public string Description { get { return m_Description; } }

		[PacketProp( 9 )]
		public short Sound { get { return m_Sound; } }

		[PacketProp( 10 )]
		public short Music { get { return m_Music; } }

		[PacketProp( 11 )]
		public short NightSound { get { return m_NightSound; } }

		[PacketProp( 12 )]
		public byte Dungeon { get { return m_Dungeon; } }

		[PacketProp( 12 )]
		public short Light { get { return m_Light; } }

		public NewRegion( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Name = reader.ReadASCIIString( 40 );
			m_Unknown = reader.ReadUInt32();
			m_StartX = reader.ReadInt16();
			m_StartY = reader.ReadInt16();
			m_Width = reader.ReadInt16();
			m_Height = reader.ReadInt16();
			m_StartZ = reader.ReadInt16();
			m_EndZ = reader.ReadInt16();
			m_Description = reader.ReadASCIIString( 40 );
			m_Sound = reader.ReadInt16();
			m_Music = reader.ReadInt16();
			m_NightSound = reader.ReadInt16();
			m_Dungeon = reader.ReadByte();
			m_Light = reader.ReadInt16();
		}
	}
}