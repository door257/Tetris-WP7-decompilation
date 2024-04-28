namespace ca.jamdat.flight
{
	public class TypeIDOfType_Node
	{
		public const sbyte v = 22;

		public const sbyte w = 22;

		public static TypeIDOfType_Node[] InstArrayTypeIDOfType_Node(int size)
		{
			TypeIDOfType_Node[] array = new TypeIDOfType_Node[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Node();
			}
			return array;
		}

		public static TypeIDOfType_Node[][] InstArrayTypeIDOfType_Node(int size1, int size2)
		{
			TypeIDOfType_Node[][] array = new TypeIDOfType_Node[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Node[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Node();
				}
			}
			return array;
		}

		public static TypeIDOfType_Node[][][] InstArrayTypeIDOfType_Node(int size1, int size2, int size3)
		{
			TypeIDOfType_Node[][][] array = new TypeIDOfType_Node[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Node[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Node[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Node();
					}
				}
			}
			return array;
		}
	}
}
