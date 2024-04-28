using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Timer
	{
		public int mStartTime;

		public virtual void destruct()
		{
		}

		public virtual void Start()
		{
			mStartTime = (int)FlApplication.GetRealTime();
		}

		public virtual void Reset()
		{
			mStartTime = 0;
		}

		public virtual int GetElapsedTime()
		{
			return (int)(FlApplication.GetRealTime() - mStartTime);
		}

		public static Timer[] InstArrayTimer(int size)
		{
			Timer[] array = new Timer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Timer();
			}
			return array;
		}

		public static Timer[][] InstArrayTimer(int size1, int size2)
		{
			Timer[][] array = new Timer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Timer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Timer();
				}
			}
			return array;
		}

		public static Timer[][][] InstArrayTimer(int size1, int size2, int size3)
		{
			Timer[][][] array = new Timer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Timer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Timer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Timer();
					}
				}
			}
			return array;
		}
	}
}
