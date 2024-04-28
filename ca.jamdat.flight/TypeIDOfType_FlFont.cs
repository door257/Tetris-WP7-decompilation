namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlFont
	{
		public const sbyte v = 36;

		public const sbyte w = 36;

		public static TypeIDOfType_FlFont[] InstArrayTypeIDOfType_FlFont(int size)
		{
			TypeIDOfType_FlFont[] array = new TypeIDOfType_FlFont[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlFont();
			}
			return array;
		}

		public static TypeIDOfType_FlFont[][] InstArrayTypeIDOfType_FlFont(int size1, int size2)
		{
			TypeIDOfType_FlFont[][] array = new TypeIDOfType_FlFont[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlFont[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlFont();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlFont[][][] InstArrayTypeIDOfType_FlFont(int size1, int size2, int size3)
		{
			TypeIDOfType_FlFont[][][] array = new TypeIDOfType_FlFont[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlFont[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlFont[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlFont();
					}
				}
			}
			return array;
		}
	}
}
