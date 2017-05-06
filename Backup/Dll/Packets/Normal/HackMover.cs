using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x32 )]
	public class HackMover : Packet
	{
		private byte m_Unknown;

		[PacketProp( 0 )]
		public byte Unknown { get { return m_Unknown; } }

		public HackMover( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Unknown = reader.ReadByte();
		}
	}
}