using System;
using System.Text;

using Ultima;

namespace SpyUO.Packets
{
	public enum SpellType
	{
		Mage,
		Necromancer,
		Paladin,
		Samurai,
		Ninja,
		Spellweaving,
		Unknown
	}

	public enum SpellUseType : byte
	{
		NoSpellbook = 0x0,
		Spellbook = 0x1,
		NoSpell = 0x2
	}

	public enum SpellExpansionType : ushort
	{
		AOS	= 0x0,
		SE	= 0x1,
		ML	= 0x2
	}

	[PacketInfo( 0xBF, 0x1C )]
	public class CommandCastSpell : Packet
	{
		private SpellUseType m_UseType;
		private uint m_Spellbook;
		private SpellExpansionType m_Expansion;
		private SpellType m_Type;
		private byte m_SpellID;
		private string m_SpellName;

		[PacketProp( 0 )]
		public SpellUseType UseType { get { return m_UseType; } }

		[PacketProp( 1, "0x{0:X}" )]
		public uint Spellbook { get { return m_Spellbook; } }

		[PacketProp( 2 )]
		public SpellExpansionType Expansion { get { return m_Expansion; } }

		[PacketProp( 3 )]
		public SpellType Type { get { return m_Type; } }

		[PacketProp( 4 )]
		public byte SpellID { get { return m_SpellID; } }

		[PacketProp( 5 )]
		public string SpellName { get { return m_SpellName; } }

		public CommandCastSpell( PacketReader reader, bool send ) : base( reader, send )
		{
			m_UseType = (SpellUseType) reader.ReadUInt16();

			if ( m_UseType == SpellUseType.Spellbook )
				m_Spellbook = reader.ReadUInt32();

			m_Expansion = (SpellExpansionType) reader.ReadByte();
			m_SpellID = reader.ReadByte();

			if ( m_SpellID >= 0x1 && m_SpellID <= 0x40 )
			{
				m_Type = SpellType.Mage;
				m_SpellName = LocalizedList.GetString( (uint) 1044380 + m_SpellID );
			}
			else if ( m_SpellID >= 0x59 && m_SpellID <= 0x68 )
			{
				m_Type = SpellType.Spellweaving;
				m_SpellName = LocalizedList.GetString( (uint) 1031601 + m_SpellID - 0x59 );
			}
			else if ( m_SpellID >= 0x65 && m_SpellID <= 0x75 )
			{
				m_Type = SpellType.Necromancer;
				m_SpellName = LocalizedList.GetString( (uint) 1060509 + m_SpellID - 0x65 );
			}
			else if ( m_SpellID >= 0xC9 && m_SpellID <= 0xD2 )
			{
				m_Type = SpellType.Paladin;
				m_SpellName = LocalizedList.GetString( (uint) 1060492 + m_SpellID - 0xC9 );
			}
			else if ( m_SpellID >= 0x91 && m_SpellID <= 0x96 )
			{
				m_Type = SpellType.Samurai;
				m_SpellName = LocalizedList.GetString( (uint) 1060595 + m_SpellID - 0x91 );
			}
			else if ( m_SpellID >= 0xF5 && m_SpellID <= 0xFC )
			{
				m_Type = SpellType.Ninja;
				m_SpellName = LocalizedList.GetString( (uint) 1060610 + m_SpellID - 0xF5 );
			}
			else
				m_Type = SpellType.Unknown;
		}
	}
}