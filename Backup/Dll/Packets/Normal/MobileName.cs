using System;

namespace SpyUO.Packets
{
	[PacketInfo( 0x98 )]
	public class MobileName : Packet
	{
		private uint m_Serial;
		private string m_Name;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public string Name { get { return m_Name; } }

		public MobileName( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadInt16();
			m_Serial = reader.ReadUInt32();
			m_Name = reader.ReadASCIIString( Math.Min( size - 7, 30 ) );
		}
	}
}