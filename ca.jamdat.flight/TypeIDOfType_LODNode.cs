namespace ca.jamdat.flight
{
	public class TypeIDOfType_LODNode
	{
		public const sbyte v = 24;

		public const sbyte w = 24;

		public static TypeIDOfType_LODNode[] InstArrayTypeIDOfType_LODNode(int size)
		{
			TypeIDOfType_LODNode[] array = new TypeIDOfType_LODNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_LODNode();
			}
			return array;
		}

		public static TypeIDOfType_LODNode[][] InstArrayTypeIDOfType_LODNode(int size1, int size2)
		{
			TypeIDOfType_LODNode[][] array = new TypeIDOfType_LODNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_LODNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_LODNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_LODNode[][][] InstArrayTypeIDOfType_LODNode(int size1, int size2, int size3)
		{
			TypeIDOfType_LODNode[][][] array = new TypeIDOfType_LODNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_LODNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_LODNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_LODNode();
					}
				}
			}
			return array;
		}
	}
}
