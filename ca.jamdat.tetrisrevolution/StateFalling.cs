namespace ca.jamdat.tetrisrevolution
{
	public class StateFalling : GameState
	{
		public StateFalling(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			TetrisGame tetrisGame = mGame;
			tetrisGame.UpdateNextMoveTime(-deltaTimeMs);
			Tetrimino fallingTetrimino = tetrisGame.GetFallingTetrimino();
			bool flag = tetrisGame.GetNextMoveTime() <= 0 && fallingTetrimino.CanMove(0, 1);
			if (flag)
			{
				tetrisGame.ShowShadowTrailUnderFallingTetrimino(false);
			}
			while (tetrisGame.GetNextMoveTime() <= 0 && fallingTetrimino.CanMove(0, 1))
			{
				fallingTetrimino.Move(0, 1, false);
				tetrisGame.OnTetriminoFall();
				if (tetrisGame.GetLastFeltRow() < fallingTetrimino.GetCoreMatrixPosY())
				{
					tetrisGame.SetLastFeltRow(fallingTetrimino.GetCoreMatrixPosY());
					tetrisGame.ResetAllLockDownDelays();
				}
				tetrisGame.UpdateNextMoveTime(tetrisGame.GetNextDropFallRate());
			}
			if (flag)
			{
				fallingTetrimino.Move(0, 0, true);
				tetrisGame.ShowShadowTrailUnderFallingTetrimino(true);
			}
			if (!fallingTetrimino.CanMove(0, 1))
			{
				SetNextGameState(5);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override sbyte GetID()
		{
			return 4;
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}
	}
}
