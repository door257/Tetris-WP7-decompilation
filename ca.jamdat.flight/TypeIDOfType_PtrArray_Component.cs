namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_Component
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_PtrArray_Component[] InstArrayTypeIDOfType_PtrArray_Component(int size)
		{
			TypeIDOfType_PtrArray_Component[] array = new TypeIDOfType_PtrArray_Component[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_Component();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_Component[][] InstArrayTypeIDOfType_PtrArray_Component(int size1, int size2)
		{
			TypeIDOfType_PtrArray_Component[][] array = new TypeIDOfType_PtrArray_Component[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_Component[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_Component();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_Component[][][] InstArrayTypeIDOfType_PtrArray_Component(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_Component[][][] array = new TypeIDOfType_PtrArray_Component[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_Component[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_Component[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_Component();
					}
				}
			}
			return array;
		}
	}
}
