using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x4E )]
	public class SetPersonalLight : Packet
	{
		private uint m_Serial;
		private byte m_Level;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public byte Level { get { return m_Level; } }

		public SetPersonalLight( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			m_Level = reader.ReadByte();
		}
	}
}