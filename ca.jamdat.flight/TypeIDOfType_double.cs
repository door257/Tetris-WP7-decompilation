namespace ca.jamdat.flight
{
	public class TypeIDOfType_double
	{
		public const sbyte v = 11;

		public const sbyte w = 11;

		public static TypeIDOfType_double[] InstArrayTypeIDOfType_double(int size)
		{
			TypeIDOfType_double[] array = new TypeIDOfType_double[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_double();
			}
			return array;
		}

		public static TypeIDOfType_double[][] InstArrayTypeIDOfType_double(int size1, int size2)
		{
			TypeIDOfType_double[][] array = new TypeIDOfType_double[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_double[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_double();
				}
			}
			return array;
		}

		public static TypeIDOfType_double[][][] InstArrayTypeIDOfType_double(int size1, int size2, int size3)
		{
			TypeIDOfType_double[][][] array = new TypeIDOfType_double[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_double[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_double[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_double();
					}
				}
			}
			return array;
		}
	}
}
