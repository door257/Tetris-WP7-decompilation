namespace ca.jamdat.flight
{
	public class VideoMode
	{
		public const sbyte videoModeWidth = 0;

		public const sbyte videoModeHeight = 1;

		public const sbyte videoModeBpp = 2;

		public const sbyte videoModeDataCount = 3;

		public int[] mData = new int[3];

		public VideoMode()
		{
		}

		public VideoMode(VideoMode other)
		{
			mData[0] = other.GetWidth();
			mData[1] = other.GetHeight();
			mData[2] = other.GetBpp();
		}

		public VideoMode(int w, int h, int b)
		{
			mData[0] = w;
			mData[1] = h;
			mData[2] = b;
		}

		public virtual int GetWidth()
		{
			return mData[0];
		}

		public virtual int GetHeight()
		{
			return mData[1];
		}

		public virtual int GetBpp()
		{
			return mData[2];
		}

		public virtual void SetWidth(int w)
		{
			mData[0] = w;
		}

		public virtual void SetHeight(int h)
		{
			mData[1] = h;
		}

		public virtual void SetBpp(int b)
		{
			mData[2] = b;
		}

		public virtual int GetBytesPerPixel()
		{
			return (mData[2] - 1 >> 3) + 1;
		}

		public static VideoMode[] InstArrayVideoMode(int size)
		{
			VideoMode[] array = new VideoMode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new VideoMode();
			}
			return array;
		}

		public static VideoMode[][] InstArrayVideoMode(int size1, int size2)
		{
			VideoMode[][] array = new VideoMode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VideoMode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VideoMode();
				}
			}
			return array;
		}

		public static VideoMode[][][] InstArrayVideoMode(int size1, int size2, int size3)
		{
			VideoMode[][][] array = new VideoMode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VideoMode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VideoMode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new VideoMode();
					}
				}
			}
			return array;
		}
	}
}
