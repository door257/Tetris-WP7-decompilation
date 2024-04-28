namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlSkeleton
	{
		public const sbyte v = 109;

		public const sbyte w = 109;

		public static TypeIDOfType_FlSkeleton[] InstArrayTypeIDOfType_FlSkeleton(int size)
		{
			TypeIDOfType_FlSkeleton[] array = new TypeIDOfType_FlSkeleton[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlSkeleton();
			}
			return array;
		}

		public static TypeIDOfType_FlSkeleton[][] InstArrayTypeIDOfType_FlSkeleton(int size1, int size2)
		{
			TypeIDOfType_FlSkeleton[][] array = new TypeIDOfType_FlSkeleton[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSkeleton[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSkeleton();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlSkeleton[][][] InstArrayTypeIDOfType_FlSkeleton(int size1, int size2, int size3)
		{
			TypeIDOfType_FlSkeleton[][][] array = new TypeIDOfType_FlSkeleton[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSkeleton[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSkeleton[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlSkeleton();
					}
				}
			}
			return array;
		}
	}
}
