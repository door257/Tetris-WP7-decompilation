using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Popup : Viewport
	{
		public const sbyte kUp = -1;

		public const sbyte kNone = 0;

		public const sbyte kDown = 1;

		public const int invalidState = 0;

		public const int showingPopup = 1;

		public const int popupScrolling = 2;

		public const int popupShown = 3;

		public const int hidingPopup = 4;

		public const sbyte kPopupFeats = 0;

		public const sbyte kPopupCareerFeat = 1;

		public const sbyte kPopupAdvancedFeat = 2;

		public const sbyte kPopupStatistics = 3;

		public const sbyte kPopupBestScores = 4;

		public const sbyte kPopupVariants = 5;

		public const sbyte kPopupVariantUnlocked = 6;

		public const sbyte kPopupLockedGameVariant = 10;

		public const sbyte kPopupSetupGameVariant = 11;

		public const sbyte kPopupTutorialPlay = 12;

		public const sbyte kPopupSetupLevelSelectPlayMenu = 13;

		public const sbyte kPopupOptionsMenu = 20;

		public const sbyte kPopupHelpMenu = 21;

		public const sbyte kPopupTutorialMain = 22;

		public const sbyte kPopupSetupLevelSelectMarathon = 23;

		public const sbyte kPopupExitAppConfirmation = 24;

		public const sbyte kPopupResetGameConfirmation = 25;

		public const sbyte kPopupLeaderboardMenu = 26;

		public const sbyte kPopupLeaderboardMessage = 27;

		public const sbyte kPopupAchievementsMenu = 98;

		public const sbyte kPopupAchievementsMessage = 99;

		public const sbyte kPopupNone = 30;

		public const sbyte kPopupQuickHint = 31;

		public const sbyte kPopupLongHint = 32;

		public const sbyte kPopupPause = 33;

		public const sbyte kPopupOptionsInGame = 34;

		public const sbyte kPopupHelpMenuInGame = 35;

		public const sbyte kPopupReturnToMainMenu = 36;

		public const sbyte kPopupTutorialGame = 37;

		public const sbyte kPopupExitAppConfirmationInGame = 38;

		public const sbyte kPopupSuccess = 39;

		public const sbyte kPopupFailure = 40;

		public const sbyte kPopupGameStats = 41;

		public const sbyte kPopupFeatMaster = 42;

		public const sbyte kPopupFeatProgress = 43;

		public const sbyte kPopupCareerFeatComplete = 44;

		public const sbyte kPopupAdvancedFeatComplete = 45;

		public const sbyte kPopupNewBest = 46;

		public const sbyte kPopupUnlockPopup = 47;

		public const sbyte kPopupRetry = 48;

		public const sbyte kPopupRetryFromPause = 49;

		public const sbyte kPopupGlossary = 50;

		public const sbyte kPopupMasterReplay = 51;

		public const sbyte kPopupDemoPurchaseConfirmation = 55;

		public const sbyte kPopupDemoFeatLockedPurchaseConfirmation = 56;

		public const sbyte kPopupDemoExitConfirmation = 57;

		public const sbyte kPopupDemoTrialOverConfirmation = 58;

		public const sbyte kPopupDemoIdleReplayPurchaseConfirmation = 59;

		public const sbyte kPopupDemoLockedReplayPurchaseConfirmation = 60;

		public const sbyte popupGeneralAnim = 0;

		public const sbyte popupRedAnim = 1;

		public const sbyte popupOnlyOpeningWhiteAnim = 2;

		public const sbyte popupOnlyClosingWhiteAnim = 3;

		public const sbyte popupWhiteAnim = 4;

		public const sbyte popupNoAnim = 5;

		public const sbyte popupAnimTypeCount = 6;

		public BaseScene mBaseScene;

		public Softkey mSelectSoftKey;

		public Softkey mClearSoftKey;

		public Viewport mPopupViewport;

		public Scroller mContentScroller;

		public Scrollbar mScrollbar;

		public Viewport mScrollbarViewport;

		public MetaPackage mContentMetaPackage;

		public int mPopupDuration;

		public bool mAutoResize;

		public bool mDetachDimBackground;

		public sbyte mPopupId;

		public ResizableFrame mResizableFrame;

		public ResizableFrame mResizableGibberish;

		public Shape mControlledShape;

		public TimerSequence mPopupAnimation;

		public sbyte mAnimationType;

		public IndexedSprite mHelixIndexedSprite;

		public int mAutoHideTimerDelay;

		public int mState;

		public short mDistanceToScroll;

		public sbyte mScrollDirection;

		public Popup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
		{
			mBaseScene = baseScene;
			mSelectSoftKey = selectSoftKey;
			mClearSoftKey = clearSoftKey;
			mAutoResize = true;
			mDetachDimBackground = true;
			mAnimationType = 0;
			mState = 0;
			mScrollDirection = 0;
			mPopupId = -1;
		}

		public override void destruct()
		{
		}

		public virtual void Load()
		{
			mScrollbar = new Scrollbar();
			mScrollbar.Load();
			mControlledShape = new Shape();
			GameApp.Get().TakeFocus();
		}

		public virtual bool IsLoaded()
		{
			if (mContentMetaPackage != null && mContentMetaPackage.IsLoaded())
			{
				return mScrollbar.IsLoaded();
			}
			return false;
		}

		public virtual void GetEntryPoints()
		{
		}

		public virtual void Initialize()
		{
		}

		public virtual void Unload()
		{
			GameApp.Get().GetTouchPopupReceiver().Unload();
			if (mDetachDimBackground)
			{
				Sprite sprite = EntryPoint.GetSprite(1310760, 300);
				sprite.SetViewport(null);
			}
			if (mPopupAnimation != null)
			{
				PopupAnimation.CleanTimerSequence(mPopupAnimation);
				AnimationController animator = GameApp.Get().GetAnimator();
				if (animator.IsValid(1))
				{
					animator.UnloadSingleAnimation(1);
				}
				PopupAnimation.DeleteTimerSequence(mPopupAnimation);
			}
			mContentScroller = null;
			mScrollbarViewport = null;
			if (mScrollbar != null)
			{
				mScrollbar.Unload();
				mScrollbar = null;
			}
			if (mControlledShape != null)
			{
				mControlledShape.SetViewport(null);
				mControlledShape = null;
			}
			if (mResizableFrame != null)
			{
				mResizableFrame.Unload();
				mResizableFrame = null;
			}
			if (mResizableGibberish != null)
			{
				mResizableGibberish.Unload();
				mResizableGibberish = null;
			}
			CustomComponentUtilities.Detach(this);
			if (mPopupViewport != null)
			{
				mPopupViewport.SetViewport(null);
				mPopupViewport = null;
			}
			FlPenManager.Get().Reset();
			if (mContentMetaPackage != null)
			{
				GameLibrary.ReleasePackage(mContentMetaPackage);
				mContentMetaPackage = null;
			}
			UnRegisterInGlobalTime();
		}

		public virtual void AttachPopupViewport(Viewport viewport)
		{
			Sprite sprite = EntryPoint.GetSprite(1310760, 300);
			if (sprite.GetViewport() == null)
			{
				sprite.SetViewport(viewport);
				mBaseScene.AttachComponentBehindSoftkeyViewport(sprite);
			}
			mPopupViewport.SetViewport(viewport);
			CustomComponentUtilities.Attach(this, mPopupViewport);
			mResizableFrame = ResizableFrame.Create(mPopupViewport);
			mControlledShape.SetViewport(viewport);
			mPopupAnimation = PopupAnimation.CreateTimerSequence(mControlledShape, mResizableFrame);
		}

		public virtual void Show()
		{
			if (mContentScroller != null)
			{
				if (mAutoResize)
				{
					mResizableFrame.Resize(mContentScroller);
				}
				mScrollbar.Initialize(mContentScroller, mScrollbarViewport);
			}
			AddGibberishToTitle();
			mPopupViewport.BringToFront();
			mAutoHideTimerDelay = mPopupDuration;
			if (mAnimationType != 3 && mAnimationType != 5)
			{
				InitializeAndRegisterTimerSequence();
				GameApp.Get().GetAnimator().StartMenuAnimation(1, 1);
				RegisterInGlobalTime();
				SetPopupState(1);
			}
			else
			{
				OnShowPopup();
			}
		}

		public virtual void Hide()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.GetTouchPopupReceiver().Unload();
			gameApp.TakeFocus();
			if (mAnimationType == 3)
			{
				InitializeAndRegisterTimerSequence();
			}
			if (mAnimationType != 2 && mAnimationType != 5)
			{
				gameApp.GetAnimator().StartMenuAnimation(1, -1);
				RegisterInGlobalTime();
				SetPopupState(4);
			}
			else
			{
				SetPopupState(4);
				OnHidePopup();
			}
		}

		public virtual void SetSoftKeys()
		{
		}

		public virtual bool HasFocus()
		{
			if (mContentScroller == null)
			{
				return false;
			}
			return mContentScroller.DescendentOrSelfHasFocus();
		}

		public override void TakeFocus()
		{
			mPopupViewport.TakeFocus();
		}

		public virtual bool OnCommand(int command)
		{
			bool result = false;
			if (command == 4)
			{
				Hide();
				result = true;
			}
			return result;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (mScrollbar != null && mScrollbarViewport != null)
			{
				mScrollbar.OnMsg(source, msg, intParam);
			}
			if (mContentScroller == null || !mContentScroller.OnDefaultMsg(source, msg, intParam))
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		protected virtual short CalcScrollModifier()
		{
			short result = 15;
			Component[] mElements = mContentScroller.mElements;
			if (mElements[0] != null && mElements[0] is Selection)
			{
				result = mElements[0].GetRectHeight();
			}
			return result;
		}

		protected virtual bool IsDistanceEnoughToScroll()
		{
			short num = CalcScrollModifier();
			return FlMath.Absolute(mDistanceToScroll) > num;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (GetPopupState() == 2 && IsDistanceEnoughToScroll())
			{
				if (mScrollDirection == 1)
				{
					mContentScroller.OnDefaultMsg(mContentScroller, -119, 2);
					mDistanceToScroll += CalcScrollModifier();
					if (mDistanceToScroll >= 0)
					{
						mScrollDirection = 0;
						SetPopupState(3);
					}
				}
				else if (mScrollDirection == -1)
				{
					mContentScroller.OnDefaultMsg(mContentScroller, -119, 1);
					mDistanceToScroll -= CalcScrollModifier();
					if (mDistanceToScroll <= 0)
					{
						mScrollDirection = 0;
						SetPopupState(3);
					}
				}
			}
			else if (GetPopupState() == 3)
			{
				if (mAutoHideTimerDelay > 0)
				{
					mAutoHideTimerDelay -= deltaTimeMs;
					if (mAutoHideTimerDelay <= 0)
					{
						Hide();
					}
				}
			}
			else if (GetPopupState() == 1)
			{
				AnimationController animator = GameApp.Get().GetAnimator();
				if (animator.IsOver(1))
				{
					OnShowPopup();
				}
			}
			else if (GetPopupState() == 4)
			{
				AnimationController animator2 = GameApp.Get().GetAnimator();
				if (animator2.IsOver(1))
				{
					OnHidePopup();
				}
			}
		}

		public virtual FlRect GetVisualRect()
		{
			short rectLeft = GetRectLeft();
			short top = (short)(GetRectTop() + mResizableFrame.GetRectTop());
			short rectWidth = GetRectWidth();
			short rectHeight = mResizableFrame.GetRectHeight();
			return new FlRect(rectLeft, top, rectWidth, rectHeight);
		}

		public virtual void SetVisualRect(FlRect rect)
		{
			SetRect(rect);
			mResizableFrame.SetTopLeft(0, 0);
			mResizableFrame.SetSize(GetSize());
			mPopupViewport.SetSize(GetSize());
			if (mContentScroller != null)
			{
				short height = (short)(rect.GetHeight() - mContentScroller.GetRectTop() - 14 - 6);
				mContentScroller.SetSize(mContentScroller.GetRectWidth(), height);
				mContentScroller.GetScrollerViewport().SetSize(mContentScroller.GetRectWidth(), height);
			}
		}

		public virtual bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool result = true;
			short y = firstPenPosition.GetY();
			short y2 = lastPenPosition.GetY();
			switch (command)
			{
			case 98:
				if (TapActivatesPopup())
				{
					Selection selectionAt = popupSelector.GetSelectionAt(popupSelector.GetSingleSelection());
					selectionAt.SendMsg(selectionAt, -120, 5);
					selectionAt.SendMsg(selectionAt, -121, 5);
				}
				else if (TapHidesPopup())
				{
					GameApp.Get().GetCommandHandler().GetCurrentScene()
						.OnCommand(4);
				}
				result = true;
				break;
			case 90:
			case 94:
				SetPopupState(2);
				mScrollDirection = 1;
				mDistanceToScroll += (short)(y2 - y);
				result = true;
				break;
			case 91:
			case 95:
				SetPopupState(2);
				mScrollDirection = -1;
				mDistanceToScroll += (short)(y2 - y);
				result = true;
				break;
			}
			return result;
		}

		public virtual sbyte GetAnimationType()
		{
			return mAnimationType;
		}

		public virtual void SetAnimationType(sbyte animationType)
		{
			mAnimationType = animationType;
		}

		public virtual void SetHelixIndexedSprite(IndexedSprite helixIndexedSprite)
		{
			mHelixIndexedSprite = helixIndexedSprite;
		}

		public virtual int GetPopupState()
		{
			return mState;
		}

		public virtual void SetDetachDimBackground(bool detachDimBackground)
		{
			mDetachDimBackground = detachDimBackground;
		}

		public virtual bool IsAutoResize()
		{
			return mAutoResize;
		}

		public virtual void SetAutoResize(bool resize)
		{
			mAutoResize = resize;
		}

		public static Popup CreateTrainerMenuPopup(sbyte popupId, BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 50:
				popup = new GlossarySelectMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 51:
				popup = new MasterReplaySelectMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 56:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 56);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public static Popup CreateRealisationsMenuPopup(sbyte popupId, BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 0:
				popup = new RealisationsFeatsPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 1:
				popup = new CareerFeatPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 2:
				popup = new AdvancedFeatPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 3:
				popup = new RealisationsStatsPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 4:
				popup = new RealisationsBestScoresPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 5:
				popup = new RealisationsVariantsPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 6:
				popup = new UnlockPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 56:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 56);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public static Popup CreatePlayMenuPopup(sbyte popupId, BaseScene baseScene, int variant, Softkey selectSoftKey, Softkey clearSoftKey)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 10:
				popup = new LockedGameVariantMenuPopup(baseScene, variant, selectSoftKey, clearSoftKey);
				break;
			case 11:
				popup = new SetupGameVariantMenuPopup(baseScene, variant, selectSoftKey, clearSoftKey);
				break;
			case 13:
				popup = new SetupLevelSelectMenuPopup(0, baseScene, variant, selectSoftKey, clearSoftKey);
				break;
			case 12:
				popup = new TutorialMenu(baseScene, selectSoftKey, clearSoftKey);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public static Popup CreateMainMenuPopup(sbyte popupId, BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 22:
				popup = new TutorialMenu(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 23:
				popup = new SetupLevelSelectMenuPopup(1, baseScene, 0, selectSoftKey, clearSoftKey);
				break;
			case 20:
				popup = new OptionsMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 21:
				popup = new HelpMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 26:
				popup = new LeaderboardMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 98:
				popup = new AchievementsMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 24:
				popup = new ExitAppConfirmationPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 25:
				popup = new ResetGameConfirmationPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 55:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 55);
				break;
			case 56:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 56);
				break;
			case 57:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 57);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public static Popup CreateLiveConnectPopup(sbyte popupId, BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, FlString errorMsg)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 27:
				popup = new LeaderboardMenuMessagePopup(baseScene, selectSoftKey, clearSoftKey, errorMsg);
				break;
			case 99:
				popup = new AchievementsMenuMessagePopup(baseScene, selectSoftKey, clearSoftKey, errorMsg);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public static Popup CreateGamePopup(sbyte popupId, BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, EndGamePopupStateMachine stateMachine)
		{
			Popup popup = null;
			switch (popupId)
			{
			case 33:
				popup = new PauseMenu(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 48:
				popup = new RetryPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 36:
				popup = new ReturnToMainMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 38:
				popup = new ExitAppConfirmationPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 49:
				popup = new RetryFromPausePopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 39:
				popup = new SuccessPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 40:
				popup = new FailurePopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 41:
				popup = new GameStatsPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 34:
				popup = new OptionsMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 35:
				popup = new HelpMenuPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 42:
				FeatsExpert.Get().ShowFeatMasterPopup();
				popup = new FeatMasterPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 43:
			{
				sbyte progressedCareerFeat = stateMachine.GetProgressedCareerFeat();
				popup = new CareerFeatPopup(baseScene, selectSoftKey, clearSoftKey, progressedCareerFeat);
				break;
			}
			case 44:
			{
				sbyte completedCareerFeat = stateMachine.GetCompletedCareerFeat();
				popup = new CareerFeatPopup(baseScene, selectSoftKey, clearSoftKey, completedCareerFeat);
				break;
			}
			case 45:
			{
				sbyte completedAdvancedFeat = stateMachine.GetCompletedAdvancedFeat();
				popup = new AdvancedFeatPopup(baseScene, selectSoftKey, clearSoftKey, completedAdvancedFeat);
				break;
			}
			case 46:
			{
				sbyte newBestType = stateMachine.GetNewBestType();
				popup = new NewBestPopup(baseScene, selectSoftKey, clearSoftKey, newBestType);
				break;
			}
			case 47:
			{
				int unlockedGameVariant = stateMachine.GetUnlockedGameVariant();
				popup = new UnlockPopup(baseScene, selectSoftKey, clearSoftKey, unlockedGameVariant);
				break;
			}
			case 32:
				popup = new LongHintPopup(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 37:
				popup = new TutorialMenu(baseScene, selectSoftKey, clearSoftKey);
				break;
			case 57:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 57);
				break;
			case 58:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 58);
				break;
			case 59:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 59);
				break;
			case 60:
				popup = new DemoConfirmationPopup(baseScene, selectSoftKey, clearSoftKey, 60);
				break;
			}
			if (popup != null)
			{
				popup.SetPopupId(popupId);
			}
			return popup;
		}

		public virtual void OnShowPopup()
		{
			mResizableFrame.SetVisible(true);
			mBaseScene.ReceiveFocus();
			TouchPopupReceiver touchPopupReceiver = GameApp.Get().GetTouchPopupReceiver();
			touchPopupReceiver.Initialize(this);
			if (mContentScroller != null)
			{
				touchPopupReceiver.CreateZone(0, new FlRect(0, 0, 480, 800), 0, 6);
			}
			touchPopupReceiver.RemoveZoneType(0, 1);
			SetPopupState(3);
		}

		public void AddTouchToContinueZone()
		{
			TouchPopupReceiver touchPopupReceiver = GameApp.Get().GetTouchPopupReceiver();
			touchPopupReceiver.CreateZone(1, 0, 0, 480, 800, 0, 1);
		}

		public virtual void OnHidePopup()
		{
			mResizableFrame.SetVisible(false);
			UnRegisterInGlobalTime();
			mBaseScene.ReceiveFocus();
			SetPopupState(0);
			SendMsg(this, 1, 0);
		}

		public virtual void ForceScrollbarUpdate()
		{
			if (mScrollbar != null)
			{
				mScrollbar.OnMsg(mContentScroller, -105, 0);
			}
		}

		public virtual bool TapActivatesPopup()
		{
			return false;
		}

		public virtual bool TapHidesPopup()
		{
			return false;
		}

		public virtual void SetPopupState(int state)
		{
			mState = state;
		}

		public virtual void InitializeAndRegisterTimerSequence()
		{
			switch (mAnimationType)
			{
			case 0:
				PopupAnimation.InitializeTimerSequence(mPopupAnimation, 1, mResizableFrame);
				break;
			case 1:
				PopupAnimation.InitializeTimerSequence(mPopupAnimation, 0, mHelixIndexedSprite, mResizableFrame);
				break;
			case 2:
			case 3:
			case 4:
				PopupAnimation.InitializeTimerSequence(mPopupAnimation, 1, mHelixIndexedSprite, mResizableFrame);
				break;
			}
			GameApp.Get().GetAnimator().ExternalRegisterAnimation(1, mPopupAnimation, 208);
		}

		public virtual void AddGibberishToTitle()
		{
			Package package = mContentMetaPackage.GetPackage();
			Text text = EntryPoint.GetText(package, 0);
			Viewport viewport = EntryPoint.GetViewport(1310760, 202);
			short rectWidth = text.GetRectWidth();
			rectWidth = (short)(rectWidth - 92);
			short lineWidth = text.GetLineWidth();
			if (lineWidth > 0 && lineWidth < rectWidth)
			{
				short rect_left = (short)(text.GetRectLeft() + lineWidth + 18);
				short rect_width = (short)(text.GetRectWidth() - (lineWidth + 18));
				short num = (short)(text.GetRectHeight() / 2);
				short rect_top = (short)(text.GetRectTop() + num / 2);
				viewport.SetRect(rect_left, rect_top, rect_width, num);
				viewport.SetViewport(mPopupViewport);
				mResizableGibberish = ResizableFrame.Create(viewport);
			}
		}

		public virtual bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public sbyte GetPopupId()
		{
			return mPopupId;
		}

		public void SetPopupId(sbyte id)
		{
			mPopupId = id;
		}
	}
}
