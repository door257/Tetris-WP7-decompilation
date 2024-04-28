namespace ca.jamdat.flight
{
	public class TypeIDOfType_Viewport3D
	{
		public const sbyte v = 69;

		public const sbyte w = 69;

		public static TypeIDOfType_Viewport3D[] InstArrayTypeIDOfType_Viewport3D(int size)
		{
			TypeIDOfType_Viewport3D[] array = new TypeIDOfType_Viewport3D[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Viewport3D();
			}
			return array;
		}

		public static TypeIDOfType_Viewport3D[][] InstArrayTypeIDOfType_Viewport3D(int size1, int size2)
		{
			TypeIDOfType_Viewport3D[][] array = new TypeIDOfType_Viewport3D[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Viewport3D[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Viewport3D();
				}
			}
			return array;
		}

		public static TypeIDOfType_Viewport3D[][][] InstArrayTypeIDOfType_Viewport3D(int size1, int size2, int size3)
		{
			TypeIDOfType_Viewport3D[][][] array = new TypeIDOfType_Viewport3D[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Viewport3D[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Viewport3D[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Viewport3D();
					}
				}
			}
			return array;
		}
	}
}
