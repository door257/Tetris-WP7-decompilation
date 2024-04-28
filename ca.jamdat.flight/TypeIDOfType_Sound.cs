namespace ca.jamdat.flight
{
	public class TypeIDOfType_Sound
	{
		public const sbyte v = 83;

		public const sbyte w = 83;

		public static TypeIDOfType_Sound[] InstArrayTypeIDOfType_Sound(int size)
		{
			TypeIDOfType_Sound[] array = new TypeIDOfType_Sound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Sound();
			}
			return array;
		}

		public static TypeIDOfType_Sound[][] InstArrayTypeIDOfType_Sound(int size1, int size2)
		{
			TypeIDOfType_Sound[][] array = new TypeIDOfType_Sound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sound();
				}
			}
			return array;
		}

		public static TypeIDOfType_Sound[][][] InstArrayTypeIDOfType_Sound(int size1, int size2, int size3)
		{
			TypeIDOfType_Sound[][][] array = new TypeIDOfType_Sound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Sound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Sound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Sound();
					}
				}
			}
			return array;
		}
	}
}
