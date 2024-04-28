using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PromptPopup : Popup
	{
		public Selector mSelector;

		public int mFocusedSelectionIndex;

		public PromptPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			HorizontalSelector.Initialize(mSelector, 0);
		}

		public override void Unload()
		{
			mSelector = null;
			base.Unload();
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

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, mSelector);
		}

		public override void TakeFocus()
		{
			mSelector.SetSingleSelection(mFocusedSelectionIndex, true);
		}

		public override bool HasFocus()
		{
			if (!mSelector.DescendentOrSelfHasFocus())
			{
				return base.HasFocus();
			}
			return true;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (msg == -127 && intParam == 1 && mSelector.GetScrollerViewport() == source.GetViewport())
			{
				int singleSelection = mSelector.GetSingleSelection();
				if (singleSelection != mFocusedSelectionIndex)
				{
					mFocusedSelectionIndex = singleSelection;
				}
				flag = true;
			}
			if (!flag)
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public virtual int GetCommand()
		{
			int singleSelection = mSelector.GetSingleSelection();
			Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
			return selectionAt.GetCommand();
		}

		public virtual int GetSingleSelection()
		{
			return mSelector.GetSingleSelection();
		}

		public virtual void SetSingleSelection(int selection)
		{
			mSelector.SetSingleSelection(selection);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}
	}
}
