using System;

namespace ca.jamdat.flight
{
	public class FlBitmapMap
	{
		public const sbyte typeNumber = 37;

		public const sbyte typeID = 37;

		public const bool supportsDynamicSerialization = true;

		public FlBitmap mReferenceBitmap;

		public FlBitmapMapBlob mBlob;

		public static FlBitmapMap Cast(object o, FlBitmapMap _)
		{
			return (FlBitmapMap)o;
		}

		public virtual sbyte GetTypeID()
		{
			return 37;
		}

		public static Type AsClass()
		{
			return null;
		}

		public virtual void destruct()
		{
			mBlob = null;
			mReferenceBitmap = null;
		}

		public virtual short GetPublicSizeXAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 6];
		}

		public virtual short GetPublicSizeYAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 7];
		}

		public virtual short GetOffsetXAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 4];
		}

		public virtual short GetOffsetYAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 5];
		}

		public virtual short GetSourceLeftAt(int index)
		{
			return mBlob.mDataMatrix[index << 3];
		}

		public virtual short GetSourceTopAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 1];
		}

		public virtual short GetSourceWidthAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 2];
		}

		public virtual short GetSourceHeightAt(int index)
		{
			return mBlob.mDataMatrix[(index << 3) + 3];
		}

		public virtual void CopyFrom(FlBitmapMap source)
		{
			mReferenceBitmap = source.mReferenceBitmap;
			mBlob = null;
			SetBitmapMapBlob(new FlBitmapMapBlob(source.mBlob));
		}

		public virtual int GetBitmapCount()
		{
			return mBlob.GetBitmapCount();
		}

		public virtual void DrawElementAt(int index, DisplayContext dc, short destRect_left, short destRect_top, short destRect_width, short destRect_height, bool flipX, bool flipY, bool tileInX, bool tileInY)
		{
			int drawProperty = 255;
			drawProperty = FlDrawPropertyUtil.ApplyTransform(drawProperty, FlDrawPropertyUtil.FlipXYToTransform(flipX, flipY));
			drawProperty = FlDrawPropertyUtil.ApplyTile(drawProperty, tileInX, tileInY);
			int num = index << 3;
			short num2 = 0;
			short num3 = 0;
			short[] mDataMatrix = mBlob.mDataMatrix;
			num2 = ((!flipX) ? ((short)(destRect_left + mDataMatrix[num + 4])) : ((short)(destRect_left + mDataMatrix[num + 6] - mDataMatrix[num + 4] - mDataMatrix[num + 2])));
			num3 = ((!flipY) ? ((short)(destRect_top + mDataMatrix[num + 5])) : ((short)(destRect_top + mDataMatrix[num + 7] - mDataMatrix[num + 5] - mDataMatrix[num + 3])));
			if (tileInX || tileInY)
			{
				dc.DrawTiledBitmapSection(mReferenceBitmap, mDataMatrix[num + 6], mDataMatrix[num + 7], (short)(num2 - destRect_left), (short)(num3 - destRect_top), mDataMatrix[num], mDataMatrix[num + 1], mDataMatrix[num + 2], mDataMatrix[num + 3], destRect_left, destRect_top, destRect_width, destRect_height, drawProperty);
			}
			else
			{
				dc.DrawBitmapSection(mReferenceBitmap, num2, num3, mDataMatrix[num], mDataMatrix[num + 1], mDataMatrix[num + 2], mDataMatrix[num + 3], drawProperty);
			}
		}

		public virtual void DrawElementAt(int index, DisplayContext dc, short destLeft, short destTop, bool flipX, bool flipY)
		{
			DrawElementAt(index, dc, destLeft, destTop, 0, 0, flipX, flipY, false, false);
		}

		public virtual void SetAsGrid(int numCol, int numRow)
		{
			short[] mDataMatrix = mBlob.mDataMatrix;
			int num = mReferenceBitmap.GetWidth() / numCol;
			int num2 = mReferenceBitmap.GetHeight() / numRow;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < numRow; i++)
			{
				for (int j = 0; j < numCol; j++)
				{
					num3 = num4 << 3;
					mDataMatrix[num3] = (short)(j * num);
					mDataMatrix[num3 + 1] = (short)(i * num2);
					mDataMatrix[num3 + 2] = (short)num;
					mDataMatrix[num3 + 3] = (short)num2;
					mDataMatrix[num3 + 6] = (short)num;
					mDataMatrix[num3 + 7] = (short)num2;
					mDataMatrix[num3 + 4] = 0;
					mDataMatrix[num3 + 5] = 0;
					num4++;
				}
			}
		}

		public virtual void OnSerialize(Package p)
		{
			mBlob = FlBitmapMapBlob.Cast(p.SerializePointer(52, false, false), null);
			mReferenceBitmap = FlBitmap.Cast(p.SerializePointer(21, true, false), null);
		}

		public virtual void SetReferenceBitmap(FlBitmap bitmap)
		{
			mReferenceBitmap = bitmap;
		}

		public virtual FlBitmap GetReferenceBitmap()
		{
			return mReferenceBitmap;
		}

		public virtual short[] GetDataMatrix()
		{
			return mBlob.mDataMatrix;
		}

		public virtual void SetBitmapMapBlob(FlBitmapMapBlob blob)
		{
			mBlob = blob;
		}

		public static FlBitmapMap[] InstArrayFlBitmapMap(int size)
		{
			FlBitmapMap[] array = new FlBitmapMap[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlBitmapMap();
			}
			return array;
		}

		public static FlBitmapMap[][] InstArrayFlBitmapMap(int size1, int size2)
		{
			FlBitmapMap[][] array = new FlBitmapMap[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapMap[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapMap();
				}
			}
			return array;
		}

		public static FlBitmapMap[][][] InstArrayFlBitmapMap(int size1, int size2, int size3)
		{
			FlBitmapMap[][][] array = new FlBitmapMap[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapMap[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapMap[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlBitmapMap();
					}
				}
			}
			return array;
		}
	}
}
