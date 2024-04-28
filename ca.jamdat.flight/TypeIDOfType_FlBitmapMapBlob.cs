namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlBitmapMapBlob
	{
		public const sbyte v = 52;

		public const sbyte w = 52;

		public static TypeIDOfType_FlBitmapMapBlob[] InstArrayTypeIDOfType_FlBitmapMapBlob(int size)
		{
			TypeIDOfType_FlBitmapMapBlob[] array = new TypeIDOfType_FlBitmapMapBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMapBlob();
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapMapBlob[][] InstArrayTypeIDOfType_FlBitmapMapBlob(int size1, int size2)
		{
			TypeIDOfType_FlBitmapMapBlob[][] array = new TypeIDOfType_FlBitmapMapBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMapBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapMapBlob();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapMapBlob[][][] InstArrayTypeIDOfType_FlBitmapMapBlob(int size1, int size2, int size3)
		{
			TypeIDOfType_FlBitmapMapBlob[][][] array = new TypeIDOfType_FlBitmapMapBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapMapBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapMapBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlBitmapMapBlob();
					}
				}
			}
			return array;
		}
	}
}
