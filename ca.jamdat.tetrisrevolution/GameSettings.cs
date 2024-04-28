namespace ca.jamdat.tetrisrevolution
{
	public class GameSettings
	{
		public const sbyte gameModeUndefined = -1;

		public const sbyte gameModeMarathon = 0;

		public const sbyte gameModeTop40 = 1;

		public const sbyte gameModePuzzle = 2;

		public const sbyte gameModeCount = 3;

		public const sbyte modeIdle = 0;

		public const sbyte modeReplay = 1;

		public const sbyte modeGlossaryReplay = 2;

		public const sbyte modeMasterReplay = 3;

		public const sbyte modeManual = 4;

		public const sbyte modeDemoIdleReplay = 5;

		public const sbyte modeDemoUnlockReplay = 6;

		public const sbyte modeGesture = 0;

		public const sbyte modeVirtualDPad = 1;

		public sbyte mGameMode;

		public sbyte mPlayMode;

		public int mGameVariant;

		public int mDifficulty;

		public bool mTutorialEnabled;

		public bool mHasTutorialShownOnceOverall;

		public bool mUserDoingTutorial;

		public sbyte mTouchMode;

		public bool mIsGhostEnabled;

		public int mTimeLimit;

		public int mLineLimit;

		public int mLastVariantPlayed;

		public GameSettings()
		{
			Reset();
		}

		public virtual void destruct()
		{
		}

		public virtual void Reset()
		{
			mPlayMode = 4;
			mGameMode = -1;
			mGameVariant = -1;
			mDifficulty = -1;
			mTutorialEnabled = false;
			mHasTutorialShownOnceOverall = false;
			mUserDoingTutorial = false;
			mIsGhostEnabled = true;
			mTimeLimit = 0;
			mLineLimit = -1;
			mTouchMode = 0;
			mLastVariantPlayed = 0;
		}

		public virtual void Read(FileSegmentStream inputStream)
		{
			if (inputStream.HasValidData())
			{
				mTutorialEnabled = inputStream.ReadBoolean();
				mHasTutorialShownOnceOverall = inputStream.ReadBoolean();
				mTouchMode = inputStream.ReadByte();
				mIsGhostEnabled = inputStream.ReadBoolean();
				mLastVariantPlayed = inputStream.ReadByte();
			}
		}

		public virtual void Write(FileSegmentStream outputStream)
		{
			outputStream.WriteBoolean(mTutorialEnabled);
			outputStream.WriteBoolean(mHasTutorialShownOnceOverall);
			outputStream.WriteByte(mTouchMode);
			outputStream.WriteBoolean(mIsGhostEnabled);
			outputStream.WriteByte((sbyte)mLastVariantPlayed);
			outputStream.SetValidDataFlag(true);
		}

		public virtual void SetGameMode(sbyte gameMode, int nbMiniVariations)
		{
			mGameMode = gameMode;
			GameApp.Get().GetReplay().SetGameMode(gameMode);
		}

		public virtual sbyte GetCurrentGameMode()
		{
			return mGameMode;
		}

		public virtual bool IsMarathonMode()
		{
			return mGameMode == 0;
		}

		public virtual void SetGameVariant(int gameVariant)
		{
			mGameVariant = gameVariant;
		}

		public virtual int GetGameVariant()
		{
			return mGameVariant;
		}

		public virtual void SetGameDifficulty(int difficulty)
		{
			mDifficulty = difficulty;
		}

		public virtual int GetGameDifficulty()
		{
			return mDifficulty;
		}

		public virtual void SetTutorialEnabled(bool enabled)
		{
			mTutorialEnabled = enabled;
		}

		public virtual bool IsTutorialEnabled()
		{
			return mTutorialEnabled;
		}

		public virtual bool NeedToShowTutorial()
		{
			if (!mTutorialEnabled)
			{
				return !mHasTutorialShownOnceOverall;
			}
			return true;
		}

		public virtual void SetTutorialShown()
		{
			mHasTutorialShownOnceOverall = true;
		}

		public virtual bool IsUserDoingTutorial()
		{
			return mUserDoingTutorial;
		}

		public virtual void SetUserDoingTutorial(bool userDoingTutorial)
		{
			mUserDoingTutorial = userDoingTutorial;
		}

		public virtual void SetGhostEnabled(bool enabled)
		{
			mIsGhostEnabled = enabled;
		}

		public virtual bool IsGhostEnabled()
		{
			return mIsGhostEnabled;
		}

		public virtual int GetLineLimit()
		{
			switch (mGameMode)
			{
			case 0:
				return 0;
			case 1:
				return 40;
			case 2:
				return 0;
			default:
				return 0;
			}
		}

		public virtual void SynchWithReplay(Replay replay)
		{
			SetGameMode(replay.GetGameMode());
			SetGameVariant(replay.GetGameType());
			SetGameDifficulty(replay.GetDifficulty());
			SetLineLimit(replay.GetLineLimit());
			SetTimeLimit(replay.GetTimeLimit());
		}

		public virtual sbyte GetPlayMode()
		{
			return mPlayMode;
		}

		public virtual void SetPlayMode(sbyte gameState)
		{
			mPlayMode = gameState;
		}

		public virtual void SetTimeLimit(int limit)
		{
			mTimeLimit = limit;
		}

		public virtual void SetLineLimit(int limit)
		{
			mLineLimit = limit;
		}

		public virtual bool IsInIdleMode()
		{
			return mPlayMode == 0;
		}

		public virtual sbyte GetTouchMode()
		{
			return mTouchMode;
		}

		public virtual void SetTouchMode(sbyte touchState)
		{
			mTouchMode = touchState;
		}

		public virtual bool IsTouchModeVirtualDPad()
		{
			return 1 == mTouchMode;
		}

		public virtual void SetLastVariantPlayed(int lastVariantPlayed)
		{
			mLastVariantPlayed = lastVariantPlayed;
		}

		public virtual int GetLastVariantPlayed()
		{
			return mLastVariantPlayed;
		}

		public virtual sbyte GetGameMode()
		{
			return mGameMode;
		}

		public virtual void SetGameMode(sbyte gameMode)
		{
			SetGameMode(gameMode, 1);
		}
	}
}
