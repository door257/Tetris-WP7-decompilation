using System;

namespace ca.jamdat.flight
{
	public class FlBitmapFontBlob : FlFontBlob
	{
		public new const sbyte typeNumber = 51;

		public new const sbyte typeID = 51;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte BFBElementCharCode = 1;

		public const sbyte BFBElementSourceLeft = 6;

		public const sbyte BFBElementSourceTop = 7;

		public const sbyte BFBElementSourceWidth = 4;

		public const sbyte BFBElementSourceHeight = 5;

		public const sbyte BFBElementAdvance = 0;

		public const sbyte BFBElementRecede = 2;

		public const sbyte BFBElementOffsetY = 3;

		public const sbyte BFBElement_to_BMBElementRecedeForMapBlob = 4;

		public const sbyte BFBElement_to_BMBElementAdvanceForMapBlob = 6;

		public const sbyte BFBElement_to_BMBElementSizeYForMapBlob = 7;

		public const sbyte BFBElement_to_BMBElementWidthForMapBlob = 2;

		public const sbyte BFBElement_to_BMBElementHeightForMapBlob = 3;

		public const sbyte fieldLowestCharCode = 0;

		public const sbyte fieldCharCodeMapCount = 1;

		public const sbyte fieldCharCodeMap = 2;

		public const sbyte metricsVersion = 0;

		public const sbyte metricsLineHeight = 1;

		public const sbyte metricsLeading = 2;

		public const sbyte metricsAscent = 3;

		public const sbyte metricsDescent = 4;

		public const sbyte metricsBaseline = 5;

		public const sbyte metricsFixedPitch = 6;

		public const sbyte metricsHAdvanceXMax = 7;

		public const sbyte metricsVAdvanceYMax = 8;

		public const sbyte metricsXHeight = 9;

		public const sbyte metricsCapsHeight = 10;

		public const sbyte metricsUnderlineOffset = 11;

		public const sbyte metricsUnderlineThickness = 12;

		public const sbyte metricsStrikeOffset = 13;

		public const sbyte metricsStrikeThickness = 14;

		public const sbyte metricsMaxUpperGlowOffset = 15;

		public const sbyte metricsMaxLowerGlowOffset = 16;

		public const sbyte metricsCount = 17;

		public static int kHighestFontFileVersion = 6;

		public short[] mIndexFromCharCode;

		public FlBitmapMapBlob mBitmapMapBlob;

		public sbyte[] mFontMetrics;

		public short mKerningListLength;

		public FlKerningPair[] mKerningList;

		public static FlBitmapFontBlob Cast(object o, FlBitmapFontBlob _)
		{
			return (FlBitmapFontBlob)o;
		}

		public override sbyte GetTypeID()
		{
			return 51;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			mIndexFromCharCode = null;
			mFontMetrics = null;
			if (mKerningList != null)
			{
				for (int i = 0; i < mKerningListLength; i++)
				{
					mKerningList[i] = null;
				}
				mKerningList = null;
			}
		}

		public override FlFontBlob OnSerialize(Package p)
		{
			mBitmapMapBlob = FlBitmapMapBlob.Cast(p.SerializePointer(52, false, false), null);
			int t = 0;
			t = p.SerializeIntrinsic(t);
			mIndexFromCharCode = new short[t];
			int count = 17;
			mFontMetrics = p.SerializeIntrinsics(mFontMetrics, count);
			if (GetVersion() > 3)
			{
				mKerningListLength = p.SerializeIntrinsic(mKerningListLength);
				if (mKerningListLength > 0)
				{
					mKerningList = new FlKerningPair[mKerningListLength];
				}
				for (int i = 0; i < mKerningListLength; i++)
				{
					mKerningList[i] = FlKerningPair.Cast(p.SerializePointer(111, false, false), null);
				}
			}
			return this;
		}

		public override sbyte GetLineHeight()
		{
			return mFontMetrics[1];
		}

