using System;

namespace SpyUO.Packets
{
	[PacketInfo( 0x2D )]
	public class Health : Packet
	{
		private uint m_Serial;
		private ushort m_MaxHits;
		private ushort m_Hits;
		private ushort m_MaxMana;
		private ushort m_Mana;
		private ushort m_MaxStama;
		private ushort m_Stama;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public ushort MaxHits { get { return m_MaxHits; } }

		[PacketProp( 2 )]
		public ushort Hits { get { return m_Hits; } }

		[PacketProp( 3 )]
		public ushort MaxMana { get { return m_MaxMana; } }

		[PacketProp( 4 )]
		public ushort Mana { get { return m_Mana; } }

		[PacketProp( 5 )]
		public ushort MaxStama { get { return m_MaxStama; } }

		[PacketProp( 6 )]
		public ushort Stama { get { return m_Stama; } }

		public Health( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_MaxHits = reader.ReadUInt16();
			m_Hits = reader.ReadUInt16();
			m_MaxMana = reader.ReadUInt16();
			m_Mana = reader.ReadUInt16();
			m_MaxStama = reader.ReadUInt16();
			m_Stama = reader.ReadUInt16();
		}
	}
}