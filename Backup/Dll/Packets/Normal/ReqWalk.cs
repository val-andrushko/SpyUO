using System;

namespace SpyUO.Packets
{
	[PacketInfo(0x02)]
	public class ReqMove : Packet
	{
		private byte m_Direction;
		private byte m_Sequence;
		private uint m_Fastwalk;

		[PacketProp( 0 )]
		public byte Direction { get { return m_Direction; } }

		[PacketProp( 1 )]
		public byte Sequence { get { return m_Sequence; } }

		[PacketProp( 2, "0x{0:X}" )]
		public uint Fastwalk { get { return m_Fastwalk; } }

		public ReqMove( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Direction = reader.ReadByte();
			m_Sequence = reader.ReadByte();
			m_Fastwalk = reader.ReadUInt32();
		}
	}
}