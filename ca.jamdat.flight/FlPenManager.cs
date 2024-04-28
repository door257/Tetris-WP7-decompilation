namespace ca.jamdat.flight
{
	public class FlPenManager
	{
		public const sbyte MsgDestinationAuto = 0;

		public const sbyte MsgDestinationListenersOnly = 1;

		public const sbyte MsgDestinationTopMostOnly = 2;

		public short[] mLastTouchedPosition;

		public bool mActived;

		public bool mEventForwardingToTopComponentEnabled;

		public PtrArray_Component mListeners;

		public Component[] mLastTouchedComponent;

		public FlPenManager()
		{
			mEventForwardingToTopComponentEnabled = true;
			mListeners = new PtrArray_Component();
			mLastTouchedPosition = new short[2];
			mLastTouchedComponent = new Component[1];
			for (int i = 0; i < 1; i++)
			{
				mLastTouchedComponent[i] = null;
			}
		}

		public virtual void destruct()
		{
			mLastTouchedComponent = null;
			mLastTouchedPosition = null;
			mListeners.Clear();
		}

		public static FlPenManager Get()
		{
			FrameworkGlobals instance = FrameworkGlobals.GetInstance();
			if (instance.mFlPenManager == null)
			{
				instance.mFlPenManager = new FlPenManager();
			}
			return instance.mFlPenManager;
		}

		public virtual sbyte RequestPenIndex()
		{
			return 0;
		}

		public virtual void FreePenIndex(sbyte index)
		{
		}

		public virtual void Reset()
		{
			if (mActived)
			{
				Deactivate();
				Activate();
			}
		}

		public virtual void Deactivate()
		{
			mActived = false;
			SendDeactivationMsg(0);
		}

		public virtual void Activate()
		{
			mActived = true;
		}

		public virtual bool IsActive()
		{
			return mActived;
		}

		public virtual void OnPenDown(short penX, short penY, sbyte penIdx)
		{
			int intParam = EncodeParam(penX, penY, penIdx);
			if (IsActive())
			{
				int num = penIdx + 1;
				mLastTouchedPosition[penIdx] = penX;
				mLastTouchedPosition[num] = penY;
				mLastTouchedComponent[penIdx] = FlApplication.GetInstance().GetHitTestComponent(penX, penY);
				SendPenMsg(mLastTouchedComponent[penIdx], -117, intParam);
			}
			FlApplication.GetInstance().ResetLastUserInputTime();
		}

		public virtual void OnPenUp(short penX, short penY, sbyte penIdx)
		{
			if (IsActive())
			{
				SendPenMsg(mLastTouchedComponent[penIdx], -118, EncodeParam(penX, penY, penIdx));
				mLastTouchedComponent[penIdx] = null;
			}
			FlApplication.GetInstance().ResetLastUserInputTime();
		}

		public virtual void OnPenDrag(short penX, short penY, sbyte penIdx)
		{
			if (IsActive())
			{
				int num = penIdx + 1;
				if (mLastTouchedPosition[penIdx] != penX || mLastTouchedPosition[num] != penY)
				{
					mLastTouchedPosition[penIdx] = penX;
					mLastTouchedPosition[num] = penY;
					Component hitTestComponent = FlApplication.GetInstance().GetHitTestComponent(penX, penY);
					Component component = mLastTouchedComponent[penIdx];
					int intParam = EncodeParam(penX, penY, penIdx);
					if (hitTestComponent != component)
					{
						SendPenMsg(component, -115, intParam, 2);
						mLastTouchedComponent[penIdx] = hitTestComponent;
					}
					SendPenMsg(hitTestComponent, -116, intParam);
				}
			}
			FlApplication.GetInstance().ResetLastUserInputTime();
		}

		public static short GetPenPositionX(int param)
		{
			return (short)((param >> 18) & 0x3FFF);
		}

		public static short GetPenPositionY(int param)
		{
			return (short)((param >> 4) & 0x3FFF);
		}

		public static sbyte GetPenIndex(int param)
		{
			return (sbyte)(param & 0xF);
		}

		public virtual void OnAppPaused()
		{
		}

		public virtual void OnAppResumed()
		{
			Reset();
		}

		public static int EncodeParam(short penX, short penY, sbyte penIdx)
		{
			int num = penX << 18;
			int num2 = penY << 4;
			return num | num2 | penIdx;
		}

		public virtual void EnableEventForwardingToTopComponent(bool enable)
		{
			if (!enable)
			{
				SendDeactivationMsg(2);
			}
			mEventForwardingToTopComponentEnabled = enable;
		}

		public virtual bool IsEventForwardingToTopComponentEnabled()
		{
			return mEventForwardingToTopComponentEnabled;
		}

		public virtual void AddListener(Component listener)
		{
			mListeners.Insert(listener);
		}

		public virtual void RemoveListener(Component listener)
		{
			mListeners.Remove(listener);
		}

		public virtual bool IsRegisteredListener(Component listener)
		{
			return mListeners.Find(listener) >= 0;
		}

		public virtual bool IsPenDown(int penIdx)
		{
			return false;
		}

		public virtual int GetPenDownCount()
		{
			return 0;
		}

		public virtual sbyte GetFreePenIndex()
		{
			return 0;
		}

		public virtual void SendDeactivationMsg(sbyte msgDestination)
		{
			short penX = -1;
			short penY = -1;
			for (int i = 0; i < 1; i++)
			{
				SendPenMsg(mLastTouchedComponent[i], -114, EncodeParam(penX, penY, (sbyte)i), msgDestination);
				if (msgDestination == 0 || msgDestination == 2)
				{
					mLastTouchedComponent[i] = null;
				}
			}
		}

		public virtual void SendPenMsg(Component lastTouchedComponent, sbyte msg, int intParam, sbyte msgDestination)
		{
			if ((msgDestination == 0 || msgDestination == 2) && lastTouchedComponent != null && mEventForwardingToTopComponentEnabled)
			{
				lastTouchedComponent.SendMsg(lastTouchedComponent, msg, intParam);
			}
			if (msgDestination == 0 || msgDestination == 1)
			{
				for (int i = mListeners.Start(); i < mListeners.End(); i++)
				{
					mListeners.GetAt(i).SendMsg(lastTouchedComponent, msg, intParam);
				}
			}
		}

		public virtual void OnPenDown(short penX, short penY)
		{
			OnPenDown(penX, penY, 0);
		}

		public virtual void OnPenUp(short penX, short penY)
		{
			OnPenUp(penX, penY, 0);
		}

		public virtual void OnPenDrag(short penX, short penY)
		{
			OnPenDrag(penX, penY, 0);
		}

		public virtual void SendPenMsg(Component lastTouchedComponent, sbyte msg, int intParam)
		{
			SendPenMsg(lastTouchedComponent, msg, intParam, 0);
		}

		public static FlPenManager[] InstArrayFlPenManager(int size)
		{
			FlPenManager[] array = new FlPenManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlPenManager();
			}
			return array;
		}

		public static FlPenManager[][] InstArrayFlPenManager(int size1, int size2)
		{
			FlPenManager[][] array = new FlPenManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlPenManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlPenManager();
				}
			}
			return array;
		}

		public static FlPenManager[][][] InstArrayFlPenManager(int size1, int size2, int size3)
		{
			FlPenManager[][][] array = new FlPenManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlPenManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlPenManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlPenManager();
					}
				}
			}
			return array;
		}
	}
}
