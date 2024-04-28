namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlRect
	{
		public const sbyte v = 40;

		public const sbyte w = 40;

		public static TypeIDOfType_FlRect[] InstArrayTypeIDOfType_FlRect(int size)
		{
			TypeIDOfType_FlRect[] array = new TypeIDOfType_FlRect[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlRect();
			}
			return array;
		}

		public static TypeIDOfType_FlRect[][] InstArrayTypeIDOfType_FlRect(int size1, int size2)
		{
			TypeIDOfType_FlRect[][] array = new TypeIDOfType_FlRect[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlRect[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlRect();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlRect[][][] InstArrayTypeIDOfType_FlRect(int size1, int size2, int size3)
		{
			TypeIDOfType_FlRect[][][] array = new TypeIDOfType_FlRect[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlRect[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlRect[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlRect();
					}
				}
			}
			return array;
		}
	}
}