		public override sbyte GetLeading()
		{
			return mFontMetrics[2];
		}

		public override void SetLeading(sbyte newLeading)
		{
			int num = mFontMetrics[2] - newLeading;
			mFontMetrics[1] = (sbyte)(mFontMetrics[1] - num);
			if (GetVersion() > 3)
			{
				mFontMetrics[5] = (sbyte)(mFontMetrics[5] - num);
			}
		}

		public virtual void CreateCharCodeMap(short[] dataMatrix, int charCount)
		{
			if (mIndexFromCharCode[1] == 0)
			{
				int num = dataMatrix[7] & 0xFF;
				int num2 = dataMatrix[(charCount - 1 << 3) + 7] & 0xFF;
				short[] array = mIndexFromCharCode;
				for (int i = num; i <= num2; i++)
				{
					array[2 + i - num] = -1;
				}
				for (int i = 0; i < charCount; i++)
				{
					int num3 = dataMatrix[(i << 3) + 7] & 0xFF;
					array[2 + num3 - num] = (short)i;
				}
				array[0] = (short)num;
				array[1] = (short)(num2 - num + 1);
			}
		}

		public override bool ValidateString(FlString toValidate)
		{
			for (int i = 0; i < toValidate.GetLength(); i++)
			{
				if (GetIndexOfChar(toValidate.GetCharAt(i)) == 255)
				{
					return false;
				}
			}
			return true;
		}

		public override void DrawString(FlBitmapMap bitmapMap, DisplayContext displayContext, FlString @string, short pointX, short pointY, int start, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (length < 0)
			{
				length = @string.GetLength() - start;
			}
			int num = start;
			bool isFirstLetter = true;
			if (GetVersion() > 3)
			{
				sbyte identicalChar = GetIdenticalChar(@string.GetCharAt(num++), length == 1);
				sbyte b = 0;
				while (length > 1)
				{
					b = identicalChar;
					identicalChar = GetIdenticalChar(@string.GetCharAt(num++), length == 2);
					if (b != 0)
					{
						int indexOfChar = GetIndexOfChar(b);
						if (indexOfChar != 255)
						{
							pointX = DrawChar(bitmapMap, displayContext, indexOfChar, pointX, pointY, isFirstLetter);
							isFirstLetter = false;
							int kerningPairIndex = GetKerningPairIndex(b, identicalChar);
							if (kerningPairIndex != -1)
							{
								pointX = (short)(pointX + mKerningList[kerningPairIndex].GetAdvanceOffset());
							}
						}
					}
					length--;
				}
				if (identicalChar != 0)
				{
					int indexOfChar2 = GetIndexOfChar(identicalChar);
					if (indexOfChar2 != 255)
					{
						pointX = DrawChar(bitmapMap, displayContext, indexOfChar2, pointX, pointY, isFirstLetter);
						isFirstLetter = false;
					}
				}
				return;
			}
			do
			{
				sbyte identicalChar2 = GetIdenticalChar(@string.GetCharAt(num++), length == 1);
				if (identicalChar2 != 0)
				{
					int indexOfChar3 = GetIndexOfChar(identicalChar2);
					if (indexOfChar3 != 255)
					{
						pointX = DrawChar(bitmapMap, displayContext, indexOfChar3, pointX, pointY, isFirstLetter);
						isFirstLetter = false;
					}
				}
			}
			while (--length != 0);
		}

