using System.Globalization;

namespace ca.jamdat.flight
{
	public class FlLang
	{
		public const short uninitialized = 0;

		public const short undefined = 1;

		public const short menu = 2;

		public const short Be = 3;

		public const short Belarusian = 3;

		public const short Bg = 4;

		public const short Bulgarian = 4;

		public const short Bs = 5;

		public const short Bosnian = 5;

		public const short Ca = 6;

		public const short Catalan = 6;

		public const short Cs = 7;

		public const short Czech = 7;

		public const short Da = 8;

		public const short Danish = 8;

		public const short De = 9;

		public const short German = 9;

		public const short El = 10;

		public const short Greek = 10;

		public const short En = 11;

		public const short English = 11;

		public const short Es = 12;

		public const short Spanish = 12;

		public const short Fi = 13;

		public const short Finnish = 13;

		public const short Fr = 14;

		public const short French = 14;

		public const short Ga = 15;

		public const short Irish = 15;

		public const short Hr = 16;

		public const short Croatian = 16;

		public const short Hu = 17;

		public const short Hungarian = 17;

		public const short Id = 18;

		public const short Indonesian = 18;

		public const short It = 19;

		public const short Italian = 19;

		public const short Ja = 20;

		public const short Japanese = 20;

		public const short Ka = 21;

		public const short Georgian = 21;

		public const short Ko = 22;

		public const short Korean = 22;

		public const short Lt = 23;

		public const short Lithuanian = 23;

		public const short Mk = 24;

		public const short Macedonian = 24;

		public const short Ms = 25;

		public const short Malay = 25;

		public const short Nl = 26;

		public const short Dutch = 26;

		public const short No = 27;

		public const short Norwegian = 27;

		public const short Pl = 28;

		public const short Polish = 28;

		public const short Pt = 29;

		public const short Portuguese = 29;

		public const short Ro = 30;

		public const short Romanian = 30;

		public const short Ru = 31;

		public const short Russian = 31;

		public const short Sk = 32;

		public const short Slovak = 32;

		public const short Sl = 33;

		public const short Slovenian = 33;

		public const short Sq = 34;

		public const short Albanian = 34;

		public const short Sr = 35;

		public const short Serbian = 35;

		public const short Sv = 36;

		public const short Swedish = 36;

		public const short Th = 37;

		public const short Thai = 37;

		public const short Tr = 38;

		public const short Turkish = 38;

		public const short Uk = 39;

		public const short Ukrainian = 39;

		public const short Vi = 40;

		public const short Vietnamese = 40;

		public const short Zh = 41;

		public const short Chinese = 41;

		public const short Deat = 4105;

		public const short GermanAustria = 4105;

		public const short Dede = 8201;

		public const short GermanGermany = 8201;

		public const short Enau = 4107;

		public const short EnglishAustralia = 4107;

		public const short Enca = 8203;

		public const short EnglishCanada = 8203;

		public const short Engb = 12299;

		public const short EnglishUnitedKingdom = 12299;

		public const short Enus = 16395;

		public const short EnglishUnitedStates = 16395;

		public const short Esco = 4108;

		public const short SpanishColombia = 4108;

		public const short Eses = 8204;

		public const short SpanishSpain = 8204;

		public const short Ptbr = 4125;

		public const short PortugueseBrazil = 4125;

		public const short Ptpt = 8221;

		public const short PortuguesePortugal = 8221;

		public const short Zhcn = 4137;

		public const short SimplifiedChinese = 4137;

		public const short Zhhk = 8233;

		public const short TraditionalChinese = 8233;

		public static short[] mSupportedLang = new short[2] { 11, 1 };

		public static short mBestLangCache = 0;

		public static short GetBestLang()
		{
			switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
			{
			case "de":
				return 9;
			case "en":
				return 11;
			case "es":
				return 12;
			case "fr":
				return 14;
			case "it":
				return 19;
			default:
				return 2;
			}
		}

		public static short GetOSLang()
		{
			short num = 1;
			string text = "NULL";
			if (text == null)
			{
				text = "NULL";
			}
			FlLog.GetInstance().Log(0, 0, StringUtils.CreateString("J2ME OsLangStr " + text));
			num = StrToFlLang(StringUtils.CreateString(text));
			FlLog.GetInstance().Log(0, 0, StringUtils.CreateString("OS Lang"), num);
			return num;
		}

		public static short GetHWManagerLangID(short lang)
		{
			switch (lang & 0xFFF)
			{
			case 14:
				return 2;
			case 12:
				return 3;
			case 9:
				return 4;
			case 19:
				return 5;
			default:
				return 1;
			}
		}

		public static short GetNbSupportedLang()
		{
			return 1;
		}

		public static FlString FlLangToStr(short lang)
		{
			int num = lang & 0xFFF;
			if (num == 11)
			{
				return StringUtils.CreateString("en");
			}
			return StringUtils.CreateString("en");
		}

		public static short StrToFlLang(FlString langStr)
		{
			if (langStr.GetLength() < 2)
			{
				return 1;
			}
			langStr.ToLower();
			int num = 3;
			sbyte[] array = new sbyte[3] { 32, 95, 45 };
			for (int i = 0; i < num; i++)
			{
				int num2 = langStr.FindChar(array[i]);
				if (num2 != -1)
				{
					langStr.RemoveCharAt(num2, 1);
				}
			}
			if (langStr.Equals(StringUtils.CreateString("menu")))
			{
				return 2;
			}
			if (langStr.Equals(StringUtils.CreateString("enau")))
			{
				return 4107;
			}
			if (langStr.Equals(StringUtils.CreateString("enca")))
			{
				return 8203;
			}
			if (langStr.Equals(StringUtils.CreateString("engb")))
			{
				return 12299;
			}
			if (langStr.Equals(StringUtils.CreateString("enus")))
			{
				return 16395;
			}
			langStr.Assign(langStr.Substring(0, 2));
			if (langStr.Equals(StringUtils.CreateString("en")))
			{
				return 11;
			}
			if (langStr.Equals(StringUtils.CreateString("ae")))
			{
				return 16395;
			}
			return 1;
		}

		public static short Localize(short lang)
		{
			short result = 1;
			for (int i = 0; i < GetNbSupportedLang(); i++)
			{
				if (lang == mSupportedLang[i])
				{
					return mSupportedLang[i];
				}
				if ((lang & 0xFFF) == (mSupportedLang[i] & 0xFFF))
				{
					result = mSupportedLang[i];
				}
			}
			return result;
		}

		public static FlLang[] InstArrayFlLang(int size)
		{
			FlLang[] array = new FlLang[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlLang();
			}
			return array;
		}

		public static FlLang[][] InstArrayFlLang(int size1, int size2)
		{
			FlLang[][] array = new FlLang[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlLang[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlLang();
				}
			}
			return array;
		}

		public static FlLang[][][] InstArrayFlLang(int size1, int size2, int size3)
		{
			FlLang[][][] array = new FlLang[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlLang[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlLang[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlLang();
					}
				}
			}
			return array;
		}
	}
}
