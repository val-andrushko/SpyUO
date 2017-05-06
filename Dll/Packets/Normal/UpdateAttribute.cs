using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0xDF )]
	public class UpdateAttribute : Packet
	{
		#region Enum
		public enum Attributes
		{
			BonusStr = 0x01,
			BonusDex = 0x02,
			BonusInt = 0x03,
			BonusHits = 0x07,
			BonusStamina = 0x08,
			BonusMana = 0x09,
			RegenHits = 0x0A,
			RegenStam = 0x0B,
			RegenMana = 0x0C,
			NightSight = 0x0D,
			Luck = 0x0E,
			ReflectPhysical = 0x10,
			EnhancePotions = 0x11,
			AttackChance = 0x12,
			DefendChance = 0x13,
			SpellDamage = 0x14,
			CastRecovery = 0x15,
			CastSpeed = 0x16,
			ManaCost = 0x17,
			ReagentCost = 0x18,
			WeaponSpeed = 0x19,
			WeaponDamage = 0x1A,
			PhysicalResistance = 0x1B,
			FireResistance = 0x1C,
			ColdResistance = 0x1D,
			PoisonResistance = 0x1E,
			EnergyResistance = 0x1F,
			MaxPhysicalResistance = 0x20,
			MaxFireResistance = 0x21,
			MaxColdResistance = 0x22,
			MaxPoisonResistance = 0x23,
			MaxEnergyResistance = 0x24,
			AmmoCost = 0x26,
			KarmaLoss = 0x28,
		}
		#endregion

		private uint m_PlayerSerial;
		private ushort m_AttributeID;
		private ushort m_ItemsCount;
		private string m_Items;

		[PacketProp( 1, "0x{0:X}" )]
		public uint PlayerSerial { get { return m_PlayerSerial; } }

		[PacketProp( 2 )]
		public ushort AttributeID { get { return m_AttributeID; } }

		[PacketProp( 3 )]
		public ushort ItemsCount { get { return m_ItemsCount; } }

		[PacketProp( 4 )]
		public string Items { get { return m_Items; } }

		public UpdateAttribute( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadUInt16();

			m_PlayerSerial = reader.ReadUInt32();
			m_AttributeID = reader.ReadUInt16();
			m_ItemsCount = reader.ReadUInt16();

			StringBuilder sb = new StringBuilder();

			for ( int i = 0; i < m_ItemsCount; i++ )
			{
				ItemsAttr tostring = new ItemsAttr();

				tostring.BaseValue = reader.ReadUInt16();
				tostring.Zero1 = reader.ReadUInt32();
				tostring.DeltaValue = reader.ReadUInt16();
				tostring.Zero2 = reader.ReadBytes( 9 );
				tostring.ItemLabelNumber = reader.ReadUInt32();
				tostring.Zero3 = reader.ReadBytes( 14 );

				sb.Append( "Item: " + i.ToString() );

				sb.Append( " BaseValue: " + tostring.BaseValue.ToString( "X" ) );
				sb.Append( " Zero1: " + tostring.Zero1.ToString() );
				sb.Append( " DeltaValue: " + tostring.DeltaValue.ToString( "X" ) );
				sb.Append( " ItemLabelNumber: " + tostring.ItemLabelNumber.ToString( "X" ) );
			}

			m_Items = sb.ToString();
		}
	}

	public class ItemsAttr
	{
		private ushort m_BaseValue;
		private uint m_Zero1;
		private ushort m_DeltaValue;
		private byte[] m_Zero2;
		private uint m_ItemLabelNumer;
		private byte[] m_Zero3;

		public ushort BaseValue
		{
			get { return m_BaseValue; }
			set { m_BaseValue = value; }
		}

		public uint Zero1
		{
			get { return m_Zero1; }
			set { m_Zero1 = value; }
		}

		public ushort DeltaValue
		{
			get { return m_DeltaValue; }
			set { m_DeltaValue = value; }
		}

		public byte[] Zero2
		{
			get { return m_Zero2; }
			set { m_Zero2 = value; }
		}

		public uint ItemLabelNumber
		{
			get { return m_ItemLabelNumer; }
			set { m_ItemLabelNumer = value; }
		}

		public byte[] Zero3
		{
			get { return m_Zero3; }
			set { m_Zero3 = value; }
		}
	}
}