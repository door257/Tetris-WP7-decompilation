namespace ca.jamdat.tetrisrevolution
{
	public class StateClearingLines : GameState
	{
		public bool mLineClearAnimHasStarted;

		public StateClearingLines(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 6;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			TetrisGame tetrisGame = mGame;
			if (tetrisGame.IsLineClearActive() && tetrisGame.GetClearedLineCount() > 0)
			{
				if (mLineClearAnimHasStarted)
				{
					if (animator.IsValid(11))
					{
						if (animator.IsOver(11))
						{
							animator.Stop(11);
							SetNextGameState(7);
						}
					}
					else
					{
						SetNextGameState(7);
					}
				}
				else
				{
					Well well = mGame.GetWell();
					well.ClearLines();
					tetrisGame.PrepareLineClearAnimation();
					tetrisGame.IncreaseTetrisSpeedLevelIfNeeded();
					int currentTetrisSpeedLevel = tetrisGame.GetCurrentTetrisSpeedLevel();
					animator.StartGameAnimation(11, 1, currentTetrisSpeedLevel);
					animator.StartGameAnimation(22, 1, currentTetrisSpeedLevel);
					mLineClearAnimHasStarted = true;
					tetrisGame.LineClearResult();
				}
			}
			else if (!tetrisGame.IsDoingAnimation())
			{
				SetNextGameState(7);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnExit()
		{
			TetrisGame tetrisGame = mGame;
			tetrisGame.UpdateSpecialGameEvent();
			if (tetrisGame.IsLineClearActive())
			{
				tetrisGame.UpdateGoal();
				tetrisGame.UpdateScore();
			}
			bool flag = tetrisGame.CanDisplayFeedback();
			int specialGameEvent = tetrisGame.GetSpecialGameEvent();
			if (specialGameEvent != -1 && flag)
			{
				tetrisGame.GetGameController().PrepareFeedbackDisplay(specialGameEvent);
			}
			tetrisGame.UpdateLevel();
			if (flag)
			{
				tetrisGame.GetGameController().UpdateFeedbackDisplay();
			}
			if (tetrisGame.GetWell().IsEmpty())
			{
				tetrisGame.GetGameStatistics().IncreaseStatistic(12);
			}
			if (flag)
			{
				tetrisGame.GetGameController().DisplayFeedbackDisplay();
			}
			if (flag && tetrisGame.IsThereTSpin())
			{
				GameApp.Get().GetAnimator().StartGameAnimation(8);
			}
			mLineClearAnimHasStarted = false;
			if (GameApp.Get().GetAnimator().IsValid(11))
			{
				tetrisGame.CleanLineClearAnim();
			}
			if (tetrisGame.IsLineClearActive() && tetrisGame.GetClearedLineCount() > 0)
			{
				tetrisGame.GetWell().GetMinoToDestroyList().ReleaseAllMinos();
			}
			tetrisGame.ResetSpecialGameEvent();
			tetrisGame.SetIsThereTSpin(false);
			tetrisGame.SetIsThereBackToBack(false);
			tetrisGame.OnExitClearingLinesState();
		}

		public override bool ResumeInCountdownState()
		{
			return true;
		}
	}
}
