using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x55 )]
	public class LoginComplete : Packet
	{
		public LoginComplete( PacketReader reader, bool send ) : base( reader, send )
		{
		}
	}
}