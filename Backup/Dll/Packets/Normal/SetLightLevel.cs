using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x4F )]
	public class SetLightLevel : Packet
	{
		private byte m_Level;

		[PacketProp( 0 )]
		public byte Level { get { return m_Level; } }

		public SetLightLevel( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Level = reader.ReadByte();
		}
	}
}