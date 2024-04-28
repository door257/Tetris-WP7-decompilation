namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_TimeControlled
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_PtrArray_TimeControlled[] InstArrayTypeIDOfType_PtrArray_TimeControlled(int size)
		{
			TypeIDOfType_PtrArray_TimeControlled[] array = new TypeIDOfType_PtrArray_TimeControlled[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_TimeControlled();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_TimeControlled[][] InstArrayTypeIDOfType_PtrArray_TimeControlled(int size1, int size2)
		{
			TypeIDOfType_PtrArray_TimeControlled[][] array = new TypeIDOfType_PtrArray_TimeControlled[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_TimeControlled[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_TimeControlled();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_TimeControlled[][][] InstArrayTypeIDOfType_PtrArray_TimeControlled(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_TimeControlled[][][] array = new TypeIDOfType_PtrArray_TimeControlled[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_TimeControlled[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_TimeControlled[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_TimeControlled();
					}
				}
			}
			return array;
		}
	}
}
