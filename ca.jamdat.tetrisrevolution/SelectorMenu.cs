using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SelectorMenu : Menu
	{
		public Selector mSelector;

		public int mFocusedSelectionIndex;

		public SelectorMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
		}

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mSelector = Selector.Cast(mPackage.GetEntryPoint(-1), null);
		}

		public override void ReceiveFocus()
		{
			base.ReceiveFocus();
			Selection selectionAt = mSelector.GetSelectionAt(mFocusedSelectionIndex);
			selectionAt.TakeFocus();
			mSelector.SetSingleSelection(mFocusedSelectionIndex, true);
		}

		public override void Unload()
		{
			if (mSelector != null)
			{
				mFocusedSelectionIndex = mSelector.GetSingleSelection();
				mSelector = null;
			}
			base.Unload();
		}

		public virtual void SetInitialSelection(int selectedIndex)
		{
			mFocusedSelectionIndex = selectedIndex;
		}

		public override bool OnCommand(int command)
		{
			bool flag = base.OnCommand(command);
			if (!flag && command == -10)
			{
				int singleSelection = mSelector.GetSingleSelection();
				Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
				flag = OnCommand(selectionAt.GetCommand());
			}
			return flag;
		}

		public virtual void EnableSelector(bool enable)
		{
			for (int i = 0; i < mSelector.GetNumSelections(); i++)
			{
				mSelector.GetSelectionAt(i).SetEnabledState(enable);
			}
			if (enable)
			{
				int singleSelection = mSelector.GetSingleSelection();
				Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
				mSelector.SetSelectionAt(singleSelection, selectionAt);
			}
		}
	}
}
