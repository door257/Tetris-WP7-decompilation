using System;

namespace ca.jamdat.flight
{
	public class IndexedSprite : Sprite
	{
		public new const sbyte typeNumber = 90;

		public new const sbyte typeID = 90;

		public new const bool supportsDynamicSerialization = true;

		public FlBitmapMap mBmpMap;

		public int mCurrentFrame;

		public int[] mValue = new int[1];

		public static IndexedSprite Cast(object o, IndexedSprite _)
		{
			return (IndexedSprite)o;
		}

		public override sbyte GetTypeID()
		{
			return 90;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			if (mBmpMap != null)
			{
				mBmpMap = null;
			}
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			if (mBmpMap != null)
			{
				mBmpMap.DrawElementAt(mCurrentFrame, displayContext, mRect_left, mRect_top, mRect_width, mRect_height, mFlipX, mFlipY, mTileInX, mTileInY);
			}
		}

		public virtual void SetBitmapMap(FlBitmapMap bitmapMap)
		{
			mBmpMap = bitmapMap;
			SetCurrentFrame(mCurrentFrame);
		}

		public override void SizeToBitmap()
		{
			int index = mCurrentFrame;
			short publicSizeXAt = mBmpMap.GetPublicSizeXAt(index);
			short publicSizeYAt = mBmpMap.GetPublicSizeYAt(index);
			SetSize(publicSizeXAt, publicSizeYAt);
		}

		public virtual void SetCurrentFrame(int frame)
		{
			FlBitmapMap flBitmapMap = mBmpMap;
			if (flBitmapMap != null)
			{
				frame %= flBitmapMap.GetBitmapCount();
				mCurrentFrame = frame;
				short width = flBitmapMap.GetPublicSizeXAt(frame);
				short height = flBitmapMap.GetPublicSizeYAt(frame);
				if (mTileInX)
				{
					width = GetRectWidth();
				}
				if (mTileInY)
				{
					height = GetRectHeight();
				}
				SetSize(width, height);
			}
		}

		public virtual int GetCurrentFrame()
		{
			return mCurrentFrame;
		}

		public virtual FlBitmapMap GetBitmapMap()
		{
			return mBmpMap;
		}

		public override void ControlValue(int valueCode, bool setValue, Controller controller)
		{
			if (valueCode == 7)
			{
				if (setValue)
				{
					int[] array = mValue;
					controller.GetControlledValue(array, 1);
					SetCurrentFrame(array[0]);
				}
				else
				{
					controller.SetRequestedValue(new int[1] { GetCurrentFrame() }, 1);
				}
			}
			else
			{
				base.ControlValue(valueCode, setValue, controller);
			}
		}

		public override void OnSerialize(Package _package)
		{
			mBitmap = null;
			base.OnSerialize(_package);
			mBmpMap = FlBitmapMap.Cast(_package.SerializePointer(37, true, false), null);
			mCurrentFrame = _package.SerializeIntrinsic(mCurrentFrame);
			SetCurrentFrame(mCurrentFrame);
		}

		public static IndexedSprite[] InstArrayIndexedSprite(int size)
		{
			IndexedSprite[] array = new IndexedSprite[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new IndexedSprite();
			}
			return array;
		}

		public static IndexedSprite[][] InstArrayIndexedSprite(int size1, int size2)
		{
			IndexedSprite[][] array = new IndexedSprite[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new IndexedSprite[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new IndexedSprite();
				}
			}
			return array;
		}

		public static IndexedSprite[][][] InstArrayIndexedSprite(int size1, int size2, int size3)
		{
			IndexedSprite[][][] array = new IndexedSprite[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new IndexedSprite[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new IndexedSprite[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new IndexedSprite();
					}
				}
			}
			return array;
		}
	}
}
