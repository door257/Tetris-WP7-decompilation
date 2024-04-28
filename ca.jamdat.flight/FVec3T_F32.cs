namespace ca.jamdat.flight
{
	public class FVec3T_F32
	{
		public static FVec3T_F32[] mPool = InstArrayFVec3T_F32(100);

		public static int mPoolIndex = -1;

		public F32 x;

		public F32 y;

		public F32 z;

		public FVec3T_F32()
		{
			x = new F32();
			y = new F32();
			z = new F32();
		}

		public FVec3T_F32(F32 value)
		{
			x = new F32(value);
			y = new F32(value);
			z = new F32(value);
		}

		public FVec3T_F32(F32 i, F32 j, F32 k)
		{
			x = new F32(i);
			y = new F32(j);
			z = new F32(k);
		}

		public FVec3T_F32(FVec3T_F32 other)
		{
			x = new F32(other.x);
			y = new F32(other.y);
			z = new F32(other.z);
		}

		public virtual FVec3T_F32 ConvertTranslationFromMaxToFlight()
		{
			return new FVec3T_F32(x, z, y.Neg());
		}

		public virtual FVec3T_F32 ConvertScaleFromMaxToFlight()
		{
			return new FVec3T_F32(x, z, y);
		}

		public virtual F32 Dot(FVec3T_F32 other, int point)
		{
			return x.Mul(other.x, point).Add(y.Mul(other.y, point)).Add(z.Mul(other.z, point));
		}

		public virtual FVec3T_F32 Cross(FVec3T_F32 other, int point)
		{
			FVec3T_F32 result = Get();
			Cross(other, point, result);
			return result;
		}

		public virtual void Cross(FVec3T_F32 other, int point, FVec3T_F32 result)
		{
			result.x = y.Mul(other.z, point).Sub(z.Mul(other.y, point));
			result.y = z.Mul(other.x, point).Sub(x.Mul(other.z, point));
			result.z = x.Mul(other.y, point).Sub(y.Mul(other.x, point));
		}

		public virtual F32 Length(int point)
		{
			F32 f = new F32(x.Square(point).Add(y.Square(point)).Add(z.Square(point)));
			return f.Sqrt(point);
		}

		public virtual F32 LengthHP(int point)
		{
			long num = x.value;
			long num2 = y.value;
			long num3 = z.value;
			long num4 = num * num + num2 * num2 + num3 * num3;
			long num5 = -2305843009213693952L;
			int num6 = 0;
			while ((num4 & num5) == 0 && num6 < 28)
			{
				num6 += 2;
				num5 >>= 2;
			}
			int num7 = 32 - num6;
			F32 f = new F32((int)(num4 >> num7), num6);
			if (num7 >= 16)
			{
				return f.Sqrt(num6).IncreasePrecision(num7 - 16);
			}
			return f.Sqrt(num6).DecreasePrecision(16 - num7);
		}

		public virtual F32 LengthSquared(int point)
		{
			return x.Square(point).Add(y.Square(point)).Add(z.Square(point));
		}

		public static F32 LengthSquared(F32 inX, F32 inY, F32 inZ, int point)
		{
			return inX.Square(point).Add(inY.Square(point)).Add(inZ.Square(point));
		}

		public virtual void DestructiveNormalizeHP(int point)
		{
			F32 f = new F32(LengthHP(point).Inverse(point));
			x = x.Mul(f, point);
			y = y.Mul(f, point);
			z = z.Mul(f, point);
		}

		public virtual FVec3T_F32 NormalizedHP(int point)
		{
			FVec3T_F32 fVec3T_F = Get(this);
			fVec3T_F.DestructiveNormalizeHP(point);
			return fVec3T_F;
		}

		public virtual void NormalizedHP(FVec3T_F32 returnValue, int point)
		{
			returnValue.Assign(this);
			returnValue.DestructiveNormalizeHP(point);
		}

		public virtual void DestructiveNormalize(int point)
		{
			F32 f = new F32(Length(point).Inverse(point));
			x = x.Mul(f, point);
			y = y.Mul(f, point);
			z = z.Mul(f, point);
		}

		public virtual FVec3T_F32 Normalized(int point)
		{
			FVec3T_F32 fVec3T_F = Get(this);
			fVec3T_F.DestructiveNormalize(point);
			return fVec3T_F;
		}

		public virtual void Normalized(FVec3T_F32 returnValue, int point)
		{
			returnValue.Assign(this);
			returnValue.DestructiveNormalize(point);
		}

		public virtual FVec3T_F32 Interpolate(FVec3T_F32 other, F32 factor, int point)
		{
			FVec3T_F32 fVec3T_F = Get();
			Interpolate(fVec3T_F, other.x, other.y, other.z, factor, point);
			return fVec3T_F;
		}

		public virtual void Interpolate(FVec3T_F32 @out, F32 ox, F32 oy, F32 oz, F32 factor, int point)
		{
			F32 f = new F32(F32.One(point).Sub(factor));
			@out.x = x.Mul(f, point).Add(ox.Mul(factor, point));
			@out.y = y.Mul(f, point).Add(oy.Mul(factor, point));
			@out.z = z.Mul(f, point).Add(oz.Mul(factor, point));
		}

		public virtual FVec3T_F32 GetAPerpendicularUnitVector(int point)
		{
			FVec3T_F32 fVec3T_F = Get();
			GetAPerpendicularUnitVector(fVec3T_F, point);
			return fVec3T_F;
		}

		public virtual void GetAPerpendicularUnitVector(FVec3T_F32 @out, int point)
		{
			F32 f = new F32(x.Square(point).Add(z.Square(point)));
			F32 f2 = new F32(f.Sqrt(point));
			if (f2.LessThan(F32.Epsilon(point)))
			{
				@out.x = F32.One(point);
				@out.y = F32.Zero(point);
				@out.z = F32.Zero(point);
			}
			else
			{
				F32 f3 = new F32(f2.Inverse(point));
				@out.x = z.Mul(f3, point);
				@out.y = F32.Zero(point);
				@out.z = x.Mul(f3, point).Neg();
			}
		}

		public static F32 GetAngleBetweenNormalizedVectors(FVec3T_F32 vec1, FVec3T_F32 vec2, int point)
		{
			F32 f = new F32(vec1.Dot(vec2, point));
			if (f.GreaterOrEqual(F32.One(point)))
			{
				return F32.Zero(point);
			}
			if (f.LessOrEqual(F32.One(point).Neg()))
			{
				return F32.Pi(point);
			}
			return f.ArcCos(point);
		}

		public virtual FVec3T_F32 Mul(F32 scalar, int point)
		{
			return Get(x.Mul(scalar, point), y.Mul(scalar, point), z.Mul(scalar, point));
		}

		public virtual FVec3T_F32 Div(F32 scalar, int point)
		{
			return Get(x.Div(scalar, point), y.Div(scalar, point), z.Div(scalar, point));
		}

		public virtual FVec3T_F32 Add(FVec3T_F32 other)
		{
			return Get(x.Add(other.x), y.Add(other.y), z.Add(other.z));
		}

		public virtual FVec3T_F32 Sub(FVec3T_F32 other)
		{
			return Get(x.Sub(other.x), y.Sub(other.y), z.Sub(other.z));
		}

		public virtual FVec3T_F32 AddAssign(FVec3T_F32 other)
		{
			x = x.Add(other.x);
			y = y.Add(other.y);
			z = z.Add(other.z);
			return this;
		}

		public virtual FVec3T_F32 SubAssign(FVec3T_F32 other)
		{
			x = x.Sub(other.x);
			y = y.Sub(other.y);
			z = z.Sub(other.z);
			return this;
		}

		public virtual FVec3T_F32 Neg()
		{
			return Get(x.Neg(), y.Neg(), z.Neg());
		}

		public virtual FVec3T_F32 Inverse(int point)
		{
			return Get(x.Inverse(point), y.Inverse(point), z.Inverse(point));
		}

		public virtual FVec3T_F32 Assign(FVec3T_F32 other)
		{
			x = other.x;
			y = other.y;
			z = other.z;
			return this;
		}

		public bool Equals(FVec3T_F32 other)
		{
			if (x.Equals(other.x) && y.Equals(other.y))
			{
				return z.Equals(other.z);
			}
			return false;
		}

		public virtual FVec3T_F32 DecreasePrecision(int deltaPoint)
		{
			return Get(x.DecreasePrecision(deltaPoint), y.DecreasePrecision(deltaPoint), z.DecreasePrecision(deltaPoint));
		}

		public virtual FVec3T_F32 IncreasePrecision(int deltaPoint)
		{
			return Get(x.IncreasePrecision(deltaPoint), y.IncreasePrecision(deltaPoint), z.IncreasePrecision(deltaPoint));
		}

		public static FVec3T_F32 UnitI(int point)
		{
			return Get(F32.One(point), F32.Zero(point), F32.Zero(point));
		}

		public static FVec3T_F32 UnitJ(int point)
		{
			return Get(F32.Zero(point), F32.One(point), F32.Zero(point));
		}

		public static FVec3T_F32 UnitK(int point)
		{
			return Get(F32.Zero(point), F32.Zero(point), F32.One(point));
		}

		public static FVec3T_F32 Zero(int point)
		{
			return Get(F32.Zero(point), F32.Zero(point), F32.Zero(point));
		}

		public static FVec3T_F32 One(int point)
		{
			return Get(F32.One(point), F32.One(point), F32.One(point));
		}

		public static FVec3T_F32 Get()
		{
			if (mPoolIndex >= 0)
			{
				return mPool[mPoolIndex--];
			}
			return new FVec3T_F32();
		}

		public static FVec3T_F32 Get(F32 i, F32 j, F32 k)
		{
			if (mPoolIndex >= 0)
			{
				FVec3T_F32 fVec3T_F = mPool[mPoolIndex--];
				fVec3T_F.x = i;
				fVec3T_F.y = j;
				fVec3T_F.z = k;
				return fVec3T_F;
			}
			return new FVec3T_F32(i, j, k);
		}

		public static FVec3T_F32 Get(FVec3T_F32 copy)
		{
			if (mPoolIndex >= 0)
			{
				FVec3T_F32 fVec3T_F = mPool[mPoolIndex--];
				fVec3T_F.Assign(copy);
				return fVec3T_F;
			}
			return new FVec3T_F32(copy);
		}

		public virtual void OnSerialize(Package _package)
		{
			x = _package.SerializeIntrinsic(x);
			y = _package.SerializeIntrinsic(y);
			z = _package.SerializeIntrinsic(z);
		}

		public static FVec3T_F32[] InstArrayFVec3T_F32(int size)
		{
			FVec3T_F32[] array = new FVec3T_F32[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FVec3T_F32();
			}
			return array;
		}

		public static FVec3T_F32[][] InstArrayFVec3T_F32(int size1, int size2)
		{
			FVec3T_F32[][] array = new FVec3T_F32[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FVec3T_F32[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FVec3T_F32();
				}
			}
			return array;
		}

		public static FVec3T_F32[][][] InstArrayFVec3T_F32(int size1, int size2, int size3)
		{
			FVec3T_F32[][][] array = new FVec3T_F32[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FVec3T_F32[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FVec3T_F32[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FVec3T_F32();
					}
				}
			}
			return array;
		}
	}
}
