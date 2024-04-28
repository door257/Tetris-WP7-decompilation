namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlFontBlob
	{
		public const sbyte v = 112;

		public const sbyte w = 112;

		public static TypeIDOfType_FlFontBlob[] InstArrayTypeIDOfType_FlFontBlob(int size)
		{
			TypeIDOfType_FlFontBlob[] array = new TypeIDOfType_FlFontBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlFontBlob();
			}
			return array;
		}

		public static TypeIDOfType_FlFontBlob[][] InstArrayTypeIDOfType_FlFontBlob(int size1, int size2)
		{
			TypeIDOfType_FlFontBlob[][] array = new TypeIDOfType_FlFontBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlFontBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlFontBlob();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlFontBlob[][][] InstArrayTypeIDOfType_FlFontBlob(int size1, int size2, int size3)
		{
			TypeIDOfType_FlFontBlob[][][] array = new TypeIDOfType_FlFontBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlFontBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlFontBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlFontBlob();
					}
				}
			}
			return array;
		}
	}
}
