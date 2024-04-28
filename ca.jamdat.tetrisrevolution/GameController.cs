using ca.jamdat.flight;
using Microsoft.Xna.Framework.Media;

namespace ca.jamdat.tetrisrevolution
{
	public class GameController : BaseScene
	{
		public const sbyte kActionInvalid = -1;

		public const sbyte kRotateCCW = 0;

		public const sbyte kRotateCW = 1;

		public const sbyte kMoveLeft = 2;

		public const sbyte kMoveRight = 3;

		public const sbyte kSoftDrop = 4;

		public const sbyte kHardDrop = 5;

		public const sbyte kHold = 6;

		public const sbyte kInvalidState = -1;

		public const sbyte kNormalState = 0;

		public const sbyte kNeedToPushWarningScreen = 1;

		public const sbyte kShowingWarningScreen = 2;

		public const sbyte kNeedToPopWarningScreen = 3;

		private const sbyte NUM_VDPAD_SPRITES = 4;

		public const sbyte kInitializingFirstGame = 0;

		public const sbyte kCreateNewGame = 1;

		public const sbyte kLoadingNewGame = 2;

		public const sbyte kReady = 3;

		public const sbyte kDisplayGameOver = 4;

		public const sbyte kWaiting = 5;

		public const sbyte kWaitingInPause = 6;

		public const sbyte kLoadingPopup = 7;

		public const sbyte kSuspended = 8;

		public AnimationController mAnimator;

		public AnimationManager mAnimationManager;

		public FeedbackDisplayManager mFeedbackDisplayManager;

		public Settings mSettings;

		public LayerComponent mLayerComponent;

		public KeyHandler mKeyHandler;

		public TetrisGame mGame;

		public sbyte mCurrentPopupId;

		public Popup mCurrentPopup;

		public sbyte mHidingPopupId;

		public Popup mHidingPopup;

		public Viewport mHoldHudTetriminoHolderViewport;

		public TimeHUD mRemainingTimeHud;

		public GoalHUD mGameSpecificHud;

		public LevelHUD mLevelHud;

		public ScoreHUD mScoreHud;

		public TPMHUD mTPMHud;

		public sbyte mGameControllerState;

		public bool mResumingFromMenu;

		public bool mNewGame;

		public Shape mHoldHudInnerShape;

		public bool mLoadingGameSuspended;

		public GameLabelHUD mGameLabelHud;

		public TouchGSReceiver mTouchGSReceiver;

		public Selection[] mVirtualDPadTouchSelections;

		public IndexedSprite[] mVirtualDPadTouchSprites;

		public EndGamePopupStateMachine mEndgamePopupStateMachine;

		public bool mIsPaused;

		public bool mNeedToEnterDelayedPauseAfterInterrupt;

		public bool mGameInitializationComplete;

		public sbyte mScreenOrientationState;

		public Viewport mScreenOrientationViewport;

		public Sprite mBackgroundBottomArrow;

		public Sprite mBackgroundBottomTouch;

		private readonly sbyte NUM_VDPAD_SELECTIONS;

		public GameController(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			NUM_VDPAD_SELECTIONS = 7;
			mAnimator = GameApp.Get().GetAnimator();
			mSettings = GameApp.Get().GetSettings();
			mCurrentPopupId = 30;
			mHidingPopupId = 30;
			mGameControllerState = 0;
			mNewGame = true;
			mGameInitializationComplete = true;
			mScreenOrientationState = 0;
			mType = 16;
			mEndgamePopupStateMachine = new EndGamePopupStateMachine();
			GameApp.Get().GetMediaPlayer().SetMenuMusicPlaying(false);
		}

		public override void destruct()
		{
			if (mEndgamePopupStateMachine != null)
			{
				mEndgamePopupStateMachine = null;
			}
			mRemainingTimeHud = null;
			mGameSpecificHud = null;
			mLevelHud = null;
			mScoreHud = null;
			mTPMHud = null;
			mGameLabelHud = null;
			mEndgamePopupStateMachine = null;
			mFeedbackDisplayManager = null;
			mBackgroundBottomArrow = null;
			mBackgroundBottomTouch = null;
		}

		public override void Load()
		{
			base.Load();
			GameApp.Get().GetMediaPlayer().GetSoundResourcesHandler()
				.LoadGameSounds(589842);
			CreateHud();
			if (mGame == null)
			{
				CreateGame();
			}
			LoadGame();
		}

