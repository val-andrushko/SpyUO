using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x1B )]
	public class LoginConfirm : Packet
	{
		private uint m_Serial;
		private uint m_Zero;
		private ushort m_ID;
		private ushort m_X;
		private ushort m_Y;
		private ushort m_Z;
		private byte m_Direction;
		private uint m_Unknown1;
		private uint m_Unknown2;
		private ushort m_Unknown3;
		private byte m_Status;
		private ushort m_HighlightColor;
		private ushort m_Unknown4;
		private uint m_Unknown5;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public uint Zero1 { get { return m_Zero; } }

		[PacketProp( 2 )]
		public ushort ID { get { return m_ID; } }

		[PacketProp( 3 )]
		public ushort X { get { return m_X; } }

		[PacketProp( 4 )]
		public ushort Y { get { return m_Y; } }

		[PacketProp( 6 )]
		public ushort Z { get { return m_Z; } }

		[PacketProp( 7 )]
		public byte Direction { get { return m_Direction; } }

		[PacketProp( 8 )]
		public uint Unknown1 { get { return m_Unknown1; } }

		[PacketProp( 9 )]
		public uint Unknown2 { get { return m_Unknown2; } }

		[PacketProp( 10 )]
		public ushort Unknown3 { get { return m_Unknown3; } }

		[PacketProp( 10 )]
		public byte Status { get { return m_Status; } }

		[PacketProp( 10 )]
		public ushort HighlightColor { get { return m_HighlightColor; } }

		[PacketProp( 10 )]
		public ushort Unknown4 { get { return m_Unknown4; } }

		[PacketProp( 10 )]
		public uint Unknown5 { get { return m_Unknown5; } }

		public LoginConfirm( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Zero = reader.ReadUInt32();
			m_ID = reader.ReadUInt16();
			m_X = reader.ReadUInt16();
			m_Y = reader.ReadUInt16();
			m_Z = reader.ReadUInt16();
			m_Direction = reader.ReadByte();
			m_Unknown1 = reader.ReadUInt32();
			m_Unknown2 = reader.ReadUInt32();
			m_Unknown3 = reader.ReadUInt16();
			m_Status = reader.ReadByte();
			m_HighlightColor = reader.ReadUInt16();
			m_Unknown4 = reader.ReadUInt16();
			m_Unknown5 = reader.ReadUInt32();
		}
	}
}