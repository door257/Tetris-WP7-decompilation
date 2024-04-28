using Microsoft.Xna.Framework;

namespace ca.jamdat.flight
{
	public abstract class DisplayContext
	{
		public const sbyte resolutionPortrait = 0;

		public const sbyte resolutionLandscape = 1;

		public const sbyte resolutionCount = 2;

		public const sbyte resolutionInvalid = 3;

		public const sbyte bmpTransformIdentity = 0;

		public const sbyte bmpTransformFlipY = 1;

		public const sbyte bmpTransformFlipX = 2;

		public const sbyte bmpTransformFlipDiag = 3;

		public const sbyte alignmentLeft = 0;

		public const sbyte alignmentCenter = 1;

		public const sbyte alignmentRight = 2;

		public const sbyte alignmentVTop = 0;

		public const sbyte alignmentVBottom = 1;

		public const sbyte alignmentVBaseline = 2;

		public const sbyte alignmentVCenter = 3;

		public int mDirtyRectIndex;

		public bool mDirtyRectsEnabled;

		public short[] mDirtyRects_left = new short[3];

		public short[] mDirtyRects_top = new short[3];

		public short[] mDirtyRects_width = new short[3];

		public short[] mDirtyRects_height = new short[3];

		public int[] mDirtyRectsAreas = new int[3];

		public int mSlotsUsed;

		public int mDirtyRectMergePixelThreshold;

		public VideoMode mVideoMode;

		public bool mInvertedLandscape;

		public short mClipRect_left;

		public short mClipRect_top;

		public short mClipRect_width;

		public short mClipRect_height;

		public bool mBypassClipping;

		public short mCumulativeOffsetX;

		public short mCumulativeOffsetY;

		public FlRect mScreenRect;

		public int mClearNextFramesCount;

		public DisplayContext(VideoMode mode)
		{
			mDirtyRectIndex = -1;
			mDirtyRectsEnabled = true;
			mVideoMode = new VideoMode(mode);
			mScreenRect = new FlRect(0, 0, (short)mode.GetWidth(), (short)mode.GetHeight());
			mDirtyRectMergePixelThreshold = mode.GetWidth();
		}

		public static short GetAlignmentOffsetX(sbyte align, short rectSizeX, short strSizeX)
		{
			short num = 0;
			if (align != 0)
			{
				num = (short)(rectSizeX - strSizeX);
				if (align == 1)
				{
					num = (short)(num >> 1);
				}
			}
			return num;
		}

		public virtual void destruct()
		{
		}

		public abstract void NativeDrawArc(int a14, int a13, int a12, int a11, int a10, int a9);

		public abstract void NativeDrawLine(int a18, int a17, int a16, int a15);

		public abstract void NativeDrawPixel(int a20, int a19);

		public abstract void NativeDrawRect(int a24, int a23, int a22, int a21);

		public abstract void NativeDrawRGB(int[] a32, int a31, int a30, int a29, int a28, int a27, int a26, bool a25);

		public abstract void NativeDrawRoundRect(int a38, int a37, int a36, int a35, int a34, int a33);

		public abstract void NativeFillArc(int a44, int a43, int a42, int a41, int a40, int a39);

		public abstract void NativeFillRect(int a48, int a47, int a46, int a45);

		public abstract void NativeFillRoundRect(int a54, int a53, int a52, int a51, int a50, int a49);

		public abstract int NativeGetClipHeight();

		public abstract int NativeGetClipWidth();

		public abstract int NativeGetClipX();

		public abstract int NativeGetClipY();

		public abstract void NativeSetClip(int a58, int a57, int a56, int a55);

		public abstract int NativeGetAlpha();

		public abstract Color NativeGetARGB();

		public abstract Color NativeGetRGB();

		public abstract void NativeSetAlpha(int a59);

		public abstract void NativeSetARGB(Color a60);

		public abstract void NativeSetRGB(Color a61);

		public abstract void NativeSetARGB(int a65, int a64, int a63, int a62);

		public abstract void NativeSetRGB(int a68, int a67, int a66);

		public abstract void NativeSetFont(object a69);

		public virtual void NativeDrawString(string str, int x, int y, sbyte flightAlignH, sbyte flightAlignV)
		{
		}

		public abstract int NativeGetTranslatedX();

		public abstract int NativeGetTranslatedY();

		public abstract void NativeTranslate(int a71, int a70);

		public virtual void DrawString(FlString @string, short rect_left, short rect_top, short rect_width, short rect_height, FlFont font, sbyte alignment, int startIndex, int nChars, short lineWidth, int drawProperty)
		{
			if (@string.GetCharAt(startIndex) != 0)
			{
				short num = (short)(rect_left + mCumulativeOffsetX);
				short num2 = (short)(rect_top + mCumulativeOffsetY);
				short num3 = rect_width;
				short num4 = rect_height;
				short num5 = mClipRect_left;
				short num6 = mClipRect_top;
				short num7 = mClipRect_width;
				short num8 = mClipRect_height;
				if (FlRect.Contains(num5, num6, num7, num8, num, num2, num3, num4))
				{
					mBypassClipping = true;
				}
				else if (!FlRect.Intersects(num5, num6, num7, num8, num, num2, num3, num4))
				{
					return;
				}
				int alignmentOffsetX = GetAlignmentOffsetX(alignment, rect_width, lineWidth);
				font.DrawString(this, @string, (short)(rect_left + alignmentOffsetX), rect_top, lineWidth, startIndex, nChars);
				mBypassClipping = false;
			}
		}

