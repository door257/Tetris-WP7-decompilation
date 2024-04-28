namespace ca.jamdat.flight
{
	public class TypeIDOfType_LOD
	{
		public const sbyte v = 110;

		public const sbyte w = 110;

		public static TypeIDOfType_LOD[] InstArrayTypeIDOfType_LOD(int size)
		{
			TypeIDOfType_LOD[] array = new TypeIDOfType_LOD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_LOD();
			}
			return array;
		}

		public static TypeIDOfType_LOD[][] InstArrayTypeIDOfType_LOD(int size1, int size2)
		{
			TypeIDOfType_LOD[][] array = new TypeIDOfType_LOD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_LOD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_LOD();
				}
			}
			return array;
		}

		public static TypeIDOfType_LOD[][][] InstArrayTypeIDOfType_LOD(int size1, int size2, int size3)
		{
			TypeIDOfType_LOD[][][] array = new TypeIDOfType_LOD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_LOD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_LOD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_LOD();
					}
				}
			}
			return array;
		}
	}
}
