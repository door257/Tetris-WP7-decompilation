namespace ca.jamdat.flight
{
	public class TypeIDOfType_boolean
	{
		public const sbyte v = 1;

		public const sbyte w = 1;

		public static TypeIDOfType_boolean[] InstArrayTypeIDOfType_boolean(int size)
		{
			TypeIDOfType_boolean[] array = new TypeIDOfType_boolean[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_boolean();
			}
			return array;
		}

		public static TypeIDOfType_boolean[][] InstArrayTypeIDOfType_boolean(int size1, int size2)
		{
			TypeIDOfType_boolean[][] array = new TypeIDOfType_boolean[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_boolean[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_boolean();
				}
			}
			return array;
		}

		public static TypeIDOfType_boolean[][][] InstArrayTypeIDOfType_boolean(int size1, int size2, int size3)
		{
			TypeIDOfType_boolean[][][] array = new TypeIDOfType_boolean[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_boolean[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_boolean[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_boolean();
					}
				}
			}
			return array;
		}
	}
}
