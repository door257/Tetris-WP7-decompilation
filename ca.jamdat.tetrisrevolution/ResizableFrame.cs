using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ResizableFrame : Viewport
	{
		public const int verticalThreeTiles = 0;

		public const int horizontalThreeTiles = 1;

		public const int horizontalTwoTiles = 2;

		public const int eightTiles = 3;

		public const int eightTilesWithBackgroundColor = 4;

		public const int nineTiles = 5;

		public const int twelveTiles = 6;

		public const int commonFrame = 7;

		public const int topLeftTileFor8 = 0;

		public const int topCenterTileFor8 = 1;

		public const int topRightTileFor8 = 2;

		public const int centerLeftTileFor8 = 3;

		public const int centerRightTileFor8 = 4;

		public const int bottomLeftTileFor8 = 5;

		public const int bottomCenterTileFor8 = 6;

		public const int bottomRightTileFor8 = 7;

		public const int middleTileFor9 = 8;

		public const int topLeftTileFor12 = 0;

		public const int topCenterTileFor12 = 1;

		public const int topRightTileFor12 = 2;

		public const int centerTopLeftTileFor12 = 3;

		public const int centerTopRightTileFor12 = 4;

		public const int centerLeftTileFor12 = 5;

		public const int centerRightTileFor12 = 6;

		public const int centerBottomLeftTileFor12 = 7;

		public const int centerBottomRightTileFor12 = 8;

		public const int bottomLeftTileFor12 = 9;

		public const int bottomCenterTileFor12 = 10;

		public const int bottomRightTileFor12 = 11;

		public int mType;

		public FlBitmapMap mTiles;

		public int mFirstTileId;

		public int mSecondTileId;

		public int mThirdTileId;

		public Color888 mBackgroundColor;

		public FlBitmap mBackgroundGridTile;

		public FlBitmap mBackgroundGradientTile;

		public short mMaxFrameHeight;

		public short mMaxScrollerHeight;

		public Scroller mScroller;

		public short mRectToPaintX;

		public short mRectToPaintY;

		public short mRectToPaintW;

		public short mRectToPaintH;

		public ResizableFrame(int type, FlBitmapMap tiles, int firstTileId, int secondTileId, int thirdTileId)
		{
			mType = type;
			mTiles = tiles;
			mFirstTileId = firstTileId;
			mSecondTileId = secondTileId;
			mThirdTileId = thirdTileId;
		}

		public ResizableFrame(int type, FlBitmapMap tiles, int firstTileId, int secondTileId)
		{
			mType = type;
			mTiles = tiles;
			mFirstTileId = firstTileId;
			mSecondTileId = secondTileId;
		}

		public ResizableFrame(int type, FlBitmapMap tiles, Color888 backgroundColor)
		{
			mType = type;
			mTiles = tiles;
			mFirstTileId = -1;
			mSecondTileId = -1;
			mThirdTileId = -1;
			mBackgroundColor = backgroundColor;
		}

		public ResizableFrame(int type, FlBitmapMap tiles)
		{
			mType = type;
			mTiles = tiles;
			mFirstTileId = -1;
			mSecondTileId = -1;
			mThirdTileId = -1;
		}

		public ResizableFrame(int type, FlBitmapMap tiles, FlBitmap backgroundGridTile, FlBitmap backgroundGradientTile)
		{
			mType = type;
			mTiles = tiles;
			mFirstTileId = -1;
			mSecondTileId = -1;
			mThirdTileId = -1;
			mBackgroundGridTile = backgroundGridTile;
			mBackgroundGradientTile = backgroundGradientTile;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(Viewport childrenContainer)
		{
			CustomComponentUtilities.Attach(this, childrenContainer);
		}

		public virtual void Unload()
		{
			if (mScroller != null)
			{
				mScroller.SetSize(mScroller.GetRectWidth(), mMaxScrollerHeight);
				SetRect(GetRectLeft(), (short)((734 - mMaxFrameHeight) / 2), GetRectWidth(), mMaxFrameHeight);
				mMaxFrameHeight = 0;
				mMaxScrollerHeight = 0;
			}
			CustomComponentUtilities.Detach(this);
			if (mTiles != null)
			{
				mTiles = null;
			}
			if (mBackgroundColor != null)
			{
				mBackgroundColor = null;
			}
			if (mBackgroundGridTile != null)
			{
				mBackgroundGridTile = null;
			}
			if (mBackgroundGradientTile != null)
			{
				mBackgroundGradientTile = null;
			}
		}

		public virtual void Resize(Scroller scroller)
		{
			Viewport scrollerViewport = scroller.GetScrollerViewport();
			short rectTop = scrollerViewport.GetRectTop();
			mMaxFrameHeight = GetRectHeight();
			mScroller = scroller;
			mMaxScrollerHeight = scroller.GetRectHeight();
			int rectWidth = scroller.GetRectWidth();
			short num = (short)(VerticalScroller.GetLowestPosition(scroller) + rectTop);
			short num2 = (short)(mMaxScrollerHeight - num);
			short num3 = (short)(mMaxFrameHeight - num2);
			if (num3 < mMaxFrameHeight)
			{
				short num4 = num3;
				num3 = ((num4 <= mMaxFrameHeight) ? num4 : ((mMaxFrameHeight > num3) ? mMaxFrameHeight : num3));
				short num5 = ((mType == 7) ? ComputeCorrectCommonFrameHeight(num3) : num3);
				scrollerViewport.SetSize((short)rectWidth, (short)(num - rectTop));
				if (scroller.GetSubtype() == -17)
				{
					VerticalScroller.ResetBottomDividerPosition(scroller);
				}
				scroller.SetSize((short)rectWidth, num);
				Viewport viewport = (Viewport)GetChild(0);
				viewport.SetSize(mRect_width, (short)(viewport.GetRectHeight() - num2));
				SetRect(GetRectLeft(), (short)((mMaxFrameHeight - num5) / 2), mRect_width, num5);
			}
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			mRectToPaintX = GetRectLeft();
			mRectToPaintY = GetRectTop();
			mRectToPaintW = GetRectWidth();
			mRectToPaintH = GetRectHeight();
			short absoluteClippingRectLeft = displayContext.GetAbsoluteClippingRectLeft();
			short absoluteClippingRectTop = displayContext.GetAbsoluteClippingRectTop();
			short absoluteClippingRectWidth = displayContext.GetAbsoluteClippingRectWidth();
			short absoluteClippingRectHeight = displayContext.GetAbsoluteClippingRectHeight();
			short num = GetAbsoluteLeft();
			short num2 = GetAbsoluteTop();
			short num3 = mRectToPaintW;
			short num4 = mRectToPaintH;
			int num5 = absoluteClippingRectLeft + absoluteClippingRectWidth;
			int num6 = num + num3;
			int num7 = absoluteClippingRectTop + absoluteClippingRectHeight;
			int num8 = num2 + num4;
			if (absoluteClippingRectLeft > num)
			{
				num = absoluteClippingRectLeft;
			}
			if (absoluteClippingRectTop > num2)
			{
				num2 = absoluteClippingRectTop;
			}
			if (num5 < num6)
			{
				num6 = num5;
			}
			if (num7 < num8)
			{
				num8 = num7;
			}
			num3 = (short)(num6 - num);
			num4 = (short)(num8 - num2);
			if (num3 > 0 && num4 > 0)
			{
				switch (mType)
				{
				case 0:
					DrawVertical3Tiles(displayContext, mFirstTileId, mSecondTileId, mThirdTileId);
					break;
				case 1:
					DrawHorizontal3Tiles(displayContext, mFirstTileId, mSecondTileId, mThirdTileId);
					break;
				case 2:
					DrawHorizontal2Tiles(displayContext, mFirstTileId, mSecondTileId);
					break;
				case 3:
					Draw8Tiles(displayContext);
					break;
				case 4:
					Draw8Tiles(displayContext);
					DrawBackgroundColor(displayContext);
					break;
				case 5:
					Draw8Tiles(displayContext);
					DrawBackgroundTile(displayContext);
					break;
				case 6:
					Draw12Tiles(displayContext);
					break;
				case 7:
					Draw12Tiles(displayContext);
					DrawCommonFrameCenter(displayContext);
					break;
				}
				base.OnDraw(displayContext);
			}
		}

		public static ResizableFrame Create(Viewport childrenContainer)
		{
			ResizableFrame resizableFrame = null;
			switch (childrenContainer.GetSubtype())
			{
			case -10:
				resizableFrame = CreateCommonFrame(childrenContainer);
				break;
			case -11:
				resizableFrame = CreateCommonGibberishViewport(childrenContainer);
				break;
			case -12:
				resizableFrame = CreateCommonCursor(childrenContainer);
				break;
			case -14:
			case -13:
				resizableFrame = CreateCommonScrollbar(childrenContainer);
				break;
			}
			resizableFrame.Initialize(childrenContainer);
			return resizableFrame;
		}

		public virtual short ComputeCorrectCommonFrameHeight(short wantedHeight)
		{
			short height = mBackgroundGridTile.GetHeight();
			FlBitmapMap flBitmapMap = mTiles;
			short publicSizeYAt = flBitmapMap.GetPublicSizeYAt(1);
			short publicSizeYAt2 = flBitmapMap.GetPublicSizeYAt(10);
			short num = (short)(publicSizeYAt + publicSizeYAt2);
			short num2 = (short)((wantedHeight - num) / height * height + num);
			if (num2 == wantedHeight)
			{
				return wantedHeight;
			}
			return (short)(num2 + height);
		}

		public virtual void DrawHorizontal2Tiles(DisplayContext displayContext, int leftTileId, int rightTileId)
		{
			FlBitmapMap flBitmapMap = mTiles;
			short num = 0;
			short publicSizeXAt = flBitmapMap.GetPublicSizeXAt(rightTileId);
			short num2 = (short)(mRectToPaintW - (num + publicSizeXAt));
			short num3 = (short)(mRectToPaintX + num);
			short destLeft = (short)(num3 + num2);
			flBitmapMap.DrawElementAt(leftTileId, displayContext, num3, mRectToPaintY, num2, mRectToPaintH, false, false, true, false);
			flBitmapMap.DrawElementAt(rightTileId, displayContext, destLeft, mRectToPaintY, false, false);
		}

		public virtual void DrawHorizontal3Tiles(DisplayContext displayContext, int leftTileId, int centerTileId, int rightTileId)
		{
			FlBitmapMap flBitmapMap = mTiles;
			short publicSizeXAt = flBitmapMap.GetPublicSizeXAt(leftTileId);
			short publicSizeXAt2 = flBitmapMap.GetPublicSizeXAt(rightTileId);
			short num = (short)(mRectToPaintW - (publicSizeXAt + publicSizeXAt2));
			short num2 = (short)(mRectToPaintX + publicSizeXAt);
			short destLeft = (short)(num2 + num);
			flBitmapMap.DrawElementAt(leftTileId, displayContext, mRectToPaintX, mRectToPaintY, false, false);
			flBitmapMap.DrawElementAt(centerTileId, displayContext, num2, mRectToPaintY, num, mRectToPaintH, false, false, true, false);
			flBitmapMap.DrawElementAt(rightTileId, displayContext, destLeft, mRectToPaintY, false, false);
		}

		public virtual void DrawVertical3Tiles(DisplayContext displayContext, int topTileId, int centerTileId, int bottomTileId)
		{
			FlBitmapMap flBitmapMap = mTiles;
			short publicSizeYAt = flBitmapMap.GetPublicSizeYAt(topTileId);
			short publicSizeYAt2 = flBitmapMap.GetPublicSizeYAt(bottomTileId);
			short num = (short)(mRectToPaintH - (publicSizeYAt + publicSizeYAt2));
			short num2 = (short)(mRectToPaintY + publicSizeYAt);
			short destTop = (short)(num2 + num);
			flBitmapMap.DrawElementAt(bottomTileId, displayContext, mRectToPaintX, destTop, false, false);
			if (publicSizeYAt + publicSizeYAt2 < mRectToPaintH)
			{
				flBitmapMap.DrawElementAt(topTileId, displayContext, mRectToPaintX, mRectToPaintY, mRectToPaintW, (short)(mRectToPaintH / 2), false, false, false, false);
			}
			else
			{
				flBitmapMap.DrawElementAt(topTileId, displayContext, mRectToPaintX, mRectToPaintY, false, false);
			}
			if (num > 0)
			{
				flBitmapMap.DrawElementAt(centerTileId, displayContext, mRectToPaintX, num2, mRectToPaintW, num, false, false, false, true);
			}
		}

		public virtual void Draw8Tiles(DisplayContext displayContext)
		{
			short num = mRectToPaintX;
			short num2 = mRectToPaintY;
			short num3 = mRectToPaintW;
			short num4 = mRectToPaintH;
			FlBitmapMap flBitmapMap = mTiles;
			short publicSizeYAt = flBitmapMap.GetPublicSizeYAt(0);
			short publicSizeXAt = flBitmapMap.GetPublicSizeXAt(3);
			short publicSizeXAt2 = flBitmapMap.GetPublicSizeXAt(4);
			short publicSizeYAt2 = flBitmapMap.GetPublicSizeYAt(5);
			short destRect_top = (short)(num2 + publicSizeYAt);
			short destRect_height = (short)(num4 - publicSizeYAt - publicSizeYAt2);
			short destRect_left = (short)(num + num3 - publicSizeXAt2);
			short num5 = (short)(num2 + num4 - publicSizeYAt2);
			mRectToPaintX = num;
			mRectToPaintY = num2;
			mRectToPaintW = num3;
			mRectToPaintH = publicSizeYAt;
			DrawHorizontal3Tiles(displayContext, 0, 1, 2);
			flBitmapMap.DrawElementAt(3, displayContext, mRectToPaintX, destRect_top, publicSizeXAt, destRect_height, false, false, false, true);
			flBitmapMap.DrawElementAt(4, displayContext, destRect_left, destRect_top, publicSizeXAt2, destRect_height, false, false, false, true);
			mRectToPaintX = num;
			mRectToPaintY = num5;
			mRectToPaintW = num3;
			mRectToPaintH = publicSizeYAt2;
			DrawHorizontal3Tiles(displayContext, 5, 6, 7);
			mRectToPaintX = (short)(num + publicSizeXAt);
			mRectToPaintY = destRect_top;
			mRectToPaintW = (short)(num3 - publicSizeXAt - publicSizeXAt2);
			mRectToPaintH = (short)(num4 - publicSizeYAt - publicSizeYAt2);
		}

		public virtual void Draw12Tiles(DisplayContext displayContext)
		{
			short num = mRectToPaintX;
			short num2 = mRectToPaintY;
			short num3 = mRectToPaintW;
			short num4 = mRectToPaintH;
			FlBitmapMap flBitmapMap = mTiles;
			short publicSizeYAt = flBitmapMap.GetPublicSizeYAt(0);
			short publicSizeXAt = flBitmapMap.GetPublicSizeXAt(3);
			short publicSizeXAt2 = flBitmapMap.GetPublicSizeXAt(4);
			short publicSizeYAt2 = flBitmapMap.GetPublicSizeYAt(9);
			short num5 = (short)(num2 + publicSizeYAt);
			short num6 = (short)(num4 - publicSizeYAt - publicSizeYAt2);
			short num7 = (short)(num + num3 - publicSizeXAt2);
			short num8 = (short)(num2 + num4 - publicSizeYAt2);
			mRectToPaintX = num;
			mRectToPaintY = num2;
			mRectToPaintW = num3;
			mRectToPaintH = publicSizeYAt;
			DrawHorizontal3Tiles(displayContext, 0, 1, 2);
			mRectToPaintX = num;
			mRectToPaintY = num5;
			mRectToPaintW = publicSizeXAt;
			mRectToPaintH = num6;
			DrawVertical3Tiles(displayContext, 3, 5, 7);
			mRectToPaintX = num7;
			mRectToPaintY = num5;
			mRectToPaintW = publicSizeXAt2;
			mRectToPaintH = num6;
			DrawVertical3Tiles(displayContext, 4, 6, 8);
			mRectToPaintX = num;
			mRectToPaintY = num8;
			mRectToPaintW = num3;
			mRectToPaintH = publicSizeYAt2;
			DrawHorizontal3Tiles(displayContext, 9, 10, 11);
			mRectToPaintX = (short)(num + publicSizeXAt);
			mRectToPaintY = num5;
			mRectToPaintW = (short)(num3 - publicSizeXAt - publicSizeXAt2);
			mRectToPaintH = (short)(num4 - publicSizeYAt - publicSizeYAt2);
		}

		public virtual void DrawBackgroundColor(DisplayContext displayContext)
		{
			displayContext.DrawRectangle(mRectToPaintX, mRectToPaintY, mRectToPaintW, mRectToPaintH, true, mBackgroundColor.GetRed(), mBackgroundColor.GetGreen(), mBackgroundColor.GetBlue());
		}

		public virtual void DrawBackgroundTile(DisplayContext displayContext)
		{
			mTiles.DrawElementAt(8, displayContext, mRectToPaintX, mRectToPaintY, mRectToPaintW, mRectToPaintH, false, false, true, true);
		}

		public virtual void DrawCommonFrameCenter(DisplayContext displayContext)
		{
			FlBitmap flBitmap = mBackgroundGridTile;
			FlBitmap flBitmap2 = mBackgroundGradientTile;
			short width = flBitmap.GetWidth();
			short height = flBitmap.GetHeight();
			short width2 = flBitmap2.GetWidth();
			short height2 = flBitmap2.GetHeight();
			short num = mRectToPaintX;
			short num2 = mRectToPaintY;
			short num3 = mRectToPaintW;
			short num4 = (short)(mRectToPaintH - height2);
			short sourceRect_left;
			short sourceRect_top;
			short sourceRect_width;
			short sourceRect_height;
			short destRect_left;
			short destRect_top;
			short destRect_width;
			short destRect_height;
			if (num4 > 0)
			{
				sourceRect_left = 0;
				sourceRect_top = 0;
				sourceRect_width = width;
				sourceRect_height = height;
				destRect_left = num;
				destRect_top = num2;
				destRect_width = num3;
				destRect_height = num4;
				displayContext.DrawTiledBitmapSection(flBitmap, width, height, 0, 0, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, destRect_left, destRect_top, destRect_width, destRect_height);
			}
			short num5 = (short)(num2 + num4);
			short num6 = 0;
			if (num5 < num2)
			{
				num6 = (short)(num2 - num5);
				num5 = num2;
			}
			short num7 = (short)(height2 - num6);
			sourceRect_left = 0;
			sourceRect_top = num6;
			sourceRect_width = width2;
			sourceRect_height = num7;
			destRect_left = num;
			destRect_top = num5;
			destRect_width = num3;
			destRect_height = num7;
			displayContext.DrawTiledBitmapSection(flBitmap2, width2, num7, 0, 0, sourceRect_left, sourceRect_top, sourceRect_width, sourceRect_height, destRect_left, destRect_top, destRect_width, destRect_height);
		}

		public static ResizableFrame CreateCommonFrame(Viewport childrenContainer)
		{
			MetaPackage package = GameLibrary.GetPackage(1310760);
			Package package2 = package.GetPackage();
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(package2, 192);
			FlBitmap flBitmap = EntryPoint.GetFlBitmap(package2, 193);
			FlBitmap flBitmap2 = EntryPoint.GetFlBitmap(package2, 194);
			GameLibrary.ReleasePackage(package);
			return new ResizableFrame(7, flBitmapMap, flBitmap, flBitmap2);
		}

		public static ResizableFrame CreateCommonCursor(Viewport childrenContainer)
		{
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(1310760, 196);
			return new ResizableFrame(5, flBitmapMap);
		}

		public static ResizableFrame CreateCommonScrollbar(Viewport childrenContainer)
		{
			FlBitmapMap flBitmapMap = null;
			flBitmapMap = ((childrenContainer.GetSubtype() != -13) ? EntryPoint.GetFlBitmapMap(524304, 2) : EntryPoint.GetFlBitmapMap(524304, 1));
			return new ResizableFrame(0, flBitmapMap, 0, 1, 2);
		}

		public static ResizableFrame CreateCommonGibberishViewport(Viewport childrenContainer)
		{
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(1310760, 197);
			return new ResizableFrame(2, flBitmapMap, 0, 1);
		}
	}
}
