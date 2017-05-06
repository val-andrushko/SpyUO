using System;

namespace SpyUO.Packets
{
	[AttributeUsage( AttributeTargets.Class )]
	public class PacketInfoAttribute : Attribute
	{
		private byte m_ID;
		private byte m_Command;

		public byte ID { get { return m_ID; } }
		public byte Command { get { return m_Command; } }

		public PacketInfoAttribute( byte id ) : this( id, 0 )
		{
			m_ID = id;
		}

		public PacketInfoAttribute( byte id, byte command )
		{
			m_ID = id;
			m_Command = command;
		}
	}
}