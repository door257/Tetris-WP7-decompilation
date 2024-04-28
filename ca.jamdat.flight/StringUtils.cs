using System;

namespace ca.jamdat.flight
{
	public class StringUtils
	{
		public static FlString CreateString(string javaString)
		{
			sbyte[] array = new sbyte[javaString.Length];
			for (int i = 0; i < javaString.Length; i++)
			{
				array[i] = (sbyte)javaString[i];
			}
			FlString flString = new FlString();
			flString.Assign(array, 0, array.Length);
			return flString;
		}

		public static FlString CreateStringPtr(string javaString)
		{
			return CreateString(javaString);
		}

		public static FlString CreateString(sbyte[] @string)
		{
			return new FlString(@string);
		}

		public static FlString CreateString(FlString @string)
		{
			return @string;
		}

		public static FlString CreateStringPtr(sbyte[] @string)
		{
			return new FlString(@string);
		}

		public static FlString CreateFromANSIString(sbyte[] data, int size, int offset)
		{
			FlString flString = new FlString();
			flString.mData = null;
			flString.mData = new sbyte[size + 1];
			Memory.Copy(flString.mData, 0, data, offset, size);
			flString.mData[size] = 0;
			return flString;
		}

		public static sbyte[] ToRawString(FlString @string)
		{
			return @string.ToRawString();
		}

		public static FlString StringAdd(FlString string1, sbyte[] string2)
		{
			return string1.Add(string2);
		}

		public static int StringLen(sbyte[] data)
		{
			int i;
			for (i = 0; data[i] != 0; i++)
			{
			}
			return i;
		}

		public static void StringNCopy(sbyte[] dest, sbyte[] source, int startInDest, int startInSource, int len)
		{
			for (int i = 0; i < len; i++)
			{
				dest[startInDest + i] = source[startInSource + i];
				if (source[startInSource + i] == 0)
				{
					break;
				}
			}
		}

		public static void StringNCopy(sbyte[] dest, sbyte[] source, int len)
		{
			StringNCopy(dest, source, 0, 0, len);
		}

		public static void StringCopy(sbyte[] dest, sbyte[] source)
		{
			for (int i = 0; (dest[i] = source[i]) != 0; i++)
			{
			}
		}

		public static void StringCat(sbyte[] dest, sbyte[] source)
		{
			StringNCopy(dest, source, StringLen(dest), 0, StringLen(source) + 1);
		}

		public static void StringCat(FlString dest, sbyte[] source)
		{
			dest.AddAssign(source);
		}

		public static int StringLong(sbyte[] dest, int value)
		{
			bool flag = false;
			if (value < 0)
			{
				flag = true;
				value = -value;
			}
			int num = 16;
			dest[--num] = 0;
			do
			{
				dest[--num] = (sbyte)(48 + value % 10);
				value /= 10;
			}
			while (value != 0);
			if (flag)
			{
				dest[--num] = 45;
			}
			return num;
		}

		public static int StringLongLong(sbyte[] dest, long value)
		{
			bool flag = false;
			if (value < 0)
			{
				flag = true;
				value = -value;
			}
			int num = 32;
			dest[--num] = 0;
			do
			{
				dest[--num] = (sbyte)(48 + value % 10);
				value /= 10;
			}
			while (value != 0);
			if (flag)
			{
				dest[--num] = 45;
			}
			return num;
		}

		public static int StringCmp(sbyte[] src1, sbyte[] src2)
		{
			return StringNCmp(src1, src2, -1);
		}

		public static int StringNCmp(sbyte[] src1, sbyte[] src2, int len)
		{
			sbyte b = 0;
			sbyte b2 = 0;
			for (int i = 0; len < 0 || i < len; i++)
			{
				b = src1[i];
				b2 = src2[i];
				if (b != b2 || b == 0)
				{
					return b - b2;
				}
			}
			return b - b2;
		}

		public static int StringChr(sbyte[] @string, int start, sbyte c)
		{
			do
			{
				if (@string[start] == c)
				{
					return start;
				}
			}
			while (@string[start++] != 0);
			return -1;
		}

		public static int StringRChr(sbyte[] @string, sbyte c)
		{
			int num = StringLen(@string);
			if (num == 0)
			{
				return 0;
			}
			int num2 = num - 1;
			do
			{
				if (@string[num2] == c)
				{
					return num2;
				}
			}
			while (--num2 >= 0);
			return -1;
		}

		public static sbyte[] StringDuplicate(sbyte[] str)
		{
			sbyte[] array = new sbyte[StringLen(str) + 1];
			StringCopy(array, str);
			return array;
		}

		public static int StringFindSubstring(sbyte[] source, sbyte[] token)
		{
			return StringFindSubstring(source, token, 0);
		}

		public static int StringFindSubstring(sbyte[] source, sbyte[] token, int from)
		{
			int num = -1;
			int num2 = 0;
			if (token[num2] == 0)
			{
				return 0;
			}
			if (from != 0 && from >= StringLen(source))
			{
				return -1;
			}
			for (int i = from; source[i] != 0; i++)
			{
				if (source[i] != token[num2])
				{
					continue;
				}
				num = i;
				do
				{
					if (token[num2] == 0)
					{
						return i;
					}
				}
				while (source[num++] == token[num2++]);
				num2 = 0;
			}
			return -1;
		}

		public static void ChangePathSeparator(sbyte[] inString)
		{
			sbyte b = 92;
			int num = StringLen(inString);
			for (int i = 0; i < num; i++)
			{
				if (inString[i] == 47 || inString[i] == 92)
				{
					inString[i] = b;
				}
			}
		}

