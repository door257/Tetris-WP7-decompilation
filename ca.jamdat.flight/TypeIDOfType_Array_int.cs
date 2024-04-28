namespace ca.jamdat.flight
{
	public class TypeIDOfType_Array_int
	{
		public const sbyte v = 93;

		public const sbyte w = 93;

		public static TypeIDOfType_Array_int[] InstArrayTypeIDOfType_Array_int(int size)
		{
			TypeIDOfType_Array_int[] array = new TypeIDOfType_Array_int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Array_int();
			}
			return array;
		}

		public static TypeIDOfType_Array_int[][] InstArrayTypeIDOfType_Array_int(int size1, int size2)
		{
			TypeIDOfType_Array_int[][] array = new TypeIDOfType_Array_int[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_int[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_int();
				}
			}
			return array;
		}

		public static TypeIDOfType_Array_int[][][] InstArrayTypeIDOfType_Array_int(int size1, int size2, int size3)
		{
			TypeIDOfType_Array_int[][][] array = new TypeIDOfType_Array_int[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_int[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_int[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Array_int();
					}
				}
			}
			return array;
		}
	}
}
