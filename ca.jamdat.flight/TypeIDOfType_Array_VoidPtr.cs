namespace ca.jamdat.flight
{
	public class TypeIDOfType_Array_VoidPtr
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_Array_VoidPtr[] InstArrayTypeIDOfType_Array_VoidPtr(int size)
		{
			TypeIDOfType_Array_VoidPtr[] array = new TypeIDOfType_Array_VoidPtr[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Array_VoidPtr();
			}
			return array;
		}

		public static TypeIDOfType_Array_VoidPtr[][] InstArrayTypeIDOfType_Array_VoidPtr(int size1, int size2)
		{
			TypeIDOfType_Array_VoidPtr[][] array = new TypeIDOfType_Array_VoidPtr[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_VoidPtr[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_VoidPtr();
				}
			}
			return array;
		}

		public static TypeIDOfType_Array_VoidPtr[][][] InstArrayTypeIDOfType_Array_VoidPtr(int size1, int size2, int size3)
		{
			TypeIDOfType_Array_VoidPtr[][][] array = new TypeIDOfType_Array_VoidPtr[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Array_VoidPtr[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Array_VoidPtr[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Array_VoidPtr();
					}
				}
			}
			return array;
		}
	}
}
