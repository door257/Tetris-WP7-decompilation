namespace ca.jamdat.tetrisrevolution
{
	public class MusicMode
	{
		public const int modeOff = 0;

		public const int modeOn = 1;

		public const int modeAuto = 2;

		public static MusicMode[] InstArrayMusicMode(int size)
		{
			MusicMode[] array = new MusicMode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new MusicMode();
			}
			return array;
		}

		public static MusicMode[][] InstArrayMusicMode(int size1, int size2)
		{
			MusicMode[][] array = new MusicMode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MusicMode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MusicMode();
				}
			}
			return array;
		}

		public static MusicMode[][][] InstArrayMusicMode(int size1, int size2, int size3)
		{
			MusicMode[][][] array = new MusicMode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MusicMode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MusicMode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new MusicMode();
					}
				}
			}
			return array;
		}
	}
}
