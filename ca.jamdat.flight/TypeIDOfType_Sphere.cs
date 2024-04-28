namespace ca.jamdat.flight
{
	public class TypeIDOfType_Sphere
	{
		public const sbyte v = 32;

		public const sbyte w = 32;

		public static TypeIDOfType_Sphere[] InstArrayTypeIDOfType_Sphere(int size)
		{
			TypeIDOfType_Sphere[] array = new TypeIDOfType_Sphere[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Sphere();
			}
			return array;
		}

		public static TypeIDOfType_Sphere[][] InstArrayTypeIDOfType_Sphere(int size1, int size2)
		{
			TypeIDOfType_Sphere[][] array = new TypeIDOfType_Sphere[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sphere[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sphere();
				}
			}
			return array;
		}

		public static TypeIDOfType_Sphere[][][] InstArrayTypeIDOfType_Sphere(int size1, int size2, int size3)
		{
			TypeIDOfType_Sphere[][][] array = new TypeIDOfType_Sphere[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sphere[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sphere[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Sphere();
					}
				}
			}
			return array;
		}
	}
}
