namespace ca.jamdat.flight
{
	public class FrameRate
	{
		public int mLastTime;

		public int mLastFrameRateTimes100;

		public virtual void Reset()
		{
			mLastTime = 0;
			mLastFrameRateTimes100 = 0;
		}

		public virtual void AdvanceFrame()
		{
			int num = (int)FlApplication.GetRunTime();
			int num2 = num - mLastTime;
			if (num2 > 0)
			{
				mLastFrameRateTimes100 = 100000 / num2;
			}
			else
			{
				mLastFrameRateTimes100 = 0;
			}
			mLastTime = num;
		}

		public virtual int GetRateTimes100()
		{
			return mLastFrameRateTimes100;
		}

		public static FrameRate[] InstArrayFrameRate(int size)
		{
			FrameRate[] array = new FrameRate[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FrameRate();
			}
			return array;
		}

		public static FrameRate[][] InstArrayFrameRate(int size1, int size2)
		{
			FrameRate[][] array = new FrameRate[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FrameRate[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FrameRate();
				}
			}
			return array;
		}

		public static FrameRate[][][] InstArrayFrameRate(int size1, int size2, int size3)
		{
			FrameRate[][][] array = new FrameRate[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FrameRate[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FrameRate[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FrameRate();
					}
				}
			}
			return array;
		}
	}
}