		public virtual void DrawMultilineString(FlString @string, int[] lines, int totalLineCount, short textBoxRect_left, short textBoxRect_top, short textBoxRect_width, short textBoxRect_height, int topLine, FlFont font, sbyte alignment, short[] linesWidth, int drawProperty)
		{
			if (font == null)
			{
				return;
			}
			int lineHeight = font.GetLineHeight();
			int num = lineHeight - font.GetLeading();
			short num2 = textBoxRect_left;
			short num3 = textBoxRect_top;
			short num4 = textBoxRect_width;
			short num5 = textBoxRect_height;
			num2 = (short)(num2 + mCumulativeOffsetX);
			short num6 = mCumulativeOffsetY;
			num3 = (short)(num3 + num6);
			bool flag = num2 >= mClipRect_left && (short)(num2 + num4 - 1) <= (short)(mClipRect_left + mClipRect_width - 1);
			int num7 = mClipRect_top;
			int num8 = (short)(mClipRect_top + mClipRect_height - 1);
			int num9 = num3;
			int num10 = (short)(num3 + num5 - 1);
			int num11 = num7 - num9;
			int num12 = topLine;
			int num13 = num9;
			if (num11 > 0)
			{
				num13 += num11;
				if (num11 > num)
				{
					num12++;
					num11 -= num;
					num12 += num11 / lineHeight;
					num = lineHeight;
				}
			}
			if (num12 <= totalLineCount - 1)
			{
				int num14 = num10;
				if (num14 > num8)
				{
					num14 = num8;
				}
				int num15 = (num12 - topLine) * lineHeight;
				short num16 = 0;
				short num17 = (short)(num3 - mCumulativeOffsetY + num15);
				num13 -= num6;
				num14 -= num6;
				int num18 = num12;
				int num19 = textBoxRect_top + num15;
				int num20 = num19 + num;
				int num21 = 0;
				int num22 = 0;
				num = lineHeight;
				do
				{
					mBypassClipping = flag && num19 >= num13 && num20 - 1 <= num14;
					num22 = lines[num18] & 0xFFFF;
					num21 = (lines[num18] >> 16) - num22 + 1;
					num16 = (short)(textBoxRect_left + GetAlignmentOffsetX(alignment, textBoxRect_width, linesWidth[num18]));
					font.DrawString(this, @string, num16, num17, linesWidth[num18], num22, num21);
					num17 = (short)(num17 + num);
					num18++;
					num19 = num20;
					num20 = num19 + num;
				}
				while (num18 < totalLineCount && num19 <= num14);
				mBypassClipping = false;
			}
		}

		public abstract void DrawRGB(int[] a79, int a78, int a77, int a76, int a75, int a74, int a73, bool a72);

		public abstract void DrawRectangle(short a88, short a87, short a86, short a85, bool a84, int a83, int a82, int a81, int a80);

		public abstract void DrawRoundRectangle(short a97, short a96, short a95, short a94, short a93, short a92, bool a91, Color888 a90, int a89);

		public virtual void DrawFrame(short rect_left, short rect_top, short rect_width, short rect_height, Color888 color, int drawProperty)
		{
			DrawRectangle((short)(rect_left - mCumulativeOffsetX), (short)(rect_top - mCumulativeOffsetY), rect_width, rect_height, false, color.GetRed(), color.GetGreen(), color.GetBlue(), drawProperty);
		}

		public virtual void DrawAbsoluteSolidRectangle(short rect_left, short rect_top, short rect_width, short rect_height, int colorRed, int colorGreen, int colorBlue, int drawProperty)
		{
			DrawRectangle((short)(rect_left - mCumulativeOffsetX), (short)(rect_top - mCumulativeOffsetY), rect_width, rect_height, true, colorRed, colorGreen, colorBlue, drawProperty);
		}

		public abstract void DrawTriangle(short a106, short a105, short a104, short a103, short a102, short a101, bool a100, Color888 a99, int a98);

		public virtual void DrawAbsoluteLine(short fromX, short fromY, short toX, short toY, Color888 color, int drawProperty)
		{
			DrawLine((short)(fromX - mCumulativeOffsetX), (short)(fromY - mCumulativeOffsetY), (short)(toX - mCumulativeOffsetX), (short)(toY - mCumulativeOffsetY), color, drawProperty);
		}

		public abstract void DrawLine(short a112, short a111, short a110, short a109, Color888 a108, int a107);

		public abstract void DrawPixel(short a116, short a115, Color888 a114, int a113);

		public virtual void DrawCircle(Vector2_short center, short radius, bool solid, Color888 fillColor, Color888 outlineColor)
		{
			short a = (short)(center.GetX() - radius);
			short a2 = (short)(center.GetY() - radius);
			short num = (short)(radius << 1);
			if (solid)
			{
				DrawArc(a, a2, num, num, 0, 360, true, fillColor, 255);
			}
			DrawArc(a, a2, num, num, 0, 360, false, outlineColor, 255);
		}

		public abstract void DrawArc(short a125, short a124, short a123, short a122, short a121, short a120, bool a119, Color888 a118, int a117);

		public abstract void Clear(Color888 a126);

		public abstract void ClearFullscreen(Color888 a127);

		public virtual void ClearNextFrames(int framesCount)
		{
			mClearNextFramesCount = framesCount;
		}

		public virtual void DrawBitmapSection(FlBitmap bitmap, short posX, short posY, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, int drawProperty)
		{
			short num = (short)(posX + mCumulativeOffsetX);
			short num2 = (short)(posY + mCumulativeOffsetY);
			short num3 = num;
			short num4 = num2;
			short num5 = sourceRect_width;
			short num6 = sourceRect_height;
			if (!mBypassClipping)
			{
				short num7 = mClipRect_left;
				short num8 = mClipRect_top;
				short num9 = mClipRect_width;
				short num10 = mClipRect_height;
				int num11 = num7 + num9;
				int num12 = num3 + num5;
				int num13 = num8 + num10;
				int num14 = num4 + num6;
				if (num7 > num3)
				{
					num3 = num7;
				}
				if (num8 > num4)
				{
					num4 = num8;
				}
				if (num11 < num12)
				{
					num12 = num11;
				}
				if (num13 < num14)
				{
					num14 = num13;
				}
				num5 = (short)(num12 - num3);
				num6 = (short)(num14 - num4);
			}
			if (num5 > 0 && num6 > 0)
			{
				short a = num3;
				short a2 = num4;
				short a3 = num5;
				short a4 = num6;
				DrawAbsoluteBitmapSection(bitmap, a, a2, sourceRect_left, sourceRect_top, a3, a4, drawProperty);
			}
		}

		public virtual void DrawBitmapSection(FlBitmap bitmap, short posX, short posY, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, bool flipX, bool flipY)
		{
			int drawProperty = 255;
			drawProperty = FlDrawPropertyUtil.ApplyTransform(drawProperty, FlDrawPropertyUtil.FlipXYToTransform(flipX, flipY));
			DrawBitmapSection(bitmap, posX, posY, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, drawProperty);
		}

