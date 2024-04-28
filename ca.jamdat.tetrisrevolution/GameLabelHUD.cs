using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameLabelHUD : HUD
	{
		public VerticalText mVerticalText;

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mVerticalText = new VerticalText(0, 0, null, true);
		}

		public override void Initialize(GameController game)
		{
			Viewport viewport = EntryPoint.GetViewport(mPackage, 69);
			FlFont flFont = EntryPoint.GetFlFont(mPackage, 70);
			mVerticalText.SetFont(flFont);
			FlString flString = null;
			if (ReplayPackageLoader.Get().IsThereAReplaySet() && ReplayPackageLoader.Get().IsReplayIsMaster())
			{
				FlString masterReplayAuthorString = Utilities.GetMasterReplayAuthorString(ReplayPackageLoader.Get().GetReplayId());
				flString = new FlString(masterReplayAuthorString.Substring(0, masterReplayAuthorString.FindChar(44)).ToRawString());
			}
			else
			{
				flString = game.GetGame().GetGameTitleString();
			}
			mVerticalText.SetCaption(flString);
			mVerticalText.Initialize(viewport);
			mVerticalText.SetY(viewport.GetRectHeight() - 1);
			IndexedSprite indexedSprite = EntryPoint.GetIndexedSprite(mPackage, 68);
			indexedSprite.SetCurrentFrame(Utilities.GetVariantIconFrameIndex(GameApp.Get().GetGameSettings().GetGameVariant()));
		}

		public override void Unload()
		{
			if (mVerticalText != null)
			{
				mVerticalText.Unload();
				mVerticalText = null;
			}
			base.Unload();
		}

		public override void SetVisible(bool visible)
		{
			Viewport viewport = EntryPoint.GetViewport(mPackage, 67);
			viewport.SetVisible(visible);
		}

		public static GameLabelHUD[] InstArrayGameLabelHUD(int size)
		{
			GameLabelHUD[] array = new GameLabelHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameLabelHUD();
			}
			return array;
		}

		public static GameLabelHUD[][] InstArrayGameLabelHUD(int size1, int size2)
		{
			GameLabelHUD[][] array = new GameLabelHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameLabelHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameLabelHUD();
				}
			}
			return array;
		}

		public static GameLabelHUD[][][] InstArrayGameLabelHUD(int size1, int size2, int size3)
		{
			GameLabelHUD[][][] array = new GameLabelHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameLabelHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameLabelHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameLabelHUD();
					}
				}
			}
			return array;
		}
	}
}
