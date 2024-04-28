using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class OptionsMenuPopup : Popup
	{
		public Selector mSelector;

		public int mFocusedSelectionIndex;

		public OptionsMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1736757);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = mSelector;
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
		}

		public override void Initialize()
		{
			if (mBaseScene.GetId() == 40)
			{
				int selectionIndexFromCommmand = Utilities.GetSelectionIndexFromCommmand(mSelector, 65);
				mSelector.GetSelectionAt(selectionIndexFromCommmand).SetEnabledState(false);
				mFocusedSelectionIndex = 1;
				int selectionIndexFromCommmand2 = Utilities.GetSelectionIndexFromCommmand(mSelector, -31);
				mSelector.GetSelectionAt(selectionIndexFromCommmand2).SetEnabledState(false);
				int selectionIndexFromCommmand3 = Utilities.GetSelectionIndexFromCommmand(mSelector, -47);
				mSelector.GetSelectionAt(selectionIndexFromCommmand3).SetEnabledState(false);
			}
			else if (GameApp.Get().GetIsDemo())
			{
				int selectionIndexFromCommmand4 = Utilities.GetSelectionIndexFromCommmand(mSelector, -31);
				if (selectionIndexFromCommmand4 != -1)
				{
					mSelector.GetSelectionAt(selectionIndexFromCommmand4).SetCommand(-33);
				}
			}
			base.Initialize();
			UpdateCheckBoxesStatus();
			mSelectSoftKey.SetFunction(0, -10);
			mClearSoftKey.SetFunction(1, 4);
			VerticalSelector.Initialize(mSelector, 0, 0);
		}

		public override bool OnCommand(int command)
		{
			bool flag = false;
			if (command == -10)
			{
				int singleSelection = mSelector.GetSingleSelection();
				Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
				flag = GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(selectionAt.GetCommand());
			}
			if ((!flag && command == 64) || command == 65 || command == 62 || command == 63)
			{
				Settings settings = GameApp.Get().GetSettings();
				bool flag2 = false;
				if (command == 65)
				{
					flag2 = !settings.IsSoundEnabled();
				}
				flag = GameApp.Get().GetCommandHandler().Execute(command);
				if (flag2)
				{
					SongPlayer.Get.Stop();
					mBaseScene.StartMusic();
				}
				UpdateCheckBoxesStatus();
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public override void Unload()
		{
			if (mSelector != null)
			{
				VerticalSelector.Uninitialize(mSelector);
				mSelector = null;
			}
			base.Unload();
		}

		public override bool HasFocus()
		{
			if (!mSelector.DescendentOrSelfHasFocus())
			{
				return base.HasFocus();
			}
			return true;
		}

		public override void TakeFocus()
		{
			mSelector.SetSingleSelection(mFocusedSelectionIndex, true);
		}

		public virtual void UpdateCheckBoxesStatus()
		{
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			Settings settings = GameApp.Get().GetSettings();
			Selection selectionAt = mSelector.GetSelectionAt(Utilities.GetSelectionIndexFromCommmand(mSelector, 65));
			UpdateCheckboxStatus(selectionAt, settings.IsSoundEnabled());
			Selection selectionAt2 = mSelector.GetSelectionAt(Utilities.GetSelectionIndexFromCommmand(mSelector, 62));
			UpdateCheckboxStatus(selectionAt2, gameSettings.IsTutorialEnabled());
			Selection selectionAt3 = mSelector.GetSelectionAt(Utilities.GetSelectionIndexFromCommmand(mSelector, 63));
			UpdateCheckboxStatus(selectionAt3, gameSettings.GetTouchMode() == 1);
			Selection selectionAt4 = mSelector.GetSelectionAt(Utilities.GetSelectionIndexFromCommmand(mSelector, 64));
			UpdateCheckboxStatus(selectionAt4, gameSettings.IsGhostEnabled());
		}

		public virtual void UpdateCheckboxStatus(Selection selection, bool @checked)
		{
			int index = ((selection.GetChild(0) != mBaseScene.GetCursor()) ? 1 : 2);
			IndexedSprite indexedSprite = (IndexedSprite)selection.GetChild(index);
			indexedSprite.SetCurrentFrame(@checked ? 1 : 0);
		}
	}
}
