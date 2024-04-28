using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VerticalText : Viewport
	{
		public FlString mCaption;

		public Text[] mCharacterTexts;

		public int mLength;

		public int mPosX;

		public int mPosY;

		public int mHeight;

		public bool mExtendUp;

		public FlFont mFont;

		public VerticalText(int posX, int posY, FlFont font, bool extendUp)
		{
			mPosX = posX;
			mPosY = posY;
			mExtendUp = extendUp;
			mFont = font;
		}

		public override void destruct()
		{
		}

		public virtual void Unload()
		{
			ClearCharacterTextsArray();
			if (mCaption != null)
			{
				mCaption = null;
			}
			SetViewport(null);
		}

		public virtual void Initialize(Viewport parentViewport)
		{
			mClipChildren = true;
			ClearCharacterTextsArray();
			if (mCaption != null)
			{
				mLength = mCaption.GetLength();
			}
			mCharacterTexts = new Text[mLength];
			if (mCaption != null)
			{
				CreateTexts();
			}
			SetViewport(parentViewport);
		}

		public virtual void SetCaption(FlString @string)
		{
			if (mCaption != null && @string.Equals(mCaption))
			{
				@string = null;
				return;
			}
			bool flag = mCharacterTexts != null;
			if (mCaption != null)
			{
				mCaption = null;
			}
			mCaption = @string;
			ClearCharacterTextsArray();
			mCharacterTexts = new Text[mLength];
			if (flag)
			{
				CreateTexts();
			}
		}

		public virtual void SetFont(FlFont font)
		{
			mFont = font;
		}

		public virtual void SetX(int newX)
		{
			mPosX = newX;
			SetXY(mPosX, mPosY);
		}

		public virtual void SetY(int newY)
		{
			mPosY = newY;
			SetXY(mPosX, mPosY);
		}

		public virtual void SetXY(int coordX, int coordY)
		{
			if (mExtendUp)
			{
				SetTopLeft((short)mPosX, (short)(mPosY - mHeight));
			}
			else
			{
				SetTopLeft((short)mPosX, (short)mPosY);
			}
		}

		public virtual int GetY()
		{
			return mPosY;
		}

		public virtual int GetX()
		{
			return mPosX;
		}

		public virtual FlFont GetFont()
		{
			return mFont;
		}

		public virtual FlString GetCaption()
		{
			return mCaption;
		}

		public virtual void AdjustRect()
		{
			int num = 0;
			for (int i = 0; i < mLength; i++)
			{
				int lineWidth = mCharacterTexts[i].GetLineWidth();
				if (lineWidth > num)
				{
					num = lineWidth;
				}
			}
			CalculateHeight();
			SetSize((short)num, (short)mHeight);
			SetXY(mPosX, mPosY);
		}

		public virtual void CalculateHeight()
		{
			mHeight = 0;
			for (int i = 0; i < mLength; i++)
			{
				sbyte charAt = mCaption.GetCharAt(i);
				int num = mFont.GetCharHeight(charAt, false);
				if (num <= 0)
				{
					num = 14;
				}
				mHeight += num;
			}
			mHeight += mLength + 1;
		}

		public virtual void CreateTexts()
		{
			Text text = null;
			CalculateHeight();
			int num = mHeight;
			for (int i = 0; i < mLength; i++)
			{
				sbyte charAt = mCaption.GetCharAt(i);
				int num2 = mFont.GetCharHeight(charAt, false);
				text = new Text();
				if (num2 <= 0)
				{
					num2 = 14;
				}
				text.SetFont(mFont);
				FlString flString = new FlString();
				flString.AddAssign(FlString.FromChar(charAt));
				text.SetCaption(flString);
				text.SetSize(text.GetLineWidth(), text.GetLineHeight());
				text.SetTopLeft(0, (short)(num - text.GetLineHeight()));
				num -= num2;
				num--;
				mCharacterTexts[i] = text;
				text.SetViewport(this);
				text = null;
			}
			AdjustRect();
		}

		public virtual void ClearCharacterTextsArray()
		{
			if (mCharacterTexts != null)
			{
				for (int i = 0; i < mLength; i++)
				{
					mCharacterTexts[i].SetViewport(null);
					mCharacterTexts[i] = null;
				}
				mCharacterTexts = null;
				mCharacterTexts = null;
				mLength = mCaption.GetLength();
			}
		}

		public VerticalText(int posX, int posY, FlFont font)
			: this(posX, posY, font, true)
		{
		}
	}
}
