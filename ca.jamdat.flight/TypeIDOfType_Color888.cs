namespace ca.jamdat.flight
{
	public class TypeIDOfType_Color888
	{
		public const sbyte v = 20;

		public const sbyte w = 20;

		public static TypeIDOfType_Color888[] InstArrayTypeIDOfType_Color888(int size)
		{
			TypeIDOfType_Color888[] array = new TypeIDOfType_Color888[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Color888();
			}
			return array;
		}

		public static TypeIDOfType_Color888[][] InstArrayTypeIDOfType_Color888(int size1, int size2)
		{
			TypeIDOfType_Color888[][] array = new TypeIDOfType_Color888[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Color888[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Color888();
				}
			}
			return array;
		}

		public static TypeIDOfType_Color888[][][] InstArrayTypeIDOfType_Color888(int size1, int size2, int size3)
		{
			TypeIDOfType_Color888[][][] array = new TypeIDOfType_Color888[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Color888[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Color888[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Color888();
					}
				}
			}
			return array;
		}
	}
}
