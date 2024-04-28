namespace ca.jamdat.flight
{
	public class TypeIDOfType_IndexedTexture
	{
		public const sbyte v = 103;

		public const sbyte w = 103;

		public static TypeIDOfType_IndexedTexture[] InstArrayTypeIDOfType_IndexedTexture(int size)
		{
			TypeIDOfType_IndexedTexture[] array = new TypeIDOfType_IndexedTexture[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_IndexedTexture();
			}
			return array;
		}

		public static TypeIDOfType_IndexedTexture[][] InstArrayTypeIDOfType_IndexedTexture(int size1, int size2)
		{
			TypeIDOfType_IndexedTexture[][] array = new TypeIDOfType_IndexedTexture[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexedTexture[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexedTexture();
				}
			}
			return array;
		}

		public static TypeIDOfType_IndexedTexture[][][] InstArrayTypeIDOfType_IndexedTexture(int size1, int size2, int size3)
		{
			TypeIDOfType_IndexedTexture[][][] array = new TypeIDOfType_IndexedTexture[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexedTexture[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexedTexture[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_IndexedTexture();
					}
				}
			}
			return array;
		}
	}
}
