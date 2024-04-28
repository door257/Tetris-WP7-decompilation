namespace ca.jamdat.flight
{
	public class Vector2_short
	{
		public const sbyte typeNumber = 39;

		public const sbyte typeID = 39;

		public const bool supportsDynamicSerialization = false;

		public short[] mData = new short[2];

		public Vector2_short()
		{
		}

		public Vector2_short(short inx, short iny)
		{
			mData[0] = inx;
			mData[1] = iny;
		}

		public Vector2_short(Vector2_short other)
		{
			mData[0] = other.mData[0];
			mData[1] = other.mData[1];
		}

		public static Vector2_short Cast(object o, Vector2_short _)
		{
			return (Vector2_short)o;
		}

		public virtual Vector2_short Assign(Vector2_short rhs)
		{
			mData[0] = rhs.mData[0];
			mData[1] = rhs.mData[1];
			return this;
		}

		public virtual Vector2_short MulAssign(short rhs)
		{
			mData[0] *= rhs;
			mData[1] *= rhs;
			return this;
		}

		public virtual Vector2_short AddAssign(Vector2_short rhs)
		{
			mData[0] = (short)(mData[0] + rhs.mData[0]);
			mData[1] = (short)(mData[1] + rhs.mData[1]);
			return this;
		}

		public virtual Vector2_short SubAssign(Vector2_short rhs)
		{
			mData[0] = (short)(mData[0] - rhs.mData[0]);
			mData[1] = (short)(mData[1] - rhs.mData[1]);
			return this;
		}

		public virtual Vector2_short Neg()
		{
			return new Vector2_short((short)(-mData[0]), (short)(-mData[1]));
		}

		public virtual short GetX()
		{
			return mData[0];
		}

		public virtual short GetY()
		{
			return mData[1];
		}

		public virtual void SetX(short inx)
		{
			mData[0] = inx;
		}

		public virtual void SetY(short iny)
		{
			mData[1] = iny;
		}

		public virtual void OffsetX(short inx)
		{
			mData[0] = (short)(mData[0] + inx);
		}

		public virtual void OffsetY(short iny)
		{
			mData[1] = (short)(mData[1] + iny);
		}

		public virtual void OnSerialize(Package p)
		{
			short t = mData[0];
			t = p.SerializeIntrinsic(t);
			mData[0] = t;
			t = mData[1];
			t = p.SerializeIntrinsic(t);
			mData[1] = t;
		}

		public bool Equals(Vector2_short rhs)
		{
			if (mData[0] == rhs.mData[0])
			{
				return mData[1] == rhs.mData[1];
			}
			return false;
		}

		public virtual Vector2_short Add(short rhs)
		{
			return new Vector2_short((short)(mData[0] + rhs), (short)(mData[1] + rhs));
		}

		public virtual Vector2_short Sub(short rhs)
		{
			return new Vector2_short((short)(mData[0] - rhs), (short)(mData[1] - rhs));
		}

		public virtual Vector2_short Add(Vector2_short rhs)
		{
			return new Vector2_short((short)(mData[0] + rhs.mData[0]), (short)(mData[1] + rhs.mData[1]));
		}

		public virtual Vector2_short Sub(Vector2_short rhs)
		{
			return new Vector2_short((short)(mData[0] - rhs.mData[0]), (short)(mData[1] - rhs.mData[1]));
		}

		public virtual Vector2_short Mul(short rhs)
		{
			return new Vector2_short((short)(mData[0] * rhs), (short)(mData[1] * rhs));
		}

		public virtual Vector2_short Div(short rhs)
		{
			return new Vector2_short((short)(mData[0] / rhs), (short)(mData[1] / rhs));
		}

		public virtual short CrossProduct(Vector2_short rhs)
		{
			return (short)(mData[0] * rhs.mData[1] - mData[1] * rhs.mData[0]);
		}

		public virtual short DotProduct(Vector2_short rhs)
		{
			return (short)(mData[0] * rhs.mData[0] + mData[1] * rhs.mData[1]);
		}

		public static Vector2_short[] InstArrayVector2_short(int size)
		{
			Vector2_short[] array = new Vector2_short[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Vector2_short();
			}
			return array;
		}

		public static Vector2_short[][] InstArrayVector2_short(int size1, int size2)
		{
			Vector2_short[][] array = new Vector2_short[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Vector2_short[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Vector2_short();
				}
			}
			return array;
		}

		public static Vector2_short[][][] InstArrayVector2_short(int size1, int size2, int size3)
		{
			Vector2_short[][][] array = new Vector2_short[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Vector2_short[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Vector2_short[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Vector2_short();
					}
				}
			}
			return array;
		}
	}
}
