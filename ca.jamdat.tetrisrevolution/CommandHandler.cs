using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class CommandHandler
	{
		public SceneStack mSceneStack;

		public SceneTransitionController mSceneTransitionController;

		public virtual void destruct()
		{
		}

		public virtual void Initialize(SceneTransitionController stc, SceneStack ss)
		{
			mSceneTransitionController = stc;
			mSceneStack = ss;
		}

		public virtual bool Execute(int command)
		{
			if (GameApp.Get().GetCheatContainer().GetCheat(1)
				.IsActivated())
			{
				return true;
			}
			return ExecuteImpl(command);
		}

		public virtual bool ExecuteSlideShowCommand(int command)
		{
			if (GameApp.Get().GetCheatContainer().GetCheat(1)
				.IsActivated())
			{
				return ExecuteImpl(command);
			}
			return false;
		}

		public virtual BaseScene GetCurrentScene()
		{
			return mSceneStack.GetTop();
		}

		public virtual bool ExecuteImpl(int command)
		{
			bool flag = ExecuteInterruptionCommand(command);
			if (!flag)
			{
				flag = ExecuteNavigationCommand(command);
			}
			if (!flag)
			{
				flag = ExecuteApplicationCommand(command);
			}
			return flag;
		}

		public virtual bool ExecuteApplicationCommand(int command)
		{
			GameApp gameApp = GameApp.Get();
			Settings settings = gameApp.GetSettings();
			GameSettings gameSettings = gameApp.GetGameSettings();
			bool result = true;
			switch (command)
			{
			case -42:
				Execute(-41);
				break;
			case 18:
				Execute(-17);
				break;
			case -16:
				gameApp.Reset();
				mSceneStack.GetTop().StartMusic();
				Execute(-17);
				break;
			case -15:
				if (!Guide.IsVisible && LiveState.IsTrial)
				{
					Guide.ShowMarketplace(PlayerIndex.One);
					GameApp.Get().SetIsInMarketplace(true);
				}
				break;
			case -11:
				gameApp.Quit();
				break;
			case -75:
				settings.SetSoundEnabled(true);
				break;
			case -76:
				settings.SetSoundEnabled(false);
				GameApp.Get().GetMediaPlayer().SetMenuMusicPlaying(false);
				break;
			case 62:
				gameSettings.SetTutorialEnabled(!gameSettings.IsTutorialEnabled());
				break;
			case 63:
				if (gameSettings.GetTouchMode() == 0)
				{
					gameSettings.SetTouchMode(1);
				}
				else
				{
					gameSettings.SetTouchMode(0);
				}
				break;
			case 64:
				gameSettings.SetGhostEnabled(!gameSettings.IsGhostEnabled());
				break;
			case 65:
				settings.SetSoundEnabled(!settings.IsSoundEnabled());
				if (!settings.IsSoundEnabled())
				{
					GameApp.Get().GetMediaPlayer().SetMenuMusicPlaying(false);
				}
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		public virtual bool ExecuteNavigationCommand(int command)
		{
			bool result = true;
			SceneStack sceneStack = mSceneStack;
			switch (command)
			{
			case -12:
				PopAndLaunchScene();
				break;
			case -26:
				sceneStack.PopAll();
				break;
			case 79:
			case 80:
			{
				TrainerMenu trainerMenu = (TrainerMenu)CreateScene(17);
				int num = 0;
				int num2 = 0;
				int replayId = ReplayPackageLoader.Get().GetReplayId();
				if (command == 79)
				{
					num = 1;
					num2 = GlossarySelectMenuPopup.ConvertReplayIdToSelectionIndex(replayId);
				}
				else
				{
					num = 2;
					num2 = MasterReplaySelectMenuPopup.ConvertReplayIdToSelectionIndex(replayId);
				}
				trainerMenu.SetPopUpToLauchOnEntry(num, num2);
				PopAllPushMainMenuAndLaunchScene(trainerMenu, 8);
				break;
			}
			case 81:
			{
				PlayMenu scene2 = (PlayMenu)CreateScene(12);
				int mainMenuSelectedIndex = ((!GameApp.Get().GetIsDemo()) ? 1 : 0);
				PopAllPushMainMenuAndLaunchScene(scene2, mainMenuSelectedIndex);
				break;
			}
			case -41:
				sceneStack.PopAll();
				PushAndLaunchScene(CreateScene(-39));
				break;
			case -17:
				sceneStack.PopAll();
				PushAndLaunchScene(CreateScene(-18));
				break;
			case 31:
			{
				BaseScene scene = CreateScene(12);
				PopAllPushMainMenuAndLaunchScene(scene, 1);
				break;
			}
			case -1:
				PushBootSequenceScenesAndLaunch();
				break;
			case -2:
				PopAndLaunchScene();
				break;
			case -27:
				ReloadCurrentScene();
				break;
			default:
			{
				BaseScene baseScene = CreateScene(command);
				if (baseScene != null)
				{
					PushAndLaunchScene(baseScene);
				}
				else
				{
					result = false;
				}
				break;
			}
			}
			return result;
		}

		public virtual bool ExecuteInterruptionCommand(int command)
		{
			bool result = true;
			SceneStack sceneStack = mSceneStack;
			switch (command)
			{
			case -13:
				SuspendScene(sceneStack.GetTop());
				break;
			case -14:
				if (GameApp.Get().IsInMarketplace())
				{
					GameApp.Get().SetIsInMarketplace(false);
					LiveState.Get.UpdateIsTrial();
					if (!LiveState.IsTrial)
					{
						GameApp.Get().SetIsDemo(false);
						Execute(-17);
					}
				}
				else if (GameApp.Get().IsInUpdate())
				{
					GameApp.Get().SetIsInUpdate(false);
					LiveState.Get.UpdateApp();
				}
				ResumeScene(sceneStack.GetTop());
				break;
			case -51:
			{
				BaseScene top = sceneStack.GetTop();
				if (top != null && top.GetId() != 24)
				{
					SuspendScene(top);
					PushAndLaunchScene(CreateScene(-49));
				}
				break;
			}
			case -52:
				if (sceneStack.GetTop() != null && sceneStack.GetTop().GetId() == 24)
				{
					PopAndLaunchScene();
				}
				ResumeScene(sceneStack.GetTop());
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		public virtual BaseScene CreateScene(int command)
		{
			BaseScene result = null;
			Menu menu = null;
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			bool flag = false;
			switch (command)
			{
			case -9:
				result = new Splash(5, 1343529, 5792);
				break;
			case -18:
				menu = new MainMenu(9, 1507374);
				break;
			case -39:
				result = new GameController(40, 1867833);
				break;
			case 12:
				result = new PlayMenu(11, 1540143);
				break;
			case 17:
				result = new TrainerMenu(12, 2064447);
				break;
			case 16:
				result = new RealisationsMenu(17, 1966140);
				break;
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
			case 46:
			case 61:
				flag = true;
				if (ReplayPackageLoader.Get().OnLoadReplayCommand(command))
				{
					gameSettings = GameApp.Get().GetGameSettings();
					gameSettings.SetPlayMode((sbyte)(flag ? 2 : 3));
					return CreateScene(75);
				}
				break;
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 59:
				if (ReplayPackageLoader.Get().OnLoadReplayCommand(command))
				{
					gameSettings = GameApp.Get().GetGameSettings();
					gameSettings.SetPlayMode((sbyte)(flag ? 2 : 3));
					return CreateScene(75);
				}
				break;
			case 75:
				if (!ReplayPackageLoader.Get().IsThereAReplaySet())
				{
					Replay replay = GameApp.Get().GetReplay();
					if (replay.IsReplayLoaded())
					{
						replay.SetReplayMode(1);
						gameSettings.SetPlayMode(1);
						gameSettings.SynchWithReplay(replay);
						return CreateScene(9);
					}
					break;
				}
				return CreateScene(9);
			case 74:
			{
				gameSettings = GameApp.Get().GetGameSettings();
				gameSettings.SetPlayMode(0);
				ReplayPackageLoader replayPackageLoader2 = ReplayPackageLoader.Get();
				int randomReplayMasterId = ReplayPackageLoader.GetRandomReplayMasterId();
				replayPackageLoader2.SetSimulationToLoad(randomReplayMasterId);
				replayPackageLoader2.InitGameSettings();
				return CreateScene(75);
			}
			case -36:
			case -35:
			{
				gameSettings = GameApp.Get().GetGameSettings();
				switch (command)
				{
				case -35:
					gameSettings.SetPlayMode(5);
					break;
				case -36:
					gameSettings.SetPlayMode(6);
					break;
				}
				ReplayPackageLoader replayPackageLoader = ReplayPackageLoader.Get();
				int randomDemoReplayMasterId = ReplayPackageLoader.GetRandomDemoReplayMasterId();
				replayPackageLoader.SetSimulationToLoad(randomDemoReplayMasterId);
				replayPackageLoader.InitGameSettings();
				return CreateScene(75);
			}
			case 9:
				result = new LoadingMenu();
				break;
			case -4:
				result = new Splash(2, 1376298, 3000);
				break;
			case -5:
				result = new Splash(4, 1409067, 3083);
				break;
			case -50:
				result = new ChangeScreenResolutionMenu(5);
				break;
			case -49:
				result = new ChangeScreenResolutionMenu(6);
				break;
			case -46:
				menu = new LanguageMenu(8, 1441836);
				break;
			case -47:
				menu = new LanguageMenu(15, 1441836);
				break;
			case -8:
				result = new EnableSoundMenu(7, 1474605, -75, -76);
				break;
			}
			if (menu != null)
			{
				result = menu;
			}
			return result;
		}

		public virtual void PushAndLaunchScene(BaseScene scene)
		{
			mSceneStack.Push(scene);
			LaunchScene(scene);
		}

		public virtual void PopAndLaunchScene()
		{
			SceneStack sceneStack = mSceneStack;
			sceneStack.Pop();
			BaseScene top = sceneStack.GetTop();
			if (top != null)
			{
				top.GetId();
				LaunchScene(top);
			}
		}

		public virtual void PopAllPushMainMenuAndLaunchScene(BaseScene scene, int mainMenuSelectedIndex)
		{
			mSceneStack.PopAll();
			MainMenu mainMenu = (MainMenu)CreateScene(-18);
			mainMenu.SetInitialSelection(mainMenuSelectedIndex);
			mSceneStack.Push(mainMenu);
			PushAndLaunchScene(scene);
		}

		public virtual void LaunchScene(BaseScene scene)
		{
			mSceneTransitionController.EnqueueTransition(scene);
		}

		public virtual void ReloadCurrentScene()
		{
			BaseScene top = mSceneStack.GetTop();
			if (top != null)
			{
				mSceneTransitionController.EnqueueTransition(top);
			}
		}

		public virtual void SuspendScene(BaseScene scene)
		{
			if (scene != null)
			{
				scene.Suspend();
			}
		}

		public virtual void ResumeScene(BaseScene scene)
		{
			if (scene != null)
			{
				scene.Resume();
			}
		}

		public virtual bool PushBootSequenceScenesAndLaunch()
		{
			int[] array = new int[7] { -18, -9, -5, -4, -8, -46, 0 };
			for (int i = 0; array[i] != 0; i++)
			{
				BaseScene baseScene = CreateScene(array[i]);
				if (baseScene != null)
				{
					LanguageManager languageManager = GameApp.Get().GetLanguageManager();
					if (baseScene.GetId() == 8 && languageManager.QueryLanguage() != 2)
					{
						baseScene = null;
					}
					else if (baseScene.GetId() == 7 && !Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
					{
						Settings settings = GameApp.Get().GetSettings();
						settings.SetSoundEnabled(false);
						baseScene = null;
					}
					else
					{
						mSceneStack.Push(baseScene);
					}
				}
			}
			LaunchScene(mSceneStack.GetTop());
			return true;
		}

		public static CommandHandler[] InstArrayCommandHandler(int size)
		{
			CommandHandler[] array = new CommandHandler[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CommandHandler();
			}
			return array;
		}

		public static CommandHandler[][] InstArrayCommandHandler(int size1, int size2)
		{
			CommandHandler[][] array = new CommandHandler[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CommandHandler[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CommandHandler();
				}
			}
			return array;
		}

		public static CommandHandler[][][] InstArrayCommandHandler(int size1, int size2, int size3)
		{
			CommandHandler[][][] array = new CommandHandler[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CommandHandler[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CommandHandler[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CommandHandler();
					}
				}
			}
			return array;
		}
	}
}
