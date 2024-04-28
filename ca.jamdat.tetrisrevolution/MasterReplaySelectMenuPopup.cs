using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class MasterReplaySelectMenuPopup : HorizontalSelectorPopup
	{
		public Text mAuthorNameText;

		public Text mTimeValueText;

		public MasterReplaySelectMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public static int ConvertReplayIdToSelectionIndex(int replayId)
		{
			switch (replayId)
			{
			case 7:
				return 0;
			case 8:
				return 2;
			case 9:
				return 3;
			case 10:
				return 8;
			case 11:
				return 4;
			case 12:
				return 7;
			case 13:
				return 6;
			case 14:
				return 5;
			case 15:
				return 9;
			case 16:
				return 10;
			case 17:
				return 1;
			case 18:
				return 11;
			default:
				return -1;
			}
		}

		public static int ConvertSelectionIndexToReplayId(int idx)
		{
			switch (idx)
			{
			case 0:
				return 7;
			case 2:
				return 8;
			case 3:
				return 9;
			case 8:
				return 10;
			case 4:
				return 11;
			case 7:
				return 12;
			case 6:
				return 13;
			case 5:
				return 14;
			case 9:
				return 15;
			case 10:
				return 16;
			case 1:
				return 17;
			case 11:
				return 18;
			default:
				return -1;
			}
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(3014748);
		}

		public override void Initialize()
		{
			base.Initialize();
			VerticalScroller.Initialize(mContentScroller, 0);
			CustomComponentUtilities.DisableLockedVariantSelections(mSelector);
			Update();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			mAuthorNameText = EntryPoint.GetText(package, 6);
			mTimeValueText = EntryPoint.GetText(package, 5);
		}

		public override void Update()
		{
			base.Update();
			int num = ConvertSelectionIndexToReplayId(mSelector.GetSingleSelection());
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1310760);
			int entryPoint = 208 + num * 5;
			int @int = EntryPoint.GetInt(preLoadedPackage, entryPoint);
			FlString flString = new FlString();
			Utilities.AddMSToStringFromMillisecond(flString, @int);
			FlString masterReplayAuthorString = Utilities.GetMasterReplayAuthorString(num);
			mAuthorNameText.SetCaption(new FlString(StringUtils.ToLowerCase(masterReplayAuthorString).ToRawString()));
			mTimeValueText.SetCaption(flString);
		}
	}
}
