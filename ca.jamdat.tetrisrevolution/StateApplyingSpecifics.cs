namespace ca.jamdat.tetrisrevolution
{
	public class StateApplyingSpecifics : GameState
	{
		public int mAccumulatedTimeMs;

		public StateApplyingSpecifics(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 8;
		}

		public override void GoToNextGameState()
		{
			if (mGame.IsGravityUpdateNeeded())
			{
				SetNextGameState(6);
			}
			else
			{
				SetNextGameState(9);
			}
		}

		public override void OnEntry()
		{
			base.OnEntry();
			mGame.OnEntryToStateApplyingSpecifics();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			mAccumulatedTimeMs += deltaTimeMs;
			if (mGame.CanApplySpecific(mAccumulatedTimeMs))
			{
				ApplySpecifics();
			}
			if (IsStateSpecificsOver())
			{
				GoToNextGameState();
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public virtual void ApplySpecifics()
		{
			mGame.OnTimeStateSpecifics();
			mAccumulatedTimeMs = 0;
		}

		public virtual bool IsStateSpecificsOver()
		{
			if (mGame.IsStateSpecificsOver())
			{
				return !mGame.IsDoingAnimation();
			}
			return false;
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}
	}
}
