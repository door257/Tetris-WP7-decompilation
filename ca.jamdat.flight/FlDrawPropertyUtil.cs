namespace ca.jamdat.flight
{
	public class FlDrawPropertyUtil
	{
		public const int BitmapStencilNone = 0;

		public const int BitmapStencilPositive = 512;

		public const int BitmapStencilNegative = 1024;

		public const int TransformNone = 0;

		public const int TransformMirrorRot180 = 65536;

		public const int TransformMirror = 131072;

		public const int TransformRot180 = 196608;

		public const int TransformMirrorRot270 = 262144;

		public const int TransformRot90 = 327680;

		public const int TransformRot270 = 393216;

		public const int TransformMirrorRot90 = 458752;

		public const int TileNone = 0;

		public const int TileX = 524288;

		public const int TileY = 1048576;

		public const int TileXY = 1572864;

		public static int GetDefaultDrawProperty()
		{
			return 255;
		}

		public static int BmpTransformToTransform(sbyte transform)
		{
			switch (transform)
			{
			case 2:
				return 131072;
			case 1:
				return 65536;
			case 3:
				return 196608;
			default:
				return 0;
			}
		}

		public static int FlipXYToTransform(bool flipX, bool flipY)
		{
			if (!flipX)
			{
				if (!flipY)
				{
					return 0;
				}
				return 65536;
			}
			if (!flipY)
			{
				return 131072;
			}
			return 196608;
		}

		public static int MIDPTransformToTransform(int midpTransform)
		{
			return midpTransform << 16;
		}

		public static int TransformToMIDPTransform(int transform)
		{
			return (int)((uint)transform >> 16);
		}

		public static sbyte GetBmpTransform(int drawProperty)
		{
			return 0;
		}

		public static bool IsFlippedX(int drawProperty)
		{
			return false;
		}

		public static bool IsFlippedY(int drawProperty)
		{
			return false;
		}

		public static int ApplyTransform(int drawProperty, int transform)
		{
			return (int)(drawProperty & 0xFFF8FFFFu) | transform;
		}

		public static int GetTransform(int drawProperty)
		{
			return 0;
		}

		public static bool IsInitialOrientation(int drawProperty)
		{
			switch (GetTransform(drawProperty))
			{
			case 0:
			case 65536:
			case 131072:
			case 196608:
				return true;
			default:
				return false;
			}
		}

		public static short GetAlpha(int drawProperty)
		{
			return 255;
		}

		public static int GetBitmapStencil(int drawProperty)
		{
			return 0;
		}

		public static int GetTile(int drawProperty)
		{
			return drawProperty & 0x180000;
		}

		public static int ApplyTile(int drawProperty, int tile)
		{
			return (int)(drawProperty & 0xFFE7FFFFu) | tile;
		}

		public static int ApplyTile(int drawProperty, bool tileX, bool tileY)
		{
			int num = 0;
			if (tileX)
			{
				num = 524288;
			}
			if (tileY)
			{
				num |= 0x100000;
			}
			return ApplyTile(drawProperty, num);
		}

		public static FlDrawPropertyUtil[] InstArrayFlDrawPropertyUtil(int size)
		{
			FlDrawPropertyUtil[] array = new FlDrawPropertyUtil[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlDrawPropertyUtil();
			}
			return array;
		}

		public static FlDrawPropertyUtil[][] InstArrayFlDrawPropertyUtil(int size1, int size2)
		{
			FlDrawPropertyUtil[][] array = new FlDrawPropertyUtil[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlDrawPropertyUtil[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlDrawPropertyUtil();
				}
			}
			return array;
		}

		public static FlDrawPropertyUtil[][][] InstArrayFlDrawPropertyUtil(int size1, int size2, int size3)
		{
			FlDrawPropertyUtil[][][] array = new FlDrawPropertyUtil[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlDrawPropertyUtil[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlDrawPropertyUtil[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlDrawPropertyUtil();
					}
				}
			}
			return array;
		}
	}
}
