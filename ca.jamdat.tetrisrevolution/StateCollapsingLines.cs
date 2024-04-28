namespace ca.jamdat.tetrisrevolution
{
	public class StateCollapsingLines : GameState
	{
		public StateCollapsingLines(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 7;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			TetrisGame tetrisGame = mGame;
			if (!IsStateWaitingOnAnim())
			{
				if (tetrisGame.IsLineClearActive() && tetrisGame.GetClearedLineCount() > 0)
				{
					tetrisGame.GetWell().CollapseLines();
				}
				if (tetrisGame.IsGravityUpdateNeeded())
				{
					tetrisGame.UpdateNextMoveTime(-deltaTimeMs);
					int gravityFallRate = tetrisGame.GetGravityFallRate();
					while (tetrisGame.GetNextMoveTime() <= 0)
					{
						if (!tetrisGame.ApplyGravity())
						{
							SetNextGameState(8);
							tetrisGame.GetWell().GravityOver();
						}
						tetrisGame.UpdateNextMoveTime(gravityFallRate);
					}
				}
				else
				{
					SetNextGameState(8);
				}
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnEntry()
		{
			base.OnEntry();
			mGame.ResetNextMoveTime();
		}

		public override void OnExit()
		{
			mGame.IncreaseCascadeLevel();
		}

		public override bool ReadyForNextState()
		{
			if (base.ReadyForNextState())
			{
				return !mGame.IsGravityUpdateNeeded();
			}
			return false;
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}

		public virtual bool IsStateWaitingOnAnim()
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			if (!mGame.IsDoingAnimation() && !animator.IsStarted(22))
			{
				return animator.IsPlaying(23);
			}
			return true;
		}
	}
}
