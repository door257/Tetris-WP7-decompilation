using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantFlashlite : TetrisGame
	{
		public int mLightTimer;

		public int mLightDurationLinesCleared;

		public int mLightDurationHardDrop;

		public VariantFlashlite(GameParameter gameParameter)
			: base(gameParameter)
		{
			mPackageId = 983070;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 10;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 98;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 99;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 101;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(3);
			mLightDurationLinesCleared = mGameParameter.GetFromPackage(package, 3, 1);
			mLightDurationHardDrop = mGameParameter.GetFromPackage(package, 3, 2);
		}

		public override void OnHardDrop()
		{
			base.OnHardDrop();
			TurnOnLight(mLightDurationHardDrop);
		}

		public override void OnTetriminoHold()
		{
			base.OnTetriminoHold();
			TurnOnFlashliteUnderFallingTetrimino();
		}

		public override void OnTetriminoSideMove()
		{
			base.OnTetriminoSideMove();
			TurnOnFlashliteUnderFallingTetrimino();
		}

		public override void OnTetriminoRotate()
		{
			base.OnTetriminoRotate();
			TurnOnFlashliteUnderFallingTetrimino();
		}

		public override void OnInitializeFallingTetrimino()
		{
			base.OnInitializeFallingTetrimino();
			TurnOnFlashliteUnderFallingTetrimino();
		}

		public override void OnTetriminoFall()
		{
			base.OnTetriminoFall();
			TurnOnFlashliteUnderFallingTetrimino();
		}

		public override void OnLinesCleared()
		{
			base.OnLinesCleared();
			TurnOnLight(mLightDurationLinesCleared);
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			sbyte currentStateID = GetCurrentStateID();
			if (currentStateID != 2 && currentStateID != 6 && currentStateID != 7 && mLightTimer > 0)
			{
				mLightTimer -= deltaTimeMs;
				if (mLightTimer < 0 && mFallingTetrimino != null)
				{
					TurnOnFlashliteUnderFallingTetrimino();
					mLightTimer = 0;
				}
			}
		}

		public override void SetTetriminoInWellVisible(bool visible)
		{
			base.SetTetriminoInWellVisible(visible);
			bool flag = mLightTimer <= 0;
			if (visible)
			{
				if (mFallingTetrimino != null && flag)
				{
					TurnOnFlashliteUnderFallingTetrimino();
				}
			}
			else if (flag)
			{
				TurnOffLight();
			}
		}

		public virtual void TurnOnLight(int duration)
		{
			TurnOffLight();
			if (duration > 0)
			{
				mLightTimer = duration;
				for (int i = 0; i < 10; i++)
				{
					TurnOnFlashliteInColumn(i, 20);
				}
			}
		}

		public virtual void TurnOffLight()
		{
			for (Tetrimino tetrimino = mWell.GetTetriminoList().GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				if (tetrimino.GetRootMino() != null && tetrimino != mFallingTetrimino && tetrimino.GetRootMinoType() != 7 && tetrimino.IsLocked())
				{
					tetrimino.SetVisible(false);
				}
			}
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Shape shape = null;
			int num = 199;
			for (int i = 0; i <= num; i++)
			{
				shape = EntryPoint.GetShape(preLoadedPackage, i + 320);
				shape.SetVisible(false);
			}
		}

		public virtual void TurnOnFlashliteInColumn(int column, int fromRow)
		{
			Tetrimino tetrimino = mWell.GetTetriminoList().GetRootTetrimino();
			if (fromRow < 20)
			{
				fromRow = 20;
			}
			while (tetrimino != null)
			{
				for (Mino mino = tetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
				{
					if (mino.GetMatrixPosX() == column && mino.GetMatrixPosY() >= fromRow)
					{
						mino.GetMinoViewport().SetVisible(true);
					}
				}
				tetrimino = tetrimino.GetNextNode();
			}
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Shape shape = null;
			for (int i = fromRow; i < 40; i++)
			{
				if (i >= 20)
				{
					int entryPoint = 10 * (i - 20) + column + 320;
					shape = EntryPoint.GetShape(preLoadedPackage, entryPoint);
					shape.SetVisible(true);
				}
			}
		}

		public virtual void TurnOnFlashliteUnderFallingTetrimino()
		{
			if (mLightTimer > 0)
			{
				return;
			}
			TurnOffLight();
			for (Mino mino = mFallingTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				if (mFallingTetrimino.IsFarthestMinoInDirection(mino.GetDefaultIdx(), 0, 1))
				{
					TurnOnFlashliteInColumn(mino.GetMatrixPosX(), mino.GetMatrixPosY() + 1);
				}
			}
		}
	}
}
