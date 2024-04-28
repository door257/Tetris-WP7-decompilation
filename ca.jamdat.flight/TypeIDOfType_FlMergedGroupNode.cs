namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlMergedGroupNode
	{
		public const sbyte v = 114;

		public const sbyte w = 114;

		public static TypeIDOfType_FlMergedGroupNode[] InstArrayTypeIDOfType_FlMergedGroupNode(int size)
		{
			TypeIDOfType_FlMergedGroupNode[] array = new TypeIDOfType_FlMergedGroupNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlMergedGroupNode();
			}
			return array;
		}

		public static TypeIDOfType_FlMergedGroupNode[][] InstArrayTypeIDOfType_FlMergedGroupNode(int size1, int size2)
		{
			TypeIDOfType_FlMergedGroupNode[][] array = new TypeIDOfType_FlMergedGroupNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlMergedGroupNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlMergedGroupNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlMergedGroupNode[][][] InstArrayTypeIDOfType_FlMergedGroupNode(int size1, int size2, int size3)
		{
			TypeIDOfType_FlMergedGroupNode[][][] array = new TypeIDOfType_FlMergedGroupNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlMergedGroupNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlMergedGroupNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlMergedGroupNode();
					}
				}
			}
			return array;
		}
	}
}
