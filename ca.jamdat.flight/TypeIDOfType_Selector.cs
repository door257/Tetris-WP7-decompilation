namespace ca.jamdat.flight
{
	public class TypeIDOfType_Selector
	{
		public const sbyte v = 96;

		public const sbyte w = 96;

		public static TypeIDOfType_Selector[] InstArrayTypeIDOfType_Selector(int size)
		{
			TypeIDOfType_Selector[] array = new TypeIDOfType_Selector[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Selector();
			}
			return array;
		}

		public static TypeIDOfType_Selector[][] InstArrayTypeIDOfType_Selector(int size1, int size2)
		{
			TypeIDOfType_Selector[][] array = new TypeIDOfType_Selector[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Selector[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Selector();
				}
			}
			return array;
		}

		public static TypeIDOfType_Selector[][][] InstArrayTypeIDOfType_Selector(int size1, int size2, int size3)
		{
			TypeIDOfType_Selector[][][] array = new TypeIDOfType_Selector[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Selector[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Selector[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Selector();
					}
				}
			}
			return array;
		}
	}
}
