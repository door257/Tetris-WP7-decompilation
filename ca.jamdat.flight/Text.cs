using System;

namespace ca.jamdat.flight
{
	public class Text : Component
	{
		public new const sbyte typeNumber = 71;

		public new const sbyte typeID = 71;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte wrapDataFieldLineStartIndex = 0;

		public const sbyte wrapDataFieldLineEndIndex = 1;

		public const sbyte wrapDataFieldLineWidth = 2;

		public const sbyte wrapDataFieldCount = 3;

		public const sbyte breakTypeLineBreakChar = 0;

		public const sbyte breakTypeWordBreakChar = 1;

		public const sbyte breakTypeWrapAfterChar = 2;

		public const sbyte breakTypeOptWordBreakChar = 3;

		public const sbyte breakTypeLastChar = 4;

		public FlFont mFont;

		public sbyte mAlignment;

		public FlString mpCaption;

		public int mCaptionLength;

		public bool mKeepTrailingWhiteSpaces;

		public short mLineCount;

		public bool mIsMultiline;

		public short mCurrentLine;

		public int[] mLines;

		public short[] mLinesWidth;

		public short mPreviousWidth;

		public static Text Cast(object o, Text _)
		{
			return (Text)o;
		}

		public override sbyte GetTypeID()
		{
			return 71;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public Text()
		{
			mAlignment = 0;
			mpCaption = new FlString();
			mLineCount = 1;
		}

		public Text(Viewport viewport)
		{
			mAlignment = 0;
			mpCaption = new FlString();
			mLineCount = 1;
			SetViewport(viewport);
		}

		public Text(Viewport viewport, FlFont inFont, FlString caption, short inRect_left, short inRect_top, short inRect_width, short inRect_height, sbyte align)
		{
			mLineCount = 1;
			SetViewport(viewport);
			SetRect(inRect_left, inRect_top, inRect_width, inRect_height);
			SetFont(inFont);
			SetCaption(caption);
			SetAlignment(align);
		}

		public override void destruct()
		{
			mpCaption = null;
			mLines = null;
			mLinesWidth = null;
		}

		public override void OnRectChange()
		{
			if (IsMultiline() && mPreviousWidth != GetRectWidth())
			{
				WrapText();
			}
		}

		public virtual void SetCaption(FlString caption, bool createSegment)
		{
			mpCaption = caption;
			SetCaptionLength(mpCaption.GetLength());
			WrapText();
			SetCurrentLine(0);
		}

		public virtual void KeepTrailingWhiteSpaces(bool keepWhiteSpaces)
		{
			mKeepTrailingWhiteSpaces = keepWhiteSpaces;
		}

		public virtual FlString GetCaption()
		{
			return mpCaption;
		}

		public virtual FlString GetLineString(int inLineIndex)
		{
			FlString flString = mpCaption;
			if (!IsMultiline())
			{
				return flString;
			}
			int lineStartIndex = GetLineStartIndex(inLineIndex);
			int lineEndIndex = GetLineEndIndex(inLineIndex);
			int nbChar = lineEndIndex - lineStartIndex + 1;
			return new FlString(flString, lineStartIndex, nbChar);
		}

		public virtual int GetLineStartIndex(int inLineIndex)
		{
			return mLines[inLineIndex] & 0xFFFF;
		}

		public virtual int GetLineEndIndex(int inLineIndex)
		{
			if (mKeepTrailingWhiteSpaces)
			{
				return (mLines[inLineIndex + 1] & 0xFFFF) - 1;
			}
			return mLines[inLineIndex] >> 16;
		}

		public virtual void SetAlignment(sbyte alignment)
		{
			if (mAlignment != alignment)
			{
				mAlignment = alignment;
				Invalidate();
			}
		}

		public virtual sbyte GetAlignment()
		{
			return mAlignment;
		}

		public virtual bool IsMultiline()
		{
			return mIsMultiline;
		}

		public virtual void SetMultiline(bool multiline)
		{
			if (multiline != mIsMultiline)
			{
				mIsMultiline = multiline;
				if (mIsMultiline)
				{
					WrapText();
				}
				Invalidate();
			}
		}

		public virtual void SetFont(FlFont font)
		{
			if (mFont != font)
			{
				mFont = font;
				if (font != null)
				{
					WrapText();
					Invalidate();
				}
			}
		}

		public virtual FlFont GetFont()
		{
			return mFont;
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			FlString flString = mpCaption;
			FlFont flFont = mFont;
			if (!flString.IsEmpty() && flFont != null)
			{
				if (mIsMultiline && mLineCount > 1)
				{
					displayContext.DrawMultilineString(flString, mLines, mLineCount, mRect_left, mRect_top, mRect_width, mRect_height, mCurrentLine, flFont, mAlignment, mLinesWidth);
				}
				else
				{
					displayContext.DrawString(flString, mRect_left, mRect_top, mRect_width, mRect_height, flFont, mAlignment, 0, mCaptionLength, mLinesWidth[0]);
				}
			}
		}

		public virtual void NextPage()
		{
			if (mCurrentLine + GetLinesPerPage() < mLineCount)
			{
				SetCurrentLine(mCurrentLine + GetLinesPerPage());
			}
		}

		public virtual void PreviousPage()
		{
			SetCurrentLine(mCurrentLine - GetLinesPerPage());
		}

		public virtual bool IsFirstPage()
		{
			return mCurrentLine < GetLinesPerPage();
		}

		public virtual bool IsLastPage()
		{
			return mCurrentLine + GetLinesPerPage() >= mLineCount;
		}

		public virtual void NextLine()
		{
			SetCurrentLine(mCurrentLine + 1);
		}

		public virtual void PreviousLine()
		{
			SetCurrentLine(mCurrentLine - 1);
		}

		public virtual void SetCurrentLine(int lineIndex)
		{
			short num = (short)((lineIndex >= 0) ? ((lineIndex < mLineCount || mLineCount <= 0) ? ((short)lineIndex) : ((short)(mLineCount - 1))) : 0);
			mCurrentLine = num;
			Invalidate();
		}

		public virtual int GetCurrentLine()
		{
			return mCurrentLine;
		}

		public virtual short GetLineHeight()
		{
			return (short)mFont.GetLineHeight();
		}

		public virtual bool IsFirstLine()
		{
			return mCurrentLine == 0;
		}

		public virtual bool IsLastLine()
		{
			return mLineCount == mCurrentLine + 1;
		}

		public virtual int GetNbPages()
		{
			if (GetLinesPerPage() != 0)
			{
				return mLineCount / GetLinesPerPage() + ((mLineCount % GetLinesPerPage() != 0) ? 1 : 0);
			}
			return 1;
		}

		public virtual int GetNbLines()
		{
			return mLineCount;
		}

		public virtual int GetLinesPerPage()
		{
			return GetRectHeight() / GetLineHeight();
		}

		public override void OnVisibilityChange()
		{
			if (IsVisible() && mIsMultiline)
			{
				Invalidate();
			}
			base.OnVisibilityChange();
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mpCaption = null;
			mpCaption = FlString.Cast(p.SerializePointer(35, false, false), null);
			SetCaptionLength(mpCaption.GetLength());
			mFont = FlFont.Cast(p.SerializePointer(36, false, false), null);
			sbyte t = mAlignment;
			t = p.SerializeIntrinsic(t);
			mAlignment = t;
			short t2 = 0;
			t2 = p.SerializeIntrinsic(t2);
			mIsMultiline = p.SerializeIntrinsic(mIsMultiline);
			WrapText();
		}

		public virtual Vector2_short GetCharIndex2DPosition(int charIndex)
		{
			if (!IsMultiline())
			{
				return new Vector2_short((short)charIndex, 0);
			}
			short num = 0;
			while ((num < mLineCount) && charIndex > (mLines[num] & 0xFFFF))
			{
				num = (short)(num + 1);
			}
			if (num == mLineCount)
			{
				num = (short)(num - 1);
			}
			return new Vector2_short((short)(charIndex - (mLines[num] & 0xFFFF)), num);
		}

		public virtual short GetLineWidth(int lineIndex)
		{
			return mLinesWidth[lineIndex];
		}

		public virtual void ComputeLineWidth(bool removeLastAdvance)
		{
			FlString flString = mpCaption;
			int num = mCaptionLength;
			if (flString == null || mFont == null)
			{
				if (flString == null)
				{
					SetCaptionLength(0);
				}
				return;
			}
			if (!mKeepTrailingWhiteSpaces)
			{
				while (num > 0 && (flString.GetCharAt(num - 1) == 32 || flString.GetCharAt(num - 1) == 29))
				{
					num--;
				}
				SetCaptionLength(num);
			}
			mLinesWidth = new short[1] { (short)mFont.GetLineWidth(flString, 0, num, removeLastAdvance) };
		}

		public virtual void WrapText()
		{
			if (!mIsMultiline)
			{
				mLineCount = 1;
				ComputeLineWidth();
				return;
			}
			short rectWidth = GetRectWidth();
			FlFont flFont = mFont;
			if (rectWidth != 0 && flFont != null)
			{
				int num = 192;
				Array_int wrapData = new Array_int(num, num);
				int num2 = WrapString(flFont, rectWidth, wrapData);
				int[] array = new int[num2 + 1];
				for (int i = 0; i <= num2; i++)
				{
					array[i] = (int)((array[i] & 0xFFFF0000u) | (short)WrapDataLineStart(wrapData, i));
					array[i] = (array[i] = (array[i] & 0xFFFF) | (WrapDataLineEnd(wrapData, i) << 16));
				}
				short[] array2 = new short[num2];
				for (int i = 0; i < num2; i++)
				{
					array2[i] = (short)WrapDataLineWidth(wrapData, i);
				}
				mLines = array;
				mLinesWidth = array2;
				mPreviousWidth = rectWidth;
				mLineCount = (short)num2;
				int num3 = num2 * GetLineHeight() - GetLeading();
				SetSize(rectWidth, (short)num3);
			}
		}

		public static void WrapDataEnsureSize(Array_int wrapData, int size)
		{
			int capacity = wrapData.GetCapacity();
			int num = size * 3;
			wrapData.SetSize((capacity > num) ? capacity : num);
		}

		public static int WrapDataArrayPosition(int entryi, sbyte field)
		{
			return entryi * 3 + field;
		}

		public static int WrapDataLineStart(Array_int wrapData, int entryi)
		{
			return wrapData.GetAt(WrapDataArrayPosition(entryi, 0));
		}

		public static void WrapDataSetLineStart(Array_int wrapData, int entryi, int value)
		{
			wrapData.SetAt(value, WrapDataArrayPosition(entryi, 0));
		}

		public static int WrapDataLineEnd(Array_int wrapData, int entryi)
		{
			return wrapData.GetAt(WrapDataArrayPosition(entryi, 1));
		}

		public static void WrapDataSetLineEnd(Array_int wrapData, int entryi, int value)
		{
			wrapData.SetAt(value, WrapDataArrayPosition(entryi, 1));
		}

		public static int WrapDataLineWidth(Array_int wrapData, int entryi)
		{
			return wrapData.GetAt(WrapDataArrayPosition(entryi, 2));
		}

		public static void WrapDataSetLineWidth(Array_int wrapData, int entryi, int value)
		{
			wrapData.SetAt(value, WrapDataArrayPosition(entryi, 2));
		}

		public virtual int WrapString(FlFont font, int width, Array_int wrapData)
		{
			FlString flString = mpCaption;
			short num = 1;
			sbyte b = 4;
			short num2 = -1;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			short num5 = 0;
			WrapDataEnsureSize(wrapData, 2);
			WrapDataSetLineStart(wrapData, 0, 0);
			short num6 = 0;
			sbyte charAt = flString.GetCharAt(0);
			int charType = Characters.GetCharType(charAt);
			if (charAt == 0)
			{
				WrapDataSetLineEnd(wrapData, 0, 0);
				WrapDataSetLineStart(wrapData, num, 1);
				num = (short)(num + 1);
			}
			while (charAt != 0)
			{
				sbyte b2 = charAt;
				int characterType = charType;
				charAt = flString.GetCharAt(num6 + 1);
				charType = Characters.GetCharType(charAt);
				bool flag2 = true;
				int charWidth = font.GetCharWidth(b2, num3 == 0, true);
				int num7 = num3 + charWidth;
				if (num7 > width)
				{
					flag2 = false;
				}
				else if (num7 == width)
				{
					flag2 = Characters.IsWordSeparator(charType);
				}
				int charWidth2 = font.GetCharWidth(b2, num3 == 0, false);
				num3 += charWidth2;
				bool flag3 = false;
				if (b2 == 13 && charAt == 10)
				{
					flag = true;
				}
				else
				{
					if (Characters.IsLineSeparator(characterType))
					{
						short num8 = (short)(1 + (flag ? 1 : 0));
						num5 = ((b != 1 || num2 != num6 - num8) ? num8 : ((short)(num5 + num8)));
						b = 0;
						flag3 = true;
					}
					else if (b >= 1 && Characters.IsWordSeparator(characterType))
					{
						if (b2 != -83 || flag2)
						{
							short num9 = 1;
							if (b2 == -83)
							{
								num9 = 0;
							}
							num5 = ((b != 1 || num2 != num6 - 1) ? num9 : ((short)(num5 + num9)));
							b = 1;
							flag3 = true;
						}
					}
					else if (b >= 3 && Characters.IsOptionalWordBreak(characterType))
					{
						b = 3;
						num5 = 1;
						flag3 = true;
					}
					if (flag2)
					{
						if (b >= 2 && Characters.CanWrapAfter(characterType) && Characters.CanWrapBefore(charType))
						{
							b = 2;
							num5 = 0;
							flag3 = true;
						}
						if (b != 0 && charAt == 0)
						{
							b = 0;
							if (!flag3)
							{
								num5 = 0;
							}
							flag3 = true;
						}
					}
					else if (b == 4)
					{
						num2 = (short)(num6 - 1);
						num4 = num3 - font.GetCharWidth(b2, num3 == 0, false);
					}
					if (flag3)
					{
						num2 = num6;
						num4 = num3;
					}
					if (b == 0 || !flag2)
					{
						num6 = num2;
						sbyte charAt2 = flString.GetCharAt(num2);
						while (num6 - num5 < num2)
						{
							num4 -= font.GetCharWidth(charAt2, num4 == 0, false);
							num2 = (short)(num2 - 1);
							if (num2 >= 0)
							{
								charAt2 = flString.GetCharAt(num2);
							}
						}
						if (num2 > WrapDataLineStart(wrapData, num - 1))
						{
							num4 -= font.GetCharWidth(charAt2, num4 == 0, false);
							num4 += font.GetCharWidth(charAt2, num4 == 0, true);
						}
						WrapDataEnsureSize(wrapData, num + 1);
						WrapDataSetLineWidth(wrapData, num - 1, num4);
						WrapDataSetLineEnd(wrapData, num - 1, num2);
						WrapDataSetLineStart(wrapData, num, num6 + 1);
						num = (short)(num + 1);
						charAt = flString.GetCharAt(num6 + 1);
						charType = Characters.GetCharType(charAt);
						num2 = -1;
						num3 = 0;
						flag = false;
						b = 4;
						num5 = 0;
					}
				}
				num6 = (short)(num6 + 1);
			}
			WrapDataEnsureSize(wrapData, num + 1);
			WrapDataSetLineStart(wrapData, num, num6);
			WrapDataSetLineEnd(wrapData, num, -1);
			return num - 1;
		}

		public void AppendLine(FlString caption)
		{
			FlString flString = mpCaption;
			if (mIsMultiline && GetRectWidth() != 0 && GetRectHeight() != 0 && mFont != null)
			{
				if (mpCaption.IsEmpty())
				{
					SetCaption(caption);
					return;
				}
				int num = mLineCount;
				int[] array = mLines;
				short[] array2 = mLinesWidth;
				mLines = null;
				mLinesWidth = null;
				mpCaption = caption;
				SetCaptionLength(mpCaption.GetLength());
				WrapText();
				int num2 = mLineCount;
				int[] array3 = mLines;
				short[] array4 = mLinesWidth;
				int num3 = num + num2;
				int[] array5 = new int[num3 + 1];
				short[] array6 = new short[num3];
				int num4 = 0;
				for (num4 = 0; num4 < num; num4++)
				{
					array5[num4] = array[num4];
					array6[num4] = array2[num4];
				}
				long num5 = array[num] & 0xFFFF;
				for (num4 = 0; num4 < num2; num4++)
				{
					array5[num4 + num] = (int)((array5[num4 + num] & 0xFFFF0000u) | ((array3[num4] & 0xFFFF) + num5));
					array5[num4 + num] = (array5[num4 + num] = (array5[num4 + num] & 0xFFFF) | (int)((array3[num4] >> 16) + num5 << 16));
					array6[num4 + num] = array4[num4];
				}
				array5[num3] = (int)((array5[num3] & 0xFFFF0000u) | ((array3[num4] & 0xFFFF) + num5 + 1));
				array = null;
				array2 = null;
				array3 = null;
				array4 = null;
				mLines = array5;
				mLinesWidth = array6;
				mLineCount = (short)num3;
			}
			mpCaption = new FlString(flString.Add(caption).Add(new FlString("\n")));
			SetCaptionLength(mpCaption.GetLength());
			flString = null;
			caption = null;
		}

		public void RemoveLines(int startLineIndex, int nbLines)
		{
			int num = mLineCount;
			int[] array = mLines;
			short[] array2 = mLinesWidth;
			int num2 = num - nbLines;
			int[] array3 = new int[num2 + 1];
			short[] array4 = new short[num2];
			for (int i = 0; i < startLineIndex; i++)
			{
				array3[i] = array[i];
				array4[i] = array2[i];
			}
			int num3 = array[startLineIndex] & 0xFFFF;
			int num4 = (array[startLineIndex + nbLines] & 0xFFFF) - num3;
			for (int i = startLineIndex + nbLines; i < num; i++)
			{
				array3[i - nbLines] = (int)((array3[i - nbLines] & 0xFFFF0000u) | ((array[i] & 0xFFFF) - num4));
				array3[i - nbLines] = (array3[i - nbLines] = (array3[i - nbLines] & 0xFFFF) | ((array[i] >> 16) - num4 << 16));
				array4[i - nbLines] = array2[i];
			}
			array3[num2] = (int)((array3[num2] & 0xFFFF0000u) | ((array[num] & 0xFFFF) - num4));
			FlString flString = mpCaption;
			mpCaption = new FlString(flString.Substring(0, num3).Add(flString.Substring(num3 + num4, array[num])));
			SetCaptionLength(mpCaption.GetLength());
			mLineCount = (short)num2;
			mLines = array3;
			mLinesWidth = array4;
		}

		public virtual sbyte GetLeading()
		{
			return mFont.GetLeading();
		}

		public virtual void SetCaptionLength(int newLength)
		{
			mCaptionLength = newLength;
		}

		public virtual void SetCaption(FlString caption)
		{
			SetCaption(caption, true);
		}

		public virtual short GetLineWidth()
		{
			return GetLineWidth(0);
		}

		public virtual void ComputeLineWidth()
		{
			ComputeLineWidth(true);
		}

		public static Text[] InstArrayText(int size)
		{
			Text[] array = new Text[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Text();
			}
			return array;
		}

		public static Text[][] InstArrayText(int size1, int size2)
		{
			Text[][] array = new Text[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Text[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Text();
				}
			}
			return array;
		}

		public static Text[][][] InstArrayText(int size1, int size2, int size3)
		{
			Text[][][] array = new Text[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Text[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Text[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Text();
					}
				}
			}
			return array;
		}
	}
}
