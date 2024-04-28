namespace ca.jamdat.flight
{
	public class TypeIDOfType_Node2D
	{
		public const sbyte v = 82;

		public const sbyte w = 82;

		public static TypeIDOfType_Node2D[] InstArrayTypeIDOfType_Node2D(int size)
		{
			TypeIDOfType_Node2D[] array = new TypeIDOfType_Node2D[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Node2D();
			}
			return array;
		}

		public static TypeIDOfType_Node2D[][] InstArrayTypeIDOfType_Node2D(int size1, int size2)
		{
			TypeIDOfType_Node2D[][] array = new TypeIDOfType_Node2D[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Node2D[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Node2D();
				}
			}
			return array;
		}

		public static TypeIDOfType_Node2D[][][] InstArrayTypeIDOfType_Node2D(int size1, int size2, int size3)
		{
			TypeIDOfType_Node2D[][][] array = new TypeIDOfType_Node2D[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Node2D[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Node2D[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Node2D();
					}
				}
			}
			return array;
		}
	}
}
