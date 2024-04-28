using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ResetGameConfirmationPopup : Popup
	{
		public ResetGameConfirmationPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mPopupDuration = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(65538);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 6);
			EntryPoint.GetText(package, 7);
		}

		public override void Show()
		{
			mSelectSoftKey.SetFunction(0, -10);
			if (mBaseScene.GetId() == 14)
			{
				mClearSoftKey.SetFunction(1, 4);
			}
			base.Show();
		}
	}
}