		public virtual void DrawTiledBitmapSection(FlBitmap bitmap, short publicSourceWidth, short publicSourceHeight, short sourceOffsetX, short sourceOffsetY, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, short destRect_left, short destRect_top, short destRect_width, short destRect_height, int drawProperty)
		{
			short num = mCumulativeOffsetX;
			short num2 = mCumulativeOffsetY;
			short num3 = (short)(destRect_left + num);
			short num4 = (short)(destRect_top + num2);
			short num5 = destRect_width;
			short num6 = destRect_height;
			int num7 = mClipRect_left + mClipRect_width;
			int num8 = num3 + num5;
			int num9 = mClipRect_top + mClipRect_height;
			int num10 = num4 + num6;
			if (mClipRect_left > num3)
			{
				num3 = mClipRect_left;
			}
			if (mClipRect_top > num4)
			{
				num4 = mClipRect_top;
			}
			if (num7 < num8)
			{
				num8 = num7;
			}
			if (num9 < num10)
			{
				num10 = num9;
			}
			num5 = (short)(num8 - num3);
			num6 = (short)(num10 - num4);
			if (num5 <= 0 || num6 <= 0)
			{
				return;
			}
			int num11 = num3 - num;
			int num12 = num11 + num5;
			int i = num4 - num2;
			int num16;
			for (int num13 = i + num6; i < num13; i += num16)
			{
				int num14 = ((publicSourceHeight != 0) ? ((i - destRect_top) % publicSourceHeight) : (i - destRect_top));
				int num15 = FlMath.Minimum(publicSourceHeight, num13 - i + num14);
				num16 = num15 - num14;
				int num17 = FlMath.Maximum(0, sourceOffsetY - num14);
				int num18 = FlMath.Maximum(0, num14 - sourceOffsetY);
				int num19 = FlMath.Minimum(sourceRect_height, num15 - sourceOffsetY);
				int num20 = FlMath.Maximum(0, num19 - num18);
				int num23;
				for (int j = num11; j < num12; j += num23)
				{
					int num21 = ((publicSourceWidth != 0) ? ((j - destRect_left) % publicSourceWidth) : (j - destRect_left));
					int num22 = FlMath.Minimum(publicSourceWidth, num12 - j + num21);
					num23 = num22 - num21;
					int num24 = FlMath.Maximum(0, sourceOffsetX - num21);
					int num25 = FlMath.Maximum(0, num21 - sourceOffsetX);
					int num26 = FlMath.Minimum(sourceRect_width, num22 - sourceOffsetX);
					int num27 = FlMath.Maximum(0, num26 - num25);
					mBypassClipping = true;
					DrawBitmapSection(bitmap, (short)(j + num24), (short)(i + num17), (short)(sourceRect_left + num25), (short)(sourceRect_top + num18), (short)num27, (short)num20, drawProperty);
				}
			}
			mBypassClipping = false;
		}

		public virtual void DrawTiledBitmapSection(FlBitmap bitmap, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, short destRect_left, short destRect_top, short destRect_width, short destRect_height, int drawProperty)
		{
			DrawTiledBitmapSection(bitmap, sourceRect_width, sourceRect_height, 0, 0, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, destRect_left, destRect_top, destRect_width, destRect_height, drawProperty);
		}

		public abstract void DrawAbsoluteBitmapSection(FlBitmap a135, int a134, int a133, int a132, int a131, int a130, int a129, int a128);

		public abstract void DrawAbsoluteBitmapSection(FlBitmap a143, int a142, int a141, int a140, int a139, int a138, int a137, sbyte a136);

		public virtual void DrawAbsoluteBitmapBypassClipping(FlBitmap bitmap, int desLeft, int desTop)
		{
			DrawBitmapSection(bitmap, (short)(desLeft - mCumulativeOffsetX), (short)(desTop - mCumulativeOffsetY), 0, 0, bitmap.GetWidth(), bitmap.GetHeight(), false, false);
		}

		public virtual void DrawAbsoluteBitmap(FlBitmap bitmap, int desLeft, int desTop)
		{
			ResetClip();
			DrawAbsoluteBitmapBypassClipping(bitmap, desLeft, desTop);
		}

		public virtual void ResetClip()
		{
		}

		public virtual void OffsetBy(short inDeltax, short inDeltay)
		{
			mCumulativeOffsetX += inDeltax;
			mCumulativeOffsetY += inDeltay;
		}

		public virtual bool ApplicationIsPortrait()
		{
			return IsResolution(false, 0);
		}

		public virtual bool ApplicationIsLandscape()
		{
			return IsResolution(false, 1);
		}

		public virtual bool DeviceIsPortrait()
		{
			return IsResolution(true, 0);
		}

		public virtual bool DeviceIsLandscape()
		{
			return IsResolution(true, 1);
		}

		public virtual bool IsResolution(bool forDevice, sbyte screenResolution)
		{
			return GetResolution(forDevice) == screenResolution;
		}

		public virtual sbyte GetResolution(bool forDevice)
		{
			VideoMode videoMode = null;
			videoMode = GetVideoMode();
			if (videoMode.GetWidth() <= videoMode.GetHeight())
			{
				return 0;
			}
			return 1;
		}

		public virtual short GetClippingRectLeft()
		{
			return (short)(GetAbsoluteClippingRectLeft() - GetCumulativeOffsetX());
		}

		public virtual short GetClippingRectTop()
		{
			return (short)(GetAbsoluteClippingRectTop() - GetCumulativeOffsetY());
		}

		public virtual short GetClippingRectWidth()
		{
			return GetAbsoluteClippingRectWidth();
		}

		public virtual short GetClippingRectHeight()
		{
			return GetAbsoluteClippingRectHeight();
		}

		public virtual void SetClippingRect(short rect_left, short rect_top, short rect_width, short rect_height)
		{
			rect_left = (short)(rect_left + GetCumulativeOffsetX());
			rect_top = (short)(rect_top + GetCumulativeOffsetY());
			SetAbsoluteClippingRect(rect_left, rect_top, rect_width, rect_height);
		}

		public virtual short GetAbsoluteClippingRectTop()
		{
			return mClipRect_top;
		}

		public virtual short GetAbsoluteClippingRectLeft()
		{
			return mClipRect_left;
		}

		public virtual short GetAbsoluteClippingRectWidth()
		{
			return mClipRect_width;
		}

		public virtual short GetAbsoluteClippingRectHeight()
		{
			return mClipRect_height;
		}

