using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantLimbo : TetrisGame
	{
		public int mMaximumHeight;

		public int mBarFallrate;

		public int mRowsPerLockdown;

		public int mRowsPerHardDrop;

		public int mRowsPerLineClear;

		public int mBarFallCounter;

		public Viewport mLineViewport;

		public int mCurrentBarHeight;

		public VariantLimbo(GameParameter gameParameter)
			: base(gameParameter)
		{
			mMaximumHeight = -1;
			mBarFallrate = -1;
			mRowsPerLockdown = -1;
			mRowsPerHardDrop = -1;
			mRowsPerLineClear = -1;
			mBarFallCounter = -1;
			mCurrentBarHeight = -1;
			mPackageId = 819225;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 5;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 78;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 79;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 81;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(6);
			mMaximumHeight = mGameParameter.GetFromPackage(package, 6, 1);
			mRowsPerLockdown = mGameParameter.GetFromPackage(package, 6, 2);
			mRowsPerHardDrop = mGameParameter.GetFromPackage(package, 6, 3);
			mRowsPerLineClear = mGameParameter.GetFromPackage(package, 6, 4);
			int fromPackage = mGameParameter.GetFromPackage(package, 6, 5);
			mBarFallrate = GetFallRateForSpeed(fromPackage);
			mBarFallCounter = mBarFallrate;
			mLineViewport = Viewport.Cast(package.GetEntryPoint(90), null);
			GameApp.Get().GetAnimator().LoadSingleAnimation(package, 14, 91);
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			mCurrentBarHeight = GetMaximumHeight();
		}

		public override void InitializeComponents(GameController gameController)
		{
			base.InitializeComponents(gameController);
			mLayerComponent.Attach(mLineViewport, 7);
			UpdateBarPosition();
			GameApp gameApp = GameApp.Get();
			gameApp.GetAnimator().StartGameAnimation(14);
			mAnimationManager.CreateCustomTimerSequence(21, 1950, 200);
		}

		public override void Unload()
		{
			if (mLineViewport != null)
			{
				mLineViewport.SetViewport(null);
				mLineViewport = null;
			}
			if (mAnimationManager != null)
			{
				mAnimationManager.ReleaseCustomTimerSequence(21);
			}
			if (mAnimator.IsValid(14))
			{
				mAnimator.UnloadSingleAnimation(14);
			}
			base.Unload();
		}

		public override bool IsDoingAnimation()
		{
			if (!mAnimator.IsPlaying(21))
			{
				return base.IsDoingAnimation();
			}
			return true;
		}

		public override void OnGameOver()
		{
			MetaPackage package = GameLibrary.GetPackage(1081377);
			Package package2 = package.GetPackage();
			KeyFrameSequence keyFrameSequence = null;
			keyFrameSequence = KeyFrameSequence.Cast(package2.GetEntryPoint(0), null);
			sbyte b = 18;
			FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(b);
			bool flag = false;
			for (int num = mCurrentBarHeight; num >= 0; num--)
			{
				if (num >= 20)
				{
					bool flag2 = false;
					WellLine line = mWell.GetLine(num);
					for (int i = 0; i < 10; i++)
					{
						if (line.IsThereLockedMino(i))
						{
							Mino lockedMino = line.GetLockedMino(i);
							lockedMino.SetCurrentAspect(b, bitmapForMinoSpriteAspect);
							mAnimationManager.RegisterMinoSpriteFrameIndexController(21, lockedMino.GetMinoSprite(), keyFrameSequence, 0, 1950);
							flag = true;
							flag2 = true;
						}
					}
					if (!flag2)
					{
						break;
					}
				}
			}
			GameLibrary.ReleasePackage(package);
			if (flag)
			{
				mAnimator.StartGameAnimation(21);
			}
			base.OnGameOver();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			if (CanMoveFallingTetrimino())
			{
				mBarFallCounter -= deltaTimeMs;
				if (mBarFallCounter <= 0 && mCurrentBarHeight < 39)
				{
					mCurrentBarHeight++;
					UpdateBarPosition();
					mBarFallCounter = mBarFallrate;
				}
			}
			if (mAnimator.IsOver(21))
			{
				mAnimator.Stop(21);
				mAnimationManager.UnregisterAnimControllers(21);
			}
		}

		public override void OnModeEndTurn()
		{
			UpdateBarPosition();
			CheckHasWon();
			if (IsLoosingGame())
			{
				mGameOverType = 7;
			}
			base.OnModeEndTurn();
		}

		public override void OnHardDropDone()
		{
			base.OnHardDropDone();
			UpdateBarHeight(mRowsPerHardDrop);
		}

		public override void OnTetriminoLock()
		{
			base.OnTetriminoLock();
			UpdateBarHeight(mRowsPerLockdown);
		}

		public override void OnLinesCleared()
		{
			UpdateBarHeight(mRowsPerLineClear * GetClearedLineCount());
			base.OnLinesCleared();
		}

		public override void CheckHasWon()
		{
			base.CheckHasWon();
			mHasWon = !IsLoosingGame() && mHasWon;
		}

		public virtual bool IsLoosingGame()
		{
			WellLine line = mWell.GetLine(mCurrentBarHeight);
			for (int i = 0; i < 10; i++)
			{
				if (line.IsThereLockedMino(i))
				{
					return true;
				}
			}
			return false;
		}

		public virtual int GetMaximumHeight()
		{
			return mMaximumHeight;
		}

		public virtual void UpdateBarPosition()
		{
			mLineViewport.SetTopLeft(82, (short)(104 + (mCurrentBarHeight - 20 + 1) * 31));
		}

		public virtual void UpdateBarHeight(int delta)
		{
			if (delta > 0)
			{
				mCurrentBarHeight -= delta;
				mBarFallCounter = mBarFallrate;
				if (mCurrentBarHeight < GetMaximumHeight())
				{
					mCurrentBarHeight = GetMaximumHeight();
				}
			}
		}
	}
}
