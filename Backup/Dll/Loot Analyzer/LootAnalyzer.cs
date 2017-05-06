using System;
using System.IO;
using System.Collections.Generic;

using SpyUO;
using SpyUO.Packets;

namespace SpyUO
{
	public class LootAnalyzer
	{
		#region Properties
		private Dictionary<uint, Mobile> m_Mobiles;
		private Dictionary<uint, Mobile> m_Corpses;
		private Dictionary<uint, Mobile> m_CorpseContainers;
		private Dictionary<uint, Item> m_Items;

		#region LootFilter
		private LootFilter m_Filter;

		/// <summary>
		/// Loot filter.
		/// </summary>
		public LootFilter Filter
		{
			get { return m_Filter; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the SpyUO.LootAnalyzer.
		/// </summary>
		public LootAnalyzer()
		{
			m_Mobiles = new Dictionary<uint, Mobile>();
			m_Corpses = new Dictionary<uint, Mobile>();
			m_CorpseContainers = new Dictionary<uint, Mobile>();
			m_Items = new Dictionary<uint, Item>();

			m_Filter = new LootFilter();
		}
		#endregion

		#region Public
		#region AnalyzePacket
		/// <summary>
		/// Analyzes packet and saves it, if its part of the loot.
		/// </summary>
		/// <param name="packet">Packet</param>
		public void AnalyzePacket( Packet packet )
		{
			if ( packet is MobileIncoming )
			{
				MobileIncoming p = (MobileIncoming) packet;

				if ( !m_Mobiles.ContainsKey( p.Serial ) )
				{
					Mobile m = new Mobile( p.Serial );
					m.Body = p.ModelId;
					m.Hue = (int) p.Hue;
					m.Notoriety = (Notoriety) p.Notoriety;
					m.Female = ( p.Flag & 0x2 ) == 1 ? true : false;
					m.Blessed = ( p.Flag & 0x8 ) == 1 ? true : false;

					m_Mobiles.Add( p.Serial, m );
				}
				else
				{
					Mobile m = m_Mobiles[ p.Serial ];

					m.Body = p.ModelId;
					m.Hue = (int) p.Hue;
					m.Notoriety = (Notoriety) p.Notoriety;
					m.Female = ( p.Flag & 0x2 ) == 1 ? true : false;
					m.Blessed = ( p.Flag & 0x8 ) == 1 ? true : false;
				}
			}
			/*else if ( packet is MobileStat )
			{
				MobileStat p = (MobileStat) packet;

				if ( m_Mobiles.ContainsKey( p.Serial ) )
				{
					Mobile m = m_Mobiles[ p.Serial ];

					if ( m.Name == null )
						m.Name = p.Name;
				}
			}*/
			else if ( packet is DeathAnimation )
			{
				DeathAnimation p = (DeathAnimation) packet;

				if ( m_Mobiles.ContainsKey( p.Serial ) )
				{
					Mobile m = m_Mobiles[ p.Serial ];

					if ( m.Corpse == 0 )
					{
						m.Corpse = p.Corpse;

						if ( !m_Corpses.ContainsKey( p.Corpse ) )
							m_Corpses.Add( p.Corpse, m );
					}
				}
			}
			else if ( packet is ContainerContentUpdate )
			{
				ContainerContentUpdate p = (ContainerContentUpdate) packet;

				if ( m_Corpses.ContainsKey( p.ContSerial ) )
				{
					Mobile m = m_Corpses[ p.ContSerial ];

					if ( m.CorpseContainer == 0 )
					{
						m.CorpseContainer = p.Serial;
						m_Filter.AddMobile( m.Name );

						if ( !m_CorpseContainers.ContainsKey( p.Serial ) )
							m_CorpseContainers.Add( p.Serial, m );
					}
				}
			}
			else if ( packet is ContainerContent )
			{
				ContainerContent p = (ContainerContent) packet;

				foreach ( ContainerContent.ContainedItem i in p.Items )
				{
					if ( m_CorpseContainers.ContainsKey( i.ContSerial ) )
					{
						Mobile m = m_CorpseContainers[ i.ContSerial ];

						if ( !m.Loot.ContainsKey( i.Serial ) )
						{
							Item item;

							if ( !m_Items.ContainsKey( i.Serial ) )
							{
								item = new Item( i.Serial );
								m_Items.Add( i.Serial, item );
							}
							else
								item = m_Items[ i.Serial ];

							item.ItemID = i.ItemId;
							item.Hue = i.Hue;
							item.Amount = i.Amount;

							m.Loot.Add( i.Serial, item );
						}
					}
				}
			}
			else if ( packet is ObjectProperties )
			{
				ObjectProperties p = (ObjectProperties) packet;

				if ( m_Corpses.ContainsKey( p.Serial ) )
				{
					Mobile m = m_Corpses[ p.Serial ];

					if ( m.CorpseName == null && p.Properties.Length > 0 )
					{
						ObjectProperties.Property prop = p.Properties[ 0 ];
						m.CorpseName = LocalizedList.Construct( prop.Number, prop.Arguments );
					}
				}
				else if ( m_Mobiles.ContainsKey( p.Serial ) )
				{
					Mobile m = m_Mobiles[ p.Serial ];

					if ( p.Properties.Length > 0 )
						m.Name = LocalizedList.Construct( p.Properties[ 0 ].Number, p.Properties[ 0 ].Arguments );
				}
				else
				{
					Item item;

					if ( !m_Items.ContainsKey( p.Serial ) )
					{
						item = new Item( p.Serial );
						m_Items.Add( p.Serial, item );
					}
					else
						item = m_Items[ p.Serial ];

					if ( item.Name != null )
						return;

					for ( int i = 0; i < p.Properties.Length; i++ )
					{
						ObjectProperties.Property prop = p.Properties[ i ];

						item.ParseProperty( i, prop.Number, prop.Arguments );
					}
				}
			}
		}
		#endregion

		#region PrintReport
		/// <summary>
		/// Saves filtered loot to a file.
		/// </summary>
		/// <param name="fileName">File name.</param>
		public void PrintReport( string fileName )
		{
			TextWriter writer = new StreamWriter( fileName );

			writer.WriteLine( Item.GetHeader( m_Filter ) );

			int totalGold = 0, minGold = Int32.MaxValue, maxGold = 0;
			int totalItems = 0, sumItems = 0, minItems = Int32.MaxValue, maxItems = 0;
			int counter = 0;

			int[] items = new int[ 100 ];
			int[] props = new int[ 100 ];

			foreach ( uint ms in m_Mobiles.Keys )
			{
				Mobile m = m_Mobiles[ ms ];

				if ( m_Filter.Filters( m.Name ) && m.Loot.Count > 0 )
				{
					foreach ( KeyValuePair<uint, Item> kvp in m.Loot )
					{
						Item item = kvp.Value;

						if ( !m_Filter.Filters( item ) )
						{
							if ( ( item.IsWearable && m_Filter.Wearable ) || ( item.IsOther && m_Filter.Other ) )
							{
								writer.WriteLine( kvp.Value.ToString( m_Filter ) );
								sumItems++;
							}

							if ( item.IsGold )
							{
								totalGold += item.Amount;

								if ( item.Amount > maxGold )
									maxGold = item.Amount;
								if ( item.Amount < minGold )
									minGold = item.Amount;
							}

							props[ item.SpecialProps ]++;
						}
					}

					if ( sumItems > 0 )
						writer.WriteLine();

					if ( sumItems > maxItems )
						maxItems = sumItems;
					if ( sumItems > 0 && sumItems < minItems )
						minItems = sumItems;

					totalItems += sumItems;
					counter++;
					items[ sumItems ]++;
					sumItems = 0;
				}
			}

			writer.WriteLine();
			writer.WriteLine( "Item count\tNumber of corpses\t\tProp count\tNumber of items" );

			int sumFullCorpses = 0;

			for ( int i = 1; i < items.Length; i++ )
			{
				sumFullCorpses += items[ i ];

				if ( items[ i ] > 0 || props[ i ] > 0 )
					writer.WriteLine( "{0}\t{1}\t\t{2}\t{3}", i, items[ i ], i, props[ i ] );
			}

			writer.WriteLine( "0\t{0}", counter - sumFullCorpses );

			writer.WriteLine();
			writer.WriteLine( "{0} items in {1} corpses, {2}/{3}/{4} items per corpse", totalItems, counter, minItems, totalItems / (double) counter, maxItems );
			writer.WriteLine( "{0} gold in {1} corpses, {2}/{3}/{4} gold per corpse", totalGold, counter, minGold, totalGold / (double) counter, maxGold );
			writer.WriteLine();
			writer.WriteLine( Mobile.GetHeader() );

			foreach ( uint ms in m_Mobiles.Keys )
			{
				Mobile m = m_Mobiles[ ms ];

				if ( m_Filter.Filters( m.Name ) )
					writer.WriteLine( m );
			}

			writer.Close();
		}
		#endregion
		#endregion
	}
}
