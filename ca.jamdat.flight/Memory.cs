using System;

namespace ca.jamdat.flight
{
	public class Memory
	{
		public static long CalculateCRC(sbyte[] data, int startPos, int size)
		{
			long[] array = new long[256];
			long num;
			for (int i = 0; i < 256; i++)
			{
				num = i;
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 1) == 0) ? (num >> 1) : (0xEDB88320u ^ (num >> 1)));
				}
				array[i] = num;
			}
			num = 4294967295L;
			for (int i = startPos; i < size + startPos; i++)
			{
				num = array[(int)(num ^ data[i]) & 0xFF] ^ (num >> 8);
			}
			long result = num ^ 0xFFFFFFFFu;
			array = null;
			return result;
		}

		public static long CalculateCRC(int[] data, int startPos, int size)
		{
			long[] array = new long[256];
			long num;
			for (int i = 0; i < 256; i++)
			{
				num = i;
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 1) == 0) ? (num >> 1) : (0xEDB88320u ^ (num >> 1)));
				}
				array[i] = num;
			}
			num = 4294967295L;
			for (int i = startPos; i < size + startPos; i++)
			{
				int num2 = data[i];
				for (int k = 0; k < 4; k++)
				{
					sbyte b = (sbyte)((num2 >> k * 8) & 0xFF);
					num = array[(int)(num ^ b) & 0xFF] ^ (num >> 8);
				}
			}
			long result = num ^ 0xFFFFFFFFu;
			array = null;
			return result;
		}

		public static void WriteIntToByte(int data, sbyte[] result, int index)
		{
			for (int i = 1; i < index + 1; i++)
			{
				result[result.Length - i] = (sbyte)(data >> 24 - i * 8);
			}
		}

		public static int WriteIntToByteArray(int data, sbyte[] result, int index)
		{
			result[index++] = (sbyte)(data >> 24);
			result[index++] = (sbyte)(data >> 16);
			result[index++] = (sbyte)(data >> 8);
			result[index++] = (sbyte)data;
			return index;
		}

		public static long ReadLong64FromArray(sbyte[] data, int index)
		{
			sbyte b = (sbyte)(data[index] & 0xFF);
			sbyte b2 = (sbyte)(data[index + 1] & 0xFF);
			sbyte b3 = (sbyte)(data[index + 2] & 0xFF);
			sbyte b4 = (sbyte)(data[index + 3] & 0xFF);
			sbyte b5 = (sbyte)(data[index + 4] & 0xFF);
			sbyte b6 = (sbyte)(data[index + 5] & 0xFF);
			sbyte b7 = (sbyte)(data[index + 6] & 0xFF);
			sbyte b8 = (sbyte)(data[index + 7] & 0xFF);
			long num = ((b & 0xFF) << 24) | ((b2 & 0xFF) << 16) | ((b3 & 0xFF) << 8) | (b4 & 0xFF);
			long num2 = ((b5 & 0xFF) << 24) | ((b6 & 0xFF) << 16) | ((b7 & 0xFF) << 8) | (b8 & 0xFF);
			return (num << 32) | (num2 & 0xFFFFFFFFu);
		}

		public static void WriteLong64ToByteArray(sbyte[] data, long toWrite, int index)
		{
			sbyte toWrite2 = (sbyte)((toWrite >> 56) & 0xFF);
			sbyte toWrite3 = (sbyte)((toWrite >> 48) & 0xFF);
			sbyte toWrite4 = (sbyte)((toWrite >> 40) & 0xFF);
			sbyte toWrite5 = (sbyte)((toWrite >> 32) & 0xFF);
			sbyte toWrite6 = (sbyte)((toWrite >> 24) & 0xFF);
			sbyte toWrite7 = (sbyte)((toWrite >> 16) & 0xFF);
			sbyte toWrite8 = (sbyte)((toWrite >> 8) & 0xFF);
			sbyte toWrite9 = (sbyte)(toWrite & 0xFF);
			WriteByte(data, toWrite2, index);
			WriteByte(data, toWrite3, index + 1);
			WriteByte(data, toWrite4, index + 2);
			WriteByte(data, toWrite5, index + 3);
			WriteByte(data, toWrite6, index + 4);
			WriteByte(data, toWrite7, index + 5);
			WriteByte(data, toWrite8, index + 6);
			WriteByte(data, toWrite9, index + 7);
		}

		public static sbyte ReadByte(sbyte[] data, int index)
		{
			return data[index];
		}

		public static void WriteByte(sbyte[] data, sbyte toWrite, int index)
		{
			data[index] = toWrite;
		}

		public static sbyte MakeByte(int b)
		{
			return (sbyte)b;
		}

		public static int MakeUnsignedByte(sbyte b)
		{
			return b & 0xFF;
		}

		public static void Set(sbyte[] inDest, int value, int size)
		{
			for (int i = 0; i < size; i++)
			{
				inDest[i] = (sbyte)value;
			}
		}

		public static void Zero(sbyte[] inDest, int size)
		{
			Set(inDest, 0, size);
		}

		public static void Set(int[] inDest, int value, int size)
		{
			for (int i = 0; i < size; i++)
			{
				inDest[i] = value;
			}
		}

		public static void Zero(int[] inDest, int size)
		{
			Set(inDest, 0, size);
		}

		public static void Set(short[] inDest, int value, int size)
		{
			for (int i = 0; i < size; i++)
			{
				inDest[i] = (short)value;
			}
		}

		public static void Zero(short[] inDest, int size)
		{
			Set(inDest, 0, size);
		}

		public static void Copy(object inDest, object inSource, int size)
		{
			Array.Copy((Array)inSource, 0, (Array)inDest, 0, size);
		}

		public static void Copy(sbyte[] inDest, int destOffset, sbyte[] inSource, int srcOffset, int size)
		{
			Array.Copy(inSource, srcOffset, inDest, destOffset, size);
		}

		public static Memory[] InstArrayMemory(int size)
		{
			Memory[] array = new Memory[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Memory();
			}
			return array;
		}

		public static Memory[][] InstArrayMemory(int size1, int size2)
		{
			Memory[][] array = new Memory[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Memory[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Memory();
				}
			}
			return array;
		}

		public static Memory[][][] InstArrayMemory(int size1, int size2, int size3)
		{
			Memory[][][] array = new Memory[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Memory[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Memory[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Memory();
					}
				}
			}
			return array;
		}
	}
}
