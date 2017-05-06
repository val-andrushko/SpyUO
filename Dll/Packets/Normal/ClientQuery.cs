using System;

namespace SpyUO.Packets
{
	[PacketInfo( 0x34 )]
	public class ClientQuery : Packet
	{
		private uint m_Fixed;
		private string m_Type;
		private uint m_Serial;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Fixed { get { return m_Fixed; } }

		[PacketProp( 1 )]
		public string Type { get { return m_Type; } }

		[PacketProp( 2, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		public ClientQuery( PacketReader reader, bool send ) : base( reader, send )
		{
			byte totype = 0;

			m_Fixed = reader.ReadUInt32();
			totype = reader.ReadByte();
			m_Serial = reader.ReadUInt32();

			if ( totype.CompareTo( 0x00 ) == 0 )
				m_Type = "God Client";
			else if ( totype.CompareTo( 0x04 ) == 0 )
				m_Type = "Basic Status: 0x11";
			else if ( totype.CompareTo( 0x05 ) == 0 )
				m_Type = "Request Skill: 0x3A";
			else
				m_Type = "Unk Type: 0x" + totype.ToString( "X" );
		}
	}
}