		public override int GetLineWidth(FlBitmapMap bitmapMap, FlString @string, int start, int length, bool removeLastAdvance, bool isBeginOfString)
		{
			int num = 0;
			int num2 = start;
			if (length < 0)
			{
				length = @string.GetLength() - num2;
			}
			if (length > 0)
			{
				bool a = isBeginOfString;
				sbyte b = 0;
				do
				{
					b = @string.GetCharAt(num2 + length - 1);
				}
				while ((b == -1 || b == 10 || GetIndexOfChar(b) == 255) && --length > 0);
				sbyte charAt = @string.GetCharAt(num2++);
				while (length-- > 1)
				{
					b = charAt;
					charAt = @string.GetCharAt(num2++);
					num += GetCharWidth(bitmapMap, b, a, false, removeLastAdvance);
					if (GetVersion() > 3)
					{
						int kerningPairIndex = GetKerningPairIndex(GetIdenticalChar(b, false), GetIdenticalChar(charAt, length == 1));
						if (kerningPairIndex != -1)
						{
							num += (short)mKerningList[kerningPairIndex].GetAdvanceOffset();
						}
					}
					a = false;
				}
				num += GetCharWidth(bitmapMap, charAt, a, true, removeLastAdvance);
			}
			return num;
		}

		public override int GetCharWidth(FlBitmapMap bitmapMap, sbyte letter, bool isFirst, bool isLast, bool removeLastAdvance)
		{
			short[] mDataMatrix = bitmapMap.mBlob.mDataMatrix;
			letter = GetIdenticalChar(letter, isLast);
			int indexOfChar = GetIndexOfChar(letter);
			int num;
			if (indexOfChar == 255)
			{
				num = 0;
			}
			else
			{
				int num2 = indexOfChar << 3;
				num = ((!isLast || !removeLastAdvance) ? mDataMatrix[num2 + 6] : mDataMatrix[num2 + 2]);
				if (!isFirst)
				{
					num += mDataMatrix[num2 + 4];
				}
			}
			return num;
		}

		public override int GetCharHeight(FlBitmapMap bitmapMap, sbyte letter, bool isLast)
		{
			short[] mDataMatrix = bitmapMap.mBlob.mDataMatrix;
			letter = GetIdenticalChar(letter, isLast);
			int indexOfChar = GetIndexOfChar(letter);
			if (indexOfChar == 255)
			{
				return 0;
			}
			int num = indexOfChar << 3;
			return mDataMatrix[num + 3];
		}

		public override int GetCharRecede(FlBitmapMap bitmapMap, sbyte letter, bool isLast)
		{
			short[] mDataMatrix = bitmapMap.mBlob.mDataMatrix;
			letter = GetIdenticalChar(letter, isLast);
			int indexOfChar = GetIndexOfChar(letter);
			int result = 0;
			if (indexOfChar != 255)
			{
				int num = indexOfChar << 3;
				result = mDataMatrix[num + 4];
			}
			return result;
		}

		public virtual short DrawChar(FlBitmapMap bitmapMap, DisplayContext displayContext, int charIndex, short pointX, short pointY, bool isFirstLetter)
		{
			short[] mDataMatrix = bitmapMap.mBlob.mDataMatrix;
			int num = charIndex << 3;
			short num2 = mDataMatrix[num + 6];
			short num3 = mDataMatrix[num + 4];
			if (isFirstLetter)
			{
				pointX = (short)(pointX - num3);
			}
			bitmapMap.DrawElementAt(charIndex, displayContext, pointX, pointY, num2, mDataMatrix[num + 7], false, false, false, false);
			pointX = (short)(pointX + num2 + num3);
			return pointX;
		}

		public virtual int GetIndexOfChar(sbyte c)
		{
			int num = (short)((c & 0xFF) - (mIndexFromCharCode[0] & 0xFF));
			if (num < 0 || num >= (mIndexFromCharCode[1] & 0xFF))
			{
				return 255;
			}
			return mIndexFromCharCode[2 + num] & 0xFF;
		}

		public virtual sbyte GetIdenticalChar(sbyte letter, bool isLast)
		{
			switch (letter & 0xFF)
			{
			case 10:
			case 13:
			case 29:
			case 31:
			case 255:
				letter = 0;
				break;
			case 30:
			case 160:
				letter = 32;
				break;
			case 173:
				letter = (sbyte)(isLast ? 45 : 0);
				break;
			}
			return letter;
		}

