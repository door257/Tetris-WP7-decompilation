namespace ca.jamdat.flight
{
	public class TypeIDOfType_Shape
	{
		public const sbyte v = 76;

		public const sbyte w = 76;

		public static TypeIDOfType_Shape[] InstArrayTypeIDOfType_Shape(int size)
		{
			TypeIDOfType_Shape[] array = new TypeIDOfType_Shape[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Shape();
			}
			return array;
		}

		public static TypeIDOfType_Shape[][] InstArrayTypeIDOfType_Shape(int size1, int size2)
		{
			TypeIDOfType_Shape[][] array = new TypeIDOfType_Shape[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Shape[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Shape();
				}
			}
			return array;
		}

		public static TypeIDOfType_Shape[][][] InstArrayTypeIDOfType_Shape(int size1, int size2, int size3)
		{
			TypeIDOfType_Shape[][][] array = new TypeIDOfType_Shape[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Shape[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Shape[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Shape();
					}
				}
			}
			return array;
		}
	}
}
