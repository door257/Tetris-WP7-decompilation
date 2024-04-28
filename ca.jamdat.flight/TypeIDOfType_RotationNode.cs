namespace ca.jamdat.flight
{
	public class TypeIDOfType_RotationNode
	{
		public const sbyte v = 27;

		public const sbyte w = 27;

		public static TypeIDOfType_RotationNode[] InstArrayTypeIDOfType_RotationNode(int size)
		{
			TypeIDOfType_RotationNode[] array = new TypeIDOfType_RotationNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_RotationNode();
			}
			return array;
		}

		public static TypeIDOfType_RotationNode[][] InstArrayTypeIDOfType_RotationNode(int size1, int size2)
		{
			TypeIDOfType_RotationNode[][] array = new TypeIDOfType_RotationNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_RotationNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_RotationNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_RotationNode[][][] InstArrayTypeIDOfType_RotationNode(int size1, int size2, int size3)
		{
			TypeIDOfType_RotationNode[][][] array = new TypeIDOfType_RotationNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_RotationNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_RotationNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_RotationNode();
					}
				}
			}
			return array;
		}
	}
}
