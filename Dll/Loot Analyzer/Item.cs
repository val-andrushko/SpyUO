using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using SpyUO.Packets;
using Ultima;

namespace SpyUO
{
	/// <summary>
	/// Enumeration of item layer values.
	/// </summary>
	public enum Layer : byte
	{
		/// <summary>
		/// Invalid layer.
		/// </summary>
		Invalid		 = 0x00,
		/// <summary>
		/// First valid layer. Equivalent to <c>Layer.OneHanded</c>.
		/// </summary>
		FirstValid   = 0x01,
		/// <summary>
		/// One handed weapon.
		/// </summary>
		OneHanded	 = 0x01,
		/// <summary>
		/// Two handed weapon or shield.
		/// </summary>
		TwoHanded	 = 0x02,
		/// <summary>
		/// Shoes.
		/// </summary>
		Shoes		 = 0x03,
		/// <summary>
		/// Pants.
		/// </summary>
		Pants		 = 0x04,
		/// <summary>
		/// Shirts.
		/// </summary>
		Shirt		 = 0x05,
		/// <summary>
		/// Helmets, hats, and masks.
		/// </summary>
		Helm		 = 0x06,
		/// <summary>
		/// Gloves.
		/// </summary>
		Gloves		 = 0x07,
		/// <summary>
		/// Rings.
		/// </summary>
		Ring		 = 0x08,
		/// <summary>
		/// Talismans.
		/// </summary>
		Talisman	 = 0x09,
		/// <summary>
		/// Gorgets and necklaces.
		/// </summary>
		Neck		 = 0x0A,
		/// <summary>
		/// Hair.
		/// </summary>
		Hair		 = 0x0B,
		/// <summary>
		/// Half aprons.
		/// </summary>
		Waist		 = 0x0C,
		/// <summary>
		/// Torso, inner layer.
		/// </summary>
		InnerTorso	 = 0x0D,
		/// <summary>
		/// Bracelets.
		/// </summary>
		Bracelet	 = 0x0E,
		/// <summary>
		/// Unused.
		/// </summary>
		Unused_xF	 = 0x0F,
		/// <summary>
		/// Beards and mustaches.
		/// </summary>
		FacialHair	 = 0x10,
		/// <summary>
		/// Torso, outer layer.
		/// </summary>
		MiddleTorso	 = 0x11,
		/// <summary>
		/// Earings.
		/// </summary>
		Earrings	 = 0x12,
		/// <summary>
		/// Arms and sleeves.
		/// </summary>
		Arms		 = 0x13,
		/// <summary>
		/// Cloaks.
		/// </summary>
		Cloak		 = 0x14,
		/// <summary>
		/// Backpacks.
		/// </summary>
		Backpack	 = 0x15,
		/// <summary>
		/// Torso, outer layer.
		/// </summary>
		OuterTorso	 = 0x16,
		/// <summary>
		/// Leggings, outer layer.
		/// </summary>
		OuterLegs	 = 0x17,
		/// <summary>
		/// Leggings, inner layer.
		/// </summary>
		InnerLegs	 = 0x18,
		/// <summary>
		/// Last valid non-internal layer. Equivalent to <c>Layer.InnerLegs</c>.
		/// </summary>
		LastUserValid= 0x18,
		/// <summary>
		/// Mount item layer.
		/// </summary>
		Mount		 = 0x19,
		/// <summary>
		/// Vendor 'buy pack' layer.
		/// </summary>
		ShopBuy		 = 0x1A,
		/// <summary>
		/// Vendor 'resale pack' layer.
		/// </summary>
		ShopResale	 = 0x1B,
		/// <summary>
		/// Vendor 'sell pack' layer.
		/// </summary>
		ShopSell	 = 0x1C,
		/// <summary>
		/// Bank box layer.
		/// </summary>
		Bank		 = 0x1D,
		/// <summary>
		/// Last valid layer. Equivalent to <c>Layer.Bank</c>.
		/// </summary>
		LastValid    = 0x1D
	}

	public enum ItemProperty
	{
		ElvesOnly,

		ColdResist,
		EnergyResist,
		FireResist,
		PhysicalResist,
		PoisonResist,