		public virtual int GetKerningPairIndex(short currentChar, short nextChar)
		{
			int num = nextChar & 0xFFFF;
			int num2 = (int)((currentChar << 16) & 0xFFFF0000u);
			int num3 = num | num2;
			int num4 = 0;
			int num5 = mKerningListLength - 1;
			while (num4 <= num5)
			{
				int num6 = num4 + num5 >> 1;
				int hashCode = mKerningList[num6].GetHashCode();
				if (num3 == hashCode)
				{
					return num6;
				}
				if (num3 < hashCode)
				{
					num5 = num6 - 1;
				}
				else
				{
					num4 = num6 + 1;
				}
			}
			return -1;
		}

		public virtual sbyte GetVersion()
		{
			return mFontMetrics[0];
		}

		public virtual sbyte GetFixedPitch()
		{
			return mFontMetrics[6];
		}

		public virtual sbyte GetHAdvanceXMax()
		{
			return mFontMetrics[7];
		}

		public virtual sbyte GetVAdvanceYMax()
		{
			return mFontMetrics[8];
		}

		public override sbyte GetAscent()
		{
			return mFontMetrics[3];
		}

		public virtual sbyte GetDescent()
		{
			return mFontMetrics[4];
		}

		public virtual sbyte GetBaseline()
		{
			return mFontMetrics[5];
		}

		public virtual sbyte GetXHeight()
		{
			return mFontMetrics[9];
		}

		public virtual sbyte GetCapsHeight()
		{
			return mFontMetrics[10];
		}

		public virtual bool HasUnderlineInfo()
		{
			return GetVersion() > 4;
		}

		public virtual int GetUnderlineOffset()
		{
			return mFontMetrics[11];
		}

		public virtual int GetUnderlineThickness()
		{
			return mFontMetrics[12];
		}

		public virtual bool HasStrikeInfo()
		{
			return GetVersion() > 4;
		}

		public virtual int GetStrikeOffset()
		{
			return mFontMetrics[13];
		}

		public virtual int GetStrikeThickness()
		{
			return mFontMetrics[14];
		}

		public override int GetGlowOffset()
		{
			return mFontMetrics[15] + mFontMetrics[16];
		}

		public virtual int GetMaxUpperGlowOffset()
		{
			return mFontMetrics[15];
		}

		public virtual int GetMaxLowerGlowOffset()
		{
			return mFontMetrics[16];
		}

		public override int GetLineWidth(FlBitmapMap bitmapMap, FlString @string, int start, int iMaxCharCount)
		{
			return GetLineWidth(bitmapMap, @string, start, iMaxCharCount, true);
		}

		public override int GetLineWidth(FlBitmapMap bitmapMap, FlString @string, int start, int iMaxCharCount, bool removeLastAdvance)
		{
			return GetLineWidth(bitmapMap, @string, start, iMaxCharCount, removeLastAdvance, true);
		}

		public override int GetCharWidth(FlBitmapMap bitmapMap, sbyte letter, bool isFirst, bool isLast)
		{
			return GetCharWidth(bitmapMap, letter, isFirst, isLast, true);
		}

		public static FlBitmapFontBlob[] InstArrayFlBitmapFontBlob(int size)
		{
			FlBitmapFontBlob[] array = new FlBitmapFontBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlBitmapFontBlob();
			}
			return array;
		}

		public static FlBitmapFontBlob[][] InstArrayFlBitmapFontBlob(int size1, int size2)
		{
			FlBitmapFontBlob[][] array = new FlBitmapFontBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapFontBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapFontBlob();
				}
			}
			return array;
		}

		public static FlBitmapFontBlob[][][] InstArrayFlBitmapFontBlob(int size1, int size2, int size3)
		{
			FlBitmapFontBlob[][][] array = new FlBitmapFontBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapFontBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapFontBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlBitmapFontBlob();
					}
				}
			}
			return array;
		}
	}
}
