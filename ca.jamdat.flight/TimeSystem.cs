using System;

namespace ca.jamdat.flight
{
	public class TimeSystem : TimeControlled
	{
		public new const sbyte typeNumber = 85;

		public new const sbyte typeID = 85;

		public new const bool supportsDynamicSerialization = true;

		public F32 mTimeFlowSpeed;

		public int mTotalTime;

		public PtrArray_TimeControlled mTimeControlleds;

		public bool mPaused;

		public bool mDirty;

		public F32 mDeltaError;

		public TimeSystem()
		{
			mTimeFlowSpeed = new F32(F32.One(16));
			mDeltaError = new F32(F32.Zero(16));
			mTimeControlleds = new PtrArray_TimeControlled();
		}

		public static TimeSystem Cast(object o, TimeSystem _)
		{
			return (TimeSystem)o;
		}

		public override sbyte GetTypeID()
		{
			return 85;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
		}

		public override void OnTime(int timeMs, int deltaTimeMs)
		{
			if (!IsPaused())
			{
				if (!mTimeFlowSpeed.Equals(F32.One(16)))
				{
					F32 f = new F32(F32.FromInt(deltaTimeMs, 16).Mul(mTimeFlowSpeed, 16).Add(mDeltaError));
					deltaTimeMs = f.ToInt(16);
					mDeltaError = f.Sub(F32.FromInt(deltaTimeMs, 16));
				}
				mTotalTime += deltaTimeMs;
				SendOnTime(mTotalTime, deltaTimeMs);
			}
			CleanUpTimeTable();
		}

		public virtual void Start()
		{
			mPaused = false;
		}

		public virtual void StartRecursively()
		{
			Start();
			for (int i = mTimeControlleds.Start(); i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null && at is TimeSystem)
				{
					((TimeSystem)at).StartRecursively();
				}
			}
		}

		public virtual void Stop()
		{
			mTotalTime = 0;
			mPaused = true;
		}

		public virtual void StopRecursively()
		{
			Stop();
			for (int i = mTimeControlleds.Start(); i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null && at is TimeSystem)
				{
					((TimeSystem)at).StopRecursively();
				}
			}
		}

		public virtual void Pause()
		{
			mPaused = true;
		}

		public virtual void AdvanceBy(int deltaTimeMs)
		{
			mTotalTime += deltaTimeMs;
			SendOnTime(mTotalTime, deltaTimeMs);
		}

		public virtual void Register(TimeControlled timeable)
		{
			if (!IsRegistered(timeable))
			{
				mTimeControlleds.Insert(timeable);
			}
		}

		public virtual void UnRegister(TimeControlled timeable)
		{
			int num = mTimeControlleds.Find(timeable);
			if (num >= 0)
			{
				mTimeControlleds.SetAt(null, num);
				mDirty = true;
			}
		}

		public virtual void UnRegisterAll()
		{
			PtrArray_TimeControlled ptrArray_TimeControlled = mTimeControlleds;
			for (int i = 0; i < ptrArray_TimeControlled.End(); i++)
			{
				if (ptrArray_TimeControlled.GetAt(i) != null)
				{
					UnRegister(ptrArray_TimeControlled.GetAt(i));
				}
			}
		}

		public virtual bool IsRegistered(TimeControlled timeable)
		{
			return mTimeControlleds.Find(timeable) >= 0;
		}

		public virtual F32 GetTimeFlowSpeed()
		{
			return mTimeFlowSpeed.IncreasePrecision(0);
		}

		public virtual void SetTimeFlowSpeed(F32 val)
		{
			mTimeFlowSpeed = val.DecreasePrecision(0);
		}

		public virtual int GetTotalTime()
		{
			return mTotalTime;
		}

		public virtual void SetTotalTime(int val)
		{
			mTotalTime = val;
		}

		public virtual void SetTotalTimeRecursively(int val)
		{
			SetTotalTime(val);
			for (int i = mTimeControlleds.Start(); i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null && at is TimeSystem)
				{
					((TimeSystem)at).SetTotalTimeRecursively(val);
				}
			}
		}

		public virtual bool IsPaused()
		{
			if (!mPaused)
			{
				return mTimeFlowSpeed.Equals(F32.Zero(16));
			}
			return true;
		}

		public override void OnSerialize(Package p)
		{
			mTotalTime = p.SerializeIntrinsic(mTotalTime);
			mPaused = p.SerializeIntrinsic(mPaused);
			mTimeFlowSpeed = p.SerializeIntrinsic(mTimeFlowSpeed);
			mTimeControlleds.OnSerialize(p);
			mDirty = false;
		}

		public virtual void RefreshAllControllersIncluded()
		{
			for (int i = mTimeControlleds.Start(); i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null)
				{
					if (at is Controller)
					{
						((Controller)at).Refresh();
					}
					else if (at is TimeSystem)
					{
						((TimeSystem)at).RefreshAllControllersIncluded();
					}
				}
			}
		}

		public virtual int GetNbChildren()
		{
			return mTimeControlleds.End();
		}

		public virtual TimeControlled GetChild(int i)
		{
			return mTimeControlleds.GetAt(i);
		}

		public virtual void CleanUpTimeTable()
		{
			if (mDirty)
			{
				PtrArray_TimeControlled ptrArray_TimeControlled = mTimeControlleds;
				int p;
				while ((p = ptrArray_TimeControlled.Find(null)) != -1)
				{
					ptrArray_TimeControlled.SetAt(ptrArray_TimeControlled.GetAt(ptrArray_TimeControlled.End() - 1), p);
					ptrArray_TimeControlled.RemoveAt(ptrArray_TimeControlled.End() - 1);
				}
				mDirty = false;
			}
		}

		public virtual void SendOnTime(int timeMs, int deltaTimeMs)
		{
			int num = mTimeControlleds.End();
			for (int i = 0; i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null)
				{
					if (i >= num)
					{
						deltaTimeMs = 0;
					}
					at.OnTime(timeMs, deltaTimeMs);
				}
			}
		}

		public static TimeSystem[] InstArrayTimeSystem(int size)
		{
			TimeSystem[] array = new TimeSystem[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TimeSystem();
			}
			return array;
		}

		public static TimeSystem[][] InstArrayTimeSystem(int size1, int size2)
		{
			TimeSystem[][] array = new TimeSystem[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeSystem[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeSystem();
				}
			}
			return array;
		}

		public static TimeSystem[][][] InstArrayTimeSystem(int size1, int size2, int size3)
		{
			TimeSystem[][][] array = new TimeSystem[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeSystem[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeSystem[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TimeSystem();
					}
				}
			}
			return array;
		}
	}
}
