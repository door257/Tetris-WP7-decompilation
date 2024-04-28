namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlSystemFontBlob
	{
		public const sbyte v = 113;

		public const sbyte w = 113;

		public static TypeIDOfType_FlSystemFontBlob[] InstArrayTypeIDOfType_FlSystemFontBlob(int size)
		{
			TypeIDOfType_FlSystemFontBlob[] array = new TypeIDOfType_FlSystemFontBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlSystemFontBlob();
			}
			return array;
		}

		public static TypeIDOfType_FlSystemFontBlob[][] InstArrayTypeIDOfType_FlSystemFontBlob(int size1, int size2)
		{
			TypeIDOfType_FlSystemFontBlob[][] array = new TypeIDOfType_FlSystemFontBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSystemFontBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSystemFontBlob();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlSystemFontBlob[][][] InstArrayTypeIDOfType_FlSystemFontBlob(int size1, int size2, int size3)
		{
			TypeIDOfType_FlSystemFontBlob[][][] array = new TypeIDOfType_FlSystemFontBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSystemFontBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSystemFontBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlSystemFontBlob();
					}
				}
			}
			return array;
		}
	}
}
