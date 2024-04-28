namespace ca.jamdat.flight
{
	public class FQuaternionT_F32
	{
		public const sbyte QUATERNION_FP = 28;

		public static FQuaternionT_F32[] mPool = InstArrayFQuaternionT_F32(50);

		public static int mPoolIndex = -1;

		public F32 s;

		public F32 v_x;

		public F32 v_y;

		public F32 v_z;

		public FQuaternionT_F32()
		{
			s = new F32();
			v_x = new F32();
			v_y = new F32();
			v_z = new F32();
		}

		public FQuaternionT_F32(F32 scalarPart, FVec3T_F32 vectorPart, int point)
		{
			s = new F32(scalarPart);
			v_x = new F32(vectorPart.x);
			v_y = new F32(vectorPart.y);
			v_z = new F32(vectorPart.z);
			DestructiveNormalize(point);
		}

		public FQuaternionT_F32(FQuaternionT_F32 other)
		{
			s = new F32(other.s);
			v_x = new F32(other.v_x);
			v_y = new F32(other.v_y);
			v_z = new F32(other.v_z);
		}

		public FQuaternionT_F32(FVec3T_F32 normalizedAxis, F32 rotationAngle, int point)
		{
			s = new F32();
			v_x = new F32();
			v_y = new F32();
			v_z = new F32();
			Init(normalizedAxis, rotationAngle, point);
		}

		public FQuaternionT_F32(FVec3T_F32 rotationAsAngularVelocityTimeIn, int point)
		{
			s = new F32();
			v_x = new F32();
			v_y = new F32();
			v_z = new F32();
			F32 f = new F32(rotationAsAngularVelocityTimeIn.LengthHP(point));
			if (f.LessThan(F32.Epsilon(point)))
			{
				s = F32.One(28);
				v_x = F32.Zero(28);
				v_y = F32.Zero(28);
				v_z = F32.Zero(28);
				return;
			}
			int deltaPoint = 28 - point;
			F32 f2 = new F32(f.DivPower2(1));
			F32 f3 = new F32(f2.Sin(point));
			F32 f4 = new F32(f2.Cos(point));
			s = f4.IncreasePrecision(deltaPoint);
			v_x = rotationAsAngularVelocityTimeIn.x;
			v_y = rotationAsAngularVelocityTimeIn.y;
			v_z = rotationAsAngularVelocityTimeIn.z;
			v_x = v_x.Div(f, point);
			v_y = v_y.Div(f, point);
			v_z = v_z.Div(f, point);
			v_x = v_x.Mul(f3, point);
			v_y = v_y.Mul(f3, point);
			v_z = v_z.Mul(f3, point);
			v_x = v_x.IncreasePrecision(deltaPoint);
			v_y = v_y.IncreasePrecision(deltaPoint);
			v_z = v_z.IncreasePrecision(deltaPoint);
		}

		public FQuaternionT_F32(F32 scalarPart, FVec3T_F32 vectorPart)
		{
			s = new F32(scalarPart);
			v_x = new F32(vectorPart.x);
			v_y = new F32(vectorPart.y);
			v_z = new F32(vectorPart.z);
		}

		public virtual void Init(FVec3T_F32 normalizedAxis, F32 rotationAngle, int point)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(rotationAngle.DivPower2(1));
			F32 f2 = new F32(f.Sin(point));
			F32 f3 = new F32(f.Cos(point));
			s = f3.IncreasePrecision(deltaPoint);
			v_x = normalizedAxis.x;
			v_y = normalizedAxis.y;
			v_z = normalizedAxis.z;
			v_x = v_x.Mul(f2, point);
			v_y = v_y.Mul(f2, point);
			v_z = v_z.Mul(f2, point);
			v_x = v_x.IncreasePrecision(deltaPoint);
			v_y = v_y.IncreasePrecision(deltaPoint);
			v_z = v_z.IncreasePrecision(deltaPoint);
		}

		public static FQuaternionT_F32 GetRotationBetweenNormalizedVectors(FVec3T_F32 source, FVec3T_F32 destination, FQuaternionT_F32 quat180, int point)
		{
			FQuaternionT_F32 fQuaternionT_F = Get();
			if (GetRotationBetweenNormalizedVectors(fQuaternionT_F, source, destination, point))
			{
				return fQuaternionT_F;
			}
			return quat180;
		}

		public static FQuaternionT_F32 GetRotationBetweenNormalizedVectors(FVec3T_F32 source, FVec3T_F32 destination, int point)
		{
			FQuaternionT_F32 fQuaternionT_F = Get();
			GetRotationBetweenNormalizedVectors(fQuaternionT_F, source, destination, point);
			return fQuaternionT_F;
		}

		public static bool GetRotationBetweenNormalizedVectors(FQuaternionT_F32 @out, FVec3T_F32 source, FVec3T_F32 destination, int point)
		{
			int deltaPoint = 28 - point;
			FVec3T_F32 fVec3T_F = new FVec3T_F32(source.IncreasePrecision(deltaPoint));
			FVec3T_F32 fVec3T_F2 = new FVec3T_F32(destination.IncreasePrecision(deltaPoint));
			fVec3T_F.DestructiveNormalizeHP(28);
			fVec3T_F2.DestructiveNormalizeHP(28);
			F32 f = new F32(fVec3T_F.Dot(fVec3T_F2, 28));
			F32 f2 = new F32();
			f2 = (f.GreaterThan(F32.One(28)) ? F32.One(28) : ((!f.LessThan(F32.One(28).Neg())) ? F32.Half(28).Mul(F32.One(28).Add(f), 28).Sqrt(28) : F32.Zero(28)));
			if (f2.LessThan(F32.Epsilon(28)))
			{
				@out.Init(source.GetAPerpendicularUnitVector(point), F32.Pi(point), point);
				return false;
			}
			@out.s = f2;
			@out.v_x = fVec3T_F.y.Mul(fVec3T_F2.z, 28).Sub(fVec3T_F.z.Mul(fVec3T_F2.y, 28));
			@out.v_y = fVec3T_F.z.Mul(fVec3T_F2.x, 28).Sub(fVec3T_F.x.Mul(fVec3T_F2.z, 28));
			@out.v_z = fVec3T_F.x.Mul(fVec3T_F2.y, 28).Sub(fVec3T_F.y.Mul(fVec3T_F2.x, 28));
			F32 f3 = new F32(f2.MulPower2(1));
			@out.v_x = @out.v_x.Div(f3, 28);
			@out.v_y = @out.v_y.Div(f3, 28);
			@out.v_z = @out.v_z.Div(f3, 28);
			return true;
		}

		public virtual FQuaternionT_F32 Mul(FQuaternionT_F32 q2)
		{
			FQuaternionT_F32 result = Get();
			Mul(result, q2);
			return result;
		}

		public virtual void Mul(FQuaternionT_F32 result, FQuaternionT_F32 q2In)
		{
			F32 f = new F32(q2In.s);
			F32 f2 = new F32(q2In.v_x);
			F32 f3 = new F32(q2In.v_y);
			F32 f4 = new F32(q2In.v_z);
			F32 other = new F32(v_x.Mul(f2, 28).Add(v_y.Mul(f3, 28)).Add(v_z.Mul(f4, 28)));
			result.s = s.Mul(f, 28).Sub(other);
			result.v_x = s.Mul(f2, 28).Add(v_x.Mul(f, 28)).Add(v_y.Mul(f4, 28))
				.Sub(v_z.Mul(f3, 28));
			result.v_y = s.Mul(f3, 28).Add(v_y.Mul(f, 28)).Sub(v_x.Mul(f4, 28))
				.Add(v_z.Mul(f2, 28));
			result.v_z = s.Mul(f4, 28).Add(v_z.Mul(f, 28)).Add(v_x.Mul(f3, 28))
				.Sub(v_y.Mul(f2, 28));
			result.DestructiveNormalize();
		}

		public virtual FQuaternionT_F32 MulAssign(FQuaternionT_F32 q2In)
		{
			F32 f = new F32(q2In.s);
			F32 f2 = new F32(q2In.v_x);
			F32 f3 = new F32(q2In.v_y);
			F32 f4 = new F32(q2In.v_z);
			F32 other = new F32(v_x.Mul(f2, 28).Add(v_y.Mul(f3, 28)).Add(v_z.Mul(f4, 28)));
			F32 f5 = new F32(s.Mul(f, 28).Sub(other));
			F32 f6 = new F32(s.Mul(f2, 28).Add(v_x.Mul(f, 28)).Add(v_y.Mul(f4, 28))
				.Sub(v_z.Mul(f3, 28)));
			F32 f7 = new F32(s.Mul(f3, 28).Add(v_y.Mul(f, 28)).Sub(v_x.Mul(f4, 28))
				.Add(v_z.Mul(f2, 28)));
			F32 f8 = new F32(s.Mul(f4, 28).Add(v_z.Mul(f, 28)).Add(v_x.Mul(f3, 28))
				.Sub(v_y.Mul(f2, 28)));
			s = f5;
			v_x = f6;
			v_y = f7;
			v_z = f8;
			DestructiveNormalize();
			return this;
		}

		public virtual FQuaternionT_F32 Assign(FQuaternionT_F32 other)
		{
			s = other.s;
			v_x = other.v_x;
			v_y = other.v_y;
			v_z = other.v_z;
			return this;
		}

		public virtual void Assign(F32 inS, F32 vX, F32 vY, F32 vZ)
		{
			s = inS;
			v_x = vX;
			v_y = vY;
			v_z = vZ;
		}

		public bool Equals(FQuaternionT_F32 q2)
		{
			if (s.Equals(q2.s) && v_x.Equals(q2.v_x) && v_y.Equals(q2.v_y))
			{
				return v_z.Equals(q2.v_z);
			}
			return false;
		}

		public virtual F32 GetS()
		{
			return s;
		}

		public virtual F32 GetVx()
		{
			return v_x;
		}

		public virtual F32 GetVy()
		{
			return v_y;
		}

		public virtual F32 GetVz()
		{
			return v_z;
		}

		public virtual FQuaternionT_F32 ConvertRotationFromMaxToFlight()
		{
			FQuaternionT_F32 fQuaternionT_F = new FQuaternionT_F32();
			FQuaternionT_F32 q = new FQuaternionT_F32(GetS(), new FVec3T_F32(GetVx(), GetVy(), GetVz()), 28);
			FQuaternionT_F32 fQuaternionT_F2 = new FQuaternionT_F32(FVec3T_F32.UnitI(16), F32.PiOver2(16).Neg(), 16);
			fQuaternionT_F.Assign(fQuaternionT_F2.Mul(q).Mul(fQuaternionT_F2.GetInverseForNormalized()));
			return fQuaternionT_F;
		}

		public virtual FVec3T_F32 Conjugate(FVec3T_F32 v2In, int point)
		{
			return Conjugate(v2In.x, v2In.y, v2In.z, point);
		}

		public virtual void Conjugate(FVec3T_F32 @out, FVec3T_F32 v2In, int point)
		{
			Conjugate(@out, v2In.x, v2In.y, v2In.z, point);
		}

		public virtual FVec3T_F32 Conjugate(F32 v2InX, F32 v2InY, F32 v2InZ, int point)
		{
			FVec3T_F32 fVec3T_F = FVec3T_F32.Get();
			Conjugate(fVec3T_F, v2InX, v2InY, v2InZ, point);
			return fVec3T_F;
		}

		public virtual void Conjugate(FVec3T_F32 @out, F32 v2InX, F32 v2InY, F32 v2InZ, int point)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(s.DecreasePrecision(deltaPoint));
			F32 f2 = new F32(v2InX);
			F32 f3 = new F32(v2InY);
			F32 f4 = new F32(v2InZ);
			F32 f5 = new F32(v_x.DecreasePrecision(deltaPoint));
			F32 f6 = new F32(v_y.DecreasePrecision(deltaPoint));
			F32 f7 = new F32(v_z.DecreasePrecision(deltaPoint));
			F32 f8 = new F32(f5.Mul(f2, point).Add(f6.Mul(f3, point)).Add(f7.Mul(f4, point)));
			f8 = f8.Neg();
			F32 f9 = new F32(f.Mul(f2, point).Add(f6.Mul(f4, point)).Sub(f7.Mul(f3, point)));
			F32 f10 = new F32(f.Mul(f3, point).Sub(f5.Mul(f4, point)).Add(f7.Mul(f2, point)));
			F32 f11 = new F32(f.Mul(f4, point).Add(f5.Mul(f3, point)).Sub(f6.Mul(f2, point)));
			@out.x = f9.Mul(f, point).Sub(f8.Mul(f5, point)).Sub(f10.Mul(f7, point))
				.Add(f11.Mul(f6, point));
			@out.y = f10.Mul(f, point).Sub(f8.Mul(f6, point)).Add(f9.Mul(f7, point))
				.Sub(f11.Mul(f5, point));
			@out.z = f11.Mul(f, point).Sub(f8.Mul(f7, point)).Sub(f9.Mul(f6, point))
				.Add(f10.Mul(f5, point));
		}

		public virtual FVec3T_F32 ConjugateUnitI(int point)
		{
			return ConjugateUnit(v_x.Neg(), s, v_z, v_y.Neg(), point);
		}

		public virtual FVec3T_F32 ConjugateUnitJ(int point)
		{
			return ConjugateUnit(v_y.Neg(), v_z.Neg(), s, v_x, point);
		}

		public virtual FVec3T_F32 ConjugateUnitK(int point)
		{
			return ConjugateUnit(v_z.Neg(), v_y, v_x.Neg(), s, point);
		}

		public virtual void ConjugateUnitI(FVec3T_F32 @out, int point)
		{
			ConjugateUnit(v_x.Neg(), s, v_z, v_y.Neg(), @out, point);
		}

		public virtual void ConjugateUnitJ(FVec3T_F32 @out, int point)
		{
			ConjugateUnit(v_y.Neg(), v_z.Neg(), s, v_x, @out, point);
		}

		public virtual void ConjugateUnitK(FVec3T_F32 @out, int point)
		{
			ConjugateUnit(v_z.Neg(), v_y, v_x.Neg(), s, @out, point);
		}

		public virtual FQuaternionT_F32 DestructiveInverseForNormalized()
		{
			v_x = v_x.Neg();
			v_y = v_y.Neg();
			v_z = v_z.Neg();
			return this;
		}

		public virtual FQuaternionT_F32 GetInverseForNormalized()
		{
			FQuaternionT_F32 fQuaternionT_F = Get();
			GetInverseForNormalized(fQuaternionT_F);
			return fQuaternionT_F;
		}

		public virtual void GetInverseForNormalized(FQuaternionT_F32 returnValue)
		{
			returnValue.s = s;
			returnValue.v_x = v_x.Neg();
			returnValue.v_y = v_y.Neg();
			returnValue.v_z = v_z.Neg();
		}

		public virtual FQuaternionT_F32 DestructiveInverse()
		{
			F32 other = new F32(v_x.Square(28).Add(v_y.Square(28)).Add(v_z.Square(28)));
			F32 f = new F32(s.Square(28).Add(other));
			F32 f2 = new F32(f.Inverse(28));
			s = s.Mul(f2, 28);
			v_x = v_x.Mul(f2.Neg(), 28);
			v_y = v_y.Mul(f2.Neg(), 28);
			v_z = v_z.Mul(f2.Neg(), 28);
			return this;
		}

		public virtual FQuaternionT_F32 GetInverse()
		{
			FQuaternionT_F32 fQuaternionT_F = Get();
			F32 other = new F32(v_x.Square(28).Add(v_y.Square(28)).Add(v_z.Square(28)));
			F32 f = new F32(s.Square(28).Add(other));
			F32 f2 = new F32(f.Inverse(28));
			fQuaternionT_F.s = s.Mul(f2, 28);
			fQuaternionT_F.v_x = v_x.Mul(f2.Neg(), 28);
			fQuaternionT_F.v_y = v_y.Mul(f2.Neg(), 28);
			fQuaternionT_F.v_z = v_z.Mul(f2.Neg(), 28);
			return fQuaternionT_F;
		}

		public virtual FQuaternionT_F32 DestructiveNormalize()
		{
			F32 other = new F32(v_x.Square(28).Add(v_y.Square(28)).Add(v_z.Square(28)));
			F32 f = new F32(s.Square(28).Add(other).Sqrt(28));
			F32 f2 = new F32(f.Inverse(28));
			s = s.Mul(f2, 28);
			v_x = v_x.Mul(f2, 28);
			v_y = v_y.Mul(f2, 28);
			v_z = v_z.Mul(f2, 28);
			return this;
		}

		public virtual FQuaternionT_F32 GetNormalized()
		{
			FQuaternionT_F32 fQuaternionT_F = Get(this);
			fQuaternionT_F.DestructiveNormalize();
			return fQuaternionT_F;
		}

		public virtual void SetIdentity()
		{
			s = F32.One(28);
			v_x = F32.Zero(28);
			v_y = F32.Zero(28);
			v_z = F32.Zero(28);
		}

		public virtual F32 GetNormalizedAxisAndRotationAngle(FVec3T_F32 normalizedAxis, int point)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(v_x.DecreasePrecision(deltaPoint));
			F32 f2 = new F32(v_y.DecreasePrecision(deltaPoint));
			F32 f3 = new F32(v_z.DecreasePrecision(deltaPoint));
			F32 f4 = new F32(s.ArcCos(28));
			F32 f5 = new F32(f4.MulPower2(1));
			F32 f6 = new F32(f4.Sin(28).DecreasePrecision(deltaPoint));
			if (f6.LessOrEqual(F32.Epsilon(point)))
			{
				normalizedAxis.x = F32.Zero(point);
				normalizedAxis.y = F32.Zero(point);
				normalizedAxis.z = F32.Zero(point);
				return F32.Zero(point);
			}
			normalizedAxis.x = f.Div(f6, point);
			normalizedAxis.y = f2.Div(f6, point);
			normalizedAxis.z = f3.Div(f6, point);
			return f5.DecreasePrecision(deltaPoint);
		}

		public virtual F32 GetNormalizedAxisAndSmallestRotationAngle(FVec3T_F32 normalizedAxis, int point)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(v_x.DecreasePrecision(deltaPoint));
			F32 f2 = new F32(v_y.DecreasePrecision(deltaPoint));
			F32 f3 = new F32(v_z.DecreasePrecision(deltaPoint));
			F32 f4 = new F32();
			F32 f5 = new F32();
			if (s.GreaterOrEqual(F32.Zero(28)))
			{
				f5 = s.ArcCos(28);
				f4 = f5.MulPower2(1);
				F32 f6 = new F32(f5.Sin(28).DecreasePrecision(deltaPoint));
				if (f6.LessOrEqual(F32.Epsilon(point)))
				{
					normalizedAxis.x = F32.Zero(point);
					normalizedAxis.y = F32.Zero(point);
					normalizedAxis.z = F32.Zero(point);
					return F32.Zero(point);
				}
				normalizedAxis.x = f.Div(f6, point);
				normalizedAxis.y = f2.Div(f6, point);
				normalizedAxis.z = f3.Div(f6, point);
			}
			else
			{
				f5 = s.Neg().ArcCos(28);
				f4 = f5.MulPower2(1);
				F32 f7 = new F32(f5.Sin(28).DecreasePrecision(deltaPoint));
				if (f7.LessOrEqual(F32.Epsilon(28)))
				{
					normalizedAxis.x = F32.Zero(point);
					normalizedAxis.y = F32.Zero(point);
					normalizedAxis.z = F32.Zero(point);
					return F32.Zero(point);
				}
				normalizedAxis.x = f.Div(f7.Neg(), point);
				normalizedAxis.y = f2.Div(f7.Neg(), point);
				normalizedAxis.z = f3.Div(f7.Neg(), point);
			}
			return f4.DecreasePrecision(deltaPoint);
		}

		public virtual FMat44T_F32 ToMatrix(int point)
		{
			FMat44T_F32 fMat44T_F = FMat44T_F32.Get();
			ToMatrix(fMat44T_F, point);
			return fMat44T_F;
		}

		public virtual void ToMatrix(FMat44T_F32 returnValue, int point)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(s.DecreasePrecision(deltaPoint));
			F32 f2 = new F32(v_x.DecreasePrecision(deltaPoint));
			F32 f3 = new F32(v_y.DecreasePrecision(deltaPoint));
			F32 f4 = new F32(v_z.DecreasePrecision(deltaPoint));
			F32 f5 = new F32(f2.MulPower2(1));
			F32 other = new F32(f5.Mul(f2, point));
			F32 f6 = new F32(f5.Mul(f3, point));
			F32 f7 = new F32(f5.Mul(f4, point));
			F32 other2 = new F32(f5.Mul(f, point));
			F32 f8 = new F32(f3.MulPower2(1));
			F32 other3 = new F32(f8.Mul(f3, point));
			F32 f9 = new F32(f8.Mul(f4, point));
			F32 other4 = new F32(f8.Mul(f, point));
			F32 f10 = new F32(f4.MulPower2(1));
			F32 other5 = new F32(f10.Mul(f4, point));
			F32 other6 = new F32(f10.Mul(f, point));
			F32 f11 = new F32(F32.One(point).Sub(other));
			returnValue.m11 = F32.One(point).Sub(other3).Sub(other5);
			returnValue.m12 = f6.Sub(other6);
			returnValue.m13 = f7.Add(other4);
			returnValue.m14 = F32.Zero(point);
			returnValue.m21 = f6.Add(other6);
			returnValue.m22 = f11.Sub(other5);
			returnValue.m23 = f9.Sub(other2);
			returnValue.m24 = F32.Zero(point);
			returnValue.m31 = f7.Sub(other4);
			returnValue.m32 = f9.Add(other2);
			returnValue.m33 = f11.Sub(other3);
			returnValue.m34 = F32.Zero(point);
			returnValue.m41 = F32.Zero(point);
			returnValue.m42 = F32.Zero(point);
			returnValue.m43 = F32.Zero(point);
			returnValue.m44 = F32.One(point);
		}

		public static FQuaternionT_F32 I()
		{
			FQuaternionT_F32 fQuaternionT_F = Get();
			fQuaternionT_F.s = F32.One(28);
			fQuaternionT_F.v_x = F32.Zero(28);
			fQuaternionT_F.v_y = F32.Zero(28);
			fQuaternionT_F.v_z = F32.Zero(28);
			return fQuaternionT_F;
		}

		public static FQuaternionT_F32 Interpolate(FQuaternionT_F32 orient1, FQuaternionT_F32 orient2, F32 factor, int point)
		{
			FVec3T_F32 normalizedAxis = new FVec3T_F32();
			FQuaternionT_F32 fQuaternionT_F = new FQuaternionT_F32(orient1.GetInverseForNormalized().Mul(orient2));
			F32 f = new F32(fQuaternionT_F.GetNormalizedAxisAndSmallestRotationAngle(normalizedAxis, point));
			F32 rotationAngle = new F32(f.Mul(factor, point));
			return orient1.Mul(new FQuaternionT_F32(normalizedAxis, rotationAngle, point));
		}

		public virtual F32 GetScalarPart(int point)
		{
			return s.DecreasePrecision(28 - point);
		}

		public virtual FVec3T_F32 GetVectorPart(int point)
		{
			int deltaPoint = 28 - point;
			return FVec3T_F32.Get(v_x.DecreasePrecision(deltaPoint), v_y.DecreasePrecision(deltaPoint), v_z.DecreasePrecision(deltaPoint));
		}

		public virtual void SimulateEuler(FVec3T_F32 rotationAsAngularVelocityTimeIn, int point)
		{
			F32 f = new F32();
			F32 f2 = new F32();
			F32 f3 = new F32();
			F32 f4 = new F32();
			F32 f5 = new F32(rotationAsAngularVelocityTimeIn.LengthHP(point));
			if (f5.LessThan(F32.Epsilon(point)))
			{
				f = F32.One(28);
				f2 = F32.Zero(28);
				f3 = F32.Zero(28);
				f4 = F32.Zero(28);
			}
			else
			{
				int deltaPoint = 28 - point;
				F32 f6 = new F32(f5.DivPower2(1));
				F32 f7 = new F32(f6.Sin(point));
				F32 f8 = new F32(f6.Cos(point));
				f = f8.IncreasePrecision(deltaPoint);
				f2 = rotationAsAngularVelocityTimeIn.x;
				f3 = rotationAsAngularVelocityTimeIn.y;
				f4 = rotationAsAngularVelocityTimeIn.z;
				f2 = f2.Div(f5, point);
				f3 = f3.Div(f5, point);
				f4 = f4.Div(f5, point);
				f2 = f2.Mul(f7, point);
				f3 = f3.Mul(f7, point);
				f4 = f4.Mul(f7, point);
				f2 = f2.IncreasePrecision(deltaPoint);
				f3 = f3.IncreasePrecision(deltaPoint);
				f4 = f4.IncreasePrecision(deltaPoint);
			}
			F32 other = new F32(f2.Mul(v_x, 28).Add(f3.Mul(v_y, 28)).Add(f4.Mul(v_z, 28)));
			F32 f9 = new F32(f.Mul(s, 28).Sub(other));
			F32 f10 = new F32(f.Mul(v_x, 28).Add(f2.Mul(s, 28)).Add(f3.Mul(v_z, 28))
				.Sub(f4.Mul(v_y, 28)));
			F32 f11 = new F32(f.Mul(v_y, 28).Add(f3.Mul(s, 28)).Sub(f2.Mul(v_z, 28))
				.Add(f4.Mul(v_x, 28)));
			F32 f12 = new F32(f.Mul(v_z, 28).Add(f4.Mul(s, 28)).Add(f2.Mul(v_y, 28))
				.Sub(f3.Mul(v_x, 28)));
			s = f9;
			v_x = f10;
			v_y = f11;
			v_z = f12;
			DestructiveNormalize();
		}

		public virtual void OnSerialize(Package _package)
		{
			FVec3T_F32 fVec3T_F = FVec3T_F32.Get(v_x, v_y, v_z);
			s = _package.SerializeIntrinsic(s);
			fVec3T_F.OnSerialize(_package);
			v_x = fVec3T_F.x;
			v_y = fVec3T_F.y;
			v_z = fVec3T_F.z;
			FVec3T_F32.mPool[++FVec3T_F32.mPoolIndex] = fVec3T_F;
		}

		public static FQuaternionT_F32 Get()
		{
			if (mPoolIndex >= 0)
			{
				return mPool[mPoolIndex--];
			}
			return new FQuaternionT_F32();
		}

		public static FQuaternionT_F32 Get(FQuaternionT_F32 copy)
		{
			if (mPoolIndex >= 0)
			{
				FQuaternionT_F32 fQuaternionT_F = mPool[mPoolIndex--];
				fQuaternionT_F.Assign(copy);
				return fQuaternionT_F;
			}
			return new FQuaternionT_F32(copy);
		}

		public static FQuaternionT_F32 Get(F32 scalarPart, FVec3T_F32 vectorPart)
		{
			if (mPoolIndex >= 0)
			{
				FQuaternionT_F32 fQuaternionT_F = mPool[mPoolIndex--];
				fQuaternionT_F.s = scalarPart;
				fQuaternionT_F.v_x = vectorPart.x;
				fQuaternionT_F.v_y = vectorPart.y;
				fQuaternionT_F.v_z = vectorPart.z;
				return fQuaternionT_F;
			}
			return new FQuaternionT_F32(scalarPart, vectorPart);
		}

		public static FQuaternionT_F32 Get(FVec3T_F32 rotationAsAngularVelocityTimeIn, int point)
		{
			if (mPoolIndex >= 0)
			{
				FQuaternionT_F32 fQuaternionT_F = mPool[mPoolIndex--];
				F32 f = new F32(rotationAsAngularVelocityTimeIn.LengthHP(point));
				if (f.LessThan(F32.Epsilon(point)))
				{
					fQuaternionT_F.s = F32.One(28);
					fQuaternionT_F.v_x = F32.Zero(28);
					fQuaternionT_F.v_y = F32.Zero(28);
					fQuaternionT_F.v_z = F32.Zero(28);
				}
				else
				{
					int deltaPoint = 28 - point;
					F32 f2 = new F32(f.DivPower2(1));
					F32 f3 = new F32(f2.Sin(point));
					F32 f4 = new F32(f2.Cos(point));
					fQuaternionT_F.s = f4.IncreasePrecision(deltaPoint);
					fQuaternionT_F.v_x = rotationAsAngularVelocityTimeIn.x;
					fQuaternionT_F.v_y = rotationAsAngularVelocityTimeIn.y;
					fQuaternionT_F.v_z = rotationAsAngularVelocityTimeIn.z;
					fQuaternionT_F.v_x = fQuaternionT_F.v_x.Div(f, point);
					fQuaternionT_F.v_y = fQuaternionT_F.v_y.Div(f, point);
					fQuaternionT_F.v_z = fQuaternionT_F.v_z.Div(f, point);
					fQuaternionT_F.v_x = fQuaternionT_F.v_x.Mul(f3, point);
					fQuaternionT_F.v_y = fQuaternionT_F.v_y.Mul(f3, point);
					fQuaternionT_F.v_z = fQuaternionT_F.v_z.Mul(f3, point);
					fQuaternionT_F.v_x = fQuaternionT_F.v_x.IncreasePrecision(deltaPoint);
					fQuaternionT_F.v_y = fQuaternionT_F.v_y.IncreasePrecision(deltaPoint);
					fQuaternionT_F.v_z = fQuaternionT_F.v_z.IncreasePrecision(deltaPoint);
				}
				return fQuaternionT_F;
			}
			return new FQuaternionT_F32(rotationAsAngularVelocityTimeIn, point);
		}

		public virtual FQuaternionT_F32 DestructiveNormalize(int point)
		{
			int deltaPoint = 28 - point;
			F32 other = new F32(v_x.Square(point).Add(v_y.Square(point)).Add(v_z.Square(point)));
			F32 f = new F32(s.Square(point).Add(other).Sqrt(point));
			F32 f2 = new F32(f.Inverse(point));
			s = s.Mul(f2, point).IncreasePrecision(deltaPoint);
			v_x = v_x.Mul(f2, point);
			v_y = v_y.Mul(f2, point);
			v_z = v_z.Mul(f2, point);
			v_x = v_x.IncreasePrecision(deltaPoint);
			v_y = v_y.IncreasePrecision(deltaPoint);
			v_z = v_z.IncreasePrecision(deltaPoint);
			return this;
		}

		public virtual void Mul(FVec3T_F32 v2In, int point, FQuaternionT_F32 result)
		{
			int deltaPoint = 28 - point;
			F32 f = new F32(s.DecreasePrecision(deltaPoint));
			F32 f2 = new F32(v2In.x);
			F32 f3 = new F32(v2In.y);
			F32 f4 = new F32(v2In.z);
			F32 f5 = new F32(v_x.DecreasePrecision(deltaPoint));
			F32 f6 = new F32(v_y.DecreasePrecision(deltaPoint));
			F32 f7 = new F32(v_z.DecreasePrecision(deltaPoint));
			result.s = f5.Mul(f2, point).Add(f6.Mul(f3, point)).Add(f7.Mul(f4, point));
			result.s = result.s.Neg();
			result.v_x = f.Mul(f2, point).Add(f6.Mul(f4, point)).Sub(f7.Mul(f3, point));
			result.v_y = f.Mul(f3, point).Sub(f5.Mul(f4, point)).Add(f7.Mul(f2, point));
			result.v_z = f.Mul(f4, point).Add(f5.Mul(f3, point)).Sub(f6.Mul(f2, point));
		}

		public virtual FVec3T_F32 ConjugateUnit(F32 ss, F32 vx, F32 vy, F32 vz, int point)
		{
			FVec3T_F32 fVec3T_F = FVec3T_F32.Get(vx.Mul(s, 28).Sub(ss.Mul(v_x, 28)).Sub(vy.Mul(v_z, 28))
				.Add(vz.Mul(v_y, 28)), vy.Mul(s, 28).Sub(ss.Mul(v_y, 28)).Add(vx.Mul(v_z, 28))
				.Sub(vz.Mul(v_x, 28)), vz.Mul(s, 28).Sub(ss.Mul(v_z, 28)).Sub(vx.Mul(v_y, 28))
				.Add(vy.Mul(v_x, 28)));
			int deltaPoint = 28 - point;
			fVec3T_F.x = fVec3T_F.x.DecreasePrecision(deltaPoint);
			fVec3T_F.y = fVec3T_F.y.DecreasePrecision(deltaPoint);
			fVec3T_F.z = fVec3T_F.z.DecreasePrecision(deltaPoint);
			return fVec3T_F;
		}

		public virtual void ConjugateUnit(F32 ss, F32 vx, F32 vy, F32 vz, FVec3T_F32 @out, int point)
		{
			@out.x = vx.Mul(s, 28).Sub(ss.Mul(v_x, 28)).Sub(vy.Mul(v_z, 28))
				.Add(vz.Mul(v_y, 28));
			@out.y = vy.Mul(s, 28).Sub(ss.Mul(v_y, 28)).Add(vx.Mul(v_z, 28))
				.Sub(vz.Mul(v_x, 28));
			@out.z = vz.Mul(s, 28).Sub(ss.Mul(v_z, 28)).Sub(vx.Mul(v_y, 28))
				.Add(vy.Mul(v_x, 28));
			int deltaPoint = 28 - point;
			@out.x = @out.x.DecreasePrecision(deltaPoint);
			@out.y = @out.y.DecreasePrecision(deltaPoint);
			@out.z = @out.z.DecreasePrecision(deltaPoint);
		}

		public static FQuaternionT_F32[] InstArrayFQuaternionT_F32(int size)
		{
			FQuaternionT_F32[] array = new FQuaternionT_F32[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FQuaternionT_F32();
			}
			return array;
		}

		public static FQuaternionT_F32[][] InstArrayFQuaternionT_F32(int size1, int size2)
		{
			FQuaternionT_F32[][] array = new FQuaternionT_F32[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FQuaternionT_F32[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FQuaternionT_F32();
				}
			}
			return array;
		}

		public static FQuaternionT_F32[][][] InstArrayFQuaternionT_F32(int size1, int size2, int size3)
		{
			FQuaternionT_F32[][][] array = new FQuaternionT_F32[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FQuaternionT_F32[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FQuaternionT_F32[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FQuaternionT_F32();
					}
				}
			}
			return array;
		}
	}
}
