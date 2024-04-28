using System;

namespace ca.jamdat.flight
{
	public class F32
	{
		public int value;

		public F32()
		{
		}

		public F32(float fValue, int pointPosition)
		{
			value = (int)(fValue * (float)(1 << pointPosition));
		}

		public F32(int fpValue, int pointPosition)
		{
			value = fpValue;
		}

		public F32(F32 other)
		{
			value = other.value;
		}

		public F32(int val)
		{
			value = val;
		}

		public virtual F32 Mul(F32 f, int n)
		{
			long num = (long)value * (long)f.value;
			return new F32((int)(num >> n), n);
		}

		public virtual F32 Div(F32 f, int n)
		{
			long num = (long)value << n;
			num /= f.value;
			return new F32((int)num, n);
		}

		public virtual F32 MulLP(F32 f, int n)
		{
			return new F32((value >> n / 2) * (f.value >> n / 2), n);
		}

		public virtual F32 DivLP(F32 f, int n)
		{
			return new F32((value << n / 4) / (f.value >> n - n / 4), n);
		}

		public virtual F32 MulPower2(int power)
		{
			return new F32(value << power, 0);
		}

		public virtual F32 DivPower2(int power)
		{
			return new F32(value >> power, 0);
		}

		public virtual F32 Inverse(int n)
		{
			long num = 4611686018427387904L;
			num /= value;
			num >>= 62 - 2 * n;
			return new F32((int)num, n);
		}

		public virtual F32 Square(int n)
		{
			return Mul(this, n);
		}

		public virtual F32 Round(int pointPos)
		{
			return Add(Half(pointPos)).Floor(pointPos);
		}

		public virtual F32 Floor(int pointPos)
		{
			F32 f = new F32(this);
			f.value = value >> pointPos << pointPos;
			return f;
		}

		public virtual F32 Ceiling(int pointPos)
		{
			F32 f = new F32(this);
			f.value = -(-value >> pointPos << pointPos);
			return f;
		}

		public virtual F32 Abs()
		{
			if (value < 0)
			{
				return Neg();
			}
			return this;
		}

		public virtual F32 Sqrt(int n)
		{
			int num = 0;
			long num2 = value;
			num2 <<= n;
			num = (int)Math.Sqrt(num2);
			return new F32(num, n);
		}

		public virtual F32 InvSqrt(int n)
		{
			return Sqrt(n).Inverse(n);
		}

		public virtual F32 Sin(int point)
		{
			F32 f = new F32(Pi(point));
			F32 f2 = new F32(PiOver2(point));
			F32 f3 = new F32(this);
			F32 other = new F32(TwoPi(point));
			if (f3.value < 0)
			{
				do
				{
					f3 = f3.Add(other);
				}
				while (f3.value < 0);
			}
			else if (f3.GreaterThan(other))
			{
				do
				{
					f3 = f3.Sub(other);
				}
				while (f3.GreaterThan(other));
			}
			int num = 1;
			if (f3.value > f.value)
			{
				if (f3.value > f.value + f2.value)
				{
					f3.value = (f.value << 1) - f3.value;
					num = -1;
				}
				else
				{
					f3.value -= f.value;
					num = -1;
				}
			}
			else if (f3.value > f2.value)
			{
				f3.value = f.value - f3.value;
			}
			F32 f4 = new F32(f3.Square(point));
			F32 f5 = new F32(ConvertConstant16(498, point));
			f5 = f5.Mul(f4, point);
			f5 = f5.Sub(ConvertConstant16(10882, point));
			f5 = f5.Mul(f4, point);
			f5.value += 1 << point;
			f5 = f5.Mul(f3, point);
			f5.value *= num;
			return f5;
		}

