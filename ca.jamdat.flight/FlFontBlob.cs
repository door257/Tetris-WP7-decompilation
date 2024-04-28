using System;

namespace ca.jamdat.flight
{
	public abstract class FlFontBlob
	{
		public const sbyte typeNumber = 112;

		public const sbyte typeID = 112;

		public const bool supportsDynamicSerialization = true;

		public static FlFontBlob Cast(object o, FlFontBlob _)
		{
			return (FlFontBlob)o;
		}

		public virtual sbyte GetTypeID()
		{
			return 112;
		}

		public static Type AsClass()
		{
			return null;
		}

		public virtual void destruct()
		{
		}

		public virtual FlFontBlob OnSerialize(Package a6)
		{
			return null;
		}

		public abstract sbyte GetLineHeight();

		public abstract void DrawString(FlBitmapMap a24, DisplayContext a23, FlString a22, short a21, short a20, int a19, int a18);

		public abstract int GetLineWidth(FlBitmapMap a30, FlString a29, int a28, int a27, bool a26, bool a25);

		public abstract int GetCharWidth(FlBitmapMap a35, sbyte a34, bool a33, bool a32, bool a31);

		public abstract int GetCharHeight(FlBitmapMap a38, sbyte a37, bool a36);

		public virtual int GetCharRecede(FlBitmapMap a9, sbyte a8, bool a7)
		{
			return 0;
		}

		public abstract void SetLeading(sbyte a39);

		public abstract sbyte GetLeading();

		public virtual sbyte GetAscent()
		{
			return 0;
		}

		public virtual int GetGlowOffset()
		{
			return 0;
		}

		public virtual bool ValidateString(FlString a11)
		{
			return true;
		}

		public virtual int GetLineWidth(FlBitmapMap bitmapMap, FlString @string, int start, int iMaxCharCount)
		{
			return GetLineWidth(bitmapMap, @string, start, iMaxCharCount, true);
		}

		public virtual int GetLineWidth(FlBitmapMap bitmapMap, FlString @string, int start, int iMaxCharCount, bool removeLastAdvance)
		{
			return GetLineWidth(bitmapMap, @string, start, iMaxCharCount, removeLastAdvance, true);
		}

		public virtual int GetCharWidth(FlBitmapMap bitmapMap, sbyte letter, bool isFirst, bool isLast)
		{
			return GetCharWidth(bitmapMap, letter, isFirst, isLast, true);
		}
	}
}
