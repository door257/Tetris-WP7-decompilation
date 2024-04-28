using System;

namespace ca.jamdat.flight
{
	public abstract class Controller : TimeControlled
	{
		public new const sbyte typeNumber = 87;

		public new const sbyte typeID = 87;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte ValueTypeNone = 0;

		public const sbyte ValueTypeBool = 1;

		public const sbyte ValueTypeInt = 2;

		public const sbyte ValueTypeFloat = 3;

		public const sbyte ValueTypeCoord2 = 4;

		public const sbyte ValueTypeRect = 5;

		public const sbyte ValueTypeColor = 6;

		public const sbyte ValueTypeVec3 = 7;

		public const sbyte ValueTypeQuaternion = 8;

		public const sbyte absolute = 0;

		public const sbyte relative = 1;

		public const sbyte uninitialized = 0;

		public const sbyte initialized = 1;

		public TimeControlled mControllee;

		public int mControlledValueCode;

		public sbyte mAbsolute;

		public sbyte mCacheState;

		public sbyte mStartState;

		public int[] mValueBuffer = new int[12];

		public Controller()
		{
		}

		public static Controller Cast(object o, Controller _)
		{
			return (Controller)o;
		}

		public override sbyte GetTypeID()
		{
			return 87;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			mControllee = null;
		}

		public virtual Vector2_short GetCoord2Value()
		{
			return new Vector2_short(GetCoord2ValueX(), GetCoord2ValueY());
		}

