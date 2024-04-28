namespace ca.jamdat.flight
{
	public class FlPixelFormat
	{
		public const int HAS_ALPHA_MASK = 32768;

		public const int ALPHA_FIRST_MASK = 16384;

		public const int IS_INDEXED_MASK = 8192;

		public const int BYTES_PER_PIXEL_MASK = 7680;

		public const int ALPHA_BITS_MASK = 480;

		public const int DIFFERENTIATING_MASK_0 = 0;

		public const int DIFFERENTIATING_MASK_1 = 1;

		public const int DIFFERENTIATING_MASK_2 = 2;

		public const int DIFFERENTIATING_MASK_3 = 3;

		public const int DIFFERENTIATING_MASK_4 = 4;

		public const int DIFFERENTIATING_MASK_5 = 5;

		public const int DIFFERENTIATING_MASK_6 = 6;

		public const int DIFFERENTIATING_MASK_7 = 7;

		public const int DIFFERENTIATING_MASK_8 = 8;

		public const int HAS_ALPHA_SHIFT = 15;

		public const int ALPHA_FIRST_SHIFT = 14;

		public const int IS_INDEXED_SHIFT = 13;

		public const int BYTES_PER_PIXEL_SHIFT = 9;

		public const int ALPHA_BITS_SHIFT = 5;

		public const int UNUSED_SHIFT = 0;

		public const int Undefined = 0;

		public const int A8 = 49920;

		public const int I8XRGB4444 = 8704;

		public const int I8XRGB4444BigEndian = 8705;

		public const int I8XRGB1555 = 8706;

		public const int I8RGB565 = 8707;

		public const int I8RGB888 = 8708;

		public const int I8RGBA8888 = 8709;

		public const int I8ARGB8888 = 8710;

		public const int I8RGBA5658 = 8711;

		public const int I8RGBA5551 = 8712;

		public const int ARGB4444 = 50304;

		public const int ARGB4444BigEndian = 50305;

		public const int XRGB4444 = 1152;

		public const int XRGB4444BigEndian = 1153;

		public const int XRGB1555 = 1024;

		public const int XRGBA15558 = 34560;

		public const int RGBA4444 = 33920;

		public const int RGB565 = 1025;

		public const int RGBA5658 = 34561;

		public const int RGB888 = 1536;

		public const int RGBA5551 = 33824;

		public const int RGBA8888 = 35072;

		public const int ARGB8888 = 51456;

		public const int PVRTC2RGB = 1;

		public const int PVRTC4RGB = 2;

		public const int PVRTC2RGBA = 32769;

		public const int PVRTC4RGBA = 32770;

		public static int GetBytesPerPixel(int value)
		{
			return (value & 0x1E00) >> 9;
		}

		public static int GetBitsPerPixel(int value)
		{
			return (value & 0x1E00) >> 9 << 3;
		}

		public static int GetColorBytesPerPixel(int value)
		{
			int num = ((value & 0x1E00) >> 9 << 3) - ((value & 0x1E0) >> 5);
			return (num - 1 >> 3) + 1;
		}

		public static int GetColorBitsPerPixel(int value)
		{
			return ((value & 0x1E00) >> 9 << 3) - ((value & 0x1E0) >> 5);
		}

		public static bool HasAlpha(int value)
		{
			return (value & 0x8000) != 0;
		}

		public static bool HasAlphaInPalette(int value)
		{
			switch (value)
			{
			case 8709:
			case 8710:
			case 8711:
			case 8712:
				return true;
			default:
				return false;
			}
		}

		public static bool IsIndexed(int value)
		{
			return (value & 0x2000) != 0;
		}

		public static bool IsPVRTC(int value)
		{
			return false;
		}

		public static FlPixelFormat[] InstArrayFlPixelFormat(int size)
		{
			FlPixelFormat[] array = new FlPixelFormat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlPixelFormat();
			}
			return array;
		}

		public static FlPixelFormat[][] InstArrayFlPixelFormat(int size1, int size2)
		{
			FlPixelFormat[][] array = new FlPixelFormat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlPixelFormat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlPixelFormat();
				}
			}
			return array;
		}

		public static FlPixelFormat[][][] InstArrayFlPixelFormat(int size1, int size2, int size3)
		{
			FlPixelFormat[][][] array = new FlPixelFormat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlPixelFormat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlPixelFormat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlPixelFormat();
					}
				}
			}
			return array;
		}
	}
}
