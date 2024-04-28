using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ProgressionExpert : Expert
	{
		public const int localAccuracy = 10000;

		public const int convertAccuracyRatio = 100;

		public const int numberOfMiniVariation = 12;

		public const int numberOfLevel = 15;

		public const int lowLevelCompletionBonus = 50000;

		public const int midLevelCompletionBonus = 3703;

		public const int highLevelCompletionBonus = 3333;

		public int[] mProgressionVariantArray;

		public int mGameCompletion;

		public int mLastMarathonDifficulty;

		public static ProgressionExpert Get()
		{
			return (ProgressionExpert)GameApp.Get().GetExpertManager().GetExpert(0);
		}

		public ProgressionExpert()
		{
			mLastMarathonDifficulty = 0;
			mProgressionVariantArray = new int[12];
			for (int i = 0; i < mProgressionVariantArray.Length; i++)
			{
				mProgressionVariantArray[i] = -1;
			}
		}

		public override void destruct()
		{
			mProgressionVariantArray = null;
		}

		public override void Update(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			if (!GameApp.Get().GetGameSettings().IsMarathonMode())
			{
				int gameVariant = gameStatistics.GetGameVariant();
				int gameLevel = gameStatistics.GetGameLevel();
				if (gameStatistics.IsStatistic(0) && mProgressionVariantArray[gameVariant] <= gameLevel)
				{
					mProgressionVariantArray[gameVariant] = gameLevel;
					mGameCompletion = ComputeGameCompletion();
				}
			}
			else
			{
				mGameCompletion = ComputeGameCompletion();
			}
		}

		public override void Reset()
		{
			for (int i = 0; i < mProgressionVariantArray.Length; i++)
			{
				mProgressionVariantArray[i] = -1;
			}
			mLastMarathonDifficulty = 0;
			mGameCompletion = 0;
		}

		public override void Read(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 12; i++)
			{
				mProgressionVariantArray[i] = fileStream.ReadByte();
			}
			mGameCompletion = ComputeGameCompletion();
			mLastMarathonDifficulty = fileStream.ReadByte();
		}

		public override void Write(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 12; i++)
			{
				fileStream.WriteByte((sbyte)mProgressionVariantArray[i]);
			}
			fileStream.WriteByte((sbyte)mLastMarathonDifficulty);
		}

		public virtual int GetHighestLevelDone(int gameType)
		{
			return mProgressionVariantArray[gameType];
		}

		public virtual int GetGameCompletion()
		{
			return mGameCompletion;
		}

		public virtual int ComputeGameCompletion()
		{
			int num = 0;
			int fpValue = 180;
			for (int i = 0; i < 12; i++)
			{
				num += FlMath.Maximum(mProgressionVariantArray[i], 0);
			}
			num *= 85;
			F32 f = new F32(num, 16);
			F32 f2 = new F32(fpValue, 16);
			int num2 = f.Div(f2, 16).Floor(16).ToInt(16);
			int statistic = GameApp.Get().GetCareerStatistics().GetStatistic(22);
			if (statistic == 0 && num2 == 0 && num != 0)
			{
				num2 = 1;
			}
			return num2 + statistic;
		}

		public virtual void SetLastMarathonDifficultyPlayed(int lastDifficulty)
		{
			mLastMarathonDifficulty = lastDifficulty;
		}

		public virtual int GetLastMarathonDifficultyPlayed()
		{
			return mLastMarathonDifficulty;
		}

		public static ProgressionExpert[] InstArrayProgressionExpert(int size)
		{
			ProgressionExpert[] array = new ProgressionExpert[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ProgressionExpert();
			}
			return array;
		}

		public static ProgressionExpert[][] InstArrayProgressionExpert(int size1, int size2)
		{
			ProgressionExpert[][] array = new ProgressionExpert[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ProgressionExpert[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ProgressionExpert();
				}
			}
			return array;
		}

		public static ProgressionExpert[][][] InstArrayProgressionExpert(int size1, int size2, int size3)
		{
			ProgressionExpert[][][] array = new ProgressionExpert[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ProgressionExpert[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ProgressionExpert[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ProgressionExpert();
					}
				}
			}
			return array;
		}
	}
}