		public virtual short GetCoord2ValueX()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return (short)(array[0] + array[4]);
			}
			return (short)array[0];
		}

		public virtual short GetCoord2ValueY()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return (short)(array[1] + array[5]);
			}
			return (short)array[1];
		}

		public virtual int GetLongValue()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return array[0] + array[4];
			}
			return array[0];
		}

		public virtual bool GetBoolValue()
		{
			return (GetLongValue() & 1) != 0;
		}

		public virtual FlRect GetRectValue()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return new FlRect((short)(array[0] + array[4]), (short)(array[1] + array[5]), (short)(array[2] + array[6]), (short)(array[3] + array[7]));
			}
			return new FlRect((short)array[0], (short)array[1], (short)array[2], (short)array[3]);
		}

		public virtual Color888 GetColorValue()
		{
			int[] array = mValueBuffer;
			Color888 color = new Color888();
			if (mAbsolute != 0)
			{
				color.SetRed(Memory.MakeUnsignedByte((sbyte)(array[0] + array[4])));
				color.SetGreen(Memory.MakeUnsignedByte((sbyte)(array[1] + array[5])));
				color.SetBlue(Memory.MakeUnsignedByte((sbyte)(array[2] + array[6])));
			}
			else
			{
				color.SetRed(Memory.MakeUnsignedByte((sbyte)array[0]));
				color.SetGreen(Memory.MakeUnsignedByte((sbyte)array[1]));
				color.SetBlue(Memory.MakeUnsignedByte((sbyte)array[2]));
			}
			return color;
		}

		public virtual FVec3T_F32 GetFVec3Value()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return new FVec3T_F32(new F32(array[0] + array[4], 16), new F32(array[1] + array[5], 16), new F32(array[2] + array[6], 16));
			}
			return new FVec3T_F32(new F32(array[0], 16), new F32(array[1], 16), new F32(array[2], 16));
		}

		public virtual FQuaternionT_F32 GetFQuaternionValue()
		{
			int[] array = mValueBuffer;
			F32 scalarPart = new F32(array[0], 28);
			FQuaternionT_F32 fQuaternionT_F = new FQuaternionT_F32(scalarPart, new FVec3T_F32(new F32(array[1], 28), new F32(array[2], 28), new F32(array[3], 28)));
			if (mAbsolute != 0)
			{
				scalarPart = new F32(array[4], 28);
				FQuaternionT_F32 fQuaternionT_F2 = new FQuaternionT_F32(scalarPart, new FVec3T_F32(new F32(array[5], 28), new F32(array[6], 28), new F32(array[7], 28)));
				fQuaternionT_F.Assign(fQuaternionT_F2.Mul(fQuaternionT_F));
			}
			return fQuaternionT_F;
		}

		public virtual F32 GetF32Value()
		{
			int[] array = mValueBuffer;
			if (mAbsolute != 0)
			{
				return new F32(array[0] + array[4], 16);
			}
			return new F32(array[0], 16);
		}

		public virtual void SetValue(short valX, short valY)
		{
			mValueBuffer[4] = valX;
			mValueBuffer[5] = valY;
		}

		public virtual void SetValue(short val_left, short val_top, short val_width, short val_height)
		{
			int[] array = mValueBuffer;
			array[4] = val_left;
			array[5] = val_top;
			array[6] = val_width;
			array[7] = val_height;
		}

		public virtual void SetValue(Color888 rgb)
		{
			int[] array = mValueBuffer;
			array[4] = rgb.GetRed();
			array[5] = rgb.GetGreen();
			array[6] = rgb.GetBlue();
		}

		public virtual void SetValue(bool val)
		{
			mValueBuffer[4] = (val ? 1 : 0);
		}

		public virtual void SetValue(int val)
		{
			mValueBuffer[4] = val;
		}

		public virtual void SetValue(FVec3T_F32 val)
		{
			int[] array = mValueBuffer;
			array[4] = val.x.ToFixedPoint(16);
			array[5] = val.y.ToFixedPoint(16);
			array[6] = val.z.ToFixedPoint(16);
		}

		public virtual void SetValue(FQuaternionT_F32 val)
		{
			int[] array = mValueBuffer;
			array[4] = val.GetS().ToFixedPoint(28);
			array[5] = val.GetVx().ToFixedPoint(28);
			array[6] = val.GetVy().ToFixedPoint(28);
			array[7] = val.GetVz().ToFixedPoint(28);
		}

		public virtual void SetValue(F32 val)
		{
			mValueBuffer[4] = val.ToFixedPoint(16);
		}

		public virtual void GetControlledValue(int[] value, int size)
		{
			for (int i = 0; i < size; i++)
			{
				value[i] = mValueBuffer[i];
				if (mAbsolute != 0)
				{
					value[i] += mValueBuffer[4 + i];
				}
			}
		}

		public virtual void SetRequestedValue(int[] value, int size)
		{
			for (int i = 0; i < size; i++)
			{
				mValueBuffer[4 + i] = value[i];
			}
		}

		public virtual void Restore()
		{
			if (mStartState != 0)
			{
				CopyValue(0, 4);
				sbyte b = mAbsolute;
				mAbsolute = 0;
				mControllee.ControlValue(mControlledValueCode, true, this);
				mAbsolute = b;
			}
			Refresh();
		}

		public virtual void Refresh()
		{
			mCacheState = 0;
		}

		public virtual void SetControlParameters(TimeControlled controllee, int valueCode)
		{
			mControllee = controllee;
			mControlledValueCode = valueCode;
			mStartState = 0;
			mCacheState = 0;
		}

		public virtual void SetControllee(TimeControlled controllee)
		{
			SetControlParameters(controllee, mControlledValueCode);
		}

		public virtual void SetControlledValueCode(int valueCode)
		{
			SetControlParameters(mControllee, valueCode);
		}

		public virtual TimeControlled GetControllee()
		{
			return mControllee;
		}

		public virtual bool IsAbsolute()
		{
			return mAbsolute == 0;
		}

		public virtual void SetIsAbsolute(bool val)
		{
			if (val != (mAbsolute == 0))
			{
				mAbsolute = (sbyte)((!val) ? 1 : 0);
				mStartState = 0;
			}
		}

		public virtual void ResetControllee()
		{
			mStartState = 0;
		}

		public override void OnSerialize(Package p)
		{
			mControllee = TimeControlled.Cast(p.SerializePointer(0, true, false), null);
			mControlledValueCode = p.SerializeIntrinsic(mControlledValueCode);
			bool t = IsAbsolute();
			t = p.SerializeIntrinsic(t);
			SetIsAbsolute(t);
		}

		public virtual void DefaultOnTime(int totalTime, int deltaTime)
		{
			if (mControllee != null)
			{
				if (mStartState == 0)
				{
					GetCurrentValue();
					mStartState = 1;
				}
				if (NeedsUpdate())
				{
					mControllee.ControlValue(mControlledValueCode, true, this);
					UpdateCache();
				}
			}
		}

		public virtual void GetCurrentValue()
		{
			mControllee.ControlValue(mControlledValueCode, false, this);
		}

		public virtual bool NeedsUpdate()
		{
			if (mCacheState == 0)
			{
				return true;
			}
			for (int i = 0; i < 4; i++)
			{
				if (mValueBuffer[8 + i] != mValueBuffer[i])
				{
					return true;
				}
			}
			return false;
		}

		public virtual void UpdateCache()
		{
			CopyValue(8, 0);
			mCacheState = 1;
		}

		public virtual void CopyValue(int firstIndex, int secondIndex)
		{
			for (int i = 0; i < 4; i++)
			{
				mValueBuffer[firstIndex + i] = mValueBuffer[secondIndex + i];
			}
		}
	}
}
