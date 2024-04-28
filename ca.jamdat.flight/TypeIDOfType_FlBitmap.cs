namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlBitmap
	{
		public const sbyte v = 21;

		public const sbyte w = 21;

		public static TypeIDOfType_FlBitmap[] InstArrayTypeIDOfType_FlBitmap(int size)
		{
			TypeIDOfType_FlBitmap[] array = new TypeIDOfType_FlBitmap[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlBitmap();
			}
			return array;
		}

		public static TypeIDOfType_FlBitmap[][] InstArrayTypeIDOfType_FlBitmap(int size1, int size2)
		{
			TypeIDOfType_FlBitmap[][] array = new TypeIDOfType_FlBitmap[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmap[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmap();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlBitmap[][][] InstArrayTypeIDOfType_FlBitmap(int size1, int size2, int size3)
		{
			TypeIDOfType_FlBitmap[][][] array = new TypeIDOfType_FlBitmap[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmap[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmap[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlBitmap();
					}
				}
			}
			return array;
		}
	}
}
