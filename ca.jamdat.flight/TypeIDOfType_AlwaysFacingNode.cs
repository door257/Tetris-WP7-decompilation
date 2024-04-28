namespace ca.jamdat.flight
{
	public class TypeIDOfType_AlwaysFacingNode
	{
		public const sbyte v = 77;

		public const sbyte w = 77;

		public static TypeIDOfType_AlwaysFacingNode[] InstArrayTypeIDOfType_AlwaysFacingNode(int size)
		{
			TypeIDOfType_AlwaysFacingNode[] array = new TypeIDOfType_AlwaysFacingNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_AlwaysFacingNode();
			}
			return array;
		}

		public static TypeIDOfType_AlwaysFacingNode[][] InstArrayTypeIDOfType_AlwaysFacingNode(int size1, int size2)
		{
			TypeIDOfType_AlwaysFacingNode[][] array = new TypeIDOfType_AlwaysFacingNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AlwaysFacingNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AlwaysFacingNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_AlwaysFacingNode[][][] InstArrayTypeIDOfType_AlwaysFacingNode(int size1, int size2, int size3)
		{
			TypeIDOfType_AlwaysFacingNode[][][] array = new TypeIDOfType_AlwaysFacingNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AlwaysFacingNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AlwaysFacingNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_AlwaysFacingNode();
					}
				}
			}
			return array;
		}
	}
}
