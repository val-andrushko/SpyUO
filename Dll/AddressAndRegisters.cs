using System;

namespace SpyUO
{
	public enum Register { Edi, Esi, Ebx, Edx, Ecx, Eax, Ebp, Esp }

	public struct AddressAndRegisters
	{
		public uint Address;
		public Register AddressRegister;
		public Register LengthRegister;

		public AddressAndRegisters( uint address, Register addressRegister, Register lengthRegister )
		{
			Address = address;
			AddressRegister = addressRegister;
			LengthRegister = lengthRegister;
		}
	}
}