using System;

namespace ca.jamdat.flight
{
	public class Selection : Viewport
	{
		public new const sbyte typeNumber = 97;

		public new const sbyte typeID = 97;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte releasedState = 0;

		public const sbyte pushedState = 1;

		public const sbyte pushedRepeatState = 2;

		public const sbyte canceledState = 3;

		public const sbyte unselectedState = 0;

		public const sbyte selectedState = 1;

		public const sbyte disabledState = 0;

		public const sbyte enabledState = 1;

		public const int STANDARD_DRAG_THRESHOLD_MS = 250;

		public const int STANDARD_SWIPE_THRESHOLD_MS = 100;

		public const int DRAG_HYSTERISIS = 7;

		public bool mEnabled;

		public bool mSelected;

		public bool mPushed;

		public sbyte mHotKey;

		public short mCommand;

		public Component mForwardFocus;

		public bool mSendRepeatOnPush;

		public int mTimeSinceLastRepeat;

		private bool mWasDraggedSinceDown;

		private int mDragStartedMs;

		private bool mSelectionBlockEnabled;

		private int mDragSelectionBlockThreshold;

		public int mPenDownCoordX;

		public int mPenDownCoordY;

		public Selection()
		{
			SetPassThrough(false);
		}

		public static Selection Cast(object o, Selection _)
		{
			return (Selection)o;
		}

		public override sbyte GetTypeID()
		{
			return 97;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			UnRegisterInGlobalTime();
		}

		public virtual void SetSelectedState(bool selected, bool propagateSelectionInParent)
		{
			SetSelectedState(selected, propagateSelectionInParent, true);
		}

		public virtual bool GetSelectedState()
		{
			return mSelected;
		}

		public virtual void SetEnabledState(bool enabled)
		{
			if (enabled != mEnabled)
			{
				if (!enabled)
				{
					CancelPush();
					SetSelectedState(false);
				}
				mEnabled = enabled;
				SendMsg(this, -126, enabled ? 1 : 0);
				if (!mEnabled && DescendentOrSelfHasFocus())
				{
					TakeFocus();
				}
			}
		}

		public virtual bool GetEnabledState()
		{
			return mEnabled;
		}

		public virtual void CancelPush()
		{
			if (mPushed)
			{
				SetPushed(false);
				SendMsg(this, -125, 3);
			}
		}

		public virtual void SetPushedState(bool onDown)
		{
			if (GetEnabledState() && onDown != mPushed)
			{
				SetPushed(onDown);
				if (!IsSelectionBlockActivated())
				{
					SendMsg(this, -125, onDown ? 1 : 0);
				}
			}
		}

		public virtual bool GetPushedState()
		{
			return mPushed;
		}

		public virtual void SetHotKey(int hotkey)
		{
			mHotKey = (sbyte)hotkey;
		}

		public virtual int GetHotKey()
		{
			return mHotKey;
		}

		public virtual void SetCommand(short command)
		{
			mCommand = command;
		}

		public virtual short GetCommand()
		{
			return mCommand;
		}

		public virtual void SetForwardFocus(Component forwardFocus)
		{
			mForwardFocus = forwardFocus;
		}

		public override Component ForwardFocus()
		{
			if (mEnabled && mForwardFocus != null)
			{
				return mForwardFocus;
			}
			return this;
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mForwardFocus = Component.Cast(p.SerializePointer(67, true, false), null);
			mEnabled = p.SerializeIntrinsic(mEnabled);
			mHotKey = p.SerializeIntrinsic(mHotKey);
			mCommand = p.SerializeIntrinsic(mCommand);
			mSendRepeatOnPush = p.SerializeIntrinsic(mSendRepeatOnPush);
		}

