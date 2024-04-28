using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class MinoSprite : IndexedSprite
	{
		public const sbyte minoAspectNone = -1;

		public const sbyte minoAspectLightBlue = 0;

		public const sbyte minoAspectDarkBlue = 1;

		public const sbyte minoAspectOrange = 2;

		public const sbyte minoAspectYellow = 3;

		public const sbyte minoAspectGreen = 4;

		public const sbyte minoAspectPurple = 5;

		public const sbyte minoAspectRed = 6;

		public const sbyte minoAspectEffectLightBlue = 7;

		public const sbyte minoAspectEffectDarkBlue = 8;

		public const sbyte minoAspectEffectOrange = 9;

		public const sbyte minoAspectEffectYellow = 10;

		public const sbyte minoAspectEffectGreen = 11;

		public const sbyte minoAspectEffectPurple = 12;

		public const sbyte minoAspectEffectRed = 13;

		public const sbyte minoAspectEffectWhite = 14;

		public const sbyte minoAspectWhite = 15;

		public const sbyte minoAspectSolid = 16;

		public const sbyte minoAspectGrey = 17;

		public const sbyte minoAspectErrorFlash = 18;

		public const sbyte minoAspectSmall = 19;

		public const sbyte minoAspectCount = 20;

		public const sbyte aspectSizeBig = 0;

		public const sbyte aspectSizeSmall = 1;

		public sbyte mMinoSpriteAspect;

		public sbyte mMinoSpriteAspectSize;

		public KeyFrameController mFrameIndexController;

		public KeyFrameController mAspectController;

		public static bool IsAspectChange(sbyte aspect)
		{
			if (aspect >= 7)
			{
				return aspect <= 15;
			}
			return false;
		}

		public MinoSprite()
		{
			mMinoSpriteAspect = -1;
			mMinoSpriteAspectSize = 0;
			mFrameIndexController = new KeyFrameController();
			mFrameIndexController.SetControlParameters(this, 7);
			mAspectController = new KeyFrameController();
			mAspectController.SetControlParameters(this, 102);
			SetSize(32, 32);
		}

		public override void destruct()
		{
			mFrameIndexController = null;
			mAspectController = null;
		}

		public virtual void Unload()
		{
			SetViewport(null);
			mBmpMap = null;
		}

		public virtual void SetMinoSpriteAspect(sbyte minoAspect, FlBitmapMap bitmapMap)
		{
			if (mBmpMap != bitmapMap || mBmpMap == null)
			{
				mMinoSpriteAspect = minoAspect;
				mBmpMap = bitmapMap;
				int num = 0;
				if (mMinoSpriteAspectSize == 1)
				{
					num = minoAspect;
				}
				SetCurrentFrame(num);
				if (GetRectWidth() != bitmapMap.GetSourceWidthAt(num))
				{
					SetSize(bitmapMap.GetSourceWidthAt(num), bitmapMap.GetSourceHeightAt(num));
				}
				else
				{
					Invalidate();
				}
			}
		}

		public virtual sbyte GetMinoSpriteAspect()
		{
			return mMinoSpriteAspect;
		}

		public static FlBitmapMap GetBitmapForMinoSpriteAspect(sbyte minoAspect, sbyte aspectSize)
		{
			int entryPoint = ((aspectSize == 0) ? minoAspect : 19);
			return EntryPoint.GetFlBitmapMap(1146915, entryPoint);
		}

		public virtual KeyFrameController GetFrameIndexController()
		{
			return mFrameIndexController;
		}

		public virtual KeyFrameController GetAspectController()
		{
			return mAspectController;
		}

		public override void ControlValue(int valueCode, bool setValue, Controller controller)
		{
			if (valueCode == 102)
			{
				if (setValue)
				{
					sbyte minoAspect = (sbyte)controller.GetLongValue();
					SetMinoSpriteAspect(minoAspect, GetBitmapForMinoSpriteAspect(minoAspect));
				}
				else
				{
					controller.SetValue(mMinoSpriteAspect);
				}
			}
			else
			{
				base.ControlValue(valueCode, setValue, controller);
			}
		}

		public virtual void SetMinoSpriteAspectSize(sbyte aspectSize)
		{
			if (mMinoSpriteAspectSize != aspectSize)
			{
				mMinoSpriteAspectSize = aspectSize;
				SetMinoSpriteAspect(mMinoSpriteAspect, GetBitmapForMinoSpriteAspect(mMinoSpriteAspect, aspectSize));
			}
		}

		public virtual sbyte GetMinoSpriteAspectSize()
		{
			return mMinoSpriteAspectSize;
		}

		public static FlBitmapMap GetBitmapForMinoSpriteAspect(sbyte minoAspect)
		{
			return GetBitmapForMinoSpriteAspect(minoAspect, 0);
		}

		public static MinoSprite[] InstArrayMinoSprite(int size)
		{
			MinoSprite[] array = new MinoSprite[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new MinoSprite();
			}
			return array;
		}

		public static MinoSprite[][] InstArrayMinoSprite(int size1, int size2)
		{
			MinoSprite[][] array = new MinoSprite[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoSprite[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoSprite();
				}
			}
			return array;
		}

		public static MinoSprite[][][] InstArrayMinoSprite(int size1, int size2, int size3)
		{
			MinoSprite[][][] array = new MinoSprite[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoSprite[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoSprite[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new MinoSprite();
					}
				}
			}
			return array;
		}
	}
}
