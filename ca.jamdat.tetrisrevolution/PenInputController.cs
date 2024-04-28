using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PenInputController : TimeControlled
	{
		public PenMsgReceiver[] mPenMsgReceivers;

		public bool[] mPenMsgReceiversToRemove;

		public PenMsgReceiver mExclusivePenMsgReceiver;

		public bool[] mIsPenDragging = new bool[16];

		public Vector2_short[] mLastPenPos = Vector2_short.InstArrayVector2_short(16);

		public bool mReceiversLocked;

		public PenInputController()
		{
			mPenMsgReceivers = new PenMsgReceiver[10];
			mPenMsgReceiversToRemove = new bool[10];
			for (int i = 0; i < 10; i++)
			{
				mPenMsgReceiversToRemove[i] = false;
			}
			Activate();
		}

		public override void destruct()
		{
			Deactivate();
			mPenMsgReceivers = null;
			for (int i = 0; i < 10; i++)
			{
			}
			mPenMsgReceiversToRemove = null;
		}

		public virtual void Activate()
		{
			FlPenManager.Get().Activate();
			for (int i = 0; i < 1; i++)
			{
				mIsPenDragging[i] = false;
				mLastPenPos[i].SetX(0);
				mLastPenPos[i].SetY(0);
			}
			RegisterInGlobalTime();
		}

		public virtual void Deactivate()
		{
			UnRegisterInGlobalTime();
			FlPenManager.Get().Deactivate();
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			LockReceivers();
			for (sbyte b = 0; b < 1; b = (sbyte)(b + 1))
			{
				if (mIsPenDragging[b])
				{
					if (mExclusivePenMsgReceiver != null)
					{
						mExclusivePenMsgReceiver.OnPenDragRepeat(mLastPenPos[b].GetX(), mLastPenPos[b].GetY(), b);
					}
					else
					{
						int i = 0;
						for (int numberOfReceivers = GetNumberOfReceivers(); i < numberOfReceivers; i++)
						{
							if (!mPenMsgReceiversToRemove[i])
							{
								PenMsgReceiver penMsgReceiver = mPenMsgReceivers[i];
								penMsgReceiver.OnPenDragRepeat(mLastPenPos[b].GetX(), mLastPenPos[b].GetY(), b);
							}
						}
					}
				}
			}
			UnLockReceivers();
		}

		public virtual void OnPenMsg(Component source, int msg, int intParam)
		{
			LockReceivers();
			if (mExclusivePenMsgReceiver != null)
			{
				ProcessMsg(mExclusivePenMsgReceiver, msg, intParam);
			}
			else
			{
				int i = 0;
				for (int numberOfReceivers = GetNumberOfReceivers(); i < numberOfReceivers; i++)
				{
					if (!mPenMsgReceiversToRemove[i])
					{
						ProcessMsg(mPenMsgReceivers[i], msg, intParam);
					}
				}
			}
			UnLockReceivers();
		}

		public virtual void RegisterExclusiveReceiver(PenMsgReceiver recv)
		{
			mExclusivePenMsgReceiver = recv;
		}

		public virtual void UnRegisterExclusiveReceiver()
		{
			mExclusivePenMsgReceiver = null;
		}

		public virtual bool IsExclusiveRegistered()
		{
			return mExclusivePenMsgReceiver != null;
		}

		public virtual void RegisterReceiver(PenMsgReceiver recv)
		{
			int numberOfReceivers = GetNumberOfReceivers();
			mPenMsgReceivers[numberOfReceivers] = recv;
		}

		public virtual void UnRegisterReceiver(PenMsgReceiver recv)
		{
			if (mReceiversLocked)
			{
				mPenMsgReceiversToRemove[GetIndexOfReceiver(recv)] = true;
			}
			else
			{
				RemoveReceiver(recv);
			}
		}

		public virtual bool IsRegistered(PenMsgReceiver recv)
		{
			return GetIndexOfReceiver(recv) != -1;
		}

		public virtual void ProcessMsg(PenMsgReceiver r, int msg, int intParam)
		{
			short penPositionX = FlPenManager.GetPenPositionX(intParam);
			short penPositionY = FlPenManager.GetPenPositionY(intParam);
			sbyte penIndex = FlPenManager.GetPenIndex(intParam);
			switch (msg)
			{
			case -118:
				r.OnPenUp(penPositionX, penPositionY, penIndex);
				mLastPenPos[penIndex].SetX(0);
				mLastPenPos[penIndex].SetY(0);
				mIsPenDragging[penIndex] = false;
				break;
			case -117:
				r.OnPenDown(penPositionX, penPositionY, penIndex);
				break;
			case -116:
				r.OnPenDrag(penPositionX, penPositionY, penIndex);
				mLastPenPos[penIndex].SetX(penPositionX);
				mLastPenPos[penIndex].SetY(penPositionY);
				mIsPenDragging[penIndex] = true;
				break;
			}
		}

		public virtual void ProcessReceiversToRemove()
		{
			PenMsgReceiver[] array = null;
			array = new PenMsgReceiver[10];
			for (int i = 0; i < 10; i++)
			{
				if (mPenMsgReceiversToRemove[i])
				{
					array[i] = mPenMsgReceivers[i];
					mPenMsgReceiversToRemove[i] = false;
				}
			}
			for (int j = 0; j < 10; j++)
			{
				if (array[j] != null)
				{
					RemoveReceiver(array[j]);
				}
			}
			array = null;
		}

		public virtual void LockReceivers()
		{
			mReceiversLocked = true;
		}

		public virtual void UnLockReceivers()
		{
			mReceiversLocked = false;
			ProcessReceiversToRemove();
		}

		public virtual void CompactReceiverList()
		{
			int num = 10;
			for (int i = 0; i < num; i++)
			{
				if (mPenMsgReceivers[i] != null)
				{
					continue;
				}
				while (num > i)
				{
					int num2 = num - 1;
					if (mPenMsgReceivers[num2] != null)
					{
						PenMsgReceiver penMsgReceiver = null;
						penMsgReceiver = mPenMsgReceivers[i];
						mPenMsgReceivers[i] = mPenMsgReceivers[num2];
						mPenMsgReceivers[num2] = penMsgReceiver;
						mPenMsgReceivers[num2] = null;
						num--;
						break;
					}
					num--;
				}
			}
		}

		public virtual void RemoveReceiver(PenMsgReceiver recv)
		{
			int indexOfReceiver = GetIndexOfReceiver(recv);
			mPenMsgReceivers[indexOfReceiver] = null;
			if (indexOfReceiver != GetNumberOfReceivers())
			{
				CompactReceiverList();
			}
		}

		public virtual int GetIndexOfReceiver(PenMsgReceiver receiver)
		{
			for (int i = 0; i < 10; i++)
			{
				if (mPenMsgReceivers[i] == receiver)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual int GetNumberOfReceivers()
		{
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				if (mPenMsgReceivers[i] != null)
				{
					num++;
				}
			}
			return num;
		}

		public static PenInputController[] InstArrayPenInputController(int size)
		{
			PenInputController[] array = new PenInputController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PenInputController();
			}
			return array;
		}

		public static PenInputController[][] InstArrayPenInputController(int size1, int size2)
		{
			PenInputController[][] array = new PenInputController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PenInputController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PenInputController();
				}
			}
			return array;
		}

		public static PenInputController[][][] InstArrayPenInputController(int size1, int size2, int size3)
		{
			PenInputController[][][] array = new PenInputController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PenInputController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PenInputController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PenInputController();
					}
				}
			}
			return array;
		}
	}
}
