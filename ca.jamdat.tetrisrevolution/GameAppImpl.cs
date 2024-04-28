using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameAppImpl
	{
		public MediaPlayer mMediaPlayer;

		public Settings mSettings;

		public GameLibrary mGameLibrary;

		public Hourglass mHourglass;

		public BootAppProcess mBootProcess;

		public GameFactory mGameFactory;

		public GameSettings mGameSettings;

		public FileManager mFileManager;

		public InputMapper mInputMapper;

		public SharedResourcesHandler mSharedResourcesHandler;

		public CommandHandler mCommandHandler;

		public AnimationController mAnimator;

		public SceneTransitionController mSceneTransitionController;

		public SceneStack mSceneStack;

		public LanguageManager mLanguageManager;

		public CheatContainer mCheatContainer;

		public CheatActivationController mCheatActivationController;

		public SceneSlideShowController mSceneSlideShowController;

		public KeyPressGenerator mKeyPressGenerator;

		public bool mCurrResolutionSupported;

		public Replay mReplay;

		public GameTimeSystem mGameTimeSystem;

		public ReplayPackageLoader mReplayLoader;

		public GameRandom mGameRandom;

		public PenInputController mPenInputController;

		public TouchMenuReceiver mTouchMenuReceiver;

		public TouchPopupReceiver mTouchPopupReceiver;

		public BioStatistics mBioStatistics;

		public CareerStatistics mCareerStatistics;

		public ExpertManager mExpertManager;

		public TipChooser mTipChooser;

		public virtual void destruct()
		{
			mMediaPlayer.Terminate();
			mSceneStack.PopAll();
			mSceneTransitionController.Terminate();
			mHourglass.Unload();
			mCheatActivationController.Terminate();
			mSharedResourcesHandler.ReleaseMenusResources();
			mSharedResourcesHandler.ReleaseAppResources();
			mSceneStack = null;
			mSceneTransitionController = null;
			mCommandHandler = null;
			mAnimator = null;
			mSharedResourcesHandler = null;
			mInputMapper = null;
			mGameSettings = null;
			mBootProcess = null;
			mGameFactory = null;
			mFileManager = null;
			mHourglass = null;
			mGameLibrary = null;
			mSettings = null;
			mMediaPlayer = null;
			if (mTouchMenuReceiver != null)
			{
				mTouchMenuReceiver = null;
			}
			if (mTouchPopupReceiver != null)
			{
				mTouchPopupReceiver = null;
			}
			mPenInputController = null;
			mLanguageManager = null;
			mCheatActivationController = null;
			mCheatContainer = null;
			mSceneSlideShowController = null;
			mKeyPressGenerator.Deactivate();
			mKeyPressGenerator = null;
			mGameRandom = null;
			mReplay = null;
			mGameTimeSystem = null;
			mReplayLoader = null;
			mBioStatistics = null;
			mCareerStatistics = null;
			mExpertManager = null;
			mTipChooser = null;
		}

		public virtual void Initialize()
		{
			mLanguageManager = new LanguageManager();
			mMediaPlayer = new MediaPlayer();
			mSettings = new Settings();
			mGameLibrary = new GameLibrary();
			mHourglass = new Hourglass();
			mReplay = new Replay();
			mGameTimeSystem = new GameTimeSystem();
			mReplayLoader = new ReplayPackageLoader();
			mFileManager = new FileManager();
			mBootProcess = new BootAppProcess();
			mGameSettings = new GameSettings();
			mGameFactory = new GameFactory();
			mInputMapper = new InputMapper();
			mSharedResourcesHandler = new SharedResourcesHandler();
			mSceneTransitionController = new SceneTransitionController();
			mSceneStack = new SceneStack();
			mCommandHandler = new CommandHandler();
			mAnimator = new AnimationController();
			mPenInputController = new PenInputController();
			mTouchMenuReceiver = new TouchMenuReceiver();
			mTouchPopupReceiver = new TouchPopupReceiver();
			mCommandHandler.Initialize(mSceneTransitionController, mSceneStack);
			mSceneSlideShowController = new SceneSlideShowController();
			mCheatContainer = new CheatContainer();
			mCheatActivationController = new CheatActivationController();
			InitializeCheats();
			mKeyPressGenerator = new KeyPressGenerator();
			mGameRandom = new GameRandom();
			GameRandom.InitSeed((int)FlApplication.GetRealTime());
			mBioStatistics = new BioStatistics();
			mCareerStatistics = new CareerStatistics();
			mExpertManager = new ExpertManager();
			mTipChooser = new TipChooser();
		}

		public virtual void Reset()
		{
			mFileManager.ResetSegmentStream();
			mGameSettings.Reset();
			mSettings.Reset();
			mBioStatistics.Reset();
			mCareerStatistics.Reset();
			mExpertManager.Reset();
			mTipChooser.Reset();
		}

		public virtual void InitializeCheats()
		{
			int[] code = new int[4] { 25, 24, 25, 26 };
			Cheat cheat = new LanguageCheat();
			cheat.Initialize(0, code, 4, 2);
			mCheatContainer.AddCheat(cheat);
			int[] code2 = new int[4] { 25, 24, 25, 25 };
			Cheat cheat2 = new SceneSlideShowCheat();
			cheat2.Initialize(1, code2, 4, 0);
			mCheatContainer.AddCheat(cheat2);
			int[] code3 = new int[4] { 25, 24, 26, 17 };
			Cheat cheat3 = new CommandCheats();
			cheat3.Initialize(7, code3, 4, 3);
			mCheatContainer.AddCheat(cheat3);
			int[] code4 = new int[4] { 25, 24, 26, 18 };
			Cheat cheat4 = new WriteFileCheat();
			cheat4.Initialize(6, code4, 4, 0);
			mCheatContainer.AddCheat(cheat4);
			int[] code5 = new int[4] { 25, 24, 26, 26 };
			Cheat cheat5 = new KeyPressGeneratorCheat();
			cheat5.Initialize(8, code5, 4, 0);
			mCheatContainer.AddCheat(cheat5);
			int[] code6 = new int[4] { 22, 20, 26, 24 };
			Cheat cheat6 = new TetrisCheat();
			cheat6.Initialize(19, code6, 4, 0);
			mCheatContainer.AddCheat(cheat6);
			int[] code7 = new int[4] { 22, 23, 19, 22 };
			Cheat cheat7 = new TetrisCheat();
			cheat7.Initialize(20, code7, 4, 0);
			mCheatContainer.AddCheat(cheat7);
			int[] code8 = new int[4] { 20, 20, 19, 25 };
			Cheat cheat8 = new TetrisCheat();
			cheat8.Initialize(21, code8, 4, 0);
			mCheatContainer.AddCheat(cheat8);
			int[] code9 = new int[4] { 24, 20, 23, 24 };
			Cheat cheat9 = new TetrisCheat();
			cheat9.Initialize(22, code9, 4, 0);
			mCheatContainer.AddCheat(cheat9);
			int[] code10 = new int[4] { 21, 20, 25, 21 };
			Cheat cheat10 = new TetrisCheat();
			cheat10.Initialize(23, code10, 4, 0);
			mCheatContainer.AddCheat(cheat10);
			int[] code11 = new int[4] { 21, 20, 25, 23 };
			Cheat cheat11 = new TetrisCheat();
			cheat11.Initialize(24, code11, 4, 0);
			mCheatContainer.AddCheat(cheat11);
			int[] code12 = new int[4] { 25, 21, 24, 24 };
			Cheat cheat12 = new TetrisCheat();
			cheat12.Initialize(25, code12, 4, 0);
			mCheatContainer.AddCheat(cheat12);
		}

		public static GameAppImpl[] InstArrayGameAppImpl(int size)
		{
			GameAppImpl[] array = new GameAppImpl[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameAppImpl();
			}
			return array;
		}

		public static GameAppImpl[][] InstArrayGameAppImpl(int size1, int size2)
		{
			GameAppImpl[][] array = new GameAppImpl[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameAppImpl[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameAppImpl();
				}
			}
			return array;
		}

		public static GameAppImpl[][][] InstArrayGameAppImpl(int size1, int size2, int size3)
		{
			GameAppImpl[][][] array = new GameAppImpl[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameAppImpl[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameAppImpl[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameAppImpl();
					}
				}
			}
			return array;
		}
	}
}
