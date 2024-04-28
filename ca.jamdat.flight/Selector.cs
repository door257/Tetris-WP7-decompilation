using System;

namespace ca.jamdat.flight
{
	public class Selector : Scroller
	{
		public new const sbyte typeNumber = 96;

		public new const sbyte typeID = 96;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte FlagSkipDisabled = 1;

		public const sbyte FlagLooping = 2;

		public const sbyte FlagTakeFocus = 4;

		private bool mIsSelectionScrollerEnabled;

		public int mCurrentSelectionIndex;

		public int mLastUpdatedSelectionIndex;

		public sbyte mFlags;

		public bool IsSelectionScrollerEnabled
		{
			get
			{
				return mIsSelectionScrollerEnabled;
			}
			set
			{
				mIsSelectionScrollerEnabled = value;
			}
		}

		public event EventHandler EventIndexChanged;

		public static Selector Cast(object o, Selector _)
		{
			return (Selector)o;
		}

		public override sbyte GetTypeID()
		{
			return 96;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public Selector()
		{
			mCurrentSelectionIndex = -1;
			mLastUpdatedSelectionIndex = -1;
			mFlags = 1;
			mIsVertical = false;
			mIsSelectionScrollerEnabled = false;
		}

		public Selector(int maxNumElements)
			: base(maxNumElements)
		{
			mCurrentSelectionIndex = -1;
			mFlags = 1;
			mIsVertical = false;
			mIsSelectionScrollerEnabled = false;
		}

		public override bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			if (-124 == msg)
			{
				for (int i = 0; i < GetNumElements(); i++)
				{
					Selection selectionAt = GetSelectionAt(i);
					if (selectionAt.IsSelfOrAncestorOf(source))
					{
						SetSingleSelection(i, GetTakeFocusOnSync());
						SendMsg(this, -105, intParam);
						i = GetNumElements();
					}
				}
			}
			return base.OnDefaultMsg(source, msg, intParam);
		}

		public virtual void SetSelectionAt(int index, Selection selection)
		{
			base.SetElementAt(index, selection);
			if (mCurrentSelectionIndex == index)
			{
				((Selection)mElements[mCurrentSelectionIndex]).SetSelectedState(true);
			}
		}

		public virtual Selection GetSelectionAt(int index)
		{
			return (Selection)base.GetElementAt(index);
		}

		public virtual int GetSingleSelection()
		{
			return mCurrentSelectionIndex;
		}

		public virtual void SetSingleSelection(int selectionIndex, bool takeFocus, bool propagateSelectionInParent)
		{
			SetSingleSelection(selectionIndex, takeFocus, propagateSelectionInParent, true);
		}

		public virtual void SetNumSelections(int numSelections)
		{
			mNumElements = numSelections;
		}

		public virtual int GetNumSelections()
		{
			return mNumElements;
		}

		public virtual void SetLooping(bool looping)
		{
			if (looping)
			{
				mFlags |= 2;
			}
			else
			{
				mFlags &= -3;
			}
		}

		public virtual bool GetLooping()
		{
			return (mFlags & 2) != 0;
		}

		public virtual void SetSkipDisabledSelection(bool isDisabled)
		{
			if (isDisabled)
			{
				mFlags |= 1;
			}
			else
			{
				mFlags &= -2;
			}
		}

		public virtual bool GetSkipDisabledSelection()
		{
			return (mFlags & 1) != 0;
		}

		public virtual void SetTakeFocusOnSync(bool takeFocusOnSync)
		{
			if (takeFocusOnSync)
			{
				mFlags |= 4;
			}
			else
			{
				mFlags &= -5;
			}
		}

		public virtual bool GetTakeFocusOnSync()
		{
			return (mFlags & 4) != 0;
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			int num = 0;
			if (p.IsReading())
			{
				num = p.SerializeIntrinsic(num);
			}
			else
			{
				mCurrentSelectionIndex = p.SerializeIntrinsic(mCurrentSelectionIndex);
			}
			mFlags = p.SerializeIntrinsic(mFlags);
			if (p.IsReading() && num >= 0)
			{
				SetSingleSelection(num);
			}
		}

		public override Component ForwardFocus()
		{
			if (GetSingleSelection() == -1)
			{
				return this;
			}
			return GetElementAt(GetSingleSelection()).ForwardFocus();
		}

