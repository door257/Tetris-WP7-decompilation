namespace ca.jamdat.flight
{
	public class TypeIDOfType_IndexedSprite
	{
		public const sbyte v = 90;

		public const sbyte w = 90;

		public static TypeIDOfType_IndexedSprite[] InstArrayTypeIDOfType_IndexedSprite(int size)
		{
			TypeIDOfType_IndexedSprite[] array = new TypeIDOfType_IndexedSprite[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_IndexedSprite();
			}
			return array;
		}

		public static TypeIDOfType_IndexedSprite[][] InstArrayTypeIDOfType_IndexedSprite(int size1, int size2)
		{
			TypeIDOfType_IndexedSprite[][] array = new TypeIDOfType_IndexedSprite[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexedSprite[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexedSprite();
				}
			}
			return array;
		}

		public static TypeIDOfType_IndexedSprite[][][] InstArrayTypeIDOfType_IndexedSprite(int size1, int size2, int size3)
		{
			TypeIDOfType_IndexedSprite[][][] array = new TypeIDOfType_IndexedSprite[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexedSprite[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexedSprite[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_IndexedSprite();
					}
				}
			}
			return array;
		}
	}
}
