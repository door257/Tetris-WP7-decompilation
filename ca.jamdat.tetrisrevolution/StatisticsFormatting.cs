using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class StatisticsFormatting
	{
		public const sbyte statisticLong = 0;

		public const sbyte statisticShortTime = 1;

		public const sbyte statisticLongTime = 2;

		public const sbyte statisticTPM = 3;

		public const sbyte statisticPercent = 4;

		public const sbyte statisticLongPercent = 5;

		public const sbyte statisticBioStatPercent = 6;

		public const sbyte statisticBioStatDecimal = 7;

		public const sbyte statisticBioStatSeconds = 8;

		public const sbyte statisticTypeCount = 9;

		public const sbyte symbolDecimalSeperator = 0;

		public const sbyte symbolPercentSign = 1;

		public const sbyte symbolSecondUnit = 2;

		public static FlString CreateStatisticString(int statisticValue, sbyte statisticType)
		{
			FlString flString = new FlString();
			AddStatisticToString(flString, statisticValue, statisticType);
			return flString;
		}

		public static void AddStatisticToString(FlString @string, int statisticValue, sbyte statisticType)
		{
			switch (statisticType)
			{
			case 1:
				Utilities.AddMSToStringFromMillisecond(@string, statisticValue);
				break;
			case 2:
				Utilities.AddHMSToStringFromMillisecond(@string, statisticValue);
				break;
			case 3:
				AddSignificandToString(@string, statisticValue, false, false);
				break;
			case 4:
				AddSignificandToString(@string, statisticValue);
				if (GameApp.Get().GetLanguageManager().GetLanguage() == 14)
				{
					@string.AddAssign(StringUtils.CreateString(" "));
				}
				@string.AddAssign(GetStatisticSymbolString(1));
				break;
			case 5:
				@string.AddAssign(new FlString(statisticValue));
				@string.AddAssign(GetStatisticSymbolString(1));
				break;
			case 6:
				AddSignificandToString(@string, statisticValue);
				@string.AddAssign(GetStatisticSymbolString(1));
				break;
			case 7:
				AddSignificandToString(@string, statisticValue, true, true);
				break;
			case 8:
				AddSignificandToString(@string, statisticValue, true, true);
				@string.AddAssign(" ");
				@string.AddAssign(GetStatisticSymbolString(2));
				break;
			default:
				@string.AddAssign(new FlString(statisticValue));
				break;
			}
		}

		public static sbyte GetGameStatisticType(sbyte id)
		{
			sbyte b = 9;
			sbyte b2 = id;
			if (b2 == 19)
			{
				return 1;
			}
			return 0;
		}

		public static sbyte GetCareerHighestStatisticType(sbyte id)
		{
			sbyte b = 9;
			switch (id)
			{
			case 1:
				return 1;
			case 2:
				return 3;
			default:
				return 0;
			}
		}

		public static sbyte GetCareerStatisticType(sbyte id)
		{
			sbyte b = 9;
			switch (id)
			{
			case 16:
				return 2;
			case 18:
				return 3;
			default:
				return 0;
			}
		}

		public static sbyte GetFeatStatisticType(sbyte id)
		{
			sbyte b = 9;
			switch (id)
			{
			case 4:
				return 2;
			case 5:
				return 4;
			default:
				return 0;
			}
		}

		public static sbyte GetBioStatisticType(sbyte id)
		{
			sbyte result = 9;
			switch (id)
			{
			case 0:
			case 1:
			case 4:
			case 5:
			case 7:
				result = 6;
				break;
			case 2:
			case 3:
				result = 7;
				break;
			case 6:
				result = 8;
				break;
			}
			return result;
		}

		public static int ConvertF32Toint(F32 fixedPointValue, int floatingPointValue)
		{
			int num = fixedPointValue.ToInt(floatingPointValue);
			long rightMask = F32.GetRightMask(floatingPointValue);
			fixedPointValue.value &= (int)rightMask;
			for (int num2 = 1; num2 < 100; num2 *= 10)
			{
				fixedPointValue = fixedPointValue.Mul(10);
				int num3 = fixedPointValue.ToInt(floatingPointValue);
				fixedPointValue.value &= (int)rightMask;
				num = num * 10 + num3;
			}
			return num;
		}

		public static void AddSignificandToString(FlString @string, int statisticValue, bool significand, bool rounding)
		{
			@string.AddAssign(new FlString(GetIntegerPart(statisticValue, !significand && rounding)));
			if (significand)
			{
				@string.AddAssign(GetStatisticSymbolString(0));
				@string.AddAssign(new FlString(GetSignificandPart(statisticValue, rounding)));
			}
		}

		public static int GetIntegerPart(int statisticValue, bool rounding)
		{
			return (statisticValue + (rounding ? 50 : 0)) / 100;
		}

		public static int GetSignificandPart(int statisticValue, bool rounding)
		{
			return (statisticValue - GetIntegerPart(statisticValue, false) * 100 + (rounding ? 5 : 0)) / 10;
		}

		public static FlString GetStatisticSymbolString(sbyte symbolId)
		{
			FlString flString = new FlString();
			switch (symbolId)
			{
			case 0:
				flString.Assign(Utilities.GetStringFromPackage(83, -2144239522));
				break;
			case 2:
				flString.Assign(Utilities.GetStringFromPackage(82, -2144239522));
				break;
			case 1:
				flString.Assign(StringUtils.CreateString("%"));
				break;
			}
			return flString;
		}

		public static void AddSignificandToString(FlString @string, int statisticValue)
		{
			AddSignificandToString(@string, statisticValue, false);
		}

		public static void AddSignificandToString(FlString @string, int statisticValue, bool significand)
		{
			AddSignificandToString(@string, statisticValue, significand, true);
		}

		public static StatisticsFormatting[] InstArrayStatisticsFormatting(int size)
		{
			StatisticsFormatting[] array = new StatisticsFormatting[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new StatisticsFormatting();
			}
			return array;
		}

		public static StatisticsFormatting[][] InstArrayStatisticsFormatting(int size1, int size2)
		{
			StatisticsFormatting[][] array = new StatisticsFormatting[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new StatisticsFormatting[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new StatisticsFormatting();
				}
			}
			return array;
		}

		public static StatisticsFormatting[][][] InstArrayStatisticsFormatting(int size1, int size2, int size3)
		{
			StatisticsFormatting[][][] array = new StatisticsFormatting[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new StatisticsFormatting[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new StatisticsFormatting[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new StatisticsFormatting();
					}
				}
			}
			return array;
		}
	}
}