		public virtual F32 Cos(int point)
		{
			F32 f = new F32(Pi(point));
			F32 f2 = new F32(PiOver2(point));
			F32 f3 = new F32(this);
			F32 other = new F32(TwoPi(point));
			if (f3.value < 0)
			{
				do
				{
					f3 = f3.Add(other);
				}
				while (f3.value < 0);
			}
			else if (f3.GreaterThan(other))
			{
				do
				{
					f3 = f3.Sub(other);
				}
				while (f3.GreaterThan(other));
			}
			int num = 1;
			if (f3.value > f.value)
			{
				if (f3.value > f.value + f2.value)
				{
					f3.value = (f.value << 1) - f3.value;
				}
				else
				{
					f3.value -= f.value;
					num = -1;
				}
			}
			else if (f3.value > f2.value)
			{
				f3.value = f.value - f3.value;
				num = -1;
			}
			F32 f4 = new F32(f3.Square(point));
			F32 f5 = new F32(ConvertConstant16(2428, point).Mul(f4, point));
			f5 = f5.Sub(ConvertConstant16(32551, point));
			f5 = f5.Mul(f4, point);
			f5.value += 1 << point;
			f5.value *= num;
			return f5;
		}

		public virtual F32 Tan(int point)
		{
			F32 f = new F32(this);
			F32 other = new F32(TwoPi(point));
			while (f.GreaterThan(other))
			{
				f = f.Sub(other);
			}
			while (f.value < 0)
			{
				f = f.Add(other);
			}
			F32 f2 = new F32(f.Square(point));
			F32 f3 = new F32(ConvertConstant16(13323, point));
			f3 = f3.Mul(f2, point);
			f3 = f3.Add(ConvertConstant16(20810, point));
			f3 = f3.Mul(f2, point);
			f3 = f3.Add(One(point));
			return f.Mul(f3, point);
		}

		public virtual F32 ArcSin(int point)
		{
			if (IsNegative())
			{
				return Neg().ArcSin(point).Neg();
			}
			F32 f = new F32(One(point).Sub(this).Sqrt(point));
			F32 f2 = new F32(ConvertConstant16(-1228, point));
			f2 = Mul(f2, point);
			f2 = f2.Add(ConvertConstant16(4866, point));
			f2 = Mul(f2, point);
			f2 = f2.Sub(ConvertConstant16(13901, point));
			f2 = Mul(f2, point);
			f2 = f2.Add(ConvertConstant16(102939, point));
			return PiOver2(point).Sub(f2.Mul(f, point));
		}

		public virtual F32 ArcCos(int point)
		{
			if (IsNegative())
			{
				return Neg().ArcCos(point).Sub(Pi(point)).Neg();
			}
			if (GreaterOrEqual(One(point)))
			{
				return Zero(point);
			}
			F32 f = new F32(One(point).Sub(this).Sqrt(point));
			F32 f2 = new F32(ConvertConstant16(-1228, point));
			f2 = Mul(f2, point);
			f2 = f2.Add(ConvertConstant16(4866, point));
			f2 = Mul(f2, point);
			f2 = f2.Sub(ConvertConstant16(13901, point));
			f2 = Mul(f2, point);
			f2 = f2.Add(ConvertConstant16(102939, point));
			return f2.Mul(f, point);
		}

		public virtual F32 ArcTan(int point)
		{
			if (Abs().GreaterThan(One(point)))
			{
				if (IsPositive())
				{
					return PiOver2(point).Sub(Inverse(point).ArcTan(point));
				}
				return PiOver2(point).Neg().Sub(Inverse(point).ArcTan(point));
			}
			F32 f = new F32(Square(point));
			F32 f2 = new F32(ConvertConstant16(1365, point));
			f2 = f2.Mul(f, point);
			f2 = f2.Sub(ConvertConstant16(5579, point));
			f2 = f2.Mul(f, point);
			f2 = f2.Add(ConvertConstant16(11805, point));
			f2 = f2.Mul(f, point);
			f2 = f2.Sub(ConvertConstant16(21646, point));
			f2 = f2.Mul(f, point);
			f2 = f2.Add(ConvertConstant16(65527, point));
			return Mul(f2, point);
		}

