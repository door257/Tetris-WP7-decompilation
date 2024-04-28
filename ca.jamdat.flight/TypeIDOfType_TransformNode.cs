namespace ca.jamdat.flight
{
	public class TypeIDOfType_TransformNode
	{
		public const sbyte v = 54;

		public const sbyte w = 54;

		public static TypeIDOfType_TransformNode[] InstArrayTypeIDOfType_TransformNode(int size)
		{
			TypeIDOfType_TransformNode[] array = new TypeIDOfType_TransformNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_TransformNode();
			}
			return array;
		}

		public static TypeIDOfType_TransformNode[][] InstArrayTypeIDOfType_TransformNode(int size1, int size2)
		{
			TypeIDOfType_TransformNode[][] array = new TypeIDOfType_TransformNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TransformNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TransformNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_TransformNode[][][] InstArrayTypeIDOfType_TransformNode(int size1, int size2, int size3)
		{
			TypeIDOfType_TransformNode[][][] array = new TypeIDOfType_TransformNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TransformNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TransformNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_TransformNode();
					}
				}
			}
			return array;
		}
	}
}
