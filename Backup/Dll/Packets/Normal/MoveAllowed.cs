using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x22 )]
	public class MoveAllowed : Packet
	{
		private byte m_Accepted;
		private byte m_Status;

		[PacketProp( 0 )]
		public byte Accepted { get { return m_Accepted; } }

		[PacketProp( 1 )]
		public byte Status { get { return m_Status; } }

		public MoveAllowed( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Accepted = reader.ReadByte();
			m_Status = reader.ReadByte();
		}
	}
}