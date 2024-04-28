using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class HelpMenuPopup : HorizontalSelectorPopup
	{
		public Viewport mHeaderViewport;

		public FlString[] mContentStrings;

		public HelpMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAutoResize = false;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1572912);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mContentStrings = new FlString[7];
			for (int i = 0; i < 6; i++)
			{
				mContentStrings[i] = EntryPoint.GetFlString(package, 7 + i);
			}
			mContentStrings[6] = CreateAboutContentString();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 5);
			mHeaderViewport = EntryPoint.GetViewport(package, 6);
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, 4);
		}

		public override void Unload()
		{
			if (mContentStrings != null)
			{
				for (int i = 0; i < 7; i++)
				{
					mContentStrings[i] = null;
				}
				mContentStrings = null;
			}
			mHeaderViewport = null;
			base.Unload();
		}

		public override void Update()
		{
			mHeaderViewport.SetVisible(mFocusedSelectionIndex == 6);
			VerticalTextScroller.InitializeWithHeader(mContentScroller, new FlString(mContentStrings[mFocusedSelectionIndex]));
		}

		public virtual FlString CreateAboutContentString()
		{
			FlString flString = new FlString();
			Package package = mContentMetaPackage.GetPackage();
			FlString flString2 = EntryPoint.GetFlString(package, 13);
			flString.AddAssign(flString2);
			FlString flString3 = EntryPoint.GetFlString(package, 14);
			flString.AddAssign(flString3);
			FlString @string = new FlString(FlApplication.GetJamdatBuildString());
			flString.AddAssign(@string);
			FlString flString4 = EntryPoint.GetFlString(package, 15);
			flString.AddAssign(flString4);
			FlString flString5 = EntryPoint.GetFlString(package, 16);
			flString.AddAssign(flString5);
			return flString;
		}
	}
}
