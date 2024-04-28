using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantScanner : TetrisGame
	{
		public int mFrequency;

		public int mOldCompletedLinesFlag;

		public EventBarIndicator mIndicator;

		public bool mScanAtEndTimeDone;

		public VariantScanner(GameParameter gameParameter)
			: base(gameParameter)
		{
			mFrequency = -1;
			mPackageId = 884763;
			mIndicator = new EventBarIndicator();
		}

		public override void destruct()
		{
			mIndicator = null;
		}

		public override int GetVariant()
		{
			return 7;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 86;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 87;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 89;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(2);
			mFrequency = mGameParameter.GetFromPackage(package, 2, 1);
			mAnimator.LoadSingleAnimation(package, 16, 30);
			mIndicator.GetEntryPoints();
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			mIndicator.Initialize(mFrequency);
			mScanAtEndTimeDone = false;
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
			mAnimationManager.CreateCustomTimerSequence(17, 753, 200);
			AttachScannerBar();
			mOldCompletedLinesFlag = 0;
			ComputeAndRegisterScannerLineControllers();
		}

		public override void Unload()
		{
			if (mAnimationManager != null)
			{
				mAnimationManager.ReleaseCustomTimerSequence(17);
			}
			ReleaseScannerBar();
			if (mAnimator.IsValid(16))
			{
				mAnimator.UnloadSingleAnimation(16);
			}
			mIndicator.Unload();
			base.Unload();
		}

		public override void OnResume()
		{
			base.OnResume();
			mIndicator.Resume();
		}

		public override void OnModeEndTurn()
		{
			base.OnModeEndTurn();
			if (mIndicator.IsFull())
			{
				mIndicator.Reset();
			}
		}

		public override void OnTetriminoLock()
		{
			mIndicator.Increment();
			if (ComputeAndRegisterScannerLineControllers())
			{
				base.OnTetriminoLock();
			}
		}

		public override void OnExitClearingLinesState()
		{
			if (mIndicator.IsFull() && mOldCompletedLinesFlag != 0)
			{
				mOldCompletedLinesFlag = 0;
				mAnimationManager.UnregisterAnimControllers(17);
			}
			base.OnExitClearingLinesState();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			sbyte currentStateID = GetCurrentStateID();
			if (!mGameTimeExpired && !mScanAtEndTimeDone && mPlayTimeMs + deltaTimeMs >= mGameParameter.GetTimeLimit())
			{
				mIndicator.OnTime(0, mFrequency);
				mScanAtEndTimeDone = true;
			}
			if (currentStateID == 6 && mIndicator.IsFull() && !mIndicator.IsDoingEvent())
			{
				mIndicator.Vanish();
				mAnimator.StartGameAnimation(16);
				if (mOldCompletedLinesFlag != 0)
				{
					mAnimator.StartGameAnimation(17);
				}
			}
			if (mAnimator.IsOver(17))
			{
				mAnimator.Stop(17);
			}
			if (mAnimator.IsOver(16))
			{
				mAnimator.Stop(16);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override bool CanTSpin()
		{
			return false;
		}

		public override bool IsDoingAnimation()
		{
			if (!mAnimator.IsPlaying(16) && !mAnimator.IsPlaying(17) && !mIndicator.IsDoingAnimation())
			{
				return base.IsDoingAnimation();
			}
			return true;
		}

		public override bool IsLineClearActive()
		{
			if (mIndicator.IsFull())
			{
				return !IsDoingAnimation();
			}
			return false;
		}

		public override bool IsTimeExpired()
		{
			if (!mIndicator.IsFull())
			{
				return base.IsTimeExpired();
			}
			return false;
		}

		public virtual bool ComputeAndRegisterScannerLineControllers()
		{
			int num = 0;
			int num2 = mWell.ComputeCompletedLineFlag();
			num = num2 ^ mOldCompletedLinesFlag;
			mOldCompletedLinesFlag = num2;
			AnimationManager animationManager = mAnimationManager;
			if (num != 0)
			{
				for (int i = mWell.GetWellTop(); i < 40; i++)
				{
					if (!Well.IsClearedLineFromFlags(num, i))
					{
						continue;
					}
					for (int j = 0; j < 10; j++)
					{
						WellLine line = mWell.GetLine(i);
						Mino lockedMino = line.GetLockedMino(j);
						sbyte currentAspect = lockedMino.GetCurrentAspect();
						if (!MinoSprite.IsAspectChange(currentAspect))
						{
							sbyte b = (sbyte)(currentAspect + 7);
							FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(b);
							lockedMino.SetCurrentAspect(b, bitmapForMinoSpriteAspect);
						}
						animationManager.RegisterScannerMinoAspectChange(lockedMino, i);
					}
				}
			}
			return num == 0;
		}

		public virtual void AttachScannerBar()
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(884763);
			Viewport viewport = EntryPoint.GetViewport(preLoadedPackage, 39);
			mLayerComponent.Attach(viewport, 6);
			viewport = EntryPoint.GetViewport(preLoadedPackage, 40);
			mLayerComponent.Attach(viewport, 1);
			viewport = EntryPoint.GetViewport(preLoadedPackage, 41);
			mLayerComponent.Attach(viewport, 1);
		}

		public virtual void ReleaseScannerBar()
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(884763);
			Viewport viewport = null;
			for (int i = 0; i < 3; i++)
			{
				viewport = EntryPoint.GetViewport(preLoadedPackage, 39 + i);
				viewport.SetViewport(null);
			}
		}
	}
}
