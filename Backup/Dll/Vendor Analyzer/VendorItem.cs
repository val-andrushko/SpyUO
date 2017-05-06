using System;
using System.Text;

using SpyUO.Packets;

namespace SpyUO
{
	public class VendorItem : Item
	{
		#region Properties
		#region SellPrice
		private int m_SellPrice;

		/// <summary>
		/// Sell price per item.
		/// </summary>
		public int SellPrice
		{
			get { return m_SellPrice; }
			set { m_SellPrice = value; }
		}
		#endregion

		#region BuyPrice
		private int m_BuyPrice;

		/// <summary>
		/// Buy price per item.
		/// </summary>
		public int BuyPrice
		{
			get { return m_BuyPrice; }
			set { m_BuyPrice = value; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Constructs a new instance of <see cref="VendorItem"/>.
		/// </summary>
		public VendorItem( uint serial ) : base( serial )
		{
		}
		#endregion

		#region Methods
		#region ToString
		/// <summary>
		/// Returns string representing <see cref="VendorItem"/>.
		/// </summary>
		/// <returns>string representing <see cref="VendorItem"/>.</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat( "{0}\t", Name );
			builder.AppendFormat( "{0}\t", NameCliloc );
			builder.AppendFormat( "0x{0:X}\t", Serial );
			builder.AppendFormat( "0x{0:X}\t", ItemID );
			builder.AppendFormat( "0x{0:X}\t", Hue );
			builder.AppendFormat( "{0}\t", Amount );
			builder.AppendFormat( "{0}\t", m_SellPrice );
			builder.AppendFormat( "{0}\t", m_BuyPrice );

			return builder.ToString();
		}

		/// <summary>
		/// Returns string representing GenericBuyInfo.
		/// </summary>
		/// <returns>string representing <see cref="VendorItem"/>.</returns>
		public string ConstructInfo()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append( "Add( new GenericBuyInfo( " );
			builder.AppendFormat( " \"#{0}\", ", NameCliloc );
			builder.AppendFormat( "typeof( {0} ), ", Name );
			builder.AppendFormat( "{0}, ", SellPrice );
			builder.AppendFormat( "{0}, ", Amount );
			builder.AppendFormat( "0x{0:X}, ", ItemID );
			builder.AppendFormat( "0x{0:X} ", Hue );
			builder.AppendFormat( "); // {0}", LocalizedList.Construct( NameCliloc, null ) );

			return builder.ToString();
		}
		#endregion

		#region GetHeader
		/// <summary>
		/// Constructs header for all items.
		/// </summary>
		/// <param name="filter">Loot filter.</param>
		/// <returns>Item header.</returns>
		public static string GetHeader()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat( "Name\t" );
			builder.AppendFormat( "Cliloc\t" );
			builder.AppendFormat( "Serial\t" );
			builder.AppendFormat( "ItemID\t" );
			builder.AppendFormat( "Hue\t" );
			builder.AppendFormat( "Amount\t" );
			builder.AppendFormat( "Sell price\t" );
			builder.AppendFormat( "Buy price" );

			return builder.ToString();
		}
		#endregion
		#endregion
	}
}
