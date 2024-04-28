using System;

namespace ca.jamdat.flight
{
	public class Sprite : Component
	{
		public new const sbyte typeNumber = 53;

		public new const sbyte typeID = 53;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte SPRITE_TILE_X = 0;

		public const sbyte SPRITE_TILE_Y = 1;

		public const sbyte SPRITE_FLIP_X = 2;

		public const sbyte SPRITE_FLIP_Y = 3;

		public FlBitmap mBitmap;

		public bool mTileInX;

		public bool mTileInY;

		public bool mFlipX;

		public bool mFlipY;

		public static Sprite Cast(object o, Sprite _)
		{
			return (Sprite)o;
		}

		public override sbyte GetTypeID()
		{
			return 53;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			mBitmap = null;
		}

		public virtual void SetBitmap(FlBitmap bitmap)
		{
			mBitmap = bitmap;
			if (mBitmap != null && !mTileInX && !mTileInY)
			{
				SetSize(mBitmap.GetWidth(), mBitmap.GetHeight());
			}
		}

		public virtual FlBitmap GetBitmap()
		{
			return mBitmap;
		}

		public virtual void SizeToBitmap()
		{
			SetSize(mBitmap.GetWidth(), mBitmap.GetHeight());
		}

		public virtual void MoveBy(short inDeltaX, short inDeltaY)
		{
			if (inDeltaX != 0 || inDeltaY != 0)
			{
				SetTopLeft((short)(GetRectLeft() + inDeltaX), (short)(GetRectTop() + inDeltaY));
			}
		}

		public virtual void SetTileableXY(bool tileable)
		{
			SetTileableX(tileable);
			SetTileableY(tileable);
		}

		public virtual void SetTileableX(bool tileable)
		{
			mTileInX = tileable;
		}

		public virtual void SetTileableY(bool tileable)
		{
			mTileInY = tileable;
		}

		public virtual bool GetTileInX()
		{
			return mTileInX;
		}

		public virtual bool GetTileInY()
		{
			return mTileInY;
		}

		public virtual void SetFlipX(bool flipX)
		{
			if (mFlipX != flipX)
			{
				mFlipX = flipX;
				Invalidate();
			}
		}

		public virtual void SetFlipY(bool flipY)
		{
			if (mFlipY != flipY)
			{
				mFlipY = flipY;
				Invalidate();
			}
		}

		public virtual bool GetFlipX()
		{
			return mFlipX;
		}

		public virtual bool GetFlipY()
		{
			return mFlipY;
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			FlBitmap flBitmap = mBitmap;
			if (flBitmap != null)
			{
				int drawProperty = 255;
				drawProperty = FlDrawPropertyUtil.ApplyTransform(drawProperty, FlDrawPropertyUtil.FlipXYToTransform(mFlipX, mFlipY));
				drawProperty = FlDrawPropertyUtil.ApplyTile(drawProperty, mTileInX, mTileInY);
				if (mTileInX || mTileInY)
				{
					short sourceRect_left = 0;
					short sourceRect_top = 0;
					short width = flBitmap.GetWidth();
					short height = flBitmap.GetHeight();
					displayContext.DrawTiledBitmapSection(flBitmap, flBitmap.GetWidth(), flBitmap.GetHeight(), 0, 0, sourceRect_left, sourceRect_top, width, height, mRect_left, mRect_top, mRect_width, mRect_height, drawProperty);
				}
				else
				{
					displayContext.DrawBitmapSection(flBitmap, GetRectLeft(), GetRectTop(), 0, 0, GetRectWidth(), GetRectHeight(), drawProperty);
				}
			}
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mBitmap = FlBitmap.Cast(p.SerializePointer(21, true, false), null);
			sbyte t = 0;
			t = p.SerializeIntrinsic(t);
			mTileInX = (t & 1) != 0;
			mTileInY = (t & 2) != 0;
			mFlipX = (t & 4) != 0;
			mFlipY = (t & 8) != 0;
		}

		public static Sprite[] InstArraySprite(int size)
		{
			Sprite[] array = new Sprite[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Sprite();
			}
			return array;
		}

		public static Sprite[][] InstArraySprite(int size1, int size2)
		{
			Sprite[][] array = new Sprite[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Sprite[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Sprite();
				}
			}
			return array;
		}

		public static Sprite[][][] InstArraySprite(int size1, int size2, int size3)
		{
			Sprite[][][] array = new Sprite[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Sprite[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Sprite[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Sprite();
					}
				}
			}
			return array;
		}
	}
}