		public override bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			if (msg >= -121 && msg <= -119 && intParam == 5)
			{
				SetPushedState(msg != -121);
				return true;
			}
			switch (msg)
			{
			case -117:
				mPenDownCoordX = FlPenManager.GetPenPositionX(intParam);
				mPenDownCoordY = FlPenManager.GetPenPositionY(intParam);
				OnPenDown();
				return false;
			case -116:
				if (GetEnabledState())
				{
					int num = FlMath.Absolute(FlPenManager.GetPenPositionX(intParam) - mPenDownCoordX);
					int num2 = FlMath.Absolute(FlPenManager.GetPenPositionY(intParam) - mPenDownCoordY);
					if (num > 7 || num2 > 7)
					{
						ActivateSelectionBlock();
					}
				}
				OnPenDown();
				return false;
			case -118:
				OnPenUp();
				return false;
			case -115:
			case -114:
				OnPenDragOut();
				return true;
			default:
				return base.OnDefaultMsg(source, msg, intParam);
			}
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			mTimeSinceLastRepeat += deltaTime;
			if (mTimeSinceLastRepeat > 150)
			{
				SendMsg(this, -125, 2);
				mTimeSinceLastRepeat = 0;
			}
		}

		public virtual void SetSendRepeatOnPush(bool repeatOnPush)
		{
			mSendRepeatOnPush = repeatOnPush;
			SetPushed(mPushed);
		}

		public virtual bool GetSendRepeatOnPush()
		{
			return mSendRepeatOnPush;
		}

		public virtual void SetSelectedState(bool selected, bool propagateSelectionInParent, bool isFinalState)
		{
			if (GetEnabledState())
			{
				if (selected != mSelected)
				{
					mSelected = selected;
					SendMsg(this, -127, selected ? 1 : 0);
				}
				if (selected && propagateSelectionInParent)
				{
					SendMsg(this, -124, isFinalState ? 1 : 0);
				}
			}
		}

		public virtual void SetPushed(bool isPushed)
		{
			mPushed = isPushed;
			if (mSendRepeatOnPush)
			{
				if (isPushed)
				{
					mTimeSinceLastRepeat = -350;
					RegisterInGlobalTime();
				}
				else
				{
					UnRegisterInGlobalTime();
				}
			}
		}

		public virtual void OnPenUp()
		{
			if (GetEnabledState() && GetSelectedState())
			{
				SetPushedState(false);
			}
			ResetOnPenDragPushBlock();
		}

		public virtual void OnPenDown()
		{
			if (GetEnabledState())
			{
				SetSelectedState(true, true);
				SetPushedState(true);
			}
		}

		public virtual void OnPenDragOut()
		{
			ResetOnPenDragPushBlock();
			CancelPush();
		}

		public virtual void SetSelectedState(bool selected)
		{
			SetSelectedState(selected, false);
		}

		protected virtual void ResetOnPenDragPushBlock()
		{
			mWasDraggedSinceDown = false;
			mDragStartedMs = 0;
		}

		protected virtual bool IsSelectionBlockActivated()
		{
			if (mDragSelectionBlockThreshold > 0 && mWasDraggedSinceDown)
			{
				return (int)FlApplication.GetRealTime() - mDragStartedMs <= mDragSelectionBlockThreshold;
			}
			return false;
		}

		protected virtual void ActivateSelectionBlock()
		{
			mWasDraggedSinceDown = true;
			mDragStartedMs = (int)FlApplication.GetRealTime();
		}

		public void SetDragSelectionBlockThreshold(int threshold)
		{
			mDragSelectionBlockThreshold = threshold;
		}

		public static Selection[] InstArraySelection(int size)
		{
			Selection[] array = new Selection[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Selection();
			}
			return array;
		}

		public static Selection[][] InstArraySelection(int size1, int size2)
		{
			Selection[][] array = new Selection[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Selection[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Selection();
				}
			}
			return array;
		}

		public static Selection[][][] InstArraySelection(int size1, int size2, int size3)
		{
			Selection[][][] array = new Selection[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Selection[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Selection[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Selection();
					}
				}
			}
			return array;
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			base.OnDraw(displayContext);
		}
	}
}
