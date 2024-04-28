namespace ca.jamdat.tetrisrevolution
{
	public class GameFactory
	{
		public TetrisGame mGame;

		public static TetrisGame GetTetrisGame()
		{
			return GameApp.Get().GetGameFactory().mGame;
		}

		public virtual void destruct()
		{
			ReleaseGame();
		}

		public virtual TetrisGame CreateNewGame()
		{
			ReleaseGame();
			mGame = CreateGame();
			return mGame;
		}

		public virtual void ReleaseGame()
		{
			if (mGame != null)
			{
				mGame.ReleaseGame();
				mGame = null;
			}
		}

		public static GameFactory Get()
		{
			return GameApp.Get().GetGameFactory();
		}

		public virtual TetrisGame CreateGame()
		{
			TetrisGame result = null;
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			int gameVariant = gameSettings.GetGameVariant();
			int gameDifficulty = gameSettings.GetGameDifficulty();
			GameParameter gameParameter = new GameParameter();
			gameParameter.SetDifficulty(gameDifficulty);
			gameParameter.SetLineLimit(GameApp.Get().GetGameSettings().GetLineLimit());
			switch (gameVariant)
			{
			case 2:
				result = new VariantVanilla(gameParameter);
				break;
			case 3:
				result = new VariantFlood(gameParameter);
				break;
			case 4:
				result = new VariantLedges(gameParameter);
				break;
			case 5:
				result = new VariantLimbo(gameParameter);
				break;
			case 6:
				result = new VariantMagnetic(gameParameter);
				break;
			case 7:
				result = new VariantScanner(gameParameter);
				break;
			case 8:
			{
				VariantSplit variantSplit = new VariantSplit(gameParameter);
				result = variantSplit;
				break;
			}
			case 9:
				result = new VariantChill(gameParameter);
				break;
			case 10:
				result = new VariantFlashlite(gameParameter);
				break;
			case 1:
				result = new VariantTreadmill(gameParameter);
				break;
			case 11:
			{
				VariantMaster variantMaster = new VariantMaster(gameParameter);
				result = variantMaster;
				break;
			}
			case 0:
			{
				VariantBasic variantBasic = new VariantBasic(gameParameter);
				result = variantBasic;
				break;
			}
			}
			return result;
		}

		public static GameFactory[] InstArrayGameFactory(int size)
		{
			GameFactory[] array = new GameFactory[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameFactory();
			}
			return array;
		}

		public static GameFactory[][] InstArrayGameFactory(int size1, int size2)
		{
			GameFactory[][] array = new GameFactory[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameFactory[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameFactory();
				}
			}
			return array;
		}

		public static GameFactory[][][] InstArrayGameFactory(int size1, int size2, int size3)
		{
			GameFactory[][][] array = new GameFactory[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameFactory[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameFactory[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameFactory();
					}
				}
			}
			return array;
		}
	}
}
