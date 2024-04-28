namespace ca.jamdat.flight
{
	public class TypeIDOfType_Line
	{
		public const sbyte v = 74;

		public const sbyte w = 74;

		public static TypeIDOfType_Line[] InstArrayTypeIDOfType_Line(int size)
		{
			TypeIDOfType_Line[] array = new TypeIDOfType_Line[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Line();
			}
			return array;
		}

		public static TypeIDOfType_Line[][] InstArrayTypeIDOfType_Line(int size1, int size2)
		{
			TypeIDOfType_Line[][] array = new TypeIDOfType_Line[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Line[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Line();
				}
			}
			return array;
		}

		public static TypeIDOfType_Line[][][] InstArrayTypeIDOfType_Line(int size1, int size2, int size3)
		{
			TypeIDOfType_Line[][][] array = new TypeIDOfType_Line[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Line[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Line[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Line();
					}
				}
			}
			return array;
		}
	}
}
