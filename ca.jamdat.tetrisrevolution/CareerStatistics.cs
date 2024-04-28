namespace ca.jamdat.tetrisrevolution
{
	public class CareerStatistics
	{
		public const sbyte careerTotalLineClears = 0;

		public const sbyte careerTotalTetrises = 1;

		public const sbyte careerTotalTriples = 2;

		public const sbyte careerTotalDoubles = 3;

		public const sbyte careerTotalSingles = 4;

		public const sbyte careerTotalTSpinMini = 5;

		public const sbyte careerTotalTSpin = 6;

		public const sbyte careerTotalTSpinMiniSingle = 7;

		public const sbyte careerTotalTSpinSingle = 8;

		public const sbyte careerTotalTSpinDouble = 9;

		public const sbyte careerTotalTSpinTriple = 10;

		public const sbyte careerTotalBravo = 11;

		public const sbyte careerTotalBackToBack = 12;

		public const sbyte careerLongestBackToBackStreak = 13;

		public const sbyte careerHighestLineClearsPayoff = 14;

		public const sbyte careerTotalTetriminosFallen = 15;

		public const sbyte careerTimePlayed = 16;

		public const sbyte careerTotalPoints = 17;

		public const sbyte careerHighestAverageTPM = 18;

		public const sbyte careerTotalMarathonGamesPlayed = 19;

		public const sbyte careerTotalMarathonGamesWon = 20;

		public const sbyte careerMarathonHighScore = 21;

		public const sbyte careerMarathonHighestLevel = 22;

		public const sbyte careerMarathonFastestTime = 23;

		public const sbyte careerTotalCascadeLineClears = 24;

		public const sbyte careerLongestCascadeLevel = 25;

		public const sbyte careerTotalTop40GamesPlayed = 26;

		public const sbyte careerTotalTop40GamesWon = 27;

		public const sbyte careerHighestAveragePointsPerLineClears = 28;

		public const sbyte careerStatisticsCount = 29;

		public const sbyte careerStatisticsLastStandardItem = 13;

		public const sbyte careerGameTypeHighestScore = 0;

		public const sbyte careerGameTypeFastestTime = 1;

		public const sbyte careerGameTypeHighestAverageTPM = 2;

		public const sbyte careerGameTypeStatisticsCount = 3;

		public const sbyte careerStatisticsArrayLength = 65;

		public bool[] mNewBestsArray;

		public int[] mCareerStatisticsArray;

		public CareerStatistics()
		{
			mCareerStatisticsArray = new int[65];
			mNewBestsArray = new bool[3];
		}

		public virtual void destruct()
		{
			mCareerStatisticsArray = null;
			mNewBestsArray = null;
		}

		public virtual int GetStatistic(sbyte id)
		{
			return mCareerStatisticsArray[id];
		}

		public virtual int GetStatistic(int gameType, sbyte id)
		{
			return mCareerStatisticsArray[GetStatisticsIdx(gameType, id)];
		}

		public virtual void Update(GameStatistics gameStatistics, bool isMarathonMode)
		{
			int gameVariant = gameStatistics.GetGameVariant();
			int statistic = gameStatistics.GetStatistic(20);
			int averageTPM = gameStatistics.GetAverageTPM();
			int statistic2 = gameStatistics.GetStatistic(19);
			if (isMarathonMode)
			{
				if (mCareerStatisticsArray[21] < statistic)
				{
					mCareerStatisticsArray[21] = statistic;
				}
				if (gameStatistics.IsStatistic(1) || gameStatistics.IsStatistic(0))
				{
					int num = gameStatistics.GetGameLevel() - 1;
					if (gameStatistics.IsStatistic(0))
					{
						num = 15;
					}
					if (mCareerStatisticsArray[22] < num)
					{
						mCareerStatisticsArray[22] = num;
					}
				}
				mCareerStatisticsArray[19]++;
				if (gameStatistics.IsStatistic(0))
				{
					mCareerStatisticsArray[20]++;
					if (mCareerStatisticsArray[18] < averageTPM)
					{
						mCareerStatisticsArray[18] = averageTPM;
					}
				}
				int num2 = mCareerStatisticsArray[23];
				if (num2 == 0 || num2 > statistic2)
				{
					mCareerStatisticsArray[23] = statistic2;
				}
			}
			else
			{
				if (mCareerStatisticsArray[GetStatisticsIdx(gameVariant, 0)] < statistic)
				{
					mNewBestsArray[0] = true;
				}
				if (gameStatistics.IsStatistic(0))
				{
					int num3 = mCareerStatisticsArray[GetStatisticsIdx(gameVariant, 1)];
					if (num3 == 0 || num3 > statistic2)
					{
						mNewBestsArray[1] = true;
					}
					if (mCareerStatisticsArray[GetStatisticsIdx(gameVariant, 2)] < averageTPM)
					{
						mNewBestsArray[2] = true;
					}
				}
				mCareerStatisticsArray[26]++;
				mCareerStatisticsArray[24] += gameStatistics.GetStatistic(15);
				mCareerStatisticsArray[25] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[25], gameStatistics.GetStatistic(16));
				int statisticsIdx = GetStatisticsIdx(gameVariant, 0);
				mCareerStatisticsArray[statisticsIdx] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[statisticsIdx], statistic);
				if (gameStatistics.IsStatistic(0))
				{
					mCareerStatisticsArray[27]++;
					mCareerStatisticsArray[18] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[18], averageTPM);
					statisticsIdx = GetStatisticsIdx(gameVariant, 1);
					if (mCareerStatisticsArray[statisticsIdx] == 0)
					{
						mCareerStatisticsArray[statisticsIdx] = statistic2;
					}
					else
					{
						mCareerStatisticsArray[statisticsIdx] = GameStatistics.GetLowestStatistic(mCareerStatisticsArray[statisticsIdx], statistic2);
					}
					statisticsIdx = GetStatisticsIdx(gameVariant, 2);
					mCareerStatisticsArray[statisticsIdx] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[statisticsIdx], averageTPM);
				}
				mCareerStatisticsArray[28] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[28], gameStatistics.GetAveragePointsPerLineClears());
			}
			mCareerStatisticsArray[0] += gameStatistics.GetStatistic(0);
			mCareerStatisticsArray[1] += gameStatistics.GetStatistic(2);
			mCareerStatisticsArray[2] += gameStatistics.GetStatistic(3);
			mCareerStatisticsArray[3] += gameStatistics.GetStatistic(4);
			mCareerStatisticsArray[4] += gameStatistics.GetStatistic(5);
			mCareerStatisticsArray[5] += gameStatistics.GetStatistic(6);
			mCareerStatisticsArray[6] += gameStatistics.GetStatistic(7);
			mCareerStatisticsArray[7] += gameStatistics.GetStatistic(8);
			mCareerStatisticsArray[8] += gameStatistics.GetStatistic(9);
			mCareerStatisticsArray[9] += gameStatistics.GetStatistic(10);
			mCareerStatisticsArray[10] += gameStatistics.GetStatistic(11);
			mCareerStatisticsArray[11] += gameStatistics.GetStatistic(12);
			mCareerStatisticsArray[12] += gameStatistics.GetStatistic(13);
			mCareerStatisticsArray[13] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[13], gameStatistics.GetStatistic(14));
			mCareerStatisticsArray[14] = GameStatistics.GetHighestStatistic(mCareerStatisticsArray[14], gameStatistics.GetStatistic(17));
			mCareerStatisticsArray[15] += gameStatistics.GetStatistic(18);
			mCareerStatisticsArray[16] += gameStatistics.GetStatistic(19);
			mCareerStatisticsArray[17] += gameStatistics.GetStatistic(20);
		}

		public virtual void Reset()
		{
			for (int i = 0; i < mCareerStatisticsArray.Length; i++)
			{
				mCareerStatisticsArray[i] = 0;
			}
			for (int j = 0; j < mNewBestsArray.Length; j++)
			{
				mNewBestsArray[j] = false;
			}
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
			if (fileStream.HasValidData())
			{
				for (int i = 0; i < 65; i++)
				{
					mCareerStatisticsArray[i] = fileStream.ReadLong();
				}
			}
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 65; i++)
			{
				fileStream.WriteLong(mCareerStatisticsArray[i]);
			}
			fileStream.SetValidDataFlag(true);
		}

		public virtual bool IsNewBests()
		{
			bool result = false;
			for (int i = 0; i < 3; i++)
			{
				if (mNewBestsArray[i])
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public virtual bool PopNewBestType()
		{
			int num = 0;
			for (num = 0; num < 3; num++)
			{
				if (mNewBestsArray[num])
				{
					mNewBestsArray[num] = false;
					break;
				}
			}
			return IsNewBests();
		}

		public virtual sbyte GetNewBestType()
		{
			int num = 0;
			for (num = 0; num < 3 && !mNewBestsArray[num]; num++)
			{
			}
			return (sbyte)num;
		}

		public virtual void MaxAllCareerFeatStats()
		{
			mCareerStatisticsArray[0] = 1000;
			mCareerStatisticsArray[1] = 100;
			mCareerStatisticsArray[24] = 50;
			mCareerStatisticsArray[6] = 25;
			mCareerStatisticsArray[16] = 5400000;
		}

		public virtual int GetCareerTop40SuccessRatio()
		{
			int num = mCareerStatisticsArray[26];
			int result = -1;
			if (num != 0)
			{
				result = 10000 * mCareerStatisticsArray[27] / num;
			}
			return result;
		}

		public static int GetStatisticsIdx(int gameType, sbyte id)
		{
			return 29 + gameType * 3 + id;
		}

		public static CareerStatistics[] InstArrayCareerStatistics(int size)
		{
			CareerStatistics[] array = new CareerStatistics[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CareerStatistics();
			}
			return array;
		}

		public static CareerStatistics[][] InstArrayCareerStatistics(int size1, int size2)
		{
			CareerStatistics[][] array = new CareerStatistics[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CareerStatistics[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CareerStatistics();
				}
			}
			return array;
		}

		public static CareerStatistics[][][] InstArrayCareerStatistics(int size1, int size2, int size3)
		{
			CareerStatistics[][][] array = new CareerStatistics[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CareerStatistics[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CareerStatistics[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CareerStatistics();
					}
				}
			}
			return array;
		}
	}
}
