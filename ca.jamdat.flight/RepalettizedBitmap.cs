using System;

namespace ca.jamdat.flight
{
	public class RepalettizedBitmap : FlBitmapImplementor
	{
		public new const sbyte typeNumber = 43;

		public new const sbyte typeID = 43;

		public new const bool supportsDynamicSerialization = true;

		public FlBitmap mSourceBitmap;

		public static RepalettizedBitmap Cast(object o, RepalettizedBitmap _)
		{
			return (RepalettizedBitmap)o;
		}

		public override sbyte GetTypeID()
		{
			return 43;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public RepalettizedBitmap()
		{
		}

		public RepalettizedBitmap(FlBitmap sourceBitmap, Palette palette)
		{
			CreateRepalettizedBitmap(sourceBitmap, palette);
			mSourceBitmap = sourceBitmap;
			mData = null;
		}

		public override void destruct()
		{
		}

		public virtual void CreateRepalettizedBitmap(FlBitmap sourceBitmap, Palette palette)
		{
			CreateImageForRepalettization(sourceBitmap, palette);
		}

		public override void OnSerialize(Package p)
		{
			FlBitmap flBitmap = null;
			flBitmap = FlBitmap.Cast(p.SerializePointer(21, true, false), null);
			CloneAttributes(flBitmap);
			Palette palette = null;
			palette = Palette.Cast(p.SerializePointer(33, false, false), null);
			CreateRepalettizedBitmap(flBitmap, palette);
			mSourceBitmap = flBitmap;
		}

		public override void SetPalette(Palette newPalette)
		{
			CreateRepalettizedBitmap(mSourceBitmap, newPalette);
		}

		public static RepalettizedBitmap[] InstArrayRepalettizedBitmap(int size)
		{
			RepalettizedBitmap[] array = new RepalettizedBitmap[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new RepalettizedBitmap();
			}
			return array;
		}

		public static RepalettizedBitmap[][] InstArrayRepalettizedBitmap(int size1, int size2)
		{
			RepalettizedBitmap[][] array = new RepalettizedBitmap[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RepalettizedBitmap[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RepalettizedBitmap();
				}
			}
			return array;
		}

		public static RepalettizedBitmap[][][] InstArrayRepalettizedBitmap(int size1, int size2, int size3)
		{
			RepalettizedBitmap[][][] array = new RepalettizedBitmap[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RepalettizedBitmap[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RepalettizedBitmap[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new RepalettizedBitmap();
					}
				}
			}
			return array;
		}
	}
}
