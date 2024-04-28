using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FailurePopup : Popup
	{
		public Text mTitleText;

		public Text mText;

		public FailurePopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(229383);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mTitleText = EntryPoint.GetText(package, 0);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			mText = EntryPoint.GetText(package, 2);
		}

		public override void Show()
		{
			FlString flString = new FlString();
			TetrisGame tetrisGame = GameFactory.GetTetrisGame();
			flString.AddAssign(tetrisGame.GetLongHintString());
			VerticalTextScroller.Initialize(mContentScroller, flString);
			SetSoftKeys();
			base.Show();
		}

		public override void OnShowPopup()
		{
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override void TakeFocus()
		{
			mContentScroller.TakeFocus();
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
