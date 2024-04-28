namespace ca.jamdat.tetrisrevolution
{
	public class StateEndingTurn : GameState
	{
		public int mCleanListCountdown;

		public StateEndingTurn(TetrisGame game)
			: base(game)
		{
			mCleanListCountdown = 10;
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 9;
		}

		public override void GoToNextGameState()
		{
			TetrisGame tetrisGame = mGame;
			tetrisGame.OnStateEndTurn();
			if (tetrisGame.GetGameOverType() == 0)
			{
				SetNextGameState(3);
			}
			else
			{
				SetNextGameState(10);
			}
		}

		public override void OnEntry()
		{
			base.OnEntry();
			mCleanListCountdown--;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (!mGame.IsDoingAnimation())
			{
				GoToNextGameState();
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnExit()
		{
			if (mCleanListCountdown == 0)
			{
				mGame.GetWell().CleanUpLists();
				mCleanListCountdown = 10;
			}
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}
	}
}
