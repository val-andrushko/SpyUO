using System;
using System.Collections.Generic;
using System.Text;

namespace SpyUO
{
	public class Vendor : Mobile
	{
		#region Properties
		#region Shop
		private Dictionary<uint, VendorItem> m_Shop;

		/// <summary>
		/// Contains items, vendor can sell.
		/// </summary>
		public Dictionary<uint, VendorItem> Shop
		{
			get { return m_Shop; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes new instance of <see cref="Vendor"/>.
		/// </summary>
		/// <param name="serial">Vendor serial</param>
		public Vendor( uint serial ) : base( serial )
		{
			m_Shop = new Dictionary<uint, VendorItem>();
		}
		#endregion

		#region Methods
		#endregion
	}
}
