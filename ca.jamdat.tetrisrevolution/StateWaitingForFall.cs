namespace ca.jamdat.tetrisrevolution
{
	public class StateWaitingForFall : GameState
	{
		public StateWaitingForFall(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 3;
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}

		public override void OnEntry()
		{
			base.OnEntry();
			TetrisGame tetrisGame = mGame;
			tetrisGame.OnEntryToStateWaitingForFall();
			tetrisGame.GetNextFallingTetrimino();
			if (tetrisGame.GetGameOverType() == 0)
			{
				tetrisGame.EvaluateAndSetBlockOut();
			}
			if (tetrisGame.GetGameOverType() == 0)
			{
				tetrisGame.OnNewFallingTetrimino();
				tetrisGame.EvaluateCanHold();
				tetrisGame.GetGameController().UpdateNextPiece(true);
				tetrisGame.OnStartingNewTurn();
				tetrisGame.ResetCascadeLevel();
				SetNextGameState(4);
			}
			else
			{
				SetNextGameState(10);
			}
		}

		public override void OnExit()
		{
		}
	}
}
