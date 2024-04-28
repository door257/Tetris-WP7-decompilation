using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class HorizontalSelectorPopup : Popup
	{
		public Selector mSelector;

		public int mFocusedSelectionIndex;

		public HorizontalSelectorPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void TakeFocus()
		{
			mSelector.TakeFocus();
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
			if (msg == -127 && intParam == 1 && mSelector.GetScrollerViewport() == source.GetViewport())
			{
				ResynchSelector();
			}
			if (msg != -121 && msg != -119)
			{
				return base.OnMsg(source, msg, intParam);
			}
			if (mSelector == null || !mSelector.OnDefaultMsg(source, msg, intParam))
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public virtual void InitSoftkeys()
		{
			mSelectSoftKey.SetFunction(3, -10);
			mClearSoftKey.SetFunction(1, 4);
		}

		public override void Initialize()
		{
			base.Initialize();
			InitSoftkeys();
			HorizontalSelector.Initialize(mSelector, mFocusedSelectionIndex);
			Update();
		}

		public virtual void SetInitialSelectionIndex(int selectionIndex)
		{
			mFocusedSelectionIndex = selectionIndex;
		}

		public override bool OnCommand(int command)
		{
			bool flag = false;
			if (command == -10)
			{
				int command2 = mSelector.GetSelectionAt(mSelector.GetSingleSelection()).GetCommand();
				flag = GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(command2);
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public override void OnShowPopup()
		{
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool flag = false;
			short absoluteTop = mSelector.GetNextArrow().GetViewport().GetAbsoluteTop();
			short num = (short)(absoluteTop + mSelector.GetNextArrow().GetViewport().GetRectHeight());
			if (lastPenPosition.GetY() > absoluteTop && lastPenPosition.GetY() < num)
			{
				return false;
			}
			if (command == 98)
			{
				OnCommand(-10);
			}
			else if (96 == command || 97 == command)
			{
				int advance = ((96 != command) ? 1 : (-1));
				mSelector.OnScrollEvent(advance);
			}
			if (!flag)
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, popupSelector);
			}
			return true;
		}

		public virtual void Update()
		{
		}

		public virtual void ResynchSelector()
		{
			int singleSelection = mSelector.GetSingleSelection();
			if (singleSelection != mFocusedSelectionIndex)
			{
				mFocusedSelectionIndex = singleSelection;
				Update();
			}
			mScrollbar.Reinitialize(mContentScroller);
		}
	}
}
