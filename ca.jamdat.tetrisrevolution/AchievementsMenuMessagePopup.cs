using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class AchievementsMenuMessagePopup : Popup
	{
		private FlString mErrorMsg;

		private Sprite mTestSprite;

		public AchievementsMenuMessagePopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, FlString msg)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAutoResize = false;
			mErrorMsg = msg;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1703988);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			Text text = EntryPoint.GetText(package, 2);
			text.SetCaption(mErrorMsg);
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, -18);
		}

		public override void Unload()
		{
			if (mContentScroller != null)
			{
				mContentScroller = null;
			}
			base.Unload();
		}
	}
}
