namespace ca.jamdat.flight
{
	public class Interpolator
	{
		public const sbyte STEP = 0;

		public const sbyte LINEAR = 1;

		public const sbyte SPLINE = 2;

		public const sbyte SLERP = 3;

		public const sbyte SQUAD = 4;

		public static F32 InterpolateLinear(F32 valueA, F32 valueB, F32 ratio)
		{
			long num = valueA.value * (65536L - (long)ratio.value);
			long num2 = (long)valueB.value * (long)ratio.value;
			return new F32((int)(num + num2 >> 16), 16);
		}

		public static Interpolator[] InstArrayInterpolator(int size)
		{
			Interpolator[] array = new Interpolator[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Interpolator();
			}
			return array;
		}

		public static Interpolator[][] InstArrayInterpolator(int size1, int size2)
		{
			Interpolator[][] array = new Interpolator[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Interpolator[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Interpolator();
				}
			}
			return array;
		}

		public static Interpolator[][][] InstArrayInterpolator(int size1, int size2, int size3)
		{
			Interpolator[][][] array = new Interpolator[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Interpolator[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Interpolator[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Interpolator();
					}
				}
			}
			return array;
		}
	}
}
