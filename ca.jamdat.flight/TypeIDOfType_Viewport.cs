namespace ca.jamdat.flight
{
	public class TypeIDOfType_Viewport
	{
		public const sbyte v = 68;

		public const sbyte w = 68;

		public static TypeIDOfType_Viewport[] InstArrayTypeIDOfType_Viewport(int size)
		{
			TypeIDOfType_Viewport[] array = new TypeIDOfType_Viewport[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Viewport();
			}
			return array;
		}

		public static TypeIDOfType_Viewport[][] InstArrayTypeIDOfType_Viewport(int size1, int size2)
		{
			TypeIDOfType_Viewport[][] array = new TypeIDOfType_Viewport[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Viewport[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Viewport();
				}
			}
			return array;
		}

		public static TypeIDOfType_Viewport[][][] InstArrayTypeIDOfType_Viewport(int size1, int size2, int size3)
		{
			TypeIDOfType_Viewport[][][] array = new TypeIDOfType_Viewport[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Viewport[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Viewport[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Viewport();
					}
				}
			}
			return array;
		}
	}
}
