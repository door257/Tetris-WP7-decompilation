namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_FlToolWindow
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_PtrArray_FlToolWindow[] InstArrayTypeIDOfType_PtrArray_FlToolWindow(int size)
		{
			TypeIDOfType_PtrArray_FlToolWindow[] array = new TypeIDOfType_PtrArray_FlToolWindow[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlToolWindow();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlToolWindow[][] InstArrayTypeIDOfType_PtrArray_FlToolWindow(int size1, int size2)
		{
			TypeIDOfType_PtrArray_FlToolWindow[][] array = new TypeIDOfType_PtrArray_FlToolWindow[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlToolWindow[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlToolWindow();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlToolWindow[][][] InstArrayTypeIDOfType_PtrArray_FlToolWindow(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_FlToolWindow[][][] array = new TypeIDOfType_PtrArray_FlToolWindow[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlToolWindow[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlToolWindow[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_FlToolWindow();
					}
				}
			}
			return array;
		}
	}
}
