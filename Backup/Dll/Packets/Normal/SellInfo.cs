using System;
using System.Text;
using System.Collections.Generic;

namespace SpyUO.Packets
{
	public class SellInfoEntry
	{
		private uint m_Serial;

		public uint Serial
		{
			get { return m_Serial; }
		}

		private short m_ItemID;

		public short ItemID
		{
			get { return m_ItemID; }
		}

		private short m_Hue;

		public short Hue
		{
			get { return m_Hue; }
		}

		private short m_Amount;

		public short Amount
		{
			get { return m_Amount; }
		}

		private short m_Price;

		public short Price
		{
			get { return m_Price; }
		}

		private string m_Name;

		public string Name
		{
			get { return m_Name; }
		}

		public SellInfoEntry( PacketReader reader )
		{
			m_Serial = reader.ReadUInt32();
			m_ItemID = reader.ReadInt16();
			m_Hue = reader.ReadInt16();
			m_Amount = reader.ReadInt16();
			m_Price = reader.ReadInt16();

			int size = reader.ReadInt16();
			m_Name = reader.ReadASCIIString( size );
		}

		public override string ToString()
		{
			return String.Format( "Serial: {0}, ItemID: {1}, Hue: {2}, Amount: {3}, Price: {4}, Name: {5};", 
				m_Serial.ToString( "X" ), m_ItemID.ToString( "X" ), m_Hue.ToString( "X" ), m_Amount, m_Price, m_Name );
		}
	}

	[PacketInfo( 0x9E )]
	public class SellInfo : Packet
	{
		private uint m_VendorSerial;
		private List<SellInfoEntry> m_List;

		[PacketProp( 0, "0x{0:X}" )]
		public uint VendorSerial { get { return m_VendorSerial; } }

		public List<SellInfoEntry> List { get { return m_List; } }

		[PacketProp( 1 )]
		public int ItemCount { get { return m_List.Count; } }

		[PacketProp( 2 )]
		public string Items
		{
			get
			{
				StringBuilder builder = new StringBuilder();

				foreach ( SellInfoEntry e in m_List )
					builder.AppendFormat( "{0} ", e );

				if ( builder.Length == 0 )
					builder.Append( "Empty" );

				return builder.ToString();
			}
		}

		public SellInfo( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadInt16();
			m_VendorSerial = reader.ReadUInt32();
			int count = reader.ReadByte();

			m_List = new List<SellInfoEntry>();

			for ( int i = 0; i < count; i++ )
				m_List.Add( new SellInfoEntry( reader ) );
		}
	}
}