using System;

namespace ca.jamdat.flight
{
	public class Scroller : Viewport
	{
		public new const sbyte typeNumber = 98;

		public new const sbyte typeID = 98;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte intermediateState = 0;

		public const sbyte finalState = 1;

		public Component[] mElements;

		public int mNumElements;

		public int mMaxNumElements;

		public Viewport mScrollerViewport;

		public Selection mNextArrow;

		public Selection mPreviousArrow;

		public bool mIsVertical;

		public bool mIsViewportCentered;

		public bool mArrowBehaviorCameFromScroller;

		public static Scroller Cast(object o, Scroller _)
		{
			return (Scroller)o;
		}

		public override sbyte GetTypeID()
		{
			return 98;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public Scroller()
		{
			mIsVertical = true;
		}

		public Scroller(int maxNumElements)
		{
			mMaxNumElements = maxNumElements;
			mIsVertical = true;
			mElements = AllocComponentTable();
		}

		public override void destruct()
		{
			for (int i = 0; i < mNumElements; i++)
			{
				mElements[i] = null;
			}
			mElements = null;
		}

		public virtual void SetMaxNumElements(int maxNumElements)
		{
			mMaxNumElements = maxNumElements;
			Component[] array = AllocComponentTable();
			int num = ((mMaxNumElements < mNumElements) ? mMaxNumElements : mNumElements);
			for (int i = 0; i < num; i++)
			{
				array[i] = mElements[i];
			}
			mElements = array;
		}

		public virtual void SetElementAt(int index, Component element)
		{
			mElements[index] = element;
		}

		public virtual Component GetElementAt(int index)
		{
			return mElements[index];
		}

		public virtual void SetNumElements(int numElements)
		{
			mNumElements = numElements;
		}

		public virtual int GetNumElements()
		{
			return mNumElements;
		}

		public virtual void SetNextArrow(Selection nextArrow)
		{
			mNextArrow = nextArrow;
		}

		public virtual Selection GetNextArrow()
		{
			return mNextArrow;
		}

		public virtual void SetPreviousArrow(Selection previousArrow)
		{
			mPreviousArrow = previousArrow;
		}

		public virtual Selection GetPreviousArrow()
		{
			return mPreviousArrow;
		}

		public virtual void SetIsViewportCentered(bool newIsViewportCentered)
		{
			mIsViewportCentered = newIsViewportCentered;
		}

		public virtual void SetScrollerViewport(Viewport scrollerViewport)
		{
			mScrollerViewport = scrollerViewport;
		}

		public virtual Viewport GetScrollerViewport()
		{
			return mScrollerViewport;
		}

		public virtual F32 GetScrollbarRatio()
		{
			Component firstElement = GetFirstElement();
			Vector2_short vector2_short = new Vector2_short();
			mScrollerViewport.GetRequiredOffsetChange(vector2_short, firstElement, false);
			if (mIsVertical)
			{
				int verticalTotalScrollingSize = GetVerticalTotalScrollingSize();
				if (mScrollerViewport.GetRectHeight() >= verticalTotalScrollingSize)
				{
					return F32.Zero(16);
				}
				return new F32(-vector2_short.GetY(), 16).Div(new F32(verticalTotalScrollingSize - mScrollerViewport.GetRectHeight(), 16), 16);
			}
			int horizontalTotalScrollingSize = GetHorizontalTotalScrollingSize();
			if (mScrollerViewport.GetRectWidth() >= horizontalTotalScrollingSize)
			{
				return F32.Zero(16);
			}
			return new F32(-vector2_short.GetX(), 16).Div(new F32(horizontalTotalScrollingSize - mScrollerViewport.GetRectWidth(), 16), 16);
		}

		public virtual short GetVisibleWindowSize()
		{
			if (mIsVertical)
			{
				return mScrollerViewport.GetRectHeight();
			}
			return mScrollerViewport.GetRectWidth();
		}

		public virtual short GetTotalScrollingSize()
		{
			if (!mIsVertical)
			{
				return GetHorizontalTotalScrollingSize();
			}
			return GetVerticalTotalScrollingSize();
		}

		public virtual short GetFirstElemOffset()
		{
			if (mIsVertical)
			{
				return mScrollerViewport.GetOffsetY();
			}
			return mScrollerViewport.GetOffsetX();
		}

		public virtual void SetVertical(bool isVertical)
		{
			mIsVertical = isVertical;
		}

		public virtual bool IsVertical()
		{
			return mIsVertical;
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mNextArrow = Selection.Cast(p.SerializePointer(97, true, false), null);
			mPreviousArrow = Selection.Cast(p.SerializePointer(97, true, false), null);
			mScrollerViewport = Viewport.Cast(p.SerializePointer(68, true, false), null);
			mIsVertical = p.SerializeIntrinsic(mIsVertical);
			mIsViewportCentered = p.SerializeIntrinsic(mIsViewportCentered);
			mNumElements = p.SerializeIntrinsic(mNumElements);
			mMaxNumElements = p.SerializeIntrinsic(mMaxNumElements);
			mElements = AllocComponentTable();
			for (int i = 0; i < mNumElements; i++)
			{
				Component component = null;
				component = Component.Cast(p.SerializePointer(67, true, false), null);
				mElements[i] = component;
			}
			if (p.IsReading())
			{
				ResetScroller();
			}
		}

		public override bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			switch (msg)
			{
			case -108:
				ResetScroller();
				return true;
			case -128:
				FocusGained(intParam);
				return false;
			case -125:
			{
				bool flag = mNextArrow != null && mNextArrow.IsSelfOrAncestorOf(source);
				bool flag2 = mPreviousArrow != null && mPreviousArrow.IsSelfOrAncestorOf(source);
				if ((intParam == 0 || 3 == intParam) && (flag || flag2))
				{
					SendMsg(this, -105, 1);
				}
				else if ((1 == intParam || 2 == intParam) && !mArrowBehaviorCameFromScroller)
				{
					if (flag)
					{
						OnScrollEvent(1);
					}
					else if (flag2)
					{
						OnScrollEvent(-1);
					}
				}
				break;
			}
			case -121:
			case -120:
			case -119:
			{
				int num = 0;
				if (mIsVertical)
				{
					switch (intParam)
					{
					case 1:
						num = -1;
						break;
					case 2:
						num = 1;
						break;
					}
				}
				else
				{
					switch (intParam)
					{
					case 3:
						num = -1;
						break;
					case 4:
						num = 1;
						break;
					}
				}
				if (num != 0)
				{
					if (mNumElements <= 0)
					{
						return true;
					}
					if (mNextArrow != null)
					{
						Selection selection = ((num == 1) ? mNextArrow : mPreviousArrow);
						selection.SetPushedState(mArrowBehaviorCameFromScroller = -121 != msg);
					}
					else if (-121 == msg)
					{
						SendMsg(this, -105, 1);
					}
					if (msg == -119)
					{
						OnScrollEvent(num);
					}
					return true;
				}
				if (IsAppropriateHotkey(msg, intParam))
				{
					return true;
				}
				break;
			}
			}
			return base.OnDefaultMsg(source, msg, intParam);
		}

