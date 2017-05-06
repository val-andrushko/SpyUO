using System;
using System.Text;
using System.Collections.Generic;

namespace SpyUO.Packets
{
	public class BuyInfoEntry
	{
		private int m_Price;

		public int Price
		{
			get { return m_Price; }
		}

		private string m_Name;

		public string Name
		{
			get { return m_Name; }
		}

		public BuyInfoEntry( PacketReader reader )
		{
			m_Price = reader.ReadInt32();

			int size = reader.ReadByte();
			m_Name = reader.ReadASCIIString( size );
		}

		public override string ToString()
		{
			return String.Format( "Price: {0}, Name: {1};", m_Price, m_Name );
		}
	}

	[PacketInfo( 0x74 )]
	public class BuyInfo : Packet
	{
		private uint m_VendorSerial;
		private List<BuyInfoEntry> m_List;

		[PacketProp( 0, "0x{0:X}" )]
		public uint VendorSerial { get { return m_VendorSerial; } }

		public List<BuyInfoEntry> List { get { return m_List; } }

		[PacketProp( 1 )]
		public int ItemCount { get { return m_List.Count; } }

		[PacketProp( 2 )]
		public string Items
		{
			get
			{
				StringBuilder builder = new StringBuilder();

				foreach ( BuyInfoEntry e in m_List )
					builder.AppendFormat( "{0} ", e );

				if ( builder.Length == 0 )
					builder.Append( "Empty" );

				return builder.ToString();
			}
		}

		public BuyInfo( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadInt16();
			m_VendorSerial = reader.ReadUInt32();
			int count = reader.ReadByte();

			m_List = new List<BuyInfoEntry>();

			for ( int i = 0; i < count; i++ )
				m_List.Add( new BuyInfoEntry( reader ) );
		}
	}
}