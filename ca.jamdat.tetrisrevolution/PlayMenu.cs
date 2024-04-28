using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PlayMenu : SelectorMenu
	{
		public const short helixAnimationIntervalTime = 175;

		public const short helixAnimationEndTime = 400;

		public const short levelAnimationIntervalTime = 25;

		public const short levelAnimationEndTime = 150;

		public const int stateRotateHelix = 0;

		public const int stateSelectVariant = 1;

		public const int stateShowLockedVariant = 2;

		public const int stateShowUnlockedVariant = 3;

		public const int stateSelectLevel = 4;

		public const int stateSetLevel = 5;

		public const int stateShowTutorial = 6;

		public const int stateHidePopup = 7;

		public const int stateLeaving = 8;

		public const int playMenuStateCount = 9;

		public const int disableValue = -1;

		public const int initializeValue = 0;

		public Popup mCurrentPopup;

		public Popup mHidingPopup;

		public VerticalText mLevelVerticalText;

		public Viewport mLevelVerticalTextViewport;

		public Helix mHelix;

		public IndexedSprite[] mHelixSprites;

		public PlayMenuHelixObserver mHelixObserver;

		public Viewport mBlopViewport;

		public int mPlayMenuState;

		public RotatingMenu mRotatingMenu;

		public VerticalCompletionViewport mVerticalCompletionViewport;

		public FlRect mSetupVariantPopupRect;

		public IndexedSprite mIconSprite;

		public PlayMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			mPlayMenuState = 0;
			mSetupVariantPopupRect = new FlRect();
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mHelix = new Helix(1);
			mHelix.Load();
		}

		public override bool IsLoaded()
		{
			if (base.IsLoaded())
			{
				return mHelix.IsLoaded();
			}
			return false;
		}

		public override void Initialize()
		{
			base.Initialize();
			ReturnToStartingState();
			GameApp gameApp = GameApp.Get();
			GameSettings gameSettings = gameApp.GetGameSettings();
			gameSettings.SetUserDoingTutorial(false);
			gameSettings.SetGameMode(1);
			gameSettings.SetGameDifficulty(0);
			int lastVariantPlayed = gameSettings.GetLastVariantPlayed();
			gameSettings.SetGameVariant(lastVariantPlayed);
			mFocusedSelectionIndex = lastVariantPlayed;
			mRotatingMenu = new RotatingMenu(false);
			VerticalSelector.Initialize(mSelector, 0, mFocusedSelectionIndex);
			mRotatingMenu.Initialize(mSelector);
			mSelector.GetNextArrow().GetViewport().SetVisible(false);
			gameSettings.SetPlayMode(4);
			FeatsExpert featsExpert = FeatsExpert.Get();
			for (int i = 0; i < mSelector.GetNumSelections(); i++)
			{
				if (featsExpert.IsGameVariantUnlocked(i))
				{
					Selection selectionAt = mSelector.GetSelectionAt(i);
					selectionAt.SetCommand(34);
				}
				else if (GameApp.Get().GetIsDemo())
				{
					Selection selectionAt2 = mSelector.GetSelectionAt(i);
					selectionAt2.SetCommand(-36);
				}
			}
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(mPackageId);
			mLevelVerticalText = new VerticalText(0, 0, EntryPoint.GetFlFont(3047517, 7), false);
			mLevelVerticalText.SetCaption(EntryPoint.GetFlString(preLoadedPackage, 3));
			mLevelVerticalText.Initialize(mViewport);
			int num = 470 - mLevelVerticalText.GetRectWidth();
			int num2 = 29 + (mSelector.GetRectHeight() - mLevelVerticalText.GetRectHeight()) / 2;
			mLevelVerticalText.SetX(-mLevelVerticalText.GetRectWidth());
			mLevelVerticalText.SetY(0);
			mLevelVerticalTextViewport = new Viewport(mViewport);
			mLevelVerticalTextViewport.SetClipChildren(true);
			mLevelVerticalTextViewport.SetTopLeft((short)num, (short)num2);
			mLevelVerticalTextViewport.SetSize(mLevelVerticalText.GetRectWidth(), mLevelVerticalText.GetRectHeight());
			mLevelVerticalText.SetViewport(mLevelVerticalTextViewport);
			mHelix.Initialize(mViewport, mSelector);
			mHelix.SetTopLeft(0, 800);
			mViewport.PutComponentBehind(mSelector, mHelix);
			Package preLoadedPackage2 = GameLibrary.GetPreLoadedPackage(1900602);
			mBlopViewport = EntryPoint.GetViewport(preLoadedPackage, 6);
			mBlopViewport.SetVisible(false);
			mBlopViewport.SetTopLeft(mBlopViewport.GetRectLeft(), mBlopViewport.GetRectTop());
			mBlopViewport.SetViewport(mHelix);
			mHelixObserver = new PlayMenuHelixObserver(this);
			mHelixObserver.Initialize(mBlopViewport, preLoadedPackage, 7);
			mHelix.SetObserver(mHelixObserver);
			mHelixSprites = new IndexedSprite[12];
			int num3 = 4;
			for (int j = 0; j < 12; j++)
			{
				mHelixSprites[j] = EntryPoint.GetIndexedSprite(preLoadedPackage2, num3++);
			}
			mVerticalCompletionViewport = new VerticalCompletionViewport(mViewport);
			gameApp.GetTouchMenuReceiver().Initialize(this);
			mIconSprite = EntryPoint.GetIndexedSprite(preLoadedPackage, 5);
			mIconSprite.SetVisible(false);
			mIconSprite.SetTopLeft((short)(mBlopViewport.GetRectLeft() + mBlopViewport.GetRectWidth() / 2 + 55), (short)(mBlopViewport.GetRectTop() + mBlopViewport.GetRectHeight() / 2 + 125));
			mIconSprite.BringToFront();
		}

		public override void Unload()
		{
			mHelixSprites = null;
			if (mRotatingMenu != null)
			{
				mRotatingMenu.Unload();
				mRotatingMenu = null;
			}
			if (mVerticalCompletionViewport != null)
			{
				mVerticalCompletionViewport.Unload();
				mVerticalCompletionViewport = null;
			}
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
			if (mLevelVerticalText != null)
			{
				mLevelVerticalText.Unload();
				mLevelVerticalText.SetViewport(null);
				mLevelVerticalText = null;
			}
			if (mBlopViewport != null)
			{
				mBlopViewport.SetViewport(null);
				mBlopViewport = null;
			}
			if (mHelix != null)
			{
				mHelix.Unload();
				mHelix = null;
			}
			if (mHelixObserver != null)
			{
				mHelixObserver.Unload();
				mHelixObserver = null;
			}
			if (mVerticalCompletionViewport != null)
			{
				mVerticalCompletionViewport.Unload();
				mVerticalCompletionViewport = null;
			}
			if (mLevelVerticalTextViewport != null)
			{
				mLevelVerticalTextViewport.SetViewport(null);
				mLevelVerticalTextViewport = null;
			}
			base.Unload();
		}

		public override void Suspend()
		{
			if (mRotatingMenu != null)
			{
				EnableSelector(false);
				mRotatingMenu.UnRegisterInGlobalTime();
			}
			base.Suspend();
		}

		public override void Resume()
		{
			if (mRotatingMenu != null)
			{
				Component currentFocus = GameApp.Get().GetCurrentFocus();
				EnableSelector(true);
				mRotatingMenu.RegisterInGlobalTime();
				GameApp.Get().SetCurrentFocus(currentFocus);
			}
			base.Resume();
		}

		public override bool OnCommand(int command)
		{
			if (!IsReadyForCommands())
			{
				return true;
			}
			bool flag = false;
			if (mPlayMenuState != 0 && mPlayMenuState != 8)
			{
				switch (command)
				{
				case -10:
				{
					int singleSelection = mSelector.GetSingleSelection();
					Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
					flag = OnCommand(selectionAt.GetCommand());
					break;
				}
				case 33:
					mPlayMenuState = 2;
					flag = LoadLockedVariant();
					break;
				case 34:
					flag = LoadUnlockedVariant();
					mPlayMenuState = 3;
					break;
				case 35:
					mPlayMenuState = 4;
					flag = LoadSelectLevelOrStartMasterVariant();
					break;
				case 36:
					mPlayMenuState = 5;
					flag = SetGameSettings();
					break;
				case 38:
					mPlayMenuState = 6;
					flag = ShowTutorialIfNeeded();
					break;
				case 32:
				case 39:
					flag = LeaveMenuAndStartGame();
					mPlayMenuState = 8;
					break;
				case 4:
					mPlayMenuState = 7;
					flag = HidePopupAndReturnToPlayMenu();
					mPlayMenuState = 1;
					break;
				}
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (msg == 1)
			{
				DeletePopup();
				flag = true;
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
			if (mHidingPopup == null && mCurrentPopup != null && mCurrentPopup.IsLoaded())
			{
				mView.UnRegisterInGlobalTime();
				InitializePopup();
			}
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			bool result = false;
			if (IsReadyForCommands())
			{
				result = mRotatingMenu.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition) || base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition);
			}
			return result;
		}

		public override void CreateTouchZones()
		{
			TouchMenuReceiver touchMenuReceiver = GameApp.Get().GetTouchMenuReceiver();
			touchMenuReceiver.CreateZone(2, 0, 0, 480, 800, 0, 2);
			touchMenuReceiver.CreateZone(1, 0, 0, 480, 800, 0, 1);
		}

		public override void CreateOpeningAnims()
		{
			int num = 0;
			num += 333;
			num += 400;
			base.CreateOpeningAnims();
			KeyFrameController[] array = mRotatingMenu.CreateOpeningAnims();
			KeyFrameController timeable = mRotatingMenu.CreateSelectedDifficultyTextAnimation(false);
			int numVisibleSelections = mRotatingMenu.GetNumVisibleSelections();
			for (int i = 0; i < numVisibleSelections; i++)
			{
				mAnimationTimerSequence.RegisterInterval(array[i], num, num + 200);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections], num, num + 200);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections * 2], num, num + 200);
			}
			mAnimationTimerSequence.RegisterInterval(timeable, num, num + 200);
			mVerticalCompletionViewport.CreateOpeningAnims(333, mAnimationTimerSequence);
			KeyFrameController keyFrameController = null;
			KeyFrameSequence keyFrameSequence = null;
			int[] array2 = new int[4];
			num -= 400;
			keyFrameController = new KeyFrameController();
			keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameController.SetControllee(mHelix);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, num, num + 400);
			int rectWidth = mHelix.GetRectWidth();
			int rectHeight = mHelix.GetRectHeight();
			int num2 = 800 - mSelector.GetRectHeight() - mHelix.GetRectHeight() - 66 - 58;
			int num3 = ((num2 <= 0) ? (800 - rectHeight) : (mSelector.GetRectHeight() + 29 + num2 / 2));
			array2[0] = (480 - rectWidth - mVerticalCompletionViewport.GetRectWidth()) / 2;
			array2[1] = 800;
			array2[2] = rectWidth;
			array2[3] = rectHeight;
			keyFrameSequence.SetKeyFrame(0, 0, array2);
			array2[1] = num3 + rectHeight / 4;
			keyFrameSequence.SetKeyFrame(1, 175, array2);
			array2[1] = num3;
			keyFrameSequence.SetKeyFrame(2, 400, array2);
			keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mLevelVerticalText);
			keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int duration = num + 400 + 150 + 200;
			mAnimationTimerSequence.RegisterInterval(keyFrameController, num + 400 + 200, duration);
			array2[0] = -mLevelVerticalText.GetRectWidth();
			array2[1] = 0;
			array2[2] = mLevelVerticalText.GetRectWidth();
			array2[3] = mLevelVerticalText.GetRectHeight();
			keyFrameSequence.SetKeyFrame(0, 0, array2);
			array2[0] = -(mLevelVerticalText.GetRectWidth() / 4);
			keyFrameSequence.SetKeyFrame(1, 25, array2);
			array2[0] = 0;
			keyFrameSequence.SetKeyFrame(2, 150, array2);
		}

		public override void CreateClosingAnims(int startTime)
		{
			startTime += 150;
			startTime += 400;
			base.CreateClosingAnims(startTime);
			mVerticalCompletionViewport.CreateClosingAnims(0, mAnimationTimerSequence);
			KeyFrameController[] array = mRotatingMenu.CreateClosingAnims();
			KeyFrameController timeable = mRotatingMenu.CreateSelectedDifficultyTextAnimation(true);
			int numVisibleSelections = mRotatingMenu.GetNumVisibleSelections();
			for (int i = 0; i < numVisibleSelections; i++)
			{
				mAnimationTimerSequence.RegisterInterval(array[i], 150, 550);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections], 150, 550);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections * 2], 150, 550);
			}
			mAnimationTimerSequence.RegisterInterval(timeable, 150, 350);
			KeyFrameController keyFrameController = null;
			KeyFrameSequence keyFrameSequence = null;
			int[] array2 = new int[4];
			keyFrameController = new KeyFrameController();
			keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameController.SetControllee(mHelix);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, 400);
			int rectWidth = mHelix.GetRectWidth();
			int rectHeight = mHelix.GetRectHeight();
			int num = 800 - mSelector.GetRectHeight() - mHelix.GetRectHeight() - 66 - 58;
			int num2 = ((num <= 0) ? (800 - rectHeight) : (mSelector.GetRectHeight() + 29 + num / 2));
			array2[0] = (480 - rectWidth - mVerticalCompletionViewport.GetRectWidth()) / 2;
			array2[1] = num2;
			array2[2] = rectWidth;
			array2[3] = rectHeight;
			keyFrameSequence.SetKeyFrame(0, 0, array2);
			array2[1] = 800 - rectHeight / 4;
			keyFrameSequence.SetKeyFrame(1, 175, array2);
			array2[1] = 800;
			keyFrameSequence.SetKeyFrame(2, 400, array2);
			keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mLevelVerticalText);
			keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, 150);
			array2[0] = 0;
			array2[1] = 0;
			array2[2] = mLevelVerticalText.GetRectWidth();
			array2[3] = mLevelVerticalText.GetRectHeight();
			keyFrameSequence.SetKeyFrame(0, 0, array2);
			array2[0] = -(mLevelVerticalText.GetRectWidth() / 4);
			keyFrameSequence.SetKeyFrame(1, 25, array2);
			array2[0] = -mLevelVerticalText.GetRectWidth();
			keyFrameSequence.SetKeyFrame(2, 150, array2);
		}

		public override void StartOpeningAnims()
		{
			base.StartOpeningAnims();
			mVerticalCompletionViewport.RegisterInGlobalTime();
		}

		public override void OnOpeningAnimsEnded()
		{
			base.OnOpeningAnimsEnded();
			mRotatingMenu.OnOpeningAnimEnded();
			mVerticalCompletionViewport.AdjustRect();
			mVerticalCompletionViewport.UnRegisterInGlobalTime();
			mSelector.GetNextArrow().GetViewport().SetVisible(false);
		}

		public override void StartClosingAnims()
		{
			base.StartClosingAnims();
			mVerticalCompletionViewport.UnRegisterInGlobalTime();
			mRotatingMenu.StartClosingAnims();
			mIconSprite.SetVisible(false);
		}

		public override bool IsOpeningAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !GameApp.Get().GetAnimator().IsPlaying(2);
			}
			return false;
		}

		public override bool IsClosingAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !GameApp.Get().GetAnimator().IsPlaying(2);
			}
			return false;
		}

		public override int GetNumOpeningAnims()
		{
			int num = 2;
			num += mRotatingMenu.GetNumVisibleSelections() * 3 + 3 + 1;
			num += 7;
			return base.GetNumOpeningAnims() + num;
		}

		public override int GetNumClosingAnims()
		{
			int num = 2;
			num += mRotatingMenu.GetNumVisibleSelections() * 3 + 3 + 1;
			num += 7;
			return base.GetNumClosingAnims() + num;
		}

		public override int GetOpeningAnimsDuration()
		{
			int num = 0;
			num += 333;
			num += 1013;
			return FlMath.Maximum(num, base.GetOpeningAnimsDuration());
		}

		public override int GetClosingAnimsDuration()
		{
			int num = 150;
			num += 400;
			num += 333;
			return FlMath.Maximum(num, base.GetClosingAnimsDuration());
		}

		public override void ReceiveFocus()
		{
			mFocusedSelectionIndex = mSelector.GetSingleSelection();
			if (mHidingPopup != null)
			{
				GameApp.Get().TakeFocus();
			}
			else if (mPlayMenuState == 2 || mPlayMenuState == 3 || mPlayMenuState == 6 || mPlayMenuState == 4)
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

		public virtual void OnHelixOnTarget()
		{
			mPlayMenuState = 1;
			mSelectSoftKey.SetFunction(7, -10);
			FeatsExpert featsExpert = FeatsExpert.Get();
			mIconSprite.SetVisible(featsExpert.IsGameVariantUnlocked(mSelector.GetSingleSelection()));
			mIconSprite.SetCurrentFrame(Utilities.GetVariantIconFrameIndex(mSelector.GetSingleSelection()));
		}

		public virtual void OnHelixLeaveTarget()
		{
			mPlayMenuState = 0;
			mSelectSoftKey.SetFunction(7, 0);
			mIconSprite.SetVisible(false);
		}

		public override bool SaveFiles(int ctx)
		{
			if (ctx == 0)
			{
				return GameApp.Get().GetFileManager().OnSave();
			}
			return base.SaveFiles(ctx);
		}

		public override void SerializeObjects()
		{
			GameApp.Get().GetFileManager().WriteObject(3);
			GameApp.Get().GetFileManager().WriteObject(2);
			GameApp.Get().GetFileManager().WriteObject(4);
		}

		public override void EnableSelector(bool enable)
		{
			mRotatingMenu.EnableRotatingSelector(enable);
		}

		public virtual bool HidePopupAndReturnToPlayMenu()
		{
			mCurrentPopup.Hide();
			mClearSoftKey.SetFunction(1, -12);
			mSelectSoftKey.SetFunction(7, -10);
			ManageSoftkeysVisibility(mCurrentPopup, false);
			return true;
		}

		public virtual bool LoadLockedVariant()
		{
			return CreateAndLoadPopup(10, 1);
		}

		public virtual bool LoadUnlockedVariant()
		{
			return CreateAndLoadPopup(11, 4);
		}

		public virtual bool LoadSelectLevelOrStartMasterVariant()
		{
			bool flag = false;
			int singleSelection = mSelector.GetSingleSelection();
			if (singleSelection == 11)
			{
				return OnCommand(36);
			}
			return CreateAndLoadPopup(13, 0);
		}

		public virtual bool SetGameSettings()
		{
			bool flag = false;
			int singleSelection = mSelector.GetSingleSelection();
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			gameSettings.SetGameVariant(singleSelection);
			if (singleSelection == 11)
			{
				gameSettings.SetGameDifficulty(14);
			}
			else
			{
				gameSettings.SetGameDifficulty(((SetupLevelSelectMenuPopup)mCurrentPopup).GetSelectedLevel());
			}
			if (singleSelection == 11)
			{
				return OnCommand(39);
			}
			return OnCommand(38);
		}

		public virtual bool ShowTutorialIfNeeded()
		{
			bool result = true;
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			if (gameSettings.NeedToShowTutorial())
			{
				CreateAndLoadPopup(12, 0);
				gameSettings.SetUserDoingTutorial(true);
			}
			else
			{
				result = OnCommand(39);
			}
			return result;
		}

		public virtual bool LeaveMenuAndStartGame()
		{
			bool flag = false;
			int singleSelection = mSelector.GetSingleSelection();
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			mCurrentPopup.Hide();
			if (gameSettings.IsUserDoingTutorial())
			{
				gameSettings.SetTutorialShown();
			}
			gameSettings.SetLastVariantPlayed(singleSelection);
			return base.OnCommand(9);
		}

		public virtual bool CreateAndLoadPopup(sbyte playMenuPopupId, sbyte popupAnimType, bool hasUserHitBackSoftkey)
		{
			bool result = true;
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Hide();
				mHidingPopup = mCurrentPopup;
			}
			int singleSelection = mSelector.GetSingleSelection();
			mCurrentPopup = Popup.CreatePlayMenuPopup(playMenuPopupId, this, singleSelection, mSelectSoftKey, mClearSoftKey);
			mCurrentPopup.SetAnimationType(popupAnimType);
			mCurrentPopup.SetHelixIndexedSprite(mHelixSprites[mSelector.GetSingleSelection()]);
			mCurrentPopup.Load();
			mView.RegisterInGlobalTime();
			return result;
		}

		public virtual void InitializePopup()
		{
			mCurrentPopup.GetEntryPoints();
			mCurrentPopup.Initialize();
			mCurrentPopup.AttachPopupViewport(mViewport);
			if (mPlayMenuState == 4)
			{
				mFocusedSelectionIndex = mSelector.GetSingleSelection();
				bool flag = mSetupVariantPopupRect.GetHeight() > 0;
				if (flag)
				{
					mCurrentPopup.SetVisualRect(mSetupVariantPopupRect);
				}
				mCurrentPopup.SetAutoResize(!flag);
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

		public virtual void ExecuteSelectedCommand()
		{
			Selection selectionAt = mSelector.GetSelectionAt(mSelector.GetSingleSelection());
			OnCommand(selectionAt.GetCommand());
		}

		public virtual void ReturnToStartingState()
		{
			mPlayMenuState = 0;
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, -12);
			ManageSoftkeysVisibility(mCurrentPopup, false);
		}

		public virtual bool CreateAndLoadPopup(sbyte playMenuPopupId, sbyte popupAnimType)
		{
			return CreateAndLoadPopup(playMenuPopupId, popupAnimType, false);
		}
	}
}
