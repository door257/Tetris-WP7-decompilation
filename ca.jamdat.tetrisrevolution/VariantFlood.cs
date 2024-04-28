using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantFlood : TetrisGame
	{
		public int mNbOfLinesToAddPerFrequency;

		public int mMinNbOfHolesPerLine;

		public int mMaxNbOfHolesPerLine;

		public int mFrequency;

		public EventBarIndicator mIndicator;

		public VariantFlood(GameParameter gameParameter)
			: base(gameParameter)
		{
			mNbOfLinesToAddPerFrequency = -1;
			mMinNbOfHolesPerLine = -1;
			mMaxNbOfHolesPerLine = -1;
			mFrequency = -1;
			mPackageId = 753687;
			mIndicator = new EventBarIndicator();
		}

		public override void destruct()
		{
			mIndicator = null;
		}

		public override int GetVariant()
		{
			return 3;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 70;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 71;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 73;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(5);
			mMinNbOfHolesPerLine = mGameParameter.GetFromPackage(package, 5, 1);
			mMaxNbOfHolesPerLine = mGameParameter.GetFromPackage(package, 5, 2);
			mNbOfLinesToAddPerFrequency = mGameParameter.GetFromPackage(package, 5, 3);
			mFrequency = mGameParameter.GetFromPackage(package, 5, 4);
			mIndicator.GetEntryPoints();
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			mIndicator.Initialize(mFrequency);
		}

		public override void ReleaseGame()
		{
			mIndicator.ReleaseBar();
			base.ReleaseGame();
		}

		public override void InitializeComponents(GameController gameController)
		{
			base.InitializeComponents(gameController);
			mLayerComponent.Attach(mIndicator.GetViewport(), 7);
		}

		public override void Unload()
		{
			mIndicator.Unload();
			base.Unload();
		}

		public override bool IsStateSpecificsOver()
		{
			return !NeedToAddLines();
		}

		public override void OnStartIntroduction()
		{
			base.OnStartIntroduction();
			if (!mStateIntroduction.IsIntroductionPaused())
			{
				mLinesToAddCount += mNbOfLinesToAddPerFrequency;
			}
		}

		public override void OnTimeStateIntroduction()
		{
			AddLinesIfNeeded();
			base.OnTimeStateIntroduction();
		}

		public override bool IsIntroductionOver()
		{
			if (base.IsIntroductionOver() && !IsDoingAnimation())
			{
				return !NeedToAddLines();
			}
			return false;
		}

		public override void OnEntryToStateApplyingSpecifics()
		{
			if (mIndicator.IsFull())
			{
				mIndicator.Vanish();
			}
		}

		public override void OnTimeStateSpecifics()
		{
			AddLinesIfNeeded();
		}

		public override void CheckForLinesToAdd()
		{
			if (mIndicator.IsFull())
			{
				mIndicator.Reset();
				mLinesToAddCount = mNbOfLinesToAddPerFrequency;
			}
			if (IsReadyToAddLines())
			{
				if (TryToAddLine((short)mMinNbOfHolesPerLine, (short)mMaxNbOfHolesPerLine))
				{
					mLinesToAddCount = 0;
					SetGameOverType(2);
				}
				else if (mLinesToAddCount == 0)
				{
					SetGravityUpdateNeeded(true);
				}
			}
		}

		public override bool IsDoingAnimation()
		{
			if (!base.IsDoingAnimation())
			{
				return mIndicator.IsDoingAnimation();
			}
			return true;
		}

		public override void OnResume()
		{
			base.OnResume();
			mIndicator.Resume();
		}

		public override void OnTetriminoLock()
		{
			base.OnTetriminoLock();
			mIndicator.Increment();
		}

		public virtual void AddLinesIfNeeded()
		{
			CheckForLinesToAdd();
		}
	}
}
