namespace ca.jamdat.flight
{
	public class TimeData
	{
		public const sbyte TD_YEAR = 0;

		public const sbyte TD_MONTH = 1;

		public const sbyte TD_DAY = 2;

		public const sbyte TD_HOUR = 3;

		public const sbyte TD_MINUTE = 4;

		public const sbyte TD_SECOND = 5;

		public const sbyte TD_SIZE = 6;

		public int[] mData = new int[6];

		public TimeData()
		{
		}

		public TimeData(FlString date)
		{
			int num = date.ToLong();
			int num2 = num / 1000000;
			int num3 = num / 10000 % 100;
			int num4 = num % 10000;
			mData[1] = num2;
			mData[2] = num3;
			mData[0] = num4;
			mData[3] = 0;
			mData[4] = 0;
			mData[5] = 0;
		}

		public TimeData(int month, int day, int year)
		{
			mData[1] = month;
			mData[2] = day;
			mData[0] = year;
			mData[3] = 0;
			mData[4] = 0;
			mData[5] = 0;
		}

		public TimeData(int month, int day, int year, int hour, int min, int sec)
		{
			mData[1] = month;
			mData[2] = day;
			mData[0] = year;
			mData[3] = hour;
			mData[4] = min;
			mData[5] = sec;
		}

		public virtual int GetHour()
		{
			return mData[3];
		}

		public virtual int GetMin()
		{
			return mData[4];
		}

		public virtual int GetSec()
		{
			return mData[5];
		}

		public virtual int GetYear()
		{
			return mData[0];
		}

		public virtual int GetMonth()
		{
			return mData[1];
		}

		public virtual int GetDay()
		{
			return mData[2];
		}

		public virtual bool IsDateValid()
		{
			int num = mData[0];
			int num2 = mData[1];
			int num3 = mData[2];
			if (num2 < 1 || num2 > 12)
			{
				return false;
			}
			if (num3 < 1)
			{
				return false;
			}
			switch (num2)
			{
			case 2:
				if ((num % 4 == 0 && num % 100 != 0) || num % 400 == 0)
				{
					return num3 < 30;
				}
				return num3 < 29;
			case 1:
			case 3:
			case 5:
			case 7:
			case 8:
			case 10:
			case 12:
				return num3 < 32;
			default:
				return num3 < 31;
			}
		}

		public bool Equals(TimeData other)
		{
			bool result = true;
			for (int i = 0; i < 6; i++)
			{
				if (mData[i] != other.mData[i])
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public static TimeData[] InstArrayTimeData(int size)
		{
			TimeData[] array = new TimeData[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TimeData();
			}
			return array;
		}

		public static TimeData[][] InstArrayTimeData(int size1, int size2)
		{
			TimeData[][] array = new TimeData[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeData[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeData();
				}
			}
			return array;
		}

		public static TimeData[][][] InstArrayTimeData(int size1, int size2, int size3)
		{
			TimeData[][][] array = new TimeData[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeData[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeData[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TimeData();
					}
				}
			}
			return array;
		}
	}
}