		public static bool IsReservedURLCharacter(sbyte c)
		{
			bool result = false;
			switch ((char)(ushort)c)
			{
			case '$':
			case '&':
			case '+':
			case ',':
			case '/':
			case ':':
			case ';':
			case '=':
			case '?':
			case '@':
				result = true;
				break;
			}
			return result;
		}

		public static bool IsUnsafeURLCharacter(sbyte c)
		{
			bool flag = c <= 31 || c >= sbyte.MaxValue;
			if (!flag)
			{
				switch ((char)(ushort)c)
				{
				case ' ':
				case '"':
				case '#':
				case '%':
				case '<':
				case '>':
				case '[':
				case '\\':
				case ']':
				case '^':
				case '`':
				case '{':
				case '|':
				case '}':
				case '~':
					flag = true;
					break;
				}
			}
			return flag;
		}

		public static bool IsValidURL(FlString url)
		{
			bool result = true;
			for (int i = 0; i < url.GetLength(); i++)
			{
				sbyte charAt = url.GetCharAt(i);
				if (charAt != 37 && IsUnsafeURLCharacter(charAt))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public static sbyte ToHexDigit(short n)
		{
			FlString flString = new FlString(CreateString("0123456789abcdef"));
			return flString.GetCharAt(n & 0xF);
		}

		public static short FromHexDigit(sbyte c)
		{
			FlString flString = new FlString(CreateString("0123456789abcdef"));
			if (c >= 65 && c <= 90)
			{
				c = (sbyte)(c - 65 + 97);
			}
			return (short)flString.FindChar(c);
		}

		public static FlString EncodeURL(FlString url, bool encodeReservedCharacters)
		{
			FlString flString = new FlString();
			sbyte[] source = ToRawString(url);
			int utf8EncodedDataSize = FlString.GetUtf8EncodedDataSize(source);
			sbyte[] array = new sbyte[utf8EncodedDataSize];
			FlString.ConvertCharToUtf8(source, array);
			for (int i = 0; i < utf8EncodedDataSize - 1; i++)
			{
				sbyte b = array[i];
				if (IsUnsafeURLCharacter(b) || (encodeReservedCharacters && IsReservedURLCharacter(b)))
				{
					flString.InsertCharAt(flString.GetLength(), 37);
					flString.InsertCharAt(flString.GetLength(), ToHexDigit((short)((b & 0xF0) / 16)));
					flString.InsertCharAt(flString.GetLength(), ToHexDigit((short)(b & 0xF)));
				}
				else
				{
					flString.InsertCharAt(flString.GetLength(), b);
				}
			}
			array = null;
			return flString;
		}

		public static FlString DecodeURL(FlString url)
		{
			int length = url.GetLength();
			sbyte[] array = new sbyte[url.GetLength() + 1];
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				sbyte charAt = url.GetCharAt(i);
				if (charAt == 37 && i <= length - 3)
				{
					short num2 = (short)(FromHexDigit(url.GetCharAt(i + 1)) * 16 + FromHexDigit(url.GetCharAt(i + 2)));
					array[num] = (sbyte)num2;
					i += 2;
				}
				else
				{
					array[num] = charAt;
				}
				num++;
			}
			array[num] = 0;
			FlString result = new FlString(FlString.DecodeUTF8(array));
			array = null;
			return result;
		}

		public static FlString ToLowerCase(FlString @string)
		{
			FlString flString = new FlString(@string);
			int length = flString.GetLength();
			for (int i = 0; i < length; i++)
			{
				sbyte charAt = flString.GetCharAt(i);
				if (charAt >= 65 && charAt <= 90)
				{
					flString.ReplaceCharAt(i, (sbyte)(charAt - 65 + 97));
				}
			}
			return flString;
		}

		public static int StringAtoI(sbyte[] pString)
		{
			return Convert.ToInt32(CreateString(pString).NativeString);
		}

		public static long StringAtoLL(sbyte[] pString)
		{
			return Convert.ToInt64(CreateString(pString).NativeString);
		}

		public static string CreateJavaString(FlString @string)
		{
			return CreateJavaString(@string, 0, @string.GetLength());
		}

		public static string CreateJavaString(FlString @string, int start, int length)
		{
			char[] array = new char[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (char)((uint)@string.GetCharAt(start++) & 0xFFu);
			}
			return new string(array);
		}

		public static int StringToBytes(string str, sbyte[] dest, int posInDest)
		{
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				int num = str[i];
				dest[posInDest++] = (sbyte)((uint)num >> 8);
				dest[posInDest++] = (sbyte)(num & 0xFF);
			}
			return posInDest;
		}

		public static FlString CreateFromANSIString(sbyte[] data, int size)
		{
			return CreateFromANSIString(data, size, 0);
		}

		public static FlString EncodeURL(FlString url)
		{
			return EncodeURL(url, true);
		}

		public static StringUtils[] InstArrayStringUtils(int size)
		{
			StringUtils[] array = new StringUtils[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new StringUtils();
			}
			return array;
		}

		public static StringUtils[][] InstArrayStringUtils(int size1, int size2)
		{
			StringUtils[][] array = new StringUtils[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new StringUtils[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new StringUtils();
				}
			}
			return array;
		}

		public static StringUtils[][][] InstArrayStringUtils(int size1, int size2, int size3)
		{
			StringUtils[][][] array = new StringUtils[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new StringUtils[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new StringUtils[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new StringUtils();
					}
				}
			}
			return array;
		}
	}
}
