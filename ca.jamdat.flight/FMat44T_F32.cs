namespace ca.jamdat.flight
{
	public class FMat44T_F32
	{
		public static FMat44T_F32[] mPool = InstArrayFMat44T_F32(20);

		public static int mPoolIndex = -1;

		public F32 m11;

		public F32 m21;

		public F32 m31;

		public F32 m41;

		public F32 m12;

		public F32 m22;

		public F32 m32;

		public F32 m42;

		public F32 m13;

		public F32 m23;

		public F32 m33;

		public F32 m43;

		public F32 m14;

		public F32 m24;

		public F32 m34;

		public F32 m44;

		public FMat44T_F32()
		{
			m11 = new F32();
			m21 = new F32();
			m31 = new F32();
			m41 = new F32();
			m12 = new F32();
			m22 = new F32();
			m32 = new F32();
			m42 = new F32();
			m13 = new F32();
			m23 = new F32();
			m33 = new F32();
			m43 = new F32();
			m14 = new F32();
			m24 = new F32();
			m34 = new F32();
			m44 = new F32();
		}

		public FMat44T_F32(int point)
		{
			m11 = new F32();
			m21 = new F32();
			m31 = new F32();
			m41 = new F32();
			m12 = new F32();
			m22 = new F32();
			m32 = new F32();
			m42 = new F32();
			m13 = new F32();
			m23 = new F32();
			m33 = new F32();
			m43 = new F32();
			m14 = new F32();
			m24 = new F32();
			m34 = new F32();
			m44 = new F32();
			SetIdentity(point);
		}

		public FMat44T_F32(F32 M11, F32 M12, F32 M13, F32 M14, F32 M21, F32 M22, F32 M23, F32 M24, F32 M31, F32 M32, F32 M33, F32 M34, F32 M41, F32 M42, F32 M43, F32 M44, int a7)
		{
			m11 = new F32(M11);
			m21 = new F32(M21);
			m31 = new F32(M31);
			m41 = new F32(M41);
			m12 = new F32(M12);
			m22 = new F32(M22);
			m32 = new F32(M32);
			m42 = new F32(M42);
			m13 = new F32(M13);
			m23 = new F32(M23);
			m33 = new F32(M33);
			m43 = new F32(M43);
			m14 = new F32(M14);
			m24 = new F32(M24);
			m34 = new F32(M34);
			m44 = new F32(M44);
		}

		public FMat44T_F32(FMat44T_F32 o)
		{
			m11 = new F32(o.m11);
			m21 = new F32(o.m21);
			m31 = new F32(o.m31);
			m41 = new F32(o.m41);
			m12 = new F32(o.m12);
			m22 = new F32(o.m22);
			m32 = new F32(o.m32);
			m42 = new F32(o.m42);
			m13 = new F32(o.m13);
			m23 = new F32(o.m23);
			m33 = new F32(o.m33);
			m43 = new F32(o.m43);
			m14 = new F32(o.m14);
			m24 = new F32(o.m24);
			m34 = new F32(o.m34);
			m44 = new F32(o.m44);
		}

		public virtual void SetIdentity(int point)
		{
			m11 = F32.One(point);
			m22 = F32.One(point);
			m33 = F32.One(point);
			m44 = F32.One(point);
			m21 = F32.Zero(point);
			m31 = F32.Zero(point);
			m41 = F32.Zero(point);
			m12 = F32.Zero(point);
			m32 = F32.Zero(point);
			m42 = F32.Zero(point);
			m13 = F32.Zero(point);
			m23 = F32.Zero(point);
			m43 = F32.Zero(point);
			m14 = F32.Zero(point);
			m24 = F32.Zero(point);
			m34 = F32.Zero(point);
		}

		public virtual void SetTranslation(FVec3T_F32 trans, int point)
		{
			SetIdentity(point);
			m14 = trans.x;
			m24 = trans.y;
			m34 = trans.z;
		}

		public virtual void SetRotationX(F32 angle, int point)
		{
			SetIdentity(point);
			m22 = angle.Cos(point);
			m33 = angle.Cos(point);
			m23 = angle.Sin(point).Neg();
			m32 = angle.Sin(point);
		}

		public virtual void SetRotationY(F32 angle, int point)
		{
			SetIdentity(point);
			m11 = angle.Cos(point);
			m33 = angle.Cos(point);
			m13 = angle.Sin(point);
			m31 = angle.Sin(point).Neg();
		}

		public virtual void SetRotationZ(F32 angle, int point)
		{
			SetIdentity(point);
			m11 = angle.Cos(point);
			m22 = angle.Cos(point);
			m12 = angle.Sin(point).Neg();
			m21 = angle.Sin(point);
		}

		public virtual void SetShearXY(F32 shx, F32 shy, int point)
		{
			SetIdentity(point);
			m13 = shx;
			m23 = shy;
		}

		public virtual void SetScale(F32 uniformScale, int point)
		{
			SetIdentity(point);
			m11 = uniformScale;
			m22 = uniformScale;
			m33 = uniformScale;
		}

		public virtual void SetScale(FVec3T_F32 scaleFactors, int point)
		{
			SetIdentity(point);
			m11 = scaleFactors.x;
			m22 = scaleFactors.y;
			m33 = scaleFactors.z;
		}

		public virtual void GetUnitTransforms(FVec3T_F32 translation, FQuaternionT_F32 rotation, FVec3T_F32 scale, int point)
		{
			FMat44T_F32 fMat44T_F = new FMat44T_F32(this);
			translation.x = m14;
			translation.y = m24;
			translation.z = m34;
			F32 f = new F32(m11.Mul(m22, point).Mul(m33, point).Add(m12.Mul(m23, point).Mul(m31, point))
				.Add(m13.Mul(m21, point).Mul(m32, point))
				.Sub(m11.Mul(m23, point).Mul(m32, point))
				.Sub(m12.Mul(m21, point).Mul(m33, point))
				.Sub(m13.Mul(m22, point).Mul(m31, point)));
			scale.x = m11.Square(point).Add(m12.Square(point)).Add(m13.Square(point))
				.Sqrt(point);
			scale.y = m21.Square(point).Add(m22.Square(point)).Add(m23.Square(point))
				.Sqrt(point);
			scale.z = m31.Square(point).Add(m32.Square(point)).Add(m33.Square(point))
				.Sqrt(point);
			if (f.LessThan(F32.Zero(point)))
			{
				scale.Assign(scale.Neg());
			}
			FMat44T_F32 fMat44T_F2 = new FMat44T_F32();
			fMat44T_F2.SetScale(scale.Inverse(point), point);
			fMat44T_F.Assign(fMat44T_F2.Mul(fMat44T_F, point));
			F32 f2 = new F32(F32.One(point).Add(fMat44T_F.m11).Add(fMat44T_F.m22)
				.Add(fMat44T_F.m33));
			F32 f3 = new F32();
			FVec3T_F32 fVec3T_F = new FVec3T_F32();
			F32 f4 = new F32();
			if (f2.GreaterThan(F32.Epsilon(point)))
			{
				f3 = f2.Sqrt(point).MulPower2(1);
				fVec3T_F.x = fMat44T_F.m32.Sub(fMat44T_F.m23).Div(f3, point);
				fVec3T_F.y = fMat44T_F.m13.Sub(fMat44T_F.m31).Div(f3, point);
				fVec3T_F.z = fMat44T_F.m21.Sub(fMat44T_F.m12).Div(f3, point);
				f4 = f3.DivPower2(2);
			}
			else if (fMat44T_F.m11.GreaterThan(fMat44T_F.m22) && fMat44T_F.m11.GreaterThan(fMat44T_F.m33))
			{
				f3 = F32.One(point).Add(fMat44T_F.m11).Sub(fMat44T_F.m22)
					.Sub(fMat44T_F.m33)
					.Sqrt(point)
					.MulPower2(1);
				fVec3T_F.x = f3.DivPower2(2);
				fVec3T_F.y = fMat44T_F.m21.Add(fMat44T_F.m12).Div(f3, point);
				fVec3T_F.z = fMat44T_F.m13.Add(fMat44T_F.m31).Div(f3, point);
				f4 = fMat44T_F.m32.Sub(fMat44T_F.m23).Div(f3, point);
			}
			else if (fMat44T_F.m22.GreaterThan(fMat44T_F.m33))
			{
				f3 = F32.One(point).Sub(fMat44T_F.m11).Add(fMat44T_F.m22)
					.Sub(fMat44T_F.m33)
					.Sqrt(point)
					.MulPower2(1);
				fVec3T_F.x = fMat44T_F.m21.Add(fMat44T_F.m12).Div(f3, point);
				fVec3T_F.y = f3.DivPower2(2);
				fVec3T_F.z = fMat44T_F.m32.Add(fMat44T_F.m23).Div(f3, point);
				f4 = fMat44T_F.m13.Sub(fMat44T_F.m31).Div(f3, point);
			}
			else
			{
				f3 = F32.One(point).Sub(fMat44T_F.m11).Sub(fMat44T_F.m22)
					.Add(fMat44T_F.m33)
					.Sqrt(point)
					.MulPower2(1);
				fVec3T_F.x = fMat44T_F.m13.Add(fMat44T_F.m31).Div(f3, point);
				fVec3T_F.y = fMat44T_F.m32.Add(fMat44T_F.m23).Div(f3, point);
				fVec3T_F.z = f3.DivPower2(2);
				f4 = fMat44T_F.m21.Sub(fMat44T_F.m12).Div(f3, point);
			}
			rotation.Assign(new FQuaternionT_F32(f4, fVec3T_F, point).GetNormalized());
		}

		public virtual FMat44T_F32 Inverse(int point)
		{
			FMat44T_F32 result = Get(point);
			Inverse(point, result);
			return result;
		}

		public virtual void Inverse(int point, FMat44T_F32 result)
		{
			FastInverse(point, result);
		}

		public virtual FMat44T_F32 FastInverse(int point)
		{
			FMat44T_F32 fMat44T_F = Get(point);
			FastInverse(point, fMat44T_F);
			return fMat44T_F;
		}

		public virtual void FastInverse(int point, FMat44T_F32 @out)
		{
			F32 f = new F32(m11.Square(point).Add(m12.Square(point)).Add(m13.Square(point)));
			f = f.Inverse(point);
			@out.m11 = f.Mul(m11, point);
			@out.m21 = f.Mul(m12, point);
			@out.m31 = f.Mul(m13, point);
			@out.m12 = f.Mul(m21, point);
			@out.m22 = f.Mul(m22, point);
			@out.m32 = f.Mul(m23, point);
			@out.m13 = f.Mul(m31, point);
			@out.m23 = f.Mul(m32, point);
			@out.m33 = f.Mul(m33, point);
			@out.m14 = @out.m11.Mul(m14, point).Add(@out.m12.Mul(m24, point)).Add(@out.m13.Mul(m34, point))
				.Neg();
			@out.m24 = @out.m21.Mul(m14, point).Add(@out.m22.Mul(m24, point)).Add(@out.m23.Mul(m34, point))
				.Neg();
			@out.m34 = @out.m31.Mul(m14, point).Add(@out.m32.Mul(m24, point)).Add(@out.m33.Mul(m34, point))
				.Neg();
			@out.m41 = F32.Zero(point);
			@out.m42 = F32.Zero(point);
			@out.m43 = F32.Zero(point);
			@out.m44 = F32.One(point);
		}

		public virtual FMat44T_F32 Mul(FMat44T_F32 other, int point)
		{
			FMat44T_F32 result = Get(point);
			Mul(result, other, point);
			return result;
		}

		public virtual void Mul(FMat44T_F32 result, FMat44T_F32 other, int point)
		{
			result.m11 = m11.Mul(other.m11, point).Add(m12.Mul(other.m21, point)).Add(m13.Mul(other.m31, point));
			result.m21 = m21.Mul(other.m11, point).Add(m22.Mul(other.m21, point)).Add(m23.Mul(other.m31, point));
			result.m31 = m31.Mul(other.m11, point).Add(m32.Mul(other.m21, point)).Add(m33.Mul(other.m31, point));
			result.m12 = m11.Mul(other.m12, point).Add(m12.Mul(other.m22, point)).Add(m13.Mul(other.m32, point));
			result.m22 = m21.Mul(other.m12, point).Add(m22.Mul(other.m22, point)).Add(m23.Mul(other.m32, point));
			result.m32 = m31.Mul(other.m12, point).Add(m32.Mul(other.m22, point)).Add(m33.Mul(other.m32, point));
			result.m13 = m11.Mul(other.m13, point).Add(m12.Mul(other.m23, point)).Add(m13.Mul(other.m33, point));
			result.m23 = m21.Mul(other.m13, point).Add(m22.Mul(other.m23, point)).Add(m23.Mul(other.m33, point));
			result.m33 = m31.Mul(other.m13, point).Add(m32.Mul(other.m23, point)).Add(m33.Mul(other.m33, point));
			result.m14 = m11.Mul(other.m14, point).Add(m12.Mul(other.m24, point)).Add(m13.Mul(other.m34, point))
				.Add(m14);
			result.m24 = m21.Mul(other.m14, point).Add(m22.Mul(other.m24, point)).Add(m23.Mul(other.m34, point))
				.Add(m24);
			result.m34 = m31.Mul(other.m14, point).Add(m32.Mul(other.m24, point)).Add(m33.Mul(other.m34, point))
				.Add(m34);
			result.m41 = F32.Zero(point);
			result.m42 = F32.Zero(point);
			result.m43 = F32.Zero(point);
			result.m44 = F32.One(point);
		}

		public virtual FMat44T_F32 CompleteMul(FMat44T_F32 other, int point)
		{
			FMat44T_F32 result = Get(point);
			CompleteMul(other, point, result);
			return result;
		}

		public virtual void CompleteMul(FMat44T_F32 other, int point, FMat44T_F32 result)
		{
			result.m11 = m11.Mul(other.m11, point).Add(m12.Mul(other.m21, point)).Add(m13.Mul(other.m31, point))
				.Add(m14.Mul(other.m41, point));
			result.m21 = m21.Mul(other.m11, point).Add(m22.Mul(other.m21, point)).Add(m23.Mul(other.m31, point))
				.Add(m24.Mul(other.m41, point));
			result.m31 = m31.Mul(other.m11, point).Add(m32.Mul(other.m21, point)).Add(m33.Mul(other.m31, point))
				.Add(m34.Mul(other.m41, point));
			result.m41 = m41.Mul(other.m11, point).Add(m42.Mul(other.m21, point)).Add(m43.Mul(other.m31, point))
				.Add(m44.Mul(other.m41, point));
			result.m12 = m11.Mul(other.m12, point).Add(m12.Mul(other.m22, point)).Add(m13.Mul(other.m32, point))
				.Add(m14.Mul(other.m42, point));
			result.m22 = m21.Mul(other.m12, point).Add(m22.Mul(other.m22, point)).Add(m23.Mul(other.m32, point))
				.Add(m24.Mul(other.m42, point));
			result.m32 = m31.Mul(other.m12, point).Add(m32.Mul(other.m22, point)).Add(m33.Mul(other.m32, point))
				.Add(m34.Mul(other.m42, point));
			result.m42 = m41.Mul(other.m12, point).Add(m42.Mul(other.m22, point)).Add(m43.Mul(other.m32, point))
				.Add(m44.Mul(other.m42, point));
			result.m13 = m11.Mul(other.m13, point).Add(m12.Mul(other.m23, point)).Add(m13.Mul(other.m33, point))
				.Add(m14.Mul(other.m43, point));
			result.m23 = m21.Mul(other.m13, point).Add(m22.Mul(other.m23, point)).Add(m23.Mul(other.m33, point))
				.Add(m24.Mul(other.m43, point));
			result.m33 = m31.Mul(other.m13, point).Add(m32.Mul(other.m23, point)).Add(m33.Mul(other.m33, point))
				.Add(m34.Mul(other.m43, point));
			result.m43 = m41.Mul(other.m13, point).Add(m42.Mul(other.m23, point)).Add(m43.Mul(other.m33, point))
				.Add(m44.Mul(other.m43, point));
			result.m14 = m11.Mul(other.m14, point).Add(m12.Mul(other.m24, point)).Add(m13.Mul(other.m34, point))
				.Add(m14.Mul(other.m44, point));
			result.m24 = m21.Mul(other.m14, point).Add(m22.Mul(other.m24, point)).Add(m23.Mul(other.m34, point))
				.Add(m24.Mul(other.m44, point));
			result.m34 = m31.Mul(other.m14, point).Add(m32.Mul(other.m24, point)).Add(m33.Mul(other.m34, point))
				.Add(m34.Mul(other.m44, point));
			result.m44 = m41.Mul(other.m14, point).Add(m42.Mul(other.m24, point)).Add(m43.Mul(other.m34, point))
				.Add(m44.Mul(other.m44, point));
		}

		public virtual FVec3T_F32 Transform(FVec3T_F32 other, int point)
		{
			FVec3T_F32 result = FVec3T_F32.Get();
			Transform(result, other.x, other.y, other.z, point);
			return result;
		}

		public virtual void Transform(FVec3T_F32 result, FVec3T_F32 other, int point)
		{
			Transform(result, other.x, other.y, other.z, point);
		}

		public virtual void Transform(FVec3T_F32 result, F32 vx, F32 vy, F32 vz, int point)
		{
			result.x = m11.Mul(vx, point).Add(m12.Mul(vy, point)).Add(m13.Mul(vz, point))
				.Add(m14);
			result.y = m21.Mul(vx, point).Add(m22.Mul(vy, point)).Add(m23.Mul(vz, point))
				.Add(m24);
			result.z = m31.Mul(vx, point).Add(m32.Mul(vy, point)).Add(m33.Mul(vz, point))
				.Add(m34);
		}

		public virtual FVec3T_F32 TransformNormal(FVec3T_F32 other, int point)
		{
			FVec3T_F32 result = FVec3T_F32.Get();
			TransformNormal(result, other.x, other.y, other.z, point);
			return result;
		}

		public virtual void TransformNormal(FVec3T_F32 result, FVec3T_F32 other, int point)
		{
			TransformNormal(result, other.x, other.y, other.z, point);
		}

		public virtual void TransformNormal(FVec3T_F32 result, F32 vx, F32 vy, F32 vz, int point)
		{
			result.x = m11.Mul(vx, point).Add(m12.Mul(vy, point)).Add(m13.Mul(vz, point));
			result.y = m21.Mul(vx, point).Add(m22.Mul(vy, point)).Add(m23.Mul(vz, point));
			result.z = m31.Mul(vx, point).Add(m32.Mul(vy, point)).Add(m33.Mul(vz, point));
		}

		public virtual FVec3T_F32 GetTranslationVector()
		{
			return FVec3T_F32.Get(m14, m24, m34);
		}

		public virtual void SetTranslationVector(FVec3T_F32 v)
		{
			m14 = v.x;
			m24 = v.y;
			m34 = v.z;
		}

		public virtual FMat44T_F32 GetMat33Part(int point)
		{
			FMat44T_F32 fMat44T_F = Get(this);
			fMat44T_F.m14 = F32.Zero(point);
			fMat44T_F.m24 = F32.Zero(point);
			fMat44T_F.m34 = F32.Zero(point);
			return fMat44T_F;
		}

		public virtual void SetMat33Part(FMat44T_F32 m)
		{
			m11 = m.m11;
			m12 = m.m12;
			m13 = m.m13;
			m21 = m.m21;
			m22 = m.m22;
			m23 = m.m23;
			m31 = m.m31;
			m32 = m.m32;
			m33 = m.m33;
		}

		public virtual void PostScale(FVec3T_F32 v, int point)
		{
			m11 = v.x.Mul(m11, point);
			m21 = v.x.Mul(m21, point);
			m31 = v.x.Mul(m31, point);
			m12 = v.y.Mul(m12, point);
			m22 = v.y.Mul(m22, point);
			m32 = v.y.Mul(m32, point);
			m13 = v.z.Mul(m13, point);
			m23 = v.z.Mul(m23, point);
			m33 = v.z.Mul(m33, point);
		}

		public virtual FMat44T_F32 TransposeMat33Part(int point)
		{
			FMat44T_F32 result = Get(point);
			TransposeMat33Part(point, result);
			return result;
		}

		public virtual void TransposeMat33Part(int a8, FMat44T_F32 result)
		{
			result.m11 = m11;
			result.m12 = m21;
			result.m13 = m31;
			result.m21 = m12;
			result.m22 = m22;
			result.m23 = m32;
			result.m31 = m13;
			result.m32 = m23;
			result.m33 = m33;
		}

		public virtual FMat44T_F32 Transpose(int point)
		{
			FMat44T_F32 result = Get(point);
			Transpose(point, result);
			return result;
		}

		public virtual void Transpose(int a9, FMat44T_F32 result)
		{
			result.m11 = m11;
			result.m12 = m21;
			result.m13 = m31;
			result.m14 = m41;
			result.m21 = m12;
			result.m22 = m22;
			result.m23 = m32;
			result.m24 = m42;
			result.m31 = m13;
			result.m32 = m23;
			result.m33 = m33;
			result.m34 = m43;
			result.m41 = m14;
			result.m42 = m24;
			result.m43 = m34;
			result.m44 = m44;
		}

		public virtual F32 GetAt(int index)
		{
			switch (index)
			{
			case 0:
				return m11;
			case 1:
				return m12;
			case 2:
				return m13;
			case 3:
				return m14;
			case 4:
				return m21;
			case 5:
				return m22;
			case 6:
				return m23;
			case 7:
				return m24;
			case 8:
				return m31;
			case 9:
				return m32;
			case 10:
				return m33;
			case 11:
				return m34;
			case 12:
				return m41;
			case 13:
				return m42;
			case 14:
				return m43;
			case 15:
				return m44;
			default:
				return m44;
			}
		}

		public virtual F32 GetAt(int row, int column)
		{
			return GetAt(row * 4 + column);
		}

		public bool Equals(FMat44T_F32 other)
		{
			if (m14.Equals(other.m14) && m24.Equals(other.m24) && m34.Equals(other.m34) && m11.Equals(other.m11) && m12.Equals(other.m12) && m13.Equals(other.m13) && m21.Equals(other.m21) && m22.Equals(other.m22) && m23.Equals(other.m23) && m31.Equals(other.m31) && m32.Equals(other.m32) && m33.Equals(other.m33))
			{
				return m44.Equals(other.m44);
			}
			return false;
		}

		public virtual FMat44T_F32 Assign(FMat44T_F32 other)
		{
			m11 = other.m11;
			m21 = other.m21;
			m31 = other.m31;
			m41 = other.m41;
			m12 = other.m12;
			m22 = other.m22;
			m32 = other.m32;
			m42 = other.m42;
			m13 = other.m13;
			m23 = other.m23;
			m33 = other.m33;
			m43 = other.m43;
			m14 = other.m14;
			m24 = other.m24;
			m34 = other.m34;
			m44 = other.m44;
			return this;
		}

		public static FMat44T_F32 I(int point)
		{
			return Get(point);
		}

		public static FMat44T_F32 Perspective(F32 fovy, F32 aspect, F32 zNear, F32 zFar, int point)
		{
			FMat44T_F32 fMat44T_F = Get(point);
			F32 f = new F32(F32.Zero(point));
			F32 f2 = new F32(fovy.DivPower2(1).DegreeToRadian(point));
			F32 f3 = new F32(zFar.Sub(zNear));
			F32 f4 = new F32(f2.Sin(point));
			F32 other = new F32(F32.Zero(point));
			if (f3.Equals(other) || f4.Equals(other) || aspect.Equals(other))
			{
				return fMat44T_F;
			}
			f = f2.Cos(point).Div(f4, point);
			fMat44T_F.m11 = f.Div(aspect, point);
			fMat44T_F.m22 = f;
			fMat44T_F.m33 = zFar.Add(zNear).Neg().Div(f3, point);
			fMat44T_F.m43 = F32.One(point).Neg();
			fMat44T_F.m34 = zNear.Mul(zFar, point).MulPower2(1).Neg()
				.Div(f3, point);
			fMat44T_F.m44 = F32.Zero(point);
			return fMat44T_F;
		}

		public static FMat44T_F32 Frustum(F32 left, F32 right, F32 bottom, F32 top, F32 zNear, F32 zFar, int point)
		{
			FMat44T_F32 fMat44T_F = Get(point);
			F32 f = new F32(zNear.MulPower2(1));
			F32 f2 = new F32(right.Sub(left));
			F32 f3 = new F32(top.Sub(bottom));
			F32 f4 = new F32(zFar.Sub(zNear));
			fMat44T_F.m11 = f.Div(f2, point);
			fMat44T_F.m22 = f.Div(f3, point);
			fMat44T_F.m33 = zFar.Add(zNear).Div(f4, point).Neg();
			fMat44T_F.m34 = zFar.Mul(f, point).Div(f4, point).Neg();
			fMat44T_F.m13 = right.Add(left).Div(f2, point);
			fMat44T_F.m23 = top.Add(bottom).Div(f3, point);
			fMat44T_F.m43 = F32.One(point).Neg();
			fMat44T_F.m44 = F32.Zero(point);
			return fMat44T_F;
		}

		public static FMat44T_F32 Get()
		{
			if (mPoolIndex >= 0)
			{
				return mPool[mPoolIndex--];
			}
			return new FMat44T_F32();
		}

		public static FMat44T_F32 Get(FMat44T_F32 copy)
		{
			if (mPoolIndex >= 0)
			{
				FMat44T_F32 fMat44T_F = mPool[mPoolIndex--];
				fMat44T_F.Assign(copy);
				return fMat44T_F;
			}
			return new FMat44T_F32(copy);
		}

		public static FMat44T_F32 Get(int point)
		{
			if (mPoolIndex >= 0)
			{
				FMat44T_F32 fMat44T_F = mPool[mPoolIndex--];
				fMat44T_F.SetIdentity(point);
				return fMat44T_F;
			}
			return new FMat44T_F32(point);
		}

		public static FMat44T_F32[] InstArrayFMat44T_F32(int size)
		{
			FMat44T_F32[] array = new FMat44T_F32[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FMat44T_F32();
			}
			return array;
		}

		public static FMat44T_F32[][] InstArrayFMat44T_F32(int size1, int size2)
		{
			FMat44T_F32[][] array = new FMat44T_F32[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FMat44T_F32[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FMat44T_F32();
				}
			}
			return array;
		}

		public static FMat44T_F32[][][] InstArrayFMat44T_F32(int size1, int size2, int size3)
		{
			FMat44T_F32[][][] array = new FMat44T_F32[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FMat44T_F32[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FMat44T_F32[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FMat44T_F32();
					}
				}
			}
			return array;
		}
	}
}