		public virtual void SetAbsoluteClippingRect(short rect_left, short rect_top, short rect_width, short rect_height)
		{
			mClipRect_left = rect_left;
			mClipRect_top = rect_top;
			mClipRect_width = rect_width;
			mClipRect_height = rect_height;
		}

		public virtual void DrawClipRectInZBuffer()
		{
		}

		public virtual void ClearClipRectInZBuffer()
		{
		}

		public virtual void BeginScene()
		{
			if (mClearNextFramesCount > 0)
			{
				mClearNextFramesCount--;
				ClearFullscreen(new Color888(0, 0, 0));
			}
			mDirtyRectIndex = 0;
		}

		public virtual void EndScene()
		{
			mDirtyRectIndex = -1;
			ClearDirtyRects();
		}

		public virtual void RenderApplication()
		{
			ClearDirtyRects();
			AddDirtyRect(0, 0, (short)GetVideoMode().GetWidth(), (short)GetVideoMode().GetHeight());
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			int[] array = mDirtyRectsAreas;
			for (int i = 0; i < mSlotsUsed - 1; i++)
			{
				short num5 = mDirtyRects_left[i];
				short num6 = mDirtyRects_top[i];
				short num7 = mDirtyRects_width[i];
				short num8 = mDirtyRects_height[i];
				for (int j = i + 1; j < mSlotsUsed; j++)
				{
					short num9 = mDirtyRects_left[j];
					short num10 = mDirtyRects_top[j];
					short num11 = mDirtyRects_width[j];
					short num12 = mDirtyRects_height[j];
					num = FlMath.Minimum(num5, num9);
					num2 = FlMath.Minimum(num6, num10);
					num3 = (short)(FlMath.Maximum((short)(num5 + num7), (short)(num9 + num11)) - num);
					num4 = (short)(FlMath.Maximum((short)(num6 + num8), (short)(num10 + num12)) - num2);
					int num13 = num3 * num4;
					if (num13 - (mDirtyRectsAreas[i] + mDirtyRectsAreas[j]) <= mDirtyRectMergePixelThreshold)
					{
						mDirtyRects_left[i] = num;
						mDirtyRects_top[i] = num2;
						mDirtyRects_width[i] = num3;
						mDirtyRects_height[i] = num4;
						array[i] = num13;
						for (int k = j + 1; k < mSlotsUsed; k++)
						{
							mDirtyRects_left[k - 1] = mDirtyRects_left[k];
							mDirtyRects_top[k - 1] = mDirtyRects_top[k];
							mDirtyRects_width[k - 1] = mDirtyRects_width[k];
							mDirtyRects_height[k - 1] = mDirtyRects_height[k];
							array[k - 1] = array[k];
						}
						num5 = mDirtyRects_left[i];
						num6 = mDirtyRects_top[i];
						num7 = mDirtyRects_width[i];
						num8 = mDirtyRects_height[i];
						mSlotsUsed--;
						j--;
					}
				}
			}
			BeginScene();
			short rect_left = mClipRect_left;
			short rect_top = mClipRect_top;
			short rect_width = mClipRect_width;
			short rect_height = mClipRect_height;
			while (SetNextDirtyRect())
			{
				if (mClipRect_width <= 0 || mClipRect_height <= 0)
				{
					SetAbsoluteClippingRect(rect_left, rect_top, rect_width, rect_height);
					continue;
				}
				FlApplication.GetInstance().OnDraw(this);
				SetAbsoluteClippingRect(rect_left, rect_top, rect_width, rect_height);
			}
			EndScene();
			FlApplication.GetInstance().SetDirty(false);
		}

		public virtual short GetCumulativeOffsetX()
		{
			return mCumulativeOffsetX;
		}

		public virtual short GetCumulativeOffsetY()
		{
			return mCumulativeOffsetY;
		}

		public virtual VideoMode GetVideoMode()
		{
			return mVideoMode;
		}

		public virtual void SetVideoMode(VideoMode mode)
		{
			mVideoMode = mode;
		}

		public virtual FlRect GetScreenRect()
		{
			return mScreenRect;
		}

		public virtual void UpdateOrientation(VideoMode newMode)
		{
			mScreenRect.Assign(new FlRect(0, 0, (short)newMode.GetWidth(), (short)newMode.GetHeight()));
			mVideoMode = newMode;
			mDirtyRectMergePixelThreshold = newMode.GetWidth();
		}

		public abstract sbyte GetDisplayAPI();

		public virtual bool InvertedLandscape()
		{
			return mInvertedLandscape;
		}

		public virtual void SetInvertedLandscape(bool inverted)
		{
			mInvertedLandscape = inverted;
		}

		public virtual void EnableVSync(bool enable)
		{
		}

		public virtual void GenericDrawArc(short x, short y, short width, short height, short startAngle, short sweepAngle, bool solid, Color888 color, int drawProperty)
		{
			if (width == height && sweepAngle >= 360)
			{
				short num = (short)(width >> 1);
				GenericDrawCircle((short)(x + num), (short)(y + num), num, solid, color, drawProperty);
			}
			else
			{
				if (width <= 0 || height <= 0)
				{
					return;
				}
				while (startAngle < 0)
				{
					startAngle = (short)(startAngle + 360);
				}
				startAngle = (short)(startAngle % 360);
				if (sweepAngle > 360 || sweepAngle < -360)
				{
					sweepAngle = 360;
				}
				if (sweepAngle < 0)
				{
					startAngle = (short)(startAngle + sweepAngle);
					sweepAngle = (short)(-sweepAngle);
				}
				int num2 = startAngle + sweepAngle;
				if (solid)
				{
					short num3 = (short)(width >> 1);
					short num4 = (short)(height >> 1);
					short xc = (short)(x + num3);
					short yc = (short)(y + num4);
					short num5 = startAngle;
					short num6 = sweepAngle;
					while (num6 > 0)
					{
						if (num5 >= 270)
						{
							int num7 = ((num6 > 360 - num5) ? 360 : (num5 + num6));
							FillArcImpl(xc, yc, num3, num4, num5, (short)num7, color, drawProperty);
							num6 = (short)(num6 - (short)(360 - num5));
							num5 = 0;
						}
						else if (num5 >= 180)
						{
							int num8 = ((num6 > 270 - num5) ? 270 : (num5 + num6));
							FillArcImpl(xc, yc, num3, num4, num5, (short)num8, color, drawProperty);
							num6 = (short)(num6 - (short)(270 - num5));
							num5 = 270;
						}
						else if (num5 >= 90)
						{
							int num9 = ((num6 > 180 - num5) ? 180 : (num5 + num6));
							FillArcImpl(xc, yc, num3, num4, num5, (short)num9, color, drawProperty);
							num6 = (short)(num6 - (short)(180 - num5));
							num5 = 180;
						}
						else
						{
							int num10 = ((num6 > 90 - num5) ? 90 : (num5 + num6));
							FillArcImpl(xc, yc, num3, num4, num5, (short)num10, color, drawProperty);
							num6 = (short)(num6 - (short)(90 - num5));
							num5 = 90;
						}
					}
				}
				else
				{
					for (short num11 = startAngle; num11 != num2; num11 = (short)(num11 + 1))
					{
						short x2 = (short)(x + (width >> 1) + ((width >> 1) * FlMath.Cos1024(num11) >> 10));
						short y2 = (short)(y + (height >> 1) - ((height >> 1) * FlMath.Sin1024(num11) >> 10));
						DrawPixel(x2, y2, color);
					}
				}
			}
		}

