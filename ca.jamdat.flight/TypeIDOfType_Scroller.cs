namespace ca.jamdat.flight
{
	public class TypeIDOfType_Scroller
	{
		public const sbyte v = 98;

		public const sbyte w = 98;

		public static TypeIDOfType_Scroller[] InstArrayTypeIDOfType_Scroller(int size)
		{
			TypeIDOfType_Scroller[] array = new TypeIDOfType_Scroller[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Scroller();
			}
			return array;
		}

		public static TypeIDOfType_Scroller[][] InstArrayTypeIDOfType_Scroller(int size1, int size2)
		{
			TypeIDOfType_Scroller[][] array = new TypeIDOfType_Scroller[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Scroller[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Scroller();
				}
			}
			return array;
		}

		public static TypeIDOfType_Scroller[][][] InstArrayTypeIDOfType_Scroller(int size1, int size2, int size3)
		{
			TypeIDOfType_Scroller[][][] array = new TypeIDOfType_Scroller[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Scroller[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Scroller[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Scroller();
					}
				}
			}
			return array;
		}
	}
}
