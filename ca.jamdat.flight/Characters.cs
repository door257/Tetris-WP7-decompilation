namespace ca.jamdat.flight
{
	public class Characters
	{
		public const sbyte space = 32;

		public const sbyte tabulation = 9;

		public const sbyte endOfString = 0;

		public const sbyte hardReturn = 13;

		public const sbyte carriageReturn = 13;

		public const sbyte lineFeed = 10;

		public const sbyte pageBreak = 12;

		public const sbyte optionalWordBreak = 31;

		public const sbyte softHyphenChar = -83;

		public const sbyte zeroWidthSpace = 29;

		public const sbyte nonBreakingSpace = -96;

		public const short typeInfoCanWrapBefore = 2;

		public const short typeInfoCanWrapAfter = 4;

		public const short typeInfoIsWordSeparator = 8;

		public const short typeInfoIsLineSeparator = 16;

		public const short typeInfoIsOptionalWordBreak = 32;

		public const short typeInfoIsFullwidth = 64;

		public const short typeInfoIsAlphabet = 128;

		public const short typeInfoIsAsianCharacter = 256;

		public const short typeInfoIsLetter = 512;

		public const short typeInfoIsNumber = 1024;

		public const short typeInfoIsSymbol = 2048;

		public const int typeOptionalWordBreak = 32;

		public const int typeWordSeparator = 14;

		public const int typeCarriageReturn = 30;

		public const int typeLineSeparator = 30;

		public const int typeEndOfString = 14;

		public const int typeAlphabetLetter = 644;

		public const int typeAlphabetNumber = 1156;

		public const int typeAlphabetSymbol = 2180;

		public const int typeFullwidthAlphabetLetter = 646;

		public const int typeFullwidthAlphabetNumber = 1158;

		public const int typeFullwidthAlphabetSymbol = 2182;

		public const int typeFullwidthPunctuation = 2180;

		public const int typeThaiConsonant = 6;

		public const int typeThaiLeadingVowel = 6;

		public const int typeThaiFollowingVowel = 6;

		public const int typeThaiBelowVowel = 4;

		public const int typeThaiAboveVowel = 4;

		public const int typeThaiTonemark = 4;

		public const int typeThaiAboveDiacritic = 4;

		public const int typeThaiBelowDiacritic = 4;

		public const int typeThaiDigit = 1030;

		public const int typeThaiSpecialCharacter = 6;

		public const int typeAsianCharacter = 260;

		public const int typeHalfWidthAsianCharacter = 260;

		public const int typeFullWidthAsianCharacter = 324;

		public const int typeAsianPunctuation = 324;

		public const int typeAsianIdeogram = 326;

		public const int typeChineseHanzi = 326;

		public const int typeJapaneseKanji = 326;

		public const int typeJapaneseHiragana = 326;

		public const int typeJapaneseKatakana = 326;

		public const int typeJapaneseHalfWidthKatakana = 260;

		public static int GetCharType(sbyte cAsChar)
		{
			int num = CharToInt(cAsChar);
			switch (cAsChar)
			{
			case 0:
				return 14;
			case -83:
			case 9:
			case 29:
			case 32:
				return 14;
			default:
				switch (cAsChar)
				{
				case 13:
					break;
				case 31:
					return 32;
				default:
					if (cAsChar == 13)
					{
						return 30;
					}
					if (num <= 47)
					{
						return 2180;
					}
					if (num <= 57)
					{
						return 1156;
					}
					if (num <= 64)
					{
						return 2180;
					}
					if (num <= 90)
					{
						return 644;
					}
					if (num <= 96)
					{
						return 2180;
					}
					if (num <= 122)
					{
						return 644;
					}
					if (num <= 191)
					{
						return 2180;
					}
					if (num <= 255)
					{
						return 644;
					}
					return 2180;
				}
				break;
			case 10:
			case 13:
				break;
			}
			return 30;
		}

		public static bool CanWrapAfter(int characterType)
		{
			return TypesFlagAndTest(characterType, 4);
		}

		public static bool CanWrapBefore(int characterType)
		{
			return TypesFlagAndTest(characterType, 2);
		}

		public static bool IsOptionalWordBreak(int characterType)
		{
			return TypesFlagAndTest(characterType, 32);
		}

		public static bool IsWordSeparator(int characterType)
		{
			return TypesFlagAndTest(characterType, 8);
		}

		public static bool IsLineSeparator(int characterType)
		{
			return TypesFlagAndTest(characterType, 16);
		}

		public static int CharToInt(sbyte c)
		{
			return c & 0xFF;
		}

		public static bool TypesFlagAndTest(int characterType, int flags)
		{
			return 0 != (characterType & flags);
		}

		public static bool TypesFlagOrTest(int characterType, int flags)
		{
			return 0 != (characterType | flags);
		}

		public static Characters[] InstArrayCharacters(int size)
		{
			Characters[] array = new Characters[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Characters();
			}
			return array;
		}

		public static Characters[][] InstArrayCharacters(int size1, int size2)
		{
			Characters[][] array = new Characters[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Characters[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Characters();
				}
			}
			return array;
		}

		public static Characters[][][] InstArrayCharacters(int size1, int size2, int size3)
		{
			Characters[][][] array = new Characters[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Characters[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Characters[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Characters();
					}
				}
			}
			return array;
		}
	}
}