		public static F32 ArcTan2(F32 x, F32 y, int point)
		{
			if (x.Abs().LessThan(Epsilon(point)))
			{
				if (y.Abs().LessThan(Epsilon(point)))
				{
					return Zero(point);
				}
				if (y.IsPositive())
				{
					return PiOver2(point);
				}
				return PiOver2(point).Neg();
			}
			F32 f = new F32(y.Div(x, point));
			if (x.IsNegative())
			{
				F32 other = new F32(f.Abs().ArcTan(point));
				other = Pi(point).Sub(other);
				if (y.IsNegative())
				{
					return other.Neg();
				}
				return other;
			}
			return f.ArcTan(point);
		}

		public virtual F32 SqrtApproximation7(int n)
		{
			F32 f = new F32(One(n));
			f = f.Add(Div(f, n)).DivPower2(1);
			f = f.Add(Div(f, n)).DivPower2(1);
			f = f.Add(Div(f, n)).DivPower2(1);
			f = f.Add(Div(f, n)).DivPower2(1);
			f = f.Add(Div(f, n)).DivPower2(1);
			return f.Add(Div(f, n)).DivPower2(1);
		}

		public virtual F32 SqrtApproximation3(int n)
		{
			F32 f = new F32(One(n).Add(Mul(Add(FromInt(6, n)), n)));
			F32 f2 = new F32(Add(One(n)).MulPower2(2));
			return new F32(f.Div(f2, n));
		}

		public virtual F32 SqrtInvApproximation3(int n)
		{
			F32 f = new F32(Add(One(n)).MulPower2(2));
			F32 f2 = new F32(One(n).Add(Mul(Add(FromInt(6, n)), n)));
			return new F32(f.Div(f2, n));
		}

		public virtual F32 ArcCos5(int point)
		{
			F32 f = new F32();
			F32 f2 = new F32();
			bool flag = LessThan(Half(point));
			f2 = ((!flag) ? One(point).Sub(Square(point)).Sqrt(point) : this);
			F32 f3 = new F32(f2.Mul(f2, point).Mul(f2, point));
			F32 f4 = new F32(f3.Mul(f2, point).Mul(f2, point));
			F32 f5 = new F32(f4.Mul(f2, point).Mul(f2, point));
			F32 f6 = new F32(f5.Mul(f2, point).Mul(f2, point));
			F32 f7 = new F32(f2);
			F32 other = new F32(f3.Div(6));
			F32 other2 = new F32(f4.Mul(3).Div(40));
			F32 other3 = new F32(f5.Mul(15).Div(336));
			F32 other4 = new F32(f6.Mul(105).Div(3024));
			if (flag)
			{
				F32 other5 = new F32(f7.Add(other).Add(other2).Add(other3)
					.Add(other4));
				return PiOver2(point).Sub(other5);
			}
			return f7.Add(other).Add(other2).Add(other3)
				.Add(other4);
		}

		public virtual F32 DecreasePrecision(int deltaPoint)
		{
			F32 f = new F32(this);
			f.value = value >> deltaPoint;
			return f;
		}

		public virtual F32 IncreasePrecision(int deltaPoint)
		{
			F32 f = new F32(this);
			f.value = value << deltaPoint;
			return f;
		}

		public virtual F32 Add(F32 other)
		{
			return new F32(value + other.value);
		}

		public virtual F32 Sub(F32 other)
		{
			return new F32(value - other.value);
		}

		public virtual F32 Mul(int scalar)
		{
			return new F32(value * scalar, 0);
		}

		public virtual F32 Div(int scalar)
		{
			return new F32(value / scalar, 0);
		}

		public virtual F32 Neg()
		{
			return new F32(-value, 0);
		}

		public virtual bool LessThan(F32 other)
		{
			return value < other.value;
		}

