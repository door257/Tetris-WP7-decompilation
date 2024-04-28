using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ca.jamdat.flight
{
	public class FlBitmapImplementor : FlBitmap
	{
		public new const sbyte typeNumber = 91;

		public new const sbyte typeID = 91;

		public new const bool supportsDynamicSerialization = true;

		internal const string PNG_HEADER = "襐乇ഊᨊ\0\r䥈䑒";

		internal const int PNG_HEADER_SIZE = 16;

		internal const int PNG_HEADER_DATA = 17;

		internal const string PNG_PLTE = "偌呅";

		internal const int PNG_PLTE_SIZE = 4;

		internal const string PNG_TRNS = "瑒乓";

		internal const int PNG_TRNS_SIZE = 4;

		internal const int PNG_IDATSIZE_SIZE = 4;

		internal const string PNG_IDAT = "䥄䅔";

		internal const int PNG_IDAT_SIZE = 4;

		internal const string PNG_IEND = "\0䥅乄깂悂";

		internal const int PNG_IEND_SIZE = 10;

		internal const int PNG_EMBEDDED_DATA_SIZE = 30;

		internal const int PNG_BYTECOUNT_OVERHEAD = 4;

		internal const int PNG_CRC_OVERHEAD = 4;

		internal const int PNG_CHUNK_OVERHEAD = 12;

		internal const int PNG_HEADER_SIZE_IN_PACKAGE = 21;

		internal const int PNG_PALETTE_SIZE_PER_COLOR = 3;

		internal const int PNG_PALETTE_FOOTER_SIZE = 4;

		internal const int PNG_PALETTE_POSITION_IN_PNG = 33;

		public Texture2D mImage;

		public FlBitmapImplementor()
		{
		}

		public FlBitmapImplementor(FlString filePath)
		{
			string text = filePath.NativeString;
			if (text[0] != '/')
			{
				text = '/' + text;
			}
			try
			{
				mImage = Texture2D.FromStream(FrameworkGlobals.GetInstance().GraphicsDeviceManager.GraphicsDevice, TitleContainer.OpenStream(text));
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
			if (mImage != null)
			{
				mDataWidth = (short)mImage.Width;
				mDataHeight = (short)mImage.Height;
			}
		}

		public static FlBitmapImplementor Cast(object o, FlBitmapImplementor _)
		{
			return (FlBitmapImplementor)o;
		}

		public override sbyte GetTypeID()
		{
			return 91;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
		}

		public override void SetSize(short width, short height)
		{
		}

		public override int GetPixelFormat()
		{
			return 51456;
		}

		public override void Clone(FlBitmap inBitmap)
		{
			short width = inBitmap.GetWidth();
			short height = inBitmap.GetHeight();
			mImage = new Texture2D(FrameworkGlobals.GetInstance().GraphicsDeviceManager.GraphicsDevice, width, height);
			uint[] data = new uint[width * height];
			inBitmap.getImage().GetData(data);
			mImage.SetData(data);
			CloneAttributes(inBitmap);
		}

		public override void Duplicate(FlBitmap inBitmap)
		{
		}

		public override void OnSerialize(Package p)
		{
			sbyte[] array = null;
			short t = 0;
			int t2 = 0;
			t2 = p.SerializeIntrinsic(t2);
			t = p.SerializeIntrinsic(t);
			short t3 = 0;
			t3 = p.SerializeIntrinsic(t3);
			bool t4 = false;
			t4 = p.SerializeIntrinsic(t4);
			int num = 33;
			int num2 = t * 3;
			if (t > 0)
			{
				num2 += 12;
				num += num2;
			}
			int num3 = t3;
			if (t3 > 0)
			{
				num3 += 12;
				num += num3;
				t2 -= t3 + 4;
			}
			num += 4 + t2 - 21 + 10 + 4;
			array = new sbyte[num];
			int posInDest = 0;
			posInDest = StringUtils.StringToBytes("襐乇ഊᨊ\0\r䥈䑒", array, posInDest);
			posInDest = Library_loadBytes(p, 17, array, posInDest);
			posInDest += num2;
			if (t3 > 0)
			{
				array[posInDest++] = 0;
				array[posInDest++] = 0;
				array[posInDest++] = (sbyte)((ushort)t3 >> 8);
				array[posInDest++] = (sbyte)(t3 & 0xFF);
				posInDest = StringUtils.StringToBytes("瑒乓", array, posInDest);
				posInDest = Library_loadBytes(p, t3 + 4, array, posInDest);
			}
			posInDest = Library_loadBytes(p, 4, array, posInDest);
			posInDest = StringUtils.StringToBytes("䥄䅔", array, posInDest);
			posInDest = Library_loadBytes(p, t2 - 21, array, posInDest);
			posInDest = StringUtils.StringToBytes("\0䥅乄깂悂", array, posInDest);
			Palette palette = null;
			palette = Palette.Cast(p.SerializePointer(33, false, false), null);
			if (palette != null)
			{
				CreateImage(array, palette.GetData());
			}
			else
			{
				CreateImage(array, null);
			}
			if (!t4)
			{
				array = null;
			}
			mData = array;
		}

		public override Palette GetPalette()
		{
			return null;
		}

		public override void SetPalette(Palette newPalette)
		{
		}

		public override void GetRGB(int[] outARGB, int outPixelFormat, int offset, int scanlength, int x, int y, int width, int height)
		{
		}

		public virtual void CreateEmptyImage(short width, short height)
		{
			mImage = new Texture2D(FrameworkGlobals.GetInstance().GraphicsDeviceManager.GraphicsDevice, width, height);
			mDataWidth = width;
			mDataHeight = height;
		}

		public virtual void CreateImageFromPNGData(sbyte[] pngData, int offset, int length)
		{
		}

		public virtual void CreateImageFromDepalettizedRawData(int[] srcRawData, int srcPixelFormat, short width, short height, Color888 colorKey, bool preserveAlphaChannel)
		{
			mDataWidth = width;
			mDataHeight = height;
		}

		public virtual void CreateAlphaImageFromRawData(int[] rawData, short width, short height, int opacity, Color888 colorKey)
		{
			int num = width * height;
			int[] srcRawData = new int[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = rawData[i];
			}
			CreateImageFromDepalettizedRawData(srcRawData, 51456, width, height, colorKey, true);
		}

		public virtual void CreateImageFromRawData(sbyte[] rawData, sbyte[] palData, short width, short height)
		{
			CreateImageFromRawData(rawData, palData, width, height, -1);
		}

		public virtual void CreateImageFromRawData(sbyte[] rawData, sbyte[] palData, short width, short height, int colorKeyIndex)
		{
			sbyte[] array = CreatePNGBuffer(rawData, palData, width, height, colorKeyIndex);
			CreateImageFromPNGData(array, 0, array.Length);
		}

		public virtual void CreateImageFromRawData(sbyte[] rawData, sbyte[] palData, short width, short height, Color888 colorKey)
		{
			int colorKeyIndex = -1;
			int num = palData.Length / 3;
			for (int i = 0; i < num; i++)
			{
				if ((palData[i * 3] & 0xFF) == colorKey.GetRed() && (palData[i * 3 + 1] & 0xFF) == colorKey.GetGreen() && (palData[i * 3 + 2] & 0xFF) == colorKey.GetBlue())
				{
					colorKeyIndex = i;
					break;
				}
			}
			CreateImageFromRawData(rawData, palData, width, height, colorKeyIndex);
		}

		public virtual sbyte[] CreatePNGBuffer(sbyte[] rawData, sbyte[] plteData, short width, short height, int colorKeyIndex)
		{
			sbyte[] array = null;
			int num = (251 + (width * height + height)) / 252;
			int num2 = 75 + plteData.Length + width * height + height + 5 * num;
			int num3 = plteData.Length / 3;
			if (colorKeyIndex != -1)
			{
				num2 += 12 + num3;
			}
			array = new sbyte[num2];
			int num4 = 0;
			num4 = StringUtils.StringToBytes("襐乇ഊᨊ\0\r䥈䑒", array, 0);
			array[11] = 13;
			int num5 = 12;
			num4 = Memory.WriteIntToByteArray(width, array, num4);
			num4 = Memory.WriteIntToByteArray(height, array, num4);
			array[num4++] = 8;
			array[num4++] = 3;
			num4 += 3;
			int data = (int)Memory.CalculateCRC(array, num5, num4 - num5);
			num4 = Memory.WriteIntToByteArray(data, array, num4);
			num4 = Memory.WriteIntToByteArray(plteData.Length, array, num4);
			num5 = num4;
			num4 = StringUtils.StringToBytes("偌呅", array, num4);
			Array.Copy(plteData, 0, array, num4, plteData.Length);
			num4 += plteData.Length;
			data = (int)Memory.CalculateCRC(array, num5, num4 - num5);
			num4 = Memory.WriteIntToByteArray(data, array, num4);
			if (colorKeyIndex != -1)
			{
				num4 = Memory.WriteIntToByteArray(num3, array, num4);
				num5 = num4;
				num4 = StringUtils.StringToBytes("瑒乓", array, num4);
				for (int i = 0; i < num3; i++)
				{
					array[num4 + i] = -1;
				}
				array[num4 + colorKeyIndex] = 0;
				num4 += num3;
				data = (int)Memory.CalculateCRC(array, num5, num4 - num5);
				num4 = Memory.WriteIntToByteArray(data, array, num4);
			}
			int data2 = width * height + 2 + 4 + height + 5 * num;
			num4 = Memory.WriteIntToByteArray(data2, array, num4);
			num5 = num4;
			num4 = StringUtils.StringToBytes("䥄䅔", array, num4);
			array[num4++] = 24;
			array[num4++] = 25;
			int num6 = 0;
			sbyte[] array2 = new sbyte[(width + 1) * height];
			for (int j = 0; j < height; j++)
			{
				array2[num6++] = 0;
				Array.Copy(rawData, j * width, array2, num6, width);
				num6 += width;
			}
			int num7 = num6;
			int num8 = 252;
			sbyte b = 0;
			for (num6 = 0; num7 - num6 > 0; num6 += num8)
			{
				if (num7 - num6 <= 252)
				{
					b = 1;
					num8 = num7 - num6;
				}
				array[num4++] = b;
				array[num4++] = (sbyte)num8;
				array[num4++] = 0;
				array[num4++] = (sbyte)(255 - num8);
				array[num4++] = -1;
				Array.Copy(array2, num6, array, num4, num8);
				num4 += num8;
			}
			int num9 = 1;
			int num10 = 0;
			for (int j = 0; j < array2.Length; j++)
			{
				num9 += array2[j] & 0xFF;
				num9 %= 65521;
				num10 += num9;
				num10 %= 65521;
			}
			array[num4++] = (sbyte)((num10 & 0xFF00) >> 8);
			array[num4++] = (sbyte)(num10 & 0xFF);
			array[num4++] = (sbyte)((num9 & 0xFF00) >> 8);
			array[num4++] = (sbyte)(num9 & 0xFF);
			data = (int)Memory.CalculateCRC(array, num5, num4 - num5);
			num4 = Memory.WriteIntToByteArray(data, array, num4);
			num4 += 2;
			num4 = StringUtils.StringToBytes("\0䥅乄깂悂", array, num4);
			return array;
		}

		public virtual void CloneAttributes(FlBitmap inBitmap)
		{
			mDataWidth = inBitmap.GetDataWidth();
			mDataHeight = inBitmap.GetDataHeight();
		}

		public override Texture2D getImage()
		{
			return mImage;
		}

		public virtual int Library_loadBytes(Package p, int bytesToRead, sbyte[] buffer, int whereInBuffer)
		{
			p.ReadBufferAtOffset(buffer, whereInBuffer, bytesToRead);
			return whereInBuffer + bytesToRead;
		}

		public virtual void CreateImageForRepalettization(FlBitmap sourceBitmap, Palette palette)
		{
			CreateImage(sourceBitmap.mData, palette.GetData());
			mData = null;
		}

		public virtual void ReallocateData(short width, short height)
		{
			if (mDataWidth != width || mDataHeight != height)
			{
				mData = null;
				mDataWidth = width;
				mDataHeight = height;
				mData = new sbyte[height * (short)GetBytesPerLine()];
			}
		}

		public virtual void CreateImage(sbyte[] bitmap, sbyte[] palette)
		{
			if (palette != null)
			{
				int num = palette.Length - 4;
				bitmap[33] = 0;
				bitmap[34] = 0;
				bitmap[35] = (sbyte)((uint)num >> 8);
				bitmap[36] = (sbyte)(num & 0xFF);
				StringUtils.StringToBytes("偌呅", bitmap, 37);
				Array.Copy(palette, 0, bitmap, 41, palette.Length);
			}
			byte[] array = new byte[bitmap.Length];
			Buffer.BlockCopy(bitmap, 0, array, 0, bitmap.Length);
			MemoryStream stream = new MemoryStream(array);
			mImage = Texture2D.FromStream(FrameworkGlobals.GetInstance().GraphicsDeviceManager.GraphicsDevice, stream);
			mDataWidth = (short)mImage.Width;
			mDataHeight = (short)mImage.Height;
		}

		public static FlBitmapImplementor[] InstArrayFlBitmapImplementor(int size)
		{
			FlBitmapImplementor[] array = new FlBitmapImplementor[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlBitmapImplementor();
			}
			return array;
		}

		public static FlBitmapImplementor[][] InstArrayFlBitmapImplementor(int size1, int size2)
		{
			FlBitmapImplementor[][] array = new FlBitmapImplementor[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapImplementor[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapImplementor();
				}
			}
			return array;
		}

		public static FlBitmapImplementor[][][] InstArrayFlBitmapImplementor(int size1, int size2, int size3)
		{
			FlBitmapImplementor[][][] array = new FlBitmapImplementor[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlBitmapImplementor[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlBitmapImplementor[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlBitmapImplementor();
					}
				}
			}
			return array;
		}
	}
}
