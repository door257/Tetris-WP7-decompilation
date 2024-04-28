namespace ca.jamdat.tetrisrevolution
{
	public class SceneId
	{
		public const int idUndefined = 0;

		public const int idInDevelopmentSplash = 1;

		public const int idLegalSplash = 2;

		public const int idCarrierSplash = 3;

		public const int idEASplash = 4;

		public const int idGameSplash = 5;

		public const int idOutOfMemoryMenu = 6;

		public const int idEnableSoundMenu = 7;

		public const int idBootLanguageMenu = 8;

		public const int idMainMenu = 9;

		public const int idMessageMenu = 10;

		public const int idPlayMenu = 11;

		public const int idTrainerMenu = 12;

		public const int idHelpMenu = 13;

		public const int idOptionsMenu = 14;

		public const int idOptionsLanguageMenu = 15;

		public const int idPauseMenu = 16;

		public const int idRealisationsMenu = 17;

		public const int idTutorialMenu = 18;

		public const int idLoadingMenu = 19;

		public const int idMoreGamesStaticMenu = 20;

		public const int idMoreGamesProductSlideShowMenu = 21;

		public const int idMoreGames16Menu = 22;

		public const int idMoreGamesExitAppPrompt = 23;

		public const int idChangeScreenResolutionMenu = 24;

		public const int idContentActivationPurchaseMenu = 25;

		public const int idContentActivationPurchaseUnableToCheckDemo = 26;

		public const int idContentActivationPurchaseUnableToCheckNoDemo = 27;

		public const int idContentActivationPurchaseUnsubscribedDemo = 28;

		public const int idContentActivationPurchaseUnsubscribedNoDemo = 29;

		public const int idMoreGamesCannotLaunchBrowserMenu = 30;

		public const int idExitAppConfirmationPrompt = 31;

		public const int idReturnToMainMenuPrompt = 32;

		public const int idResumeSavedGamePrompt = 33;

		public const int idResetGamePrompt = 34;

		public const int idGameStatsMenu = 35;

		public const int idFeatsProgressMenu = 36;

		public const int idFeatsSuccessMenu = 37;

		public const int idNewBestMenu = 38;

		public const int idUnlockMenu = 39;

		public const int idGameScene = 40;

		public const int idAnimationViewerScene = 41;

		public static SceneId[] InstArraySceneId(int size)
		{
			SceneId[] array = new SceneId[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneId();
			}
			return array;
		}

		public static SceneId[][] InstArraySceneId(int size1, int size2)
		{
			SceneId[][] array = new SceneId[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneId[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneId();
				}
			}
			return array;
		}

		public static SceneId[][][] InstArraySceneId(int size1, int size2, int size3)
		{
			SceneId[][][] array = new SceneId[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneId[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneId[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneId();
					}
				}
			}
			return array;
		}
	}
}
