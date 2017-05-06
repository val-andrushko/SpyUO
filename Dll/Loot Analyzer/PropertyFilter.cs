using System;
using System.IO;
using System.Collections.Generic;

namespace SpyUO
{
	public class PropertyFilter
	{
		#region Properties
		#region Values
		private List<object> m_Values;

		/// <summary>
		/// Allowed values in a property.
		/// </summary>
		public List<object> Values
		{
			get { return m_Values; }
		}
		#endregion

		#region Exclude
		private bool m_Exclude;

		/// <summary>
		/// Determines whether <see cref="Values"/> are (false) or not (true) allowed in a property.
		/// </summary>
		public bool Exclude
		{
			get { return m_Exclude; }
			set { m_Exclude = value; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Constructs a new instance of <see cref="PropertyFilter"/>.
		/// </summary>
		public PropertyFilter()
		{
			m_Values = new List<object>();
			m_Exclude = false;
		}
		#endregion

		#region Methods
		#region Filters
		/// <summary>
		/// Determines whether a value is allowed 
		/// </summary>
		/// <param name="value">Value to filter.</param>
		/// <returns>true if <paramref name="value"/> is filtered out, false otherwise.</returns>
		public bool Filters( object value )
		{
			if ( m_Values.Count == 0 )
				return false;

			foreach ( object o in m_Values )
				if ( o.Equals( value ) )
					return m_Exclude;

			return !m_Exclude;
		}
		#endregion

		#region Save
		/// <summary>
		/// Saves this property filter to binary stream.
		/// </summary>
		/// <param name="writer">Binary stream.</param>
		public void Save( BinaryWriter writer )
		{
			writer.Write( m_Exclude );

			writer.Write( m_Values.Count );

			foreach ( object o in m_Values )
			{
				if ( o is string )
				{
					writer.Write( (byte) 3 );
					writer.Write( (string) o );
				}
				else if ( o is int )
				{
					writer.Write( (byte) 2 );
					writer.Write( (int) o );
				}
				else if ( o is double )
				{
					writer.Write( (byte) 1 );
					writer.Write( (double) o );
				}
				else
					writer.Write( (byte) 0 );
			}
		}
		#endregion

		#region Load
		/// <summary>
		/// Loads this property filter from binary stream.
		/// </summary>
		/// <param name="reader">Binary stream.</param>
		public void Load( BinaryReader reader )
		{
			m_Exclude = reader.ReadBoolean();

			int count = reader.ReadInt32();

			for ( int i = 0; i < count; i++ )
			{
				switch ( reader.ReadByte() )
				{
					case 3: m_Values.Add( reader.ReadString() ); break;
					case 2: m_Values.Add( reader.ReadInt32() ); break;
					case 1: m_Values.Add( reader.ReadDouble() ); break;
				}
			}
		}
		#endregion
		#endregion
	}
}
