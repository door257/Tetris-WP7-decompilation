namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_FlNodeAnimation
	{
		public const sbyte v = 108;

		public const sbyte w = 108;

		public static TypeIDOfType_PtrArray_FlNodeAnimation[] InstArrayTypeIDOfType_PtrArray_FlNodeAnimation(int size)
		{
			TypeIDOfType_PtrArray_FlNodeAnimation[] array = new TypeIDOfType_PtrArray_FlNodeAnimation[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlNodeAnimation();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlNodeAnimation[][] InstArrayTypeIDOfType_PtrArray_FlNodeAnimation(int size1, int size2)
		{
			TypeIDOfType_PtrArray_FlNodeAnimation[][] array = new TypeIDOfType_PtrArray_FlNodeAnimation[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlNodeAnimation[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlNodeAnimation();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_FlNodeAnimation[][][] InstArrayTypeIDOfType_PtrArray_FlNodeAnimation(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_FlNodeAnimation[][][] array = new TypeIDOfType_PtrArray_FlNodeAnimation[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_FlNodeAnimation[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_FlNodeAnimation[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_FlNodeAnimation();
					}
				}
			}
			return array;
		}
	}
}
