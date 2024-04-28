namespace ca.jamdat.flight
{
	public class FlFont
	{
		public const sbyte typeNumber = 36;

		public const sbyte typeID = 36;

		public const bool supportsDynamicSerialization = false;

		public FlFontBlob mFontBlob;

		public FlBitmapMap mBitmapMap;

		public static FlFont Cast(object o, FlFont _)
		{
			return (FlFont)o;
		}

		public virtual void destruct()
		{
			mFontBlob = null;
			mBitmapMap = null;
		}

		public virtual int GetLineWidth(FlString @string, int start, int length, bool removeLastAdvance, bool isBeginOfString)
		{
			if (length == 0)
			{
				return 0;
			}
			return mFontBlob.GetLineWidth(mBitmapMap, @string, start, length, removeLastAdvance, isBeginOfString);
		}

		public virtual int GetLineWidth(FlString @string, int nChars)
		{
			return GetLineWidth(@string, 0, nChars);
		}

		public virtual int GetLineWidth(FlString @string)
		{
			return GetLineWidth(@string, 0, @string.GetLength());
		}

		public virtual int GetLineHeight()
		{
			return mFontBlob.GetLineHeight();
		}

		public virtual sbyte GetAscent()
		{
			return mFontBlob.GetAscent();
		}

		public virtual int GetGlowOffset()
		{
			return mFontBlob.GetGlowOffset();
		}

		public virtual Vector2_short GetLineSize(FlString @string, int startIndex, int nChars)
		{
			return new Vector2_short((short)GetLineWidth(@string, startIndex, nChars), (short)GetLineHeight());
		}

		public virtual int GetCharWidth(sbyte letter, bool first, bool last, bool removeLastAdvance)
		{
			return mFontBlob.GetCharWidth(mBitmapMap, letter, first, last, removeLastAdvance);
		}

		public virtual int GetCharHeight(sbyte letter, bool last)
		{
			return mFontBlob.GetCharHeight(mBitmapMap, letter, last);
		}

		public virtual int GetCharRecede(sbyte letter, bool isLast)
		{
			return mFontBlob.GetCharRecede(mBitmapMap, letter, isLast);
		}

		public virtual sbyte GetLeading()
		{
			return mFontBlob.GetLeading();
		}

		public virtual void SetLeading(sbyte newLeading)
		{
			mFontBlob.SetLeading(newLeading);
		}

		public virtual void SetReferenceBitmap(FlBitmap bitmap)
		{
			mBitmapMap.SetReferenceBitmap(bitmap);
		}

		public virtual FlBitmap GetReferenceBitmap()
		{
			return mBitmapMap.GetReferenceBitmap();
		}

		public virtual bool ValidateString(FlString toValidate)
		{
			return mFontBlob.ValidateString(toValidate);
		}

		public virtual void OnSerialize(Package p)
		{
			mFontBlob = FlFontBlob.Cast(p.SerializePointer(112, true, false), null);
			mBitmapMap = FlBitmapMap.Cast(p.SerializePointer(37, true, false), null);
			if (mFontBlob is FlBitmapFontBlob)
			{
				mBitmapMap.SetBitmapMapBlob(((FlBitmapFontBlob)mFontBlob).mBitmapMapBlob);
				short[] mDataMatrix = mBitmapMap.mBlob.mDataMatrix;
				int bitmapCount = mBitmapMap.GetBitmapCount();
				((FlBitmapFontBlob)mFontBlob).CreateCharCodeMap(mDataMatrix, bitmapCount);
			}
		}

		public virtual void DrawString(DisplayContext displayContext, FlString @string, short pointX, short pointY, short width, int start, int length)
		{
			mFontBlob.DrawString(mBitmapMap, displayContext, @string, pointX, pointY, start, length);
		}

		public virtual int GetLineWidth(FlString @string, int start, int nChars)
		{
			return GetLineWidth(@string, start, nChars, true);
		}

		public virtual int GetLineWidth(FlString @string, int start, int nChars, bool removeLastAdvance)
		{
			return GetLineWidth(@string, start, nChars, removeLastAdvance, true);
		}

		public virtual Vector2_short GetLineSize(FlString @string)
		{
			return GetLineSize(@string, 0);
		}

		public virtual Vector2_short GetLineSize(FlString @string, int startIndex)
		{
			return GetLineSize(@string, startIndex, -1);
		}

		public virtual int GetCharWidth(sbyte letter, bool first, bool last)
		{
			return GetCharWidth(letter, first, last, true);
		}

		public virtual int GetCharRecede(sbyte letter)
		{
			return GetCharRecede(letter, false);
		}

		public static FlFont[] InstArrayFlFont(int size)
		{
			FlFont[] array = new FlFont[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlFont();
			}
			return array;
		}

		public static FlFont[][] InstArrayFlFont(int size1, int size2)
		{
			FlFont[][] array = new FlFont[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlFont[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlFont();
				}
			}
			return array;
		}

		public static FlFont[][][] InstArrayFlFont(int size1, int size2, int size3)
		{
			FlFont[][][] array = new FlFont[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlFont[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlFont[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlFont();
					}
				}
			}
			return array;
		}
	}
}
