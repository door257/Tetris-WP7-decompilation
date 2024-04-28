using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ca.jamdat.flight
{
	public class MIDPDisplayContextImp : DisplayContext
	{
		private SpriteBatch mGraphics;

		private Color mColor;

		private Texture2D mRect;

		public MIDPDisplayContextImp(VideoMode mode)
			: base(mode)
		{
			mColor = Color.White;
		}

		public MIDPDisplayContextImp(FlBitmap dstBitmap)
			: base(new VideoMode(dstBitmap.GetWidth(), dstBitmap.GetHeight(), FlPixelFormat.GetColorBitsPerPixel(dstBitmap.GetPixelFormat())))
		{
		}

		public override void NativeDrawArc(int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
		}

		public override void NativeDrawLine(int x1, int y1, int x2, int y2)
		{
		}

		public override void NativeDrawPixel(int x, int y)
		{
			NativeFillRect(x, y, 1, 1);
		}

		public override void NativeDrawRect(int x, int y, int width, int height)
		{
		}

		public override void NativeDrawRGB(int[] pixels, int offset, int desLeft, int desTop, int width, int height, int scanlength, bool processAlpha)
		{
		}

		public override void NativeDrawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
		{
		}

		public override void NativeFillArc(int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
		}

		public override void NativeFillRect(int x, int y, int width, int height)
		{
			try
			{
				FrameworkGlobals instance = FrameworkGlobals.GetInstance();
				if (width <= 0 || height <= 0 || instance == null || instance.GraphicsDeviceManager == null || instance.GraphicsDeviceManager.GraphicsDevice == null)
				{
					return;
				}
				if (mRect == null)
				{
					mRect = new Texture2D(instance.GraphicsDeviceManager.GraphicsDevice, width, height);
					Color[] array = new Color[width * height];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Color.White;
					}
					mRect.SetData(array);
				}
				mGraphics.Draw(mRect, new Vector2(x, y), new Rectangle(0, 0, width, height), mColor);
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public override void NativeFillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
		{
		}

		public void NativeFillTriangle(int x0, int y0, int x1, int y1, int x2, int y2)
		{
		}

		public override int NativeGetClipHeight()
		{
			return 0;
		}

		public override int NativeGetClipWidth()
		{
			return 0;
		}

		public override int NativeGetClipX()
		{
			return 0;
		}

		public override int NativeGetClipY()
		{
			return 0;
		}

		public override void NativeSetClip(int x, int y, int width, int height)
		{
		}

		public override int NativeGetAlpha()
		{
			return 255;
		}

		public override Color NativeGetARGB()
		{
			return mColor;
		}

		public override Color NativeGetRGB()
		{
			return mColor;
		}

		public override void NativeSetAlpha(int a)
		{
		}

		public override void NativeSetARGB(Color argb)
		{
			mColor = argb;
		}

		public override void NativeSetRGB(Color rgb)
		{
			mColor = rgb;
		}

		public override void NativeSetARGB(int a, int r, int g, int b)
		{
			Color color = new Color(r, g, b, a);
			mColor = color;
		}

		public override void NativeSetRGB(int r, int g, int b)
		{
			Color color = new Color(r, g, b);
			mColor = color;
		}

		public override void NativeSetFont(object font)
		{
		}

		public void NativeDrawString(string str, int x, int y, int nativeAlign)
		{
		}

		public override void NativeDrawString(string str, int x, int y, sbyte flightAlignH, sbyte flightAlignV)
		{
			NativeDrawString(str, x, y, TranslateAnchors(flightAlignH, flightAlignV));
		}

		public override int NativeGetTranslatedX()
		{
			return 0;
		}

		public override int NativeGetTranslatedY()
		{
			return 0;
		}

		public override void NativeTranslate(int x, int y)
		{
		}

		public void NativeDrawRegion(Texture2D image, int srcLeft, int srcTop, int width, int height, int transform, int desLeft, int desTop)
		{
			try
			{
				if (image != null && image.Width > 0 && image.Height > 0)
				{
					mColor = new Color(255, 255, 255, 255);
					Rectangle value = new Rectangle(srcLeft, srcTop, width, height);
					Rectangle destinationRectangle = new Rectangle(desLeft, desTop, width, height);
					mGraphics.Draw(image, destinationRectangle, value, mColor);
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void SetGraphics(SpriteBatch g)
		{
			mGraphics = g;
		}

		private void SetClip(int x, int y, int width, int height)
		{
			NativeSetClip(x, y, width, height);
		}

		public override void Clear(Color888 color)
		{
			Color a = NativeGetARGB();
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			NativeSetARGB(255, color.GetRed(), color.GetGreen(), color.GetBlue());
			NativeFillRect(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			NativeSetARGB(a);
		}

		public override void ClearFullscreen(Color888 color)
		{
			FrameworkGlobals.GetInstance().GraphicsDeviceManager.GraphicsDevice.Clear(new Color(color.GetRed(), color.GetGreen(), color.GetBlue(), 255));
		}

		public override void ResetClip()
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
		}

		public override void DrawAbsoluteBitmapBypassClipping(FlBitmap bitmap, int desLeft, int desTop)
		{
		}

		public override void DrawAbsoluteBitmapSection(FlBitmap bitmap, int desLeft, int desTop, int srcLeft, int srcTop, int width, int height, int drawProperty)
		{
			SetClip(desLeft, desTop, width, height);
			NativeSetAlpha(FlDrawPropertyUtil.GetAlpha(drawProperty));
			Texture2D image = bitmap.getImage();
			NativeDrawRegion(image, srcLeft, srcTop, width, height, 0, desLeft, desTop);
		}

		public override void DrawAbsoluteBitmapSection(FlBitmap bitmap, int desLeft, int desTop, int srcLeft, int srcTop, int width, int height, sbyte transform)
		{
			DrawAbsoluteBitmapSection(bitmap, desLeft, desTop, srcLeft, srcTop, width, height, 0);
		}

		public override void DrawRGB(int[] pixels, int offset, int desLeft, int desTop, int width, int height, int scanlength, bool processAlpha)
		{
			desLeft += mCumulativeOffsetX;
			desTop += mCumulativeOffsetY;
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			NativeDrawRGB(pixels, offset, desLeft, desTop, width, height, scanlength, processAlpha);
		}

		public override void DrawArc(short x, short y, short width, short height, short startAngle, short sweepAngle, bool solid, Color888 color, int drawProperty)
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			x = (short)(x + mCumulativeOffsetX);
			y = (short)(y + mCumulativeOffsetY);
			NativeSetARGB(FlDrawPropertyUtil.GetAlpha(drawProperty), color.GetRed(), color.GetGreen(), color.GetBlue());
			if (solid)
			{
				NativeFillArc(x, y, width, height, startAngle, sweepAngle);
			}
			else
			{
				NativeDrawArc(x, y, width, height, startAngle, sweepAngle);
			}
		}

		public override void DrawLine(short startX, short startY, short endX, short endY, Color888 color, int drawProperty)
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			startX = (short)(startX + mCumulativeOffsetX);
			startY = (short)(startY + mCumulativeOffsetY);
			endX = (short)(endX + mCumulativeOffsetX);
			endY = (short)(endY + mCumulativeOffsetY);
			NativeSetARGB(FlDrawPropertyUtil.GetAlpha(drawProperty), color.GetRed(), color.GetGreen(), color.GetBlue());
			NativeDrawLine(startX, startY, endX, endY);
		}

		public override void DrawPixel(short x, short y, Color888 color, int drawProperty)
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			x = (short)(x + mCumulativeOffsetX);
			y = (short)(y + mCumulativeOffsetY);
			NativeSetARGB(FlDrawPropertyUtil.GetAlpha(drawProperty), color.GetRed(), color.GetGreen(), color.GetBlue());
			NativeDrawPixel(x, y);
		}

		public override void DrawRectangle(short rect_left, short rect_top, short rect_width, short rect_height, bool solid, int colorRed, int colorGreen, int colorBlue, int drawProperty)
		{
			rect_left = (short)(rect_left + mCumulativeOffsetX);
			rect_top = (short)(rect_top + mCumulativeOffsetY);
			NativeSetARGB(FlDrawPropertyUtil.GetAlpha(drawProperty), colorRed, colorGreen, colorBlue);
			if (!solid)
			{
				if (mClipRect_top <= rect_top)
				{
					FillRectangle(rect_left, rect_top, rect_width, 1);
				}
				if (mClipRect_left <= rect_left)
				{
					FillRectangle(rect_left, rect_top, 1, rect_height);
				}
				if (mClipRect_top + mClipRect_height >= rect_top + rect_height)
				{
					FillRectangle(rect_left, (short)(rect_top + rect_height - 1), rect_width, 1);
				}
				if (mClipRect_left + mClipRect_width >= rect_left + rect_width)
				{
					FillRectangle((short)(rect_left + rect_width - 1), rect_top, 1, rect_height);
				}
			}
			else
			{
				FillRectangle(rect_left, rect_top, rect_width, rect_height);
			}
		}

		public override void DrawRoundRectangle(short rect_left, short rect_top, short rect_width, short rect_height, short arcWidth, short arcHeight, bool solid, Color888 color, int drawProperty)
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			rect_left = (short)(rect_left + mCumulativeOffsetX);
			rect_top = (short)(rect_top + mCumulativeOffsetY);
			NativeSetARGB(FlDrawPropertyUtil.GetAlpha(drawProperty), color.GetRed(), color.GetGreen(), color.GetBlue());
			if (solid)
			{
				NativeFillRoundRect(rect_left, rect_top, rect_width, rect_height, arcWidth, arcHeight);
			}
			else
			{
				NativeDrawRoundRect(rect_left, rect_top, rect_width, rect_height, arcWidth, arcHeight);
			}
		}

		public override void DrawTriangle(short x1, short y1, short x2, short y2, short x3, short y3, bool solid, Color888 color, int drawProperty)
		{
			SetClip(mClipRect_left, mClipRect_top, mClipRect_width, mClipRect_height);
			base.GenericDrawTriangle(x1, y1, x2, y2, x3, y3, solid, color, drawProperty);
		}

		private void FillRectangle(short clippedRect_left, short clippedRect_top, short clippedRect_width, short clippedRect_height)
		{
			int num = mClipRect_left + mClipRect_width;
			int num2 = clippedRect_left + clippedRect_width;
			int num3 = mClipRect_top + mClipRect_height;
			int num4 = clippedRect_top + clippedRect_height;
			if (mClipRect_left > clippedRect_left)
			{
				clippedRect_left = mClipRect_left;
			}
			if (mClipRect_top > clippedRect_top)
			{
				clippedRect_top = mClipRect_top;
			}
			if (num < num2)
			{
				num2 = num;
			}
			if (num3 < num4)
			{
				num4 = num3;
			}
			clippedRect_width = (short)(num2 - clippedRect_left);
			clippedRect_height = (short)(num4 - clippedRect_top);
			SetClip(clippedRect_left, clippedRect_top, clippedRect_width, clippedRect_height);
			NativeFillRect(clippedRect_left, clippedRect_top, clippedRect_width, clippedRect_height);
		}

		public override sbyte GetDisplayAPI()
		{
			return 0;
		}

		private int TranslateAnchors(int alignmentH, int alignmentV)
		{
			int result = 0;
			switch (alignmentH)
			{
			default:
				switch (alignmentV)
				{
				default:
					return result;
				}
			}
		}
	}
}
