using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0xDC )]
	public class PropertyListInfo : Packet
	{
		private uint m_Serial;
		private uint m_Hash;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1, "0x{0:X}" )]
		public uint Hash { get { return m_Hash; } }

		public PropertyListInfo( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Hash = reader.ReadUInt32();
		}
	}
}