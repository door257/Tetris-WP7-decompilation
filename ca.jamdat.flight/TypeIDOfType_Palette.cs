namespace ca.jamdat.flight
{
	public class TypeIDOfType_Palette
	{
		public const sbyte v = 33;

		public const sbyte w = 33;

		public static TypeIDOfType_Palette[] InstArrayTypeIDOfType_Palette(int size)
		{
			TypeIDOfType_Palette[] array = new TypeIDOfType_Palette[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Palette();
			}
			return array;
		}

		public static TypeIDOfType_Palette[][] InstArrayTypeIDOfType_Palette(int size1, int size2)
		{
			TypeIDOfType_Palette[][] array = new TypeIDOfType_Palette[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Palette[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Palette();
				}
			}
			return array;
		}

		public static TypeIDOfType_Palette[][][] InstArrayTypeIDOfType_Palette(int size1, int size2, int size3)
		{
			TypeIDOfType_Palette[][][] array = new TypeIDOfType_Palette[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Palette[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Palette[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Palette();
					}
				}
			}
			return array;
		}
	}
}