		StrRequirement,
		MinDurability,
		MaxDurability,
		Blessed,
		Cursed,
		UsesRemaining,
		MageArmor,

		LowerStatReq,
		SelfRepair,
		HitLeechHits,
		HitLeechStam,
		HitLeechMana,
		HitLowerAttack,
		HitLowerDefend,
		HitMagicArrow,
		HitHarm,
		HitFireball,
		HitLightning,
		HitDispel,
		HitColdArea,
		HitFireArea,
		HitPoisonArea,
		HitEnergyArea,
		HitPhysicalArea,
		ResistPhysicalBonus,
		ResistFireBonus,
		ResistColdBonus,
		ResistPoisonBonus,
		ResistEnergyBonus,
		UseBestSkill,
		MageWeapon,
		DurabilityBonus,

		RegenHits,
		RegenStam,
		RegenMana,
		DefendChance,
		AttackChance,
		BonusStr,
		BonusDex,
		BonusInt,
		BonusHits,
		BonusStam,
		BonusMana,
		WeaponDamageInc,
		WeaponSpeedInc,
		SpellDamage,
		CastRecovery,
		CastSpeed,
		LowerManaCost,
		LowerRegCost,
		ReflectPhysical,
		EnhancePotions,
		Luck,
		SpellChanneling,
		NightSight,
		SkillBonus1,
		SkillBonus2,
		SkillBonus3,
		SkillBonus4,
		SkillBonus5,
		SkillValue1,
		SkillValue2,
		SkillValue3,
		SkillValue4,
		SkillValue5,

		Slayer1,
		Slayer2,
		Poison,
		PhysicalDamage,
		FireDamage,
		ColdDamage,
		PoisonDamage,
		EnergyDamage,
		DirectDamage,
		ChaosDamage,
		MinWeaponDamage,
		MaxWeaponDamage,
		WeaponRange,
		WeaponSpeed,
		OneHanded,
		TwoHanded,
		SkillSwords,
		SkillMacing,
		SkillFencing,
		SkillArchery,
		Blanaced,
	}

	public class Item
	{
		#region Properties
		#region Serial
		private uint m_Serial;

		/// <summary>
		/// Item serial.
		/// </summary>
		public uint Serial
		{
			get { return m_Serial; }
		}
		#endregion

		#region NameCliloc
		private uint m_NameCliloc;

		/// <summary>
		/// Name cliloc.
		/// </summary>
		public uint NameCliloc
		{
			get { return m_NameCliloc; }
			set { m_NameCliloc = value; }
		}
		#endregion

		#region Name
		private string m_Name;

		/// <summary>
		/// Name.
		/// </summary>
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		#endregion

		#region ItemID
		private int m_ItemID;

		/// <summary>
		/// Item ID.
		/// </summary>
		public int ItemID
		{
			get { return m_ItemID; }
			set 
			{
				m_ItemData = TileData.ItemTable[ value ];
				m_ItemID = value; 
			}
		}

		/// <summary>
		/// Determines whether this item is gold.
		/// </summary>
		public bool IsGold
		{
			get { return m_ItemID == 0xEED; }
		}
		#endregion

		#region Hue
		private int m_Hue;

		/// <summary>
		/// Hue.
		/// </summary>
		public int Hue
		{
			get { return m_Hue; }
			set { m_Hue = value; }
		}
		#endregion

		#region Amount
		private int m_Amount;

		/// <summary>
		/// Amount.
		/// </summary>
		public int Amount
		{
			get { return m_Amount; }
			set { m_Amount = value; }
		}
		#endregion

		#region Weight
		private int m_Weight;

		/// <summary>
		/// Weight
		/// </summary>
		public int Weight
		{
			get { return m_Weight; }
			set { m_Weight = value; }
		}
		#endregion

		#region Layer
		private Layer m_Layer;

		/// <summary>
		/// Item layer.
		/// </summary>
		public Layer Layer
		{
			get { return m_Layer; }
			set { m_Layer = value; }
		}
		#endregion

		#region Layer
		private int m_Price;

		/// <summary>
		/// Item price.
		/// </summary>
		public int Price
		{
			get { return m_Price; }
			set { m_Price = value; }
		}
		#endregion

