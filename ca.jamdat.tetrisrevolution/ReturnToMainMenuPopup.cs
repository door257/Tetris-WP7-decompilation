using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ReturnToMainMenuPopup : Popup
	{
		public ReturnToMainMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(163845);
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
			mSelectSoftKey.SetFunction(0, -10);
			base.Show();
		}
	}
}
