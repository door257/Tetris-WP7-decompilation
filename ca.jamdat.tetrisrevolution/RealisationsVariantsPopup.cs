using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsVariantsPopup : Popup
	{
		public RealisationsVariantsPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
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
			mContentMetaPackage = GameLibrary.GetPackage(2949210);
		}

		public override void GetEntryPoints()
		{
			Package package = mContentMetaPackage.GetPackage();
			FeatsExpert featsExpert = FeatsExpert.Get();
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			Selector selector = EntryPoint.GetSelector(package, 2);
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
			FlString @string = new FlString(EntryPoint.GetFlString(package, 4));
			VerticalSelector.Initialize(selector, 0, 0);
			int num = 5;
			int num2 = 17;
			int num3 = 29;
			for (int i = 0; i < 12; i++)
			{
				if (!featsExpert.IsGameVariantUnlocked(i))
				{
					Text text = EntryPoint.GetText(package, num + i);
					text.SetCaption(new FlString(@string));
				}
				int highestLevelDone = progressionExpert.GetHighestLevelDone(i);
				Text text2 = EntryPoint.GetText(package, num2 + i);
				text2.SetCaption(new FlString(FlMath.Maximum(highestLevelDone, 0)));
				FlString flString = new FlString();
				Text text3 = EntryPoint.GetText(package, num3 + i);
				Utilities.GetRomanNumeral(i + 1, flString);
				flString.AddAssign(".");
				text3.SetCaption(flString);
			}
			mContentScroller = selector;
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(0, -10);
			mClearSoftKey.SetFunction(1, 4);
			((Selector)mContentScroller).IsSelectionScrollerEnabled = true;
			Component[] mElements = mContentScroller.mElements;
			Selection selection = null;
			Component[] array = mElements;
			foreach (Component component in array)
			{
				if (component is Selection)
				{
					selection = (Selection)component;
					selection.SetDragSelectionBlockThreshold(250);
				}
			}
		}

		public override void Unload()
		{
			if (mContentScroller != null)
			{
				VerticalSelector.Uninitialize((Selector)mContentScroller);
			}
			base.Unload();
		}

		public override void TakeFocus()
		{
			mContentScroller.TakeFocus();
		}

		public override bool OnCommand(int command)
		{
			bool flag = false;
			if (command == -10)
			{
				int selectionIndex = GetSelectionIndex();
				Selection selectionAt = ((Selector)mContentScroller).GetSelectionAt(selectionIndex);
				SetSelectionIndex(GetSelectionIndex());
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
	}
}
