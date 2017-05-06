using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x77 )]
	public class NakedMob : Packet
	{
		private uint m_Serial;
		private ushort m_ID;
		private ushort m_X;
		private ushort m_Y;
		private sbyte m_Z;
		private sbyte m_Direction;
		private ushort m_SkinColor;
		private byte m_Status;
		private Notoriety m_Notoriety;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Type { get { return m_Serial; } }

		[PacketProp( 1 )]
		public ushort ID { get { return m_ID; } }

		[PacketProp( 2 )]
		public ushort X { get { return m_X; } }

		[PacketProp( 3 )]
		public ushort Y { get { return m_Y; } }

		[PacketProp( 4 )]
		public sbyte Z { get { return m_Z; } }

		[PacketProp( 5 )]
		public sbyte Direction { get { return m_Direction; } }

		[PacketProp( 6 )]
		public ushort SkinColor { get { return m_SkinColor; } }

		[PacketProp( 7 )]
		public byte Status { get { return m_Status; } }

		[PacketProp( 8 )]
		public Notoriety Nototiety { get { return m_Notoriety; } }

		public NakedMob( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_ID = reader.ReadUInt16();
			m_X = reader.ReadUInt16();
			m_Y = reader.ReadUInt16();
			m_Z = reader.ReadSByte();
			m_Direction = reader.ReadSByte();
			m_SkinColor = reader.ReadUInt16();
			m_Status = reader.ReadByte();
			m_Notoriety = (Notoriety) reader.ReadByte();
		}
	}
}