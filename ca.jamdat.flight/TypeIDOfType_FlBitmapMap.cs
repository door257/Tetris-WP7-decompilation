namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlBitmapMap
	{
		public const sbyte v = 37;

		public const sbyte w = 37;

		public static TypeIDOfType_FlBitmapMap[] InstArrayTypeIDOfType_FlBitmapMap(int size)
		{
			TypeIDOfType_FlBitmapMap[] array = new TypeIDOfType_FlBitmapMap[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMap();
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapMap[][] InstArrayTypeIDOfType_FlBitmapMap(int size1, int size2)
		{
			TypeIDOfType_FlBitmapMap[][] array = new TypeIDOfType_FlBitmapMap[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMap[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapMap();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapMap[][][] InstArrayTypeIDOfType_FlBitmapMap(int size1, int size2, int size3)
		{
			TypeIDOfType_FlBitmapMap[][][] array = new TypeIDOfType_FlBitmapMap[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMap[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapMap[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlBitmapMap();
					}
				}
			}
			return array;
		}
	}
}
