namespace ca.jamdat.flight
{
	public class TypeIDOfType_Sprite
	{
		public const sbyte v = 53;

		public const sbyte w = 53;

		public static TypeIDOfType_Sprite[] InstArrayTypeIDOfType_Sprite(int size)
		{
			TypeIDOfType_Sprite[] array = new TypeIDOfType_Sprite[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Sprite();
			}
			return array;
		}

		public static TypeIDOfType_Sprite[][] InstArrayTypeIDOfType_Sprite(int size1, int size2)
		{
			TypeIDOfType_Sprite[][] array = new TypeIDOfType_Sprite[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sprite[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sprite();
				}
			}
			return array;
		}

		public static TypeIDOfType_Sprite[][][] InstArrayTypeIDOfType_Sprite(int size1, int size2, int size3)
		{
			TypeIDOfType_Sprite[][][] array = new TypeIDOfType_Sprite[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sprite[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sprite[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Sprite();
					}
				}
			}
			return array;
		}
	}
}
