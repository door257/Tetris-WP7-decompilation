using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ScoreHUD : HUD
	{
		public Text mScoreText;

		public Text mTitleText;

		public GameScore mGameScore;

		public int mPreviousScoreValue;

		public int mHighestScoreWithTitle;

		public ScoreHUD()
		{
			mPreviousScoreValue = -1;
		}

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mScoreText = EntryPoint.GetText(mPackage, 73);
			mTitleText = EntryPoint.GetText(mPackage, 74);
			mTitleText.SetVisible(true);
			CalculateHighestScoreWithTitle();
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			Update();
		}

		public override void Update()
		{
			base.Update();
			int currentScore = mGame.GetGameScore().GetCurrentScore();
			if (currentScore != mPreviousScoreValue)
			{
				mPreviousScoreValue = currentScore;
				FlString caption = new FlString(currentScore);
				mScoreText.SetCaption(caption);
				mTitleText.SetVisible(currentScore < mHighestScoreWithTitle);
			}
		}

		public override void Reset()
		{
			mPreviousScoreValue = -1;
			Update();
		}

		public virtual void CalculateHighestScoreWithTitle()
		{
			int num = (313 - mTitleText.GetLineWidth()) / 13;
			int num2 = 1;
			for (int i = 0; i < num; i++)
			{
				if (num2 >= 9999999)
				{
					break;
				}
				num2 *= 10;
			}
			mHighestScoreWithTitle = num2;
		}

		public static ScoreHUD[] InstArrayScoreHUD(int size)
		{
			ScoreHUD[] array = new ScoreHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ScoreHUD();
			}
			return array;
		}

		public static ScoreHUD[][] InstArrayScoreHUD(int size1, int size2)
		{
			ScoreHUD[][] array = new ScoreHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ScoreHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ScoreHUD();
				}
			}
			return array;
		}

		public static ScoreHUD[][][] InstArrayScoreHUD(int size1, int size2, int size3)
		{
			ScoreHUD[][][] array = new ScoreHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ScoreHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ScoreHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ScoreHUD();
					}
				}
			}
			return array;
		}
	}
}
