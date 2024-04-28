namespace ca.jamdat.tetrisrevolution
{
	public class GameScore
	{
		public TetrisGame mGame;

		public int mCurrentScore;

		public int mCascadeLevel;

		public bool mHasClearedLine;

		public GameStatistics mGameStatistics;

		public LineScoreDisplay[] mLineScoreDisplays;

		public int mCurrentDisplayIdx;

		public int mDisplayCleanerIdx;

		public bool mNewHighScoreFeedbackDisplayShown;

		public GameScore(TetrisGame tetrisGame, GameStatistics gameStatistics)
		{
			mGame = tetrisGame;
			mGameStatistics = gameStatistics;
			mCurrentDisplayIdx = -1;
			mLineScoreDisplays = new LineScoreDisplay[20];
			for (int i = 0; i < mLineScoreDisplays.Length; i++)
			{
				mLineScoreDisplays[i] = null;
			}
		}

		public virtual void destruct()
		{
		}

		public virtual void Unload()
		{
			for (int i = 0; i < 20; i++)
			{
				if (mLineScoreDisplays[i] != null)
				{
					mLineScoreDisplays[i].Unload();
					mLineScoreDisplays[i] = null;
				}
			}
			mDisplayCleanerIdx = 0;
			mCurrentDisplayIdx = -1;
		}

		public virtual void ReleaseDisplayArray()
		{
			mLineScoreDisplays = null;
		}

		public virtual void IncreaseScore(int pts)
		{
			mCurrentScore += pts;
		}

		public virtual void UpdateScore()
		{
			int clearedLineCount = mGame.GetClearedLineCount();
			bool flag = mGame.IsThereTSpin();
			if (clearedLineCount != 0 || flag)
			{
				int num = 0;
				num = ((mCascadeLevel != 0 || !flag) ? GetPointForLineClear() : GetPointForTSpin());
				num *= mGame.GetCurrentLevel();
				if (mGame.IsThereBackToBack() && (clearedLineCount >= 4 || (flag && clearedLineCount > 0)))
				{
					num += num / 2;
				}
				mGameStatistics.OnUpdateScore(num, clearedLineCount, flag, mGame.GetSpecialGameEvent(), mGame.IsThereBackToBack(), mCascadeLevel);
				IncreaseScore(num);
				ShowLineScoreDisplay(num);
			}
		}

		public virtual int GetCurrentScore()
		{
			return mCurrentScore;
		}

		public virtual bool NeedToDisplayNewHighScoreFeedback()
		{
			bool result = false;
			int variant = mGame.GetVariant();
			bool flag = mGame.IsMarathonMode();
			if (!mNewHighScoreFeedbackDisplayShown)
			{
				CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
				int num = -1;
				num = ((!flag) ? careerStatistics.GetStatistic(variant, 0) : careerStatistics.GetStatistic(21));
				if (num != 0 && GetCurrentScore() > num)
				{
					result = true;
					mNewHighScoreFeedbackDisplayShown = true;
				}
			}
			return result;
		}

		public virtual void IncreaseCascadeLevel()
		{
			mCascadeLevel++;
		}

		public virtual void ResetCascadeLevel()
		{
			mGameStatistics.OnResetCascadeLevel(mCascadeLevel);
			mCascadeLevel = 0;
		}

		public virtual int GetCascadeLevel()
		{
			return mCascadeLevel;
		}

		public virtual void OnTurnEnd()
		{
			mHasClearedLine = false;
			mGameStatistics.OnTurnEnd(mGame.IsBackToBackPossible());
		}

		public virtual void OnGameOver()
		{
			for (int i = 0; i < 20 && mLineScoreDisplays[i] != null; i++)
			{
				mLineScoreDisplays[i].StopAndDetachAnimation();
			}
			mGameStatistics.OnGameOver(mCurrentScore, mCascadeLevel, 0, GameApp.Get().GetGameSettings().IsMarathonMode());
		}

		public virtual void CleanScoreDisplayAnimIfOver()
		{
			if (mCurrentDisplayIdx >= 0 && mLineScoreDisplays[mDisplayCleanerIdx].IsAnimationOver())
			{
				mLineScoreDisplays[mDisplayCleanerIdx].StopAndDetachAnimation();
				mDisplayCleanerIdx++;
				if (mDisplayCleanerIdx > mCurrentDisplayIdx)
				{
					mDisplayCleanerIdx = 0;
					mCurrentDisplayIdx = -1;
				}
			}
		}

		public virtual TetrisGame GetGame()
		{
			return mGame;
		}

		public virtual int GetPointForLineClear()
		{
			int clearedLineCount = mGame.GetClearedLineCount();
			int result = 0;
			switch (clearedLineCount)
			{
			case 1:
				result = 100;
				break;
			case 2:
				result = 300;
				break;
			case 3:
				result = 500;
				break;
			case 4:
				result = 800;
				break;
			case 5:
				result = 1200;
				break;
			case 6:
				result = 1700;
				break;
			case 7:
				result = 2300;
				break;
			case 8:
				result = 3000;
				break;
			case 9:
				result = 3800;
				break;
			case 10:
				result = 4700;
				break;
			case 11:
				result = 5700;
				break;
			case 12:
				result = 6800;
				break;
			case 13:
				result = 8000;
				break;
			case 14:
				result = 9300;
				break;
			case 15:
				result = 10700;
				break;
			case 16:
				result = 12200;
				break;
			case 17:
				result = 13800;
				break;
			case 18:
				result = 15500;
				break;
			case 19:
				result = 17300;
				break;
			case 20:
				result = 19200;
				break;
			}
			return result;
		}

		public virtual int GetPointForTSpin()
		{
			int specialGameEvent = mGame.GetSpecialGameEvent();
			int result = 0;
			switch (specialGameEvent)
			{
			case 0:
				result = 100;
				break;
			case 2:
			case 7:
				result = 200;
				break;
			case 1:
				result = 400;
				break;
			case 3:
			case 8:
				result = 800;
				break;
			case 4:
			case 9:
				result = 1200;
				break;
			case 5:
			case 10:
				result = 1600;
				break;
			}
			return result;
		}

		public virtual void ShowLineScoreDisplay(int score)
		{
			mCurrentDisplayIdx++;
			if (mLineScoreDisplays[mCurrentDisplayIdx] == null)
			{
				mLineScoreDisplays[mCurrentDisplayIdx] = new LineScoreDisplay();
			}
			mLineScoreDisplays[mCurrentDisplayIdx].Initialize(score, mGame);
		}
	}
}
