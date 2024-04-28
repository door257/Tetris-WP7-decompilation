using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LockedGameVariantMenuPopup : Popup
	{
		public int mVariant;

		public IndexedSprite mLockedSprite;

		public Text mContentText;

		public LockedGameVariantMenuPopup(BaseScene baseScene, int variant, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mVariant = variant;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2818134);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentText = EntryPoint.GetText(package, 3);
			mLockedSprite = EntryPoint.GetIndexedSprite(package, 4);
			mContentScroller = EntryPoint.GetScroller(package, 2);
			mScrollbarViewport = EntryPoint.GetViewport(package, 5);
		}

		public override void Initialize()
		{
			base.Initialize();
			FlString flString = new FlString();
			flString.AddAssign(Utilities.GetFeatDescriptionString(mVariant));
			mContentText.SetCaption(flString);
			short rectWidth = mContentText.GetRectWidth();
			short height = (short)(mContentText.GetNbLines() * mContentText.GetLineHeight());
			mContentText.SetSize(rectWidth, height);
			mContentScroller.GetScrollerViewport().SetSize(mContentScroller.GetRectWidth(), mContentScroller.GetRectHeight());
			mContentScroller.ResetScroller();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, 4);
		}

		public override bool TapHidesPopup()
		{
			return true;
		}
	}
}
