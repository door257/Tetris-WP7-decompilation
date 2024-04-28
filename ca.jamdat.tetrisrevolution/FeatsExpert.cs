namespace ca.jamdat.tetrisrevolution
{
	public class FeatsExpert : Expert
	{
		public const sbyte careerFeatUndefined = -1;

		public const sbyte careerFeatTetriminator = 0;

		public const sbyte careerFeatSpringClearing = 1;

		public const sbyte careerFeatWaterfall = 2;

		public const sbyte careerFeatSpinMaster = 3;

		public const sbyte careerFeatTenacious = 4;

		public const sbyte careerFeatTetrisMaster = 5;

		public const sbyte careerFeatsCount = 6;

		public const sbyte careerFeatStage_0 = 0;

		public const sbyte careerFeatStage_1 = 1;

		public const sbyte careerFeatStage_2 = 2;

		public const sbyte careerFeatStage_3 = 3;

		public const sbyte CareerFeatStagesCount = 4;

		public const sbyte advancedFeatUndefined = -1;

		public const sbyte advancedFeatWayOver = 0;

		public const sbyte advancedFeatMaster = 1;

		public const sbyte advancedFeatDodecadent = 2;

		public const sbyte advancedFeatOnARoll = 3;

		public const sbyte advancedFeatFrenzy = 4;

		public const sbyte advancedFeatsCount = 5;

		protected sbyte[] mCareerFeatArray;

		protected bool[] mAdvancedFeatArray;

		protected bool[] mNewFeatProgressedArray;

		protected bool[] mNewFeatCompletedArray;

		protected bool[] mNewAdvancedFeatArray;

		protected bool mFeatMasterPopupShown;

		protected bool[] mGameModeUnlockedArray;

		protected bool[] mNewGameModeUnlockedArray;

		protected bool mUnlockAdvancedFeatsFlag;

		public static FeatsExpert Get()
		{
			return (FeatsExpert)GameApp.Get().GetExpertManager().GetExpert(1);
		}

		public FeatsExpert()
		{
			mCareerFeatArray = new sbyte[6];
			mAdvancedFeatArray = new bool[5];
			mNewFeatProgressedArray = new bool[6];
			mNewFeatCompletedArray = new bool[6];
			mNewAdvancedFeatArray = new bool[5];
			mGameModeUnlockedArray = new bool[12];
			mNewGameModeUnlockedArray = new bool[12];
			Reset();
		}

		public override void destruct()
		{
			mCareerFeatArray = null;
			mAdvancedFeatArray = null;
			mNewFeatProgressedArray = null;
			mNewFeatCompletedArray = null;
			mNewAdvancedFeatArray = null;
			mGameModeUnlockedArray = null;
			mNewGameModeUnlockedArray = null;
		}

		public override void Update(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			ResetNewFeatProgressedArray();
			ResetNewFeatCompletedArray();
			ResetNewAdvancedFeatArray();
			ResetNewGameModeUnlockedArray();
			for (int i = 0; i < 6; i++)
			{
				if (!CanUpdateCareerFeats(i))
				{
					continue;
				}
				sbyte b = mCareerFeatArray[i];
				sbyte newCareerFeatStage = GetNewCareerFeatStage((sbyte)i, gameStatistics, careerStatistics);
				if (newCareerFeatStage > b)
				{
					mCareerFeatArray[i] = newCareerFeatStage;
					if (newCareerFeatStage == 3)
					{
						mNewFeatCompletedArray[i] = true;
					}
					else
					{
						mNewFeatProgressedArray[i] = true;
					}
				}
			}
			for (int j = 0; j < 12; j++)
			{
				if (CanUpdateVariantUnlocks(j))
				{
					bool flag = IsGameVariantUnlocked(j, gameStatistics, careerStatistics);
					if (!mGameModeUnlockedArray[j] && flag)
					{
						mGameModeUnlockedArray[j] = true;
						mNewGameModeUnlockedArray[j] = true;
					}
				}
			}
			for (int k = 0; k < 5; k++)
			{
				if (CanUpdateAdvanceFeats(k))
				{
					bool flag2 = mAdvancedFeatArray[k];
					bool flag = IsNewAdvancedFeatUnlocked((sbyte)k, gameStatistics, careerStatistics);
					if (!flag2 && (flag || mUnlockAdvancedFeatsFlag))
					{
						mAdvancedFeatArray[k] = true;
						mNewAdvancedFeatArray[k] = true;
					}
				}
			}
			AchievementManager.Update();
		}

		public override void Reset()
		{
			for (int i = 0; i < mCareerFeatArray.Length; i++)
			{
				mCareerFeatArray[i] = 0;
			}
			for (int j = 0; j < mAdvancedFeatArray.Length; j++)
			{
				mAdvancedFeatArray[j] = false;
			}
			mFeatMasterPopupShown = false;
			for (int k = 0; k < mGameModeUnlockedArray.Length; k++)
			{
				mGameModeUnlockedArray[k] = false;
			}
			mGameModeUnlockedArray[0] = true;
			mGameModeUnlockedArray[1] = true;
			mGameModeUnlockedArray[2] = true;
			ResetNewFeatProgressedArray();
			ResetNewFeatCompletedArray();
			ResetNewAdvancedFeatArray();
			ResetNewGameModeUnlockedArray();
		}

		public override void Read(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 6; i++)
			{
				mCareerFeatArray[i] = fileStream.ReadByte();
			}
			mFeatMasterPopupShown = fileStream.ReadBoolean();
			fileStream.ReadBooleanArray(mGameModeUnlockedArray, 12);
			fileStream.ReadBooleanArray(mAdvancedFeatArray, 5);
		}

		public override void Write(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 6; i++)
			{
				fileStream.WriteByte(mCareerFeatArray[i]);
			}
			fileStream.WriteBoolean(mFeatMasterPopupShown);
			fileStream.WriteBooleanArray(mGameModeUnlockedArray, 12);
			fileStream.WriteBooleanArray(mAdvancedFeatArray, 5);
		}

		public virtual int GetCareerFeatPercent(sbyte feat)
		{
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			int num = 0;
			switch (feat)
			{
			case 0:
				num = careerStatistics.GetStatistic(1);
				break;
			case 1:
				num = careerStatistics.GetStatistic(0);
				break;
			case 2:
				num = careerStatistics.GetStatistic(24);
				break;
			case 3:
				num = careerStatistics.GetStatistic(6);
				break;
			case 4:
				num = careerStatistics.GetStatistic(16);
				break;
			case 5:
			{
				ProgressionExpert progressionExpert = ProgressionExpert.Get();
				num = progressionExpert.GetGameCompletion() * 100;
				break;
			}
			}
			int num2 = num * 100 / GetCareerFeatStageValue(feat, 3);
			if (num2 >= 100)
			{
				return 100;
			}
			return num2;
		}

		public virtual sbyte GetCareerFeatStage(sbyte feat)
		{
			return mCareerFeatArray[feat];
		}

		public static int GetCareerFeatStageValue(sbyte feat, sbyte stage)
		{
			int result = -1;
			switch (feat)
			{
			case 0:
			{
				int num4;
				switch (stage)
				{
				default:
					num4 = -1;
					break;
				case 3:
					num4 = 100;
					break;
				case 2:
					num4 = 50;
					break;
				case 1:
					num4 = 25;
					break;
				}
				result = num4;
				break;
			}
			case 1:
			{
				int num6;
				switch (stage)
				{
				default:
					num6 = -1;
					break;
				case 3:
					num6 = 1000;
					break;
				case 2:
					num6 = 500;
					break;
				case 1:
					num6 = 100;
					break;
				}
				result = num6;
				break;
			}
			case 2:
			{
				int num2;
				switch (stage)
				{
				default:
					num2 = -1;
					break;
				case 3:
					num2 = 50;
					break;
				case 2:
					num2 = 25;
					break;
				case 1:
					num2 = 10;
					break;
				}
				result = num2;
				break;
			}
			case 3:
			{
				int num5;
				switch (stage)
				{
				default:
					num5 = -1;
					break;
				case 3:
					num5 = 25;
					break;
				case 2:
					num5 = 10;
					break;
				case 1:
					num5 = 5;
					break;
				}
				result = num5;
				break;
			}
			case 4:
			{
				int num3;
				switch (stage)
				{
				default:
					num3 = -1;
					break;
				case 3:
					num3 = 5400000;
					break;
				case 2:
					num3 = 3600000;
					break;
				case 1:
					num3 = 1800000;
					break;
				}
				result = num3;
				break;
			}
			case 5:
			{
				int num;
				switch (stage)
				{
				default:
					num = -1;
					break;
				case 3:
					num = 10000;
					break;
				case 2:
					num = 5000;
					break;
				case 1:
					num = 2500;
					break;
				}
				result = num;
				break;
			}
			}
			return result;
		}

		public virtual bool IsGameVariantUnlocked(int gameType)
		{
			if (GameApp.Get().GetIsDemo())
			{
				if (gameType != 0 && gameType != 1)
				{
					return gameType == 2;
				}
				return true;
			}
			return mGameModeUnlockedArray[gameType];
		}

		public virtual int GetUnlockVariantCount()
		{
			int num = 0;
			for (int i = 0; i < 12; i++)
			{
				if (IsGameVariantUnlocked(i))
				{
					num++;
				}
			}
			return num;
		}

		public virtual bool IsANewGameModeUnlocked(int gameType)
		{
			return mNewGameModeUnlockedArray[gameType];
		}

		public virtual bool IsAdvancedFeatUnlocked(sbyte feat)
		{
			return mAdvancedFeatArray[feat];
		}

		public virtual bool IsCareerFeatUnlocked(sbyte feat)
		{
			return GetCareerFeatPercent(feat) == 100;
		}

		public virtual bool IsANewCareerFeat(sbyte feat)
		{
			return mNewFeatProgressedArray[feat];
		}

		public virtual bool IsANewCompletedCareerFeat(sbyte feat)
		{
			return mNewFeatCompletedArray[feat];
		}

		public virtual bool IsANewAdvancedCareerFeat(sbyte feat)
		{
			return mNewAdvancedFeatArray[feat];
		}

		public virtual bool IsProgressedFeat()
		{
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				if (flag)
				{
					break;
				}
				flag = IsANewCareerFeat((sbyte)i);
			}
			return flag;
		}

		public virtual bool IsCompletedCareerFeat()
		{
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				if (flag)
				{
					break;
				}
				flag = mNewFeatCompletedArray[i];
			}
			return flag;
		}

		public virtual bool IsCompletedAdvancedFeat()
		{
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				if (flag)
				{
					break;
				}
				flag = mNewAdvancedFeatArray[i];
			}
			return flag;
		}

		public virtual bool IsNewGameVariantUnlocked()
		{
			bool flag = false;
			for (int i = 0; i < 12; i++)
			{
				if (flag)
				{
					break;
				}
				flag = mNewGameModeUnlockedArray[i];
			}
			return flag;
		}

		public virtual bool IsFeatMasterPopupShown()
		{
			return mFeatMasterPopupShown;
		}

		public virtual void ShowFeatMasterPopup()
		{
			mFeatMasterPopupShown = true;
		}

		public virtual bool PopProgressedFeat()
		{
			for (int i = 0; i < 6; i++)
			{
				if (IsANewCareerFeat((sbyte)i))
				{
					mNewFeatProgressedArray[i] = false;
					break;
				}
			}
			return IsProgressedFeat();
		}

		public virtual sbyte GetProgressedFeat()
		{
			int i;
			for (i = 0; i < 6 && !IsANewCareerFeat((sbyte)i); i++)
			{
			}
			return (sbyte)i;
		}

		public virtual bool PopCompletedCareerFeat()
		{
			for (int i = 0; i < 6; i++)
			{
				if (mNewFeatCompletedArray[i])
				{
					mNewFeatCompletedArray[i] = false;
					break;
				}
			}
			return IsCompletedCareerFeat();
		}

		public virtual sbyte GetCompletedCareerFeat()
		{
			int i;
			for (i = 0; i < 6 && !mNewFeatCompletedArray[i]; i++)
			{
			}
			return (sbyte)i;
		}

		public virtual bool PopCompletedAdvancedFeat()
		{
			for (int i = 0; i < 5; i++)
			{
				if (mNewAdvancedFeatArray[i])
				{
					mNewAdvancedFeatArray[i] = false;
					break;
				}
			}
			return IsCompletedAdvancedFeat();
		}

		public virtual sbyte GetCompletedAdvancedFeat()
		{
			int i;
			for (i = 0; i < 5 && !mNewAdvancedFeatArray[i]; i++)
			{
			}
			return (sbyte)i;
		}

		public virtual bool PopUnlockedGameVariant()
		{
			for (int i = 0; i < 12; i++)
			{
				if (mNewGameModeUnlockedArray[i])
				{
					mNewGameModeUnlockedArray[i] = false;
					break;
				}
			}
			return IsNewGameVariantUnlocked();
		}

		public virtual int GetUnlockedGameVariant()
		{
			int i;
			for (i = 0; i < 12 && !mNewGameModeUnlockedArray[i]; i++)
			{
			}
			return i;
		}

		public virtual void UnlockAdvancedFeats()
		{
			mUnlockAdvancedFeatsFlag = true;
		}

		public virtual void UnlockAllGameVariants()
		{
			for (int i = 0; i < 12; i++)
			{
				mGameModeUnlockedArray[i] = true;
			}
		}

		public virtual bool CanUpdateCareerFeats(int feat)
		{
			return true;
		}

		public virtual bool CanUpdateAdvanceFeats(int feat)
		{
			bool flag = true;
			return !GameApp.Get().GetGameSettings().IsMarathonMode() || feat == 4;
		}

		public virtual bool CanUpdateVariantUnlocks(int variant)
		{
			bool flag = true;
			return !GameApp.Get().GetGameSettings().IsMarathonMode() || (variant != 10 && variant != 3 && variant != 8 && variant != 5 && variant != 6 && variant != 11 && variant != 7);
		}

		public virtual void ResetNewFeatProgressedArray()
		{
			for (int i = 0; i < mNewFeatProgressedArray.Length; i++)
			{
				mNewFeatProgressedArray[i] = false;
			}
		}

		public virtual void ResetNewFeatCompletedArray()
		{
			for (int i = 0; i < mNewFeatCompletedArray.Length; i++)
			{
				mNewFeatCompletedArray[i] = false;
			}
		}

		public virtual void ResetNewAdvancedFeatArray()
		{
			for (int i = 0; i < mNewAdvancedFeatArray.Length; i++)
			{
				mNewAdvancedFeatArray[i] = false;
			}
		}

		public virtual void ResetNewGameModeUnlockedArray()
		{
			for (int i = 0; i < mNewGameModeUnlockedArray.Length; i++)
			{
				mNewGameModeUnlockedArray[i] = false;
			}
		}

		public static sbyte GetNewCareerFeatStage(sbyte feat, GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			sbyte result = 0;
			switch (feat)
			{
			case 0:
				result = GetCareerFeatTetriminatorStage(gameStatistics, careerStatistics);
				break;
			case 1:
				result = GetCareerFeatSpringClearingStage(gameStatistics, careerStatistics);
				break;
			case 2:
				result = GetCareerFeatWaterfallStage(gameStatistics, careerStatistics);
				break;
			case 3:
				result = GetCareerFeatSpinMasterStage(gameStatistics, careerStatistics);
				break;
			case 4:
				result = GetCareerFeatTenaciousStage(gameStatistics, careerStatistics);
				break;
			case 5:
				result = GetCareerFeatTetrisMasterStage(gameStatistics, careerStatistics);
				break;
			}
			return result;
		}

		public static sbyte GetCareerFeatTetriminatorStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return FindCareerFeatStage(careerStatistics.GetStatistic(1), 25, 50, 100);
		}

		public static sbyte GetCareerFeatSpringClearingStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return FindCareerFeatStage(careerStatistics.GetStatistic(0), 100, 500, 1000);
		}

		public static sbyte GetCareerFeatWaterfallStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return FindCareerFeatStage(careerStatistics.GetStatistic(24), 10, 25, 50);
		}

		public static sbyte GetCareerFeatSpinMasterStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return FindCareerFeatStage(careerStatistics.GetStatistic(6), 5, 10, 25);
		}

		public static sbyte GetCareerFeatTenaciousStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return FindCareerFeatStage(careerStatistics.GetStatistic(16), 1800000, 3600000, 5400000);
		}

		public static sbyte GetCareerFeatTetrisMasterStage(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			int statisticValue = progressionExpert.GetGameCompletion() * 100;
			return FindCareerFeatStage(statisticValue, 2500, 5000, 10000);
		}

		public static bool IsGameVariantUnlocked(int gameType, GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			bool result = false;
			switch (gameType)
			{
			case 2:
				result = IsVariantVanillaUnlocked(gameStatistics, careerStatistics);
				break;
			case 9:
				result = IsVariantChillUnlocked(gameStatistics, careerStatistics);
				break;
			case 10:
				result = IsVariantFlashliteUnlocked(gameStatistics, careerStatistics);
				break;
			case 3:
				result = IsVariantFloodUnlocked(gameStatistics, careerStatistics);
				break;
			case 4:
				result = IsVariantLedgesUnlocked(gameStatistics, careerStatistics);
				break;
			case 5:
				result = IsVariantLimboUnlocked(gameStatistics, careerStatistics);
				break;
			case 6:
				result = IsVariantMagneticUnlocked(gameStatistics, careerStatistics);
				break;
			case 7:
				result = IsVariantScannerUnlocked(gameStatistics, careerStatistics);
				break;
			case 8:
				result = IsVariantSplitUnlocked(gameStatistics, careerStatistics);
				break;
			case 1:
				result = IsVariantTreadmillUnlocked(gameStatistics, careerStatistics);
				break;
			case 11:
				result = IsVariantMasterUnlocked(gameStatistics, careerStatistics);
				break;
			}
			return result;
		}

		public static bool IsVariantVanillaUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return true;
		}

		public static bool IsVariantChillUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.IsStatistic(0) && gameStatistics.GetStatistic(1) >= 8 && 7 == gameStatistics.GetGameVariant();
		}

		public static bool IsVariantFlashliteUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.IsStatistic(0) && gameStatistics.GetGameLevel() >= 10;
		}

		public static bool IsVariantFloodUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.IsStatistic(0) && gameStatistics.GetStatistic(19) < 240000;
		}

		public static bool IsVariantLedgesUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return careerStatistics.GetStatistic(0) >= 400;
		}

		public static bool IsVariantLimboUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.GetGameVariant() == 0 && gameStatistics.IsStatistic(0);
		}

		public static bool IsVariantMagneticUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			if (gameStatistics.GetGameVariant() == 2 && gameStatistics.IsStatistic(0))
			{
				return gameStatistics.GetStatistic(1) >= 6;
			}
			return false;
		}

		public static bool IsVariantMasterUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			bool result = true;
			for (int i = 0; i < 11; i++)
			{
				if (progressionExpert.GetHighestLevelDone(i) <= 0)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public static bool IsVariantScannerUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			int num = 0;
			if (gameStatistics.IsStatistic(0))
			{
				for (int i = 0; i < 12; i++)
				{
					if (num >= 3)
					{
						break;
					}
					if (progressionExpert.GetHighestLevelDone(i) > 0)
					{
						num++;
					}
				}
			}
			return num == 3;
		}

		public static bool IsVariantSplitUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.GetStatistic(2) >= 4;
		}

		public static bool IsVariantTreadmillUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return true;
		}

		public static bool IsNewAdvancedFeatUnlocked(sbyte feat, GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			bool result = false;
			switch (feat)
			{
			case 0:
				result = IsAdvancedFeatWayOverUnlocked(gameStatistics, careerStatistics);
				break;
			case 1:
				result = IsAdvancedFeatMasterUnlocked(gameStatistics, careerStatistics);
				break;
			case 2:
				result = IsAdvancedFeatDodecadentUnlocked(gameStatistics, careerStatistics);
				break;
			case 3:
				result = IsAdvancedFeatOnARollUnlocked(gameStatistics, careerStatistics);
				break;
			case 4:
				result = IsAdvancedFeatFrenzyUnlocked(gameStatistics, careerStatistics);
				break;
			}
			return result;
		}

		public static bool IsAdvancedFeatWayOverUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.GetStatistic(0) >= 44;
		}

		public static bool IsAdvancedFeatMasterUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.GetGameVariant() == 11 && gameStatistics.IsStatistic(0);
		}

		public static bool IsAdvancedFeatDodecadentUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			return gameStatistics.GetStatistic(1) >= 12;
		}

		public static bool IsAdvancedFeatOnARollUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			if (gameStatistics.IsStatistic(0))
			{
				return gameStatistics.IsStatistic(2);
			}
			return false;
		}

		public static bool IsAdvancedFeatFrenzyUnlocked(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			if (gameStatistics.IsStatistic(0))
			{
				return careerStatistics.GetStatistic(18) >= 3500;
			}
			return false;
		}

		public static sbyte FindCareerFeatStage(int statisticValue, int stage1Limit, int stage2Limit, int stage3Limit)
		{
			if (statisticValue >= stage3Limit)
			{
				return 3;
			}
			if (statisticValue >= stage2Limit)
			{
				return 2;
			}
			if (statisticValue >= stage1Limit)
			{
				return 1;
			}
			return 0;
		}

		public static FeatsExpert[] InstArrayFeatsExpert(int size)
		{
			FeatsExpert[] array = new FeatsExpert[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FeatsExpert();
			}
			return array;
		}

		public static FeatsExpert[][] InstArrayFeatsExpert(int size1, int size2)
		{
			FeatsExpert[][] array = new FeatsExpert[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FeatsExpert[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FeatsExpert();
				}
			}
			return array;
		}

		public static FeatsExpert[][][] InstArrayFeatsExpert(int size1, int size2, int size3)
		{
			FeatsExpert[][][] array = new FeatsExpert[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FeatsExpert[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FeatsExpert[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FeatsExpert();
					}
				}
			}
			return array;
		}
	}
}
