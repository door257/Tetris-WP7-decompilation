namespace ca.jamdat.flight
{
	public class TypeIDOfType_FVec2T_FlFixedPoint_int
	{
		public const sbyte v = 42;

		public const sbyte w = 42;

		public static TypeIDOfType_FVec2T_FlFixedPoint_int[] InstArrayTypeIDOfType_FVec2T_FlFixedPoint_int(int size)
		{
			TypeIDOfType_FVec2T_FlFixedPoint_int[] array = new TypeIDOfType_FVec2T_FlFixedPoint_int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FVec2T_FlFixedPoint_int();
			}
			return array;
		}

		public static TypeIDOfType_FVec2T_FlFixedPoint_int[][] InstArrayTypeIDOfType_FVec2T_FlFixedPoint_int(int size1, int size2)
		{
			TypeIDOfType_FVec2T_FlFixedPoint_int[][] array = new TypeIDOfType_FVec2T_FlFixedPoint_int[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FVec2T_FlFixedPoint_int[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FVec2T_FlFixedPoint_int();
				}
			}
			return array;
		}

		public static TypeIDOfType_FVec2T_FlFixedPoint_int[][][] InstArrayTypeIDOfType_FVec2T_FlFixedPoint_int(int size1, int size2, int size3)
		{
			TypeIDOfType_FVec2T_FlFixedPoint_int[][][] array = new TypeIDOfType_FVec2T_FlFixedPoint_int[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FVec2T_FlFixedPoint_int[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FVec2T_FlFixedPoint_int[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FVec2T_FlFixedPoint_int();
					}
				}
			}
			return array;
		}
	}
}
