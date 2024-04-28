namespace ca.jamdat.flight
{
	public class TypeIDOfType_Selection
	{
		public const sbyte v = 97;

		public const sbyte w = 97;

		public static TypeIDOfType_Selection[] InstArrayTypeIDOfType_Selection(int size)
		{
			TypeIDOfType_Selection[] array = new TypeIDOfType_Selection[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Selection();
			}
			return array;
		}

		public static TypeIDOfType_Selection[][] InstArrayTypeIDOfType_Selection(int size1, int size2)
		{
			TypeIDOfType_Selection[][] array = new TypeIDOfType_Selection[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Selection[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Selection();
				}
			}
			return array;
		}

		public static TypeIDOfType_Selection[][][] InstArrayTypeIDOfType_Selection(int size1, int size2, int size3)
		{
			TypeIDOfType_Selection[][][] array = new TypeIDOfType_Selection[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Selection[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Selection[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Selection();
					}
				}
			}
			return array;
		}
	}
}
