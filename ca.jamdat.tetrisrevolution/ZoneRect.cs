using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ZoneRect : Zone
	{
		public const sbyte kInvalid = 0;

		public const sbyte kTappable = 1;

		public const sbyte kDraggable = 2;

		public const sbyte kSwipeable = 4;

		public const sbyte kDefault = 7;

		public sbyte mType;

		public short mLeft;

		public short mTop;

		public short mWidth;

		public short mHeight;

		public ZoneRect()
		{
			mType = 0;
		}

		public override void destruct()
		{
		}

		public virtual void SetType(sbyte zoneType)
		{
			mType = zoneType;
		}

		public virtual void AddType(sbyte zoneType)
		{
			mType |= zoneType;
		}

		public virtual void RemoveType(sbyte zoneType)
		{
			mType = (sbyte)(mType & ~zoneType);
		}

		public virtual bool IsZoneType(sbyte zoneType)
		{
			return (mType & zoneType) == zoneType;
		}

		public virtual bool IsDefaultType()
		{
			if (IsTappableType() && IsDraggableType())
			{
				return IsSwipeableType();
			}
			return false;
		}

		public virtual bool IsTappableType()
		{
			return IsZoneType(1);
		}

		public virtual bool IsDraggableType()
		{
			return IsZoneType(2);
		}

		public virtual bool IsSwipeableType()
		{
			return IsZoneType(4);
		}

		public override bool IsInside(short x, short y)
		{
			short rect_left = mLeft;
			short rect_top = mTop;
			short rect_width = mWidth;
			short rect_height = mHeight;
			return FlRect.Contains(rect_left, rect_top, rect_width, rect_height, x, y);
		}

		public override Component CreateVisualComponent()
		{
			Shape shape = new Shape();
			int parR = FlMath.Random(0, 255);
			int parG = FlMath.Random(0, 255);
			int parB = FlMath.Random(0, 255);
			shape.SetColor(new Color888(parR, parG, parB));
			shape.SetTopLeft(mLeft, mTop);
			shape.SetSize(mWidth, mHeight);
			return shape;
		}

		public override Vector2_short GetCenter()
		{
			return new Vector2_short((short)(mLeft + mWidth / 2), (short)(mTop + mWidth / 2));
		}

		public virtual Vector2_short GetSize()
		{
			return new Vector2_short(mWidth, mHeight);
		}

		public virtual void SetTopLeftX(short topLeftX)
		{
			mLeft = topLeftX;
			Refresh();
		}

		public virtual void SetTopLeftY(short topLeftY)
		{
			mTop = topLeftY;
			Refresh();
		}

		public virtual void SetWidth(short width)
		{
			mWidth = width;
			Refresh();
		}

		public virtual void SetHeight(short height)
		{
			mHeight = height;
			Refresh();
		}

		public static ZoneRect[] InstArrayZoneRect(int size)
		{
			ZoneRect[] array = new ZoneRect[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ZoneRect();
			}
			return array;
		}

		public static ZoneRect[][] InstArrayZoneRect(int size1, int size2)
		{
			ZoneRect[][] array = new ZoneRect[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ZoneRect[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ZoneRect();
				}
			}
			return array;
		}

		public static ZoneRect[][][] InstArrayZoneRect(int size1, int size2, int size3)
		{
			ZoneRect[][][] array = new ZoneRect[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ZoneRect[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ZoneRect[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ZoneRect();
					}
				}
			}
			return array;
		}
	}
}
