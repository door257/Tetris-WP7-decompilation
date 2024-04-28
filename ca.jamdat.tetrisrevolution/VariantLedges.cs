using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantLedges : TetrisGame
	{
		public int mDisabledTetriminoRow;

		public int mBottomRow;

		public int mTopRow;

		public int mNumberOfLedges;

		public bool mDestroyFallingTetrimino;

		public bool mWasSoftDropping;

		public VariantLedges(GameParameter gameParameter)
			: base(gameParameter)
		{
			mBottomRow = -1;
			mTopRow = -1;
			mNumberOfLedges = -1;
			mPackageId = 786456;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 4;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 74;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 75;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 77;
		}

		public override void SetSoftDropActive(bool active)
		{
			mWasSoftDropping = active;
			if (mFallingTetrimino != null && (active || !IsFallingTetriminoDisabled() || !HasAtLeastOneFallingMinoInVisualWell()))
			{
				base.SetSoftDropActive(active);
			}
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override int GetBottomWellLimit()
		{
			return 45;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(4);
			mBottomRow = mGameParameter.GetFromPackage(package, 4, 1);
			mTopRow = mGameParameter.GetFromPackage(package, 4, 2);
			mNumberOfLedges = mGameParameter.GetFromPackage(package, 4, 3);
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			PopulatePlacementMatrix(mTopRow, mBottomRow, mNumberOfLedges, 10);
			mDisabledTetriminoRow = FlMath.Minimum(mBottomRow + 3, 38);
			mGameStatistics.SetStatistic(3, true);
		}

		public virtual bool IsFallingTetriminoDisabled()
		{
			return mFallingTetrimino.GetCoreMatrixPosY() >= mDisabledTetriminoRow;
		}

		public override bool CanRotate()
		{
			return !IsFallingTetriminoDisabled();
		}

		public override bool CanMove()
		{
			return !IsFallingTetriminoDisabled();
		}

		public override void OnEntryToStateWaitingForFall()
		{
			if (mDestroyFallingTetrimino)
			{
				mWell.GetTetriminoList().ReleaseTetrimino(mFallingTetrimino);
				mFallingTetrimino = null;
				mDestroyFallingTetrimino = false;
			}
		}

		public override void OnTetriminoFall()
		{
			if (!HasAtLeastOneFallingMinoInVisualWell())
			{
				OnTetriminoOutsideWell();
			}
			else if (IsFallingTetriminoDisabled())
			{
				DiscardTetriminoVisual(true);
			}
		}

		public override void OnHardDropDone()
		{
			base.OnHardDropDone();
			if (HasAtLeastOneFallingMinoInVisualWell())
			{
				SetNextGameState(5);
			}
			else
			{
				OnTetriminoOutsideWell();
			}
		}

		public override bool UseFloatingMinos()
		{
			return true;
		}

		public override bool TryingToDestroyFloatingMino(Mino mino)
		{
			return false;
		}

		public virtual void OnTetriminoFallenThrough()
		{
			mDestroyFallingTetrimino = true;
			mGameStatistics.SetStatistic(3, false);
		}

		public override bool IsFallingTetriminoLockable()
		{
			if (base.IsFallingTetriminoLockable() && !IsFallingTetriminoDisabled())
			{
				return HasAtLeastOneFallingMinoInVisualWell();
			}
			return false;
		}

		public override void OnResetKeyPressed()
		{
			base.OnResetKeyPressed();
			mWasSoftDropping = false;
		}

		public override bool IsBlockAvailable(int column, int row)
		{
			bool result = true;
			if (FindNbOfItemsInPlacementMatrixCol(mTopRow, mBottomRow, column) >= 1 || !base.IsBlockAvailable(column, row))
			{
				result = false;
			}
			return result;
		}

		public override bool UseHardDropScore()
		{
			return HasAtLeastOneFallingMinoInVisualWell();
		}

		public virtual int FindNbOfItemsInPlacementMatrixCol(int bottom, int top, int col)
		{
			int num = 0;
			for (int i = 0; i < mPlacementMatrixHeight; i++)
			{
				if (mPlacementMatrix[i][col])
				{
					num++;
				}
			}
			return num;
		}

		public virtual bool HasAtLeastOneFallingMinoInVisualWell()
		{
			for (Mino mino = mFallingTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				if (mino.GetMatrixPosY() < 40)
				{
					return true;
				}
			}
			return false;
		}

		public virtual void OnTetriminoOutsideWell()
		{
			SetNextGameState(3);
			DiscardTetriminoVisual(false);
			ResetNextMoveTime();
			OnTetriminoFallenThrough();
			SetSoftDropActive(mWasSoftDropping);
			ReleaseGhost();
		}

		public virtual void DiscardTetriminoVisual(bool start)
		{
			bool flag = mWasSoftDropping;
			SetSoftDropActive(start);
			mWasSoftDropping = flag;
		}
	}
}
