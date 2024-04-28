using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TouchReceiver : PenMsgReceiver
	{
		public const sbyte kNoDir = 0;

		public const sbyte kNorth = 1;

		public const sbyte kEast = 2;

		public const sbyte kSouth = 3;

		public const sbyte kWest = 4;

		public const sbyte kNoAction = 0;

		public const sbyte kTap = 1;

		public const sbyte kDrag = 2;

		public const sbyte kFlick = 3;

		public BaseScene mCurrentScene;

		public Vector2_short mFirstPenInput;

		public Vector2_short mLastPenInput;

		public sbyte mTouchDirection;

		private sbyte mTouchAction;

		public int mFlickStartTimeMs;

		public short mAccumulatedDragDistanceX;

		public short mAccumulatedDragDistanceY;

		public short mDragStepX;

		public short mDragStepY;

		public ZoneRect[] mTouchZones;

		private bool mMultipleTouchDirectionsWithinSingleDrag;

		private sbyte mLastTouchDirection;

		public TouchReceiver()
		{
			mFirstPenInput = new Vector2_short(new Vector2_short(0, 0));
			mLastPenInput = new Vector2_short(new Vector2_short(0, 0));
			mTouchDirection = 0;
			mTouchAction = 0;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(BaseScene currentScene)
		{
			mCurrentScene = currentScene;
		}

		public virtual void Unload()
		{
			mCurrentScene = null;
		}

		public override void OnPenDown(short x, short y, sbyte penIndex)
		{
			if (mCurrentScene.IsReadyForCommands())
			{
				OnProcessPenDown(x, y, penIndex);
				if (penIndex == 0)
				{
					mTouchAction = 1;
					mFlickStartTimeMs = (int)FlApplication.GetRealTime();
					mLastPenInput.SetX(x);
					mLastPenInput.SetY(y);
					mFirstPenInput.SetX(x);
					mFirstPenInput.SetY(y);
					mAccumulatedDragDistanceX = 0;
					mAccumulatedDragDistanceY = 0;
					mDragStepX = 0;
					mDragStepY = 0;
				}
			}
		}

		public override void OnPenUp(short x, short y, sbyte penIndex)
		{
			if (!mCurrentScene.IsReadyForCommands())
			{
				return;
			}
			OnProcessPenUp(x, y, penIndex);
			if (penIndex != 0)
			{
				return;
			}
			if (mTouchDirection != 0)
			{
				int num = (int)FlApplication.GetRealTime() - mFlickStartTimeMs;
				if (num < 350 && !mMultipleTouchDirectionsWithinSingleDrag)
				{
					mTouchAction = 3;
					OnProcessPenSwipe();
				}
			}
			else if (mTouchAction == 1)
			{
				OnPenTap(x, y);
			}
			mTouchDirection = (mLastTouchDirection = 0);
			mTouchAction = 0;
			mMultipleTouchDirectionsWithinSingleDrag = false;
		}

		public override void OnPenDrag(short x, short y, sbyte penIndex)
		{
			if (!mCurrentScene.IsReadyForCommands() || penIndex != 0)
			{
				return;
			}
			short dragDistanceX = GetDragDistanceX(mLastPenInput, new Vector2_short(x, y));
			short dragDistanceY = GetDragDistanceY(mLastPenInput, new Vector2_short(x, y));
			if (dragDistanceX == 0 && dragDistanceY == 0)
			{
				return;
			}
			mAccumulatedDragDistanceX += dragDistanceX;
			mAccumulatedDragDistanceY += dragDistanceY;
			mLastPenInput.SetX(x);
			mLastPenInput.SetY(y);
			if (penIndex != 0)
			{
				return;
			}
			if (mTouchDirection != 0)
			{
				mLastTouchDirection = mTouchDirection;
			}
			if (mAccumulatedDragDistanceX != 0 && FlMath.Absolute(mAccumulatedDragDistanceX) >= FlMath.Absolute(mAccumulatedDragDistanceY))
			{
				mDragStepX = GetDragStepX(mAccumulatedDragDistanceX);
				if (mDragStepX > 0)
				{
					mTouchAction = 2;
					mTouchDirection = 2;
					mAccumulatedDragDistanceX = 0;
					mAccumulatedDragDistanceY = 0;
				}
				else if (mDragStepX < 0)
				{
					mTouchAction = 2;
					mTouchDirection = 4;
					mAccumulatedDragDistanceX = 0;
					mAccumulatedDragDistanceY = 0;
				}
				else
				{
					mTouchDirection = 0;
				}
			}
			if (mAccumulatedDragDistanceY != 0 && FlMath.Absolute(mAccumulatedDragDistanceX) < FlMath.Absolute(mAccumulatedDragDistanceY))
			{
				mDragStepY = GetDragStepY(mAccumulatedDragDistanceY);
				if (mDragStepY > 0)
				{
					mTouchAction = 2;
					mTouchDirection = 3;
					mAccumulatedDragDistanceX = 0;
					mAccumulatedDragDistanceY = 0;
				}
				else if (mDragStepY < 0)
				{
					mTouchAction = 2;
					mTouchDirection = 1;
					mAccumulatedDragDistanceX = 0;
					mAccumulatedDragDistanceY = 0;
				}
				else
				{
					mTouchDirection = 0;
				}
			}
			if (mLastTouchDirection != 0 && mTouchDirection != 0 && !mMultipleTouchDirectionsWithinSingleDrag)
			{
				mMultipleTouchDirectionsWithinSingleDrag = mLastTouchDirection != mTouchDirection;
			}
			if (ShouldResetFlickStartTime())
			{
				mFlickStartTimeMs = (int)FlApplication.GetRealTime();
			}
		}

		public override void OnPenDragRepeat(short x, short y, sbyte penIndex)
		{
			if (mCurrentScene.IsReadyForCommands())
			{
				OnPenDrag(x, y, penIndex);
				if (mTouchDirection != 0)
				{
					OnProcessPenDrag();
					mFirstPenInput.SetX(mLastPenInput.GetX());
					mFirstPenInput.SetY(mLastPenInput.GetY());
				}
			}
		}

		public virtual void CreateZone(int zoneId, FlRect zoneRect, int command, sbyte zoneType)
		{
			CreateZone(zoneId, zoneRect.GetLeft(), zoneRect.GetTop(), zoneRect.GetWidth(), zoneRect.GetHeight(), command, zoneType);
		}

		public virtual void CreateZone(int zoneId, short zoneX, short zoneY, short zoneW, short zoneH, int command, sbyte zoneType)
		{
			mTouchZones[zoneId] = new ZoneRect();
			mTouchZones[zoneId].SetType(zoneType);
			mTouchZones[zoneId].SetCommand(command);
			mTouchZones[zoneId].SetTopLeftX(zoneX);
			mTouchZones[zoneId].SetTopLeftY(zoneY);
			mTouchZones[zoneId].SetWidth(zoneW);
			mTouchZones[zoneId].SetHeight(zoneH);
		}

		public virtual void AddZoneType(int zoneId, sbyte zoneType)
		{
			mTouchZones[zoneId].AddType(zoneType);
		}

		public virtual void RemoveZoneType(int zoneId, sbyte zoneType)
		{
			mTouchZones[zoneId].RemoveType(zoneType);
		}

		public virtual bool IsRegistered()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			return penInputController.IsRegistered(this);
		}

		public virtual void Register()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			if (!IsRegistered())
			{
				penInputController.RegisterReceiver(this);
			}
		}

		public virtual void UnRegister()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			if (IsRegistered())
			{
				penInputController.UnRegisterReceiver(this);
			}
		}

		public virtual void ProcessCommand(bool keyUp, int command)
		{
		}

		public virtual void SendKey(bool keyUp, int keyToSend)
		{
			if (keyUp)
			{
				mCurrentScene.OnKeyUp(keyToSend);
			}
			else
			{
				mCurrentScene.OnKeyDown(keyToSend);
			}
		}

		public virtual void ForwardDragOrSwipeCommand(int command, int nbOfZones, sbyte zoneType)
		{
			for (int i = 0; i < nbOfZones; i++)
			{
				if (mTouchZones[i] != null && mTouchZones[i].IsZoneType(zoneType))
				{
					short x = mFirstPenInput.GetX();
					short y = mFirstPenInput.GetY();
					if (mTouchZones[i].IsInside(x, y))
					{
						SendTouchCommand(command, i);
					}
				}
			}
		}

		public virtual void SendTouchCommand(int command, int zoneId)
		{
			mCurrentScene.OnTouchCommand(command, zoneId, mFirstPenInput, mLastPenInput);
		}

		public virtual void OnProcessPenDown(short xCoord, short yCoord, sbyte penIndex)
		{
		}

		public virtual void OnProcessPenUp(short xCoord, short yCoord, sbyte penIndex)
		{
		}

		public virtual void OnPenTap(short xCoord, short yCoord)
		{
		}

		public virtual void OnSwipeNorth()
		{
		}

		public virtual void OnSwipeEast()
		{
		}

		public virtual void OnSwipeSouth()
		{
		}

		public virtual void OnSwipeWest()
		{
		}

		public virtual void OnDragNorth()
		{
		}

		public virtual void OnDragEast()
		{
		}

		public virtual void OnDragSouth()
		{
		}

		public virtual void OnDragWest()
		{
		}

		public virtual bool ShouldResetFlickStartTime()
		{
			return true;
		}

		public virtual void OnProcessPenDrag()
		{
			switch (mTouchDirection)
			{
			case 1:
				OnDragNorth();
				break;
			case 3:
				OnDragSouth();
				break;
			case 2:
				OnDragEast();
				break;
			case 4:
				OnDragWest();
				break;
			}
		}

		public virtual void OnProcessPenSwipe()
		{
			switch (mTouchDirection)
			{
			case 1:
				OnSwipeNorth();
				break;
			case 3:
				OnSwipeSouth();
				break;
			case 2:
				OnSwipeEast();
				break;
			case 4:
				OnSwipeWest();
				break;
			}
		}

		public virtual short GetDragDistanceX(Vector2_short pt1, Vector2_short pt2)
		{
			return (short)(pt2.GetX() - pt1.GetX());
		}

		public virtual short GetDragDistanceY(Vector2_short pt1, Vector2_short pt2)
		{
			return (short)(pt2.GetY() - pt1.GetY());
		}

		public virtual short GetDragStepX(short accumulatedDistanceX)
		{
			short num = 0;
			if (FlMath.Absolute(accumulatedDistanceX) >= 144)
			{
				num = 3;
			}
			else if (FlMath.Absolute(accumulatedDistanceX) >= 96)
			{
				num = 2;
			}
			else if (FlMath.Absolute(accumulatedDistanceX) >= 12)
			{
				num = 1;
			}
			if (accumulatedDistanceX < 0)
			{
				num = (short)(-num);
			}
			return num;
		}

		public virtual short GetDragStepY(short accumulatedDistanceY)
		{
			short num = 0;
			if (FlMath.Absolute(accumulatedDistanceY) >= 20)
			{
				num = 4;
			}
			if (accumulatedDistanceY < 0)
			{
				num = (short)(-num);
			}
			return num;
		}

		public static TouchReceiver[] InstArrayTouchReceiver(int size)
		{
			TouchReceiver[] array = new TouchReceiver[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TouchReceiver();
			}
			return array;
		}

		public static TouchReceiver[][] InstArrayTouchReceiver(int size1, int size2)
		{
			TouchReceiver[][] array = new TouchReceiver[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchReceiver[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchReceiver();
				}
			}
			return array;
		}

		public static TouchReceiver[][][] InstArrayTouchReceiver(int size1, int size2, int size3)
		{
			TouchReceiver[][][] array = new TouchReceiver[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchReceiver[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchReceiver[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TouchReceiver();
					}
				}
			}
			return array;
		}
	}
}
