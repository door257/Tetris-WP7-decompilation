namespace ca.jamdat.flight
{
	public class TypeIDOfType_Appearance
	{
		public const sbyte v = 44;

		public const sbyte w = 44;

		public static TypeIDOfType_Appearance[] InstArrayTypeIDOfType_Appearance(int size)
		{
			TypeIDOfType_Appearance[] array = new TypeIDOfType_Appearance[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Appearance();
			}
			return array;
		}

		public static TypeIDOfType_Appearance[][] InstArrayTypeIDOfType_Appearance(int size1, int size2)
		{
			TypeIDOfType_Appearance[][] array = new TypeIDOfType_Appearance[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Appearance[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Appearance();
				}
			}
			return array;
		}

		public static TypeIDOfType_Appearance[][][] InstArrayTypeIDOfType_Appearance(int size1, int size2, int size3)
		{
			TypeIDOfType_Appearance[][][] array = new TypeIDOfType_Appearance[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Appearance[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Appearance[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Appearance();
					}
				}
			}
			return array;
		}
	}
}
