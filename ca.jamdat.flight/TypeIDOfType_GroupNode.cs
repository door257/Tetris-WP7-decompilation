namespace ca.jamdat.flight
{
	public class TypeIDOfType_GroupNode
	{
		public const sbyte v = 23;

		public const sbyte w = 23;

		public static TypeIDOfType_GroupNode[] InstArrayTypeIDOfType_GroupNode(int size)
		{
			TypeIDOfType_GroupNode[] array = new TypeIDOfType_GroupNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_GroupNode();
			}
			return array;
		}

		public static TypeIDOfType_GroupNode[][] InstArrayTypeIDOfType_GroupNode(int size1, int size2)
		{
			TypeIDOfType_GroupNode[][] array = new TypeIDOfType_GroupNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_GroupNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_GroupNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_GroupNode[][][] InstArrayTypeIDOfType_GroupNode(int size1, int size2, int size3)
		{
			TypeIDOfType_GroupNode[][][] array = new TypeIDOfType_GroupNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_GroupNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_GroupNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_GroupNode();
					}
				}
			}
			return array;
		}
	}
}
