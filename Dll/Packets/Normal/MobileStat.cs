using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0x11 )]
	public class MobileStat : Packet
	{
		private uint m_Serial;
		private string m_Name;
		private ushort m_HitPoints;
		private ushort m_MaxHitPoints;
		private byte m_AllowNameChange;
		private byte m_Features;
		private byte m_Gender;
		private ushort m_Strength;
		private ushort m_Dexterity;
		private ushort m_Intelligence;
		private ushort m_Stamina;
		private ushort m_MaxStamina;
		private ushort m_Mana;
		private ushort m_MaxMana;
		private uint m_Gold;
		private ushort m_ArmorRating;
		private ushort m_Weight;

		// Type = 5
		private ushort m_MaxWeight;
		private byte m_Race;

		// Type = 2
		private ushort m_StatCap;

		// Type = 3
		private byte m_Followers;
		private byte m_MaxFollowers;
		
		// Type = 4
		private ushort m_FireRes;
		private ushort m_ColdRes;
		private ushort m_PoisonRes;
		private ushort m_EnergyRes;
		private ushort m_Luck;
		private ushort m_MinWeapDamage;
		private ushort m_MaxWeapDamage;
		private uint m_TithingPoints;

		// Type = 6
		private ushort m_HitChanceIncr;
		private ushort m_SwingSpeedIncr;
		private ushort m_DamageChanceIncr;
		private ushort m_LowerReagCost;
		private ushort m_HitPointsReg;

		private ushort m_StaminaPointsReg;
		private ushort m_ManaReag;
		private ushort m_ReflectPhysDamage;
		private ushort m_EnhancePotions;
		private ushort m_DefenceChanceIncr;

		private ushort m_SpellDamageIncr;
		private ushort m_FasterCastRecovery;
		private ushort m_FasterCasting;
		private ushort m_LowerManaCost;
		private ushort m_StrengthIncr;

		private ushort m_DexterityIncr;
		private ushort m_IntelligenceIncr;
		private ushort m_HitPointsIncr;
		private ushort m_StaminaIncr;
		private ushort m_ManaIncr;

		private ushort m_MaxHitPointsIncr;
		private ushort m_MaxStaminaIncr;
		private ushort m_MaxManaIncr;

		[PacketProp( 1, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 2 )]
		public string Name { get { return m_Name; } }

		[PacketProp( 3 )]
		public ushort HitPoints { get { return m_HitPoints; } }

		[PacketProp( 4 )]
		public ushort MaxHits { get { return m_MaxHitPoints; } }

		[PacketProp( 5 )]
		public byte AllowNameChange { get { return m_AllowNameChange; } }

		[PacketProp( 6 )]
		public byte Features { get { return m_Features; } }

		[PacketProp( 7 )]
		public byte Gender { get { return m_Gender; } }

		[PacketProp( 8 )]
		public ushort Strength { get { return m_Strength; } }

		[PacketProp( 9 )]
		public ushort Dexterity { get { return m_Dexterity; } }

		[PacketProp( 10 )]
		public ushort Intelligence { get { return m_Intelligence; } }

		[PacketProp( 11 )]
		public ushort Stamina { get { return m_Stamina; } }

		[PacketProp( 12 )]
		public ushort MaxStamina { get { return m_MaxStamina; } }

		[PacketProp( 13 )]
		public ushort Mana { get { return m_Mana; } }

		[PacketProp( 14 )]
		public ushort MaxMana { get { return m_MaxMana; } }

		[PacketProp( 15 )]
		public uint Gold { get { return m_Gold; } }

		[PacketProp( 16 )]
		public ushort Armor { get { return m_ArmorRating; } }

		[PacketProp( 17 )]
		public ushort Weight { get { return m_Weight; } }

		[PacketProp( 18 )]
		public ushort MaxWeight { get { return m_MaxWeight; } }

		[PacketProp( 19 )]
		public byte Race { get { return m_Race; } }

		[PacketProp( 20 )]
		public ushort StatCap { get { return m_StatCap; } }

		[PacketProp( 21 )]
		public byte Followers { get { return m_Followers; } }

		[PacketProp( 22 )]
		public byte MaxFollowers { get { return m_MaxFollowers; } }

		[PacketProp( 23 )]
		public ushort FireRes { get { return m_FireRes; } }

		[PacketProp( 24 )]
		public ushort ColdRes { get { return m_ColdRes; } }

		[PacketProp( 25 )]
		public ushort PoisonRes { get { return m_PoisonRes; } }

		[PacketProp( 26 )]
		public ushort EnergyRes { get { return m_EnergyRes; } }

		[PacketProp( 27 )]
		public ushort Luck { get { return m_Luck; } }

		[PacketProp( 28 )]
		public ushort MinWeapDamage { get { return m_MinWeapDamage; } }

		[PacketProp( 29 )]
		public ushort MaxWeapDamage { get { return m_MaxWeapDamage; } }

		[PacketProp( 30 )]
		public uint TithingPoints { get { return m_TithingPoints; } }

		[PacketProp( 31 )]
		public ushort HitChanceIncr { get { return m_HitChanceIncr; } }

		[PacketProp( 32 )]
		public ushort SwingSpeedIncr { get { return m_SwingSpeedIncr; } }

		[PacketProp( 33 )]
		public ushort DamageChanceIncr { get { return m_DamageChanceIncr; } }

		[PacketProp( 34 )]
		public ushort LowerReagCost { get { return m_LowerReagCost; } }

		[PacketProp( 35 )]
		public ushort HitPointsReg { get { return m_HitPointsReg; } }

		[PacketProp( 36 )]
		public ushort StaminaReg { get { return m_StaminaPointsReg; } }

		[PacketProp( 37 )]
		public ushort ManaReg { get { return m_ManaReag; } }

		[PacketProp( 38 )]
		public ushort ReflectPhysDamage { get { return m_ReflectPhysDamage; } }

		[PacketProp( 39 )]
		public ushort EnhancePotions { get { return m_EnhancePotions; } }

		[PacketProp( 40 )]
		public ushort DefenceChanceIncr { get { return m_DefenceChanceIncr; } }

		[PacketProp( 41 )]
		public ushort SpellDamageIncr { get { return m_SpellDamageIncr; } }

		[PacketProp( 42 )]
		public ushort FasterCastRec { get { return m_FasterCastRecovery; } }

		[PacketProp( 43 )]
		public ushort FasterCasting { get { return m_FasterCasting; } }

		[PacketProp( 44 )]
		public ushort LowerManaCost { get { return m_LowerManaCost; } }

		[PacketProp( 45 )]
		public ushort StrengthIncr { get { return m_StrengthIncr; } }

		[PacketProp( 46 )]
		public ushort DexterityIncr { get { return m_DexterityIncr; } }

		[PacketProp( 47 )]
		public ushort IntelligenceIncr { get { return m_IntelligenceIncr; } }

		[PacketProp( 48 )]
		public ushort HitPointsIncr { get { return m_HitPointsIncr; } }

		[PacketProp( 49 )]
		public ushort StaminaIncr { get { return m_StaminaIncr; } }

		[PacketProp( 50 )]
		public ushort ManaIncr { get { return m_ManaIncr; } }

		[PacketProp( 51 )]
		public ushort MaxHitPointsIncr { get { return m_MaxHitPointsIncr; } }

		[PacketProp( 52 )]
		public ushort MaxStaminaIncr { get { return m_MaxStaminaIncr; } }

		[PacketProp( 53 )]
		public ushort MaxManaIncr { get { return m_MaxManaIncr; } }

		public MobileStat( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadUInt16();

			m_Serial = reader.ReadUInt32();

			m_Name = reader.ReadASCIIString( 30 );
			m_HitPoints = reader.ReadUInt16();
			m_MaxHitPoints = reader.ReadUInt16();
			m_AllowNameChange = reader.ReadByte();
			m_Features = reader.ReadByte();
			m_Gender = reader.ReadByte();

			m_Strength = reader.ReadUInt16();
			m_Dexterity = reader.ReadUInt16();
			m_Intelligence = reader.ReadUInt16();
			m_Stamina = reader.ReadUInt16();
			m_MaxStamina = reader.ReadUInt16();
			m_Mana = reader.ReadUInt16();
			m_MaxMana = reader.ReadUInt16();

			m_Gold = reader.ReadUInt32();
			m_ArmorRating = reader.ReadUInt16();
			m_Weight = reader.ReadUInt16();

			if ( m_Features <= 5 )
			{
				m_MaxWeight = reader.ReadUInt16();
				m_Race = reader.ReadByte();
			}

			if ( m_Features <= 3 )
			{
				m_Followers = reader.ReadByte();
				m_StatCap = reader.ReadUInt16();
				m_Followers = reader.ReadByte();
				m_MaxFollowers = reader.ReadByte();
			}

			if ( m_Features <= 4 )
			{
				m_FireRes = reader.ReadUInt16();
				m_ColdRes = reader.ReadUInt16();
				m_PoisonRes = reader.ReadUInt16();
				m_EnergyRes = reader.ReadUInt16();
				m_Luck = reader.ReadUInt16();
				m_MinWeapDamage = reader.ReadUInt16();
				m_MaxWeapDamage = reader.ReadUInt16();
				m_TithingPoints = reader.ReadUInt32();
			}

			if ( m_Features <= 6 )
			{
				m_HitChanceIncr = reader.ReadUInt16();
				m_SwingSpeedIncr = reader.ReadUInt16();
				m_DamageChanceIncr = reader.ReadUInt16();
				m_LowerReagCost = reader.ReadUInt16();
				m_HitPointsReg = reader.ReadUInt16();

				m_StaminaPointsReg = reader.ReadUInt16();
				m_ManaReag = reader.ReadUInt16();
				m_ReflectPhysDamage = reader.ReadUInt16();
				m_EnhancePotions = reader.ReadUInt16();
				m_DefenceChanceIncr = reader.ReadUInt16();

				m_SpellDamageIncr = reader.ReadUInt16();
				m_FasterCastRecovery = reader.ReadUInt16();
				m_FasterCasting = reader.ReadUInt16();
				m_LowerManaCost = reader.ReadUInt16();
				m_StrengthIncr = reader.ReadUInt16();

				m_DexterityIncr = reader.ReadUInt16();
				m_IntelligenceIncr = reader.ReadUInt16();
				m_HitPointsIncr = reader.ReadUInt16();
				m_StaminaIncr = reader.ReadUInt16();
				m_ManaIncr = reader.ReadUInt16();

				m_MaxHitPointsIncr = reader.ReadUInt16();
				m_MaxStaminaIncr = reader.ReadUInt16();
				m_MaxManaIncr = reader.ReadUInt16();
			}
		}
	}
}