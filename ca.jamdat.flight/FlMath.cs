namespace ca.jamdat.flight
{
	public class FlMath
	{
		public const int maxInt8Value = 127;

		public const int minInt8Value = -128;

		public const int maxInt16Value = 32767;

		public const int minInt16Value = -32768;

		public const int maxInt32Value = int.MaxValue;

		public const int minInt32Value = -2147483647;

		public static float epsilon;

		public static float twoPi;

		public static float Pi;

		public static int[] SIN_TABLE = new int[91]
		{
			0, 18, 36, 54, 71, 89, 107, 125, 143, 160,
			178, 195, 213, 230, 248, 265, 282, 299, 316, 333,
			350, 367, 384, 400, 416, 433, 449, 465, 481, 496,
			512, 527, 543, 558, 573, 587, 602, 616, 630, 644,
			658, 672, 685, 698, 711, 724, 737, 749, 761, 773,
			784, 796, 807, 818, 828, 839, 849, 859, 868, 878,
			887, 896, 904, 912, 920, 928, 935, 943, 949, 956,
			962, 968, 974, 979, 984, 989, 994, 998, 1002, 1005,
			1008, 1011, 1014, 1016, 1018, 1020, 1022, 1023, 1023, 1024,
			1024
		};

		public static int IntegerSqrt(int x)
		{
			return 0;
		}

		public static short Random(short a, short b)
		{
			if (b == a)
			{
				return a;
			}
			return (short)(a + Rand(true) % (b - a + 1));
		}

		public static int Random(int a, int b)
		{
			if (b == a)
			{
				return a;
			}
			return a + Rand() % (b - a + 1);
		}

		public static int Rand(bool returnAsShort)
		{
			int randomState = FrameworkGlobals.GetInstance().randomState;
			int num = randomState;
			if (!returnAsShort)
			{
				long num2 = randomState;
				num2 = (long)randomState * 214013L + 2531011;
				num = (int)(num2 >> 32) & 0x7FFFFFFF;
				FrameworkGlobals.GetInstance().randomState = (int)num2;
			}
			else
			{
				randomState = randomState * 214013 + 2531011;
				num = (randomState >> 16) & 0x7FFF;
				FrameworkGlobals.GetInstance().randomState = randomState;
			}
			return num;
		}

		public static void Seed(int newSeed)
		{
			FrameworkGlobals.GetInstance().randomState = newSeed;
		}

		public static int Modulo(int value, int modulo)
		{
			value %= modulo;
			if (value < 0)
			{
				value += modulo;
			}
			return value;
		}

		public static int Sin1024(int degrees)
		{
			while (degrees > 360)
			{
				degrees -= 360;
			}
			while (degrees < 0)
			{
				degrees += 360;
			}
			if (degrees >= 0 && degrees <= 90)
			{
				return SIN_TABLE[degrees];
			}
			if (degrees > 90 && degrees <= 180)
			{
				return SIN_TABLE[180 - degrees];
			}
			if (degrees > 180 && degrees <= 270)
			{
				return -SIN_TABLE[degrees - 180];
			}
			return -SIN_TABLE[360 - degrees];
		}

		public static int Cos1024(int degrees)
		{
			while (degrees > 360)
			{
				degrees -= 360;
			}
			while (degrees < 0)
			{
				degrees += 360;
			}
			if (degrees >= 0 && degrees <= 90)
			{
				return SIN_TABLE[90 - degrees];
			}
			if (degrees > 90 && degrees <= 180)
			{
				return -SIN_TABLE[degrees - 90];
			}
			if (degrees > 180 && degrees <= 270)
			{
				return -SIN_TABLE[270 - degrees];
			}
			return SIN_TABLE[degrees - 270];
		}

		public static sbyte Absolute(sbyte a)
		{
			if (a < 0)
			{
				a = (sbyte)(-a);
			}
			return a;
		}

		public static short Absolute(short a)
		{
			if (a < 0)
			{
				a = (short)(-a);
			}
			return a;
		}

		public static int Absolute(int a)
		{
			if (a < 0)
			{
				a = -a;
			}
			return a;
		}

		public static long Absolute(long a)
		{
			if (a < 0)
			{
				a = -a;
			}
			return a;
		}

		public static F32 Absolute(F32 a)
		{
			return a.Abs();
		}

		public static sbyte Minimum(sbyte a, sbyte b)
		{
			if (a < b)
			{
				b = a;
			}
			return b;
		}

		public static short Minimum(short a, short b)
		{
			if (a < b)
			{
				b = a;
			}
			return b;
		}

		public static int Minimum(int a, int b)
		{
			if (a < b)
			{
				b = a;
			}
			return b;
		}

		public static long Minimum(long a, long b)
		{
			if (a < b)
			{
				b = a;
			}
			return b;
		}

		public static F32 Minimum(F32 a, F32 b)
		{
			if (!a.LessThan(b))
			{
				return b;
			}
			return a;
		}

		public static sbyte Maximum(sbyte a, sbyte b)
		{
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		public static short Maximum(short a, short b)
		{
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		public static int Maximum(int a, int b)
		{
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		public static long Maximum(long a, long b)
		{
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		public static F32 Maximum(F32 a, F32 b)
		{
			if (!a.GreaterThan(b))
			{
				return b;
			}
			return a;
		}

		public static bool IsOdd(int number)
		{
			return (number & 1) == 1;
		}

		public static bool IsEven(int number)
		{
			return (number & 1) == 0;
		}

		public static bool IsPowerOf2(int number)
		{
			return (number & (number - 1)) == 0;
		}

		public static int GetPowerOf2(int number)
		{
			if (!IsPowerOf2(number))
			{
				return -1;
			}
			int i;
			for (i = 0; number >> i != 0; i++)
			{
			}
			return i - 1;
		}

		public static int GetNextPowerOf2(int number)
		{
			if (IsPowerOf2(number))
			{
				return number;
			}
			int i;
			for (i = 0; number >> i != 0; i++)
			{
			}
			return 1 << i;
		}

		public static int Pow(int @base, int power)
		{
			if (@base == 0 || power < 0)
			{
				return 0;
			}
			if (power == 0)
			{
				return 1;
			}
			int num = 1;
			while (power != 0)
			{
				if ((power & 1) == 1)
				{
					num *= @base;
				}
				@base *= @base;
				power >>= 1;
			}
			return num;
		}

		public static float Sqrt(float x)
		{
			return 0f;
		}

		public static float ArcTan(float a, float b)
		{
			return 0f;
		}

		public static float Absolute(float a)
		{
			if (!(a < 0f))
			{
				return a;
			}
			return 0f - a;
		}

		public static float Minimum(float a, float b)
		{
			if (!(a < b))
			{
				return b;
			}
			return a;
		}

		public static float Maximum(float a, float b)
		{
			if (!(a > b))
			{
				return b;
			}
			return a;
		}

		public static int Round(float f)
		{
			return 0;
		}

		public static int Floor(float f)
		{
			return 0;
		}

		public static int Ceiling(float f)
		{
			return 0;
		}

		public static float SubtractFloor(float f)
		{
			return f - (float)Floor(f);
		}

		public static bool AbnormalValue(double a4)
		{
			return false;
		}

		public static void CheckForAbnormalValue(double f)
		{
		}

		public static float NearExponentialDecay(float x)
		{
			return 0f;
		}

		public static float GetUniformRandom(float low, float high)
		{
			return 0f;
		}

		public static float GetGaussianRandom(float low, float high)
		{
			return 0f;
		}

		public static int Rand()
		{
			return Rand(false);
		}

		public static float GetUniformRandom()
		{
			return GetUniformRandom(0f);
		}

		public static float GetUniformRandom(float low)
		{
			return GetUniformRandom(low, 1f);
		}

		public static float GetGaussianRandom()
		{
			return GetGaussianRandom(0f);
		}

		public static float GetGaussianRandom(float low)
		{
			return GetGaussianRandom(low, 1f);
		}

		public static FlMath[] InstArrayFlMath(int size)
		{
			FlMath[] array = new FlMath[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlMath();
			}
			return array;
		}

		public static FlMath[][] InstArrayFlMath(int size1, int size2)
		{
			FlMath[][] array = new FlMath[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlMath[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlMath();
				}
			}
			return array;
		}

		public static FlMath[][][] InstArrayFlMath(int size1, int size2, int size3)
		{
			FlMath[][][] array = new FlMath[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlMath[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlMath[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlMath();
					}
				}
			}
			return array;
		}
	}
}
