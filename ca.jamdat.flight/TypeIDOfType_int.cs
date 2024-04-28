namespace ca.jamdat.flight
{
	public class TypeIDOfType_int
	{
		public const sbyte v = 5;

		public const sbyte w = 5;

		public static TypeIDOfType_int[] InstArrayTypeIDOfType_int(int size)
		{
			TypeIDOfType_int[] array = new TypeIDOfType_int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_int();
			}
			return array;
		}

		public static TypeIDOfType_int[][] InstArrayTypeIDOfType_int(int size1, int size2)
		{
			TypeIDOfType_int[][] array = new TypeIDOfType_int[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_int[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_int();
				}
			}
			return array;
		}

		public static TypeIDOfType_int[][][] InstArrayTypeIDOfType_int(int size1, int size2, int size3)
		{
			TypeIDOfType_int[][][] array = new TypeIDOfType_int[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_int[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_int[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_int();
					}
				}
			}
			return array;
		}
	}
}
