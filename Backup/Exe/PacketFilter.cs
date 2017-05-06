using System;
using SpyUO.Packets;

namespace SpyUO
{
	public class PacketFilter
	{
		private bool[] m_Table;
		private bool[] m_CommandTable;
		private bool[][] m_PropsHave;
		private bool[][] m_CommandPropsHave;
		private string[][] m_PropsTable;
		private string[][] m_CommandPropsTable;

		public bool[] Table { get { return m_Table; } }
		public bool[] CommandTable { get { return m_CommandTable; } }
		public bool[][] PropsHave { get { return m_PropsHave; } }
		public bool[][] CommandPropsHave { get { return m_CommandPropsHave; } }
		public string[][] PropsTable { get { return m_PropsTable; } }
		public string[][] CommandPropsTable { get { return m_CommandPropsTable; } }

		public PacketFilter()
		{
			m_Table = new bool[ 0x100 ];
			m_CommandTable = new bool[ 0x100 ];
			m_PropsHave = new bool[ 0x100 ][];
			m_CommandPropsHave = new bool[ 0x100 ][];
			m_PropsTable = new string[ 0x100 ][];
			m_CommandPropsTable = new string[ 0x100 ][];

			for ( int i = 0; i < m_Table.Length; i++ )
			{
				m_Table[ i ] = Packet.Table[ i ] != null;

				PacketProp[] props = Packet.PropsTable[ i ];

				if ( props != null )
				{
					m_PropsTable[ i ] = new string[ props.Length ];
					m_PropsHave[ i ] = new bool[ props.Length ];

					for ( int j = 0; j < props.Length; j++ )
						m_PropsHave[ i ][ j ] = true;
				}
			}

			for ( int i = 0; i < m_CommandTable.Length; i++ )
			{
				m_CommandTable[ i ] = Packet.CommandTable[ i ] != null;

				PacketProp[] props = Packet.CommandPropsTable[ i ];

				if ( props != null )
				{
					m_CommandPropsTable[ i ] = new string[ props.Length ];
					m_CommandPropsHave[ i ] = new bool[ props.Length ];

					for ( int j = 0; j < props.Length; j++ )
						m_CommandPropsHave[ i ][ j ] = true;
				}
			}
		}

		public bool Filter( TimePacket timePacket )
		{
			Packet packet = timePacket.Packet;
			byte id = packet.PacketID;
			ushort com = 0;

			if ( id == 0xBF )
			{
				com = packet.Command;

				if ( com < 0x100 && !m_CommandTable[ com ] )
					return false;
			}
			else if ( !m_Table[ id ] )
				return false;

			PacketProp[] props = packet.GetPacketProperties();

			if ( props != null )
			{
				string[] filterProps = null;
				bool[] mustHave = null;
					
				if ( id == 0xBF )
				{
					if ( com < 0x100 )
					{
						filterProps = m_CommandPropsTable[ com ];
						mustHave = m_CommandPropsHave[ com ];
					}
					else
						return false;
				}
				else
				{
					filterProps = m_PropsTable[ id ];
					mustHave = m_PropsHave[ id ];
				}

				for ( int i = 0; i < props.Length; i++ )
				{
					if ( mustHave[ i ] )
					{
						if ( filterProps[ i ] != null && !FilterProp( filterProps[ i ], props[ i ].GetStringValue() ) )
							return false;
					}
					else
					{
						if ( filterProps[ i ] != null && FilterProp( filterProps[ i ], props[ i ].GetStringValue() ) )
							return false;
					}
				}
			}

			return true;
		}

		private bool FilterProp( string filterProp, string prop )
		{
			string[] splt = filterProp.Split( '|' );

			foreach ( string s in splt )
			{
				if ( s == prop )
					return true;
			}

			return false;
		}
	}
}