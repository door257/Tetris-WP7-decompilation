using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PauseMenu : Popup
	{
		public Selector mSelector;

		public PauseMenu(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1769526);
		}

		public override void GetEntryPoints()
		{
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = mSelector;
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
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

		public override void Show()
		{
			mSelectSoftKey.SetFunction(7, -10);
			mClearSoftKey.SetFunction(1, 8);
			VerticalSelector.Initialize(mSelector, 0, 0);
			base.Show();
		}

		public override void TakeFocus()
		{
			mSelector.SetSingleSelection(0, true);
		}

		public override bool HasFocus()
		{
			if (!mSelector.DescendentOrSelfHasFocus())
			{
				return base.HasFocus();
			}
			return true;
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
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}
	}
}
