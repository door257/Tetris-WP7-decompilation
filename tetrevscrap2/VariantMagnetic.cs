using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantMagnetic : TetrisGame
	{
		public int mNumberOfColors;

		public sbyte mLeftMagnetColor;

		public sbyte mRightMagnetColor;

		public sbyte mLastFallingTetriminoColor;

		public sbyte mStrongerMagnetColor;

		public bool mBlueLateralCheckNeeded;

		public bool mRedLateralCheckNeeded;

		public Component mLeftMagnet;

		public Component mRightMagnet;

		public VariantMagnetic(GameParameter gameParameter)
			: base(gameParameter)
		{
			mNumberOfColors = -1;
			mLeftMagnetColor = -1;
			mRightMagnetColor = -1;
			mLastFallingTetriminoColor = 6;
			mStrongerMagnetColor = 6;
			mPackageId = 851994;
			ResetLateralGravityCheckFlags();
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 6;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 82;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 83;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 85;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			mAnimator.LoadSingleAnimation(package, 15, 30);
			mNumberOfColors = mGameParameter.GetFromPackage(package, 2, 1);
			GetGameParameters(2);
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			SetMagnetColors();
		}

		public override void InitializeComponents(GameController gameControler)
		{
			base.InitializeComponents(gameControler);
			InitializeMagnets();
		}

		public override void Unload()
		{
			if (mLeftMagnet != null)
			{
				mLayerComponent.Detach(mLeftMagnet);
				mLeftMagnet = null;
			}
			if (mRightMagnet != null)
			{
				mLayerComponent.Detach(mRightMagnet);
				mRightMagnet = null;
			}
			if (mAnimator.IsValid(15))
			{
				mAnimator.UnloadSingleAnimation(15);
			}
			base.Unload();
		}

		public override void OnLinesCleared()
		{
			ResetLateralGravityCheckFlags();
			base.OnLinesCleared();
		}

		public override void OnInitializeFallingTetrimino()
		{
			sbyte currentAspect = mFallingTetrimino.GetRootMino().GetCurrentAspect();
			if (currentAspect != 3)
			{
				mLastFallingTetriminoColor = currentAspect;
			}
			mStrongerMagnetColor = mLastFallingTetriminoColor;
			ResetLateralGravityCheckFlags();
			base.OnInitializeFallingTetrimino();
		}

		public override void OnInitializeNewTetrimino(Tetrimino tetrimino)
		{
			int num = GameRandom.Random(0, mNumberOfColors);
			sbyte allMinoAspect = -1;
			switch (num)
			{
			case 0:
				allMinoAspect = 1;
				break;
			case 1:
				allMinoAspect = 6;
				break;
			case 2:
				allMinoAspect = 3;
				break;
			}
			tetrimino.SetAllMinoAspect(allMinoAspect);
		}

		public override void OnEntryToStateApplyingSpecifics()
		{
			if (!IsLateralGravityOver() && mLastFallingTetriminoColor != 3)
			{
				SetGravityUpdateNeeded(true);
			}
		}

		public override bool ApplyGravity()
		{
			bool flag = false;
			bool flag2 = false;
			if (!IsLateralGravityOver())
			{
				flag2 = mWell.UpdateTetriminoPos(2);
				if (mStrongerMagnetColor == 1)
				{
					mBlueLateralCheckNeeded = flag2;
					mStrongerMagnetColor = 6;
				}
				else if (mStrongerMagnetColor == 6)
				{
					mRedLateralCheckNeeded = flag2;
					mStrongerMagnetColor = 1;
				}
			}
			if (IsLateralGravityOver())
			{
				flag = base.ApplyGravity();
				if (flag)
				{
					ResetLateralGravityCheckFlags();
				}
			}
			return !IsLateralGravityOver() || flag;
		}

		public override int GetGravityDirection(Tetrimino tetrimino, int updateType)
		{
			if (updateType == 2)
			{
				sbyte currentAspect = tetrimino.GetRootMino().GetCurrentAspect();
				if (currentAspect == mStrongerMagnetColor)
				{
					if (currentAspect == mLeftMagnetColor)
					{
						return 3;
					}
					if (currentAspect == mRightMagnetColor)
					{
						return 1;
					}
				}
				return 0;
			}
			return base.GetGravityDirection(tetrimino, updateType);
		}

		public override sbyte GetSoftDropTrailAspect(Tetrimino tetrimino)
		{
			return tetrimino.GetRootMino().GetCurrentAspect();
		}

		public virtual void InitializeMagnets()
		{
			Package package = mMetaPackage.GetPackage();
			Component component = EntryPoint.GetComponent(package, 32);
			Component component2 = EntryPoint.GetComponent(package, 33);
			mLeftMagnet = component2;
			mRightMagnet = component;
			short rectTop = mLeftMagnet.GetRectTop();
			mLeftMagnet.SetTopLeft(82, rectTop);
			mRightMagnet.SetTopLeft(394, rectTop);
			mLayerComponent.Attach(mLeftMagnet, 7);
			mLayerComponent.Attach(mRightMagnet, 7);
			mAnimator.StartGameAnimation(15);
		}

		public virtual void SetMagnetColors()
		{
			mLeftMagnetColor = 6;
			mRightMagnetColor = 1;
		}

		public virtual void ResetLateralGravityCheckFlags()
		{
			mRedLateralCheckNeeded = true;
			mBlueLateralCheckNeeded = true;
		}

		public virtual bool IsLateralGravityOver()
		{
			if (!mBlueLateralCheckNeeded)
			{
				return !mRedLateralCheckNeeded;
			}
			return false;
		}
	}
}
