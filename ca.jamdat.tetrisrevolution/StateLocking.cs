namespace ca.jamdat.tetrisrevolution
{
	public class StateLocking : GameState
	{
		public StateLocking(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 5;
		}

		public override void OnEntry()
		{
			base.OnEntry();
			mGame.ResetNextMoveTime();
			mGame.UpdateNextMoveTime(mGame.GetNextDropFallRate());
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			TetrisGame tetrisGame = mGame;
			sbyte nextGameState = -1;
			tetrisGame.UpdateLockDownDelay(-deltaTimeMs);
			if (!tetrisGame.GetFallingTetrimino().IsLocked() && tetrisGame.GetFallingTetrimino().CanMove(0, 1))
			{
				nextGameState = 4;
			}
			else if (tetrisGame.IsHardDropping() || tetrisGame.HasLockDownDelayExpired() || tetrisGame.IsExtendedMoveExpired())
			{
				if (tetrisGame.IsFallingTetriminoLockable())
				{
					LockTetrimino();
				}
				nextGameState = 6;
			}
			if (!tetrisGame.IsDoingAnimation())
			{
				SetNextGameState(nextGameState);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}

		public virtual void LockTetrimino()
		{
			mGame.LockFallingTetrimino();
		}
	}
}
