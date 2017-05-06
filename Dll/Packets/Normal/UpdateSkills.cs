using System;
using System.Text;
using System.Collections.Generic;

namespace SpyUO.Packets
{
	public enum SkillLock : byte
	{
		Up = 0,
		Down = 1,
		Locked = 2
	}

	public enum SkillName
	{
		Alchemy = 0,
		Anatomy = 1,
		AnimalLore = 2,
		ItemID = 3,
		ArmsLore = 4,
		Parry = 5,
		Begging = 6,
		Blacksmith = 7,
		Fletching = 8,
		Peacemaking = 9,
		Camping = 10,
		Carpentry = 11,
		Cartography = 12,
		Cooking = 13,
		DetectHidden = 14,
		Discordance = 15,
		EvalInt = 16,
		Healing = 17,
		Fishing = 18,
		Forensics = 19,
		Herding = 20,
		Hiding = 21,
		Provocation = 22,
		Inscribe = 23,
		Lockpicking = 24,
		Magery = 25,
		MagicResist = 26,
		Tactics = 27,
		Snooping = 28,
		Musicianship = 29,
		Poisoning = 30,
		Archery = 31,
		SpiritSpeak = 32,
		Stealing = 33,
		Tailoring = 34,
		AnimalTaming = 35,
		TasteID = 36,
		Tinkering = 37,
		Tracking = 38,
		Veterinary = 39,
		Swords = 40,
		Macing = 41,
		Fencing = 42,
		Wrestling = 43,
		Lumberjacking = 44,
		Mining = 45,
		Meditation = 46,
		Stealth = 47,
		RemoveTrap = 48,
		Necromancy = 49,
		Focus = 50,
		Chivalry = 51,
		Bushido = 52,
		Ninjitsu = 53,
		Spellweaving = 54
	}

	[PacketInfo( 0x3A )]
	public class UpdateSkills : Packet
	{
		private List<SkillEntry> m_Entries;
		private string m_EntriesString;

		[PacketProp( 0 )]
		public string Entries
		{
			get { return m_EntriesString; }
		}

		public UpdateSkills( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadUInt16();
			int count = reader.ReadByte();

			if ( count > 54 )
				count = 1;

			m_Entries = new List<SkillEntry>();
			StringBuilder b = new StringBuilder();
			SkillEntry entry;

			for ( int i = count - 1; i >= 0; i-- )
			{
				entry = new SkillEntry( reader );
				m_Entries.Add( entry );
				b.Append( entry.ToString() );

				if ( i > 0 )
					b.Append( " - " );
			}

			m_EntriesString = b.ToString();
		}
	}

	public class SkillEntry
	{
		private SkillName m_Name;
		private int m_Value;
		private int m_BaseValue;
		private SkillLock m_Lock;
		private int m_Cap;

		public SkillName Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		public int Value
		{
			get { return m_Value; }
			set { m_Value = value; }
		}

		public int BaseValue
		{
			get { return m_BaseValue; }
			set { m_BaseValue = value; }
		}

		public SkillLock Lock
		{
			get { return m_Lock; }
			set { m_Lock = value; }
		}

		public int Cap
		{
			get { return m_Cap; }
			set { m_Cap = value; }
		}

		public SkillEntry( PacketReader reader )
		{
			m_Name = (SkillName) reader.ReadUInt16();
			m_Value = reader.ReadUInt16();
			m_BaseValue = reader.ReadUInt16();
			m_Lock = (SkillLock) reader.ReadByte();
			m_Cap = reader.ReadUInt16();
		}

		public SkillEntry( SkillName name, int value, int baseValue, SkillLock lockType, int cap )
		{
			m_Name = name;
			m_Value = value;
			m_BaseValue = baseValue;
			m_Lock = lockType;
			m_Cap = cap;
		}

		public override string ToString()
		{
			return String.Format( "{0}, {1}, {2}, {3}, {4}", m_Name, m_Value, m_BaseValue, m_Lock, m_Cap );
		}
	}
}