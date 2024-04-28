namespace ca.jamdat.flight
{
	public class TypeIDOfType_CompositingMode
	{
		public const sbyte v = 49;

		public const sbyte w = 49;

		public static TypeIDOfType_CompositingMode[] InstArrayTypeIDOfType_CompositingMode(int size)
		{
			TypeIDOfType_CompositingMode[] array = new TypeIDOfType_CompositingMode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_CompositingMode();
			}
			return array;
		}

		public static TypeIDOfType_CompositingMode[][] InstArrayTypeIDOfType_CompositingMode(int size1, int size2)
		{
			TypeIDOfType_CompositingMode[][] array = new TypeIDOfType_CompositingMode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_CompositingMode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_CompositingMode();
				}
			}
			return array;
		}

		public static TypeIDOfType_CompositingMode[][][] InstArrayTypeIDOfType_CompositingMode(int size1, int size2, int size3)
		{
			TypeIDOfType_CompositingMode[][][] array = new TypeIDOfType_CompositingMode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_CompositingMode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_CompositingMode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_CompositingMode();
					}
				}
			}
			return array;
		}
	}
}
