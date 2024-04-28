using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantTreadmill : TetrisGame
	{
		public int mTreadmillSpeed;

		public int mTimeAcc;

		public int mMatrixCurrentShift;

		public Viewport mTreadMillMeter;

		public bool mOffsetMeter;

		public VariantTreadmill(GameParameter gameParameter)
			: base(gameParameter)
		{
			mPackageId = 688149;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 1;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 62;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 63;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 65;
		}

		public override bool IsGravityEnabled()
		{
			return false;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(2);
			mTreadmillSpeed = mGameParameter.GetFromPackage(package, 2, 1);
			mTreadMillMeter = EntryPoint.GetViewport(package, 30);
		}

		public override void OnModeEndTurn()
		{
			mTimeAcc = 0;
			mMatrixCurrentShift = 0;
			base.OnModeEndTurn();
		}

		public override void InitializeComponents(GameController gameController)
		{
			base.InitializeComponents(gameController);
			mLayerComponent.Attach(mTreadMillMeter, 7);
		}

		public override void Unload()
		{
			mTreadMillMeter.SetViewport(null);
			base.Unload();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			sbyte currentStateID = GetCurrentStateID();
			if (currentStateID == 9)
			{
				mTimeAcc += deltaTimeMs;
				bool flag = mOffsetMeter;
				while (mTimeAcc >= 100 && mMatrixCurrentShift < mTreadmillSpeed)
				{
					mTimeAcc -= 100;
					mWell.ShiftMatrix(1);
					mMatrixCurrentShift++;
					mOffsetMeter = !mOffsetMeter;
				}
				if (mOffsetMeter != flag)
				{
					short left = (short)(mOffsetMeter ? 116 : 85);
					short rectTop = mTreadMillMeter.GetRectTop();
					mTreadMillMeter.SetTopLeft(left, rectTop);
				}
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override bool IsDoingAnimation()
		{
			sbyte currentStateID = GetCurrentStateID();
			if (currentStateID != 9 || mMatrixCurrentShift == mTreadmillSpeed)
			{
				return base.IsDoingAnimation();
			}
			return true;
		}
	}
}
