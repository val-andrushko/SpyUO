using System;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;

namespace SpyUO.Packets
{
	public class Packet
	{
		private static Type[] m_Table;
		private static Type[] m_CommandTable;
		private static PacketProp[][] m_PropsTable;
		private static PacketProp[][] m_CommandPropsTable;

		public static Type[] Table { get { return m_Table; } }
		public static Type[] CommandTable { get { return m_CommandTable; } }
		public static PacketProp[][] PropsTable { get { return m_PropsTable; } }
		public static PacketProp[][] CommandPropsTable { get { return m_CommandPropsTable; } }

		static Packet()
		{
			m_Table = new Type[ 0x100 ];
			m_CommandTable = new Type[ 0x100 ];
			m_PropsTable = new PacketProp[0x100][];
			m_CommandPropsTable = new PacketProp[ 0x100 ][];

			Type[] types = Assembly.GetExecutingAssembly().GetTypes();

			foreach ( Type type in types )
			{
				PacketInfoAttribute[] attrs = (PacketInfoAttribute[]) type.GetCustomAttributes( typeof( PacketInfoAttribute ), false );

				if ( attrs.Length > 0 )
				{
					byte id = attrs[0].ID;
					ushort comm = attrs[0].Command;

					if ( id == 0xBF )
						m_CommandTable[ comm ] = type;
					else
						m_Table[ id ] = type;

					PropertyInfo[] properties = type.GetProperties();
					ArrayList list = new ArrayList();

					foreach ( PropertyInfo propInfo in properties )
					{
						PacketPropAttribute[] propsAttrs = (PacketPropAttribute[]) propInfo.GetCustomAttributes( typeof( PacketPropAttribute ), true );

						if ( propsAttrs.Length > 0 )
						{
							PacketProp pp = new PacketProp( propInfo, propsAttrs[0], null );

							list.Add( pp );
						}
					}

					list.Sort();

					if ( id == 0xBF )
						m_CommandPropsTable[ comm ] = (PacketProp[]) list.ToArray( typeof( PacketProp ) );
					else
						m_PropsTable[ id ] = (PacketProp[]) list.ToArray( typeof( PacketProp ) );
				}
			}
		}

		public static Packet Create( byte[] data, bool send )
		{
			PacketReader reader = new PacketReader( data );
			byte id = reader.ReadByte();
			Type type = null;

			if ( id == 0xBF )
			{
				ushort size = reader.ReadUInt16();
				byte cmd = (byte) reader.ReadUInt16();
				type = m_CommandTable[ cmd ];
			}
			else
				type = m_Table[ id ];

			if ( type != null )
				return (Packet) Activator.CreateInstance( type, new object[] { reader, send } );
			else
				return new Packet( reader, send );
		}

		private byte[] m_Data;
		private bool m_Send;

		public byte[] Data { get { return m_Data; } }
		public bool Send { get { return m_Send; } }
		public byte PacketID { get { return m_Data[ 0 ]; } }
		public byte Command { get { return m_Data.Length > 4 ? m_Data[ 4 ] : (byte) 0 ; } }

		public Packet( PacketReader reader, bool send )
		{
			m_Data = reader.Data;
			m_Send = send;
		}

		public PacketProp[] GetPacketProperties()
		{
			byte id = PacketID;
			PacketProp[] tableProps = null;

			if ( id == 0xBF )
				tableProps = m_CommandPropsTable[ Command ];
			else
				tableProps = m_PropsTable[ id ];

			if ( tableProps != null )
			{
				PacketProp[] props = new PacketProp[tableProps.Length];

				for ( int i = 0; i < props.Length; i++ )
				{
					PacketProp pp = tableProps[i];
					props[i] = new PacketProp( pp, pp.PropInfo.GetValue( this, null ) );
				}

				return props;
			}
			else
				return null;
		}

		public virtual void AddContextMenuItems( ToolStripItemCollection list )
		{
		}
	}
}