using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LongHintPopup : Popup
	{
		public Text mTitleText;

		public Text mText;

		public LongHintPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(458766);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mTitleText = EntryPoint.GetText(package, 0);
			mText = EntryPoint.GetText(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
		}

		public override void Show()
		{
			GameController gameController = (GameController)mBaseScene;
			TetrisGame game = gameController.GetGame();
			mTitleText.SetCaption(game.GetGameTitleString());
			FlString flString = new FlString();
			flString.AddAssign(game.GetLongHintString());
			VerticalTextScroller.Initialize(mContentScroller, flString);
			mSelectSoftKey.SetFunction(0, 4);
			mClearSoftKey.SetFunction(4, -19);
			base.Show();
		}

		public override void OnShowPopup()
		{
			mContentScroller.TakeFocus();
			base.OnShowPopup();
		}
	}
}
