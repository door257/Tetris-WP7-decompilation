using System;
using System.IO;
using ca.jamdat.flight;
using Microsoft.Xna.Framework.GamerServices;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class TetrisLeaderboard
	{
		private int mID = -1;

		private AsyncCallback mCallback;

		private LeaderboardReader mReader;

		private TetrisLeaderboardState mState;

		public TetrisLeaderboard(int pIndex)
		{
			mID = 100 + pIndex;
		}

		public void ResetState()
		{
			mState = TetrisLeaderboardState.Waiting;
		}

		public LeaderboardReader GetReader()
		{
			return mReader;
		}

		public void SetReader(LeaderboardReader pReader)
		{
			ResetReader();
			mReader = pReader;
		}

		public void ResetReader()
		{
			if (mReader != null)
			{
				mReader.Dispose();
			}
			mReader = null;
		}

		public void Read(AsyncCallback pCallback, bool pForceRead)
		{
			if (mState == TetrisLeaderboardState.Waiting)
			{
				mCallback = pCallback;
				if (pForceRead)
				{
					ResetReader();
				}
				if (mReader == null)
				{
					PerformRead();
				}
				else
				{
					Callback();
				}
			}
		}

		private void PerformRead()
		{
			try
			{
				mState = TetrisLeaderboardState.Loading;
				LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreRecent, mID);
				LeaderboardReader.BeginRead(leaderboardId, LiveState.Gamer, 100, LeaderboardReadCallback, LiveState.Gamer);
			}
			catch (Exception exception)
			{
				OnError();
				FlLog.Log(exception);
			}
		}

		private void EndRead(IAsyncResult result)
		{
			LeaderboardReader leaderboardReader = LeaderboardReader.EndRead(result);
			if (leaderboardReader != null)
			{
				SetReader(leaderboardReader);
			}
			mState = TetrisLeaderboardState.Waiting;
		}

		public void Write(GameStatistics gameStats, GameScore gameScore)
		{
			LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, mID);
			LeaderboardEntry leaderboard = LiveState.Gamer.LeaderboardWriter.GetLeaderboard(leaderboardId);
			leaderboard.Rating = gameScore.GetCurrentScore();
			BinaryWriter binaryWriter = new BinaryWriter(leaderboard.Columns.GetValueStream("ScoreBlob"));
			binaryWriter.Write(new byte[3]
			{
				(byte)StatisticsFormatting.CreateStatisticString(gameStats.GetAverageTPM(), 3).ToLong(),
				(byte)gameStats.GetGameLevel(),
				(byte)gameStats.GetStatistic(19)
			});
			ResetReader();
		}

		private void Callback()
		{
			Callback(null);
		}

		private void Callback(IAsyncResult result)
		{
			if (mCallback != null)
			{
				mCallback(result);
			}
		}

		protected void LeaderboardReadCallback(IAsyncResult result)
		{
			if (result.AsyncState != null)
			{
				try
				{
					EndRead(result);
					Callback(result);
				}
				catch (Exception exception)
				{
					OnError();
					FlLog.Log(exception);
				}
			}
		}

		private void OnError()
		{
			mState = TetrisLeaderboardState.Error;
			LeaderboardImpl.Get.OnError();
		}
	}
}
