using System;
using System.Text;
using System.Collections.Generic;

namespace SpyUO
{
	public enum Notoriety
	{
		Innocent = 1,
		Ally = 2,
		CanBeAttacked = 3,
		Criminal = 4,
		Enemy = 5,
		Murderer = 6,
		Invulnerable = 7
	}

	public class Mobile
	{
		#region Serial
		private uint m_Serial;

		/// <summary>
		/// Mobile serial.
		/// </summary>
		public uint Serial
		{
			get { return m_Serial; }
		}
		#endregion

		#region Name
		private string m_Name;

		/// <summary>
		/// Mobile name.
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		#endregion

		#region Body
		private int m_Body;

		/// <summary>
		/// Mobile body id.
		/// </summary>
		public int Body
		{
			get { return m_Body; }
			set { m_Body = value; }
		}
		#endregion

		#region Hue
		private int m_Hue;

		/// <summary>
		/// Mobile hue.
		/// </summary>
		public int Hue
		{
			get { return m_Hue; }
			set { m_Hue = value; }
		}
		#endregion

		#region Notoriety
		private Notoriety m_Notoriety;

		/// <summary>
		/// Mobile notoriety.
		/// </summary>
		public Notoriety Notoriety
		{
			get { return m_Notoriety; }
			set { m_Notoriety = value; }
		}
		#endregion

		#region Female
		private bool m_Female;

		/// <summary>
		/// Determines whether this mobile is female.
		/// </summary>
		public bool Female
		{
			get { return m_Female; }
			set { m_Female = value; }
		}
		#endregion

		#region Blessed
		private bool m_Blessed;

		/// <summary>
		/// Determines whether this mobile is blessed.
		/// </summary>
		public bool Blessed
		{
			get { return m_Blessed; }
			set { m_Blessed = value; }
		}
		#endregion

		#region IsParagon
		/// <summary>
		/// Determines whether this mobile is paragon.
		/// </summary>
		public bool IsParagon
		{
			get { return m_Hue == 0x501; }
		}
		#endregion

		#region Corpse
		private uint m_Corpse;

		/// <summary>
		/// Serial of the corpse.
		/// </summary>
		public uint Corpse
		{
			get { return m_Corpse; }
			set { m_Corpse = value; }
		}

		private string m_CorpseName;

		/// <summary>
		/// Corpse name.
		/// </summary>
		public string CorpseName
		{
			get { return m_CorpseName; }
			set { m_CorpseName = value; }
		}

		private uint m_CorpseContainer;

		/// <summary>
		/// Corpse container serial.
		/// </summary>
		public uint CorpseContainer
		{
			get { return m_CorpseContainer; }
			set { m_CorpseContainer = value; }
		}
		#endregion

		#region Loot
		private Dictionary<uint, Item> m_Loot;

		/// <summary>
		/// Loot.
		/// </summary>
		public Dictionary<uint, Item> Loot
		{
			get { return m_Loot; }
		}
		#endregion

		#region Items
		private List<Item> m_Items;

		public List<Item> Items
		{
			get { return m_Items; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the SpyUO.Mobile class with specific serial.
		/// </summary>
		/// <param name="serial">Mobile serial.</param>
		public Mobile( uint serial )
		{
			m_Serial = serial;
			m_Loot = new Dictionary<uint, Item>();
			m_Items = new List<Item>();
		}
		#endregion

		#region Methods
		#region ToString
		/// <summary>
		/// Returns a string that represents this mobile.
		/// </summary>
		/// <returns>A string that represents this mobile.</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat( "0x{0}\t", m_Serial.ToString( "X8" ) );
			builder.AppendFormat( "{0}\t", m_Name );
			builder.AppendFormat( "0x{0}\t", m_Body.ToString( "X" ) );
			builder.AppendFormat( "0x{0}\t", m_Hue.ToString( "X" ) );
			builder.AppendFormat( "{0}\t", m_Notoriety );
			builder.AppendFormat( "{0}\t", m_Female );
			builder.AppendFormat( "{0}\t", m_Blessed );
			builder.AppendFormat( "{0}", m_CorpseName );

			return builder.ToString();
		}
		#endregion

		#region GetHeader
		/// <summary>
		/// Returns a string that represents the header of this mobile.
		/// </summary>
		/// <returns>A string that represents the header of this mobile.</returns>
		public static string GetHeader()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat( "Serial\t" );
			builder.AppendFormat( "Name\t" );
			builder.AppendFormat( "Body\t" );
			builder.AppendFormat( "Hue\t" );
			builder.AppendFormat( "Notoriety\t" );
			builder.AppendFormat( "Female\t" );
			builder.AppendFormat( "Blessed\t" );
			builder.AppendFormat( "CorpseName" );

			return builder.ToString();
		}
		#endregion
		#endregion
	}
}
