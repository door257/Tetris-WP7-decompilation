using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GlossarySelectMenuPopup : HorizontalSelectorPopup
	{
		public FlString[] mContentStrings;

		public GlossarySelectMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(2981979);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			mContentStrings = new FlString[7];
			for (int i = 0; i < 7; i++)
			{
				mContentStrings[i] = EntryPoint.GetFlString(package, 5 + i);
			}
		}

		public override void Unload()
		{
			mContentStrings = null;
			base.Unload();
		}

		public static int ConvertReplayIdToSelectionIndex(int replayId)
		{
			switch (replayId)
			{
			case 0:
				return 2;
			case 1:
				return 4;
			case 2:
				return 0;
			case 3:
				return 5;
			case 4:
				return 3;
			case 5:
				return 1;
			case 6:
				return 6;
			default:
				return -1;
			}
		}

		public override void Update()
		{
			VerticalTextScroller.Initialize(mContentScroller, mContentStrings[mFocusedSelectionIndex]);
		}
	}
}
