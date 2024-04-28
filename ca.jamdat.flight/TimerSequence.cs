using System;

namespace ca.jamdat.flight
{
	public class TimerSequence : TimeSystem
	{
		public new const sbyte typeNumber = 86;

		public new const sbyte typeID = 86;

		public new const bool supportsDynamicSerialization = true;

		public const short Infinite = short.MaxValue;

		public short[] mStartDurationTimes;

		public TimerSequence()
		{
		}

		public TimerSequence(int maxNumInterval)
		{
			mStartDurationTimes = new short[maxNumInterval * 2];
			mTimeControlleds.SetSize(maxNumInterval);
		}

		public static TimerSequence Cast(object o, TimerSequence _)
		{
			return (TimerSequence)o;
		}

		public override sbyte GetTypeID()
		{
			return 86;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			mStartDurationTimes = null;
		}

		public override void Register(TimeControlled timeable)
		{
		}

		public override void UnRegister(TimeControlled timeable)
		{
			PtrArray_TimeControlled ptrArray_TimeControlled = mTimeControlleds;
			for (int i = 0; i < ptrArray_TimeControlled.End(); i++)
			{
				if (ptrArray_TimeControlled.GetAt(i) == timeable)
				{
					ptrArray_TimeControlled.SetAt(null, i);
				}
			}
		}

		public virtual void RegisterInterval(TimeControlled timeable, int startTime, int duration)
		{
			PtrArray_TimeControlled ptrArray_TimeControlled = mTimeControlleds;
			int i;
			for (i = 0; i < ptrArray_TimeControlled.End() && ptrArray_TimeControlled.GetAt(i) != null; i++)
			{
			}
			ptrArray_TimeControlled.SetAt(timeable, i);
			mStartDurationTimes[i * 2] = (short)startTime;
			mStartDurationTimes[i * 2 + 1] = (short)duration;
		}

		public override void SetTotalTimeRecursively(int val)
		{
			SetTotalTime(val);
			for (int i = mTimeControlleds.Start(); i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at != null && at is TimeSystem)
				{
					short num = mStartDurationTimes[i * 2];
					((TimeSystem)at).SetTotalTimeRecursively(val - num);
				}
			}
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			short t = 0;
			t = p.SerializeIntrinsic(t);
			mStartDurationTimes = p.SerializeIntrinsics(mStartDurationTimes, t);
			mTimeControlleds.SetSize(t / 2);
		}

		public override void CleanUpTimeTable()
		{
		}

		public override void SendOnTime(int timeMs, int deltaTimeMs)
		{
			if (Empty())
			{
				base.SendOnTime(timeMs, deltaTimeMs);
				return;
			}
			for (int i = 0; i < mTimeControlleds.End(); i++)
			{
				TimeControlled at = mTimeControlleds.GetAt(i);
				if (at == null)
				{
					continue;
				}
				short num = mStartDurationTimes[i * 2];
				short num2 = mStartDurationTimes[i * 2 + 1];
				short num3 = (short)((num2 != short.MaxValue) ? (num + num2) : 32767);
				int num4 = timeMs - deltaTimeMs;
				int num5 = timeMs;
				if ((num5 >= num && (num4 <= num3 || num3 == short.MaxValue)) || (num4 >= num && (num5 <= num3 || num3 == short.MaxValue)))
				{
					if (num > num4)
					{
						num4 = num;
					}
					if (num3 != short.MaxValue && num3 < num5)
					{
						num5 = num3;
					}
					at.OnTime(num5 - num, num5 - num4);
				}
			}
		}

		public virtual bool Empty()
		{
			return mStartDurationTimes.Length == 0;
		}

		public static TimerSequence[] InstArrayTimerSequence(int size)
		{
			TimerSequence[] array = new TimerSequence[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TimerSequence();
			}
			return array;
		}

		public static TimerSequence[][] InstArrayTimerSequence(int size1, int size2)
		{
			TimerSequence[][] array = new TimerSequence[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimerSequence[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimerSequence();
				}
			}
			return array;
		}

		public static TimerSequence[][][] InstArrayTimerSequence(int size1, int size2, int size3)
		{
			TimerSequence[][][] array = new TimerSequence[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimerSequence[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimerSequence[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TimerSequence();
					}
				}
			}
			return array;
		}
	}
}
