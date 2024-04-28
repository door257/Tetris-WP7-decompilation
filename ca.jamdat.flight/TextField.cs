using System;

namespace ca.jamdat.flight
{
	public class TextField : Viewport
	{
		public new const sbyte typeNumber = 99;

		public new const sbyte typeID = 99;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte charRemoval = 0;

		public const sbyte charAppend = 1;

		public const sbyte charCycle = 2;

		public Component mCursor;

		public Text mText;

		public FlString mString;

		public bool mIsFocused;

		public short mMaxLength;

		public bool mIsCapitalised;

		public bool mIsCapitalisedLock;

		public bool mIsDigitsOnly;

		public sbyte mCursorRecedeOnLastChar;

		public int mCursorBlinkTimeLeft;

		public FlString mKeysString;

		public int mCurrentKeysStringIndex;

		public int mCursorPosition;

		public int mLastKey;

		public int mCycleTimeLeft;

		public int[] mFirstIndexs = new int[13];

		public static TextField Cast(object o, TextField _)
		{
			return (TextField)o;
		}

		public override sbyte GetTypeID()
		{
			return 99;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public TextField()
		{
			mString = new FlString();
			Initialize();
		}

		public TextField(Viewport viewport)
			: base(viewport)
		{
			mString = new FlString();
			Initialize();
		}

		public TextField(Viewport viewport, FlFont inFont, FlString caption, short inRect_left, short inRect_top, short inRect_width, short inRect_height, sbyte align, bool multiline)
			: base(viewport)
		{
			mString = new FlString();
			Initialize();
			SetRect(inRect_left, inRect_top, inRect_width, inRect_height);
			mString.Assign(caption);
			mText = new Text(this, inFont, new FlString(mString), 0, 0, inRect_width, inRect_height, align);
			mText.SetMultiline(multiline);
			mText.KeepTrailingWhiteSpaces(true);
		}

		public override void destruct()
		{
			mText.SetViewport(null);
			if (mCursor != null)
			{
				mCursor.SetViewport(null);
			}
			mCursor = null;
			mText = null;
			mKeysString = null;
		}

		public override void OnTime(int a8, int deltaTimeMs)
		{
			if (!mIsFocused)
			{
				return;
			}
			mCursorBlinkTimeLeft -= deltaTimeMs;
			if (mCursor != null && mCursorBlinkTimeLeft <= 0)
			{
				ResetCursor(!mCursor.IsVisible());
			}
			if (mCycleTimeLeft > 0)
			{
				mCycleTimeLeft -= deltaTimeMs;
				if (mCycleTimeLeft <= 0)
				{
					UpdateCursorPosition(1);
				}
			}
		}

		public override bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			switch (msg)
			{
			case -128:
				SetFocus(intParam == 1);
				return true;
			case -117:
				SendMsg(this, -113, 0);
				return true;
			default:
				return base.OnDefaultMsg(source, msg, intParam);
			case -121:
			case -120:
			case -119:
				if (!IsKeySupported(intParam))
				{
					return base.OnDefaultMsg(source, msg, intParam);
				}
				if ((intParam == 6 || intParam == 10) && mString.GetLength() == 0)
				{
					StopCycling();
					return false;
				}
				if (msg == -121)
				{
					switch (intParam)
					{
					case 6:
					case 10:
						StopCycling();
						if (mCursorPosition > 0)
						{
							mString.RemoveCharAt(mCursorPosition - 1);
							UpdateDisplay();
							UpdateCursorPosition(-1);
							SendMsg(this, -104, 0);
						}
						break;
					case 16:
						StopCycling();
						SetCapitalised(!mIsCapitalised);
						break;
					default:
						if (mIsDigitsOnly && (intParam < 17 || intParam > 26))
						{
							return true;
						}
						if (mCycleTimeLeft > 0 && intParam == mLastKey)
						{
							mCurrentKeysStringIndex++;
							mCycleTimeLeft = 800;
							if (mCurrentKeysStringIndex == GetKeysStringIndexFor(mLastKey + 1))
							{
								mCurrentKeysStringIndex = GetKeysStringIndexFor(mLastKey);
							}
							mString.RemoveCharAt(mCursorPosition);
							mString.InsertCharAt(mCursorPosition, GetCurrentChar());
							UpdateDisplay();
							UpdateCursorPosition(0);
							SendMsg(this, -104, 2);
						}
						else
						{
							InsertChar(intParam);
						}
						break;
					}
				}
				return true;
			}
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mCursor = Component.Cast(p.SerializePointer(67, true, false), null);
			mText = Text.Cast(p.SerializePointer(71, true, false), null);
			mString.Assign(mText.GetCaption());
			mText.SetViewport(this);
			mText.KeepTrailingWhiteSpaces(true);
			mMaxLength = p.SerializeIntrinsic(mMaxLength);
			mIsCapitalised = p.SerializeIntrinsic(mIsCapitalised);
			mIsCapitalisedLock = p.SerializeIntrinsic(mIsCapitalisedLock);
			mIsDigitsOnly = p.SerializeIntrinsic(mIsDigitsOnly);
			mCursorRecedeOnLastChar = p.SerializeIntrinsic(mCursorRecedeOnLastChar);
			SetCursor(mCursor);
		}

