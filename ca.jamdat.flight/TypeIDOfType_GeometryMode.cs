namespace ca.jamdat.flight
{
	public class TypeIDOfType_GeometryMode
	{
		public const sbyte v = 46;

		public const sbyte w = 46;

		public static TypeIDOfType_GeometryMode[] InstArrayTypeIDOfType_GeometryMode(int size)
		{
			TypeIDOfType_GeometryMode[] array = new TypeIDOfType_GeometryMode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_GeometryMode();
			}
			return array;
		}

		public static TypeIDOfType_GeometryMode[][] InstArrayTypeIDOfType_GeometryMode(int size1, int size2)
		{
			TypeIDOfType_GeometryMode[][] array = new TypeIDOfType_GeometryMode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_GeometryMode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_GeometryMode();
				}
			}
			return array;
		}

		public static TypeIDOfType_GeometryMode[][][] InstArrayTypeIDOfType_GeometryMode(int size1, int size2, int size3)
		{
			TypeIDOfType_GeometryMode[][][] array = new TypeIDOfType_GeometryMode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_GeometryMode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_GeometryMode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_GeometryMode();
					}
				}
			}
			return array;
		}
	}
}
