using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameApp : FlApplication
	{
		private bool mIsDemo;

		private bool mIsInMarketplace;

		private bool mIsInUpdate;

		private bool mIsTrial;

		public GameAppImpl mImpl;

		public GameApp()
		{
			InitializeRandomSeed();
			mImpl = new GameAppImpl();
			mImpl.Initialize();
			mImpl.mCurrResolutionSupported = IsScreenResolutionSupported();
			mIsDemo = false;
			mIsInUpdate = false;
			mIsInMarketplace = false;
			RegisterInGlobalTime();
		}

		public override void destruct()
		{
			FlApplication.ProcessExit();
			UnRegisterInGlobalTime();
			mImpl = null;
		}

		public bool GetIsDemo()
		{
			return mIsDemo;
		}

		public void SetIsDemo(bool isDemo)
		{
			mIsDemo = isDemo;
		}

		public bool IsInMarketplace()
		{
			return mIsInMarketplace;
		}

		public bool IsInUpdate()
		{
			return mIsInUpdate;
		}

		public void SetIsInMarketplace(bool isInMarketplace)
		{
			mIsInMarketplace = isInMarketplace;
		}

		public void SetIsInUpdate(bool isInUpdate)
		{
			mIsInUpdate = isInUpdate;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool result = false;
			if (msg == -103)
			{
				Quit();
				result = true;
			}
			return result;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mImpl.mBootProcess != null && mImpl.mBootProcess.OnTime())
			{
				UnRegisterInGlobalTime();
				mImpl.mBootProcess = null;
			}
		}

		public override void Suspend()
		{
			SuspendApp();
		}

		public override void Resume()
		{
			mImpl.mCommandHandler.Execute(-14);
		}

		public override void SaveGame()
		{
			mImpl.mFileManager.WriteAllObjects();
			mImpl.mFileManager.OnSave();
		}

		public virtual void Reset()
		{
			mImpl.Reset();
		}

		public virtual void Quit()
		{
			if (!GetKeyPressGenerator().IsActive())
			{
				SaveGame();
				FlApplication.Exit();
			}
		}

		public static GameApp Get()
		{
			return (GameApp)FlApplication.GetInstance();
		}

		public virtual Settings GetSettings()
		{
			return mImpl.mSettings;
		}

		public virtual Hourglass GetHourglass()
		{
			return mImpl.mHourglass;
		}

		public virtual SharedResourcesHandler GetSharedResourcesHandler()
		{
			return mImpl.mSharedResourcesHandler;
		}

		public virtual MediaPlayer GetMediaPlayer()
		{
			return mImpl.mMediaPlayer;
		}

		public virtual GameLibrary GetLibrary()
		{
			return mImpl.mGameLibrary;
		}

		public virtual FileManager GetFileManager()
		{
			return mImpl.mFileManager;
		}

		public virtual InputMapper GetInputMapper()
		{
			return mImpl.mInputMapper;
		}

		public virtual CommandHandler GetCommandHandler()
		{
			return mImpl.mCommandHandler;
		}

		public virtual GameFactory GetGameFactory()
		{
			return mImpl.mGameFactory;
		}

		public virtual GameSettings GetGameSettings()
		{
			return mImpl.mGameSettings;
		}

		public virtual Replay GetReplay()
		{
			return mImpl.mReplay;
		}

		public virtual GameTimeSystem GetGameTimeSystem()
		{
			return mImpl.mGameTimeSystem;
		}

		public virtual ReplayPackageLoader GetReplayLoader()
		{
			return mImpl.mReplayLoader;
		}

		public virtual AnimationController GetAnimator()
		{
			return mImpl.mAnimator;
		}

		public virtual PenInputController GetPenInputController()
		{
			return mImpl.mPenInputController;
		}

		public virtual TouchMenuReceiver GetTouchMenuReceiver()
		{
			return mImpl.mTouchMenuReceiver;
		}

		public virtual TouchPopupReceiver GetTouchPopupReceiver()
		{
			return mImpl.mTouchPopupReceiver;
		}

		public virtual bool IsBooting()
		{
			return mImpl.mBootProcess != null;
		}

		public virtual LanguageManager GetLanguageManager()
		{
			return mImpl.mLanguageManager;
		}

		public virtual CheatActivationController GetCheatActivationController()
		{
			return mImpl.mCheatActivationController;
		}

		public virtual CheatContainer GetCheatContainer()
		{
			return mImpl.mCheatContainer;
		}

		public virtual SceneSlideShowController GetSceneSlideShowController()
		{
			return mImpl.mSceneSlideShowController;
		}

		public virtual KeyPressGenerator GetKeyPressGenerator()
		{
			return mImpl.mKeyPressGenerator;
		}

		public override void OnScreenSizeChange()
		{
			CommandHandler mCommandHandler = mImpl.mCommandHandler;
			BaseScene currentScene = mCommandHandler.GetCurrentScene();
			if (currentScene != null)
			{
				currentScene.OnScreenSizeChange();
			}
			bool mCurrResolutionSupported = mImpl.mCurrResolutionSupported;
			bool flag = IsScreenResolutionSupported();
			mImpl.mCurrResolutionSupported = flag;
			if (!flag)
			{
				PushChangeScreenResolution();
			}
			else if (currentScene != null)
			{
				PopChangeScreenResolution();
			}
			Hourglass mHourglass = mImpl.mHourglass;
			if (mHourglass != null && mHourglass.IsLoaded())
			{
				short left = (short)((GetRectWidth() - 32) / 2);
				short top = (short)((GetRectHeight() - 32) / 2);
				mHourglass.SetTopLeft(left, top);
			}
		}

		public virtual bool IsScreenResolutionSupported()
		{
			DisplayContext mainDisplayContext = DisplayManager.GetMainDisplayContext();
			bool flag = false;
			return mainDisplayContext.IsResolution(true, 0);
		}

		public static int InitializeRandomSeed()
		{
			int num = (int)FlApplication.GetRealTime();
			FlMath.Seed(num);
			return num;
		}

		public virtual GameRandom GetGameRandom()
		{
			return mImpl.mGameRandom;
		}

		public virtual BioStatistics GetBioStatistics()
		{
			return mImpl.mBioStatistics;
		}

		public virtual CareerStatistics GetCareerStatistics()
		{
			return mImpl.mCareerStatistics;
		}

		public virtual ExpertManager GetExpertManager()
		{
			return mImpl.mExpertManager;
		}

		public virtual TipChooser GetTipChooser()
		{
			return mImpl.mTipChooser;
		}

		public virtual void SuspendApp()
		{
			MediaPlayer mediaPlayer = Get().GetMediaPlayer();
			mediaPlayer.StopMusic();
			mediaPlayer.SetMenuMusicPlaying(false);
			mImpl.mCommandHandler.Execute(-13);
		}

		public virtual void PushChangeScreenResolution()
		{
			CommandHandler commandHandler = GetCommandHandler();
			BaseScene currentScene = commandHandler.GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 40)
			{
				GameController gameController = (GameController)currentScene;
				SetClipChildren(true);
				gameController.OnValidScreenOrientation(false);
			}
			else
			{
				commandHandler.Execute(-51);
			}
		}

		public virtual void PopChangeScreenResolution()
		{
			CommandHandler commandHandler = GetCommandHandler();
			BaseScene currentScene = commandHandler.GetCurrentScene();
			if (currentScene != null && currentScene.GetId() == 40)
			{
				GameController gameController = (GameController)currentScene;
				gameController.OnValidScreenOrientation(true);
			}
			else
			{
				commandHandler.Execute(-52);
			}
		}

		public static GameApp[] InstArrayGameApp(int size)
		{
			GameApp[] array = new GameApp[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameApp();
			}
			return array;
		}

		public static GameApp[][] InstArrayGameApp(int size1, int size2)
		{
			GameApp[][] array = new GameApp[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameApp[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameApp();
				}
			}
			return array;
		}

		public static GameApp[][][] InstArrayGameApp(int size1, int size2, int size3)
		{
			GameApp[][][] array = new GameApp[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameApp[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameApp[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameApp();
					}
				}
			}
			return array;
		}
	}
}
