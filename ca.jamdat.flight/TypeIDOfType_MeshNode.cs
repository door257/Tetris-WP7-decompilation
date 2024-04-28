namespace ca.jamdat.flight
{
	public class TypeIDOfType_MeshNode
	{
		public const sbyte v = 25;

		public const sbyte w = 25;

		public static TypeIDOfType_MeshNode[] InstArrayTypeIDOfType_MeshNode(int size)
		{
			TypeIDOfType_MeshNode[] array = new TypeIDOfType_MeshNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_MeshNode();
			}
			return array;
		}

		public static TypeIDOfType_MeshNode[][] InstArrayTypeIDOfType_MeshNode(int size1, int size2)
		{
			TypeIDOfType_MeshNode[][] array = new TypeIDOfType_MeshNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MeshNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MeshNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_MeshNode[][][] InstArrayTypeIDOfType_MeshNode(int size1, int size2, int size3)
		{
			TypeIDOfType_MeshNode[][][] array = new TypeIDOfType_MeshNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MeshNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MeshNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_MeshNode();
					}
				}
			}
			return array;
		}
	}
}
