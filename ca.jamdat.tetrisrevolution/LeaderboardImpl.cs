using System;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class LeaderboardImpl
	{
		public const int LEADERBOARD_START_ID = 100;

		public const int LEADERBOARDS_COUNT = 13;

		public const int BlobTPM = 0;

		public const int BlobLevel = 1;

		public const int BlobTime = 2;

		private object mLeaderboardLockObject = new object();

		private TetrisLeaderboard[] mLeaderboards = new TetrisLeaderboard[13];

		private static LeaderboardImpl mInstance = new LeaderboardImpl();

		public static LeaderboardImpl Get
		{
			get
			{
				return mInstance;
			}
		}

		public event EventHandler EventError;

		private LeaderboardImpl()
		{
			for (int i = 0; i != 13; i++)
			{
				mLeaderboards[i] = new TetrisLeaderboard(i);
			}
		}

		public TetrisLeaderboard GetLeaderboard(int pSelectionIndex)
		{
			if (pSelectionIndex >= 0 && pSelectionIndex < 13)
			{
				return mLeaderboards[pSelectionIndex];
			}
			return null;
		}

		public void ResetState()
		{
			for (int i = 0; i != 13; i++)
			{
				GetLeaderboard(i).ResetState();
			}
		}

		public void ResetLeaderboards()
		{
			for (int i = 0; i != 13; i++)
			{
				GetLeaderboard(i).ResetReader();
			}
		}

		public void ReadAll()
		{
			for (int i = 0; i != 13; i++)
			{
				Read(i, null, false);
			}
		}

		public void Read(int pSelectionIndex, AsyncCallback pCallback, bool pForceRead)
		{
			if (!LiveState.IsTrial)
			{
				ResetLeaderboards();
				GetLeaderboard(pSelectionIndex).Read(pCallback, pForceRead);
			}
		}

		public void Write(GameStatistics gameStats, GameScore gameScore)
		{
			if (!LiveState.IsTrial)
			{
				int leaderboardIndex = GetLeaderboardIndex(gameStats);
				GetLeaderboard(leaderboardIndex).Write(gameStats, gameScore);
			}
		}

		private int GetLeaderboardIndex(GameStatistics gameStats)
		{
			if (gameStats.mGameMode != 0)
			{
				return gameStats.GetGameVariant() + 1;
			}
			return 0;
		}

		public void OnError()
		{
			if (this.EventError != null)
			{
				this.EventError(this, null);
			}
		}
	}
}