		public virtual void GenericDrawCircle(short centerX, short centerY, short radius, bool solid, Color888 color, int drawProperty)
		{
			int num = 1 - radius;
			int num2 = 1;
			int num3 = -2 * radius;
			int num4 = 0;
			int num5 = radius;
			if (solid)
			{
				DrawLine((short)(centerX - radius), centerY, (short)(centerX + radius), centerY, color, drawProperty);
			}
			else
			{
				DrawPixel((short)(centerX + radius), centerY, color);
				DrawPixel((short)(centerX - radius), centerY, color);
			}
			DrawPixel(centerX, (short)(centerY + radius), color);
			DrawPixel(centerX, (short)(centerY - radius), color);
			while (num4 < num5)
			{
				if (num >= 0)
				{
					num5--;
					num3 += 2;
					num += num3;
				}
				num4++;
				num2 += 2;
				num += num2;
				if (solid)
				{
					DrawLine((short)(centerX - num4), (short)(centerY + num5), (short)(centerX + num4), (short)(centerY + num5), color, drawProperty);
					DrawLine((short)(centerX - num4), (short)(centerY - num5), (short)(centerX + num4), (short)(centerY - num5), color, drawProperty);
					DrawLine((short)(centerX - num5), (short)(centerY + num4), (short)(centerX + num5), (short)(centerY + num4), color, drawProperty);
					DrawLine((short)(centerX - num5), (short)(centerY - num4), (short)(centerX + num5), (short)(centerY - num4), color, drawProperty);
					continue;
				}
				DrawPixel((short)(centerX + num4), (short)(centerY + num5), color);
				DrawPixel((short)(centerX - num4), (short)(centerY + num5), color);
				DrawPixel((short)(centerX + num4), (short)(centerY - num5), color);
				DrawPixel((short)(centerX - num4), (short)(centerY - num5), color);
				DrawPixel((short)(centerX + num5), (short)(centerY + num4), color);
				DrawPixel((short)(centerX - num5), (short)(centerY + num4), color);
				DrawPixel((short)(centerX + num5), (short)(centerY - num4), color);
				DrawPixel((short)(centerX - num5), (short)(centerY - num4), color);
			}
		}

		public virtual void GenericDrawTriangle(short x0, short y0, short x1, short y1, short x2, short y2, bool solid, Color888 color, int drawProperty)
		{
			DrawLine(x0, y0, x1, y1, color, drawProperty);
			DrawLine(x2, y2, x1, y1, color, drawProperty);
			DrawLine(x0, y0, x2, y2, color, drawProperty);
			if (solid)
			{
				x0 = (short)(x0 + mCumulativeOffsetX);
				y0 = (short)(y0 + mCumulativeOffsetY);
				x1 = (short)(x1 + mCumulativeOffsetX);
				y1 = (short)(y1 + mCumulativeOffsetY);
				x2 = (short)(x2 + mCumulativeOffsetX);
				y2 = (short)(y2 + mCumulativeOffsetY);
				GenericFillAbsoluteTriangle(x0, y0, x1, y1, x2, y2, color, drawProperty);
			}
		}

		public virtual void GenericFillAbsoluteTriangle(short x0, short y0, short x1, short y1, short x2, short y2, Color888 color, int drawProperty)
		{
			short num = 0;
			if (y1 < y0)
			{
				num = x1;
				x1 = x0;
				x0 = num;
				num = y1;
				y1 = y0;
				y0 = num;
			}
			if (y2 < y0)
			{
				num = x2;
				x2 = x0;
				x0 = num;
				num = y2;
				y2 = y0;
				y0 = num;
			}
			if (y2 < y1)
			{
				num = x2;
				x2 = x1;
				x1 = num;
				num = y2;
				y2 = y1;
				y1 = num;
			}
			int num2 = 8;
			int num3 = x0 << num2;
			int num4 = y0 << num2;
			int num5 = x1 << num2;
			int num6 = y1 << num2;
			int num7 = x2 << num2;
			int num8 = y2 << num2;
			int num9 = 1073741824;
			int num10 = ((num6 > num4) ? ((num5 - num3 << num2) / (num6 - num4)) : num9);
			int num11 = ((num8 > num4) ? ((num7 - num3 << num2) / (num8 - num4)) : num9);
			int num12 = ((num8 > num6) ? ((num7 - num5 << num2) / (num8 - num6)) : num9);
			int num13 = num3;
			int num14 = num4;
			int num15 = num3;
			if (num10 == num9)
			{
				num15 = num5;
			}
			int num16 = num4;
			if (num10 > num11)
			{
				while (num14 < num6)
				{
					DrawAbsoluteLine((short)(num13 >> num2), (short)(num14 >> num2), (short)(num15 >> num2), (short)(num14 >> num2), color, drawProperty);
					num14 += 1 << num2;
					num16 += 1 << num2;
					num13 += num11;
					num15 += num10;
				}
				num15 = num5;
				num16 = num6;
				while (num14 < num8)
				{
					DrawAbsoluteLine((short)(num13 >> num2), (short)(num14 >> num2), (short)(num15 >> num2), (short)(num14 >> num2), color, drawProperty);
					num14 += 1 << num2;
					num16 += 1 << num2;
					num13 += num11;
					num15 += num12;
				}
			}
			else
			{
				while (num14 < num6)
				{
					DrawAbsoluteLine((short)(num13 >> num2), (short)(num14 >> num2), (short)(num15 >> num2), (short)(num14 >> num2), color, drawProperty);
					num14 += 1 << num2;
					num16 += 1 << num2;
					num13 += num10;
					num15 += num11;
				}
				while (num14 < num8)
				{
					DrawAbsoluteLine((short)(num13 >> num2), (short)(num14 >> num2), (short)(num15 >> num2), (short)(num14 >> num2), color, drawProperty);
					num14 += 1 << num2;
					num16 += 1 << num2;
					num13 += num12;
					num15 += num11;
				}
			}
		}

