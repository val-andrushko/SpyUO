using System;
using System.IO;

namespace SpyUO.Packets
{
	[PacketInfo( 0x1A )]
	public class WorldItem : Packet
	{
		private ushort m_ItemId;
		private ushort m_Hue;
		private Point3D m_Position;
		private uint m_Serial;
		private ushort m_Count;
		private byte m_Direction;
		private byte m_Flag;

		[PacketProp( 0, "0x{0:X}" )]
		public ushort ItemId { get { return m_ItemId; } }

		[PacketProp( 1 )]
		public string ItemIdName
		{
			get
			{
				try
				{
					if ( m_ItemId < 0x4000 )
						return Ultima.TileData.ItemTable[m_ItemId].Name;

					return "Over 0x4000";
				}
				catch
				{
					return null;   
				}
			} 
		}

		[PacketProp( 2, "0x{0:X}" )]
		public uint Hue { get { return m_Hue; } }

		[PacketProp( 3 )]
		public Point3D Position { get { return m_Position; } }

		[PacketProp( 4, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 5 )]
		public ushort Count { get { return m_Count; } }

		[PacketProp( 6, "0x{0:X}" )]
		public byte Direction { get { return m_Direction; } }

		[PacketProp( 7, "0x{0:X}" )]
		public byte Flag { get { return m_Flag; } }

		public WorldItem( PacketReader reader, bool send ) : base( reader, send )
		{
			reader.ReadUInt16();

			m_Serial = reader.ReadUInt32();
			m_ItemId = reader.ReadUInt16();

			if ( (m_Serial & 0x80000000) != 0 )
			{
				m_Serial &= ~0x80000000;
				m_Count = reader.ReadUInt16();
			}
			else
				m_Count = 0;

			if ( (m_ItemId & 0x8000) != 0 )
			{
				m_ItemId &= 0x7FFF;
				m_ItemId += reader.ReadByte();
			}

			ushort x = reader.ReadUInt16();
			ushort y = reader.ReadUInt16();

			if ( (x & 0x8000) != 0 )
				m_Direction = reader.ReadByte();
			else
				m_Direction = 0;

			sbyte z = reader.ReadSByte();

			if ( (y & 0x8000) != 0 )
				m_Hue = reader.ReadUInt16();
			else
				m_Hue = 0;

			if ( (y & 0x4000) != 0 )
				m_Flag = reader.ReadByte();
			else
				m_Flag = 0;

			m_Position = new Point3D( x & ~0x8000, y & ~0xC000, z );
		}

		public void WriteRunUOClass( StreamWriter writer, string name )
		{
			writer.WriteLine( "using System;" );
			writer.WriteLine( "using Server;" );

			writer.WriteLine();

			writer.WriteLine( "namespace Server.Items" );
			writer.WriteLine( "{" );

			writer.WriteLine( "\tpublic class {0} : Item", name );
			writer.WriteLine( "\t{" );

			writer.WriteLine( "\t\t[Constructable]" );
			writer.WriteLine( "\t\tpublic {0}() : base( 0x{1} )", name, m_ItemId.ToString( "X" ) );
			writer.WriteLine( "\t\t{" );

			if ( m_Count > 0 )
			{
				writer.WriteLine( "\t\t\tStackable = true;" );
			}

			if ( m_Hue > 0 )
			{
				writer.WriteLine( "\t\t\tHue = 0x{0};", m_Hue.ToString( "X" ) );
			}

			writer.WriteLine( "\t\t}\n" );


			writer.WriteLine( "\t\tpublic {0}( Serial serial ) : base( serial )", name );
			writer.WriteLine( "\t\t{" );
			writer.WriteLine( "\t\t}\n" );


			writer.WriteLine( "\t\tpublic override void Serialize( GenericWriter writer )" );
			writer.WriteLine( "\t\t{" );
			writer.WriteLine( "\t\t\tbase.Serialize( writer );" );
			writer.WriteLine();
			writer.WriteLine( "\t\t\twriter.WriteEncodedInt( 0 ); // version" );
			writer.WriteLine( "\t\t}\n" );


			writer.WriteLine( "\t\tpublic override void Deserialize( GenericReader reader )" );
			writer.WriteLine( "\t\t{" );
			writer.WriteLine( "\t\t\tbase.Deserialize( reader );" );
			writer.WriteLine();
			writer.WriteLine( "\t\t\tint version = reader.ReadEncodedInt();" );
			writer.WriteLine( "\t\t}" );

			writer.WriteLine( "\t}" );
			writer.Write( "}" );
		}
	}
}