		#region ItemData
		private ItemData m_ItemData;

		/// <summary>
		/// ItemData
		/// </summary>
		public ItemData ItemData
		{
			get { return m_ItemData; }
		}

		/// <summary>
		/// Determines whether this item is wearable.
		/// </summary>
		public bool IsWearable
		{
			get
			{
				return ( m_ItemData.Flags & TileFlag.Wearable ) != 0;
			}
		}

		/// <summary>
		/// Determines whether this item is NOT wearable.
		/// </summary>
		public bool IsOther
		{
			get
			{
				return !IsWearable;
			}
		}
		#endregion

		#region AOS Properties
		private Dictionary<ItemProperty, object> m_Properties;

		/// <summary>
		/// Collections of item properties.
		/// </summary>
		public Dictionary<ItemProperty, object> Properties
		{
			get { return m_Properties; }
		}

		/// <summary>
		/// Number of properties in <see cref="ItemProperty"/>.
		/// </summary>
		public static int PropCount
		{
			get { return Enum.GetValues( typeof( ItemProperty ) ).Length; }
		}

		private static string[] m_DefaultNames = new string[]
		{
			"Serial",
			"Name Cliloc",
			"Name",
			"ItemID",
			"Hue",
			"Amount",
			"Weight"
		};

		/// <summary>
		/// Headers of item properties, not in <see cref="ItemProperty"/>.
		/// </summary>
		public static string[] DefaultNames
		{
			get { return m_DefaultNames; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the SpyUO.Item class with specific serial.
		/// </summary>
		/// <param name="serial">Item serial.</param>
		public Item( uint serial )
		{
			m_Serial = serial;

			m_Properties = new Dictionary<ItemProperty, object>();
		}
		#endregion

		#region Methods
		#region ParseProperty
		/// <summary>
		/// Parses and adds a property to <see cref="Properties"/> list.
		/// </summary>
		/// <param name="index">Number of the property.</param>
		/// <param name="number">Localized number of this property.</param>
		/// <param name="args">Arguments for the <paramref name="number"/>.</param>
		public void ParseProperty( int index, uint number, string args )
		{
			string prop = LocalizedList.Construct( number, args );

			if ( index == 0 )
			{
				m_Name = prop;

				if ( m_Amount > 1 )
					m_Name = m_Name.Replace( m_Amount.ToString() + " ", String.Empty );

				if ( args != null )
				{
					int s = args.IndexOf( '#' );

					if ( s >= 0 )
					{
						int e = args.IndexOf( '\t', s );

						if ( e < 0 )
							e = args.Length;

						UInt32.TryParse( args.Substring( s + 1, e - s - 1 ), out m_NameCliloc );
					}
					else
						m_NameCliloc = number;
				}
			}

			if ( prop.Contains( " slayer" ) )
			{
				if ( m_Properties.ContainsKey( ItemProperty.Slayer1 ) )
					m_Properties.Add( ItemProperty.Slayer2, prop );
				else
					m_Properties.Add( ItemProperty.Slayer1, prop );
			}

			switch ( number )
			{
				case 1072225:
				case 1072788:
				case 1072789: Int32.TryParse( args, out m_Weight ); break;
				case 1062412:
				case 1062413:
				case 1062414:
				case 1062415:
				case 1062416: AddProperty( ItemProperty.Poison, args ); break;
				case 1060445: AddProperty( ItemProperty.ColdResist, args ); break;
				case 1060446: AddProperty( ItemProperty.EnergyResist, args ); break;
				case 1060447: AddProperty( ItemProperty.FireResist, args ); break;
				case 1060448: AddProperty( ItemProperty.PhysicalResist, args ); break;
				case 1060449: AddProperty( ItemProperty.PoisonResist, args ); break;
				case 1060400: AddProperty( ItemProperty.UseBestSkill, args ); break;
				case 1060401: AddProperty( ItemProperty.WeaponDamageInc, args ); break;
				case 1060408: AddProperty( ItemProperty.DefendChance, args ); break;
				case 1060411: AddProperty( ItemProperty.EnhancePotions, args ); break;
				case 1060412: AddProperty( ItemProperty.CastRecovery, args ); break;
				case 1060413: AddProperty( ItemProperty.CastSpeed, args ); break;
				case 1060415: AddProperty( ItemProperty.AttackChance, args ); break;
				case 1060416: AddProperty( ItemProperty.HitColdArea, args ); break;
				case 1060417: AddProperty( ItemProperty.HitDispel, args ); break;
				case 1060418: AddProperty( ItemProperty.HitEnergyArea, args ); break;
				case 1060419: AddProperty( ItemProperty.HitFireArea, args ); break;
				case 1060420: AddProperty( ItemProperty.HitFireball, args ); break;
				case 1060421: AddProperty( ItemProperty.HitHarm, args ); break;
				case 1060422: AddProperty( ItemProperty.HitLeechHits, args ); break;
				case 1060423: AddProperty( ItemProperty.HitLightning, args ); break;
				case 1060424: AddProperty( ItemProperty.HitLowerAttack, args ); break;
				case 1060425: AddProperty( ItemProperty.HitLowerDefend, args ); break;
				case 1060426: AddProperty( ItemProperty.HitMagicArrow, args ); break;
				case 1060427: AddProperty( ItemProperty.HitLeechMana, args ); break;
				case 1060428: AddProperty( ItemProperty.HitPhysicalArea, args ); break;
				case 1060429: AddProperty( ItemProperty.HitPoisonArea, args ); break;
				case 1060430: AddProperty( ItemProperty.HitLeechStam, args ); break;

				case 1060409: AddProperty( ItemProperty.BonusDex, args ); break;
				case 1060431: AddProperty( ItemProperty.BonusHits, args ); break;
				case 1060432: AddProperty( ItemProperty.BonusInt, args ); break;
				case 1060433: AddProperty( ItemProperty.LowerManaCost, args ); break;
				case 1060434: AddProperty( ItemProperty.LowerRegCost, args ); break;
				case 1060435: AddProperty( ItemProperty.LowerStatReq, args ); break;
				case 1060436: AddProperty( ItemProperty.Luck, args ); break;
				case 1060438: AddProperty( ItemProperty.MageWeapon, args ); break;
				case 1060439: AddProperty( ItemProperty.BonusMana, args ); break;
				case 1060440: AddProperty( ItemProperty.RegenMana, args ); break;
				case 1060441: AddProperty( ItemProperty.NightSight ); break;
				case 1060442: AddProperty( ItemProperty.ReflectPhysical, args ); break;
				case 1060443: AddProperty( ItemProperty.RegenStam, args ); break;
				case 1060444: AddProperty( ItemProperty.RegenHits, args ); break;
				case 1060410: AddProperty( ItemProperty.DurabilityBonus, args ); break;
				case 1060450: AddProperty( ItemProperty.SelfRepair, args ); break;
				case 1060482: AddProperty( ItemProperty.SpellChanneling ); break;
				case 1060483: AddProperty( ItemProperty.SpellDamage, args ); break;
				case 1060484: AddProperty( ItemProperty.BonusStam, args ); break;
				case 1060485: AddProperty( ItemProperty.BonusStr, args ); break;
				case 1060486: AddProperty( ItemProperty.WeaponSpeedInc, args ); break;

				case 1060403: AddProperty( ItemProperty.PhysicalDamage, args ); break;
				case 1060405: AddProperty( ItemProperty.FireDamage, args ); break;
				case 1060404: AddProperty( ItemProperty.ColdDamage, args ); break;
				case 1060406: AddProperty( ItemProperty.PoisonDamage, args ); break;
				case 1060407: AddProperty( ItemProperty.EnergyDamage, args ); break;
				case 1079978: AddProperty( ItemProperty.DirectDamage, args ); break;
				case 1072846: AddProperty( ItemProperty.ChaosDamage, args ); break;
				case 1061168: AddTwoProperties( ItemProperty.MinWeaponDamage, ItemProperty.MaxWeaponDamage, args ); break;
				case 1061169: AddProperty( ItemProperty.WeaponRange, args ); break;
				case 1061167: AddProperty( ItemProperty.WeaponSpeed, args ); break;
				case 1061170: AddProperty( ItemProperty.StrRequirement, args ); break;
				case 1061171: AddProperty( ItemProperty.OneHanded ); break;
				case 1061824: AddProperty( ItemProperty.TwoHanded ); break;
				case 1061172: AddProperty( ItemProperty.SkillSwords, args ); break;
				case 1061173: AddProperty( ItemProperty.SkillMacing, args ); break;
				case 1061174: AddProperty( ItemProperty.SkillFencing, args ); break;
				case 1061175: AddProperty( ItemProperty.SkillArchery, args ); break;
				case 1060639: AddTwoProperties( ItemProperty.MinDurability, ItemProperty.MaxDurability, args ); break;
				case 1038021: AddProperty( ItemProperty.Blessed ); break;
				case 1049643: AddProperty( ItemProperty.Cursed ); break;
				case 1072792: AddProperty( ItemProperty.Blanaced ); break;
				case 1060584: AddProperty( ItemProperty.UsesRemaining, args ); break;
				case 1075086: AddProperty( ItemProperty.ElvesOnly ); break;
				case 1060451: AddTwoProperties( ItemProperty.SkillBonus1, ItemProperty.SkillValue1, args ); break;
				case 1060452: AddTwoProperties( ItemProperty.SkillBonus2, ItemProperty.SkillValue2, args ); break;
				case 1060453: AddTwoProperties( ItemProperty.SkillBonus3, ItemProperty.SkillValue3, args ); break;
				case 1060454: AddTwoProperties( ItemProperty.SkillBonus4, ItemProperty.SkillValue4, args ); break;
				case 1060455: AddTwoProperties( ItemProperty.SkillBonus5, ItemProperty.SkillValue5, args ); break;
				case 1060437: AddProperty( ItemProperty.MageArmor ); break;
			}
		}
		#endregion

		#region AddProperties
		/// <summary>
		/// Add an item property with no arguments.
		/// </summary>
		/// <param name="prop">Item property (<see cref="ItemProperty"/>)</param>
		public void AddProperty( ItemProperty prop )
		{
			if ( m_Properties.ContainsKey( prop ) )
				return;

			m_Properties.Add( prop, 1 );
		}

		/// <summary>
		/// Add an item property with arguments.
		/// </summary>
		/// <param name="prop">Item property (<see cref="ItemProperty"/>).</param>
		/// <param name="args">Arguments.</param>
		public void AddProperty( ItemProperty prop, string args )
		{
			if ( m_Properties.ContainsKey( prop ) )
				return;

			int intVal;

			if ( args.StartsWith( "#" ) && Int32.TryParse( args.Substring( 1, args.Length - 1 ), out intVal ) )
				args = LocalizedList.GetString( (uint) intVal );

			if ( Int32.TryParse( args, out intVal ) )
			{
				m_Properties.Add( prop, intVal );
			}
			else
			{
				double doubleVal;

				if ( Double.TryParse( args, out doubleVal ) )
					m_Properties.Add( prop, doubleVal );
				else
					m_Properties.Add( prop, args );
			}
		}

		/// <summary>
		/// Add two item properties with no arguments.
		/// </summary>
		/// <param name="prop1">First item property (<see cref="ItemProperty"/>).</param>
		/// <param name="prop2">Second item property (<see cref="ItemProperty"/>).</param>
		/// <param name="args">Arguments.</param>
		public void AddTwoProperties( ItemProperty prop1, ItemProperty prop2, string args )
		{
			string[] split = args.Split( '\t' );

			if ( split.Length > 0 )
				AddProperty( prop1, split[ 0 ] );
			if ( split.Length > 1 )
				AddProperty( prop2, split[ 1 ] );
		}
		#endregion

		#region CountSpecialProps
		/// <summary>
		/// Number of special properties (AOS + AOS weapon attributes).
		/// </summary>
		public int SpecialProps
		{
			get
			{
				Array list = Enum.GetValues( typeof( ItemProperty ) );
				int count = 0;

				for ( int i = 0; i < list.Length; i++ )
				{
					ItemProperty p = (ItemProperty) i;

					if ( m_Properties.ContainsKey( p ) && IsSpecialProperty( p ) )
						count++;
				}

				return count;
			}
		}
		#endregion

		#region IsSpecial
		private static bool[] m_Specials = new bool[]
		{
			false, false, false, false, false, false,
			false, false, false, false, false, false,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			true, true, true, true, true, true,
			false, false, false, false, false, true,
			true, false, false, false, false, false,
			false, false, false, false, false, false,
			false, false, false, false, false, false,
			false, false
		};

		/// <summary>
		/// Determines whether <paramref name="prop"/>.
		/// </summary>
		/// <param name="prop">Item property.</param>
		/// <param name="value">Value of <paramref name="prop"/></param>
		/// <returns>true if property is special and contains a value, false otherwise.</returns>
		public static bool IsSpecialProperty( ItemProperty prop )
		{
			int i = (int) prop;

			return m_Specials[ i ];
		}
		#endregion

		#region ToString
		/// <summary>
		/// Converts this item to one line string 
		/// </summary>
		/// <param name="filter">Loot filter.</param>
		/// <returns>Item converted into string.</returns>
		public string ToString( LootFilter filter )
		{
			StringBuilder builder = new StringBuilder();
			int l = PropCount;

			builder.Append( Filter( filter, l, "0x{0}\t", m_Serial.ToString( "X8" ) ) );
			builder.Append( Filter( filter, l + 1, "{0}\t", m_NameCliloc ) );
			builder.Append( Filter( filter, l + 2, "{0}\t", m_Name ) );
			builder.Append( Filter( filter, l + 3, "0x{0}\t", m_ItemID.ToString( "X4" ) ) );
			builder.Append( Filter( filter, l + 4, "0x{0}\t", m_Hue.ToString( "X" ) ) );
			builder.Append( Filter( filter, l + 5, "{0}\t", m_Amount ) );
			builder.Append( Filter( filter, l + 6, "{0}\t", m_Weight ) );

			Array list = Enum.GetValues( typeof( ItemProperty ) );

			for ( int i = 0; i < list.Length; i++ )
			{
				ItemProperty p = (ItemProperty) i;

				if ( filter == null || filter.Filters( (int) p ) )
				{
					if ( m_Properties.ContainsKey( p ) )
						builder.AppendFormat( "{0}\t", m_Properties[ p ] );
					else
						builder.Append( '\t' );
				}
			}

			return builder.ToString();
		}

		public override string ToString()
		{
			return ToString( null );
		}
		#endregion

		#region GetHeader
		/// <summary>
		/// Constructs header for all items.
		/// </summary>
		/// <param name="filter">Loot filter.</param>
		/// <returns>Item header.</returns>
		public static string GetHeader( LootFilter filter )
		{
			StringBuilder builder = new StringBuilder();
			int l = PropCount;

			builder.Append( Filter( filter, l, "Serial\t", null ) );
			builder.Append( Filter( filter, l + 1, "Localized Number\t", null ) );
			builder.Append( Filter( filter, l + 2, "Name\t", null ) );
			builder.Append( Filter( filter, l + 3, "ItemID\t", null ) );
			builder.Append( Filter( filter, l + 4, "Hue\t", null ) );
			builder.Append( Filter( filter, l + 5, "Amount\t", null ) );
			builder.Append( Filter( filter, l + 6, "Weight\t", null ) );

			string[] list = Enum.GetNames( typeof( ItemProperty ) );

			for ( int i = 0; i < list.Length; i++ )
			{
				if ( filter == null || filter.Filters( i ) )
					builder.AppendFormat( "{0}\t", list[ i ] );
			}

			return builder.ToString();
		}
		#endregion

		#region Filter
		/// <summary>
		/// Filters item property
		/// </summary>
		/// <param name="filter">Loot filter.</param>
		/// <param name="index">Item property (<see cref="ItemProperty"/>).</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An object to format.</param>
		/// <returns>Empty string when filtered and constructed item property otherwise.</returns>
		public static string Filter( LootFilter filter, int index, string format, object args )
		{
			if ( filter == null || filter.Filters( index ) )
				return String.Format( format, args );

			return String.Empty;
		}
		#endregion
		#endregion
	}
}
