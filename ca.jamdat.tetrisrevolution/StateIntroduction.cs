namespace ca.jamdat.tetrisrevolution
{
	public class StateIntroduction : GameState
	{
		public bool mIsIntroductionPaused;

		public StateIntroduction(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 1;
		}

		public override void OnEntry()
		{
			mGame.OnStartIntroduction();
			mIsIntroductionPaused = false;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mIsIntroductionPaused)
			{
				mGame.OnStartIntroduction();
				mIsIntroductionPaused = false;
			}
			mGame.OnTimeStateIntroduction();
			if (mGame.IsIntroductionOver())
			{
				SetNextGameState(2);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnExit()
		{
			mGame.OnStopIntroduction();
		}

		public virtual void OnPause()
		{
		}

		public virtual bool IsIntroductionPaused()
		{
			return mIsIntroductionPaused;
		}
	}
}
