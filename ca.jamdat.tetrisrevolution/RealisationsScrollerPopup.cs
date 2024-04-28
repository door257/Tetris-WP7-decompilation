using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsScrollerPopup : Popup
	{
		public Selector mCategorySelector;

		public Scroller[] mContentScrollerArray;

		public int mNumberOfCategories;

		public RealisationsScrollerPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAutoResize = false;
		}

		public override void destruct()
		{
		}

		public override void Unload()
		{
			if (mContentScrollerArray != null)
			{
				for (int i = 0; i < mNumberOfCategories; i++)
				{
					VerticalScroller.Uninitialize(mContentScrollerArray[i]);
					mContentScrollerArray[i] = null;
				}
				mContentScrollerArray = null;
			}
			mScrollbarViewport = null;
			mCategorySelector = null;
			mCategorySelector = null;
			base.Unload();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mScrollbarViewport = EntryPoint.GetViewport(package, 2);
			mCategorySelector = EntryPoint.GetSelector(package, 3);
			mNumberOfCategories = mCategorySelector.GetNumSelections();
			mContentScrollerArray = new Scroller[mNumberOfCategories];
			for (int i = 0; i < mNumberOfCategories; i++)
			{
				mContentScrollerArray[i] = EntryPoint.GetScroller(package, 4 + i);
				mContentScrollerArray[i].SetVisible(false);
			}
			mContentScroller = mContentScrollerArray[0];
		}

		public override void Initialize()
		{
			base.Initialize();
			for (int i = 0; i < mNumberOfCategories; i++)
			{
				if (mContentScrollerArray[i] is Selector)
				{
					VerticalSelector.Initialize((Selector)mContentScrollerArray[i], 0, 0);
				}
				else
				{
					VerticalScroller.Initialize(mContentScrollerArray[i], 0);
				}
				mContentScrollerArray[i].SetVisible(false);
			}
			mContentScroller.SetVisible(true);
			HorizontalSelector.Initialize(mCategorySelector, 0);
			mSelectSoftKey.SetFunction(0, -10);
			mClearSoftKey.SetFunction(1, 4);
		}

		public override void TakeFocus()
		{
			if (mContentScroller is Selector)
			{
				Selector selector = (Selector)mContentScroller;
				selector.GetSelectionAt(selector.GetSingleSelection()).TakeFocus();
			}
			else
			{
				mCategorySelector.TakeFocus();
			}
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (msg == -127 && intParam == 1 && mCategorySelector.GetScrollerViewport() == source.GetViewport())
			{
				UpdateContentScrollers();
			}
			if (msg != -121 && msg != -119)
			{
				return base.OnMsg(source, msg, intParam);
			}
			if (mCategorySelector == null || !mCategorySelector.OnDefaultMsg(source, msg, intParam))
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public virtual int GetCategoryIndex()
		{
			return mCategorySelector.GetSingleSelection();
		}

		public virtual void SetCategoryIndex(int index)
		{
			mCategorySelector.SetSingleSelection(index);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool flag = false;
			if (96 == command || 97 == command)
			{
				int advance = ((96 != command) ? 1 : (-1));
				mCategorySelector.OnScrollEvent(advance);
			}
			if (!flag)
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, popupSelector);
			}
			return true;
		}

		public virtual void UpdateContentScrollers()
		{
			int singleSelection = mCategorySelector.GetSingleSelection();
			mContentScroller.SetVisible(false);
			mContentScroller = mContentScrollerArray[singleSelection];
			mContentScroller.SetVisible(true);
			if (mContentScroller is Selector)
			{
				Selector selector = (Selector)mContentScroller;
				int num = 0;
				selector.SetSingleSelection(num, true);
				mCategorySelector.GetSelectionAt(singleSelection).SetForwardFocus(selector.GetSelectionAt(num));
			}
			else
			{
				mContentScroller.ResetScroller();
			}
			mScrollbar.Reinitialize(mContentScroller);
		}
	}
}
