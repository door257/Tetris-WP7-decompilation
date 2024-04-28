namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlBitmapImplementor
	{
		public const sbyte v = 91;

		public const sbyte w = 91;

		public static TypeIDOfType_FlBitmapImplementor[] InstArrayTypeIDOfType_FlBitmapImplementor(int size)
		{
			TypeIDOfType_FlBitmapImplementor[] array = new TypeIDOfType_FlBitmapImplementor[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapImplementor();
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapImplementor[][] InstArrayTypeIDOfType_FlBitmapImplementor(int size1, int size2)
		{
			TypeIDOfType_FlBitmapImplementor[][] array = new TypeIDOfType_FlBitmapImplementor[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapImplementor[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapImplementor();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlBitmapImplementor[][][] InstArrayTypeIDOfType_FlBitmapImplementor(int size1, int size2, int size3)
		{
			TypeIDOfType_FlBitmapImplementor[][][] array = new TypeIDOfType_FlBitmapImplementor[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlBitmapImplementor[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlBitmapImplementor[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlBitmapImplementor();
					}
				}
			}
			return array;
		}
	}
}
