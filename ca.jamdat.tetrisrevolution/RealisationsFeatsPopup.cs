using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsFeatsPopup : RealisationsScrollerPopup
	{
		public const int categoryCareer = 0;

		public const int categoryAdvanced = 1;

		public const int categoryCount = 2;

		public Text[] mCareerFeatValues;

		public IndexedSprite[] mAdvancedFeatUnlockSprites;

		public RealisationsFeatsPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2850903);
		}

		public override void Unload()
		{
			if (mCareerFeatValues != null)
			{
				for (int i = 0; i < 6; i++)
				{
					mCareerFeatValues[i] = null;
				}
			}
			if (mAdvancedFeatUnlockSprites != null)
			{
				for (int j = 0; j < 5; j++)
				{
					mAdvancedFeatUnlockSprites[j] = null;
				}
			}
			mCareerFeatValues = null;
			mAdvancedFeatUnlockSprites = null;
			base.Unload();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mAdvancedFeatUnlockSprites = new IndexedSprite[5];
			mCareerFeatValues = new Text[6];
			int num = 6;
			for (int i = 0; i < 6; i++)
			{
				mCareerFeatValues[i] = EntryPoint.GetText(package, num + i);
			}
			int num2 = 12;
			for (int j = 0; j < 5; j++)
			{
				mAdvancedFeatUnlockSprites[j] = EntryPoint.GetIndexedSprite(package, num2 + j);
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			FeatsExpert featsExpert = FeatsExpert.Get();
			FlString flString = null;
			((Selector)mContentScroller).IsSelectionScrollerEnabled = true;
			for (int i = 0; i < 6; i++)
			{
				int careerFeatPercent = featsExpert.GetCareerFeatPercent((sbyte)i);
				flString = StatisticsFormatting.CreateStatisticString(careerFeatPercent, 5);
				mCareerFeatValues[i].SetCaption(flString);
			}
			for (int j = 0; j < 5; j++)
			{
				bool flag = featsExpert.IsAdvancedFeatUnlocked((sbyte)j);
				mAdvancedFeatUnlockSprites[j].SetCurrentFrame(flag ? 1 : 0);
			}
		}

		public override bool OnCommand(int command)
		{
			bool flag = false;
			if (command == -10)
			{
				int selectionIndex = GetSelectionIndex();
				Selection selectionAt = ((Selector)mContentScroller).GetSelectionAt(selectionIndex);
				flag = GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(selectionAt.GetCommand());
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public virtual int GetSelectionIndex()
		{
			return ((Selector)mContentScroller).GetSingleSelection();
		}

		public virtual void SetSelectionIndex(int index)
		{
			((Selector)mContentScroller).SetSingleSelection(index, true);
			ForceScrollbarUpdate();
		}

		public override void OnShowPopup()
		{
			base.OnShowPopup();
			UpdateContentScrollers();
		}

		public override void UpdateContentScrollers()
		{
			base.UpdateContentScrollers();
			Component[] mElements = mContentScroller.mElements;
			Selection selection = null;
			Component[] array = mElements;
			foreach (Component component in array)
			{
				if (component is Selection)
				{
					selection = (Selection)component;
					selection.SetDragSelectionBlockThreshold(100);
				}
			}
		}
	}
}
