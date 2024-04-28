using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantSplit : TetrisGame
	{
		public Shape mLineShape;

		public Component mDimComponent;

		public bool mStartLeft;

		public VariantSplit(GameParameter gameParameter)
			: base(gameParameter)
		{
			mPackageId = 917532;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 8;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 90;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 91;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 93;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			GetGameParameters(1);
		}

		public override void InitializeComponents(GameController gameController)
		{
			base.InitializeComponents(gameController);
			Package package = mMetaPackage.GetPackage();
			mLineShape = EntryPoint.GetShape(package, 15);
			mLayerComponent.Attach(mLineShape, 7);
			mDimComponent = EntryPoint.GetComponent(package, 16);
			mLayerComponent.Attach(mDimComponent, 7);
		}

		public override void Unload()
		{
			if (mLineShape != null)
			{
				mLineShape.SetViewport(null);
				mLineShape = null;
			}
			if (mDimComponent != null)
			{
				mDimComponent.SetViewport(null);
				mDimComponent = null;
			}
			base.Unload();
		}

		public override void OnTetriminoLock()
		{
			base.OnTetriminoLock();
			mStartLeft = !mStartLeft;
		}

		public override int GetLeftWellLimit()
		{
			if (!mStartLeft)
			{
				return 4;
			}
			return -1;
		}

		public override int GetRightWellLimit()
		{
			if (!mStartLeft)
			{
				return 10;
			}
			return 5;
		}

		public override int GetTetriminoStartPositionX()
		{
			if (!mStartLeft)
			{
				return 7;
			}
			return 2;
		}

		public override void OnEntryToStateWaitingForFall()
		{
			short left = (short)(mStartLeft ? 241 : 82);
			mDimComponent.SetTopLeft(left, mDimComponent.GetRectTop());
			mDimComponent.SetVisible(true);
		}

		public override void OnLinesCleared()
		{
			mDimComponent.SetVisible(false);
		}

		public override void OnGameOver()
		{
			base.OnGameOver();
			mDimComponent.SetVisible(false);
		}
	}
}
