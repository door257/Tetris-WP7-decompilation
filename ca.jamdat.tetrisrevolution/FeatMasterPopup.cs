using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FeatMasterPopup : Popup
	{
		public Text mTitleText;

		public Text mText;

		public FeatMasterPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(327690);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			FlString flString = EntryPoint.GetFlString(package, 2);
			VerticalTextScroller.InitializeWithHeader(mContentScroller, flString);
		}

		public override void Show()
		{
			SetSoftKeys();
			base.Show();
		}

		public override void OnShowPopup()
		{
			mContentScroller.TakeFocus();
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override void SetSoftKeys()
		{
			mSelectSoftKey.SetFunction(0, 4);
			mClearSoftKey.SetFunction(2, 4);
			base.SetSoftKeys();
		}

		public override bool TapHidesPopup()
		{
			return true;
		}
	}
}
