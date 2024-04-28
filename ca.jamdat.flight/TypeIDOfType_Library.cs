namespace ca.jamdat.flight
{
	public class TypeIDOfType_Library
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_Library[] InstArrayTypeIDOfType_Library(int size)
		{
			TypeIDOfType_Library[] array = new TypeIDOfType_Library[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Library();
			}
			return array;
		}

		public static TypeIDOfType_Library[][] InstArrayTypeIDOfType_Library(int size1, int size2)
		{
			TypeIDOfType_Library[][] array = new TypeIDOfType_Library[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Library[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Library();
				}
			}
			return array;
		}

		public static TypeIDOfType_Library[][][] InstArrayTypeIDOfType_Library(int size1, int size2, int size3)
		{
			TypeIDOfType_Library[][][] array = new TypeIDOfType_Library[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Library[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Library[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Library();
					}
				}
			}
			return array;
		}
	}
}
