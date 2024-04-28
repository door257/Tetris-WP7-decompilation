using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameParameter
	{
		public const int timeLimitIndex = 0;

		public const int lineLimitIndex = 1;

		public const int difficultyIndex = 2;

		public const int statsIndexCount = 3;

		public const int kDifficultyUndefined = -1;

		public const int kDifficulty1 = 0;

		public const int kDifficulty2 = 1;

		public const int kDifficulty3 = 2;

		public const int kDifficulty4 = 3;

		public const int kDifficulty5 = 4;

		public const int kDifficulty6 = 5;

		public const int kDifficulty7 = 6;

		public const int kDifficulty8 = 7;

		public const int kDifficulty9 = 8;

		public const int kDifficulty10 = 9;

		public const int kDifficulty11 = 10;

		public const int kDifficulty12 = 11;

		public const int kDifficulty13 = 12;

		public const int kDifficulty14 = 13;

		public const int kDifficulty15 = 14;

		public const int kDifficultyCount = 15;

		public int[] mGameParam;

		public GameParameter()
		{
			mGameParam = new int[3];
		}

		public virtual void destruct()
		{
			mGameParam = null;
		}

		public virtual void SetLineLimit(int lineLimit)
		{
			mGameParam[1] = lineLimit;
		}

		public virtual int GetLineLimit()
		{
			return mGameParam[1];
		}

		public virtual bool HasLineLimit()
		{
			return mGameParam[1] != 0;
		}

		public virtual void SetTimeLimit(int timeLimit)
		{
			mGameParam[0] = timeLimit;
		}

		public virtual int GetTimeLimit()
		{
			return mGameParam[0];
		}

		public virtual bool HasTimeLimit()
		{
			return mGameParam[0] != 0;
		}

		public virtual int GetDifficulty()
		{
			return mGameParam[2];
		}

		public virtual void SetDifficulty(int difficulty)
		{
			mGameParam[2] = difficulty;
		}

		public virtual int GetFromPackage(Package _package, int numberOfParameter, int firstEntryPointIndex)
		{
			int num = 0;
			return _package.GetEntryPoint(firstEntryPointIndex + numberOfParameter * GetDifficulty(), (int[])null);
		}
	}
}
