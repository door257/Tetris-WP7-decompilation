using ca.jamdat.flight;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class MainMenu : RotatingSelectorMenu
	{
		public const int stateMainMenu = 0;

		public const int stateSelectMarathonLevel = 1;

		public const int stateSetMarathonLevel = 2;

		public const int stateShowTutorial = 3;

		public const int stateOptionsMenu = 4;

		public const int stateResetGameConfirmation = 5;

		public const int stateHelpMenu = 6;

		public const int stateExitAppConfirmation = 7;

		public const int stateLeaving = 8;

		public const int stateLeaderboardMenu = 9;

		public const int stateDemoPurchaseConfirmation = 10;

		public const int stateDemoFeatLockedPurchaseConfirmation = 11;

		public const int stateAchievementsMenu = 12;

		public int mMainMenuTimedOutCounterMs;

		public Sprite mLogoSprite;

		public int mMainMenuState;

		public Popup mCurrentPopup;

		public Popup mHidingPopup;

		public int mCheckIsTrialInterval;

		public int mTimeElapsed;

		public bool mIsDemo;

		public MainMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			mMainMenuTimedOutCounterMs = 30000;
			mMainMenuState = 0;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			GameApp gameApp = GameApp.Get();
			gameApp.GetGameSettings().SetUserDoingTutorial(false);
			gameApp.GetReplay().SetReplayMode(0);
			mView.RegisterInGlobalTime();
			Viewport viewport = (Viewport)mViewport.GetChild(0);
			mLogoSprite = (Sprite)viewport.GetChild(0);
			InitializeSoftkeys();
			mIsDemo = GameApp.Get().GetIsDemo();
			mMainMenuState = 0;
			mCheckIsTrialInterval = 30000;
			mTimeElapsed = 0;
			AchievementManager.Update();
			mRotatingMenu.mIsMainMenu = true;
		}

		public override void Unload()
		{
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			if (mHidingPopup != null)
			{
				mHidingPopup.Unload();
				mHidingPopup = null;
			}
			if (mView != null)
			{
				mView.UnRegisterInGlobalTime();
			}
			base.Unload();
		}

		public override void ReceiveFocus()
		{
			if (mHidingPopup != null)
			{
				GameApp.Get().TakeFocus();
			}
			else if (mMainMenuState != 0)
			{
				mCurrentPopup.TakeFocus();
				EnableSelector(false);
			}
			else
			{
				base.ReceiveFocus();
				EnableSelector(true);
			}
		}

		public override void CreateOpeningAnims()
		{
			base.CreateOpeningAnims();
			KeyFrameController keyFrameController = EntryPoint.GetKeyFrameController(1507374, 3);
			keyFrameController.SetControllee(mLogoSprite);
			int num = 200;
			num += 333;
			num += 541;
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, num);
		}

		public override void CreateClosingAnims(int startTime)
		{
			base.CreateClosingAnims(startTime);
			KeyFrameController keyFrameController = EntryPoint.GetKeyFrameController(1507374, 4);
			keyFrameController.SetControllee(mLogoSprite);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, 200);
		}

		public override int GetNumOpeningAnims()
		{
			return base.GetNumOpeningAnims() + 1;
		}

		public override int GetNumClosingAnims()
		{
			return base.GetNumClosingAnims() + 1;
		}

		public override int GetOpeningAnimsDuration()
		{
			return FlMath.Maximum(200, base.GetOpeningAnimsDuration());
		}

		public override int GetClosingAnimsDuration()
		{
			return FlMath.Maximum(200, base.GetClosingAnimsDuration());
		}

		public override bool OnCommand(int command)
		{
			if (!IsReadyForCommands())
			{
				return true;
			}
			bool flag = false;
			switch (command)
			{
			case 35:
				InitializeSoftkeys();
				flag = InitializeMenuPopup(23);
				mMainMenuState = 1;
				break;
			case 36:
				mMainMenuState = 2;
				flag = SetMarathonGame();
				break;
			case 13:
				mMainMenuState = 3;
				flag = ShowTutorialIfNeeded();
				break;
			case 32:
			case 39:
			{
				mMainMenuState = 8;
				GameApp gameApp = GameApp.Get();
				GameSettings gameSettings = gameApp.GetGameSettings();
				if (gameSettings.IsUserDoingTutorial())
				{
					gameSettings.SetTutorialShown();
				}
				mCurrentPopup.Hide();
				gameApp.GetCommandHandler().Execute(9);
				flag = true;
				break;
			}
			case -20:
				flag = InitializeMenuPopup(20);
				mMainMenuState = 4;
				break;
			case -31:
				flag = InitializeMenuPopup(25);
				mMainMenuState = 5;
				break;
			case -32:
				flag = InitializeMenuPopup(55);
				mMainMenuState = 10;
				break;
			case -33:
				flag = InitializeMenuPopup(56);
				mMainMenuState = 11;
				break;
			case -21:
				flag = InitializeMenuPopup(21);
				mMainMenuState = 6;
				break;
			case -22:
				flag = InitializeMenuPopup(26);
				mMainMenuState = 9;
				break;
			case -24:
				flag = InitializeMenuPopup(98);
				mMainMenuState = 12;
				break;
			case -28:
				flag = ((!GameApp.Get().GetIsDemo()) ? InitializeMenuPopup(24) : InitializeMenuPopup(57));
				mMainMenuState = 7;
				break;
			case 4:
				if (mMainMenuState == 5)
				{
					mMainMenuState = 4;
					flag = InitializeMenuPopup(20);
					break;
				}
				mMainMenuState = 0;
				mCurrentPopup.Hide();
				InitializeSoftkeys();
				flag = true;
				ManageSoftkeysVisibility(mCurrentPopup, false);
				break;
			case -16:
				mCurrentPopup.Hide();
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

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			mMainMenuTimedOutCounterMs = 30000;
			bool flag = false;
			if (msg == 1)
			{
				DeletePopup();
				flag = true;
			}
			switch (msg)
			{
			case -118:
			case -117:
			case -116:
				mRotatingMenu.HandlePenMsg(msg, intParam);
				break;
			}
			if (!flag)
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			if (mCurrentPopup != null && mCurrentPopup.IsLoaded() && mCurrentPopup.GetViewport() == null && mHidingPopup == null)
			{
				PreparePopup();
			}
			if (mMainMenuState != 0)
			{
				return;
			}
			mMainMenuTimedOutCounterMs -= deltaTimeMs;
			if (mMainMenuTimedOutCounterMs <= 0)
			{
				mMainMenuTimedOutCounterMs = 30000;
				if (GameApp.Get().GetIsDemo())
				{
					GameApp.Get().GetCommandHandler().Execute(-35);
				}
				else
				{
					GameApp.Get().GetCommandHandler().Execute(74);
				}
				mView.UnRegisterInGlobalTime();
			}
			if (mIsDemo)
			{
				mTimeElapsed += deltaTimeMs;
				if (mTimeElapsed >= mCheckIsTrialInterval)
				{
					CheckDemoState();
					mTimeElapsed = 0;
				}
			}
		}

		public void CheckDemoState()
		{
			if (!LiveState.IsTrial)
			{
				GameApp.Get().SetIsDemo(false);
				GameApp.Get().GetCommandHandler().Execute(-17);
			}
		}

		public override bool SaveFiles(int ctx)
		{
			switch (ctx)
			{
			case 0:
				return GameApp.Get().GetFileManager().OnSave();
			case 1:
				return GameApp.Get().GetFileManager().OnSave();
			default:
				return base.SaveFiles(ctx);
			}
		}

		public override void SerializeObjects()
		{
			GameApp.Get().GetFileManager().WriteObject(3);
			GameApp.Get().GetFileManager().WriteObject(2);
			GameApp.Get().GetFileManager().WriteObject(4);
			GameApp.Get().GetFileManager().WriteObject(0);
		}

		public override void DisableSelectionOnInitialize()
		{
			base.DisableSelectionOnInitialize();
		}

		public virtual bool SetMarathonGame()
		{
			bool flag = false;
			GameApp gameApp = GameApp.Get();
			GameSettings gameSettings = gameApp.GetGameSettings();
			SetupLevelSelectMenuPopup setupLevelSelectMenuPopup = (SetupLevelSelectMenuPopup)mCurrentPopup;
			int selectedLevel = setupLevelSelectMenuPopup.GetSelectedLevel();
			gameSettings.SetGameMode(0);
			gameSettings.SetGameVariant(0);
			gameSettings.SetPlayMode(4);
			gameSettings.SetGameDifficulty(selectedLevel);
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			progressionExpert.SetLastMarathonDifficultyPlayed(selectedLevel);
			return OnCommand(13);
		}

		public virtual bool ShowTutorialIfNeeded()
		{
			bool result = true;
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			if (gameSettings.NeedToShowTutorial())
			{
				InitializeMenuPopup(22);
				gameSettings.SetUserDoingTutorial(true);
			}
			else
			{
				result = OnCommand(39);
			}
			return result;
		}

		public virtual void InitializeSoftkeys()
		{
			mSelectSoftKey.SetFunction(0, -10);
			mClearSoftKey.SetFunction(2, -28);
		}

		public virtual bool InitializeMenuPopup(sbyte mainMenuPopupId, sbyte popupAnimType)
		{
			bool result = true;
			mFocusedSelectionIndex = mSelector.GetSingleSelection();
			if (mCurrentPopup != null)
			{
				mHidingPopup = mCurrentPopup;
				mHidingPopup.Hide();
				mCurrentPopup = null;
			}
			mCurrentPopup = Popup.CreateMainMenuPopup(mainMenuPopupId, this, mSelectSoftKey, mClearSoftKey);
			mCurrentPopup.SetAnimationType(popupAnimType);
			mCurrentPopup.Load();
			mView.RegisterInGlobalTime();
			return result;
		}

		public virtual void PreparePopup()
		{
			mCurrentPopup.GetEntryPoints();
			mCurrentPopup.Initialize();
			mCurrentPopup.AttachPopupViewport(mViewport);
			if (mMainMenuState == 1)
			{
				ProgressionExpert progressionExpert = ProgressionExpert.Get();
				SetupLevelSelectMenuPopup setupLevelSelectMenuPopup = (SetupLevelSelectMenuPopup)mCurrentPopup;
				setupLevelSelectMenuPopup.SetSelectedLevel(progressionExpert.GetLastMarathonDifficultyPlayed());
			}
			ManageSoftkeysVisibility(mCurrentPopup, true);
			mCurrentPopup.Show();
		}

		public virtual void DeletePopup()
		{
			if (mHidingPopup == null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			else
			{
				mHidingPopup.Unload();
				mHidingPopup = null;
			}
		}

		public virtual bool InitializeMenuPopup(sbyte mainMenuPopupId)
		{
			return InitializeMenuPopup(mainMenuPopupId, 0);
		}
	}
}
