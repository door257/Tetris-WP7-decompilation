using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SuccessPopup : Popup
	{
		public Text mTitleText;

		public SuccessPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mPopupDuration = 6000;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(196614);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mTitleText = EntryPoint.GetText(package, 0);
			mContentScroller = EntryPoint.GetScroller(package, 4);
			mScrollbarViewport = EntryPoint.GetViewport(package, 5);
		}

		public override void Show()
		{
			SetSoftKeys();
			FlString @string = new FlString(Utilities.GetStringFromPackage(2, 196614));
			VerticalTextScroller.Initialize(mContentScroller, @string);
			base.Show();
		}

		public override void OnShowPopup()
		{
			mContentScroller.TakeFocus();
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override bool TapHidesPopup()
		{
			return true;
		}

		public override void SetSoftKeys()
		{
			mSelectSoftKey.SetFunction(0, 4);
			mClearSoftKey.SetFunction(2, 4);
			base.SetSoftKeys();
		}
	}
}