		public virtual void FocusGained(int entering)
		{
			if (mNextArrow != null)
			{
				bool selectedState = entering != 0;
				mNextArrow.SetSelectedState(selectedState);
				mPreviousArrow.SetSelectedState(selectedState);
			}
		}

		public virtual void ResetScroller()
		{
			if (mNumElements > 0)
			{
				Component firstElement = GetFirstElement();
				Component lastElement = GetLastElement();
				Viewport viewport = mScrollerViewport;
				bool flag = (mIsVertical ? (GetVerticalTotalScrollingSize() > viewport.GetRectHeight()) : (GetHorizontalTotalScrollingSize() > viewport.GetRectWidth()));
				short inPointX = viewport.GetOffsetX();
				short inPointY = viewport.GetOffsetY();
				if (mIsViewportCentered && !flag)
				{
					if (mIsVertical)
					{
						inPointY = (short)(-(viewport.GetRectHeight() + firstElement.GetRectTop() - lastElement.GetBottom()) / 2);
					}
					else
					{
						inPointX = (short)(-(viewport.GetRectWidth() + firstElement.GetRectLeft() - lastElement.GetRight()) / 2);
					}
				}
				else if (mIsVertical)
				{
					inPointY = firstElement.GetRectTop();
				}
				else
				{
					inPointX = firstElement.GetRectLeft();
				}
				viewport.OffsetTo(inPointX, inPointY);
			}
			UpdateScroller();
		}

		public virtual void UpdateScroller()
		{
			CheckIfArrowsAreNeeded();
			UpdateArrowsEnabledState();
		}

		public virtual short GetScrollingPosition()
		{
			if (!mIsVertical)
			{
				return mScrollerViewport.GetOffsetX();
			}
			return mScrollerViewport.GetOffsetY();
		}

		public virtual void SetScrollingPosition(short pos)
		{
			short inPointX = 0;
			short inPointY = 0;
			if (mIsVertical)
			{
				inPointY = pos;
			}
			else
			{
				inPointX = pos;
			}
			mScrollerViewport.OffsetTo(inPointX, inPointY);
		}

		public virtual Component[] AllocComponentTable()
		{
			return new Component[mMaxNumElements];
		}

