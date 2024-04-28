namespace ca.jamdat.flight
{
	public class TypeIDOfType_Vector2_short
	{
		public const sbyte v = 39;

		public const sbyte w = 39;

		public static TypeIDOfType_Vector2_short[] InstArrayTypeIDOfType_Vector2_short(int size)
		{
			TypeIDOfType_Vector2_short[] array = new TypeIDOfType_Vector2_short[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Vector2_short();
			}
			return array;
		}

		public static TypeIDOfType_Vector2_short[][] InstArrayTypeIDOfType_Vector2_short(int size1, int size2)
		{
			TypeIDOfType_Vector2_short[][] array = new TypeIDOfType_Vector2_short[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Vector2_short[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Vector2_short();
				}
			}
			return array;
		}

		public static TypeIDOfType_Vector2_short[][][] InstArrayTypeIDOfType_Vector2_short(int size1, int size2, int size3)
		{
			TypeIDOfType_Vector2_short[][][] array = new TypeIDOfType_Vector2_short[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Vector2_short[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Vector2_short[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Vector2_short();
					}
				}
			}
			return array;
		}
	}
}
