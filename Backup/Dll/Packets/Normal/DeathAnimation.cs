using System;
using System.Text;
using System.Collections;

namespace SpyUO.Packets
{
	[PacketInfo( 0xAF )]
	public class DeathAnimation : Packet
	{
		private uint m_Serial;
		private uint m_Corpse;
		private uint m_Unknown;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1, "0x{0:X}" )]
		public uint Corpse { get { return m_Corpse; } }

		[PacketProp( 2, "0x{0:X}" )]
		public uint Unknown { get { return m_Unknown; } }

		public DeathAnimation( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Corpse = reader.ReadUInt32();
			m_Unknown = reader.ReadUInt32();
		}
	}
}