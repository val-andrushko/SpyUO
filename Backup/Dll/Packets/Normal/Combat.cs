using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x72 )]
	public class Combat : Packet
	{
		private byte m_WarMode;
		private uint m_Unknown;

		[PacketProp( 0 )]
		public byte WarMode { get { return m_WarMode; } }

		[PacketProp( 1 )]
		public uint Unknown { get { return m_Unknown; } }

		public Combat( PacketReader reader, bool send ) : base( reader, send )
		{
			m_WarMode = reader.ReadByte();
			m_Unknown = reader.ReadUInt32();
		}
	}
}