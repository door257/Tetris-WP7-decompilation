namespace ca.jamdat.tetrisrevolution
{
	public class StateCountdown : GameState
	{
		public bool mHasShownCountdown;

		public StateCountdown(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 2;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (!mHasShownCountdown)
			{
				mGame.OnStartCountdown();
				mHasShownCountdown = true;
			}
			if (mGame.IsCountdownOver())
			{
				if (mNextState == -1)
				{
					mNextState = 3;
				}
				SetNextGameState(mNextState);
				base.OnTime(totalTimeMs, deltaTimeMs);
			}
		}

		public override void OnExit()
		{
			mGame.OnStopCountdown();
			mGame.SetTetriminoInWellVisible(true);
			mHasShownCountdown = false;
		}

		public virtual void OnPause()
		{
			mGame.OnStopCountdown();
		}
	}
}
