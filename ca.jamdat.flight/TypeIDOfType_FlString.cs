namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlString
	{
		public const sbyte v = 35;

		public const sbyte w = 35;

		public static TypeIDOfType_FlString[] InstArrayTypeIDOfType_FlString(int size)
		{
			TypeIDOfType_FlString[] array = new TypeIDOfType_FlString[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlString();
			}
			return array;
		}

		public static TypeIDOfType_FlString[][] InstArrayTypeIDOfType_FlString(int size1, int size2)
		{
			TypeIDOfType_FlString[][] array = new TypeIDOfType_FlString[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlString[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlString();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlString[][][] InstArrayTypeIDOfType_FlString(int size1, int size2, int size3)
		{
			TypeIDOfType_FlString[][][] array = new TypeIDOfType_FlString[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlString[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlString[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlString();
					}
				}
			}
			return array;
		}
	}
}
