using System;
using System.IO;
using System.Collections.Generic;

using SpyUO.Packets;

namespace SpyUO
{
	public class VendorAnalyzer
	{
		#region Properties
		private Dictionary<uint, Vendor> m_Vendors;
		private Dictionary<uint, Item> m_Items;
		private Dictionary<uint, Vendor> m_Shops;

		private int[] m_Prices;
		private int m_Counter;
		#endregion

		#region Constructors
		public VendorAnalyzer()
		{
			m_Vendors = new Dictionary<uint, Vendor>();
			m_Items = new Dictionary<uint, Item>();
			m_Shops = new Dictionary<uint, Vendor>();

			m_Prices = null;
			m_Counter = 0;
		}
		#endregion

		#region Methods
		#region AnalyzePacket
		/// <summary>
		/// Analyzes packet and saves it, if it is related to vendors.
		/// </summary>
		/// <param name="packet">Packet.</param>
		public void AnalyzePacket( Packet packet )
		{
			if ( packet is MobileIncoming )
			{
				MobileIncoming p = (MobileIncoming) packet;

				if ( !m_Vendors.ContainsKey( p.Serial ) && p.Notoriety == Notoriety.Invulnerable )
				{
					Vendor m = new Vendor( p.Serial );
					m.Body = p.ModelId;
					m.Hue = (int) p.Hue;
					m.Notoriety = (Notoriety) p.Notoriety;
					m.Female = ( p.Flag & 0x2 ) == 1 ? true : false;
					m.Blessed = ( p.Flag & 0x8 ) == 1 ? true : false;

					foreach ( MobileIncoming.EquipInfo i in p.Equipment )
					{
						Item item = new Item( i.Serial );
						item.ItemID = i.ItemId;
						item.Layer = (Layer) i.Layer;
						item.Hue = i.Hue;
						m.Items.Add( item );
						m_Items.Add( i.Serial, item );

						if ( item.Layer == Layer.ShopBuy )
							m_Shops.Add( item.Serial, m );
					}

					m_Vendors.Add( p.Serial, m );
				}
			}
			else if ( packet is ContainerContent )
			{
				ContainerContent p = (ContainerContent) packet;

				foreach ( ContainerContent.ContainedItem i in p.Items )
				{
					if ( m_Shops.ContainsKey( i.ContSerial ) )
					{
						Vendor m = m_Shops[ i.ContSerial ];

						if ( !m.Shop.ContainsKey( i.Serial ) )
						{
							VendorItem item = new VendorItem( i.Serial );

							if ( !m_Items.ContainsKey( i.Serial ) )
								m_Items.Add( i.Serial, item );

							item.ItemID = i.ItemId;
							item.Hue = i.Hue;
							item.Amount = i.Amount;

							m.Shop.Add( i.Serial, item );
						}
					}
				}
			}
			else if ( packet is BuyInfo )
			{
				BuyInfo p = (BuyInfo) packet;

				if ( m_Shops.ContainsKey( p.VendorSerial ) )
				{
					m_Prices = new int[ p.ItemCount ];
					m_Counter = 0;

					for ( int i = 0; i < p.ItemCount; i++ )
						m_Prices[ i ] = p.List[ i ].Price;
				}
			}
			else if ( packet is ObjectProperties )
			{
				ObjectProperties p = (ObjectProperties) packet;

				if ( m_Items.ContainsKey( p.Serial ) )
				{
					Item item = m_Items[ p.Serial ];

					if ( item.Name != null )
						return;

					for ( int i = 0; i < p.Properties.Length; i++ )
					{
						ObjectProperties.Property prop = p.Properties[ i ];
						item.ParseProperty( i, prop.Number, prop.Arguments );
					}

					if ( item is VendorItem && m_Prices != null && m_Counter < m_Prices.Length )
					{
						( (VendorItem) item ).SellPrice = m_Prices[ m_Counter++ ];
					}
				}
				else if ( m_Vendors.ContainsKey( p.Serial ) )
				{
					Mobile m = m_Vendors[ p.Serial ];

					if ( m.Name == null && p.Properties.Length > 0 )
						m.Name = LocalizedList.Construct( p.Properties[ 0 ].Number, p.Properties[ 0 ].Arguments );
				}
			}
		}
		#endregion

		#region PrintReport
		/// <summary>
		/// Saves vendor info to a file.
		/// </summary>
		/// <param name="fileName">File name.</param>
		public void PrintReport( string fileName )
		{
			TextWriter writer = new StreamWriter( fileName );

			foreach ( KeyValuePair<uint, Vendor> kvp in m_Vendors )
			{
				Vendor v = kvp.Value;

				if ( v != null && v.Shop.Count > 0 )
				{
					writer.WriteLine( v.Name );
					writer.WriteLine( VendorItem.GetHeader() );
					
					foreach ( KeyValuePair<uint, VendorItem> pvk in v.Shop )
					{
						writer.WriteLine( pvk.Value );
					}

					writer.WriteLine();
					writer.WriteLine( "SBInfo" );

					foreach ( KeyValuePair<uint, VendorItem> pvk in v.Shop )
					{
						writer.WriteLine( pvk.Value.ConstructInfo() );
					}

					writer.WriteLine();
					writer.WriteLine();
				}
			}

			writer.Close();
		}
		#endregion
		#endregion
	}
}
