namespace ca.jamdat.flight
{
	public class TypeIDOfType_TimeSystem
	{
		public const sbyte v = 85;

		public const sbyte w = 85;

		public static TypeIDOfType_TimeSystem[] InstArrayTypeIDOfType_TimeSystem(int size)
		{
			TypeIDOfType_TimeSystem[] array = new TypeIDOfType_TimeSystem[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_TimeSystem();
			}
			return array;
		}

		public static TypeIDOfType_TimeSystem[][] InstArrayTypeIDOfType_TimeSystem(int size1, int size2)
		{
			TypeIDOfType_TimeSystem[][] array = new TypeIDOfType_TimeSystem[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TimeSystem[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TimeSystem();
				}
			}
			return array;
		}

		public static TypeIDOfType_TimeSystem[][][] InstArrayTypeIDOfType_TimeSystem(int size1, int size2, int size3)
		{
			TypeIDOfType_TimeSystem[][][] array = new TypeIDOfType_TimeSystem[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TimeSystem[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TimeSystem[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_TimeSystem();
					}
				}
			}
			return array;
		}
	}
}
