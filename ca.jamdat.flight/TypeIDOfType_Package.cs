namespace ca.jamdat.flight
{
	public class TypeIDOfType_Package
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_Package[] InstArrayTypeIDOfType_Package(int size)
		{
			TypeIDOfType_Package[] array = new TypeIDOfType_Package[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Package();
			}
			return array;
		}

		public static TypeIDOfType_Package[][] InstArrayTypeIDOfType_Package(int size1, int size2)
		{
			TypeIDOfType_Package[][] array = new TypeIDOfType_Package[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Package[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Package();
				}
			}
			return array;
		}

		public static TypeIDOfType_Package[][][] InstArrayTypeIDOfType_Package(int size1, int size2, int size3)
		{
			TypeIDOfType_Package[][][] array = new TypeIDOfType_Package[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Package[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Package[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Package();
					}
				}
			}
			return array;
		}
	}
}
