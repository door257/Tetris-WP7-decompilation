namespace ca.jamdat.tetrisrevolution
{
	public class TetrisCheat : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			base.Activate(param);
			switch (GetId())
			{
			case 22:
				WinCurrentGame();
				Deactivate();
				break;
			case 19:
				UnlockAllVariants();
				Deactivate();
				break;
			case 20:
				MaxAllFeats();
				Deactivate();
				break;
			case 21:
				UnlockAllAdvancedFeats();
				Deactivate();
				break;
			case 23:
			case 24:
				GenerateSameTetrimino();
				Deactivate();
				break;
			case 25:
				CycleLoadingTip();
				Deactivate();
				break;
			}
		}

		public virtual void MaxAllFeats()
		{
			BaseScene currentScene = GameApp.Get().GetCommandHandler().GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 40)
			{
				CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
				careerStatistics.MaxAllCareerFeatStats();
			}
		}

		public virtual void UnlockAllAdvancedFeats()
		{
			BaseScene currentScene = GameApp.Get().GetCommandHandler().GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 40)
			{
				FeatsExpert featsExpert = FeatsExpert.Get();
				featsExpert.UnlockAdvancedFeats();
				WinCurrentGame();
			}
		}

		public virtual void UnlockAllVariants()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			featsExpert.UnlockAllGameVariants();
		}

		public virtual void WinCurrentGame()
		{
			BaseScene currentScene = GameApp.Get().GetCommandHandler().GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 40)
			{
				TetrisGame tetrisGame = GameFactory.GetTetrisGame();
				if (tetrisGame != null)
				{
					tetrisGame.SetHasCheatedToWin();
					tetrisGame.SetNextGameState(10);
				}
			}
		}

		public virtual void GenerateSameTetrimino()
		{
			BaseScene currentScene = GameApp.Get().GetCommandHandler().GetCurrentScene();
			if (currentScene == null || (currentScene.GetId() != 40 && currentScene.GetId() != 16))
			{
				return;
			}
			TetrisGame tetrisGame = GameFactory.GetTetrisGame();
			if (tetrisGame != null)
			{
				switch (GetId())
				{
				case 23:
					tetrisGame.ForceNextTetriminoI();
					break;
				case 24:
					tetrisGame.ForceNextTetriminoO();
					break;
				}
			}
		}

		public virtual void CycleLoadingTip()
		{
			BaseScene currentScene = GameApp.Get().GetCommandHandler().GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 19)
			{
				((LoadingMenu)currentScene).ForceCyclingTipFlag();
			}
		}

		public static TetrisCheat[] InstArrayTetrisCheat(int size)
		{
			TetrisCheat[] array = new TetrisCheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TetrisCheat();
			}
			return array;
		}

		public static TetrisCheat[][] InstArrayTetrisCheat(int size1, int size2)
		{
			TetrisCheat[][] array = new TetrisCheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TetrisCheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TetrisCheat();
				}
			}
			return array;
		}

		public static TetrisCheat[][][] InstArrayTetrisCheat(int size1, int size2, int size3)
		{
			TetrisCheat[][][] array = new TetrisCheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TetrisCheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TetrisCheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TetrisCheat();
					}
				}
			}
			return array;
		}
	}
}
