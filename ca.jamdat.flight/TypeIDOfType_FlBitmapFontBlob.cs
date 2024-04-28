namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlBitmapFontBlob
	{
		public const sbyte v = 51;

		public const sbyte w = 51;

		public static TypeIDOfType_FlBitmapFontBlob[] InstArrayTypeIDOfType_FlBitmapFontBlob(int size)
		{
			TypeIDOfType_FlBitmapFontBlob[] array = new TypeIDOfType_FlBitmapFontBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapFontBlob();
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapFontBlob[][] InstArrayTypeIDOfType_FlBitmapFontBlob(int size1, int size2)
		{
			TypeIDOfType_FlBitmapFontBlob[][] array = new TypeIDOfType_FlBitmapFontBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapFontBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapFontBlob();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapFontBlob[][][] InstArrayTypeIDOfType_FlBitmapFontBlob(int size1, int size2, int size3)
		{
			TypeIDOfType_FlBitmapFontBlob[][][] array = new TypeIDOfType_FlBitmapFontBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapFontBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapFontBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlBitmapFontBlob();
					}
				}
			}
			return array;
		}
	}
}
