namespace ca.jamdat.flight
{
	public class TypeIDOfType_Resource
	{
		public const sbyte v = 75;

		public const sbyte w = 75;

		public static TypeIDOfType_Resource[] InstArrayTypeIDOfType_Resource(int size)
		{
			TypeIDOfType_Resource[] array = new TypeIDOfType_Resource[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Resource();
			}
			return array;
		}

		public static TypeIDOfType_Resource[][] InstArrayTypeIDOfType_Resource(int size1, int size2)
		{
			TypeIDOfType_Resource[][] array = new TypeIDOfType_Resource[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Resource[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Resource();
				}
			}
			return array;
		}

		public static TypeIDOfType_Resource[][][] InstArrayTypeIDOfType_Resource(int size1, int size2, int size3)
		{
			TypeIDOfType_Resource[][][] array = new TypeIDOfType_Resource[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Resource[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Resource[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Resource();
					}
				}
			}
			return array;
		}
	}
}
