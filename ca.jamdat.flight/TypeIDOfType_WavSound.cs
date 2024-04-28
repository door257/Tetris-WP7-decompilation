namespace ca.jamdat.flight
{
	public class TypeIDOfType_WavSound
	{
		public const sbyte v = 57;

		public const sbyte w = 57;

		public static TypeIDOfType_WavSound[] InstArrayTypeIDOfType_WavSound(int size)
		{
			TypeIDOfType_WavSound[] array = new TypeIDOfType_WavSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_WavSound();
			}
			return array;
		}

		public static TypeIDOfType_WavSound[][] InstArrayTypeIDOfType_WavSound(int size1, int size2)
		{
			TypeIDOfType_WavSound[][] array = new TypeIDOfType_WavSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_WavSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_WavSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_WavSound[][][] InstArrayTypeIDOfType_WavSound(int size1, int size2, int size3)
		{
			TypeIDOfType_WavSound[][][] array = new TypeIDOfType_WavSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_WavSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_WavSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_WavSound();
					}
				}
			}
			return array;
		}
	}
}
