namespace ca.jamdat.flight
{
	public class TypeIDOfType_SkinnedMeshNode
	{
		public const sbyte v = 100;

		public const sbyte w = 100;

		public static TypeIDOfType_SkinnedMeshNode[] InstArrayTypeIDOfType_SkinnedMeshNode(int size)
		{
			TypeIDOfType_SkinnedMeshNode[] array = new TypeIDOfType_SkinnedMeshNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_SkinnedMeshNode();
			}
			return array;
		}

		public static TypeIDOfType_SkinnedMeshNode[][] InstArrayTypeIDOfType_SkinnedMeshNode(int size1, int size2)
		{
			TypeIDOfType_SkinnedMeshNode[][] array = new TypeIDOfType_SkinnedMeshNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SkinnedMeshNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SkinnedMeshNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_SkinnedMeshNode[][][] InstArrayTypeIDOfType_SkinnedMeshNode(int size1, int size2, int size3)
		{
			TypeIDOfType_SkinnedMeshNode[][][] array = new TypeIDOfType_SkinnedMeshNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SkinnedMeshNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SkinnedMeshNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_SkinnedMeshNode();
					}
				}
			}
			return array;
		}
	}
}
