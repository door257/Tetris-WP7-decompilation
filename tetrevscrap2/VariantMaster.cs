namespace ca.jamdat.tetrisrevolution
{
	public class VariantMaster : TetrisGame
	{
		public VariantMaster(GameParameter gameParameter)
			: base(gameParameter)
		{
			mCanSoftDrop = false;
			mPackageId = 1015839;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 11;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 102;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 103;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 105;
		}

		public override bool IsGravityEnabled()
		{
			return false;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			GetGameParameters(1);
		}

		public override void GetGameParameters(int numberOfGameParams)
		{
			base.GetGameParameters(numberOfGameParams);
			if (GameApp.Get().GetReplay().IsPlaying())
			{
				InitSpeed(30);
			}
			else
			{
				InitSpeed(25);
			}
		}

		public override int ValidateFallSpeed(int nextFallTimeMs)
		{
			return nextFallTimeMs;
		}

		public override void ResetLockDownDelay()
		{
			if (GameApp.Get().GetReplay().IsPlaying())
			{
				mLockDownDelayMs = 500;
			}
			else
			{
				mLockDownDelayMs = 600;
			}
		}
	}
}
