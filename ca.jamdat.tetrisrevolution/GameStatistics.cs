namespace ca.jamdat.tetrisrevolution
{
	public class GameStatistics
	{
		public const sbyte statisticsNoTimePlayed = -1;

		public const sbyte statisticsNoLineClears = -1;

		public const sbyte totalLineClears = 0;

		public const sbyte highestLineClearsInOneTurn = 1;

		public const sbyte totalTetrises = 2;

		public const sbyte totalTriples = 3;

		public const sbyte totalDoubles = 4;

		public const sbyte totalSingles = 5;

		public const sbyte totalTSpinMini = 6;

		public const sbyte totalTSpin = 7;

		public const sbyte totalTSpinMiniSingle = 8;

		public const sbyte totalTSpinSingle = 9;

		public const sbyte totalTSpinDouble = 10;

		public const sbyte totalTSpinTriple = 11;

		public const sbyte totalBravo = 12;

		public const sbyte totalBackToBack = 13;

		public const sbyte longestBackToBackStreak = 14;

		public const sbyte totalCascadeLineClears = 15;

		public const sbyte highestCascadeLevel = 16;

		public const sbyte highestLineClearsPayoff = 17;

		public const sbyte totalTetriminosFallen = 18;

		public const sbyte timePlayed = 19;

		public const sbyte totalPoints = 20;

		public const sbyte highestLevel = 21;

		public const sbyte gameCommonStatisticsCount = 22;

		public const sbyte totalHardDrops = 22;

		public const sbyte totalSoftDrops = 23;

		public const sbyte totalRotations = 24;

		public const sbyte totalMovements = 25;

		public const sbyte totalTimeBtwnGenerationAndInput = 26;

		public const sbyte totalMatrixFillArea = 27;

		public const sbyte totalNbOfTurns = 28;

		public const sbyte tempBioStatisticsCount = 29;

		public const sbyte gameBioStatisticsCount = 7;

		public const sbyte longStatisticsCount = 29;

		public const sbyte wonGame = 0;

		public const sbyte hasLeveledUp = 1;

		public const sbyte onlyPayoffBonusOnLineClears = 2;

		public const sbyte modeLedgeNotTetriminoFellThrough = 3;

		public const sbyte booleanStatisticsCount = 4;

		public const sbyte temporaryLineClearsInOneTurn = 0;

		public const sbyte temporaryBackToBackStreak = 1;

		public const sbyte temporaryIntStatisticCount = 2;

		public sbyte mGameMode;

		public int mGameVariant;

		public int mGameLevel;

		public int[] mIntegerStatisticsArray;

		public bool[] mBooleanStatisticsArray;

		public int[] mTemporaryIntStatisticsArray;

		public int mTetrisSpeedLevel;

		public bool mCanUpdateSoftDropBioStats;

		public bool mCanUpdateTotalTimeBtwnGenerationAndInputBioStat;

		public int mMatrixFillAreaLastTurn;

		public bool mSpecialMoveFlag;

		public GameStatistics(int gameVariant, int startingLevel)
		{
			mGameMode = GameApp.Get().GetGameSettings().GetCurrentGameMode();
			mGameVariant = gameVariant;
			mGameLevel = startingLevel;
			mTetrisSpeedLevel = -1;
			Initialize();
		}

		public virtual void destruct()
		{
		}

		public virtual void Destroy()
		{
			mIntegerStatisticsArray = null;
			mTemporaryIntStatisticsArray = null;
			mBooleanStatisticsArray = null;
		}

		public virtual void Reset()
		{
			mGameVariant = -1;
			mGameLevel = -1;
			for (int i = 0; i < mIntegerStatisticsArray.Length; i++)
			{
				mIntegerStatisticsArray[i] = 0;
			}
			for (int j = 0; j < mBooleanStatisticsArray.Length; j++)
			{
				mBooleanStatisticsArray[j] = false;
			}
			for (int k = 0; k < mTemporaryIntStatisticsArray.Length; k++)
			{
				mTemporaryIntStatisticsArray[k] = 0;
			}
			SetStatistic(2, true);
		}

		public virtual int GetGameVariant()
		{
			return mGameVariant;
		}

		public virtual void SetGameType(int gameType)
		{
			mGameVariant = gameType;
		}

		public virtual int GetGameLevel()
		{
			return mGameLevel;
		}

		public virtual void SetGameLevel(int gameLevel)
		{
			mGameLevel = gameLevel;
		}

		public virtual int GetStatistic(sbyte id)
		{
			return mIntegerStatisticsArray[id];
		}

		public virtual void SetStatistic(sbyte id, int val)
		{
			mIntegerStatisticsArray[id] = val;
		}

		public virtual void IncreaseStatistic(sbyte id, int val)
		{
			mIntegerStatisticsArray[id] += val;
		}

		public virtual void DecreaseStatistic(sbyte id, int val)
		{
			mIntegerStatisticsArray[id] -= val;
		}

		public virtual void KeepHighestStatistic(sbyte id, int val)
		{
			mIntegerStatisticsArray[id] = GetHighestStatistic(mIntegerStatisticsArray[id], val);
		}

		public virtual void KeepHighestStatistic(sbyte intId, sbyte tempId)
		{
			mIntegerStatisticsArray[intId] = GetHighestStatistic(mIntegerStatisticsArray[intId], mTemporaryIntStatisticsArray[tempId]);
		}

		public virtual bool IsStatistic(sbyte id)
		{
			return mBooleanStatisticsArray[id];
		}

		public virtual void SetStatistic(sbyte id, bool val)
		{
			mBooleanStatisticsArray[id] = val;
		}

		public virtual int GetTemporaryStatistic(sbyte id)
		{
			return mTemporaryIntStatisticsArray[id];
		}

		public virtual void SetTemporaryStatistic(sbyte id, int val)
		{
			mTemporaryIntStatisticsArray[id] = val;
		}

		public virtual void IncreaseTemporaryStatistic(sbyte id, int val)
		{
			mTemporaryIntStatisticsArray[id] += val;
		}

		public virtual void DecreaseTemporaryStatistic(sbyte id, int val)
		{
			mTemporaryIntStatisticsArray[id] -= val;
		}

		public virtual void ResetTemporaryStatistic(sbyte id)
		{
			mTemporaryIntStatisticsArray[id] = 0;
		}

		public virtual void OnLevelUp(int newLevel)
		{
			SetStatistic(1, true);
			SetGameLevel(newLevel);
		}

		public virtual void OnUpdateScore(int score, int clearedLinesCount, bool isThereTSpin, int specialGameEvent, bool isThereBackToBack, int cascadeLevel)
		{
			KeepHighestStatistic(17, score);
			IncreaseTemporaryStatistic(0, clearedLinesCount);
			IncreaseStatistic(0, clearedLinesCount);
			switch (clearedLinesCount)
			{
			case 0:
				if (isThereTSpin)
				{
					if (specialGameEvent == 1)
					{
						IncreaseStatistic(7);
					}
					else
					{
						IncreaseStatistic(6);
					}
				}
				break;
			case 1:
				IncreaseStatistic(5);
				if (isThereTSpin)
				{
					if (specialGameEvent == 3 || specialGameEvent == 8)
					{
						IncreaseStatistic(7);
						IncreaseStatistic(9);
					}
					else
					{
						IncreaseStatistic(6);
						IncreaseStatistic(8);
					}
				}
				break;
			case 2:
				IncreaseStatistic(4);
				if (isThereTSpin)
				{
					IncreaseStatistic(7);
					IncreaseStatistic(10);
				}
				break;
			case 3:
				IncreaseStatistic(3);
				if (isThereTSpin)
				{
					IncreaseStatistic(7);
					IncreaseStatistic(11);
				}
				break;
			default:
				IncreaseStatistic(2);
				break;
			}
			if (isThereBackToBack)
			{
				IncreaseTemporaryStatistic(1);
				IncreaseStatistic(13);
			}
			if (cascadeLevel > 0)
			{
				IncreaseStatistic(15, clearedLinesCount);
			}
			if (isThereTSpin || (cascadeLevel > 0 && clearedLinesCount > 0) || clearedLinesCount >= 4)
			{
				mSpecialMoveFlag = true;
			}
		}

		public virtual void OnResetCascadeLevel(int cascadeLevel)
		{
			DoCascadeLevel(cascadeLevel);
		}

		public virtual void OnTurnEnd(bool isBackToBackPossible)
		{
			DoBackToBackStreak(isBackToBackPossible);
			KeepHighestStatistic((sbyte)1, (sbyte)0);
			DoSpecialMovesCheck();
			mSpecialMoveFlag = false;
			ResetTemporaryStatistic(0);
			IncreaseStatistic(18);
		}

		public virtual void OnGameOver(int score, int cascadeLevel, int comboLevel, bool isMarathon)
		{
			IncreaseStatistic(20, score);
			IncreaseStatistic(21, GetGameLevel());
			DoBackToBackStreak(false);
			DoCascadeLevel(cascadeLevel);
		}

		public virtual void OnHardDrop()
		{
			IncreaseStatistic(22);
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = false;
		}

		public virtual void OnLineClearResult(int nbOfLinesCleared)
		{
			if (mGameMode == 0)
			{
				int num = nbOfLinesCleared * 10;
				DecreaseStatistic(27, num);
				mMatrixFillAreaLastTurn -= num;
			}
		}

		public virtual void OnSoftDropActive()
		{
			if (mCanUpdateSoftDropBioStats)
			{
				IncreaseStatistic(23);
				mCanUpdateSoftDropBioStats = false;
			}
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = false;
		}

		public virtual void OnStartingNewTurn()
		{
			IncreaseStatistic(28);
			mCanUpdateSoftDropBioStats = true;
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = true;
		}

		public virtual void OnTetriminoHold()
		{
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = false;
		}

		public virtual void OnTetriminoLock()
		{
			if (mGameMode == 0)
			{
				mMatrixFillAreaLastTurn += 4;
				IncreaseStatistic(27, mMatrixFillAreaLastTurn);
			}
		}

		public virtual void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mCanUpdateTotalTimeBtwnGenerationAndInputBioStat)
			{
				IncreaseStatistic(26, deltaTimeMs);
			}
		}

		public virtual void OnValidDoPieceAction()
		{
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = false;
		}

		public virtual int GetAverageTPM()
		{
			return GetTPM(mIntegerStatisticsArray[18], mIntegerStatisticsArray[19]);
		}

		public virtual int GetCurrentTPM(int currentGamePlayTime)
		{
			return GetTPM(mIntegerStatisticsArray[18], currentGamePlayTime);
		}

		public virtual int GetAveragePointsPerLineClears()
		{
			int num = mIntegerStatisticsArray[0];
			int result = -1;
			if (num != 0)
			{
				result = 100 * mIntegerStatisticsArray[20] / num;
			}
			return result;
		}

		public static int GetHighestStatistic(int value1, int value2)
		{
			if (value1 <= value2)
			{
				return value2;
			}
			return value1;
		}

		public static int GetLowestStatistic(int value1, int value2)
		{
			if (value1 >= value2)
			{
				return value2;
			}
			return value1;
		}

		public virtual int GetCurrentTetrisSpeedLevel()
		{
			return mTetrisSpeedLevel;
		}

		public virtual void IncreaseTetrisSpeedLevelIfNeeded(bool lastClearWasTetris)
		{
			if (lastClearWasTetris && mTetrisSpeedLevel != 330 && mGameMode == 1)
			{
				int num = 100;
				int statistic = GetStatistic(2);
				if (statistic == 0)
				{
					num = 130;
				}
				else if (statistic >= 1)
				{
					num = 330;
				}
				mTetrisSpeedLevel = num;
			}
		}

		public static int GetTPM(int numberOfTetriminosFallen, int timePlayed)
		{
			int num = -1;
			int num2 = timePlayed / 1000;
			if (num2 != 0)
			{
				num = 100 * numberOfTetriminosFallen * 60 / num2;
			}
			if (num > 9900)
			{
				num = 9900;
			}
			return num;
		}

		public virtual void Initialize()
		{
			mIntegerStatisticsArray = new int[29];
			mBooleanStatisticsArray = new bool[4];
			mTemporaryIntStatisticsArray = new int[2];
			mTetrisSpeedLevel = 100;
			SetStatistic(2, true);
			mCanUpdateSoftDropBioStats = false;
			mCanUpdateTotalTimeBtwnGenerationAndInputBioStat = false;
			mMatrixFillAreaLastTurn = 0;
		}

		public virtual void DoCascadeLevel(int cascadeLevel)
		{
			if (cascadeLevel != 0)
			{
				cascadeLevel--;
			}
			KeepHighestStatistic(16, cascadeLevel);
		}

		public virtual void DoBackToBackStreak(bool isBackToBackPossible)
		{
			if (!isBackToBackPossible && GetTemporaryStatistic(1) != 0)
			{
				KeepHighestStatistic((sbyte)14, (sbyte)1);
				ResetTemporaryStatistic(1);
			}
		}

		public virtual void DoSpecialMovesCheck()
		{
			if (GetTemporaryStatistic(0) > 0 && !mSpecialMoveFlag)
			{
				SetStatistic(2, false);
			}
		}

		public virtual void IncreaseStatistic(sbyte id)
		{
			IncreaseStatistic(id, 1);
		}

		public virtual void DecreaseStatistic(sbyte id)
		{
			DecreaseStatistic(id, 1);
		}

		public virtual void IncreaseTemporaryStatistic(sbyte id)
		{
			IncreaseTemporaryStatistic(id, 1);
		}

		public virtual void DecreaseTemporaryStatistic(sbyte id)
		{
			DecreaseTemporaryStatistic(id, 1);
		}
	}
}
