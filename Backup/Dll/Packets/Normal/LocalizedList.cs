using System;
using Ultima;

namespace SpyUO.Packets
{
	public class LocalizedList
	{
		private static StringList m_List = new StringList( "ENU" );

		public static string GetString( uint number )
		{
			try
			{
				return m_List.Table[ (int) number ] as string;
			}
			catch
			{
				// TODO logging
			}

			return String.Empty;
		}

		public static string Construct( uint number, string args )
		{
			string value = LocalizedList.GetString( number );
			string[] split = null;

			if ( args != null )
			{
				split = args.Split( '\t' );

				for ( int i = 0; i < split.Length; i++ )
				{
					string s = split[ i ];

					if ( s.StartsWith( "#" ) )
					{
						string sub = s.Substring( 1, s.Length - 1 );
						uint num;

						if ( UInt32.TryParse( sub, out num ) )
							split[ i ] = LocalizedList.GetString( num );
					}
				}
			}

			if ( value != null )
			{
				int diff = 0;
				int index = 0;
				int num;
				int start;
				int end;

				while ( index < value.Length && ( start = value.IndexOf( '~', index ) ) >= 0 )
				{
					end = value.IndexOf( '~', start + 1 ) + 1;
					num = value[ start + 1 ] - 49;
					diff = value.Length;

					if ( split != null && num < split.Length )
						value = value.Replace( value.Substring( start, end - start ), split[ num ] );

					diff -= value.Length;
					index = end - diff;
				}
			}

			return value.Trim();
		}
	}
}