using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RetryPopup : Popup
	{
		public RetryPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(131076);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
		}

		public override void Show()
		{
			SetSoftKeys();
			base.Show();
		}

		public override void SetSoftKeys()
		{
			mSelectSoftKey.SetFunction(0, -10);
			mClearSoftKey.SetFunction(2, -17);
			base.SetSoftKeys();
		}

		public int GetCommand()
		{
			return 1;
		}
	}
}
