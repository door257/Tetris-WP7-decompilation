namespace ca.jamdat.flight
{
	public class FlConsoleWindow : Viewport
	{
		private struct FlMonitorData
		{
			public FlString mKey;

			public FlString mVal;
		}

		public enum Mode
		{
			ModeUnset,
			ModeHidden,
			ModeShown,
			ModeAbove,
			ModeSaveFileReader
		}

		public enum LetterCase
		{
			LetterCaseUpper,
			LetterCaseLower,
			LetterCaseBoth
		}

		private const int MAX_CONSOLE_LINES = 64;

		private const int MAX_MONITOR_LINES = 8;

		private const int SCROLL_NB_LINES = 5;

		private const int TOUCH_EVT_LENGTH = 3;

		private const int TOUCH_EVT_SIZE = 60;

		private const int KEY_SEQ_LENGTH = 6;

		private Text mConsoleText;

		private Shape mConsoleBkg;

		private Shape mMonitorBkg;

		private Shape mSoftkeyBar;

		private Text mAcceptText;

		private Text mDeclineText;

		private Text mNbLinesText;

		private Text[] mMonitorText;

		private short mConsoleWidth;

		private short mConsoleHeight;

		private Color888 mConsoleColor;

		private Color888 mSoftkeyBarColor;

		private bool mFirstTime;

		private bool mSendKeys;

		private short mKeyIdx;

		private LetterCase mLetterCase;

		private Mode mConsoleMode;

		private bool mConsoleAboveOnly;

		private bool mConsoleInitTextAdded;

		private bool mMonitorPrintKey;

		private FlMonitorData[] mMonitorData;

		private readonly FlString mMonitorSeperatorStr;

		private readonly FlString mMonitorTrueStr;

		private readonly FlString mMonitorFalseStr;

		private readonly FlString mInitTextStr;

		private static readonly short[] DebugConsoleDisplaySequence = new short[6] { 1, 1, 2, 2, 3, 4 };

		private readonly FlString mPageDownStr;

		private readonly FlString mPageUpStr;

		private short mPenIdx;

		private FlConsoleWindow()
		{
			mConsoleHeight = 0;
			mFirstTime = true;
			mSendKeys = false;
			mKeyIdx = 0;
			mConsoleAboveOnly = false;
			mConsoleInitTextAdded = false;
			mMonitorPrintKey = false;
			mPenIdx = 0;
			mMonitorSeperatorStr = new FlString(": ");
			mMonitorTrueStr = new FlString("True");
			mMonitorFalseStr = new FlString("False");
			mInitTextStr = new FlString("\n...........................\nKEY 0: CLEAR CONSOLE\nTOUCH: TAP TOP-RIGHT 3 TIMES: CLEAR CONSOLE\n...........................");
			mPageDownStr = new FlString("DW  ");
			mPageUpStr = new FlString("  UP");
			Viewport instance = FlApplication.GetInstance();
			SetRect(instance.GetRectLeft(), instance.GetRectTop(), instance.GetRectWidth(), instance.GetRectHeight());
			mConsoleWidth = instance.GetRectWidth();
			mConsoleBkg = new Shape();
			mMonitorBkg = new Shape();
			mSoftkeyBar = new Shape();
			mConsoleText = new Text();
			mAcceptText = new Text();
			mDeclineText = new Text();
			mNbLinesText = new Text();
			mConsoleColor = new Color888(0, 0, 0);
			mSoftkeyBarColor = new Color888(128, 128, 128);
			UpdateColors();
			mConsoleText.SetMultiline(true);
			mAcceptText.SetMultiline(false);
			mDeclineText.SetMultiline(false);
			mNbLinesText.SetMultiline(false);
			mMonitorText = new Text[8];
			for (int i = 0; i < 8; i++)
			{
				mMonitorText[i] = new Text();
				mMonitorText[i].SetMultiline(false);
			}
			mNbLinesText.SetAlignment(1);
			mAcceptText.SetAlignment(0);
			mDeclineText.SetAlignment(2);
			ConsoleUpdateLineCount();
			SetLetterCase(LetterCase.LetterCaseBoth);
			ConsoleSetMode(Mode.ModeHidden);
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (msg == -122 && source == this)
			{
				Viewport viewport = ((intParam == 1) ? this : null);
				mConsoleBkg.SetViewport(viewport);
				mMonitorBkg.SetViewport(viewport);
				mConsoleText.SetViewport(viewport);
				mSoftkeyBar.SetViewport(viewport);
				mAcceptText.SetViewport(viewport);
				mDeclineText.SetViewport(viewport);
				mNbLinesText.SetViewport(viewport);
				Text[] array = mMonitorText;
				foreach (Text text in array)
				{
					text.SetViewport(viewport);
				}
			}
			if (mConsoleMode == Mode.ModeShown || mConsoleMode == Mode.ModeSaveFileReader)
			{
				if (msg == -117)
				{
					short penPositionX = FlPenManager.GetPenPositionX(intParam);
					short penPositionY = FlPenManager.GetPenPositionY(intParam);
					short num = (short)(mSoftkeyBar.GetRight() / 4);
					if (penPositionY > mSoftkeyBar.GetRectTop() - 60)
					{
						if (penPositionX <= num)
						{
							ConsoleSetMode(Mode.ModeAbove);
						}
						else if (penPositionX > num && penPositionX <= 2 * num)
						{
							ConsoleScroll(true);
						}
						else if (penPositionX > 2 * num && penPositionX <= 3 * num)
						{
							ConsoleScroll(false);
						}
						else
						{
							ConsoleSetMode(Mode.ModeHidden);
						}
					}
					else if (penPositionX >= mConsoleWidth - 60 && penPositionY <= 60)
					{
						mPenIdx++;
						if (mPenIdx == 3)
						{
							ConsoleClear();
							mPenIdx = 0;
						}
					}
				}
				if (msg == -121)
				{
					switch (intParam)
					{
					case 6:
					case 8:
					case 9:
					case 10:
					case 14:
						ConsoleSetMode(Mode.ModeHidden);
						break;
					case 5:
					case 7:
					case 13:
						ConsoleSetMode(Mode.ModeAbove);
						break;
					case 17:
						ConsoleClear();
						break;
					case 2:
						ConsoleScroll(true);
						break;
					case 1:
						ConsoleScroll(false);
						break;
					}
				}
			}
			else
			{
				if (msg == -117)
				{
					short penPositionX2 = FlPenManager.GetPenPositionX(intParam);
					short penPositionY2 = FlPenManager.GetPenPositionY(intParam);
					if (penPositionX2 <= 60 && penPositionY2 <= 60)
					{
						mPenIdx++;
						if (mPenIdx == 3)
						{
							ConsoleSetMode(Mode.ModeShown);
							mPenIdx = 0;
						}
					}
					else
					{
						mPenIdx = 0;
					}
				}
				if (msg == -121)
				{
					if (intParam == 8)
					{
						ConsoleSetMode(Mode.ModeShown);
					}
					if (DebugConsoleDisplaySequence[mKeyIdx] == intParam)
					{
						mKeyIdx++;
						if (mKeyIdx == 6)
						{
							ConsoleSetMode(Mode.ModeShown);
							mKeyIdx = 0;
						}
					}
					else
					{
						mKeyIdx = 0;
					}
				}
			}
			return true;
		}

		public void SetFont(FlFont font)
		{
			mConsoleText.SetFont(font);
			mAcceptText.SetFont(font);
			mDeclineText.SetFont(font);
			mNbLinesText.SetFont(font);
			Text[] array = mMonitorText;
			foreach (Text text in array)
			{
				text.SetFont(font);
			}
			if (font != null)
			{
				SetViewport(FlApplication.GetInstance());
				Resize();
				ConsoleAddInitText();
			}
		}

		public void Resize()
		{
			FlFont font = mConsoleText.GetFont();
			Viewport instance = FlApplication.GetInstance();
			if (font != null)
			{
				SetRect(instance.GetRectLeft(), instance.GetRectTop(), instance.GetRectWidth(), instance.GetRectHeight());
				short num = (short)(mRect_height / font.GetLineHeight() - 1);
				short num2 = 0;
				short num3 = (short)(num - num2);
				mConsoleHeight = (short)(num3 * font.GetLineHeight());
				short num4 = (short)(num2 * font.GetLineHeight());
				short rect_top = num4;
				short rect_top2 = (short)(mRect_height - font.GetLineHeight());
				mMonitorBkg.SetRect(0, 0, mRect_width, num4);
				mConsoleBkg.SetRect(0, rect_top, mRect_width, (short)(mRect_height - font.GetLineHeight() - num4));
				mConsoleText.SetRect(0, rect_top, mRect_width, mConsoleHeight);
				mSoftkeyBar.SetRect(0, rect_top2, mRect_width, (short)font.GetLineHeight());
				mAcceptText.SetRect(0, rect_top2, mRect_width, (short)font.GetLineHeight());
				mDeclineText.SetRect(0, rect_top2, mRect_width, (short)font.GetLineHeight());
				mNbLinesText.SetRect(0, rect_top2, mRect_width, (short)font.GetLineHeight());
				if (mConsoleWidth != instance.GetRectWidth())
				{
					ConsoleConsoleAddOutputPrivate(new FlString("Console Resize"));
					mConsoleWidth = instance.GetRectWidth();
				}
				ConsoleGoToLastLine();
			}
		}

		public void ConsoleSetMode(Mode mode)
		{
			if (mode == mConsoleMode)
			{
				return;
			}
			if (mConsoleAboveOnly)
			{
				mode = Mode.ModeAbove;
			}
			switch (mode)
			{
			case Mode.ModeHidden:
				mConsoleMode = Mode.ModeHidden;
				mConsoleText.SetVisible(false);
				mConsoleBkg.SetVisible(false);
				mMonitorBkg.SetVisible(false);
				mSoftkeyBar.SetVisible(false);
				mAcceptText.SetVisible(false);
				mDeclineText.SetVisible(false);
				mNbLinesText.SetVisible(false);
				if (mFirstTime)
				{
					mFirstTime = false;
					mSendKeys = true;
				}
				else
				{
					mSendKeys = false;
				}
				break;
			case Mode.ModeShown:
				mConsoleMode = Mode.ModeShown;
				mConsoleText.SetVisible(true);
				mConsoleBkg.SetVisible(true);
				mMonitorBkg.SetVisible(true);
				mSoftkeyBar.SetVisible(true);
				mAcceptText.SetVisible(true);
				mDeclineText.SetVisible(true);
				mNbLinesText.SetVisible(true);
				ConsoleGoToLastLine();
				BringToFront();
				break;
			case Mode.ModeAbove:
				mConsoleMode = Mode.ModeAbove;
				mConsoleText.SetVisible(true);
				mConsoleBkg.SetVisible(false);
				mMonitorBkg.SetVisible(false);
				mSoftkeyBar.SetVisible(false);
				mNbLinesText.SetVisible(false);
				mAcceptText.SetVisible(false);
				mDeclineText.SetVisible(false);
				ConsoleGoToLastLine();
				SetViewport(FlApplication.GetInstance());
				BringToFront();
				mSendKeys = false;
				break;
			case Mode.ModeSaveFileReader:
				ConsoleSetMode(Mode.ModeShown);
				mConsoleMode = Mode.ModeSaveFileReader;
				break;
			}
		}

		private void ConsoleScroll(bool down)
		{
			for (int i = 0; i < 5; i++)
			{
				if (down)
				{
					mConsoleText.NextLine();
				}
				else
				{
					mConsoleText.PreviousLine();
				}
			}
			ConsoleUpdateLineCount();
		}

		private void ConsoleUpdateLineCount()
		{
			if (mConsoleMode == Mode.ModeSaveFileReader || mConsoleMode == Mode.ModeShown)
			{
				FlString flString = new FlString();
				flString.AddAssign(mPageDownStr);
				flString.AddAssign(new FlString(mConsoleText.GetCurrentLine()));
				flString.InsertCharAt(flString.GetLength(), 47);
				flString.AddAssign(new FlString(mConsoleText.GetNbLines() - 1));
				flString.AddAssign(mPageUpStr);
				mNbLinesText.SetCaption(flString);
			}
		}

		private void UpdateColors()
		{
			mConsoleBkg.SetColor(mConsoleColor);
			mMonitorBkg.SetColor(mSoftkeyBarColor);
			mSoftkeyBar.SetColor(mSoftkeyBarColor);
		}

		private void ConsoleGoToLastLine()
		{
			mConsoleText.SetCurrentLine(mConsoleText.GetNbLines() - mConsoleText.GetLinesPerPage());
			ConsoleUpdateLineCount();
		}

		private void ConsoleSetAboveOnly(bool enable)
		{
			mConsoleAboveOnly = enable;
			if (mConsoleAboveOnly && mConsoleText.GetFont() != null)
			{
				ConsoleSetMode(Mode.ModeAbove);
			}
		}

		public void ConsoleClear()
		{
			mConsoleText.SetCaption(new FlString());
			ConsoleUpdateLineCount();
		}

		public void SetLetterCase(LetterCase letterCase)
		{
			if (mLetterCase != letterCase)
			{
				mLetterCase = letterCase;
				FlString flString = new FlString("Above");
				FlString flString2 = new FlString("Hide");
				switch (letterCase)
				{
				case LetterCase.LetterCaseUpper:
					flString.ToUpper();
					flString2.ToUpper();
					break;
				case LetterCase.LetterCaseLower:
					flString.ToLower();
					flString2.ToLower();
					break;
				}
				mAcceptText.SetCaption(flString);
				mDeclineText.SetCaption(flString2);
			}
		}

		public void SetConsoleColor(int r, int g, int b)
		{
			mConsoleColor.SetRed(r);
			mConsoleColor.SetGreen(g);
			mConsoleColor.SetBlue(b);
			UpdateColors();
		}

		public void SetSoftkeyBarColor(int r, int g, int b)
		{
			mSoftkeyBarColor.SetRed(r);
			mSoftkeyBarColor.SetGreen(g);
			mSoftkeyBarColor.SetBlue(b);
			UpdateColors();
		}

		private void ConsoleAddInitText()
		{
			if (!mConsoleInitTextAdded)
			{
				ConsoleConsoleAddOutputPrivate(mInitTextStr);
				mConsoleInitTextAdded = true;
			}
		}

		public void ConsoleAddOutput(FlString inTextOutput)
		{
			if (mConsoleMode != Mode.ModeSaveFileReader)
			{
				ConsoleConsoleAddOutputPrivate(inTextOutput);
			}
		}

		private void ConsoleConsoleAddOutputPrivate(FlString inTextOutput)
		{
			if (mConsoleText.GetFont() != null)
			{
				if (mLetterCase == LetterCase.LetterCaseLower)
				{
					inTextOutput.ToLower();
				}
				else if (mLetterCase == LetterCase.LetterCaseUpper)
				{
					inTextOutput.ToUpper();
				}
				mConsoleText.AppendLine(new FlString(inTextOutput));
				mConsoleText.SetSize(mRect_width, mConsoleHeight);
				while (mConsoleText.GetNbLines() > 64)
				{
					mConsoleText.RemoveLines(0, mConsoleText.GetNbLines() - 64);
				}
				ConsoleGoToLastLine();
				ConsoleUpdateLineCount();
				if (mConsoleMode != Mode.ModeAbove && mConsoleAboveOnly)
				{
					ConsoleSetMode(Mode.ModeAbove);
				}
			}
		}

		private FlString MonitorGetVal(FlString key)
		{
			return null;
		}

		public void MonitorSetVal(FlString key, FlString val)
		{
		}

		public void MonitorSetVal(FlString key, int val)
		{
			MonitorSetVal(key, new FlString(val));
		}

		public void MonitorSetVal(FlString key, bool val)
		{
			MonitorSetVal(key, val ? mMonitorTrueStr : mMonitorFalseStr);
		}

		private void MonitorUpdateText(int monitorIdx)
		{
		}

		private short MonitorGetNbLines()
		{
			for (short num = 0; num < 8; num = (short)(num + 1))
			{
				if (mMonitorData[num].mKey.IsEmpty())
				{
					return num;
				}
			}
			return 8;
		}

		public void MonitorClear()
		{
		}

		public bool SendInputsToGame()
		{
			if (!mSendKeys || mConsoleMode == Mode.ModeSaveFileReader || mConsoleMode == Mode.ModeShown)
			{
				mSendKeys = true;
				return false;
			}
			return true;
		}
	}
}
