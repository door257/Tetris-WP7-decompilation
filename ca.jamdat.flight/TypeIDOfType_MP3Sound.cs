namespace ca.jamdat.flight
{
	public class TypeIDOfType_MP3Sound
	{
		public const sbyte v = 64;

		public const sbyte w = 64;

		public static TypeIDOfType_MP3Sound[] InstArrayTypeIDOfType_MP3Sound(int size)
		{
			TypeIDOfType_MP3Sound[] array = new TypeIDOfType_MP3Sound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_MP3Sound();
			}
			return array;
		}

		public static TypeIDOfType_MP3Sound[][] InstArrayTypeIDOfType_MP3Sound(int size1, int size2)
		{
			TypeIDOfType_MP3Sound[][] array = new TypeIDOfType_MP3Sound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MP3Sound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MP3Sound();
				}
			}
			return array;
		}

		public static TypeIDOfType_MP3Sound[][][] InstArrayTypeIDOfType_MP3Sound(int size1, int size2, int size3)
		{
			TypeIDOfType_MP3Sound[][][] array = new TypeIDOfType_MP3Sound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MP3Sound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MP3Sound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_MP3Sound();
					}
				}
			}
			return array;
		}
	}
}
