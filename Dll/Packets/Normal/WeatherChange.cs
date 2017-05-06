using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x65 )]
	public class WeatherChange : Packet
	{
		private byte m_Type;
		private byte m_Number;
		private byte m_Temperature;
		private string m_Unk;

		[PacketProp( 0 )]
		public byte Type { get { return m_Type; } }
		[PacketProp( 1 )]
		public byte Number { get { return m_Number; } }
		[PacketProp( 2 )]
		public byte Temperature { get { return m_Temperature; } }
		[PacketProp( 3 )]
		public string Unk { get { return m_Unk; } }

		public WeatherChange( PacketReader reader, bool send ) : base( reader, send )
		{
			m_Type = reader.ReadByte();
			m_Number = reader.ReadByte();
			m_Temperature = reader.ReadByte();

			byte[] tounk = new byte[ reader.Data.Length - 3 ];
			StringBuilder sb = new StringBuilder();

			for ( int i = 0; i < ( reader.Data.Length - 3 ); i++ )
			{
				sb.Append( ( reader.ReadByte() ).ToString( "x" ) );
				sb.Append( "-" );
			}

			m_Unk = sb.ToString();
		}
	}
}