using System;
using System.IO;
using System.Collections.Generic;

namespace SpyUO
{
	public class LootFilter
	{
		#region Properties
		#region Item Filter
		private bool[] m_ItemFilter;

		/// <summary>
		/// Item filter.
		/// </summary>
		public bool[] ItemFilter
		{
			get { return m_ItemFilter; }
		}
		#endregion

		#region ItemPropertyFilter
		private PropertyFilter[] m_PropertyFilter;

		public PropertyFilter[] PropertyFilter
		{
			get { return m_PropertyFilter; }
		}
		#endregion

		#region MobileFilter
		private Dictionary<string, bool> m_MobileFilter;

		/// <summary>
		/// Mobile filter.
		/// </summary>
		public Dictionary<string, bool> MobileFilter
		{
			get { return m_MobileFilter; }
		}
		#endregion

		#region Wearable
		private bool m_Wearable;
		
		/// <summary>
		/// Determines whether this filters filters wearables.
		/// </summary>
		public bool Wearable
		{
			get { return m_Wearable; }
			set { m_Wearable = value; }
		}
		#endregion

		#region Other
		private bool m_Other;

		/// <summary>
		/// Determines whether this filters filters non wearables.
		/// </summary>
		public bool Other
		{
			get { return m_Other; }
			set { m_Other = value; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the SpyUO.LootFilter class.
		/// </summary>
		public LootFilter()
		{
			Array array = Enum.GetValues( typeof( ItemProperty ) );
			m_ItemFilter = new bool[ 7 + array.Length ];
			m_PropertyFilter = new PropertyFilter[ 7 + array.Length ];

			for ( int i = 0; i < m_ItemFilter.Length; i++ )
			{
				m_ItemFilter[ i ] = true;
				m_PropertyFilter[ i ] = new PropertyFilter();
			}

			m_MobileFilter = new Dictionary<string, bool>();
			m_Wearable = true;
			m_Other = true;
		}
		#endregion

		#region Methods
		#region Filters
		/// <summary>
		/// Determines whether this filter filters property <paramref name="prop"/>.
		/// </summary>
		/// <param name="prop">Item property.</param>
		/// <returns>true if filters, false otherwise.</returns>
		public bool Filters( int prop )
		{
			if ( prop >= 0 && prop < m_ItemFilter.Length )
				return m_ItemFilter[ prop ];

			return false;
		}

		public bool Filters( Item item )
		{
			if ( item != null )
			{
				Array array = Enum.GetValues( typeof( ItemProperty ) );

				for ( int i = 0; i < m_ItemFilter.Length; i++ )
				{
					bool on = m_ItemFilter[ i ];
					PropertyFilter filter = m_PropertyFilter[ i ];

					if ( on )
					{
						if ( i >= array.Length )
						{
							switch ( i - array.Length )
							{
								case 0: if ( filter.Filters( item.Serial ) ) return true; else break;
								case 1: if ( filter.Filters( item.NameCliloc ) ) return true; else break;
								case 2: if ( filter.Filters( item.Name ) ) return true; else break;
								case 3: if ( filter.Filters( item.ItemID ) ) return true; else break;
								case 4: if ( filter.Filters( item.Hue ) ) return true; else break;
								case 5: if ( filter.Filters( item.Amount ) ) return true; else break;
								case 6: if ( filter.Filters( item.Weight ) ) return true; else break;
							}
						}
						else
						{
							ItemProperty prop = (ItemProperty) i;

							if ( item.Properties.ContainsKey( prop ) && filter.Filters( item.Properties[ prop ] ) )
								return true;
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Determines whether this filter filters mobile named <paramref name="mobile"/>.
		/// </summary>
		/// <param name="prop">Mobile name.</param>
		/// <returns>true if filters, false otherwise.</returns>
		public bool Filters( string mobile )
		{
			if ( mobile != null && m_MobileFilter.ContainsKey( mobile ) )
				return m_MobileFilter[ mobile ];

			return false;
		}
		#endregion

		#region AddMobile
		/// <summary>
		/// Adds a mobile to this filter 
		/// </summary>
		/// <param name="name">Mobile name.</param>
		/// <returns>false whether mobile name is already in this filter, true otherwise.</returns>
		public bool AddMobile( string name )
		{
			if ( name == null || m_MobileFilter.ContainsKey( name ) )
				return false;

			m_MobileFilter.Add( name, true );
			return true;
		}
		#endregion

		#region Toggle
		/// <summary>
		/// Toggles item property filtering.
		/// </summary>
		/// <param name="prop">Item property to toggle.</param>
		public void Toggle( int prop )
		{
			if ( prop >= 0 && prop < m_ItemFilter.Length )
				m_ItemFilter[ prop ] = !m_ItemFilter[ prop ];
		}

		/// <summary>
		/// Toggles mobile filtering.
		/// </summary>
		/// <param name="prop">Mobile name to toggle.</param>
		public void Toggle( string mobile )
		{
			if ( mobile != null && m_MobileFilter.ContainsKey( mobile ) )
				m_MobileFilter[ mobile ] = !m_MobileFilter[ mobile ];
		}

		private static int[] m_ArmorMask = new int[]
		{
			(int) ItemProperty.ColdResist,
			(int) ItemProperty.EnergyResist,
			(int) ItemProperty.FireResist,
			(int) ItemProperty.PhysicalResist,
			(int) ItemProperty.PoisonResist,
			(int) ItemProperty.MageArmor,
		};

		/// <summary>
		/// Toggles all armor only properties.
		/// </summary>
		/// <param name="on">On/Off.</param>
		public void ToggleArmor( bool on )
		{
			for ( int i = 0; i < m_ArmorMask.Length; i++ )
				m_ItemFilter[ m_ArmorMask[ i ] ] = on;
		}

		private static int[] m_WeaponMask = new int[]
		{
			(int) ItemProperty.Slayer1,
			(int) ItemProperty.Slayer2,
			(int) ItemProperty.Poison,
			(int) ItemProperty.PhysicalDamage,
			(int) ItemProperty.FireDamage,
			(int) ItemProperty.ColdDamage,
			(int) ItemProperty.PoisonDamage,
			(int) ItemProperty.EnergyDamage,
			(int) ItemProperty.DirectDamage,
			(int) ItemProperty.ChaosDamage,
			(int) ItemProperty.MinWeaponDamage,
			(int) ItemProperty.MaxWeaponDamage,
			(int) ItemProperty.WeaponRange,
			(int) ItemProperty.WeaponSpeed,
			(int) ItemProperty.OneHanded,
			(int) ItemProperty.TwoHanded,
			(int) ItemProperty.SkillSwords,
			(int) ItemProperty.SkillMacing,
			(int) ItemProperty.SkillFencing,
			(int) ItemProperty.SkillArchery,
			(int) ItemProperty.Blanaced,
			(int) ItemProperty.LowerStatReq,
			(int) ItemProperty.SelfRepair,
			(int) ItemProperty.HitLeechHits,
			(int) ItemProperty.HitLeechStam,
			(int) ItemProperty.HitLeechMana,
			(int) ItemProperty.HitLowerAttack,
			(int) ItemProperty.HitLowerDefend,
			(int) ItemProperty.HitMagicArrow,
			(int) ItemProperty.HitHarm,
			(int) ItemProperty.HitFireball,
			(int) ItemProperty.HitLightning,
			(int) ItemProperty.HitDispel,
			(int) ItemProperty.HitColdArea,
			(int) ItemProperty.HitFireArea,
			(int) ItemProperty.HitPoisonArea,
			(int) ItemProperty.HitEnergyArea,
			(int) ItemProperty.HitPhysicalArea,
			(int) ItemProperty.ResistPhysicalBonus,
			(int) ItemProperty.ResistFireBonus,
			(int) ItemProperty.ResistColdBonus,
			(int) ItemProperty.ResistPoisonBonus,
			(int) ItemProperty.ResistEnergyBonus,
			(int) ItemProperty.UseBestSkill,
			(int) ItemProperty.MageWeapon,
			(int) ItemProperty.DurabilityBonus,
		};

		/// <summary>
		/// Toggles all weapon only properties.
		/// </summary>
		/// <param name="on">On/Off.</param>
		public void ToggleWeapon( bool on )
		{
			for ( int i = 0; i < m_WeaponMask.Length; i++ )
				m_ItemFilter[ m_WeaponMask[ i ] ] = on;
		}

		private static int[] m_AosMask = new int[]
		{
			(int) ItemProperty.RegenHits,
			(int) ItemProperty.RegenStam,
			(int) ItemProperty.RegenMana,
			(int) ItemProperty.DefendChance,
			(int) ItemProperty.AttackChance,
			(int) ItemProperty.BonusStr,
			(int) ItemProperty.BonusDex,
			(int) ItemProperty.BonusInt,
			(int) ItemProperty.BonusHits,
			(int) ItemProperty.BonusStam,
			(int) ItemProperty.BonusMana,
			(int) ItemProperty.WeaponDamageInc,
			(int) ItemProperty.WeaponSpeedInc,
			(int) ItemProperty.SpellDamage,
			(int) ItemProperty.CastRecovery,
			(int) ItemProperty.CastSpeed,
			(int) ItemProperty.LowerManaCost,
			(int) ItemProperty.LowerRegCost,
			(int) ItemProperty.ReflectPhysical,
			(int) ItemProperty.EnhancePotions,
			(int) ItemProperty.Luck,
			(int) ItemProperty.SpellChanneling,
			(int) ItemProperty.NightSight,
			(int) ItemProperty.SkillBonus1,
			(int) ItemProperty.SkillBonus2,
			(int) ItemProperty.SkillBonus3,
			(int) ItemProperty.SkillBonus4,
			(int) ItemProperty.SkillBonus5,
			(int) ItemProperty.SkillValue1,
			(int) ItemProperty.SkillValue2,
			(int) ItemProperty.SkillValue3,
			(int) ItemProperty.SkillValue4,
			(int) ItemProperty.SkillValue5,
		};

		/// <summary>
		/// Toggles all AOS only properties.
		/// </summary>
		/// <param name="on">On/Off.</param>
		public void ToggleAos( bool on )
		{
			for ( int i = 0; i < m_AosMask.Length; i++ )
				m_ItemFilter[ m_AosMask[ i ] ] = on;
		}

		/// <summary>
		/// Toggles all item properties.
		/// </summary>
		/// <param name="on">On/Off.</param>
		public void ToggleItems( bool on )
		{
			for ( int i = 0; i < m_ItemFilter.Length; i++ )
				m_ItemFilter[ i ] = on;
		}

		/// <summary>
		/// Toggles all mobiles.
		/// </summary>
		/// <param name="on">On/Off.</param>
		public void ToggleMobiles( bool on )
		{
			string[] keys = new string[ m_MobileFilter.Count ];
			m_MobileFilter.Keys.CopyTo( keys, 0 );
			
			for ( int i = keys.Length - 1; i >= 0; i-- )
				m_MobileFilter[ keys[ i ] ] = on;
		}
		#endregion

		#region Serialization
		/// <summary>
		/// Saves this filter to the file name.
		/// </summary>
		/// <param name="file">File name.</param>
		public void Save( string file )
		{
			using ( FileStream stream = File.Create( file ) )
			using ( BinaryWriter writer = new BinaryWriter( stream ) )
			{
				for ( int i = 0; i < m_ItemFilter.Length; i++ )
				{
					writer.Write( m_ItemFilter[ i ] );
					m_PropertyFilter[ i ].Save( writer );
				}

				writer.Write( m_MobileFilter.Count );

				foreach ( KeyValuePair<string, bool> kvp in m_MobileFilter )
				{
					writer.Write( kvp.Key );
					writer.Write( kvp.Value );
					
				}

				writer.Write( m_Wearable );
				writer.Write( m_Other );
			}
		}

		/// <summary>
		/// Loads filter from a file.
		/// </summary>
		/// <param name="file">File name.</param>
		public void Load( string file )
		{
			using ( FileStream stream = File.Open( file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
			using ( BinaryReader reader = new BinaryReader( stream ) )
			{
				if ( stream.Length < m_ItemFilter.Length )
					throw new FileLoadException( "Filter too short!" );

				for ( int i = 0; i < m_ItemFilter.Length; i++ )
				{
					m_ItemFilter[ i ] = reader.ReadBoolean();
					m_PropertyFilter[ i ].Load( reader );
				}

				int length = reader.ReadInt32();

				for ( int i = 0; i < length; i++ )
				{
					string mob = reader.ReadString();
					bool on = reader.ReadBoolean();

					if ( !m_MobileFilter.ContainsKey( mob ) )
						m_MobileFilter.Add( mob, on );

					m_Wearable = reader.ReadBoolean();
					m_Other = reader.ReadBoolean();
				}
			}
		}
		#endregion
		#endregion
	}
}
