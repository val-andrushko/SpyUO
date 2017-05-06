using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x73 )]
	public class PingPong : Packet
	{
		private byte m_Value;

		[PacketProp( 0 )]
		public byte Value { get { return m_Value; } }

		public PingPong( PacketReader reader, bool send ) : base( reader, send )
		{
			// client sends this every 61 seconds
			m_Value = reader.ReadByte();
		}
	}
}