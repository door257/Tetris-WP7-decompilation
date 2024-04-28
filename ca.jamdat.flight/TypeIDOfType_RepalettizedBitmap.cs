namespace ca.jamdat.flight
{
	public class TypeIDOfType_RepalettizedBitmap
	{
		public const sbyte v = 43;

		public const sbyte w = 43;

		public static TypeIDOfType_RepalettizedBitmap[] InstArrayTypeIDOfType_RepalettizedBitmap(int size)
		{
			TypeIDOfType_RepalettizedBitmap[] array = new TypeIDOfType_RepalettizedBitmap[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_RepalettizedBitmap();
			}
			return array;
		}

		public static TypeIDOfType_RepalettizedBitmap[][] InstArrayTypeIDOfType_RepalettizedBitmap(int size1, int size2)
		{
			TypeIDOfType_RepalettizedBitmap[][] array = new TypeIDOfType_RepalettizedBitmap[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_RepalettizedBitmap[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_RepalettizedBitmap();
				}
			}
			return array;
		}

		public static TypeIDOfType_RepalettizedBitmap[][][] InstArrayTypeIDOfType_RepalettizedBitmap(int size1, int size2, int size3)
		{
			TypeIDOfType_RepalettizedBitmap[][][] array = new TypeIDOfType_RepalettizedBitmap[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_RepalettizedBitmap[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_RepalettizedBitmap[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_RepalettizedBitmap();
					}
				}
			}
			return array;
		}
	}
}