		public virtual void GenericDrawRoundRectangle(short rect_left, short rect_top, short rect_width, short rect_height, short arcWidth, short arcHeight, bool solid, Color888 color, int drawProperty)
		{
			short num = (short)(rect_left + rect_width - 1);
			short num2 = (short)(rect_top + rect_height - 1);
			if (arcWidth > rect_width)
			{
				arcWidth = rect_width;
			}
			if (arcHeight > rect_height)
			{
				arcHeight = rect_height;
			}
			short num3 = (short)(arcWidth >> 1);
			short num4 = (short)(arcHeight >> 1);
			short num5 = (short)(rect_left + num3);
			short num6 = (short)(num - num3);
			short num7 = (short)(rect_top + num4);
			short num8 = (short)(num2 - num4);
			DrawArc(rect_left, rect_top, arcWidth, arcHeight, 90, 90, solid, color, drawProperty);
			DrawArc((short)(num - arcWidth), rect_top, arcWidth, arcHeight, 0, 90, solid, color, drawProperty);
			DrawArc(rect_left, (short)(num2 - arcHeight), arcWidth, arcHeight, 180, 90, solid, color, drawProperty);
			DrawArc((short)(num - arcWidth), (short)(num2 - arcHeight), arcWidth, arcHeight, 270, 90, solid, color, drawProperty);
			if (solid)
			{
				int red = color.GetRed();
				int green = color.GetGreen();
				int blue = color.GetBlue();
				short num9 = (short)(num6 - num5 + 1);
				short num10 = (short)(num8 - num7 + 1);
				if (num9 > 0)
				{
					DrawRectangle(num5, rect_top, num9, num4, true, red, green, blue, drawProperty);
					DrawRectangle(num5, (short)(num8 + 1), num9, num4, true, red, green, blue, drawProperty);
				}
				if (num10 > 0)
				{
					DrawRectangle(rect_left, num7, rect_width, num10, true, red, green, blue, drawProperty);
				}
			}
			else
			{
				if (num5 < num6)
				{
					DrawLine(num5, rect_top, num6, rect_top, color, drawProperty);
					DrawLine(num5, num2, num6, num2, color, drawProperty);
				}
				if (num7 < num8)
				{
					DrawLine(rect_left, num7, rect_left, num8, color, drawProperty);
					DrawLine(num, num7, num, num8, color, drawProperty);
				}
			}
		}

		public static DisplayContext CreateContext(VideoMode mode)
		{
			return new MIDPDisplayContextImp(mode);
		}

		public static DisplayContext CreateContext(FlBitmap dstBitmap)
		{
			return new MIDPDisplayContextImp(dstBitmap);
		}

		public virtual void AddDirtyRect(short newRect_left, short newRect_top, short newRect_width, short newRect_height)
		{
			if (newRect_width <= 0 || newRect_height <= 0)
			{
				return;
			}
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			bool flag = false;
			short num5 = 0;
			short num6 = 0;
			short num7 = 0;
			short num8 = 0;
			int num9 = 2000000;
			int num10 = 0;
			int num11 = 1;
			int num12 = newRect_width * newRect_height;
			int[] array = mDirtyRectsAreas;
			int num13 = mSlotsUsed;
			for (int i = 0; i < num13; i++)
			{
				short num14 = mDirtyRects_left[i];
				short num15 = mDirtyRects_top[i];
				short num16 = mDirtyRects_width[i];
				short num17 = mDirtyRects_height[i];
				num = FlMath.Minimum(num14, newRect_left);
				num2 = FlMath.Minimum(num15, newRect_top);
				num3 = (short)(FlMath.Maximum((short)(num14 + num16), (short)(newRect_left + newRect_width)) - num);
				num4 = (short)(FlMath.Maximum((short)(num15 + num17), (short)(newRect_top + newRect_height)) - num2);
				int num18 = num3 * num4;
				if (num18 - (mDirtyRectsAreas[i] + num12) <= mDirtyRectMergePixelThreshold)
				{
					mDirtyRects_left[i] = num;
					mDirtyRects_top[i] = num2;
					mDirtyRects_width[i] = num3;
					mDirtyRects_height[i] = num4;
					array[i] = num18;
					return;
				}
				if (num18 < num9)
				{
					num9 = num18;
					num5 = num;
					num6 = num2;
					num7 = num3;
					num8 = num4;
					num10 = i;
				}
			}
			if (num13 < 3)
			{
				mDirtyRects_left[num13] = newRect_left;
				mDirtyRects_top[num13] = newRect_top;
				mDirtyRects_width[num13] = newRect_width;
				mDirtyRects_height[num13] = newRect_height;
				array[num13] = newRect_width * newRect_height;
				mSlotsUsed++;
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = i + 1; j < 3; j++)
				{
					short num19 = mDirtyRects_left[i];
					short num20 = mDirtyRects_top[i];
					short num21 = mDirtyRects_width[i];
					short num22 = mDirtyRects_height[i];
					short num23 = mDirtyRects_left[j];
					short num24 = mDirtyRects_top[j];
					short num25 = mDirtyRects_width[j];
					short num26 = mDirtyRects_height[j];
					num = FlMath.Minimum(num19, num23);
					num2 = FlMath.Minimum(num20, num24);
					num3 = (short)(FlMath.Maximum((short)(num19 + num21), (short)(num23 + num25)) - num);
					num4 = (short)(FlMath.Maximum((short)(num20 + num22), (short)(num24 + num26)) - num2);
					int num18 = num3 * num4;
					if (num18 < num9)
					{
						flag = true;
						num9 = num18;
						num5 = num;
						num6 = num2;
						num7 = num3;
						num8 = num4;
						num10 = i;
						num11 = j;
					}
				}
			}
			mDirtyRects_left[num10] = num5;
			mDirtyRects_top[num10] = num6;
			mDirtyRects_width[num10] = num7;
			mDirtyRects_height[num10] = num8;
			array[num10] = num9;
			if (flag)
			{
				mDirtyRects_left[num11] = newRect_left;
				mDirtyRects_top[num11] = newRect_top;
				mDirtyRects_width[num11] = newRect_width;
				mDirtyRects_height[num11] = newRect_height;
				array[num11] = newRect_width * newRect_height;
			}
		}

