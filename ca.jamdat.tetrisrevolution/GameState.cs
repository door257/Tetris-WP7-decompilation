namespace ca.jamdat.tetrisrevolution
{
	public abstract class GameState
	{
		public const sbyte kStateInvalid = -1;

		public const sbyte kStateWaitingForInitialize = 0;

		public const sbyte kStateIntroduction = 1;

		public const sbyte kStateCountdown = 2;

		public const sbyte kStateWaitingForFall = 3;

		public const sbyte kStateFalling = 4;

		public const sbyte kStateLocking = 5;

		public const sbyte kStateClearingLines = 6;

		public const sbyte kStateCollapsingLines = 7;

		public const sbyte kStateApplyingSpecifics = 8;

		public const sbyte kStateEndingTurn = 9;

		public const sbyte kStateGameOver = 10;

		public TetrisGame mGame;

		public sbyte mNextState;

		public bool mSkipOnEntry;

		public GameState(TetrisGame tetrisGame)
		{
			mGame = tetrisGame;
			mNextState = -1;
		}

		public virtual void destruct()
		{
		}

		public static bool IsStateIDValid(sbyte stateID)
		{
			return stateID <= 10;
		}

		public virtual void OnEntry()
		{
		}

		public virtual void OnExit()
		{
		}

		public virtual bool OnCommand(int command)
		{
			return false;
		}

		public virtual bool OnKeyDown(int key)
		{
			return false;
		}

		public virtual bool OnKeyUp(int key)
		{
			return false;
		}

		public virtual bool OnKeyDownOrRepeat(int key)
		{
			return false;
		}

		public virtual void OnTime(int a6, int a5)
		{
			if (ReadyForNextState())
			{
				mGame.ChangeState(mNextState);
				mNextState = -1;
			}
		}

		public virtual void GoToNextGameState()
		{
		}

		public virtual bool SkipNextStateEntry()
		{
			return mSkipOnEntry;
		}

		public virtual bool ResumeInCountdownState()
		{
			return false;
		}

		public virtual void SetSkipOnEntry(bool skipOnEntry)
		{
			mSkipOnEntry = skipOnEntry;
		}

		public abstract sbyte GetID();

		public virtual bool ReadyForNextState()
		{
			return mNextState != -1;
		}

		public virtual void SetNextGameState(sbyte nextState)
		{
			if (mNextState != 10 && mNextState != 3)
			{
				mNextState = nextState;
			}
		}
	}
}
