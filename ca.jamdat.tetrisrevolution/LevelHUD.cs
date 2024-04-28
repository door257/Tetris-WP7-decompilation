using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LevelHUD : HUD
	{
		public Text mLevelText;

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mHudViewport = EntryPoint.GetViewport(mPackage, 76);
			mLevelText = EntryPoint.GetText(mPackage, 77);
		}

		public override void Initialize(GameController game)
		{
			base.Initialize(game);
			Update();
		}

		public override void Clean()
		{
			if (mHudViewport != null)
			{
				mHudViewport = null;
			}
			if (mLevelText != null)
			{
				mLevelText = null;
			}
			base.Clean();
		}

		public override void Update()
		{
			mLevelText.SetCaption(new FlString(mGame.GetCurrentLevel()));
		}

		public static LevelHUD[] InstArrayLevelHUD(int size)
		{
			LevelHUD[] array = new LevelHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LevelHUD();
			}
			return array;
		}

		public static LevelHUD[][] InstArrayLevelHUD(int size1, int size2)
		{
			LevelHUD[][] array = new LevelHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LevelHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LevelHUD();
				}
			}
			return array;
		}

		public static LevelHUD[][][] InstArrayLevelHUD(int size1, int size2, int size3)
		{
			LevelHUD[][][] array = new LevelHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LevelHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LevelHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LevelHUD();
					}
				}
			}
			return array;
		}
	}
}
