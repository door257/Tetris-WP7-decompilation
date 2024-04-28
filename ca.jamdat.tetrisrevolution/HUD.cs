using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class HUD : TimeControlled
	{
		public TetrisGame mGame;

		public Package mPackage;

		public Viewport mHudViewport;

		public Text mHudTitleText;

		public override void destruct()
		{
		}

		public virtual void SetGame(TetrisGame game)
		{
			mGame = game;
		}

		public virtual void GetEntryPoints()
		{
			mPackage = GameLibrary.GetPreLoadedPackage(1867833);
		}

		public virtual void Initialize(GameController game)
		{
		}

		public virtual void Clean()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Reset()
		{
		}

		public virtual void Unload()
		{
			Clean();
			DetachHud();
			mHudTitleText = null;
		}

		public virtual void SetVisible(bool bVisible)
		{
			mHudViewport.SetVisible(bVisible);
		}

		public virtual void DetachHud()
		{
			if (mHudViewport != null)
			{
				mHudViewport.SetViewport(null);
			}
		}

		public static HUD[] InstArrayHUD(int size)
		{
			HUD[] array = new HUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new HUD();
			}
			return array;
		}

		public static HUD[][] InstArrayHUD(int size1, int size2)
		{
			HUD[][] array = new HUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new HUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new HUD();
				}
			}
			return array;
		}

		public static HUD[][][] InstArrayHUD(int size1, int size2, int size3)
		{
			HUD[][][] array = new HUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new HUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new HUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new HUD();
					}
				}
			}
			return array;
		}
	}
}
