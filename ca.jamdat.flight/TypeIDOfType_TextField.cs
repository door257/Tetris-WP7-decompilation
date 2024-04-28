namespace ca.jamdat.flight
{
	public class TypeIDOfType_TextField
	{
		public const sbyte v = 99;

		public const sbyte w = 99;

		public static TypeIDOfType_TextField[] InstArrayTypeIDOfType_TextField(int size)
		{
			TypeIDOfType_TextField[] array = new TypeIDOfType_TextField[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_TextField();
			}
			return array;
		}

		public static TypeIDOfType_TextField[][] InstArrayTypeIDOfType_TextField(int size1, int size2)
		{
			TypeIDOfType_TextField[][] array = new TypeIDOfType_TextField[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TextField[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TextField();
				}
			}
			return array;
		}

		public static TypeIDOfType_TextField[][][] InstArrayTypeIDOfType_TextField(int size1, int size2, int size3)
		{
			TypeIDOfType_TextField[][][] array = new TypeIDOfType_TextField[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TextField[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TextField[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_TextField();
					}
				}
			}
			return array;
		}
	}
}