		public virtual void SetFocus(bool isFocused)
		{
			if (isFocused != mIsFocused)
			{
				mIsFocused = isFocused;
				mCycleTimeLeft = 0;
				mCursorPosition = mString.GetLength();
				UpdateCursorPosition(0);
				ResetCursor(isFocused);
				if (mIsFocused)
				{
					TakeFocus();
					RegisterInGlobalTime();
				}
				else
				{
					UnRegisterInGlobalTime();
				}
			}
		}

		public virtual void SetMaxCharacters(short length)
		{
			mMaxLength = length;
		}

		public virtual void SetCapitalised(bool capital)
		{
			mIsCapitalised = mIsCapitalisedLock || capital;
		}

		public virtual void SetLockCapitalised(bool locked)
		{
			mIsCapitalisedLock = locked;
		}

		public virtual void SetCursor(Component cursor)
		{
			if (mCursor != null)
			{
				mCursor.SetViewport(null);
				mCursor = null;
			}
			mCursor = cursor;
			if (mCursor != null)
			{
				mCursor.SetViewport(this);
				mCursor.SetVisible(mIsFocused);
				UpdateCursorPosition(0);
				if (mCursorRecedeOnLastChar == -1)
				{
					mCursorRecedeOnLastChar = (sbyte)mCursor.GetRectWidth();
				}
			}
		}

		public virtual void SetIsDigitOnly(bool digitsOnly)
		{
			mIsDigitsOnly = digitsOnly;
		}

		public virtual void SetCursorRecedeOnLastChar(sbyte cursorRecedeOnLastChar)
		{
			mCursorRecedeOnLastChar = cursorRecedeOnLastChar;
		}

		public virtual Component GetCursor()
		{
			return mCursor;
		}

		public virtual Text GetText()
		{
			return mText;
		}

		public virtual short GetMaxCharacters()
		{
			return mMaxLength;
		}

		public virtual bool IsDigitOnly()
		{
			return mIsDigitsOnly;
		}

		public virtual Text BeginTextChange()
		{
			return mText;
		}

		public virtual void EndTextChange()
		{
			mString.Assign(mText.GetCaption());
			mCursorPosition = mString.GetLength();
			UpdateDisplay();
			UpdateCursorPosition(0);
		}

		public virtual void InsertCharacterInSet(sbyte character, int key)
		{
			if (mKeysString.FindChar(character) == -1)
			{
				mKeysString.InsertCharAt(mFirstIndexs[key - 15 + 1], character);
				for (int i = key - 15 + 1; i < 13; i++)
				{
					mFirstIndexs[i]++;
				}
			}
		}

		public virtual void RemoveCharacterFromSet(sbyte character)
		{
			int num = mKeysString.FindChar(character);
			if (num < 0)
			{
				return;
			}
			mKeysString.RemoveCharAt(num);
			for (int i = 0; i < 13; i++)
			{
				if (mFirstIndexs[i] > num)
				{
					mFirstIndexs[i]--;
				}
			}
		}

		public virtual void CloneCharacterSets(TextField from)
		{
			mKeysString.Assign(from.mKeysString);
			for (int i = 0; i < 13; i++)
			{
				mFirstIndexs[i] = from.mFirstIndexs[i];
			}
		}

		public virtual void Initialize()
		{
			mMaxLength = 255;
			mCursorRecedeOnLastChar = -1;
			mKeysString = new FlString("0@1abc2def3ghi4jkl5mno6pqrs7tuv8wxyz9 ");
			SetFirstIndex(17, 0);
			SetFirstIndex(18, 1);
			SetFirstIndex(19, 3);
			SetFirstIndex(20, 7);
			SetFirstIndex(21, 11);
			SetFirstIndex(22, 15);
			SetFirstIndex(23, 19);
			SetFirstIndex(24, 23);
			SetFirstIndex(25, 28);
			SetFirstIndex(26, 32);
			SetFirstIndex(16, 37);
			SetFirstIndex(15, 37);
			SetFirstIndex(27, 38);
		}

