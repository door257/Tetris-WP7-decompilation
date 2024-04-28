namespace ca.jamdat.flight
{
	public class FlRect
	{
		public const sbyte typeNumber = 40;

		public const sbyte typeID = 40;

		public const bool supportsDynamicSerialization = false;

		public short[] mData = new short[4];

		public FlRect()
		{
		}

		public FlRect(FlRect other)
		{
			mData[0] = other.GetLeft();
			mData[1] = other.GetTop();
			mData[2] = other.GetWidth();
			mData[3] = other.GetHeight();
		}

		public FlRect(short left, short top, short width, short height)
		{
			mData[0] = left;
			mData[1] = top;
			mData[2] = width;
			mData[3] = height;
		}

		public FlRect(Vector2_short topLeft, Vector2_short size)
		{
			mData[0] = topLeft.GetX();
			mData[1] = topLeft.GetY();
			mData[2] = size.GetX();
			mData[3] = size.GetY();
		}

		public static FlRect Cast(object o, FlRect _)
		{
			return (FlRect)o;
		}

		public virtual Vector2_short GetCenter()
		{
			return new Vector2_short((short)(GetLeft() + GetWidth() / 2), (short)(GetTop() + GetHeight() / 2));
		}

		public virtual short GetWidth()
		{
			return mData[2];
		}

		public virtual short GetHeight()
		{
			return mData[3];
		}

		public virtual short GetTop()
		{
			return mData[1];
		}

		public virtual short GetLeft()
		{
			return mData[0];
		}

		public virtual short GetRight()
		{
			return (short)(GetLeft() + GetWidth() - 1);
		}

		public virtual void SetCenter(Vector2_short center)
		{
			SetLeft((short)(center.GetX() - GetWidth() / 2));
			SetTop((short)(center.GetY() - GetHeight() / 2));
		}

		public virtual void SetWidth(int inWidth)
		{
			mData[2] = (short)inWidth;
		}

		public virtual void SetHeight(int inHeight)
		{
			mData[3] = (short)inHeight;
		}

		public virtual void SetTop(int inTop)
		{
			mData[1] = (short)inTop;
		}

		public virtual void SetLeft(int inLeft)
		{
			mData[0] = (short)inLeft;
		}

		public virtual void SetRight(int right)
		{
			SetWidth((short)(right - GetLeft() + 1));
		}

		public virtual short GetBottom()
		{
			return (short)(GetTop() + GetHeight() - 1);
		}

		public virtual void SetBottom(int bottom)
		{
			SetHeight((short)(bottom + 1 - GetTop()));
		}

		public virtual Vector2_short GetTopLeft()
		{
			return new Vector2_short(GetLeft(), GetTop());
		}

		public virtual void SetTopLeft(Vector2_short topLeft)
		{
			SetLeft(topLeft.GetX());
			SetTop(topLeft.GetY());
		}

		public virtual Vector2_short GetTopRight()
		{
			return new Vector2_short(GetRight(), GetTop());
		}

		public virtual Vector2_short GetBottomRight()
		{
			return new Vector2_short(GetRight(), GetBottom());
		}

		public virtual void SetBottomRight(Vector2_short bottomRight)
		{
			SetWidth((short)(bottomRight.GetX() + 1 - GetLeft()));
			SetHeight((short)(bottomRight.GetY() + 1 - GetTop()));
		}

		public virtual Vector2_short GetBottomLeft()
		{
			return new Vector2_short(GetLeft(), GetBottom());
		}

		public virtual Vector2_short GetSize()
		{
			return new Vector2_short(mData[2], mData[3]);
		}

		public virtual void MoveBy(Vector2_short delta)
		{
			SetLeft((short)(GetLeft() + delta.GetX()));
			SetTop((short)(GetTop() + delta.GetY()));
		}

		public virtual void MoveBy(int deltaX, int deltaY)
		{
			SetLeft((short)(GetLeft() + deltaX));
			SetTop((short)(GetTop() + deltaY));
		}

		public static bool Contains(short rect_left, short rect_top, short rect_width, short rect_height, short ptX, short ptY)
		{
			if (rect_left <= ptX && ptX <= rect_left + rect_width - 1 && rect_top <= ptY)
			{
				return ptY <= rect_top + rect_height - 1;
			}
			return false;
		}

		public virtual bool ClipLine(Vector2_short p0, Vector2_short p1)
		{
			if (IsEmpty())
			{
				return false;
			}
			F32 f = new F32(p0.GetX() << 16, 16);
			F32 f2 = new F32(p0.GetY() << 16, 16);
			F32 f3 = new F32(p1.GetX() << 16, 16);
			F32 f4 = new F32(p1.GetY() << 16, 16);
			F32 f5 = new F32(GetLeft() << 16, 16);
			F32 f6 = new F32(GetTop() << 16, 16);
			F32 f7 = new F32(GetLeft() + GetWidth() - 1 << 16, 16);
			F32 f8 = new F32(GetTop() + GetHeight() - 1 << 16, 16);
			int num = 0;
			int num2 = 0;
			int num3 = calcode(f, f2, f5, f6, f7, f8);
			int num4 = calcode(f3, f4, f5, f6, f7, f8);
			do
			{
				if ((num3 | num4) == 0)
				{
					num = 1;
					num2 = 1;
					continue;
				}
				if ((num3 & num4) != 0)
				{
					num2 = 1;
					continue;
				}
				int num5 = ((num3 != 0) ? num3 : num4);
				F32 f9;
				F32 f10;
				if (((uint)num5 & (true ? 1u : 0u)) != 0)
				{
					f9 = f.Add(f3.Sub(f).Mul(f8.Sub(f2).Div(f4.Sub(f2), 16), 16));
					f10 = f8;
				}
				else if (((uint)num5 & 2u) != 0)
				{
					f9 = f.Add(f3.Sub(f).Mul(f6.Sub(f2).Div(f4.Sub(f2), 16), 16));
					f10 = f6;
				}
				else if (((uint)num5 & 4u) != 0)
				{
					f10 = f2.Add(f4.Sub(f2).Mul(f7.Sub(f).Div(f3.Sub(f), 16), 16));
					f9 = f7;
				}
				else
				{
					f10 = f2.Add(f4.Sub(f2).Mul(f5.Sub(f).Div(f3.Sub(f), 16), 16));
					f9 = f5;
				}
				if (num5 == num3)
				{
					f = f9;
					f2 = f10;
					num3 = calcode(f, f2, f5, f6, f7, f8);
				}
				else
				{
					f3 = f9;
					f4 = f10;
					num4 = calcode(f3, f4, f5, f6, f7, f8);
				}
			}
			while (num2 == 0);
			bool result = ((num != 0) ? true : false);
			p0.SetX((short)f.ToInt(16));
			p0.SetY((short)f2.ToInt(16));
			p1.SetX((short)f3.ToInt(16));
			p1.SetY((short)f4.ToInt(16));
			return result;
		}

		public static bool Intersects(short rect1_left, short rect1_top, short rect1_width, short rect1_height, short rect2_left, short rect2_top, short rect2_width, short rect2_height)
		{
			if (rect2_left + rect2_width > rect1_left && rect2_top + rect2_height > rect1_top && rect1_left + rect1_width > rect2_left)
			{
				return rect1_top + rect1_height > rect2_top;
			}
			return false;
		}

		public static bool Contains(short rect_left, short rect_top, short rect_width, short rect_height, short insideRect_left, short insideRect_top, short insideRect_width, short insideRect_height)
		{
			if (insideRect_left >= rect_left && insideRect_top >= rect_top && (short)(insideRect_left + insideRect_width - 1) <= (short)(rect_left + rect_width - 1))
			{
				return (short)(insideRect_top + insideRect_height - 1) <= (short)(rect_top + rect_height - 1);
			}
			return false;
		}

		public virtual void OffsetTop(short pixel)
		{
			SetTop((short)(GetTop() + pixel));
		}

		public virtual void OffsetLeft(short pixel)
		{
			SetLeft((short)(GetLeft() + pixel));
		}

		public bool Equals(FlRect rhs)
		{
			if (mData[0] == rhs.mData[0] && mData[1] == rhs.mData[1] && mData[2] == rhs.mData[2])
			{
				return mData[3] == rhs.mData[3];
			}
			return false;
		}

		public virtual FlRect Add(Vector2_short rhs)
		{
			return new FlRect((short)(GetLeft() + rhs.GetX()), (short)(GetTop() + rhs.GetY()), GetWidth(), GetHeight());
		}

		public virtual bool IsEmpty()
		{
			if (GetWidth() > 0)
			{
				return GetHeight() <= 0;
			}
			return true;
		}

		public virtual FlRect GetVideoModeRect()
		{
			return DisplayManager.GetVideoModeRect();
		}

		public virtual void OnSerialize(Package p)
		{
			short left = GetLeft();
			left = p.SerializeIntrinsic(left);
			SetLeft(left);
			left = GetTop();
			left = p.SerializeIntrinsic(left);
			SetTop(left);
			left = GetWidth();
			left = p.SerializeIntrinsic(left);
			SetWidth(left);
			left = GetHeight();
			left = p.SerializeIntrinsic(left);
			SetHeight(left);
		}

		public virtual FlRect Assign(FlRect other)
		{
			mData[0] = other.GetLeft();
			mData[1] = other.GetTop();
			mData[2] = other.GetWidth();
			mData[3] = other.GetHeight();
			return this;
		}

		public static int calcode(F32 x, F32 y, F32 xwmin, F32 ywmin, F32 xwmax, F32 ywmax)
		{
			int num = 0;
			if (y.GreaterThan(ywmax))
			{
				num |= 1;
			}
			if (y.LessThan(ywmin))
			{
				num |= 2;
			}
			if (x.GreaterThan(xwmax))
			{
				num |= 4;
			}
			if (x.LessThan(xwmin))
			{
				num |= 8;
			}
			return num;
		}

		public static FlRect[] InstArrayFlRect(int size)
		{
			FlRect[] array = new FlRect[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlRect();
			}
			return array;
		}

		public static FlRect[][] InstArrayFlRect(int size1, int size2)
		{
			FlRect[][] array = new FlRect[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlRect[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlRect();
				}
			}
			return array;
		}

		public static FlRect[][][] InstArrayFlRect(int size1, int size2, int size3)
		{
			FlRect[][][] array = new FlRect[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlRect[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlRect[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlRect();
					}
				}
			}
			return array;
		}
	}
}
