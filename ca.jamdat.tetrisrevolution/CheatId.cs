namespace ca.jamdat.tetrisrevolution
{
	public class CheatId
	{
		public const int idLanguage = 0;

		public const int idSceneSlideShow = 1;

		public const int idAccelerometerMonitor = 2;

		public const int idFpsMonitor = 3;

		public const int idMemoryMonitor = 4;

		public const int idWriteFileDurationMonitor = 5;

		public const int idWriteFile = 6;

		public const int idCommand = 7;

		public const int idRandomKeyPress = 8;

		public const int idMoreGames15ImplConfig = 9;

		public const int idMoreGames15BuyConfig = 10;

		public const int idMoreGames15CatConfig = 11;

		public const int idMoreGames15ConfiguredProductCountConfig = 12;

		public const int idMoreGames15SwapConfiguredProducts = 13;

		public const int idMoreGames16ImplConfig = 14;

		public const int idMoreGames16BuyConfig = 15;

		public const int idMoreGames16CatConfig = 16;

		public const int idMoreGames16ConfiguredProductCountConfig = 17;

		public const int idMoreGames16SwapConfiguredProducts = 18;

		public const int idUnlockAllVariants = 19;

		public const int idMaxAllFeats = 20;

		public const int idGiveAdvFeats = 21;

		public const int idWinCurrentGame = 22;

		public const int idOnlyITetrimino = 23;

		public const int idOnlyOTetrimino = 24;

		public const int idCycleTip = 25;

		public const int idCount = 26;

		public static CheatId[] InstArrayCheatId(int size)
		{
			CheatId[] array = new CheatId[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CheatId();
			}
			return array;
		}

		public static CheatId[][] InstArrayCheatId(int size1, int size2)
		{
			CheatId[][] array = new CheatId[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatId[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatId();
				}
			}
			return array;
		}

		public static CheatId[][][] InstArrayCheatId(int size1, int size2, int size3)
		{
			CheatId[][][] array = new CheatId[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatId[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatId[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CheatId();
					}
				}
			}
			return array;
		}
	}
}
