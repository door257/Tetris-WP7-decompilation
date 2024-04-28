namespace ca.jamdat.flight
{
	public class TypeIDOfType_float
	{
		public const sbyte v = 10;

		public const sbyte w = 10;

		public static TypeIDOfType_float[] InstArrayTypeIDOfType_float(int size)
		{
			TypeIDOfType_float[] array = new TypeIDOfType_float[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_float();
			}
			return array;
		}

		public static TypeIDOfType_float[][] InstArrayTypeIDOfType_float(int size1, int size2)
		{
			TypeIDOfType_float[][] array = new TypeIDOfType_float[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_float[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_float();
				}
			}
			return array;
		}

		public static TypeIDOfType_float[][][] InstArrayTypeIDOfType_float(int size1, int size2, int size3)
		{
			TypeIDOfType_float[][][] array = new TypeIDOfType_float[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_float[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_float[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_float();
					}
				}
			}
			return array;
		}
	}
}
