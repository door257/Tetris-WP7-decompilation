namespace ca.jamdat.flight
{
	public class TypeIDOfType_ScalingNode
	{
		public const sbyte v = 55;

		public const sbyte w = 55;

		public static TypeIDOfType_ScalingNode[] InstArrayTypeIDOfType_ScalingNode(int size)
		{
			TypeIDOfType_ScalingNode[] array = new TypeIDOfType_ScalingNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_ScalingNode();
			}
			return array;
		}

		public static TypeIDOfType_ScalingNode[][] InstArrayTypeIDOfType_ScalingNode(int size1, int size2)
		{
			TypeIDOfType_ScalingNode[][] array = new TypeIDOfType_ScalingNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ScalingNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ScalingNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_ScalingNode[][][] InstArrayTypeIDOfType_ScalingNode(int size1, int size2, int size3)
		{
			TypeIDOfType_ScalingNode[][][] array = new TypeIDOfType_ScalingNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ScalingNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ScalingNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_ScalingNode();
					}
				}
			}
			return array;
		}
	}
}
