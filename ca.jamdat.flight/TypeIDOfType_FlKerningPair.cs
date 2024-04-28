namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlKerningPair
	{
		public const sbyte v = 111;

		public const sbyte w = 111;

		public static TypeIDOfType_FlKerningPair[] InstArrayTypeIDOfType_FlKerningPair(int size)
		{
			TypeIDOfType_FlKerningPair[] array = new TypeIDOfType_FlKerningPair[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlKerningPair();
			}
			return array;
		}

		public static TypeIDOfType_FlKerningPair[][] InstArrayTypeIDOfType_FlKerningPair(int size1, int size2)
		{
			TypeIDOfType_FlKerningPair[][] array = new TypeIDOfType_FlKerningPair[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlKerningPair[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlKerningPair();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlKerningPair[][][] InstArrayTypeIDOfType_FlKerningPair(int size1, int size2, int size3)
		{
			TypeIDOfType_FlKerningPair[][][] array = new TypeIDOfType_FlKerningPair[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlKerningPair[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlKerningPair[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlKerningPair();
					}
				}
			}
			return array;
		}
	}
}
