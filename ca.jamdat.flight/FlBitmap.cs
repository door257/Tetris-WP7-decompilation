using System;
using Microsoft.Xna.Framework.Graphics;

namespace ca.jamdat.flight
{
	public abstract class FlBitmap
	{
		public const sbyte typeNumber = 21;

		public const sbyte typeID = 21;

		public const bool supportsDynamicSerialization = true;

		public const int FlBitmapFilterNone = 0;

		public const int FlBitmapFilterSmooth = 1;

		public const int IS_REPAL_FLIPPED_MASK = 8388608;

		public const int IS_FLIPPED_XY_MASK = 4194304;

		public const int IS_FLIPPED_Y_MASK = 2097152;

		public const int IS_FLIPPED_X_MASK = 1048576;

		public const int IS_ADDITIVE_MASK = 524288;

		public const int IS_REPALETTIZED_MASK = 262144;

		public const int IS_COLOR_KEYED_MASK = 131072;

		public const int PIXEL_FORMAT_MASK = 65535;

		public sbyte[] mData;

		public Vector2_short mPower2Size;

		public short mPaddedWidth;

		public short mPaddedHeight;

		public int[] mInflatedData;

		public short mDataWidth;

		public short mDataHeight;

		public int mBitmapFilter;

		public FlBitmap()
		{
			mBitmapFilter = 0;
			mPower2Size = new Vector2_short();
		}

		public static FlBitmap Cast(object o, FlBitmap _)
		{
			return (FlBitmap)o;
		}

		public virtual sbyte GetTypeID()
		{
			return 21;
		}

		public static Type AsClass()
		{
			return null;
		}

		public virtual void destruct()
		{
		}

		public virtual short GetWidth()
		{
			return mDataWidth;
		}

		public virtual short GetHeight()
		{
			return mDataHeight;
		}

		public virtual short GetDataWidth()
		{
			return mDataWidth;
		}

		public virtual short GetDataHeight()
		{
			return mDataHeight;
		}

		public virtual short GetPaddedWidth()
		{
			return mPaddedWidth;
		}

		public virtual short GetPaddedHeight()
		{
			return mPaddedHeight;
		}

		public abstract void SetSize(short a13, short a12);

		public abstract void Clone(FlBitmap a14);

		public abstract void Duplicate(FlBitmap a15);

		public abstract void GetRGB(int[] a23, int a22, int a21, int a20, int a19, int a18, int a17, int a16);

		public abstract Palette GetPalette();

		public abstract void SetPalette(Palette a24);

		public virtual void OnSerialize(Package _package)
		{
		}

		public abstract int GetPixelFormat();

		public virtual int GetBytesPerLine()
		{
			return mDataWidth * 4;
		}

		public abstract Texture2D getImage();
	}
}
