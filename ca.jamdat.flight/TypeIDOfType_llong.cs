namespace ca.jamdat.flight
{
	public class TypeIDOfType_llong
	{
		public const sbyte v = 13;

		public const sbyte w = 13;

		public static TypeIDOfType_llong[] InstArrayTypeIDOfType_llong(int size)
		{
			TypeIDOfType_llong[] array = new TypeIDOfType_llong[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_llong();
			}
			return array;
		}

		public static TypeIDOfType_llong[][] InstArrayTypeIDOfType_llong(int size1, int size2)
		{
			TypeIDOfType_llong[][] array = new TypeIDOfType_llong[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_llong[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_llong();
				}
			}
			return array;
		}

		public static TypeIDOfType_llong[][][] InstArrayTypeIDOfType_llong(int size1, int size2, int size3)
		{
			TypeIDOfType_llong[][][] array = new TypeIDOfType_llong[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_llong[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_llong[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_llong();
					}
				}
			}
			return array;
		}
	}
}
