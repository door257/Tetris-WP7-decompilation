namespace ca.jamdat.flight
{
	public class Palette
	{
		public const sbyte typeNumber = 33;

		public const sbyte typeID = 33;

		public const bool supportsDynamicSerialization = false;

		public sbyte[] mColors;

		public static Palette Cast(object o, Palette _)
		{
			return (Palette)o;
		}

		public virtual void destruct()
		{
			mColors = null;
			mColors = null;
		}

		public virtual void InitializeToZero()
		{
			Memory.Set(mColors, 0, NumBytesInPalette());
		}

		public virtual int NumBytesPerColor()
		{
			return 3;
		}

		public virtual int NumBytesInPalette()
		{
			return mColors.Length;
		}

		public virtual short GetNumColors()
		{
			return (short)((mColors.Length - 4) / NumBytesPerColor());
		}

		public virtual int GetPixelFormat()
		{
			return 1536;
		}

		public virtual int GetJavaSizeOfPalette(short numColors)
		{
			return numColors * NumBytesPerColor() + 4;
		}

		public virtual Palette OnSerialize(Package p)
		{
			short t = 0;
			t = p.SerializeIntrinsic(t);
			if (p.IsReading())
			{
				mColors = p.SerializeIntrinsics(mColors, GetJavaSizeOfPalette(t));
			}
			return this;
		}

		public virtual sbyte[] GetData()
		{
			return mColors;
		}

		public static Palette[] InstArrayPalette(int size)
		{
			Palette[] array = new Palette[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Palette();
			}
			return array;
		}

		public static Palette[][] InstArrayPalette(int size1, int size2)
		{
			Palette[][] array = new Palette[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Palette[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Palette();
				}
			}
			return array;
		}

		public static Palette[][][] InstArrayPalette(int size1, int size2, int size3)
		{
			Palette[][][] array = new Palette[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Palette[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Palette[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Palette();
					}
				}
			}
			return array;
		}
	}
}
