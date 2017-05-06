using System;

namespace SpyUO.Packets
{
	[PacketInfo( 0x20 )]
	public class MoveMobile : Packet
	{
		private uint m_Serial;
		private ushort m_Body;
		private byte m_Zero1;
		private ushort m_SkinColor;
		private byte m_Status;
		private ushort m_X;
		private ushort m_Y;
		private ushort m_Zero2;
		private byte m_Direction;
		private sbyte m_Z;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public ushort ID { get { return m_Body; } }

		[PacketProp( 2 )]
		public byte Zero1 { get { return m_Zero1; } }

		[PacketProp( 3 )]
		public ushort SkinColor { get { return m_SkinColor; } }

		[PacketProp( 4 )]
		public byte Status { get { return m_Status; } }

		[PacketProp( 5 )]
		public ushort X { get { return m_X; } }

		[PacketProp( 6 )]
		public ushort Y { get { return m_Y; } }

		[PacketProp( 7 )]
		public ushort Zero2 { get { return m_Zero2; } }

		[PacketProp( 8 )]
		public byte Direction { get { return m_Direction; } }

		[PacketProp( 9 )]
		public sbyte Z { get { return m_Z; } }

		public MoveMobile( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Body = reader.ReadUInt16();
			m_Zero1 = reader.ReadByte();
			m_SkinColor = reader.ReadUInt16();
			m_Status = reader.ReadByte();
			m_X = reader.ReadUInt16();
			m_Y = reader.ReadUInt16();
			m_Zero2 = reader.ReadUInt16();
			m_Direction = reader.ReadByte();
			m_Z = reader.ReadSByte();
		}
	}
}