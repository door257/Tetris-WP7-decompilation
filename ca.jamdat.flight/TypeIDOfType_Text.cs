namespace ca.jamdat.flight
{
	public class TypeIDOfType_Text
	{
		public const sbyte v = 71;

		public const sbyte w = 71;

		public static TypeIDOfType_Text[] InstArrayTypeIDOfType_Text(int size)
		{
			TypeIDOfType_Text[] array = new TypeIDOfType_Text[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Text();
			}
			return array;
		}

		public static TypeIDOfType_Text[][] InstArrayTypeIDOfType_Text(int size1, int size2)
		{
			TypeIDOfType_Text[][] array = new TypeIDOfType_Text[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Text[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Text();
				}
			}
			return array;
		}

		public static TypeIDOfType_Text[][][] InstArrayTypeIDOfType_Text(int size1, int size2, int size3)
		{
			TypeIDOfType_Text[][][] array = new TypeIDOfType_Text[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Text[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Text[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Text();
					}
				}
			}
			return array;
		}
	}
}
