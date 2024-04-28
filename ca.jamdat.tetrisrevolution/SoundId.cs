namespace ca.jamdat.tetrisrevolution
{
	public class SoundId
	{
		public const int idNone = -1;

		public const int firstMenuSound = 0;

		public const int idMenuLoop = 0;

		public const int idSoundOption = 1;

		public const int idGameTypeA = 2;

		public const int idGameTypeB = 3;

		public const int idGameTypeC = 4;

		public const int idCount = 5;

		public const int firstGameSound = 1;

		public static int GetPackageEntryPointIndex(int soundId)
		{
			switch (soundId)
			{
			case 0:
				return 0;
			case 1:
				return 2;
			case 2:
				return 1;
			case 3:
				return 3;
			case 4:
				return 0;
			default:
				return -1;
			}
		}

		public static SoundId[] InstArraySoundId(int size)
		{
			SoundId[] array = new SoundId[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SoundId();
			}
			return array;
		}

		public static SoundId[][] InstArraySoundId(int size1, int size2)
		{
			SoundId[][] array = new SoundId[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundId[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundId();
				}
			}
			return array;
		}

		public static SoundId[][][] InstArraySoundId(int size1, int size2, int size3)
		{
			SoundId[][][] array = new SoundId[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundId[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundId[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SoundId();
					}
				}
			}
			return array;
		}
	}
}
