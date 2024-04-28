using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TimeHUD : HUD
	{
		public Text mTimeText;

		public int mPreviousTimeMs;

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mHudViewport = EntryPoint.GetViewport(mPackage, 82);
			mTimeText = EntryPoint.GetText(mPackage, 83);
		}

		public override void Initialize(GameController game)
		{
			base.Initialize(game);
			UpdateCountDown(0);
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			int playTimeMs = mGame.GetPlayTimeMs();
			if (playTimeMs - mPreviousTimeMs > 999)
			{
				UpdateCountDown(playTimeMs);
			}
		}

		public virtual void UpdateCountDown(int playTimeMs)
		{
			if (mGame.IsUsingTimeLimit())
			{
				playTimeMs = mGame.GetTimeLimit() - playTimeMs;
			}
			if (playTimeMs < 0)
			{
				playTimeMs = 0;
			}
			if (playTimeMs > 5999000)
			{
				playTimeMs = 5999000;
			}
			mTimeText.SetCaption(Utilities.CreateMSTimeStringFromMillisecond(playTimeMs));
			mPreviousTimeMs = playTimeMs / 1000 * 1000;
		}

		public override void Reset()
		{
			mPreviousTimeMs = -1;
			OnTime(0, 0);
		}

		public override void Clean()
		{
			if (mTimeText != null)
			{
				mTimeText = null;
			}
			if (mHudViewport != null)
			{
				mHudViewport = null;
			}
			base.Clean();
		}

		public static TimeHUD[] InstArrayTimeHUD(int size)
		{
			TimeHUD[] array = new TimeHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TimeHUD();
			}
			return array;
		}

		public static TimeHUD[][] InstArrayTimeHUD(int size1, int size2)
		{
			TimeHUD[][] array = new TimeHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeHUD();
				}
			}
			return array;
		}

		public static TimeHUD[][][] InstArrayTimeHUD(int size1, int size2, int size3)
		{
			TimeHUD[][][] array = new TimeHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TimeHUD();
					}
				}
			}
			return array;
		}
	}
}
