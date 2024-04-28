namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlSlider
	{
		public const sbyte v = 101;

		public const sbyte w = 101;

		public static TypeIDOfType_FlSlider[] InstArrayTypeIDOfType_FlSlider(int size)
		{
			TypeIDOfType_FlSlider[] array = new TypeIDOfType_FlSlider[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlSlider();
			}
			return array;
		}

		public static TypeIDOfType_FlSlider[][] InstArrayTypeIDOfType_FlSlider(int size1, int size2)
		{
			TypeIDOfType_FlSlider[][] array = new TypeIDOfType_FlSlider[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSlider[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSlider();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlSlider[][][] InstArrayTypeIDOfType_FlSlider(int size1, int size2, int size3)
		{
			TypeIDOfType_FlSlider[][][] array = new TypeIDOfType_FlSlider[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSlider[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSlider[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlSlider();
					}
				}
			}
			return array;
		}
	}
}
