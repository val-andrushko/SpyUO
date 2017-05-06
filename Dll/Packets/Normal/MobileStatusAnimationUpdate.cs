using System;
using System.Text;

namespace SpyUO.Packets
{
	[PacketInfo( 0xE2 )]
	public class MobileStatusAnimationUpdate : Packet
	{
		private uint m_Serial;
		private ushort m_Action;
		private byte m_Zero;
		private byte m_Count;
		private byte m_Unk;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public ushort Action { get { return m_Action; } }

		[PacketProp( 2 )]
		public byte Zero { get { return m_Zero; } }

		[PacketProp( 3 )]
		public byte SubAction { get { return m_Count; } }

		[PacketProp( 4 )]
		public byte Unk { get { return m_Unk; } }

		public MobileStatusAnimationUpdate( PacketReader reader, bool send )
			: base( reader, send )
		{
			m_Serial = reader.ReadUInt32();
			//What is action?
			m_Action = reader.ReadUInt16();
			m_Zero = reader.ReadByte();
			m_Count = reader.ReadByte();
			m_Unk = reader.ReadByte();
		}
	}
}