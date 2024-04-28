namespace ca.jamdat.flight
{
	public class TypeIDOfType_Array_boolean
	{
		public const sbyte v = 92;

		public const sbyte w = 92;

		public static TypeIDOfType_Array_boolean[] InstArrayTypeIDOfType_Array_boolean(int size)
		{
			TypeIDOfType_Array_boolean[] array = new TypeIDOfType_Array_boolean[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Array_boolean();
			}
			return array;
		}

		public static TypeIDOfType_Array_boolean[][] InstArrayTypeIDOfType_Array_boolean(int size1, int size2)
		{
			TypeIDOfType_Array_boolean[][] array = new TypeIDOfType_Array_boolean[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_boolean[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_boolean();
				}
			}
			return array;
		}

		public static TypeIDOfType_Array_boolean[][][] InstArrayTypeIDOfType_Array_boolean(int size1, int size2, int size3)
		{
			TypeIDOfType_Array_boolean[][][] array = new TypeIDOfType_Array_boolean[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_boolean[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_boolean[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Array_boolean();
					}
				}
			}
			return array;
		}
	}
}
