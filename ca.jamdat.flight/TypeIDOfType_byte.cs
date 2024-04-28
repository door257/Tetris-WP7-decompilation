namespace ca.jamdat.flight
{
	public class TypeIDOfType_byte
	{
		public const sbyte v = 2;

		public const sbyte w = 2;

		public static TypeIDOfType_byte[] InstArrayTypeIDOfType_byte(int size)
		{
			TypeIDOfType_byte[] array = new TypeIDOfType_byte[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_byte();
			}
			return array;
		}

		public static TypeIDOfType_byte[][] InstArrayTypeIDOfType_byte(int size1, int size2)
		{
			TypeIDOfType_byte[][] array = new TypeIDOfType_byte[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_byte[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_byte();
				}
			}
			return array;
		}

		public static TypeIDOfType_byte[][][] InstArrayTypeIDOfType_byte(int size1, int size2, int size3)
		{
			TypeIDOfType_byte[][][] array = new TypeIDOfType_byte[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_byte[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_byte[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_byte();
					}
				}
			}
			return array;
		}
	}
}