		public virtual void ResetCursor(bool visible)
		{
			if (mCursor != null)
			{
				mCursorBlinkTimeLeft = 800;
				mCursor.SetVisible(visible);
			}
		}

		public virtual void UpdateCursorPosition(int offset)
		{
			mCursorPosition += offset;
			ResetCursor(mIsFocused);
			Vector2_short vector2_short = new Vector2_short(mText.GetCharIndex2DPosition(mCursorPosition));
			FlString flString = new FlString(mText.GetLineString(vector2_short.GetY()));
			int lineWidth = mText.GetFont().GetLineWidth(flString, 0, flString.GetLength(), false);
			int num = mText.GetFont().GetLineWidth(flString, 0, vector2_short.GetX(), false);
			int alignmentOffsetX = DisplayContext.GetAlignmentOffsetX(mText.GetAlignment(), mText.GetRectWidth(), (short)lineWidth);
			if (mCursor != null)
			{
				if ((flString.GetLength() == mMaxLength && num >= lineWidth) || mText.GetAlignment() == 2)
				{
					num -= mCursorRecedeOnLastChar;
				}
				mCursor.SetTopLeft((short)FlMath.Minimum(num + alignmentOffsetX, mText.GetRectWidth()), (short)(vector2_short.GetY() * mText.GetFont().GetLineHeight()));
			}
		}

		public virtual void UpdateDisplay()
		{
			FlString caption = new FlString(mString);
			mText.SetCaption(caption, false);
			short num = 0;
			if (mCursor != null)
			{
				num = mCursor.GetRectWidth();
			}
			if (!mText.IsMultiline())
			{
				mText.ComputeLineWidth(false);
			}
			else
			{
				SetSize((short)(mText.GetRectWidth() + num), mText.GetRectHeight());
			}
		}

		public virtual sbyte GetCurrentChar()
		{
			if (mIsDigitsOnly)
			{
				return (sbyte)(48 + (mLastKey - 17));
			}
			sbyte b = mKeysString.GetCharAt(mCurrentKeysStringIndex);
			if (mIsCapitalised)
			{
				int num = b - 97;
				if (num >= 0 && num <= 26)
				{
					b = (sbyte)(65 + num);
				}
			}
			return b;
		}

		public virtual void InsertChar(int key)
		{
			if (mString.GetLength() >= mMaxLength)
			{
				return;
			}
			StopCycling();
			mLastKey = key;
			mCurrentKeysStringIndex = GetKeysStringIndexFor(mLastKey);
			if (mCurrentKeysStringIndex != GetKeysStringIndexFor(mLastKey + 1))
			{
				sbyte currentChar = GetCurrentChar();
				mString.InsertCharAt(mCursorPosition, currentChar);
				UpdateDisplay();
				if (CanCycle())
				{
					mCycleTimeLeft = 800;
					UpdateCursorPosition(0);
				}
				else
				{
					UpdateCursorPosition(1);
				}
				SendMsg(this, -104, 1);
			}
		}

		public virtual bool IsKeySupported(int key)
		{
			if (key == 14 || key == 10 || key == 6)
			{
				return true;
			}
			if (key >= 15)
			{
				return key < 27;
			}
			return false;
		}

		public virtual void StopCycling()
		{
			if (mCycleTimeLeft > 0)
			{
				mCycleTimeLeft = -1;
				UpdateCursorPosition(1);
			}
		}

		public virtual bool CanCycle()
		{
			if (mIsDigitsOnly)
			{
				return false;
			}
			return GetKeysStringIndexFor(mLastKey) != GetKeysStringIndexFor(mLastKey + 1) - 1;
		}

		public virtual int GetKeysStringIndexFor(int key)
		{
			return mFirstIndexs[key - 15];
		}

		public virtual void SetFirstIndex(int key, int idx)
		{
			mFirstIndexs[key - 15] = idx;
		}

		public static TextField[] InstArrayTextField(int size)
		{
			TextField[] array = new TextField[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TextField();
			}
			return array;
		}

		public static TextField[][] InstArrayTextField(int size1, int size2)
		{
			TextField[][] array = new TextField[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TextField[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TextField();
				}
			}
			return array;
		}

		public static TextField[][][] InstArrayTextField(int size1, int size2, int size3)
		{
			TextField[][][] array = new TextField[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TextField[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TextField[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TextField();
					}
				}
			}
			return array;
		}
	}
}