		public override bool IsLoaded()
		{
			GameApp gameApp = GameApp.Get();
			if (base.IsLoaded() && mGame != null && mGame.IsLoaded() && gameApp.GetMediaPlayer().GetSoundResourcesHandler().AreGameSoundsLoaded())
			{
				return ReplayPackageLoader.Get().IsLoaded();
			}
			return false;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			ReplayPackageLoader.Get().GetEntryPoints();
			mAnimator.LoadAnimations(mPackage, 4, 3, 7);
			mRemainingTimeHud.GetEntryPoints();
			mGameSpecificHud.GetEntryPoints();
			mLevelHud.GetEntryPoints();
			mScoreHud.GetEntryPoints();
			mTPMHud.GetEntryPoints();
			mGameLabelHud.GetEntryPoints();
			Color888.Cast(mPackage.GetEntryPoint(2), null);
			mVirtualDPadTouchSelections = new Selection[NUM_VDPAD_SELECTIONS];
			mVirtualDPadTouchSprites = new IndexedSprite[4];
			mBackgroundBottomArrow = EntryPoint.GetSprite(mPackage, 71);
			mBackgroundBottomTouch = EntryPoint.GetSprite(mPackage, 72);
			mBackgroundBottomArrow.SetViewport(mViewport);
			mBackgroundBottomTouch.SetViewport(mViewport);
			mBackgroundBottomArrow.SendToBack();
			mBackgroundBottomTouch.SendToBack();
			for (int i = 0; i < NUM_VDPAD_SELECTIONS; i++)
			{
				mVirtualDPadTouchSelections[i] = Selection.Cast(mPackage.GetEntryPoint(54 + i), null);
			}
			for (int j = 0; j < 4; j++)
			{
				mVirtualDPadTouchSprites[j] = IndexedSprite.Cast(mPackage.GetEntryPoint(54 + NUM_VDPAD_SELECTIONS + j), null);
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			InitSelectSoftkey();
			InitClearSoftkey();
			InitializeGame();
			UpdateGameState();
		}

		private void UpdateGameState()
		{
		}

		public override void Unload()
		{
			SetGameControllerState(8);
			GameApp gameApp = GameApp.Get();
			gameApp.GetMediaPlayer().StopMusic();
			gameApp.GetMediaPlayer().GetSoundResourcesHandler().UnloadGameSounds();
			if (mHidingPopup != null)
			{
				ReleasePopup();
			}
			if (mCurrentPopup != null)
			{
				ReleasePopup();
			}
			if (mRemainingTimeHud != null)
			{
				mRemainingTimeHud.Unload();
				mRemainingTimeHud = null;
			}
			if (mGameSpecificHud != null)
			{
				mGameSpecificHud.Unload();
				mGameSpecificHud = null;
			}
			if (mLevelHud != null)
			{
				mLevelHud.Unload();
				mLevelHud = null;
			}
			if (mScoreHud != null)
			{
				mScoreHud.Unload();
				mScoreHud = null;
			}
			if (mTPMHud != null)
			{
				mTPMHud.Unload();
				mTPMHud = null;
			}
			if (mGameLabelHud != null)
			{
				mGameLabelHud.Unload();
				mGameLabelHud = null;
			}
			if (mHoldHudTetriminoHolderViewport != null)
			{
				mHoldHudTetriminoHolderViewport.SetViewport(null);
				mHoldHudTetriminoHolderViewport = null;
			}
			if (mKeyHandler != null)
			{
				mKeyHandler = null;
			}
			CleanWellComponentAndGame();
			if (mAnimator != null)
			{
				mAnimator.UnloadAnimations(4, 7);
			}
			if (mTouchGSReceiver != null)
			{
				mTouchGSReceiver.Unload();
				mTouchGSReceiver = null;
			}
			mVirtualDPadTouchSelections = null;
			mVirtualDPadTouchSprites = null;
			if (mFeedbackDisplayManager != null)
			{
				mFeedbackDisplayManager.Unload();
			}
			if (mScreenOrientationViewport != null)
			{
				mScreenOrientationViewport.SetViewport(null);
				mScreenOrientationViewport = null;
			}
			ReplayPackageLoader.Get().Unload();
			GameApp.Get().GetGameFactory().ReleaseGame();
			mGame = null;
			base.Unload();
		}

		public override void Suspend()
		{
			base.Suspend();
			if (mCurrentPopup != null)
			{
				mCurrentPopup.UnRegisterInGlobalTime();
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			if (mHidingPopup != null)
			{
				mHidingPopup.UnRegisterInGlobalTime();
				mHidingPopup.Unload();
				mHidingPopup = null;
			}
			SetGameControllerState(8);
		}

		public override void Resume()
		{
			if (mScreenOrientationState == 2)
			{
				GameApp.Get().OnScreenSizeChange();
				return;
			}
			ResumeIntoGame();
			base.Resume();
		}

		public override void OnSceneAttached()
		{
			base.OnSceneAttached();
			StartGame();
			if (mResumingFromMenu)
			{
				ResumingFromMenu();
			}
			GameApp.Get().GetHourglass().SetTopLeft(224, 384);
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mView != null && mView.IsAttached() && IsLoaded())
			{
				if (mScreenOrientationState == 1)
				{
					OnValidScreenOrientation(false);
				}
				else if (mScreenOrientationState == 3)
				{
					OnValidScreenOrientation(true);
				}
				GameApp.Get().SetClipChildren(false);
			}
			if (mNeedToEnterDelayedPauseAfterInterrupt && mGame.GetCurrentStateID() != 0)
			{
				mNeedToEnterDelayedPauseAfterInterrupt = false;
				OnCommand(GetInitialClearSoftkeyCommand());
				return;
			}
			switch (mGameControllerState)
			{
			case 3:
			{
				sbyte currentStateID = mGame.GetCurrentStateID();
				bool recordOnTime = currentStateID != 2 && currentStateID != 1 && currentStateID != 0;
				GameTimeSystem.OnTime(this, mGame.GetPlayTimeMs(), deltaTimeMs, recordOnTime);
				break;
			}
			case 1:
				CreateNewGame();
				break;
			case 2:
				if (mGame.IsLoaded())
				{
					mNewGame = true;
					mGameSpecificHud.DetachHud();
					InitializeGame();
					StartGame();
					mView.TakeFocus();
					mGameInitializationComplete = true;
				}
				break;
			case 7:
				if (mHidingPopup == null && mCurrentPopup.IsLoaded())
				{
					mCurrentPopup.GetEntryPoints();
					mCurrentPopup.Initialize();
					ShowPopup();
				}
				break;
			case 4:
				if (mGameControllerState != 8 && !Replay.Get().IsPlaying())
				{
					mEndgamePopupStateMachine.Start();
					OnCommand(mEndgamePopupStateMachine.NextCommand());
				}
				else
				{
					OnReplayEnd();
				}
				break;
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public virtual bool IsMenuScene()
		{
			return false;
		}

		public override bool OnCommand(int command)
		{
			if (!IsReadyForCommands())
			{
				return true;
			}
			bool flag = true;
			switch (command)
			{
			case -19:
				LoadPopup(33);
				break;
			case -20:
				LoadPopup(34);
				break;
			case -21:
				LoadPopup(35);
				break;
			case -29:
				LoadPopup(36);
				break;
			case -28:
				if (GameApp.Get().GetIsDemo())
				{
					LoadPopup(57);
				}
				else
				{
					LoadPopup(38);
				}
				break;
			case 8:
				HidePopup();
				ReturnFromPauseMenu(true);
				break;
			case 5:
				LoadPopup(49);
				break;
			case 6:
				FlPenManager.Get().Deactivate();
				if (mCurrentPopup != null)
				{
					mCurrentPopup.Hide();
					ManageSoftkeysVisibility(mCurrentPopup, true);
					ReleasePopup();
				}
				ReturnFromPauseMenu(false);
				SetGameControllerState(1);
				mIsPaused = false;
				MediaPlayer.Get().StopMusic();
				break;
			case 1:
				HidePopup();
				ManageSoftkeysVisibility(mCurrentPopup, true);
				ReturnFromPauseMenu(false);
				mEndgamePopupStateMachine.Reset();
				SetGameControllerState(1);
				mIsPaused = false;
				MediaPlayer.Get().StopMusic();
				break;
			case 3:
				HidePopup();
				break;
			case 2:
				HidePopup();
				ManageSoftkeysVisibility(mCurrentPopup, false);
				break;
			case 4:
			case 32:
				HidePopup();
				break;
			case 20:
				LoadPopup(39);
				break;
			case 21:
				LoadPopup(40);
				break;
			case 19:
				LoadPopup(46);
				break;
			case 24:
				LoadPopup(41);
				break;
			case 25:
				LoadPopup(42);
				break;
			case 26:
				LoadPopup(43);
				break;
			case 27:
				LoadPopup(44);
				break;
			case 28:
				LoadPopup(45);
				break;
			case 29:
				LoadPopup(47);
				break;
			case 22:
				LoadPopup(48);
				break;
			case -34:
				LoadPopup(58);
				break;
			case -37:
				LoadPopup(59);
				break;
			case -38:
				LoadPopup(60);
				break;
			case 38:
				LoadPopup(37);
				break;
			case 7:
				LoadPopup(32);
				break;
			case -10:
				mCurrentPopup.OnCommand(command);
				break;
			default:
				flag = false;
				break;
			}
			if (!flag && mCurrentPopup != null)
			{
				flag = mCurrentPopup.OnCommand(command);
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public override void ReceiveFocus()
		{
			if (mGameControllerState != 7 && mCurrentPopup != null && mCurrentPopup.GetPopupState() != 4 && mGameControllerState != 8)
			{
				mCurrentPopup.TakeFocus();
			}
			else
			{
				base.ReceiveFocus();
			}
		}

		public virtual void OnGameOnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (!mAnimator.IsPlaying(9))
			{
				mKeyHandler.OnTime(0, deltaTimeMs);
				mGame.OnTime(0, deltaTimeMs);
				UpdateGameUI(deltaTimeMs);
			}
		}

		public virtual void OnKeyReplay()
		{
			Replay replay = GameApp.Get().GetReplay();
			int currentTimeIndex = replay.GetCurrentTimeIndex();
			if (replay.IsPlaying())
			{
				ReplayEvent replayEvent = replay.PeekEvent();
				while (replay.HasNextEvent() && replayEvent != null && replayEvent.GetTime() == currentTimeIndex && ReplayEvent.IsKeyEvent(replayEvent.GetType()))
				{
					mGame.PlayEvent(replay.PopEvent());
					replayEvent = replay.PeekEvent();
				}
			}
		}

		public virtual void OnReplayEnd()
		{
			OnCommand(GetInitialClearSoftkeyCommand());
		}

		public override void StartOpeningAnims()
		{
			base.StartOpeningAnims();
			if (mGame.GetCurrentStateID() == 0)
			{
				SetTouchArrowVisibility(false);
				mAnimator.StartMenuAnimation(9);
			}
		}

		public override bool IsOpeningAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !mAnimator.IsPlaying(9);
			}
			return false;
		}

		public override void OnOpeningAnimsEnded()
		{
			base.OnOpeningAnimsEnded();
			bool flag = GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad();
			SetBackgroundVisibility(flag);
			SetTouchArrowVisibility(flag);
		}

		public virtual Viewport GetViewport()
		{
			return mViewport;
		}

		public virtual TetrisGame GetGame()
		{
			return mGame;
		}

		public virtual AnimationManager GetAnimationManager()
		{
			return mAnimationManager;
		}

		public virtual LayerComponent GetLayerComponent()
		{
			return mLayerComponent;
		}

		public virtual void UpdateNextPiece(bool startAnim)
		{
			Tetrimino nextTetrimino = mGame.GetNextTetrimino();
			LayerComponent layerComponent = mLayerComponent;
			if (nextTetrimino != null && layerComponent != null)
			{
				for (int i = 0; i < 5; i++)
				{
					nextTetrimino = mGame.GetNextTetrimino(i);
					nextTetrimino.SetAllMinoAspectSize(1);
					layerComponent.AttachToNextQueueLayer(nextTetrimino, i);
				}
			}
			if (startAnim)
			{
				mAnimator.StartGameAnimation(6);
			}
		}

		public virtual void ManageGameOver()
		{
			SetGameControllerState(4);
			if (mGame.IsGameTimeExpired())
			{
				mRemainingTimeHud.UpdateCountDown(mGame.GetTimeLimit());
			}
		}

		public virtual void UpdateGoalHUD()
		{
			if (mGame.IsGoalHUDEnabled())
			{
				mGameSpecificHud.Update();
			}
		}

		public virtual void UpdateLevelHUD()
		{
			mLevelHud.Update();
		}

		public virtual void SwapFallingAndHoldTetrinimos()
		{
			TetrisGame tetrisGame = mGame;
			mLayerComponent.AttachTetriminoInNextHudOrHoldHud(tetrisGame.GetHeldTetrimino(), 5);
			mLayerComponent.AttachTetrimino(tetrisGame.GetFallingTetrimino(), 2);
		}

		public virtual void SetLoadingGameSuspended(bool suspended)
		{
			mLoadingGameSuspended = suspended;
		}

		public virtual void StartGame()
		{
			StartMusic();
			mView.RegisterInGlobalTime();
			if (mLoadingGameSuspended)
			{
				mLoadingGameSuspended = false;
				if (!mResumingFromMenu)
				{
					OnCommand(-19);
				}
			}
		}

		public virtual void PrepareFeedbackDisplay(int feedbackType)
		{
			mFeedbackDisplayManager.PrepareFeedbackDisplay(feedbackType, mGame.GetGameScore());
		}

		public virtual void DisplayFeedbackDisplay()
		{
			mFeedbackDisplayManager.DisplayFeedback();
		}

		public virtual void UpdateFeedbackDisplay()
		{
			mFeedbackDisplayManager.UpdateFeedbackDisplay(mGame);
		}

		public override void StartMusic()
		{
			GameApp gameApp = GameApp.Get();
			MediaPlayer mediaPlayer = gameApp.GetMediaPlayer();
			if (mediaPlayer.GetSoundResourcesHandler().AreGameSoundsLoaded() && mCurrentPopupId != 39 && mCurrentPopupId != 40)
			{
				int num = -1;
				int num2 = mGame.GetCurrentLevel() - 1;
				if (num == -1 && num2 >= 0 && num2 <= 4)
				{
					num = 2;
				}
				if (num == -1 && num2 >= 5 && num2 <= 9)
				{
					num = 3;
				}
				if (num == -1 && num2 >= 10 && num2 <= 14)
				{
					num = 4;
				}
				if (mCurrentPopupId == 34)
				{
					mediaPlayer.PlayMusic(1, false);
				}
				else if (num != -1)
				{
					mediaPlayer.PlayMusic(num, true);
				}
			}
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = true;
			int num = 0;
			switch (msg)
			{
			case 1:
				OnHidePopup();
				break;
			case -125:
			{
				TetrisGame tetrisGame = mGame;
				if (source == mVirtualDPadTouchSelections[2])
				{
					num = 3;
				}
				else if (source == mVirtualDPadTouchSelections[3])
				{
					num = 4;
				}
				if (source == mVirtualDPadTouchSelections[4])
				{
					num = 2;
				}
				else if (intParam == 1)
				{
					if (tetrisGame.CanMoveFallingTetrimino())
					{
						if (source == mVirtualDPadTouchSelections[0])
						{
							num = 24;
						}
						else if (source == mVirtualDPadTouchSelections[1])
						{
							num = 7;
						}
						else if (source == mVirtualDPadTouchSelections[5])
						{
							num = 1;
						}
						else if (source == mVirtualDPadTouchSelections[6])
						{
							num = 17;
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
				break;
			}
			default:
				flag = false;
				break;
			}
			if (num != 0)
			{
				if (intParam == 1)
				{
					OnKeyDown(num);
				}
				else
				{
					OnKeyUp(num);
				}
			}
			if (!flag)
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public override bool IsReadyForCommands()
		{
			if (mGameControllerState != 8)
			{
				if (mGameControllerState != 7 && mGameControllerState != 2)
				{
					return base.IsReadyForCommands();
				}
				return false;
			}
			return true;
		}

		public virtual void OnValidScreenOrientation(bool isValid)
		{
			if (!IsLoaded() || mView == null || !mView.IsAttached())
			{
				if (isValid)
				{
					mScreenOrientationState = 3;
				}
				else
				{
					mScreenOrientationState = 1;
				}
				return;
			}
			if (isValid)
			{
				if (mScreenOrientationViewport != null)
				{
					mScreenOrientationViewport.SetViewport(null);
				}
				ResizeScreenComponents();
				mView.RegisterInGlobalTime();
				FlPenManager.Get().Activate();
				if (mEndgamePopupStateMachine.IsShowingEndGamePopups())
				{
					if (mCurrentPopup != null)
					{
						mCurrentPopup.SetSoftKeys();
					}
					ReceiveFocus();
				}
				ResumeIntoGame();
				mScreenOrientationState = 0;
				return;
			}
			mScreenOrientationState = 2;
			GameApp.Get().TakeFocus();
			FlPenManager.Get().Deactivate();
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			Suspend();
			if ((mView != null || mView.IsAttached()) && mView.IsRegisteredInGlobalTime())
			{
				mView.UnRegisterInGlobalTime();
			}
			if (mScreenOrientationViewport == null)
			{
				ScreenOrientationInitialize();
			}
			mScreenOrientationViewport.SetViewport(mViewport);
			ResizeScreenComponents();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(7, 0);
		}

		public static int GetInitialClearSoftkeyCommand()
		{
			Replay replay = Replay.Get();
			if (replay.IsPlaying())
			{
				switch (GameApp.Get().GetGameSettings().GetPlayMode())
				{
				case 0:
					return -17;
				case 5:
					return -37;
				case 6:
					return -38;
				default:
				{
					ReplayPackageLoader replayPackageLoader = ReplayPackageLoader.Get();
					if (replayPackageLoader.IsReplayIsGlossary())
					{
						return 79;
					}
					if (replayPackageLoader.IsReplayIsMaster())
					{
						return 80;
					}
					return -19;
				}
				}
			}
			return -19;
		}

		public static int GetInitialClearSoftkeyFunction()
		{
			ReplayPackageLoader replayPackageLoader = ReplayPackageLoader.Get();
			if (replayPackageLoader.IsThereAReplaySet())
			{
				return 1;
			}
			return 4;
		}

		public virtual void ResumeIntoGame()
		{
			if (mGame == null)
			{
				return;
			}
			if (mGame.GetCurrentStateID() == 0)
			{
				mNeedToEnterDelayedPauseAfterInterrupt = true;
				if (mGameInitializationComplete)
				{
					mGame.GotoWaitingForIntroductionState();
				}
				else
				{
					SetGameControllerState(2);
				}
			}
			else
			{
				mIsPaused = true;
				if (!mEndgamePopupStateMachine.IsShowingEndGamePopups())
				{
					OnCommand(GetInitialClearSoftkeyCommand());
				}
				else
				{
					OnCommand(mEndgamePopupStateMachine.CurrentCommand());
				}
			}
		}

		public virtual void ResumingFromMenu()
		{
			mGame.OnResume();
			if (mGameControllerState == 6)
			{
				OnCommand(-19);
				if (mGame.GetCurrentStateID() == 0)
				{
					mGame.GotoWaitingForIntroductionState();
				}
			}
		}

		public virtual void CreateNewGame()
		{
			GameApp.Get().TakeFocus();
			CleanWellComponentAndGame();
			mKeyHandler = null;
			CreateGame();
			LoadGame();
			mGameInitializationComplete = false;
			SetGameControllerState(2);
		}

		public virtual void CreateGame()
		{
			if (mKeyHandler != null)
			{
				mKeyHandler = null;
			}
			mGame = GameApp.Get().GetGameFactory().CreateNewGame();
			mKeyHandler = new KeyHandler(mGame);
		}

		public virtual void CleanWellComponentAndGame()
		{
			GameApp.Get().GetMediaPlayer().StopMusic();
			mGame.Unload();
			if (mAnimationManager != null)
			{
				mAnimationManager.Clean();
			}
			if (mLayerComponent != null)
			{
				mLayerComponent.Clean();
			}
			mAnimationManager = null;
			mLayerComponent = null;
			if (mFeedbackDisplayManager != null)
			{
				mFeedbackDisplayManager.Unload();
				mFeedbackDisplayManager = null;
			}
		}

		public virtual void LoadGame()
		{
			mGame.Load();
			ReplayPackageLoader.Get().Load();
		}

		public virtual void InitializeGame()
		{
			TetrisGame tetrisGame = mGame;
			tetrisGame.GetEntryPoints();
			mRemainingTimeHud.SetGame(tetrisGame);
			mGameSpecificHud.SetGame(tetrisGame);
			mLevelHud.SetGame(tetrisGame);
			mScoreHud.SetGame(tetrisGame);
			mTPMHud.SetGame(tetrisGame);
			mGameLabelHud.SetGame(tetrisGame);
			if (!GameApp.Get().GetGameSettings().IsMarathonMode())
			{
				mLevelHud.SetVisible(false);
				mRemainingTimeHud.SetVisible(true);
			}
			else
			{
				mLevelHud.SetVisible(true);
				mRemainingTimeHud.SetVisible(false);
			}
			bool flag = GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad();
			SetBackgroundVisibility(flag);
			mRemainingTimeHud.Reset();
			ResetKeyPressed();
			FlApplication.GetInstance().MapKey(16, 0);
			FlApplication.GetInstance().MapKey(15, 0);
			mLayerComponent = new LayerComponent();
			mLayerComponent.GetEntryPoints(mPackage);
			mLayerComponent.SetUILayersVisible(false);
			mFeedbackDisplayManager = new FeedbackDisplayManager(GetLayerComponent());
			mAnimationManager = new AnimationManager();
			mAnimationManager.GetEntryPoints(mPackage, mLayerComponent);
			mAnimationManager.CreateLineReboundAnim(tetrisGame.GetWell());
			if (tetrisGame.IsHoldHUDEnabled())
			{
				InitializeHeldPiece();
			}
			if (tetrisGame.IsGoalHUDEnabled())
			{
				mGameSpecificHud.Initialize(this);
			}
			mRemainingTimeHud.Initialize(this);
			mLevelHud.Initialize(this);
			mScoreHud.Initialize(this);
			mTPMHud.Initialize(this);
			mGameLabelHud.Initialize(this);
			mGameLabelHud.SetVisible(!flag);
			if (mNewGame)
			{
				mNewGame = false;
				tetrisGame.InitializeGame();
			}
			if (mGameControllerState != 6 && mGame.IsWaitingForInitialize())
			{
				SetGameControllerState(3);
			}
			if (mTouchGSReceiver == null)
			{
				mTouchGSReceiver = new TouchGSReceiver();
				mTouchGSReceiver.Initialize(this, mHoldHudTetriminoHolderViewport);
			}
			UpdateGoalHUD();
			UpdateNextPiece(false);
			tetrisGame.InitializeComponents(this);
			sbyte playMode = GameApp.Get().GetGameSettings().GetPlayMode();
			if (playMode == 5 || playMode == 6)
			{
				Text text = EntryPoint.GetText(mPackage, 44);
				FlString flString = EntryPoint.GetFlString(-2144239522, 22);
				text.SetTopLeft(text.GetRectLeft(), 140);
				text.SetCaption(flString);
				mAnimator.StartMenuAnimation(10);
			}
			else if (GameApp.Get().GetGameSettings().IsInIdleMode())
			{
				mAnimator.StartMenuAnimation(10);
			}
		}

		public virtual void InitializeHeldPiece()
		{
			mHoldHudTetriminoHolderViewport = EntryPoint.GetViewport(mPackage, 50);
			Tetrimino heldTetrimino = mGame.GetHeldTetrimino();
			if (heldTetrimino != null)
			{
				mLayerComponent.AttachTetriminoInNextHudOrHoldHud(heldTetrimino, 5);
			}
		}

		public virtual void InitSelectSoftkey()
		{
			int fction = 7;
			int command = 0;
			bool visible = false;
			mSelectSoftKey.SetFunction(fction, command);
			GetSoftkeyViewport().GetChild(0).SetVisible(visible);
		}

		public virtual void InitClearSoftkey()
		{
			mClearSoftKey.SetFunction(GetInitialClearSoftkeyFunction(), GetInitialClearSoftkeyCommand());
		}

		public virtual void OnHidePopupQuickHint()
		{
			if (GameApp.Get().GetGameSettings().GetPlayMode() == 0)
			{
				mClearSoftKey.SetFunction(1, -17);
				mSelectSoftKey.SetFunction(7, 0);
			}
			else if (Replay.Get().IsPlaying())
			{
				mSelectSoftKey.SetEnabled(false);
			}
			else
			{
				mClearSoftKey.SetFunction(4, -19);
				mSelectSoftKey.SetFunction(6, 7);
			}
			if (mCurrentPopupId != 33)
			{
				SetGameControllerState(3);
			}
		}

		public virtual void OnHidePopupLongHint()
		{
			mSelectSoftKey.SetFunction(6, 7);
			mClearSoftKey.SetFunction(4, -19);
			if (mCurrentPopupId != 33)
			{
				SetGameControllerState(3);
				mGame.ResumeInCountdown();
			}
		}

		public virtual void OnHidePopupTutorial()
		{
			mSelectSoftKey.SetFunction(6, 38);
			mClearSoftKey.SetFunction(4, -19);
			if (mCurrentPopupId != 33)
			{
				SetGameControllerState(3);
				mGame.ResumeInCountdown();
			}
		}

		public virtual void OnHidePopup()
		{
			GameApp.Get().GetInputMapper().ChangeMapping(2);
			sbyte b = ((mHidingPopupId == 30) ? mCurrentPopupId : mHidingPopupId);
			if (mCurrentPopupId == 48 && mGameControllerState != 7)
			{
				RetryPopup retryPopup = (RetryPopup)mCurrentPopup;
				int command = retryPopup.GetCommand();
				ReleasePopup();
				if (b == 48 && command != 1)
				{
					OnCommand(-17);
				}
			}
			else
			{
				ReleasePopup();
				if (mCurrentPopup == null && b == 33)
				{
					SetGameControllerState(3);
				}
				else
				{
					switch (b)
					{
					case 31:
						OnHidePopupQuickHint();
						break;
					case 37:
						OnHidePopupTutorial();
						break;
					case 32:
						OnHidePopupLongHint();
						break;
					default:
						if (mCurrentPopup == null && mGameControllerState != 8)
						{
							OnCommand(mEndgamePopupStateMachine.NextCommand());
						}
						break;
					case 34:
					case 35:
					case 36:
					case 38:
						break;
					}
				}
			}
			ResetKeyPressed();
		}

		public override bool OnKeyUp(int key)
		{
			bool flag = false;
			if (mGameControllerState == 3 && mKeyHandler.OnKeyUp(key))
			{
				flag = true;
			}
			if (!flag)
			{
				return base.OnKeyUp(key);
			}
			return true;
		}

		public override bool OnKeyDown(int key)
		{
			bool flag = false;
			if (mGameControllerState == 3 && mKeyHandler.OnKeyDown(key))
			{
				flag = true;
			}
			if (!flag)
			{
				return base.OnKeyDown(key);
			}
			return true;
		}

		public virtual void ResetKeyPressed()
		{
			mKeyHandler.ResetKeyPressed();
			mGame.OnResetKeyPressed();
		}

		public virtual void CreateHud()
		{
			mRemainingTimeHud = new TimeHUD();
			mGameSpecificHud = new GoalHUD();
			mLevelHud = new LevelHUD();
			mScoreHud = new ScoreHUD();
			mTPMHud = new TPMHUD();
			mGameLabelHud = new GameLabelHUD();
		}

		public virtual void HandleSoftDropEffect()
		{
			Tetrimino fallingTetrimino = mGame.GetFallingTetrimino();
			if (mGame.IsSoftDropping() && fallingTetrimino.CanMove(0, 1))
			{
				mGame.OnSoftDrop();
				mAnimationManager.ShowSoftDropTrail(fallingTetrimino);
			}
			else
			{
				mAnimationManager.HideSoftDropTrail();
			}
		}

		public virtual void SetGameControllerState(sbyte nextState)
		{
			mGameControllerState = nextState;
			mResumingFromMenu = nextState != 3;
		}

		public virtual void UpdateGameUI(int deltaTimeMs)
		{
			mRemainingTimeHud.OnTime(0, deltaTimeMs);
			mScoreHud.OnTime(0, deltaTimeMs);
			mTPMHud.OnTime(0, deltaTimeMs);
			sbyte currentStateID = mGame.GetCurrentStateID();
			if (currentStateID == 4 || currentStateID == 5)
			{
				HandleSoftDropEffect();
			}
		}

		public virtual void ReturnFromPauseMenu(bool isResuming)
		{
			InitSelectSoftkey();
			mClearSoftKey.SetFunction(4, -19);
			GameApp.Get().GetInputMapper().ChangeMapping(2);
			if (isResuming)
			{
				mGame.ResumeFromPauseMenu();
			}
			ResetKeyPressed();
			FlPenManager.Get().Activate();
			bool flag = GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad();
			SetBackgroundVisibility(flag);
			SetTouchArrowVisibility(flag);
			mGameLabelHud.SetVisible(!flag);
		}

		public virtual void LoadPopup(sbyte popupId)
		{
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Hide();
				mHidingPopupId = mCurrentPopupId;
				mHidingPopup = mCurrentPopup;
			}
			mCurrentPopupId = popupId;
			mCurrentPopup = Popup.CreateGamePopup(popupId, this, mSelectSoftKey, mClearSoftKey, mEndgamePopupStateMachine);
			SetGameControllerState(7);
			mCurrentPopup.Load();
		}

		public virtual void ReleasePopup()
		{
			if (mHidingPopup == null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
				mCurrentPopupId = 30;
			}
			else
			{
				mHidingPopup.SetDetachDimBackground(false);
				mHidingPopup.Unload();
				mHidingPopup = null;
				mHidingPopupId = 30;
			}
		}

		public virtual void ShowPopup()
		{
			mGame.OnPause();
			if (mGame.GetGameOverType() == 0)
			{
				mClearSoftKey.SetFunction(1, -19);
			}
			else
			{
				mClearSoftKey.SetFunction(7, 0);
			}
			mCurrentPopup.AttachPopupViewport(mViewport);
			mCurrentPopup.Show();
			mCurrentPopup.TakeFocus();
			ManageSoftkeysVisibility(mCurrentPopup, false);
			GameApp.Get().GetInputMapper().ChangeMapping(3);
			if (mCurrentPopupId == 33)
			{
				SetGameControllerState(6);
			}
			else
			{
				SetGameControllerState(5);
			}
			if (!mIsPaused)
			{
				mIsPaused = true;
				MediaPlayer.Get().PauseMusic();
			}
		}

		public virtual void HidePopup()
		{
			mCurrentPopup.Hide();
			ManageSoftkeysVisibility(mCurrentPopup, true);
			mTPMHud.Reset();
			mScoreHud.Reset();
			if (mIsPaused && mGame.GetGameOverType() == 0)
			{
				mIsPaused = false;
				if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
				{
					MediaPlayer.Get().ResumeMusic();
					return;
				}
				Settings settings = GameApp.Get().GetSettings();
				settings.SetSoundEnabled2(false);
			}
		}

		public virtual void SetTouchArrowVisibility(bool visible)
		{
			for (int i = 0; i < NUM_VDPAD_SELECTIONS; i++)
			{
				mVirtualDPadTouchSelections[i].SetVisible(visible);
			}
			for (int j = 0; j < 4; j++)
			{
				mVirtualDPadTouchSprites[j].SetVisible(visible);
			}
		}

		protected void SetBackgroundVisibility(bool isDPadVisible)
		{
			mBackgroundBottomArrow.SetVisible(isDPadVisible);
			mBackgroundBottomTouch.SetVisible(!isDPadVisible);
		}

		public virtual void ScreenOrientationInitialize()
		{
			mScreenOrientationViewport = EntryPoint.GetViewport(mPackage, 529);
			Shape shape = EntryPoint.GetShape(mPackage, 530);
			Scroller scroller = EntryPoint.GetScroller(mPackage, 531);
			Viewport viewport = EntryPoint.GetViewport(mPackage, 532);
			Text text = EntryPoint.GetText(mPackage, 533);
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			mScreenOrientationViewport.SetRect(screenRect);
			shape.SetRect(screenRect);
			scroller.SetRect(screenRect);
			viewport.SetRect(screenRect);
			text.CenterInRect(screenRect.GetLeft(), screenRect.GetTop(), screenRect.GetWidth(), screenRect.GetHeight());
			scroller.ResetScroller();
		}

		public virtual void ResizeScreenComponents()
		{
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			mView.SetRect(screenRect);
			mView.GetViewport().SetRect(screenRect);
			mViewport.SetRect(screenRect);
			Viewport viewport = EntryPoint.GetViewport(mPackage, 1);
			viewport.BringToFront();
			viewport.SetTopLeft(0, (short)(screenRect.GetHeight() - viewport.GetRectHeight()));
			viewport.SetSize(screenRect.GetWidth(), viewport.GetRectHeight());
			Component child = viewport.GetChild(1);
			child.SetTopLeft((short)(screenRect.GetWidth() - child.GetRectWidth()), child.GetRectTop());
			mSelectSoftKey.UpdatePos(5);
			mClearSoftKey.UpdatePos(6);
		}
	}
}