		public override void UpdateArrowsEnabledState()
		{
			if (mNextArrow == null)
			{
				return;
			}
			if (!GetLooping())
			{
				int num = mCurrentSelectionIndex;
				int num2 = mNumElements - 1;
				int num3 = -1;
				int num4 = -1;
				for (int i = 0; i < mNumElements; i++)
				{
					if (num4 == -1 && ((Selection)mElements[i]).GetEnabledState())
					{
						num4 = i;
					}
					if (num3 == -1 && ((Selection)mElements[num2 - i]).GetEnabledState())
					{
						num3 = num2 - i;
					}
					if ((num4 | num3) != -1)
					{
						break;
					}
				}
				mNextArrow.SetEnabledState(num < num3);
				mPreviousArrow.SetEnabledState(num > num4);
			}
			else
			{
				mNextArrow.SetEnabledState(true);
				mPreviousArrow.SetEnabledState(true);
			}
		}

		public override void OnScrollEvent(int advance, bool updateSelection)
		{
			if (IsSelectionScrollerEnabled)
			{
				base.OnScrollEvent(advance, updateSelection);
			}
			else
			{
				int num = mCurrentSelectionIndex;
				int num2 = num;
				int num3 = mNumElements;
				bool looping = GetLooping();
				bool skipDisabledSelection = GetSkipDisabledSelection();
				do
				{
					num2 += advance;
					int num4 = num2;
					if (num2 < 0)
					{
						num2 = num3 - 1;
					}
					if (num2 >= num3)
					{
						num2 = 0;
					}
					if (looping)
					{
						if (num2 == num)
						{
							break;
						}
					}
					else if (num2 != num4)
					{
						num2 = num4 - advance;
						if (skipDisabledSelection && !((Selection)mElements[num2]).GetEnabledState())
						{
							num2 = mCurrentSelectionIndex;
						}
						break;
					}
				}
				while (skipDisabledSelection && !((Selection)mElements[num2]).GetEnabledState());
				if (updateSelection)
				{
					if (mLastUpdatedSelectionIndex != mCurrentSelectionIndex)
					{
						mCurrentSelectionIndex = mLastUpdatedSelectionIndex;
					}
					SetSingleSelection(num2, true, true, false);
				}
				else
				{
					mCurrentSelectionIndex = num2;
				}
			}
			OnIndexChanged();
		}

		public override bool IsAppropriateHotkey(int msg, int key)
		{
			bool skipDisabledSelection = GetSkipDisabledSelection();
			for (int i = 0; i < mNumElements; i++)
			{
				Selection selection = (Selection)mElements[i];
				if ((selection.GetEnabledState() || !skipDisabledSelection) && key == selection.GetHotKey())
				{
					SetSingleSelection(i, true, true, true);
					selection.SetPushedState(msg != -121);
					return true;
				}
			}
			return false;
		}

		public virtual void SetSingleSelection(int selectionIndex, bool takeFocus, bool propagateSelectionInParent, bool isFinalState)
		{
			int num = mCurrentSelectionIndex;
			mCurrentSelectionIndex = selectionIndex;
			if (num >= 0 && num != selectionIndex && num < mNumElements)
			{
				((Selection)mElements[num]).SetSelectedState(false);
			}
			Selection selection = (Selection)mElements[selectionIndex];
			mScrollerViewport.ChangeOffsetToShow(selection, false);
			selection.SetSelectedState(true, propagateSelectionInParent, isFinalState);
			UpdateArrowsEnabledState();
			if (takeFocus)
			{
				selection.TakeFocus();
			}
			mLastUpdatedSelectionIndex = mCurrentSelectionIndex;
			OnIndexChanged();
		}

		public virtual void SetSingleSelection(int selectionIndex)
		{
			SetSingleSelection(selectionIndex, false);
		}

		public virtual void SetSingleSelection(int selectionIndex, bool takeFocus)
		{
			SetSingleSelection(selectionIndex, takeFocus, false);
		}

		public override void OnScrollEvent(int advance)
		{
			OnScrollEvent(advance, true);
		}

		public static Selector[] InstArraySelector(int size)
		{
			Selector[] array = new Selector[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Selector();
			}
			return array;
		}

		public static Selector[][] InstArraySelector(int size1, int size2)
		{
			Selector[][] array = new Selector[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Selector[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Selector();
				}
			}
			return array;
		}

		public static Selector[][][] InstArraySelector(int size1, int size2, int size3)
		{
			Selector[][][] array = new Selector[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Selector[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Selector[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Selector();
					}
				}
			}
			return array;
		}

		protected void OnIndexChanged()
		{
			if (this.EventIndexChanged != null)
			{
				this.EventIndexChanged(this, null);
			}
		}
	}
}
