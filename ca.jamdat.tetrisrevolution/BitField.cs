namespace ca.jamdat.tetrisrevolution
{
	public class BitField
	{
		public static bool IsBitOn(int bitfield, int mask)
		{
			return (bitfield & mask) != 0;
		}

		public static bool IsBitOff(int bitfield, int mask)
		{
			return (bitfield & mask) == 0;
		}

		public static int SetBitOn(int bitfield, int mask)
		{
			return bitfield | mask;
		}

		public static int SetBitOff(int bitfield, int mask)
		{
			return bitfield & ~mask;
		}

		public static int SetBit(int bitfield, int mask, bool on)
		{
			if (on)
			{
				return SetBitOn(bitfield, mask);
			}
			return SetBitOff(bitfield, mask);
		}

		public static int GetValue(int bitfield, int mask, int rightshift)
		{
			return Rshift(bitfield & mask, rightshift);
		}

		public static int SetValue(int bitfield, int value, int mask, int leftshift)
		{
			return (bitfield & ~mask) | (value << leftshift);
		}

		public static int AddValue(int bitfield, int delta, int mask, int shift)
		{
			int value = GetValue(bitfield, mask, shift) + delta;
			return SetValue(bitfield, value, mask, shift);
		}

		public static int SetBool(int bitfield, bool value, int mask)
		{
			if (value)
			{
				return SetBitOn(bitfield, mask);
			}
			return SetBitOff(bitfield, mask);
		}

		public static int BoolToBit(bool value)
		{
			if (value)
			{
				return 1;
			}
			return 0;
		}

		public static bool BitToBool(int bit)
		{
			if (bit == 1)
			{
				return true;
			}
			return false;
		}

		public static bool WillOverflow(int newVal, int mask, int shift)
		{
			int num = Rshift(mask, shift);
			return ((newVal | num) & ~num) != 0;
		}

		public static int Rshift(int val, int shift)
		{
			return val >> shift;
		}

		public static BitField[] InstArrayBitField(int size)
		{
			BitField[] array = new BitField[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new BitField();
			}
			return array;
		}

		public static BitField[][] InstArrayBitField(int size1, int size2)
		{
			BitField[][] array = new BitField[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BitField[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BitField();
				}
			}
			return array;
		}

		public static BitField[][][] InstArrayBitField(int size1, int size2, int size3)
		{
			BitField[][][] array = new BitField[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BitField[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BitField[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new BitField();
					}
				}
			}
			return array;
		}
	}
}
