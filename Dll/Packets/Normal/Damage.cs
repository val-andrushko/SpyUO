using System;

namespace SpyUO.Packets
{
	[PacketInfo( 0xB )]
	public class Damage : Packet
	{
		private uint m_Serial;
		private short m_Amount;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public short Amount { get { return m_Amount; } }

		public Damage( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Amount = reader.ReadInt16();
		}
	}
}