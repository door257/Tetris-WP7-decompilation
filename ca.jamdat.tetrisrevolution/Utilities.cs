using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Utilities
	{
		public static int GetKeyValue(int key)
		{
			if (key >= 17 && key <= 26)
			{
				return key - 17;
			}
			return -1;
		}

		public static int GetSelectionIndexFromCommmand(Selector selector, int command)
		{
			int numSelections = selector.GetNumSelections();
			for (int i = 0; i < numSelections; i++)
			{
				if (selector.GetSelectionAt(i).GetCommand() == command)
				{
					return i;
				}
			}
			return -1;
		}

		public static void AddSelection(Selector selector, Selection selection, int index)
		{
			selection.SetVisible(true);
			int numSelections = selector.GetNumSelections();
			selector.SetMaxNumElements(numSelections + 1);
			selector.SetNumSelections(numSelections + 1);
			short rectHeight = selection.GetRectHeight();
			for (int num = numSelections; num > index; num--)
			{
				selector.SetSelectionAt(num, selector.GetSelectionAt(num - 1));
				selector.GetSelectionAt(num).SetTopLeft(0, (short)(num * rectHeight));
			}
			selector.SetSelectionAt(index, selection);
			selector.GetSelectionAt(index).SetEnabledState(true);
			selection.SetTopLeft(0, (short)(index * rectHeight));
			selector.UpdateScroller();
		}

		public static void RemoveSelection(Selector selector, int index)
		{
			selector.GetSelectionAt(index).SetEnabledState(false);
			selector.GetElementAt(index).SetVisible(false);
			int numSelections = selector.GetNumSelections();
			short rectHeight = selector.GetElementAt(index).GetRectHeight();
			for (int i = index; i < numSelections - 1; i++)
			{
				selector.SetElementAt(i, selector.GetElementAt(i + 1));
				selector.GetElementAt(i).SetTopLeft(0, (short)(i * rectHeight));
			}
			selector.SetNumElements(numSelections - 1);
			selector.UpdateScroller();
		}

		public static void Replace(FlString str, FlString strToFind, FlString strToUse)
		{
			for (int num = str.FindSubstring(strToFind); num >= 0; num = str.FindSubstring(strToFind))
			{
				FlString flString = new FlString();
				flString.AddAssign(str.Substring(0, num));
				flString.AddAssign(strToUse);
				flString.AddAssign(str.Substring(num + strToFind.GetLength(), str.GetLength() - 1));
				str.Assign(flString);
			}
		}

		public static FlString CreateMSTimeStringFromMillisecond(int millisecond)
		{
			FlString flString = new FlString();
			AddMSToStringFromMillisecond(flString, millisecond);
			return flString;
		}

		public static void AddMSToStringFromMillisecond(FlString time, int millisecond)
		{
			int num = millisecond;
			int num2 = 0;
			int num3 = 0;
			num2 = num / 60000;
			num -= num2 * 60000;
			num3 = num / 1000;
			if (num2 < 10)
			{
				time.AddAssign(new FlString(0));
			}
			time.AddAssign(new FlString(num2));
			time.AddAssign(StringUtils.CreateString(":"));
			if (num3 < 10)
			{
				time.AddAssign(new FlString(0));
			}
			time.AddAssign(new FlString(num3));
		}

		public static FlString CreateHMSTimeStringFromMillisecond(int millisecond)
		{
			FlString flString = new FlString();
			AddHMSToStringFromMillisecond(flString, millisecond);
			return flString;
		}

		public static void AddHMSToStringFromMillisecond(FlString time, int millisecond)
		{
			int num = millisecond / 1000;
			int num2 = num;
			int num3 = 0;
			int num4 = 0;
			num4 = num2 / 3600;
			num2 -= num4 * 3600;
			num3 = num2 / 60;
			num = num2 - num3 * 60;
			if (num4 < 10)
			{
				time.AddAssign(new FlString(0));
			}
			time.AddAssign(new FlString(num4));
			time.AddAssign(StringUtils.CreateString(":"));
			if (num3 < 10)
			{
				time.AddAssign(new FlString(0));
			}
			time.AddAssign(new FlString(num3));
			time.AddAssign(StringUtils.CreateString(":"));
			if (num < 10)
			{
				time.AddAssign(new FlString(0));
			}
			time.AddAssign(new FlString(num));
		}

		public static void AddPercentToString(FlString @string)
		{
			if (GameApp.Get().GetLanguageManager().GetLanguage() == 14)
			{
				@string.AddAssign(StringUtils.CreateString(" "));
			}
			@string.AddAssign("%");
		}

		public static FlString GetMenuStringFromPackage(int entryPoint)
		{
			return GetStringFromPackage(entryPoint, -2144075681);
		}

		public static FlString GetGameStringFromPackage(int entryPoint)
		{
			return GetStringFromPackage(entryPoint, -2143911840);
		}

		public static FlString GetStringFromPackage(int entryPoint, int packageId)
		{
			GameApp.Get();
			FlString flString = null;
			MetaPackage package = GameLibrary.GetPackage(packageId);
			flString = FlString.Cast(package.GetPackage().GetEntryPoint(entryPoint), null);
			GameLibrary.ReleasePackage(package);
			return flString;
		}

		public static FlString GetGameVariantString(int variant)
		{
			FlString result = null;
			switch (variant)
			{
			case 2:
				result = EntryPoint.GetFlString(-2144239522, 41);
				break;
			case 3:
				result = EntryPoint.GetFlString(-2144239522, 42);
				break;
			case 4:
				result = EntryPoint.GetFlString(-2144239522, 43);
				break;
			case 5:
				result = EntryPoint.GetFlString(-2144239522, 44);
				break;
			case 6:
				result = EntryPoint.GetFlString(-2144239522, 45);
				break;
			case 7:
				result = EntryPoint.GetFlString(-2144239522, 46);
				break;
			case 8:
				result = EntryPoint.GetFlString(-2144239522, 47);
				break;
			case 9:
				result = EntryPoint.GetFlString(-2144239522, 48);
				break;
			case 10:
				result = EntryPoint.GetFlString(-2144239522, 49);
				break;
			case 1:
				result = EntryPoint.GetFlString(-2144239522, 40);
				break;
			case 0:
				result = EntryPoint.GetFlString(-2144239522, 39);
				break;
			case 11:
				result = EntryPoint.GetFlString(-2144239522, 50);
				break;
			}
			return result;
		}

		public static FlString GetFeatDescriptionString(int variant)
		{
			int firstStringIdx = 95;
			int lastStringIdx = 106;
			return GetCommonStringInRange(variant, firstStringIdx, lastStringIdx, 12);
		}

		public static FlString GetUnlockedVariantString(int variantId)
		{
			int firstStringIdx = 107;
			int lastStringIdx = 118;
			return GetCommonStringInRange(variantId, firstStringIdx, lastStringIdx, 12);
		}

		public static FlString GetUnlockedCareerFeatString(int careerFeatId)
		{
			int firstStringIdx = 84;
			int lastStringIdx = 89;
			return GetCommonStringInRange(careerFeatId, firstStringIdx, lastStringIdx, 6);
		}

		public static FlString GetUnlockedAdvancedFeatString(int advancedFeatId)
		{
			int firstStringIdx = 90;
			int lastStringIdx = 94;
			return GetCommonStringInRange(advancedFeatId, firstStringIdx, lastStringIdx, 5);
		}

		public static FlString ReverseString(FlString @string)
		{
			int length = @string.GetLength();
			FlString flString = new FlString();
			flString.InsertCharAt(0, @string.GetCharAt(--length));
			while (length > 0)
			{
				flString.InsertCharAt(flString.GetLength(), @string.GetCharAt(--length));
			}
			@string = null;
			return flString;
		}

		public static FlString GetMasterReplayAuthorString(int replay)
		{
			FlString result = null;
			switch (replay)
			{
			case 7:
				result = EntryPoint.GetFlString(-2144239522, 70);
				break;
			case 8:
				result = EntryPoint.GetFlString(-2144239522, 71);
				break;
			case 9:
				result = EntryPoint.GetFlString(-2144239522, 72);
				break;
			case 10:
				result = EntryPoint.GetFlString(-2144239522, 73);
				break;
			case 11:
				result = EntryPoint.GetFlString(-2144239522, 74);
				break;
			case 12:
				result = EntryPoint.GetFlString(-2144239522, 75);
				break;
			case 13:
				result = EntryPoint.GetFlString(-2144239522, 76);
				break;
			case 14:
				result = EntryPoint.GetFlString(-2144239522, 77);
				break;
			case 15:
				result = EntryPoint.GetFlString(-2144239522, 78);
				break;
			case 16:
				result = EntryPoint.GetFlString(-2144239522, 79);
				break;
			case 17:
				result = EntryPoint.GetFlString(-2144239522, 80);
				break;
			case 18:
				result = EntryPoint.GetFlString(-2144239522, 81);
				break;
			}
			return result;
		}

		public static int GetVariantIconFrameIndex(int variant)
		{
			int result = 0;
			switch (variant)
			{
			case 2:
				result = 1;
				break;
			case 3:
				result = 4;
				break;
			case 4:
				result = 10;
				break;
			case 5:
				result = 3;
				break;
			case 6:
				result = 9;
				break;
			case 7:
				result = 6;
				break;
			case 8:
				result = 5;
				break;
			case 9:
				result = 8;
				break;
			case 10:
				result = 7;
				break;
			case 1:
				result = 2;
				break;
			case 0:
				result = 0;
				break;
			case 11:
				result = 11;
				break;
			}
			return result;
		}

		public static void GetRomanNumeral(int num, FlString @string)
		{
			while (num > 0)
			{
				while (num >= 5)
				{
					while (num >= 10)
					{
						@string.AddAssign("X");
						num -= 10;
					}
					if (num == 9)
					{
						@string.AddAssign("IX");
						num -= 9;
					}
					else if (num >= 5)
					{
						@string.AddAssign("V");
						num -= 5;
					}
				}
				switch (num)
				{
				case 4:
					@string.AddAssign("IV");
					num -= 4;
					break;
				default:
					@string.AddAssign("I");
					num--;
					break;
				case 0:
					break;
				}
			}
		}

		public static int PowerFunction(int @base, int exponent)
		{
			int num = 1;
			for (int i = 0; i < exponent; i++)
			{
				num *= @base;
			}
			return num;
		}

		public static F32 SafeDivide(int value, int divisor, int floatingPoint)
		{
			int num = 31 - floatingPoint;
			int num2 = 1 << num;
			int num3 = 1;
			while (value / num3 >= num2)
			{
				num3 *= 2;
			}
			int intValue = value / num3;
			int intValue2 = value % num3;
			F32 f = new F32(F32.FromInt(intValue, floatingPoint).Div(divisor));
			F32 other = new F32(F32.FromInt(intValue2, floatingPoint).Div(divisor));
			return new F32(f.Mul(num3).Add(other));
		}

		public static FlString GetCommonStringInRange(int stringOffsetIdx, int firstStringIdx, int lastStringIdx, int stringRangeSize)
		{
			int entryPoint = firstStringIdx + stringOffsetIdx;
			return GetStringFromPackage(entryPoint, -2144239522);
		}

		public static Utilities[] InstArrayUtilities(int size)
		{
			Utilities[] array = new Utilities[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Utilities();
			}
			return array;
		}

		public static Utilities[][] InstArrayUtilities(int size1, int size2)
		{
			Utilities[][] array = new Utilities[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Utilities[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Utilities();
				}
			}
			return array;
		}

		public static Utilities[][][] InstArrayUtilities(int size1, int size2, int size3)
		{
			Utilities[][][] array = new Utilities[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Utilities[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Utilities[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Utilities();
					}
				}
			}
			return array;
		}
	}
}