		public virtual bool LessOrEqual(F32 other)
		{
			return value <= other.value;
		}

		public virtual bool GreaterThan(F32 other)
		{
			return value > other.value;
		}

		public virtual bool GreaterOrEqual(F32 other)
		{
			return value >= other.value;
		}

		public bool Equals(F32 other)
		{
			return value == other.value;
		}

		public virtual F32 NotAllowed(F32 other)
		{
			value = other.value;
			return this;
		}

		public virtual bool IsNegative()
		{
			return value < 0;
		}

		public virtual bool IsPositive()
		{
			return !IsNegative();
		}

		public virtual int ToInt(int n)
		{
			return value >> n;
		}

		public static F32 FromInt(int intValue, int n)
		{
			return new F32(intValue << n, n);
		}

		public virtual F32 DegreeToRadian(int point)
		{
			return Div(Number180OverPi(point), point);
		}

		public virtual F32 RadianToDegree(int point)
		{
			return Mul(Number180OverPi(point), point);
		}

		public static F32 Pi(int point)
		{
			return ConvertConstant16(205887, point);
		}

		public static F32 TwoPi(int point)
		{
			return Pi(point).MulPower2(1);
		}

		public static F32 PiOver2(int point)
		{
			return Pi(point).DivPower2(1);
		}

		public static F32 PiOver4(int point)
		{
			return Pi(point).DivPower2(2);
		}

		public static F32 Number180OverPi(int point)
		{
			return FromInt(180, point).Div(Pi(point), point);
		}

		public static F32 Epsilon(int point)
		{
			return new F32(10, point);
		}

		public static F32 One(int point)
		{
			return new F32(1 << point, point);
		}

		public static F32 Zero(int point)
		{
			return new F32(0, point);
		}

		public static F32 Half(int point)
		{
			return new F32(1 << point - 1, point);
		}

		public static F32 Quarter(int point)
		{
			return new F32(1 << point - 2, point);
		}

		public static F32 ThreeQuarter(int point)
		{
			return new F32((1 << point - 1) | (1 << point - 2), point);
		}

		public static F32 OneHundredth(int point)
		{
			return FromInt(100, point).Inverse(point);
		}

		public static F32 MaxValue(int point)
		{
			int fpValue = int.MaxValue;
			return new F32(fpValue, point);
		}

		public static F32 MinValue(int point)
		{
			return MaxValue(point).Neg();
		}

		public virtual int GetInternalRep(int pt)
		{
			return value;
		}

		public virtual int ToFixedPoint(int a3)
		{
			return value;
		}

		public static int FromFixedPoint(int fp, int a4)
		{
			return fp;
		}

		public static void ConvertArrayToBaseType(int[] output, F32[] input, int length, int point)
		{
			for (int i = 0; i < length; i++)
			{
				output[i] = input[i].ToFixedPoint(point);
			}
		}

		public static long GetRightMask(int nbits)
		{
			long num = 0L;
			for (int i = 0; i < nbits; i++)
			{
				num <<= 1;
				num |= 1;
			}
			return num;
		}

		public static long GetLeftMask(int nbits)
		{
			return ~GetRightMask(64 - nbits);
		}

		public static F32 ConvertConstant16(int constant, int point)
		{
			if (point == 16)
			{
				return new F32(constant, 16);
			}
			if (point < 16)
			{
				return new F32(constant >> 16 - point, point);
			}
			return new F32(constant << point - 16, point);
		}

		public static F32[] InstArrayF32(int size)
		{
			F32[] array = new F32[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new F32();
			}
			return array;
		}

		public static F32[][] InstArrayF32(int size1, int size2)
		{
			F32[][] array = new F32[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new F32[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new F32();
				}
			}
			return array;
		}

		public static F32[][][] InstArrayF32(int size1, int size2, int size3)
		{
			F32[][][] array = new F32[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new F32[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new F32[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new F32();
					}
				}
			}
			return array;
		}
	}
}
