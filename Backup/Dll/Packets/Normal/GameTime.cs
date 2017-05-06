using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x5B )]
	public class GameTime : Packet
	{
		private byte m_Hours;
		private byte m_Minutes;
		private byte m_Seconds;

		[PacketProp( 0 )]
		public byte Hours { get { return m_Hours; } }

		[PacketProp( 1 )]
		public byte Minutes { get { return m_Minutes; } }

		[PacketProp( 2 )]
		public byte Seconds { get { return m_Seconds; } }

		public GameTime( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Hours = reader.ReadByte();
			m_Minutes = reader.ReadByte();
			m_Seconds = reader.ReadByte();
		}
	}
}