		public virtual void AddDirtyComponent(Component dirtyComponent)
		{
			short rectLeft = dirtyComponent.GetRectLeft();
			short rectTop = dirtyComponent.GetRectTop();
			short rectWidth = dirtyComponent.GetRectWidth();
			short rectHeight = dirtyComponent.GetRectHeight();
			Viewport viewport = dirtyComponent.GetViewport();
			if (viewport != null)
			{
				Component component = dirtyComponent;
				short num = 0;
				short num2 = 0;
				while (viewport != null)
				{
					if (!viewport.IsVisible())
					{
						return;
					}
					num = (short)(num + viewport.GetRectLeft() - viewport.GetOffsetX());
					num2 = (short)(num2 + viewport.GetRectTop() - viewport.GetOffsetY());
					component = viewport;
					viewport = viewport.GetViewport();
				}
				if (FlApplication.GetInstance() == component)
				{
					rectLeft = (short)(rectLeft + num);
					rectTop = (short)(rectTop + num2);
					short num3 = (short)(num + dirtyComponent.GetViewport().GetOffsetX());
					short num4 = (short)(num2 + dirtyComponent.GetViewport().GetOffsetY());
					short rectWidth2 = dirtyComponent.GetViewport().GetRectWidth();
					short rectHeight2 = dirtyComponent.GetViewport().GetRectHeight();
					int num5 = num3 + rectWidth2;
					int num6 = rectLeft + rectWidth;
					int num7 = num4 + rectHeight2;
					int num8 = rectTop + rectHeight;
					if (num3 > rectLeft)
					{
						rectLeft = num3;
					}
					if (num4 > rectTop)
					{
						rectTop = num4;
					}
					if (num5 < num6)
					{
						num6 = num5;
					}
					if (num7 < num8)
					{
						num8 = num7;
					}
					rectWidth = (short)(num6 - rectLeft);
					rectHeight = (short)(num8 - rectTop);
					AddDirtyRect(rectLeft, rectTop, rectWidth, rectHeight);
				}
			}
			else if (dirtyComponent == FlApplication.GetInstance())
			{
				AddDirtyRect(rectLeft, rectTop, rectWidth, rectHeight);
			}
		}

		public virtual bool HasDirtyRects()
		{
			return mSlotsUsed > 0;
		}

		public virtual void ClearDirtyRects()
		{
			mSlotsUsed = 0;
		}

		public virtual bool SetNextDirtyRect()
		{
			if (mDirtyRectIndex >= mSlotsUsed)
			{
				return false;
			}
			int num = mDirtyRectIndex;
			short num2 = mDirtyRects_left[num];
			short num3 = mDirtyRects_top[num];
			short num4 = mDirtyRects_width[num];
			short num5 = mDirtyRects_height[num];
			int num6 = num2 + num4;
			int num7 = mClipRect_left + mClipRect_width;
			int num8 = num3 + num5;
			int num9 = mClipRect_top + mClipRect_height;
			if (num2 > mClipRect_left)
			{
				mClipRect_left = num2;
			}
			if (num3 > mClipRect_top)
			{
				mClipRect_top = num3;
			}
			if (num6 < num7)
			{
				num7 = num6;
			}
			if (num8 < num9)
			{
				num9 = num8;
			}
			mClipRect_width = (short)(num7 - mClipRect_left);
			mClipRect_height = (short)(num9 - mClipRect_top);
			mDirtyRectIndex++;
			return true;
		}

		public virtual void SetDirtyRectsEnable(bool enable)
		{
			mDirtyRectsEnabled = enable;
		}

		public virtual bool IsDirtyRectsEnabled()
		{
			return mDirtyRectsEnabled;
		}

