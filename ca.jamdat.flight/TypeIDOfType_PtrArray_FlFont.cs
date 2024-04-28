namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_FlFont
	{
		public const sbyte v = 95;

		public const sbyte w = 95;

		public static TypeIDOfType_PtrArray_FlFont[] InstArrayTypeIDOfType_PtrArray_FlFont(int size)
		{
			TypeIDOfType_PtrArray_FlFont[] array = new TypeIDOfType_PtrArray_FlFont[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlFont();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlFont[][] InstArrayTypeIDOfType_PtrArray_FlFont(int size1, int size2)
		{
			TypeIDOfType_PtrArray_FlFont[][] array = new TypeIDOfType_PtrArray_FlFont[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlFont[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlFont();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlFont[][][] InstArrayTypeIDOfType_PtrArray_FlFont(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_FlFont[][][] array = new TypeIDOfType_PtrArray_FlFont[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlFont[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlFont[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_FlFont();
					}
				}
			}
			return array;
		}
	}
}
