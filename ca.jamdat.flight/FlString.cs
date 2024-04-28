namespace ca.jamdat.flight
{
	public class FlString
	{
		public const sbyte typeNumber = 35;

		public const sbyte typeID = 35;

		public const bool supportsDynamicSerialization = false;

		public const short kBufferSizeForLongValue = 16;

		public sbyte[] mData;

		public string NativeString
		{
			get
			{
				return StringUtils.CreateJavaString(this);
			}
		}

		public FlString()
		{
			Assign(new sbyte[1] { 0 }, 0, 0);
		}

		public FlString(FlString @string)
		{
			Assign(@string.mData, 0, @string.GetLength());
		}

		public FlString(FlString @string, int startIndex, int nbChar)
		{
			Assign(@string.mData, startIndex, nbChar);
		}

		public FlString(sbyte[] @string)
		{
			Assign(@string, 0, StringUtils.StringLen(@string));
		}

		public FlString(string @string)
		{
			Assign(@string);
		}

		public FlString(sbyte value)
		{
			Assign(value);
		}

		public FlString(short value)
		{
			Assign(value);
		}

		public FlString(int value)
		{
			Assign(value);
		}

		public FlString(long value)
		{
			Assign(value);
		}

		public FlString(F32 f32Number, int f32Point, int txtNumDigitsAfterPoint)
		{
			Assign(StringUtils.CreateString(""));
			bool flag = f32Number.IsNegative();
			F32 f = new F32();
			int num = ((!flag) ? f32Number : f32Number.Neg()).ToFixedPoint(f32Point);
			int num2 = num >> f32Point;
			int[] array = null;
			if (txtNumDigitsAfterPoint > 0)
			{
				array = new int[txtNumDigitsAfterPoint];
				int num3 = num - (num2 << f32Point);
				num3 *= 10;
				array[0] = num3 >> f32Point;
				for (int i = 1; i < txtNumDigitsAfterPoint; i++)
				{
					num3 -= array[i - 1] << f32Point;
					num3 *= 10;
					array[i] = num3 >> f32Point;
				}
			}
			new FlString();
			AddAssign(flag ? StringUtils.CreateString("-") : StringUtils.CreateString(""));
			FlString flString = new FlString();
			flString.Assign(num2);
			AddAssign(flString);
			if (txtNumDigitsAfterPoint > 0)
			{
				AddAssign(StringUtils.CreateString("."));
				for (int j = 0; j < txtNumDigitsAfterPoint; j++)
				{
					FlString flString2 = new FlString();
					flString2.Assign(array[j]);
					AddAssign(flString2);
				}
			}
			array = null;
		}

		public FlString(Stream stream)
		{
			short num = stream.ReadShort();
			mData = new sbyte[num + 1];
			if (num != 0)
			{
				stream.Read(mData, num);
			}
			mData[num] = 0;
		}

		public static FlString Cast(object o, FlString _)
		{
			return (FlString)o;
		}

		public static FlString FromChar(sbyte c)
		{
			return StringUtils.CreateString(new sbyte[2] { c, 0 });
		}

		public virtual void destruct()
		{
			mData = null;
		}

		public virtual FlString Assign(FlString @string)
		{
			Assign(@string.mData, 0, @string.GetLength());
			return this;
		}

		public virtual FlString AddAssign(FlString @string)
		{
			return AddAssign(@string.mData);
		}

		public virtual FlString Assign(sbyte[] @string)
		{
			Assign(@string, 0, StringUtils.StringLen(@string));
			return this;
		}

		public virtual FlString AddAssign(sbyte[] @string)
		{
			if (mData == @string)
			{
				return this;
			}
			int length = GetLength();
			int num = StringUtils.StringLen(@string);
			GuaranteeCapacity(length + num);
			StringUtils.StringNCopy(mData, @string, length, 0, num + 1);
			return this;
		}

		public virtual FlString Add(sbyte[] other)
		{
			FlString flString = new FlString(mData);
			flString.AddAssign(other);
			return flString;
		}

		public virtual FlString Add(FlString other)
		{
			return Add(other.mData);
		}

		public bool Equals(FlString string2)
		{
			int length = GetLength();
			if (string2.GetLength() != length)
			{
				return false;
			}
			sbyte[] array = string2.mData;
			for (int i = 0; i < length; i++)
			{
				if (mData[i] != array[i])
				{
					return false;
				}
			}
			return true;
		}

		public virtual sbyte GetCharAt(int offset)
		{
			return mData[offset];
		}

		public virtual int GetLength()
		{
			return StringUtils.StringLen(mData);
		}

		public virtual bool IsEmpty()
		{
			return mData[0] == 0;
		}

		public virtual void Write(Stream stream, bool writeStrLen)
		{
			int length = GetLength();
			if (writeStrLen)
			{
				stream.WriteShort((short)length);
			}
			if (length != 0)
			{
				stream.Write(mData, length);
			}
		}

		public virtual int ToLong()
		{
			int length = GetLength();
			long num = 1L;
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			if (length == 0)
			{
				return 0;
			}
			for (int i = 0; i < length; i++)
			{
				num *= 10;
			}
			do
			{
				sbyte b = mData[num2++];
				num /= 10;
				if (b == 45)
				{
					flag = true;
				}
				else
				{
					num3 += (int)(num * (b - 48));
				}
			}
			while (num != 1);
			if (flag)
			{
				num3 = -num3;
			}
			return num3;
		}

		public virtual long ToLong64()
		{
			int length = GetLength();
			long num = 1L;
			int num2 = 0;
			long num3 = 0L;
			bool flag = false;
			if (length == 0)
			{
				return 0L;
			}
			for (int i = 0; i < length; i++)
			{
				num *= 10;
			}
			do
			{
				sbyte b = mData[num2++];
				num /= 10;
				if (b == 45)
				{
					flag = true;
				}
				else
				{
					num3 += (int)(num * (b - 48));
				}
			}
			while (num != 1);
			if (flag)
			{
				num3 = -num3;
			}
			return num3;
		}

		public virtual bool IsNumber()
		{
			int i = 0;
			if (mData[i] == 45)
			{
				i++;
			}
			for (; i < GetLength(); i++)
			{
				if (mData[i] < 48 || mData[i] > 57)
				{
					return false;
				}
			}
			return true;
		}

		public virtual int FindChar(sbyte inChar, int startIndex)
		{
			int length = GetLength();
			for (int i = startIndex; i < length; i++)
			{
				if (mData[i] == inChar)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual int FindSubstring(FlString token)
		{
			return StringUtils.StringFindSubstring(mData, token.mData);
		}

		public virtual int FindSubstring(FlString token, int from)
		{
			int result = -1;
			if (from >= 0)
			{
				result = StringUtils.StringFindSubstring(mData, token.mData, from);
			}
			return result;
		}

		public virtual FlString Substring(int startIndex, int length)
		{
			int length2 = GetLength();
			if (startIndex >= length2)
			{
				return new FlString();
			}
			int num = length2 - startIndex;
			if (length < 0 || length > num)
			{
				length = num;
			}
			return new FlString(this, startIndex, length);
		}

		public virtual void Assign(int value)
		{
			sbyte[] array = new sbyte[16];
			int num = StringUtils.StringLong(array, value);
			Assign(array, num, 16 - num);
		}

		public virtual void Assign(long value)
		{
			sbyte[] array = new sbyte[32];
			int num = StringUtils.StringLongLong(array, value);
			Assign(array, num, 32 - num);
		}

		public virtual FlString Assign(string @string)
		{
			Assign(StringUtils.CreateString(@string));
			return this;
		}

		public virtual FlString AddAssign(string @string)
		{
			AddAssign(StringUtils.CreateString(@string));
			return this;
		}

		public virtual FlString Add(string @string)
		{
			return Add(StringUtils.CreateString(@string));
		}

		public virtual void ToUpper()
		{
			int length = GetLength();
			for (int i = 0; i < length; i++)
			{
				sbyte charAt = GetCharAt(i);
				if (charAt >= 97 && charAt <= 122)
				{
					ReplaceCharAt(i, (sbyte)(charAt - 97 + 65));
				}
				else if (Memory.MakeUnsignedByte(charAt) >= 224 && Memory.MakeUnsignedByte(charAt) <= 246)
				{
					ReplaceCharAt(i, (sbyte)(Memory.MakeUnsignedByte(charAt) - 224 + 192));
				}
				else if (Memory.MakeUnsignedByte(charAt) >= 248 && Memory.MakeUnsignedByte(charAt) <= 254)
				{
					ReplaceCharAt(i, (sbyte)(Memory.MakeUnsignedByte(charAt) - 248 + 216));
				}
			}
		}

		public virtual void ToLower()
		{
			int length = GetLength();
			for (int i = 0; i < length; i++)
			{
				sbyte charAt = GetCharAt(i);
				if (charAt >= 65 && charAt <= 90)
				{
					ReplaceCharAt(i, (sbyte)(charAt - 65 + 97));
				}
				else if (Memory.MakeUnsignedByte(charAt) >= 192 && Memory.MakeUnsignedByte(charAt) <= 214)
				{
					ReplaceCharAt(i, (sbyte)(Memory.MakeUnsignedByte(charAt) - 192 + 224));
				}
				else if (Memory.MakeUnsignedByte(charAt) >= 216 && Memory.MakeUnsignedByte(charAt) <= 222)
				{
					ReplaceCharAt(i, (sbyte)(Memory.MakeUnsignedByte(charAt) - 216 + 248));
				}
			}
		}

		public virtual void Trim()
		{
			int length = GetLength();
			int i = 0;
			int num = length - 1;
			for (; i < length; i++)
			{
				sbyte charAt = GetCharAt(i);
				if (charAt != 32 && charAt != 9 && charAt != 10 && charAt != 13)
				{
					break;
				}
			}
			while (num > i)
			{
				sbyte charAt2 = GetCharAt(num);
				if (charAt2 != 32 && charAt2 != 9 && charAt2 != 10 && charAt2 != 13)
				{
					break;
				}
				num--;
			}
			if (i > 0 || num < length - 1)
			{
				Assign(Substring(i, num - i + 1));
			}
		}

		public virtual void RemoveCharAt(int inPosition, int nbChar)
		{
			do
			{
				mData[inPosition] = mData[inPosition + nbChar];
			}
			while (mData[inPosition++] != 0);
		}

		public virtual void InsertCharAt(int inPosition, sbyte inLetter)
		{
			int length = GetLength();
			if (inPosition <= length)
			{
				GuaranteeCapacity(length + 1);
				for (int num = length + 1; num > inPosition; num--)
				{
					mData[num] = mData[num - 1];
				}
				mData[inPosition] = inLetter;
			}
		}

		public virtual void ReplaceCharAt(int inPosition, sbyte inLetter)
		{
			mData[inPosition] = inLetter;
		}

		public virtual void ReplaceStringAt(int inPosition, FlString inString, int nbCharToReplace)
		{
			int length = inString.GetLength();
			if (length < nbCharToReplace)
			{
				for (int i = 0; i < length; i++)
				{
					ReplaceCharAt(inPosition + i, inString.GetCharAt(i));
				}
				RemoveCharAt(inPosition + length, nbCharToReplace - length);
				return;
			}
			for (int j = 0; j < nbCharToReplace; j++)
			{
				ReplaceCharAt(inPosition + j, inString.GetCharAt(j));
			}
			for (int k = nbCharToReplace; k < length; k++)
			{
				InsertCharAt(inPosition + k, inString.GetCharAt(k));
			}
		}

		public virtual void ReplaceOccurencesOfBy(FlString toRemove, FlString toReplace)
		{
			int length = toRemove.GetLength();
			int length2 = toReplace.GetLength();
			int from = 0;
			while (true)
			{
				from = FindSubstring(toRemove, from);
				if (from != -1)
				{
					ReplaceStringAt(from, toReplace, length);
					from += length2;
					continue;
				}
				break;
			}
		}

		public virtual sbyte[] ToRawString()
		{
			return mData;
		}

		public static void SkipStream(Stream stream)
		{
			int a = stream.ReadShort();
			stream.Skip(a);
		}

		public virtual void OnSerialize(Package p)
		{
			short t = 0;
			t = p.SerializeIntrinsic(t);
			mData = null;
			sbyte[] t2 = null;
			t2 = p.SerializeIntrinsics(t2, t);
			mData = t2;
		}

		public static FlString DecodeUTF8(sbyte[] utf8EncodedString)
		{
			int i;
			for (i = 0; utf8EncodedString[i] != 0; i++)
			{
			}
			sbyte[] array = new sbyte[i + 1];
			ConvertUtf8ToChar(utf8EncodedString, array);
			FlString result = new FlString(array);
			array = null;
			return result;
		}

		public static int ConvertUtf8ToChar(sbyte[] source, sbyte[] dest)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			do
			{
				if ((source[num] & 0xF0) == 240)
				{
					dest[num2] = 63;
					sbyte b = source[num];
					do
					{
						num++;
					}
					while (((uint)(b = (sbyte)(b << 1)) & 0x80u) != 0);
				}
				else if ((source[num] & 0xE0) == 224)
				{
					sbyte b2 = source[num];
					sbyte b3 = source[num + 1];
					sbyte b4 = source[num + 2];
					num += 3;
					b2 = (sbyte)(b2 & 0xF);
					b3 = (sbyte)(b3 & 0x3F);
					b4 = (sbyte)(b4 & 0x3F);
					sbyte b5 = (sbyte)((b4 & 0x3F) | ((b3 & 3) << 6));
					sbyte b6 = (sbyte)((b3 >> 2) | (b2 << 4));
					dest[num2] = (sbyte)((b6 << 8) | Memory.MakeUnsignedByte(b5));
				}
				else if ((source[num] & 0xC0) == 192)
				{
					sbyte b7 = source[num];
					sbyte b8 = source[num + 1];
					num += 2;
					b7 = (sbyte)(b7 & 0x1F);
					b8 = (sbyte)(b8 & 0x3F);
					sbyte b9 = (sbyte)((b8 & 0x3F) | ((b7 & 3) << 6));
					sbyte b10 = (sbyte)(b7 >> 2);
					dest[num2] = (sbyte)((b10 << 8) | Memory.MakeUnsignedByte(b9));
				}
				else if ((source[num] & 0x80) == 0)
				{
					if (source[num] == 0)
					{
						flag = true;
					}
					dest[num2] = source[num];
					num++;
				}
				else
				{
					dest[num2] = 63;
					num++;
				}
				num2++;
			}
			while (!flag);
			return num2;
		}

		public static int ConvertCharToUtf8(sbyte[] source, sbyte[] dest)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			do
			{
				if ((Memory.MakeUnsignedByte(source[num]) & 0xFF80) == 0)
				{
					if (source[num] == 0)
					{
						flag = true;
					}
					dest[num2] = source[num];
					num2++;
				}
				else if ((Memory.MakeUnsignedByte(source[num]) & 0xF800) == 0)
				{
					sbyte b = source[num];
					sbyte b2 = (sbyte)(Memory.MakeUnsignedByte(source[num]) >> 8);
					sbyte b3 = (sbyte)(0x80 | (b & 0x3F));
					sbyte b4 = (sbyte)(0xC0 | ((b2 & 7) << 2) | (Memory.MakeUnsignedByte(b) >> 6));
					dest[num2] = b4;
					dest[num2 + 1] = b3;
					num2 += 2;
				}
				else if (((uint)Memory.MakeUnsignedByte(source[num]) & 0xF800u) != 0)
				{
					sbyte b5 = source[num];
					sbyte b6 = (sbyte)(Memory.MakeUnsignedByte(source[num]) >> 8);
					sbyte b7 = (sbyte)(0x80 | (b5 & 0x3F));
					sbyte b8 = (sbyte)(0x80 | ((b6 & 0xF) << 2) | (Memory.MakeUnsignedByte(b5) >> 6));
					sbyte b9 = (sbyte)(0xE0 | (Memory.MakeUnsignedByte((sbyte)(b6 & 0xF0)) >> 4));
					dest[num2] = b9;
					dest[num2 + 1] = b8;
					dest[num2 + 2] = b7;
					num2 += 3;
				}
				else
				{
					dest[num2] = 63;
					num2++;
				}
				num++;
			}
			while (!flag);
			return num2;
		}

		public static int GetUtf8EncodedDataSize(sbyte[] source)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			do
			{
				if (((uint)Memory.MakeUnsignedByte(source[num2]) & 0xFF80u) != 0)
				{
					num = (((Memory.MakeUnsignedByte(source[num2]) & 0xF800) == 0) ? (num + 2) : (((Memory.MakeUnsignedByte(source[num2]) & 0xF800) == 0) ? (num + 1) : (num + 3)));
				}
				else
				{
					if (source[num2] == 0)
					{
						flag = true;
					}
					num++;
				}
				num2++;
			}
			while (!flag);
			return num;
		}

		public override bool Equals(object obj)
		{
			if (obj is FlString)
			{
				return Equals((FlString)obj);
			}
			return false;
		}

		public virtual int ParseTaggedString(short[] segmentIndex, sbyte[] segmentFont, int maxStackSize)
		{
			int num = 0;
			int num2 = 0;
			int length = GetLength();
			sbyte[] array = new sbyte[maxStackSize];
			sbyte[] array2 = new sbyte[length];
			segmentIndex[num] = 0;
			segmentFont[num++] = 0;
			array[num2] = 0;
			short num3 = 0;
			for (short num4 = 0; num4 < (short)length; num4 = (short)(num4 + 1))
			{
				if (num4 + 3 <= (short)length && mData[num4] == 60 && mData[num4 + 2] == 62)
				{
					if (mData[num4 + 1] == 47)
					{
						num2--;
						if (segmentIndex[num - 1] != num3 && segmentFont[num - 1] != array[num2])
						{
							segmentIndex[num] = num3;
							segmentFont[num++] = array[num2];
						}
						else
						{
							segmentFont[num - 1] = array[num2];
						}
						num4 = (short)(num4 + 2);
					}
					else if (mData[num4 + 1] >= 48 && mData[num4 + 1] <= 57)
					{
						sbyte b = (sbyte)(mData[num4 + 1] - 48);
						if (segmentIndex[num - 1] != num3 && segmentFont[num - 1] != b)
						{
							segmentIndex[num] = num3;
							segmentFont[num++] = b;
						}
						else
						{
							segmentFont[num - 1] = b;
						}
						array[++num2] = b;
						num4 = (short)(num4 + 2);
					}
					else
					{
						array2[num3++] = mData[num4];
					}
				}
				else
				{
					array2[num3++] = mData[num4];
				}
			}
			segmentIndex[num] = (short)(num3 + 1);
			segmentFont[num++] = 0;
			if (num > 2)
			{
				Assign(array2, 0, num3);
			}
			array = null;
			array2 = null;
			return num;
		}

		public virtual void GuaranteeCapacity(int size)
		{
			if (size + 1 > GetCapacity())
			{
				int num = size + 1 >> 4;
				int num2 = num + 1 << 4;
				sbyte[] source = mData;
				mData = new sbyte[num2];
				StringUtils.StringCopy(mData, source);
				source = null;
			}
		}

		public virtual void Assign(sbyte[] str, int startIndex, int nbChar)
		{
			if (mData != str)
			{
				mData = null;
				mData = new sbyte[nbChar + 1];
				StringUtils.StringNCopy(mData, str, 0, startIndex, nbChar);
				mData[GetCapacity() - 1] = 0;
			}
		}

		public virtual int GetCapacity()
		{
			return mData.Length;
		}

		public virtual void Write(Stream stream)
		{
			Write(stream, true);
		}

		public virtual int FindChar(sbyte inChar)
		{
			return FindChar(inChar, 0);
		}

		public virtual void RemoveCharAt(int inPosition)
		{
			RemoveCharAt(inPosition, 1);
		}

		public static FlString[] InstArrayFlString(int size)
		{
			FlString[] array = new FlString[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlString();
			}
			return array;
		}

		public static FlString[][] InstArrayFlString(int size1, int size2)
		{
			FlString[][] array = new FlString[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlString[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlString();
				}
			}
			return array;
		}

		public static FlString[][][] InstArrayFlString(int size1, int size2, int size3)
		{
			FlString[][][] array = new FlString[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlString[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlString[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlString();
					}
				}
			}
			return array;
		}
	}
}
