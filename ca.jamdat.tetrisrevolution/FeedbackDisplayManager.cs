namespace ca.jamdat.tetrisrevolution
{
	public class FeedbackDisplayManager
	{
		public LayerComponent mLayerComponent;

		public FeedbackDisplay[] mFeedbackDisplay;

		public bool mNeedToDisplayFeedback;

		public int mCurrentDisplayIdx;

		public int mDisplayCleanerIdx;

		public FeedbackDisplayManager(LayerComponent layerComponent)
		{
			mLayerComponent = layerComponent;
			mCurrentDisplayIdx = -1;
			mFeedbackDisplay = new FeedbackDisplay[5];
			for (int i = 0; i < mFeedbackDisplay.Length; i++)
			{
				mFeedbackDisplay[i] = null;
			}
		}

		public virtual void destruct()
		{
		}

		public virtual void Unload()
		{
			for (int i = 0; i < 5; i++)
			{
				if (mFeedbackDisplay[i] != null)
				{
					mFeedbackDisplay[i].Unload();
					mFeedbackDisplay[i] = null;
				}
			}
			mFeedbackDisplay = null;
		}

		public virtual void UpdateFeedbackDisplay(TetrisGame game)
		{
			GameScore gameScore = game.GetGameScore();
			int clearedLineCount = game.GetClearedLineCount();
			if (clearedLineCount > 0 && game.IsLineClearActive())
			{
				GameStatistics gameStatistics = game.GetGameStatistics();
				sbyte currentGameMode = GameApp.Get().GetGameSettings().GetCurrentGameMode();
				if (game.GetWell().IsEmpty())
				{
					PrepareFeedbackDisplay(12, gameScore);
				}
				if (currentGameMode == 1)
				{
					int specialGameEvent = game.GetSpecialGameEvent();
					if (specialGameEvent == 6 || specialGameEvent == 11)
					{
						int num = gameStatistics.GetStatistic(2) - 1;
						if (num == 0 || num == 1)
						{
							PrepareFeedbackDisplay(17, gameScore);
						}
					}
					int goalCountdown = game.GetGoalCountdown();
					if (goalCountdown <= 20 && goalCountdown + clearedLineCount > 20)
					{
						PrepareFeedbackDisplay(15, gameScore);
					}
				}
				int statistic = gameStatistics.GetStatistic(0);
				if (statistic != 0 && clearedLineCount > statistic % 15)
				{
					int currentTPM = gameStatistics.GetCurrentTPM(game.GetPlayTimeMs());
					if (currentTPM < 2500)
					{
						PrepareFeedbackDisplay(13, gameScore);
					}
					else if (currentTPM > 4000)
					{
						PrepareFeedbackDisplay(14, gameScore);
					}
				}
			}
			if (gameScore.NeedToDisplayNewHighScoreFeedback())
			{
				PrepareFeedbackDisplay(16, gameScore);
			}
		}

		public virtual void PrepareFeedbackDisplay(int feedbackType, GameScore gameScore)
		{
			if (mCurrentDisplayIdx == -1 || (mFeedbackDisplay[mCurrentDisplayIdx] != null && mFeedbackDisplay[mCurrentDisplayIdx].IsAnimationPlaying()))
			{
				mCurrentDisplayIdx = GetNextFeedbackDisplayIndex();
				if (mCurrentDisplayIdx == 5)
				{
					mCurrentDisplayIdx = 0;
				}
			}
			if (mFeedbackDisplay[mCurrentDisplayIdx] == null)
			{
				mFeedbackDisplay[mCurrentDisplayIdx] = new FeedbackDisplay(mLayerComponent);
			}
			mFeedbackDisplay[mCurrentDisplayIdx].PrepareFeedbackDisplay(feedbackType, gameScore);
			mNeedToDisplayFeedback = true;
		}

		public virtual void DisplayFeedback()
		{
			if (mNeedToDisplayFeedback)
			{
				mFeedbackDisplay[mCurrentDisplayIdx].DisplayFeedback();
				mNeedToDisplayFeedback = false;
			}
		}

		public virtual int GetNextFeedbackDisplayIndex()
		{
			int num = 0;
			return mCurrentDisplayIdx + 1;
		}
	}
}
