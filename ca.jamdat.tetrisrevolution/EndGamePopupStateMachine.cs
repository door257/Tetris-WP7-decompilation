namespace ca.jamdat.tetrisrevolution
{
	public class EndGamePopupStateMachine
	{
		public const int stateInvalid = -1;

		public const int stateStartingSequencePopup = 0;

		public const int stateSuccessPopup = 1;

		public const int stateFailurePopup = 2;

		public const int stateNewBestPopup = 3;

		public const int stateGameStatsPopup = 4;

		public const int stateCareerFeatCompletePopup = 5;

		public const int stateAdvancedFeatCompletePopup = 6;

		public const int stateFeatProgressPopup = 7;

		public const int stateFeatMasterPopup = 8;

		public const int stateUnlockPopup = 9;

		public const int stateRetryPopup = 10;

		public const int stateDemoTrialOverPopup = 11;

		public const int popupStateCount = 12;

		public sbyte mLastNewBestType;

		public sbyte mLastCompletedCareerFeat;

		public sbyte mLastProgressedCareerFeat;

		public sbyte mLastCompletedAdvancedFeat;

		public int mLastUnlockedGameVariant;

		public int mCurrentCommand;

		public int mCurrentPopupState;

		public EndGamePopupStateMachine()
		{
			mCurrentPopupState = -1;
			mLastNewBestType = 3;
			mLastCompletedCareerFeat = -1;
			mLastProgressedCareerFeat = -1;
			mLastCompletedAdvancedFeat = -1;
			mLastUnlockedGameVariant = -1;
		}

		public virtual void destruct()
		{
		}

		public virtual void Start()
		{
			mCurrentPopupState = 0;
		}

		public virtual void Reset()
		{
			mLastNewBestType = 3;
			mCurrentPopupState = -1;
		}

		public virtual int NextCommand()
		{
			int num = 0;
			TetrisGame tetrisGame = GameFactory.GetTetrisGame();
			switch (mCurrentPopupState)
			{
			case 0:
				if (tetrisGame.HasWon())
				{
					ChangeState(1);
					num = 20;
				}
				else
				{
					ChangeState(2);
					num = 21;
				}
				break;
			case 1:
			case 2:
			case 3:
				if (!ChangeState(4))
				{
					num = 19;
					mCurrentPopupState = 3;
				}
				else
				{
					num = 24;
				}
				break;
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				if (!ChangeState(5))
				{
					num = 26;
					mCurrentPopupState = 7;
				}
				else if (!ChangeState(6))
				{
					num = 27;
					mCurrentPopupState = 5;
				}
				else if (!ChangeState(9))
				{
					num = 28;
					mCurrentPopupState = 9;
				}
				else if (!ChangeState(8))
				{
					num = 29;
					mCurrentPopupState = 9;
				}
				else if (!ChangeState(10))
				{
					num = 25;
					mCurrentPopupState = 8;
				}
				break;
			}
			if (num == 0)
			{
				if (tetrisGame.HasWon())
				{
					Reset();
					num = ((!GameApp.Get().GetGameSettings().IsMarathonMode()) ? 31 : 18);
				}
				else if (GameApp.Get().GetIsDemo())
				{
					mCurrentPopupState = 11;
					num = -34;
				}
				else
				{
					mCurrentPopupState = 10;
					num = 22;
				}
			}
			if (mCurrentCommand != num)
			{
				mCurrentCommand = num;
			}
			return num;
		}

		public virtual int CurrentCommand()
		{
			return mCurrentCommand;
		}

		public virtual bool AcquireNewBest()
		{
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			bool result = false;
			if (careerStatistics.IsNewBests() && careerStatistics.GetNewBestType() != mLastNewBestType)
			{
				mLastNewBestType = careerStatistics.GetNewBestType();
				careerStatistics.PopNewBestType();
				result = true;
			}
			return result;
		}

		public virtual sbyte GetNewBestType()
		{
			return mLastNewBestType;
		}

		public virtual bool AcquireProgressedFeat()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool result = false;
			if (featsExpert.IsProgressedFeat() && featsExpert.GetProgressedFeat() != mLastProgressedCareerFeat)
			{
				mLastProgressedCareerFeat = featsExpert.GetProgressedFeat();
				featsExpert.PopProgressedFeat();
				result = true;
			}
			return result;
		}

		public virtual bool AcquireCompletedCareerFeat()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool result = false;
			if (featsExpert.IsCompletedCareerFeat() && featsExpert.GetCompletedCareerFeat() != mLastCompletedCareerFeat)
			{
				mLastCompletedCareerFeat = featsExpert.GetCompletedCareerFeat();
				featsExpert.PopCompletedCareerFeat();
				result = true;
			}
			return result;
		}

		public virtual bool AcquireCompletedAdvancedFeat()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool result = false;
			if (featsExpert.IsCompletedAdvancedFeat() && featsExpert.GetCompletedAdvancedFeat() != mLastCompletedAdvancedFeat)
			{
				mLastCompletedAdvancedFeat = featsExpert.GetCompletedAdvancedFeat();
				featsExpert.PopCompletedAdvancedFeat();
				result = true;
			}
			return result;
		}

		public virtual sbyte GetProgressedCareerFeat()
		{
			return mLastProgressedCareerFeat;
		}

		public virtual sbyte GetCompletedCareerFeat()
		{
			return mLastCompletedCareerFeat;
		}

		public virtual sbyte GetCompletedAdvancedFeat()
		{
			return mLastCompletedAdvancedFeat;
		}

		public virtual bool AcquireVariantUnlocked()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool result = false;
			if (featsExpert.IsNewGameVariantUnlocked() && featsExpert.GetUnlockedGameVariant() != mLastUnlockedGameVariant)
			{
				mLastUnlockedGameVariant = featsExpert.GetUnlockedGameVariant();
				featsExpert.PopUnlockedGameVariant();
				result = true;
			}
			return result;
		}

		public virtual int GetUnlockedGameVariant()
		{
			return mLastUnlockedGameVariant;
		}

		public virtual bool IsShowingEndGamePopups()
		{
			return mCurrentPopupState != -1;
		}

		public virtual bool ChangeState(int state)
		{
			bool flag = false;
			switch (state)
			{
			case 3:
				flag = true;
				break;
			case 4:
				flag = ((!AcquireNewBest()) ? true : false);
				break;
			case 7:
				flag = true;
				break;
			case 5:
				flag = ((!AcquireProgressedFeat()) ? true : false);
				break;
			case 6:
				flag = ((!AcquireCompletedCareerFeat()) ? true : false);
				break;
			case 9:
				flag = !AcquireCompletedAdvancedFeat() && ((!AcquireNewBest()) ? true : false);
				break;
			case 8:
				flag = !AcquireVariantUnlocked() && !AcquireCompletedAdvancedFeat() && ((!AcquireNewBest()) ? true : false);
				break;
			case 10:
				flag = (ProgressionExpert.Get().GetGameCompletion() != 100 || FeatsExpert.Get().IsFeatMasterPopupShown()) && !AcquireVariantUnlocked() && !AcquireCompletedAdvancedFeat() && ((!AcquireNewBest()) ? true : false);
				break;
			case 1:
			case 2:
				flag = true;
				break;
			}
			if (flag && mCurrentPopupState != state)
			{
				mCurrentPopupState = state;
			}
			return flag;
		}

		public static EndGamePopupStateMachine[] InstArrayEndGamePopupStateMachine(int size)
		{
			EndGamePopupStateMachine[] array = new EndGamePopupStateMachine[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new EndGamePopupStateMachine();
			}
			return array;
		}

		public static EndGamePopupStateMachine[][] InstArrayEndGamePopupStateMachine(int size1, int size2)
		{
			EndGamePopupStateMachine[][] array = new EndGamePopupStateMachine[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EndGamePopupStateMachine[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EndGamePopupStateMachine();
				}
			}
			return array;
		}

		public static EndGamePopupStateMachine[][][] InstArrayEndGamePopupStateMachine(int size1, int size2, int size3)
		{
			EndGamePopupStateMachine[][][] array = new EndGamePopupStateMachine[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EndGamePopupStateMachine[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EndGamePopupStateMachine[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new EndGamePopupStateMachine();
					}
				}
			}
			return array;
		}
	}
}
