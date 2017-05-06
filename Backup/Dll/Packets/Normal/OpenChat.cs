using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0xB5 )]
	public class OpenChat : Packet
	{
		private string m_ChatName;

		[PacketProp( 0 )]
		public string ChatName { get { return m_ChatName; } }

		public OpenChat( PacketReader reader, bool send ) : base( reader, send )
		{
			m_ChatName = reader.ReadASCIIString( 0x39 );
		}
	}
}