		public virtual void UpdateArrowsEnabledState()
		{
			if (mNextArrow != null && mNumElements > 0)
			{
				Vector2_short vector2_short = new Vector2_short();
				mScrollerViewport.GetRequiredOffsetChange(vector2_short, GetFirstElement(), false);
				mPreviousArrow.SetEnabledState(vector2_short.GetY() < 0);
				mScrollerViewport.GetRequiredOffsetChange(vector2_short, GetLastElement(), true);
				mNextArrow.SetEnabledState(vector2_short.GetY() > 0);
			}
		}

		public virtual void CheckIfArrowsAreNeeded()
		{
			if (mNextArrow != null)
			{
				bool visible = false;
				if (mNumElements > 0)
				{
					Vector2_short vector2_short = new Vector2_short();
					mScrollerViewport.GetRequiredOffsetChange(vector2_short, GetFirstElement(), false);
					bool flag = vector2_short.GetX() == 0;
					bool flag2 = vector2_short.GetY() == 0;
					mScrollerViewport.GetRequiredOffsetChange(vector2_short, GetLastElement(), true);
					bool flag3 = vector2_short.GetX() == 0;
					bool flag4 = vector2_short.GetY() == 0;
					visible = !flag || !flag2 || !flag3 || !flag4;
				}
				mNextArrow.SetVisible(visible);
				mPreviousArrow.SetVisible(visible);
			}
		}

		public virtual void OnScrollEvent(int advance, bool updateSelection)
		{
			int num = -1;
			Vector2_short vector2_short = new Vector2_short();
			Component[] array = mElements;
			if (advance == 1)
			{
				for (int i = 0; i < mNumElements; i++)
				{
					mScrollerViewport.GetRequiredOffsetChange(vector2_short, array[i], true);
					if (((mIsVertical && vector2_short.GetY() > 0) || (!mIsVertical && vector2_short.GetX() > 0)) && array[i].IsVisible())
					{
						num = i;
						break;
					}
				}
			}
			else
			{
				for (int num2 = mNumElements - 1; num2 >= 0; num2--)
				{
					mScrollerViewport.GetRequiredOffsetChange(vector2_short, array[num2], false);
					if (((mIsVertical && vector2_short.GetY() < 0) || (!mIsVertical && vector2_short.GetX() < 0)) && array[num2].IsVisible())
					{
						num = num2;
						break;
					}
				}
			}
			if (num >= 0)
			{
				if (array[num] is Text)
				{
					short lineHeight = ((Text)array[num]).GetLineHeight();
					mScrollerViewport.OffsetBy(0, (short)(advance * lineHeight));
				}
				else
				{
					mScrollerViewport.ChangeOffsetToShow(array[num], advance == 1);
				}
				UpdateArrowsEnabledState();
				SendMsg(this, -105, 0);
			}
		}

		public virtual bool IsAppropriateHotkey(int msg, int key)
		{
			return false;
		}

		public virtual Component GetFirstElement()
		{
			Component[] array = mElements;
			for (int i = 0; i < mNumElements; i++)
			{
				if (array[i].IsVisible() && mScrollerViewport.IsSelfOrAncestorOf(array[i].GetViewport()))
				{
					return array[i];
				}
			}
			return array[0];
		}

		public virtual Component GetLastElement()
		{
			Component[] array = mElements;
			for (int num = mNumElements - 1; num >= 0; num--)
			{
				if (array[num].IsVisible() && mScrollerViewport.IsSelfOrAncestorOf(array[num].GetViewport()))
				{
					return array[num];
				}
			}
			return mElements[mNumElements - 1];
		}

		public virtual short GetVerticalTotalScrollingSize()
		{
			Component lastElement = GetLastElement();
			return (short)(lastElement.GetAbsoluteTop() + lastElement.GetRectHeight() - GetFirstElement().GetAbsoluteTop());
		}

		public virtual short GetHorizontalTotalScrollingSize()
		{
			Component lastElement = GetLastElement();
			return (short)(lastElement.GetAbsoluteLeft() + lastElement.GetRectWidth() - GetFirstElement().GetAbsoluteLeft());
		}

		public virtual void OnScrollEvent(int advance)
		{
			OnScrollEvent(advance, true);
		}

		public static Scroller[] InstArrayScroller(int size)
		{
			Scroller[] array = new Scroller[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Scroller();
			}
			return array;
		}

		public static Scroller[][] InstArrayScroller(int size1, int size2)
		{
			Scroller[][] array = new Scroller[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scroller[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scroller();
				}
			}
			return array;
		}

		public static Scroller[][][] InstArrayScroller(int size1, int size2, int size3)
		{
			Scroller[][][] array = new Scroller[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scroller[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scroller[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Scroller();
					}
				}
			}
			return array;
		}
	}
}
