namespace ca.jamdat.flight
{
	public class Color888
	{
		public const sbyte typeNumber = 20;

		public const sbyte typeID = 20;

		public const bool supportsDynamicSerialization = false;

		public short[] rgb = new short[3];

		public Color888()
		{
		}

		public Color888(int parR, int parG, int parB)
		{
			rgb[0] = (short)parR;
			rgb[1] = (short)parG;
			rgb[2] = (short)parB;
		}

		public Color888(Color888 other)
		{
			rgb[0] = other.rgb[0];
			rgb[1] = other.rgb[1];
			rgb[2] = other.rgb[2];
		}

		public static Color888 Cast(object o, Color888 _)
		{
			return (Color888)o;
		}

		public virtual void OnSerialize(Package p)
		{
			sbyte t = (sbyte)rgb[0];
			t = p.SerializeIntrinsic(t);
			rgb[0] = (short)Memory.MakeUnsignedByte(t);
			t = (sbyte)rgb[1];
			t = p.SerializeIntrinsic(t);
			rgb[1] = (short)Memory.MakeUnsignedByte(t);
			t = (sbyte)rgb[2];
			t = p.SerializeIntrinsic(t);
			rgb[2] = (short)Memory.MakeUnsignedByte(t);
		}

		public virtual int GetRed()
		{
			return rgb[0];
		}

		public virtual int GetGreen()
		{
			return rgb[1];
		}

		public virtual int GetBlue()
		{
			return rgb[2];
		}

		public virtual void SetRed(int red)
		{
			rgb[0] = (short)red;
		}

		public virtual void SetGreen(int green)
		{
			rgb[1] = (short)green;
		}

		public virtual void SetBlue(int blue)
		{
			rgb[2] = (short)blue;
		}

		public virtual Color888 Darker()
		{
			return new Color888(GetRed() / 2, GetGreen() / 2, GetBlue() / 2);
		}

		public virtual Color888 Lighter()
		{
			return new Color888(255 - (255 - GetRed()) / 2, 255 - (255 - GetGreen()) / 2, 255 - (255 - GetBlue()) / 2);
		}

		public virtual int ToRGB888()
		{
			return (rgb[0] << 16) | (rgb[1] << 8) | rgb[2];
		}

		public virtual Color888 ToColor888()
		{
			return this;
		}

		public bool Equals(Color888 RHS)
		{
			if (rgb[0] == RHS.rgb[0] && rgb[1] == RHS.rgb[1])
			{
				return rgb[2] == RHS.rgb[2];
			}
			return false;
		}

		public virtual Color888 Assign(Color888 color)
		{
			rgb[0] = color.rgb[0];
			rgb[1] = color.rgb[1];
			rgb[2] = color.rgb[2];
			return this;
		}

		public static Color888 Color888From565(short color565)
		{
			short parR = (short)(((color565 & 0xF800) >> 8) | 7);
			short parG = (short)(((color565 & 0x7E0) >> 3) | 3);
			short parB = (short)(((color565 & 0x1F) << 3) | 7);
			return new Color888(parR, parG, parB);
		}

		public static Color888[] InstArrayColor888(int size)
		{
			Color888[] array = new Color888[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Color888();
			}
			return array;
		}

		public static Color888[][] InstArrayColor888(int size1, int size2)
		{
			Color888[][] array = new Color888[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Color888[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Color888();
				}
			}
			return array;
		}

		public static Color888[][][] InstArrayColor888(int size1, int size2, int size3)
		{
			Color888[][][] array = new Color888[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Color888[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Color888[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Color888();
					}
				}
			}
			return array;
		}
	}
}