		public virtual void FillArcImpl(short xc, short yc, short a, short b, short startAngle, short endAngle, Color888 color, int drawProperty)
		{
			short num = (short)(a * a);
			short num2 = (short)(b * b);
			short num3 = (short)(xc + (a * FlMath.Cos1024(startAngle) >> 10));
			short num4 = (short)(yc - (b * FlMath.Sin1024(startAngle) >> 10));
			short num5 = (short)(xc + (a * FlMath.Cos1024(endAngle) >> 10));
			short num6 = (short)(yc - (b * FlMath.Sin1024(endAngle) >> 10));
			GenericDrawTriangle(xc, yc, num3, num4, num5, num6, true, color, drawProperty);
			int num7 = 0;
			if (startAngle >= 270)
			{
				num7 = 3;
			}
			else if (startAngle >= 180)
			{
				num7 = 2;
			}
			else if (startAngle >= 90)
			{
				num7 = 1;
			}
			int num8;
			int num11;
			int num10;
			int num9;
			switch (num7)
			{
			case 0:
				num8 = num5;
				num11 = num6;
				num10 = num3;
				num9 = num4;
				break;
			case 1:
				num8 = num5;
				num9 = num6;
				num10 = num3;
				num11 = num4;
				break;
			case 2:
				num8 = num3;
				num9 = num6;
				num10 = num5;
				num11 = num4;
				break;
			default:
				num8 = num3;
				num9 = num4;
				num10 = num5;
				num11 = num6;
				break;
			}
			if (num8 < 0)
			{
				num8 = 0;
			}
			if (num8 < mClipRect_left)
			{
				num8 = mClipRect_left;
			}
			if (num9 > mClipRect_top + mClipRect_height - 1)
			{
				num9 = mClipRect_top + mClipRect_height - 1;
			}
			if (num10 > mClipRect_left + mClipRect_width - 1)
			{
				num10 = mClipRect_left + mClipRect_width - 1;
			}
			if (num11 < 0)
			{
				num11 = 0;
			}
			if (num11 < mClipRect_top)
			{
				num11 = mClipRect_top;
			}
			switch (num7)
			{
			case 1:
			{
				for (int num16 = num9; num16 >= num11; num16--)
				{
					int num17 = num8;
					bool flag4 = false;
					while (true)
					{
						if (!flag4)
						{
							if ((num17 - xc) * (num17 - xc) * num2 + (num16 - yc) * (num16 - yc) * num <= num2 * num)
							{
								flag4 = true;
							}
							else
							{
								num8++;
							}
						}
						if (flag4)
						{
							DrawPixel((short)num17, (short)num16, color);
						}
						if (num17 == num10 || (num3 - num5) * (num16 - num6) > (num17 - num5) * (num4 - num6))
						{
							break;
						}
						num17++;
					}
				}
				break;
			}
			case 0:
			{
				for (int num13 = num9; num13 >= num11; num13--)
				{
					int num14 = num10;
					bool flag2 = false;
					while (true)
					{
						if (!flag2)
						{
							if ((num14 - xc) * (num14 - xc) * num2 + (num13 - yc) * (num13 - yc) * num <= num2 * num)
							{
								flag2 = true;
							}
							else
							{
								num10--;
							}
						}
						if (flag2)
						{
							DrawPixel((short)num14, (short)num13, color);
						}
						if (num14 == num8 || (num3 - num5) * (num13 - num6) > (num14 - num5) * (num4 - num6))
						{
							break;
						}
						num14--;
					}
				}
				break;
			}
			case 2:
			{
				for (int j = num11; j <= num9; j++)
				{
					int num15 = num8;
					bool flag3 = false;
					while (true)
					{
						if (!flag3)
						{
							if ((num15 - xc) * (num15 - xc) * num2 + (j - yc) * (j - yc) * num <= num2 * num)
							{
								flag3 = true;
							}
							else
							{
								num8++;
							}
						}
						if (flag3)
						{
							DrawPixel((short)num15, (short)j, color);
						}
						if (num15 == num10 || (num3 - num5) * (j - num6) > (num15 - num5) * (num4 - num6))
						{
							break;
						}
						num15++;
					}
				}
				break;
			}
			case 3:
			{
				for (int i = num11; i <= num9; i++)
				{
					int num12 = num10;
					bool flag = false;
					while (true)
					{
						if (!flag)
						{
							if ((num12 - xc) * (num12 - xc) * num2 + (i - yc) * (i - yc) * num <= num2 * num)
							{
								flag = true;
							}
							else
							{
								num10--;
							}
						}
						if (flag)
						{
							DrawPixel((short)num12, (short)i, color);
						}
						if (num12 == num8 || (num3 - num5) * (i - num6) > (num12 - num5) * (num4 - num6))
						{
							break;
						}
						num12--;
					}
				}
				break;
			}
			}
		}

		public virtual void DrawString(FlString @string, short rect_left, short rect_top, short rect_width, short rect_height, FlFont font, sbyte alignment, int startIndex, int nChars, short lineWidth)
		{
			DrawString(@string, rect_left, rect_top, rect_width, rect_height, font, alignment, startIndex, nChars, lineWidth, 255);
		}

		public virtual void DrawMultilineString(FlString @string, int[] lines, int totalLineCount, short rect_left, short rect_top, short rect_width, short rect_height, int topLine, FlFont font, sbyte alignment, short[] linesWidth)
		{
			DrawMultilineString(@string, lines, totalLineCount, rect_left, rect_top, rect_width, rect_height, topLine, font, alignment, linesWidth, 255);
		}

		public virtual void DrawRectangle(short rect_left, short rect_top, short rect_width, short rect_height, bool solid, int colorRed, int colorGreen, int colorBlue)
		{
			DrawRectangle(rect_left, rect_top, rect_width, rect_height, solid, colorRed, colorGreen, colorBlue, 255);
		}

		public virtual void DrawRoundRectangle(short rect_left, short rect_top, short rect_width, short rect_height, short arcWidth, short arcHeight, bool solid, Color888 color)
		{
			DrawRoundRectangle(rect_left, rect_top, rect_width, rect_height, arcWidth, arcHeight, solid, color, 255);
		}

		public virtual void DrawFrame(short rect_left, short rect_top, short rect_width, short rect_height, Color888 color)
		{
			DrawFrame(rect_left, rect_top, rect_width, rect_height, color, 255);
		}

		public virtual void DrawAbsoluteSolidRectangle(short rect_left, short rect_top, short rect_width, short rect_height, int colorRed, int colorGreen, int colorBlue)
		{
			DrawAbsoluteSolidRectangle(rect_left, rect_top, rect_width, rect_height, colorRed, colorGreen, colorBlue, 255);
		}

		public virtual void DrawAbsoluteLine(short fromX, short fromY, short toX, short toY, Color888 color)
		{
			DrawAbsoluteLine(fromX, fromY, toX, toY, color, 255);
		}

		public virtual void DrawLine(short fromX, short fromY, short toX, short toY, Color888 color)
		{
			DrawLine(fromX, fromY, toX, toY, color, 255);
		}

		public virtual void DrawPixel(short x, short y, Color888 color)
		{
			DrawPixel(x, y, color, 255);
		}

		public virtual void DrawTiledBitmapSection(FlBitmap bitmap, short publicSourceWidth, short publicSourceHeight, short sourceOffsetX, short sourceOffsetY, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, short destRect_left, short destRect_top, short destRect_width, short destRect_height)
		{
			DrawTiledBitmapSection(bitmap, publicSourceWidth, publicSourceHeight, sourceOffsetX, sourceOffsetY, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, destRect_left, destRect_top, destRect_width, destRect_height, 255);
		}

		public virtual void DrawTiledBitmapSection(FlBitmap bitmap, short sourceRect_left, short sourceRect_top, short sourceRect_width, short sourceRect_height, short destRect_left, short destRect_top, short destRect_width, short destRect_height)
		{
			DrawTiledBitmapSection(bitmap, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, destRect_left, destRect_top, destRect_width, destRect_height, 255);
		}

		public virtual void GenericFillAbsoluteTriangle(short x0, short y0, short x1, short y1, short x2, short y2, Color888 color)
		{
			GenericFillAbsoluteTriangle(x0, y0, x1, y1, x2, y2, color, 255);
		}
	}
}
