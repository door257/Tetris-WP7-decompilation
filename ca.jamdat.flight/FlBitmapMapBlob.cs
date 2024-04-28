namespace ca.jamdat.flight
{
	public class FlBitmapMapBlob
	{
		public const sbyte typeNumber = 52;

		public const sbyte typeID = 52;

		public const bool supportsDynamicSerialization = false;

		public const sbyte publicSizeX = 6;

		public const sbyte publicSizeY = 7;

		public const sbyte offsetX = 4;

		public const sbyte offsetY = 5;

		public const sbyte sourceWidth = 2;

		public const sbyte sourceHeight = 3;

		public const sbyte sourceLeft = 0;

		public const sbyte sourceTop = 1;

		public short[] mDataMatrix;

		public FlBitmapMapBlob()
		{
		}

		public FlBitmapMapBlob(int FlBitmapCount)
		{
			mDataMatrix = new short[FlBitmapCount << 3];
		}

		public FlBitmapMapBlob(FlBitmapMapBlob original)
		{
			if (original.mDataMatrix != null)
			{
				int num = original.mDataMatrix.Length;
				mDataMatrix = new short[num];
				Memory.Copy(mDataMatrix, original.mDataMatrix, num);
			}
			else
			{
				mDataMatrix = null;
			}
		}

		public static FlBitmapMapBlob Cast(object o, FlBitmapMapBlob _)
		{
			return (FlBitmapMapBlob)o;
		}

		public virtual void destruct()
		{
			mDataMatrix = null;
		}

		public virtual void SetFrameData(int frameIndex, short framePublicSizeX, short framePublicSizeY, short frameOffsetX, short frameOffsetY, short frameWidth, short frameHeight, short frameLeft, short frameTop)
		{
			short[] array = mDataMatrix;
			int num = frameIndex << 3;
			array[num + 6] = framePublicSizeX;
			array[num + 7] = framePublicSizeY;
			array[num + 4] = frameOffsetX;
			array[num + 5] = frameOffsetY;
			array[num + 2] = frameWidth;
			array[num + 3] = frameHeight;
			array[num] = frameLeft;
			array[num + 1] = frameTop;
		}

		public virtual int GetBitmapCount()
		{
			return mDataMatrix.Length >> 3;
		}

		public virtual FlBitmapMapBlob OnSerialize(Package p)
		{
			short t = 0;
			t = p.SerializeIntrinsic(t);
			mDataMatrix = p.SerializeIntrinsics(mDataMatrix, t << 3);
			return this;
		}

		public static FlBitmapMapBlob[] InstArrayFlBitmapMapBlob(int size)
		{
			FlBitmapMapBlob[] array = new FlBitmapMapBlob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlBitmapMapBlob();
			}
			return array;
		}

		public static FlBitmapMapBlob[][] InstArrayFlBitmapMapBlob(int size1, int size2)
		{
			FlBitmapMapBlob[][] array = new FlBitmapMapBlob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapMapBlob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapMapBlob();
				}
			}
			return array;
		}

		public static FlBitmapMapBlob[][][] InstArrayFlBitmapMapBlob(int size1, int size2, int size3)
		{
			FlBitmapMapBlob[][][] array = new FlBitmapMapBlob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapMapBlob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapMapBlob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlBitmapMapBlob();
					}
				}
			}
			return array;
		}
	}
}
