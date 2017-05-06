using System;
using System.Text;
using System.Windows.Forms;

using Ultima;

namespace SpyUO.Packets
{
	public enum MessageType
	{
		Regular = 0x00,
		System = 0x01,
		Emote = 0x02,
		Label = 0x06,
		Focus = 0x07,
		Whisper = 0x08,
		Yell = 0x09,
		Spell = 0x0A,

		Guild = 0x0D,
		Alliance = 0x0E,
		Command = 0x0F,

		Encoded = 0xC0
	}

	[PacketInfo( 0xC1 )]
	public class LocalizedMessage : Packet
	{
		private uint m_Serial;
		private ushort m_Body;
		private MessageType m_Type;
		private ushort m_Hue;
		private ushort m_Font;
		private uint m_Number;
		private string m_Name;
		private string m_Arguments;
		private string m_Message;

		[PacketProp( 0, "0x{0:X}" )]
		public uint Serial { get { return m_Serial; } }

		[PacketProp( 1 )]
		public ushort Body { get { return m_Body; } }

		[PacketProp( 2 )]
		public MessageType Type { get { return m_Type; } }

		[PacketProp( 3 )]
		public ushort Hue { get { return m_Hue; } }

		[PacketProp( 4 )]
		public ushort Font { get { return m_Font; } }

		[PacketProp( 5 )]
		public uint Number { get { return m_Number; } }
				
		[PacketProp( 6 )]
		public string Name { get { return m_Name; } }

		[PacketProp( 7 )]
		public string Arguments { get { return m_Arguments; } }

		[PacketProp( 8 )]
		public string Message { get { return m_Message; } }

		public LocalizedMessage( PacketReader reader, bool send ) : base( reader, send )
		{
			int size = reader.ReadInt16();

			m_Serial = reader.ReadUInt32();
			m_Body = reader.ReadUInt16();
			m_Type = (MessageType) reader.ReadByte();
			m_Hue = reader.ReadUInt16();
			m_Font = reader.ReadUInt16();
			m_Number = reader.ReadUInt32();
			m_Name = reader.ReadASCIIString( 30 );
			m_Arguments = Encoding.Unicode.GetString( reader.Data, reader.Index, reader.Data.Length - reader.Index - 2 );
			m_Message = LocalizedList.GetString( m_Number );
		}
	}
}