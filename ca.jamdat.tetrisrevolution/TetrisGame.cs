using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public abstract class TetrisGame
	{
		public const int kVariantUndefined = -1;

		public const int kVariantBasic = 0;

		public const int kVariantTreadmill = 1;

		public const int kVariantVanilla = 2;

		public const int kVariantFlood = 3;

		public const int kVariantLedges = 4;

		public const int kVariantLimbo = 5;

		public const int kVariantMagnetic = 6;

		public const int kVariantScanner = 7;

		public const int kVariantSplit = 8;

		public const int kVariantChill = 9;

		public const int kVariantFlashlite = 10;

		public const int kVariantMaster = 11;

		public const int kVariantCount = 12;

		public const int kMovePieceLeft = 0;

		public const int kMovePieceRight = 1;

		public const int kRotatePieceClockWise = 2;

		public const int kRotatePieceCounterClockWise = 3;

		public const int kNoAction = 4;

		public const int kGameNotOver = 0;

		public const int kGameOverLockOut = 1;

		public const int kGameOverTopOut = 2;

		public const int kGameOverBlockOut = 3;

		public const int kGameOverNoLinesLeft = 4;

		public const int kGameOverNoTetriminoLeft = 5;

		public const int kGameOverNoTimeLeft = 6;

		public const int kGameOverModeOver = 7;

		public const int kGameOverTrialOver = 8;

		public const int kNone = -1;

		public const int kTSpinMini = 0;

		public const int kTSpin = 1;

		public const int kTSpinMiniSingle = 2;

		public const int kTSpinSingle = 3;

		public const int kTSpinDouble = 4;

		public const int kTSpinTriple = 5;

		public const int kTetris = 6;

		public const int kTSpinMiniSingleBackToBack = 7;

		public const int kTSpinSingleBackToBack = 8;

		public const int kTSpinDoubleBackToBack = 9;

		public const int kTSpinTripleBackToBack = 10;

		public const int kTetrisBackToBack = 11;

		public const int kBravo = 12;

		public const int kSlowTPM = 13;

		public const int kFastTPM = 14;

		public const int kHalfway = 15;

		public const int kNewHighScore = 16;

		public const int kTetrisGearSpeedUp = 17;

		public const int kLevelUp = 18;

		public const int kSpecialGameEventCount = 19;

		public const int kNoDirection = 0;

		public const int kEast = 1;

		public const int kSouth = 2;

		public const int kWest = 3;

		public const int worstOrEqual = -1;

		public const int betterOrEqual = 1;

		public StateApplyingSpecifics mStateApplyingSpecifics;

		public StateClearingLines mStateClearingLines;

		public StateCollapsingLines mStateCollapsingLines;

		public StateCountdown mStateCountdown;

		public StateEndingTurn mStateEndingTurn;

		public StateFalling mStateFalling;

		public StateGameOver mStateGameOver;

		public StateIntroduction mStateIntroduction;

		public StateLocking mStateLocking;

		public StateWaitingForFall mStateWaitingForFall;

		public StateWaitingForInitialize mStateWaitingForInitialize;

		public GameParameter mGameParameter;

		public int mSpeed;

		public int mGoalCountDown;

		public int mPieceFallRate;

		public int mGravityFallRate;

		public int mSoftDropFallRate;

		public int mClearedLineCount;

		public int mMinoCount;

		public bool mBackToBackPossible;

		public int mPlayTimeMs;

		public bool mGameTimeExpired;

		public bool mCanHold;

		public bool mHardDropping;

		public int mLinesToAddCount;

		public GameState mCurrentGameState;

		public int mGameOverType;

		public Well mWell;

		public GameController mGameController;

		public LayerComponent mLayerComponent;

		public Tetrimino mFallingTetrimino;

		public Tetrimino mGhostTetrimino;

		public Tetrimino mHeldTetrimino;

		public TetriminoQueue mTetriminoQueue;

		public int mNumberOfFloatingSpecialMinos;

		public int mPlacementMatrixHeight;

		public int mPlacementMatrixRowOffset;

		public bool[][] mPlacementMatrix;

		public int mNextMoveTimeMs;

		public int mLockDownDelayMs;

		public int mExtendedLockDownRemainingMoves;

		public bool mLockedOut;

		public bool mHasWon;

		public int mLastFeltRow;

		public bool mIsThereTSpin;

		public bool mIsThereBackToBack;

		public MetaPackage mMetaPackage;

		public int mPackageId;

		public AnimationManager mAnimationManager;

		public AnimationController mAnimator;

		public bool mIsGravityNeeded;

		public int mSpecialGameEvent;

		public GameScore mGameScore;

		public GameStatistics mGameStatistics;

		public bool mHasCheatedToWin;

		public bool mCanHardDrop;

		public bool mCanSoftDrop;

		public int mFallTimeMs;

		public bool mIsMarathonMode;

		public int mCurrentLevel;

		public WellObserver mWellObserver;

		public bool mIsSoftDropping;

		public Tetrimino[] mGhostTetriminoList;

		public TetrisGame(GameParameter gameParameter)
		{
			mGameParameter = gameParameter;
			mSpeed = 1;
			mCanHold = true;
			mGameOverType = 0;
			mLastFeltRow = 19;
			mAnimator = GameApp.Get().GetAnimator();
			mSpecialGameEvent = -1;
			mCanHardDrop = true;
			mCanSoftDrop = true;
			mIsMarathonMode = GameApp.Get().GetGameSettings().IsMarathonMode();
			mCurrentLevel = GetDifficulty() + 1;
			mWell = new Well(this);
			CreateStates();
			SetIsThereTSpin(false);
			SetIsThereBackToBack(false);
			if (GameApp.Get().GetReplay().IsRecording())
			{
				GameApp.Get().GetReplay().Reset();
			}
			else
			{
				GameApp.Get().GetReplay().RestartReplay();
			}
		}

		public virtual void destruct()
		{
		}

		public virtual void GetGameParameters(int numberOfGameParams)
		{
			Package package = mMetaPackage.GetPackage();
			int index = numberOfGameParams * mGameParameter.GetDifficulty();
			int entryPoint = package.GetEntryPoint(index, (int[])null);
			InitSpeed(entryPoint);
		}

		public virtual int SetTetriminoWellBottom(Tetrimino tetrimino)
		{
			Well well = mWell;
			Mino mino = tetrimino.GetRootMino();
			int bottomWellLimit = GetBottomWellLimit();
			int num = bottomWellLimit;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int wellTop = mWell.GetWellTop();
			while (mino != null)
			{
				num2 = mino.GetDefaultIdx();
				if (tetrimino.IsFarthestMinoInDirection(num2, 0, 1))
				{
					num3 = mino.GetMatrixPosX();
					num4 = mino.GetMatrixPosY();
					for (num5 = ((wellTop > num4) ? wellTop : (num4 + 1)); num5 <= 40; num5++)
					{
						if (num5 >= 40 || well.IsThereLockedMino(num3, num5))
						{
							if (num5 >= 40)
							{
								num5 = bottomWellLimit;
							}
							if (num5 - num4 < num)
							{
								num = num5 - num4;
							}
							break;
						}
					}
				}
				mino = mino.GetNextNode();
			}
			num--;
			tetrimino.SetCoreMatrixPos(tetrimino.GetCoreMatrixPosX(), tetrimino.GetCoreMatrixPosY() + num);
			return num;
		}

		public virtual void InitializeGame()
		{
			GameApp.Get().GetReplay().SetGameParameters(GetVariant(), mGameParameter);
			GameTimeSystem.Reset();
			mMinoCount = 0;
			if (mIsMarathonMode)
			{
				mGoalCountDown = 5 * mCurrentLevel;
			}
			else
			{
				mGoalCountDown = mGameParameter.GetLineLimit();
			}
			CreateTetriminoQueue();
			SetPieceFallRateForSpeed();
			mGameStatistics = new GameStatistics(GetVariant(), mCurrentLevel);
			mGameScore = new GameScore(this, mGameStatistics);
			mWellObserver = new WellObserver(mWell);
			HideShadowTrailMatrix();
			CreateGhostTetriminoPool();
		}

		public virtual void CreateTetriminoQueue()
		{
			mTetriminoQueue = new TetriminoQueueWithBag(this);
			mTetriminoQueue.CreateQueue(5);
		}

		public virtual void ReleaseGame()
		{
			mWell = null;
			mGameParameter = null;
			mTetriminoQueue = null;
			if (mGameScore != null)
			{
				mGameScore.ReleaseDisplayArray();
			}
			mGameScore = null;
			if (mGameStatistics != null)
			{
				mGameStatistics.Destroy();
				mGameStatistics = null;
			}
			if (mWellObserver != null)
			{
				mWellObserver = null;
			}
			ReleaseStates();
		}

		public virtual void Load()
		{
			mMetaPackage = GameLibrary.GetPackage(mPackageId);
		}

		public virtual bool IsLoaded()
		{
			if (mMetaPackage != null)
			{
				return mMetaPackage.IsLoaded();
			}
			return false;
		}

		public virtual void GetEntryPoints()
		{
		}

		public virtual void InitializeComponents(GameController gameController)
		{
			mGameController = gameController;
			mAnimationManager = mGameController.GetAnimationManager();
			mLayerComponent = mGameController.GetLayerComponent();
			mLayerComponent.AttachAllSpecialMinosToWellIfNeeded(mWell.GetSpecialMinoList());
		}

		public virtual void Unload()
		{
			DestroyGhostTetriminoPool();
			if (mAnimationManager != null)
			{
				if (mAnimator.IsPlaying(20))
				{
					mAnimator.Skip(20);
				}
				mAnimationManager.ReleaseCustomTimerSequence(20);
			}
			if (mAnimationManager != null)
			{
				mAnimationManager.ReleaseCustomTimerSequence(11);
			}
			mAnimationManager = null;
			if (mWell != null)
			{
				mWell.Unload();
			}
			if (mGameScore != null)
			{
				mGameScore.Unload();
			}
			if (mMetaPackage != null)
			{
				GameLibrary.ReleasePackage(mMetaPackage);
				mMetaPackage = null;
			}
			mGameController = null;
			mLayerComponent = null;
		}

		public virtual Tetrimino CreateTetrimino(sbyte newPieceType)
		{
			Tetrimino tetrimino = mWell.CreateTetrimino(newPieceType);
			OnInitializeNewTetrimino(tetrimino);
			return tetrimino;
		}

		public virtual void ReleaseTetrimino(Tetrimino tetrimino)
		{
			if (tetrimino != null && tetrimino.GetTetriminoType() == -2)
			{
				SpecialMino specialMino = (SpecialMino)tetrimino;
				if (specialMino.IsFloating())
				{
					mNumberOfFloatingSpecialMinos--;
				}
			}
			mWell.ReleaseTetrimino(tetrimino);
		}

		public virtual Tetrimino GetNextTetrimino(int index)
		{
			return mTetriminoQueue.GetTetriminoAt(index);
		}

		public virtual void ForceNextTetriminoI()
		{
			mTetriminoQueue.ForceNextTetriminoI();
		}

		public virtual void ForceNextTetriminoO()
		{
			mTetriminoQueue.ForceNextTetriminoO();
		}

		public virtual Tetrimino GetFallingTetrimino()
		{
			return mFallingTetrimino;
		}

		public virtual Tetrimino GetNextFallingTetrimino()
		{
			mFallingTetrimino = mTetriminoQueue.PopNextTetrimino();
			mFallingTetrimino.SetAllMinoAspectSize(0);
			mGameController.GetLayerComponent().AttachTetrimino(mFallingTetrimino, 2);
			mFallingTetrimino.MoveToStartPosition();
			OnInitializeFallingTetrimino();
			return mFallingTetrimino;
		}

		public virtual void DoPieceAction(int action)
		{
			bool flag = false;
			ShowShadowTrailUnderFallingTetrimino(false);
			int num;
			int dirX;
			int dirY;
			switch (action)
			{
			case 1:
				num = 1;
				goto IL_002d;
			case 0:
				num = -1;
				goto IL_002d;
			case 2:
			case 3:
				{
					flag = mFallingTetrimino.Rotate(action == 2 && CanRotate());
					if (!flag)
					{
						break;
					}
					if (mFallingTetrimino.GetTetriminoType() == 5)
					{
						if (CheckAndFlagTSpin())
						{
							SetIsThereTSpin(true);
							mWell.SetScoreDisplayRow(mFallingTetrimino.GetCoreMatrixPosY());
							((TetriminoT)mFallingTetrimino).SetTSpinned(true);
						}
						else
						{
							SetIsThereTSpin(false);
							((TetriminoT)mFallingTetrimino).SetTSpinned(false);
						}
					}
					if (CheckExtendedLockDownRemainingMoves())
					{
						ResetLockDownDelay();
					}
					OnTetriminoRotate();
					mGameStatistics.IncreaseStatistic(24);
					UpdateGhostOrientation();
					break;
				}
				IL_002d:
				dirX = num;
				dirY = 0;
				if (mFallingTetrimino == null)
				{
					return;
				}
				if (mFallingTetrimino.CanMove(dirX, dirY) && CanMove())
				{
					SetIsThereTSpin(false);
					flag = true;
					mFallingTetrimino.Move(dirX, dirY);
					if (CheckExtendedLockDownRemainingMoves())
					{
						ResetLockDownDelay();
					}
					OnTetriminoSideMove();
					mGameStatistics.IncreaseStatistic(25);
					UpdateGhostPosition();
				}
				break;
			}
			if (flag)
			{
				mGameStatistics.OnValidDoPieceAction();
			}
			ShowShadowTrailUnderFallingTetrimino(true);
		}

		public virtual Well GetWell()
		{
			return mWell;
		}

		public virtual void CheckForLinesToAdd()
		{
		}

		public virtual void AddLine(short lineMask)
		{
			WellLine line = mWell.GetLine(39);
			line.Clear(this);
			SpecialMino specialMino = null;
			for (int i = 0; i < 10; i++)
			{
				if ((lineMask & (1 << i)) != 0)
				{
					specialMino = CreateSpecialMino(8);
					specialMino.SetCoreMatrixPos(i, 40);
					line.SetLockedMino(i, specialMino.GetRootMino());
					mLayerComponent.AttachTetrimino(specialMino, 3);
				}
			}
			mLinesToAddCount--;
			mWell.OnLineAdded();
		}

		public virtual bool IsReadyToAddLines()
		{
			return NeedToAddLines();
		}

		public virtual bool NeedToAddLines()
		{
			return mLinesToAddCount > 0;
		}

		public virtual bool TryToAddLine(short minNbOfHolesPerLine, short maxNbOfHolesPerLine)
		{
			bool flag = mWell.TryToAddLine();
			if (!flag)
			{
				int holeCount = GameRandom.Random(minNbOfHolesPerLine, maxNbOfHolesPerLine);
				AddLine(GetNextBrokenLine(holeCount));
			}
			return flag;
		}

		public virtual void SetSoftDropActive(bool active)
		{
			if (mFallingTetrimino == null)
			{
				return;
			}
			bool flag = IsSoftDropping();
			if (active && !flag)
			{
				mGameStatistics.OnSoftDropActive();
				mNextMoveTimeMs /= 18;
			}
			else if (!active && flag)
			{
				mNextMoveTimeMs *= 18;
				if (mNextMoveTimeMs > mPieceFallRate)
				{
					mNextMoveTimeMs = mPieceFallRate;
				}
				if (mAnimationManager != null)
				{
					mAnimationManager.HideSoftDropTrail();
				}
			}
			mIsSoftDropping = active;
		}

		public virtual bool IsSoftDropping()
		{
			return mIsSoftDropping;
		}

		public virtual void HardDropFallingTetrimino()
		{
			sbyte currentStateID = GetCurrentStateID();
			if (currentStateID == 4 || currentStateID == 5)
			{
				ShowShadowTrailUnderFallingTetrimino(false);
				SetHardDropping(true);
				mGameStatistics.OnHardDrop();
				OnHardDrop();
				HardDropTetrimino(mFallingTetrimino);
				ReleaseGhost();
				OnHardDropDone();
			}
		}

		public virtual void HardDropTetrimino(Tetrimino tetrimino)
		{
			int num = 0;
			if (mGhostTetrimino != null)
			{
				int coreMatrixPosX = mGhostTetrimino.GetCoreMatrixPosX();
				int coreMatrixPosY = mGhostTetrimino.GetCoreMatrixPosY();
				num = coreMatrixPosY - tetrimino.GetCoreMatrixPosY();
				tetrimino.SetCoreMatrixPos(coreMatrixPosX, coreMatrixPosY);
			}
			else
			{
				num = SetTetriminoWellBottom(tetrimino);
			}
			if (UseHardDropScore())
			{
				mGameScore.IncreaseScore(num * 2);
			}
			mLockDownDelayMs = 0;
			mExtendedLockDownRemainingMoves = 0;
		}

		public virtual void PrepareGhostTetrimino()
		{
			if (GameApp.Get().GetGameSettings().IsGhostEnabled())
			{
				InitializeGhost();
				AttachGhostToLayer();
			}
		}

		public virtual void UpdateGhostPosition()
		{
			Tetrimino tetrimino = mGhostTetrimino;
			Tetrimino tetrimino2 = mFallingTetrimino;
			if (GameApp.Get().GetGameSettings().IsGhostEnabled() && tetrimino != null)
			{
				int coreMatrixPosX = tetrimino2.GetCoreMatrixPosX();
				int coreMatrixPosY = tetrimino2.GetCoreMatrixPosY();
				tetrimino.SetCoreMatrixPos(coreMatrixPosX, coreMatrixPosY);
				SetTetriminoWellBottom(tetrimino);
			}
		}

		public virtual void UpdateGhostOrientation()
		{
			if (GameApp.Get().GetGameSettings().IsGhostEnabled() && mGhostTetrimino != null)
			{
				mGhostTetrimino.SetFacingDir(mFallingTetrimino.GetFacingDir());
				UpdateGhostPosition();
				for (Mino mino = mGhostTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
				{
					mino.SetMinoBorders();
				}
			}
		}

		public virtual void UpdateGhostColor()
		{
			Mino rootMino = mFallingTetrimino.GetRootMino();
			sbyte currentAspect = rootMino.GetCurrentAspect();
			for (Mino mino = mGhostTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.SetMinoGhostAspect(currentAspect);
			}
		}

		public virtual void ReleaseGhost()
		{
			if (mGhostTetrimino != null)
			{
				mLayerComponent.DetachTetrimino(mGhostTetrimino);
				mGhostTetrimino = null;
			}
		}

		public virtual void UpdateScore()
		{
			mGameScore.UpdateScore();
		}

		public virtual void UpdateGoal()
		{
			int clearedLineCount = GetClearedLineCount();
			bool flag = IsThereTSpin();
			if (clearedLineCount <= 0 && !flag)
			{
				return;
			}
			int num = clearedLineCount;
			if (mIsMarathonMode)
			{
				if (flag)
				{
					switch (GetSpecialGameEvent())
					{
					case 0:
						num = 1;
						break;
					case 2:
					case 7:
						num = 2;
						break;
					case 1:
						num = 4;
						break;
					case 3:
					case 8:
						num = 8;
						break;
					case 4:
					case 9:
						num = 12;
						break;
					case 5:
					case 10:
						num = 16;
						break;
					}
				}
				else
				{
					switch (clearedLineCount)
					{
					case 1:
						num = 1;
						break;
					case 2:
						num = 3;
						break;
					case 3:
						num = 5;
						break;
					case 4:
						num = 8;
						break;
					}
				}
				if (IsThereBackToBack())
				{
					num = num * 3 / 2;
				}
			}
			mGoalCountDown -= num;
			mGoalCountDown = FlMath.Maximum(mGoalCountDown, 0);
		}

		public virtual void UpdateLevel()
		{
			if (mIsMarathonMode && mGoalCountDown <= 0)
			{
				LevelUp();
			}
		}

		public virtual void IncreaseScore(int pts)
		{
			mGameScore.IncreaseScore(pts);
		}

		public virtual void IncreaseCascadeLevel()
		{
			mGameScore.IncreaseCascadeLevel();
		}

		public virtual void ResetCascadeLevel()
		{
			mGameScore.ResetCascadeLevel();
		}

		public virtual bool IsCascading()
		{
			return mGameScore.GetCascadeLevel() > 0;
		}

		public virtual void PrepareHoldTetrimino()
		{
			if (mCanHold && (GetCurrentStateID() == 4 || GetCurrentStateID() == 5))
			{
				ReleaseGhost();
				mAnimationManager.HideSoftDropTrail();
				HoldTetrimino();
			}
		}

		public virtual Tetrimino GetHeldTetrimino()
		{
			return mHeldTetrimino;
		}

		public virtual int GetCurrentLevel()
		{
			return mCurrentLevel;
		}

		public virtual int GetSpeed()
		{
			return mSpeed;
		}

		public virtual int GetGoalCountdown()
		{
			return mGoalCountDown;
		}

		public virtual int GetNbOfFloatingSpecialMinos()
		{
			return mNumberOfFloatingSpecialMinos;
		}

		public virtual bool IsThereTSpin()
		{
			return mIsThereTSpin;
		}

		public virtual void SetIsThereTSpin(bool isTSpin)
		{
			mIsThereTSpin = isTSpin;
		}

		public virtual bool CheckAndFlagTSpin()
		{
			if (!CanTSpin())
			{
				return false;
			}
			Tetrimino tetrimino = mFallingTetrimino;
			int coreMatrixPosX = tetrimino.GetCoreMatrixPosX();
			int coreMatrixPosY = tetrimino.GetCoreMatrixPosY();
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			if (coreMatrixPosX > 0)
			{
				flag3 = mWell.IsThereLockedMino(coreMatrixPosX - 1, coreMatrixPosY - 1);
				if (coreMatrixPosY < 39)
				{
					flag4 = mWell.IsThereLockedMino(coreMatrixPosX - 1, coreMatrixPosY + 1);
				}
			}
			if (coreMatrixPosX < 9)
			{
				flag = mWell.IsThereLockedMino(coreMatrixPosX + 1, coreMatrixPosY - 1);
				if (coreMatrixPosY < 39)
				{
					flag2 = mWell.IsThereLockedMino(coreMatrixPosX + 1, coreMatrixPosY + 1);
				}
			}
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			switch (tetrimino.GetFacingDir())
			{
			case 0:
				flag5 = flag3;
				flag6 = flag;
				flag7 = flag4;
				flag8 = flag2;
				break;
			case 2:
				flag5 = flag2;
				flag6 = flag4;
				flag7 = flag;
				flag8 = flag3;
				break;
			case 1:
				flag5 = flag;
				flag6 = flag2;
				flag7 = flag3;
				flag8 = flag4;
				break;
			case 3:
				flag5 = flag4;
				flag6 = flag3;
				flag7 = flag2;
				flag8 = flag;
				break;
			}
			if (flag5 && flag6 && (flag7 || flag8))
			{
				mSpecialGameEvent = 1;
			}
			else if (flag7 && flag8 && (flag5 || flag6))
			{
				mSpecialGameEvent = 0;
			}
			else
			{
				mSpecialGameEvent = -1;
			}
			if (mSpecialGameEvent != 1)
			{
				return mSpecialGameEvent == 0;
			}
			return true;
		}

		public virtual bool IsBackToBackPossible()
		{
			return mBackToBackPossible;
		}

		public virtual bool IsThereBackToBack()
		{
			return mIsThereBackToBack;
		}

		public virtual void SetIsThereBackToBack(bool isBackToBack)
		{
			mIsThereBackToBack = isBackToBack;
		}

		public virtual GameController GetGameController()
		{
			return mGameController;
		}

		public virtual int GetClearedLineCount()
		{
			return mClearedLineCount;
		}

		public virtual void SetClearedLineCount(int lineClearedCount)
		{
			mClearedLineCount = lineClearedCount;
		}

		public abstract int GetVariant();

		public virtual bool IsMarathonMode()
		{
			return mIsMarathonMode;
		}

		public abstract int GetGameTitleStringEntryPoint();

		public abstract int GetQuickHintStringEntryPoint();

		public abstract int GetLongHintStringEntryPoint();

		public virtual FlString GetGameTitleString()
		{
			FlString flString = null;
			if (mIsMarathonMode)
			{
				return Utilities.GetStringFromPackage(32, 1310760);
			}
			return Utilities.GetGameStringFromPackage(GetGameTitleStringEntryPoint());
		}

		public virtual FlString GetQuickHintString()
		{
			return new FlString(Utilities.GetGameStringFromPackage(GetQuickHintStringEntryPoint()));
		}

		public virtual FlString GetLongHintString()
		{
			return new FlString(Utilities.GetGameStringFromPackage(GetLongHintStringEntryPoint()));
		}

		public virtual void ResumeInCountdown()
		{
			if (mCurrentGameState.ResumeInCountdownState())
			{
				ChangeState(2, true);
			}
			if (mCurrentGameState.GetID() == 2)
			{
				OnStartCountdown();
				SetTetriminoInWellVisible(false);
			}
		}

		public virtual void SetTetriminoInWellVisible(bool visible)
		{
			mLayerComponent.SetUILayersVisible(visible);
			if (visible)
			{
				if (GameApp.Get().GetGameSettings().GetTouchMode() == 1)
				{
					mLayerComponent.DisplayNextQueue(2);
				}
				else
				{
					mLayerComponent.DisplayNextQueue(5);
				}
			}
			if (visible)
			{
				ShowShadowTrailUnderFallingTetrimino(true);
			}
			else
			{
				HideShadowTrailMatrix();
			}
		}

		public virtual bool CanRecordKeyEvents()
		{
			if (GetCurrentStateID() != 2)
			{
				return GetCurrentStateID() != 1;
			}
			return false;
		}

		public virtual sbyte GetCurrentStateID()
		{
			return mCurrentGameState.GetID();
		}

		public virtual void SetNextGameState(sbyte nextState)
		{
			mCurrentGameState.SetNextGameState(nextState);
		}

		public virtual int GetGameOverType()
		{
			return mGameOverType;
		}

		public virtual bool IsModeOver()
		{
			return GetGameOverType() != 0;
		}

		public virtual void EvaluateAndSetBlockOut()
		{
			Tetrimino tetrimino = mFallingTetrimino;
			if (tetrimino != null && !tetrimino.CanRotate(0, 0, 0))
			{
				SetGameOverType(3);
			}
		}

		public virtual void OnNewFallingTetrimino()
		{
			mMinoCount++;
			if (GameApp.Get().GetGameSettings().GetTouchMode() == 0)
			{
				SetSoftDropActive(false);
			}
			SetHardDropping(false);
			ResetAllLockDownDelays();
			SetLastFeltRow(19);
			UpdateNextMoveTime(GetNextDropFallRate());
			if (mFallingTetrimino.CanMove(0, 1))
			{
				mFallingTetrimino.Move(0, 1);
			}
			PrepareGhostTetrimino();
			ShowShadowTrailUnderFallingTetrimino(true);
		}

		public virtual bool IsHoldHUDEnabled()
		{
			return true;
		}

		public virtual bool IsGoalHUDEnabled()
		{
			return true;
		}

		public virtual int GetGoalHUDStringEntryPoint()
		{
			int result = 8;
			if (GameApp.Get().GetGameSettings().IsMarathonMode())
			{
				result = 5;
			}
			return result;
		}

		public virtual int GetGoalHUDValue()
		{
			return mGoalCountDown;
		}

		public virtual GameScore GetGameScore()
		{
			return mGameScore;
		}

		public virtual GameStatistics GetGameStatistics()
		{
			return mGameStatistics;
		}

		public virtual int GetCurrentTetrisSpeedLevel()
		{
			return mGameStatistics.GetCurrentTetrisSpeedLevel();
		}

		public virtual void IncreaseTetrisSpeedLevelIfNeeded()
		{
			mGameStatistics.IncreaseTetrisSpeedLevelIfNeeded(GetClearedLineCount() >= 4);
		}

		public virtual bool IsWaitingForInitialize()
		{
			return GetCurrentStateID() == 0;
		}

		public virtual void GotoWaitingForIntroductionState()
		{
			ChangeState(1);
		}

		public virtual void OnStartIntroduction()
		{
		}

		public virtual bool IsIntroductionOver()
		{
			return true;
		}

		public virtual void OnStopIntroduction()
		{
		}

		public virtual void OnStartCountdown()
		{
			mAnimator.StartMenuAnimation(5);
		}

		public virtual bool IsCountdownOver()
		{
			return !mAnimator.IsPlaying(5);
		}

		public virtual void OnStopCountdown()
		{
			mAnimator.Skip(5);
		}

		public virtual void OnModeEndTurn()
		{
			if (HasWon())
			{
				mGameOverType = 7;
			}
			else
			{
				mWellObserver.OnModeEndTurn();
			}
			mGameController.UpdateGoalHUD();
		}

		public virtual void OnHardDrop()
		{
		}

		public virtual void OnHardDropDone()
		{
			if (mFallingTetrimino.GetRootMino() != null)
			{
				SetNextGameState(5);
			}
			else
			{
				SetNextGameState(6);
			}
		}

		public virtual void OnInitializeFallingTetrimino()
		{
			mAnimationManager.UpdateSoftDropTrailBitmapMap(GetSoftDropTrailAspect(mFallingTetrimino));
		}

		public virtual void OnInitializeNewTetrimino(Tetrimino tetrimino)
		{
		}

		public virtual void OnLinesCleared()
		{
			SetGravityUpdateNeeded(true);
		}

		public virtual void OnEntryToStateApplyingSpecifics()
		{
		}

		public virtual void OnExitClearingLinesState()
		{
			if (mFallingTetrimino != null && mFallingTetrimino.GetRootMino() == null)
			{
				mFallingTetrimino = null;
			}
		}

		public virtual void OnEntryToStateWaitingForFall()
		{
		}

		public virtual void OnGravityOver()
		{
			SetGravityUpdateNeeded(false);
		}

		public virtual void OnPause()
		{
			if (GetCurrentStateID() == 1)
			{
				mStateIntroduction.OnPause();
			}
			if (GetCurrentStateID() == 2)
			{
				mStateCountdown.OnPause();
			}
			mLayerComponent.SetUILayersVisible(false);
			HideShadowTrailMatrix();
			SetTetriminoInWellVisible(false);
		}

		public virtual void OnResume()
		{
			SynchGhostOption();
			TetriminoList tetriminoList = mWell.GetTetriminoList();
			mLayerComponent.ReattachTetriminosToWellIfNeeded(tetriminoList, 2);
			tetriminoList.UpdateAllTetriminosAspect();
			TetriminoList specialMinoList = mWell.GetSpecialMinoList();
			mLayerComponent.ReattachSpecialMinosToWellIfNeeded(specialMinoList);
			specialMinoList.UpdateAllTetriminosAspect();
			sbyte currentStateID = GetCurrentStateID();
			if (mHardDropping && (currentStateID == 4 || currentStateID == 5))
			{
				OnHardDropDone();
			}
			if (mFallingTetrimino != null && (GetCurrentStateID() == 4 || GetCurrentStateID() == 5))
			{
				mAnimationManager.UpdateSoftDropTrailBitmapMap(GetSoftDropTrailAspect(mFallingTetrimino));
			}
		}

		public virtual void OnSoftDrop()
		{
		}

		public virtual void OnResetKeyPressed()
		{
			SetSoftDropActive(false);
		}

		public virtual void OnStartingNewTurn()
		{
			mGameStatistics.OnStartingNewTurn();
		}

		public virtual void OnTetriminoHold()
		{
		}

		public virtual void OnTetriminoLock()
		{
			mAnimationManager.InitialiseLockDownAnimation(mFallingTetrimino, mLayerComponent);
			if (IsThereTSpin())
			{
				GetGameController().GetAnimationManager().InitializeTSpinLockAnimation(GetFallingTetrimino(), GetGameController().GetLayerComponent());
				mAnimator.StartGameAnimation(7);
			}
			mAnimator.StartGameAnimation(20);
		}

		public virtual void OnTetriminoFall()
		{
			if (IsSoftDropping() && mLastFeltRow < mFallingTetrimino.GetCoreMatrixPosY())
			{
				mGameScore.IncreaseScore(1);
			}
		}

		public virtual void OnTetriminoSideMove()
		{
		}

		public virtual void OnTetriminoRotate()
		{
		}

		public virtual void OnTetriminoAboutToLock()
		{
		}

		public virtual void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			sbyte iD = mCurrentGameState.GetID();
			mGameStatistics.OnTime(totalTimeMs, deltaTimeMs);
			if (iD != 1 && iD != 2 && iD != 0)
			{
				if (mGameOverType == 0)
				{
					mPlayTimeMs += deltaTimeMs;
				}
				if (deltaTimeMs > 125)
				{
					deltaTimeMs = 125;
				}
				if (IsUsingTimeLimit() && IsTimeExpired())
				{
					mGameTimeExpired = true;
					if (iD != 5 && iD != 6 && iD != 8)
					{
						SetGameOverType(6);
						SetNextGameState(10);
					}
				}
				if ((iD == 4 || iD == 5) && mAnimator.IsOver(20))
				{
					mAnimator.Stop(20);
					mAnimationManager.CleanLockDownAnimation();
				}
			}
			mGameScore.CleanScoreDisplayAnimIfOver();
			mCurrentGameState.OnTime(totalTimeMs, deltaTimeMs);
		}

		public virtual void OnTimeStateIntroduction()
		{
		}

		public virtual void OnTimeStateSpecifics()
		{
		}

		public virtual void OnGameOver()
		{
			if (HasWon())
			{
				mGameStatistics.SetStatistic(0, true);
				OnGameWon();
			}
			mGameStatistics.IncreaseStatistic(19, mPlayTimeMs);
			mGameScore.OnGameOver();
			GameApp gameApp = GameApp.Get();
			if (IsGameStatisticsUpdateNeeded())
			{
				bool flag = false;
				flag = gameApp.GetGameSettings().IsMarathonMode();
				if (flag)
				{
					gameApp.GetBioStatistics().Update(mGameStatistics);
				}
				gameApp.GetCareerStatistics().Update(mGameStatistics, flag);
				gameApp.GetExpertManager().Update(mGameStatistics, gameApp.GetCareerStatistics());
			}
		}

		public virtual void OnGameWon()
		{
		}

		public virtual void OnTimeStateGameOver()
		{
		}

		public virtual bool ApplyGravity()
		{
			return mWell.UpdateTetriminoPos(1);
		}

		public virtual bool IsStateSpecificsOver()
		{
			return true;
		}

		public virtual bool CanRotate()
		{
			return true;
		}

		public virtual bool CanMove()
		{
			return true;
		}

		public virtual bool CanTSpin()
		{
			return true;
		}

		public virtual int GetGravityDirection(Tetrimino a15, int a14)
		{
			return 2;
		}

		public virtual int GetLeftWellLimit()
		{
			return -1;
		}

		public virtual int GetRightWellLimit()
		{
			return 10;
		}

		public virtual int GetBottomWellLimit()
		{
			return 40;
		}

		public virtual int GetTetriminoStartPositionX()
		{
			return 4;
		}

		public virtual bool IsFallingTetriminoLockable()
		{
			return !GetFallingTetrimino().IsLocked();
		}

		public virtual void SetHasCheatedToWin()
		{
			mHasCheatedToWin = true;
			mHasWon = true;
		}

		public virtual bool HasWon()
		{
			CheckHasWon();
			if (mHasCheatedToWin)
			{
				mHasWon = true;
			}
			return mHasWon;
		}

		public virtual void CheckHasWon()
		{
			if (IsUsingLineLimit())
			{
				mHasWon = mGoalCountDown <= 0;
			}
			else if (IsUsingTimeLimit())
			{
				mHasWon = IsGameTimeExpired();
			}
		}

		public virtual bool CanApplySpecific(int deltaTimeMs)
		{
			return true;
		}

		public virtual bool TryingToDestroyFloatingMino(Mino mino)
		{
			return true;
		}

		public virtual bool UseFloatingMinos()
		{
			return false;
		}

		public virtual bool NeedToCheckFloatingMinoForBravo()
		{
			return false;
		}

		public virtual sbyte GetSoftDropTrailAspect(Tetrimino tetrimino)
		{
			return Mino.GetDefaultAspect(tetrimino.GetTetriminoType());
		}

		public virtual bool IsCheckForCollisionNeeded()
		{
			if (!UseFloatingMinos())
			{
				return IsGravityEnabled();
			}
			return true;
		}

		public virtual void SetGameOverType(int gameOverType)
		{
			mGameOverType = gameOverType;
		}

		public virtual void CheckForGameOver()
		{
			int num = 0;
			if (IsModeOver())
			{
				num = 7;
			}
			if (num == 0 && IsGameTimeExpired() && IsUsingTimeLimit())
			{
				num = 6;
			}
			if (GameApp.Get().GetIsDemo() && mMinoCount >= 15 && !GameApp.Get().GetReplay().IsPlaying())
			{
				num = 8;
			}
			if (num != 0)
			{
				SetGameOverType(num);
			}
		}

		public virtual bool IsGravityUpdateNeeded()
		{
			return mIsGravityNeeded;
		}

		public virtual void SetGravityUpdateNeeded(bool isGravityNeeded)
		{
			mIsGravityNeeded = isGravityNeeded && IsGravityEnabled();
		}

		public virtual void OnStateEndTurn()
		{
			mGameScore.OnTurnEnd();
			CheckForGameOver();
			OnModeEndTurn();
		}

		public virtual int GetPlayTimeMs()
		{
			return mPlayTimeMs;
		}

		public virtual void ResetSpecialGameEvent()
		{
			mSpecialGameEvent = -1;
		}

		public virtual int GetSpecialGameEvent()
		{
			return mSpecialGameEvent;
		}

		public virtual void UpdateSpecialGameEvent()
		{
			int num = 0;
			if (IsLineClearActive())
			{
				num = GetClearedLineCount();
			}
			bool flag = IsThereTSpin();
			if (flag)
			{
				if (mSpecialGameEvent == 1 || mFallingTetrimino.GetLastRotationPt() == 4)
				{
					switch (num)
					{
					case 0:
						mSpecialGameEvent = 1;
						break;
					case 1:
						mSpecialGameEvent = 3;
						break;
					case 2:
						mSpecialGameEvent = 4;
						break;
					case 3:
						mSpecialGameEvent = 5;
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 0:
						mSpecialGameEvent = 0;
						break;
					case 1:
						mSpecialGameEvent = 2;
						break;
					}
				}
			}
			if (num >= 4)
			{
				if (GetVariant() == 7)
				{
					if (mWell.ComputeLineAdjacency(false) >= 4)
					{
						mSpecialGameEvent = 6;
					}
				}
				else
				{
					mSpecialGameEvent = 6;
				}
			}
			if (mSpecialGameEvent == 6 || (flag && num > 0))
			{
				if (mBackToBackPossible)
				{
					SetIsThereBackToBack(true);
					mSpecialGameEvent += 5;
				}
				mBackToBackPossible = true;
			}
			else if (num > 0 && num < 4)
			{
				mBackToBackPossible = false;
			}
		}

		public virtual bool CanDisplayFeedback()
		{
			bool flag = false;
			return GetCurrentLevel() < 10 && !GetWellObserver().IsWarningActive();
		}

		public virtual int GetDifficulty()
		{
			return mGameParameter.GetDifficulty();
		}

		public virtual void InitSpeed(int speed)
		{
			mSpeed = speed;
			mPieceFallRate = GetFallRateForSpeed(speed);
			mGravityFallRate = 55;
			mSoftDropFallRate = mPieceFallRate / 18;
		}

		public virtual int GetTimeLimit()
		{
			return mGameParameter.GetTimeLimit();
		}

		public virtual bool IsLineClearUsed()
		{
			return true;
		}

		public virtual bool IsLineClearActive()
		{
			return IsLineClearUsed();
		}

		public virtual void CheckForClearedLines()
		{
			mWell.UpdateClearedLineFlag();
			SetClearedLineCount(Well.GetClearedLineCountFromFlags(mWell.GetClearedLineFlags()));
		}

		public virtual bool IsGravityEnabled()
		{
			return false;
		}

		public virtual bool IsUsingTimeLimit()
		{
			return mGameParameter.HasTimeLimit();
		}

		public virtual bool IsUsingLineLimit()
		{
			return mGameParameter.HasLineLimit();
		}

		public virtual bool CanMoveFallingTetrimino()
		{
			bool result = false;
			sbyte currentStateID = GetCurrentStateID();
			if (mFallingTetrimino != null && !mFallingTetrimino.IsLocked() && CanMove() && !IsHardDropping() && (currentStateID != 5 || !IsExtendedMoveExpired()) && (currentStateID == 4 || currentStateID == 5) && (GetVariant() != 11 || !mFallingTetrimino.CanMove(0, 1)))
			{
				result = true;
			}
			return result;
		}

		public virtual bool CheckExtendedLockDownRemainingMoves()
		{
			return --mExtendedLockDownRemainingMoves > 0;
		}

		public virtual int ValidateFallSpeed(int nextFallTimeMs)
		{
			if (nextFallTimeMs < 30)
			{
				nextFallTimeMs = 30;
			}
			return nextFallTimeMs;
		}

		public virtual bool HasLockDownDelayExpired()
		{
			return GetLockDownDelay() <= 0;
		}

		public virtual void UpdateLockDownDelay(int deltaTime)
		{
			mLockDownDelayMs += deltaTime;
		}

		public virtual void ResetAllLockDownDelays()
		{
			ResetLockDownDelay();
			ResetExtendedLockDownRemainingMoves();
		}

		public virtual void ResetLockDownDelay()
		{
			if (GameApp.Get().GetReplay().IsPlaying())
			{
				mLockDownDelayMs = 500;
			}
			else
			{
				mLockDownDelayMs = 600;
			}
		}

		public virtual bool IsGameTimeExpired()
		{
			return mGameTimeExpired;
		}

		public virtual void ChangeState(sbyte ID, bool skipStateExit)
		{
			sbyte iD = mCurrentGameState.GetID();
			if (!skipStateExit)
			{
				mCurrentGameState.OnExit();
			}
			bool flag = mCurrentGameState.SkipNextStateEntry();
			switch (ID)
			{
			case 8:
				mCurrentGameState = mStateApplyingSpecifics;
				break;
			case 6:
				mCurrentGameState = mStateClearingLines;
				break;
			case 7:
				mCurrentGameState = mStateCollapsingLines;
				break;
			case 2:
				mCurrentGameState = mStateCountdown;
				break;
			case 9:
				mCurrentGameState = mStateEndingTurn;
				break;
			case 4:
				mCurrentGameState = mStateFalling;
				break;
			case 10:
				mCurrentGameState = mStateGameOver;
				break;
			case 1:
				mCurrentGameState = mStateIntroduction;
				break;
			case 5:
				mCurrentGameState = mStateLocking;
				break;
			case 0:
				mCurrentGameState = mStateWaitingForInitialize;
				break;
			case 3:
				mCurrentGameState = mStateWaitingForFall;
				break;
			}
			if (skipStateExit && iD != 2)
			{
				mCurrentGameState.SetNextGameState(iD);
			}
			if (!flag)
			{
				mCurrentGameState.OnEntry();
			}
			mCurrentGameState.SetSkipOnEntry(skipStateExit);
		}

		public virtual void LockFallingTetrimino()
		{
			OnTetriminoAboutToLock();
			SetHardDropping(false);
			mLockedOut = mFallingTetrimino.Lock();
			if (mLockedOut)
			{
				SetGameOverType(1);
				SetNextGameState(10);
			}
			else
			{
				mGameStatistics.OnTetriminoLock();
				OnTetriminoLock();
				CheckForClearedLines();
			}
			ShowShadowTrailUnderFallingTetrimino(false);
			ReleaseGhost();
		}

		public virtual void LineClearResult()
		{
			mGameStatistics.OnLineClearResult(GetClearedLineCount());
		}

		public virtual int GetLockDownDelay()
		{
			return mLockDownDelayMs;
		}

		public virtual void ResetNextMoveTime()
		{
			mNextMoveTimeMs = 0;
		}

		public virtual int GetNextMoveTime()
		{
			return mNextMoveTimeMs;
		}

		public virtual void UpdateNextMoveTime(int deltaTime)
		{
			mNextMoveTimeMs += deltaTime;
		}

		public virtual int GetLastFeltRow()
		{
			return mLastFeltRow;
		}

		public virtual void SetLastFeltRow(int row)
		{
			mLastFeltRow = row;
		}

		public virtual int GetGravityFallRate()
		{
			return mGravityFallRate;
		}

		public virtual int GetNextDropFallRate()
		{
			return ValidateFallSpeed(IsSoftDropping() ? mSoftDropFallRate : mPieceFallRate);
		}

		public virtual void SetPieceFallRateForSpeed()
		{
			mPieceFallRate = GetFallRateForSpeed(mSpeed);
		}

		public virtual bool IsHardDropping()
		{
			return mHardDropping;
		}

		public virtual void SetHardDropping(bool state)
		{
			mHardDropping = state;
		}

		public virtual void EvaluateCanHold()
		{
			mCanHold = ((GetNextTetrimino() != null || mHeldTetrimino != null) ? true : false);
		}

		public virtual bool HasFloatingSpecialMinos()
		{
			return mNumberOfFloatingSpecialMinos > 0;
		}

		public virtual bool IsDoingAnimation()
		{
			return mAnimationManager.IsDoingAnimation();
		}

		public virtual void PrepareLineClearAnimation()
		{
			if (GameApp.Get().GetGameSettings().IsGhostEnabled() && mGhostTetrimino != null)
			{
				mLayerComponent.DetachTetrimino(mGhostTetrimino);
			}
			CreateClearLineAnimation();
		}

		public virtual void ReleaseCustomTimerSequence(sbyte animId)
		{
			mAnimationManager.ReleaseCustomTimerSequence(animId);
		}

		public virtual void CleanLineClearAnim()
		{
			mAnimationManager.CleanClearLineAnim();
		}

		public virtual bool PlayEvent(ReplayEvent replayEvent)
		{
			bool result = true;
			switch (replayEvent.GetType())
			{
			case 0:
				DoPieceAction(2);
				break;
			case 1:
				DoPieceAction(3);
				break;
			case 2:
				DoPieceAction(0);
				break;
			case 3:
				DoPieceAction(1);
				break;
			case 4:
				SetSoftDropActive(true);
				break;
			case 5:
				SetSoftDropActive(false);
				break;
			case 7:
				PrepareHoldTetrimino();
				break;
			case 6:
				HardDropFallingTetrimino();
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		public virtual bool CanSoftDrop()
		{
			return mCanSoftDrop;
		}

		public virtual bool CanHardDrop()
		{
			return mCanHardDrop;
		}

		public virtual void ShowShadowTrailUnderFallingTetrimino(bool visible)
		{
			Tetrimino tetrimino = mFallingTetrimino;
			bool flag = tetrimino != null && (!tetrimino.IsLocked() || !visible);
			if (mGhostTetrimino != null && tetrimino.GetCoreMatrixPosY() == mGhostTetrimino.GetCoreMatrixPosY() && tetrimino.GetCoreMatrixPosX() == mGhostTetrimino.GetCoreMatrixPosX())
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			Mino mino = tetrimino.GetRootMino();
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Well well = mWell;
			Shape shape = null;
			Tetrimino tetrimino2 = mGhostTetrimino;
			while (mino != null)
			{
				if (tetrimino.IsFarthestMinoInDirection(mino.GetDefaultIdx(), 0, 1))
				{
					int num = mino.GetMatrixPosY() + 1;
					int matrixPosX = mino.GetMatrixPosX();
					if (num < 20)
					{
						num = 20;
					}
					int num2 = 0;
					int num3 = 0;
					for (int i = num; i < 40 && !well.IsThereLockedMino(matrixPosX, i) && (tetrimino2 == null || !tetrimino2.HasMinoAtCoordinates(matrixPosX, i)); i++)
					{
						num3 = 10 * (i - 20) + matrixPosX;
						num2 = 120 + num3;
						shape = EntryPoint.GetShape(preLoadedPackage, num2);
						shape.SetVisible(visible);
						num2 = 320 + num3;
						shape = EntryPoint.GetShape(preLoadedPackage, num2);
						shape.SetVisible(!visible);
					}
				}
				mino = mino.GetNextNode();
			}
		}

		public virtual void ResumeFromPauseMenu()
		{
			SynchGhostOption();
			ResumeInCountdown();
		}

		public virtual bool IsExtendedMoveExpired()
		{
			return mExtendedLockDownRemainingMoves <= 0;
		}

		public virtual WellObserver GetWellObserver()
		{
			return mWellObserver;
		}

		public virtual SpecialMino CreateSpecialMino(sbyte minoType)
		{
			SpecialMino specialMino = (SpecialMino)mWell.CreateTetrimino(-2);
			specialMino.SetSpecialType(minoType);
			if (specialMino.IsFloating())
			{
				mNumberOfFloatingSpecialMinos++;
			}
			return specialMino;
		}

		public virtual int GetFallRateForSpeed(int speed)
		{
			switch (speed)
			{
			case 1:
				return 1000;
			case 2:
				return 900;
			case 3:
				return 800;
			case 4:
				return 700;
			case 5:
				return 600;
			case 6:
				return 450;
			case 7:
				return 320;
			case 8:
				return 220;
			case 9:
				return 150;
			case 10:
				return 100;
			case 11:
				return 85;
			case 12:
				return 70;
			case 13:
				return 55;
			case 14:
				return 40;
			case 15:
				return 30;
			case 30:
				return 1;
			default:
				return 0;
			}
		}

		public virtual bool IsTimeExpired()
		{
			if (!mGameTimeExpired)
			{
				return mPlayTimeMs >= mGameParameter.GetTimeLimit();
			}
			return false;
		}

		public virtual bool UseHardDropScore()
		{
			return true;
		}

		public virtual void InitializePlacementMatrix(int topRow, int bottomRow)
		{
			int num = (mPlacementMatrixHeight = bottomRow - topRow + 1);
			mPlacementMatrixRowOffset = topRow;
			mPlacementMatrix = RectangularArrays.ReturnRectangularBoolArray(num, 10);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					mPlacementMatrix[i][j] = false;
				}
			}
		}

		public virtual void ReleasePlacementMatrix()
		{
			for (int i = 0; i < mPlacementMatrixHeight; i++)
			{
				mPlacementMatrix[i] = null;
			}
			mPlacementMatrix = null;
		}

		public virtual void SetPlacementMatrixPos(int row, int col, bool takePos)
		{
			row -= mPlacementMatrixRowOffset;
			mPlacementMatrix[row][col] = takePos;
		}

		public virtual void ClearPlacementMatrixPos()
		{
			for (int i = 0; i < mPlacementMatrixHeight; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					mPlacementMatrix[i][j] = false;
				}
			}
		}

		public virtual void PopulatePlacementMatrix(int topRow, int bottomRow, int nbToAdd, sbyte minoType)
		{
			InitializePlacementMatrix(topRow, bottomRow);
			int num = 0;
			while (num < nbToAdd)
			{
				int num2 = GameRandom.Random(0, 9);
				int num3 = GameRandom.Random(topRow, bottomRow);
				if (IsBlockAvailable(num2, num3))
				{
					SetPlacementMatrixPos(num3, num2, true);
					SpecialMino specialMino = CreateSpecialMino(minoType);
					specialMino.SetCoreMatrixPos(num2, num3);
					mWell.AddMino(num3, num2, specialMino.GetRootMino());
					num++;
				}
			}
			ReleasePlacementMatrix();
		}

		public virtual bool IsPosTakenInPlacementMatrix(int row, int col)
		{
			row -= mPlacementMatrixRowOffset;
			return mPlacementMatrix[row][col];
		}

		public virtual bool IsBlockAvailable(int column, int row)
		{
			bool result = true;
			if (IsPosTakenInPlacementMatrix(row, column) || (column + 1 < 10 && mWell.IsThereLockedMino(column + 1, row)) || (column - 1 >= 0 && mWell.IsThereLockedMino(column - 1, row)) || (row + 1 < 40 && mWell.IsThereLockedMino(column, row + 1)) || (row - 1 >= 0 && mWell.IsThereLockedMino(column, row - 1)))
			{
				result = false;
			}
			return result;
		}

		public virtual short GetNextBrokenLine(int holeCount)
		{
			int num = 3;
			int num2 = 1023;
			for (int i = 0; i < holeCount; i++)
			{
				int num3 = 1 << GameRandom.Random(0, 9);
				while ((num2 & num3) == 0)
				{
					if (num3 < 1 << num)
					{
						num3 <<= 10;
					}
					num3 >>= num;
				}
				num2 &= ~num3;
			}
			return (short)num2;
		}

		public virtual void CreateStates()
		{
			mStateApplyingSpecifics = new StateApplyingSpecifics(this);
			mStateClearingLines = new StateClearingLines(this);
			mStateCollapsingLines = new StateCollapsingLines(this);
			mStateCountdown = new StateCountdown(this);
			mStateEndingTurn = new StateEndingTurn(this);
			mStateFalling = new StateFalling(this);
			mStateGameOver = new StateGameOver(this);
			mStateIntroduction = new StateIntroduction(this);
			mStateLocking = new StateLocking(this);
			mStateWaitingForFall = new StateWaitingForFall(this);
			mStateWaitingForInitialize = new StateWaitingForInitialize(this);
			mCurrentGameState = mStateWaitingForInitialize;
		}

		public virtual void ReleaseStates()
		{
			mStateApplyingSpecifics = null;
			mStateClearingLines = null;
			mStateCollapsingLines = null;
			mStateCountdown = null;
			mStateEndingTurn = null;
			mStateFalling = null;
			mStateGameOver = null;
			mStateIntroduction = null;
			mStateLocking = null;
			mStateWaitingForFall = null;
			mStateWaitingForInitialize = null;
			mCurrentGameState = null;
		}

		public virtual bool IsValidMinoPosition(int posX, int posY)
		{
			if (0 <= posX && posX < 10 && 20 <= posY)
			{
				return posY <= 39;
			}
			return false;
		}

		public virtual void CreateGhostTetriminoPool()
		{
			mGhostTetriminoList = new Tetrimino[7];
			for (int i = 0; i < 7; i++)
			{
				mGhostTetriminoList[i] = TetriminoList.CreateTetriminoObject((sbyte)i, mWell);
				for (Mino mino = mGhostTetriminoList[i].GetRootMino(); mino != null; mino = mino.GetNextNode())
				{
					mino.SetType(7);
				}
			}
		}

		public virtual void DestroyGhostTetriminoPool()
		{
			if (mGhostTetriminoList != null)
			{
				for (int i = 0; i < 7; i++)
				{
					if (mGhostTetriminoList[i] != null)
					{
						mGhostTetriminoList[i].Unload();
						mGhostTetriminoList[i] = null;
					}
				}
			}
			mGhostTetriminoList = null;
			mGhostTetrimino = null;
		}

		public virtual Tetrimino GetGhostTetrimino(sbyte tetriminoType)
		{
			return mGhostTetriminoList[tetriminoType];
		}

		public virtual void HideShadowTrailMatrix()
		{
			MetaPackage package = GameLibrary.GetPackage(1867833);
			Package package2 = package.GetPackage();
			Shape shape = null;
			int num = 199;
			for (int i = 0; i <= num; i++)
			{
				shape = EntryPoint.GetShape(package2, i + 120);
				shape.SetVisible(false);
				shape = EntryPoint.GetShape(package2, i + 320);
				shape.SetVisible(true);
			}
			GameLibrary.ReleasePackage(package);
		}

		public virtual void InitializeGhost()
		{
			mGhostTetrimino = GetGhostTetrimino(mFallingTetrimino.GetTetriminoType());
			UpdateGhostOrientation();
			UpdateGhostColor();
			UpdateGhostPosition();
		}

		public virtual void HoldTetrimino()
		{
			if (mFallingTetrimino.GetTetriminoType() == 5)
			{
				((TetriminoT)mFallingTetrimino).SetTSpinned(false);
				SetIsThereTSpin(false);
				ResetSpecialGameEvent();
			}
			ShowShadowTrailUnderFallingTetrimino(false);
			if (mHeldTetrimino == null)
			{
				mHeldTetrimino = mFallingTetrimino;
				GetNextFallingTetrimino();
				mGameController.UpdateNextPiece(true);
			}
			else
			{
				Tetrimino tetrimino = mHeldTetrimino;
				mHeldTetrimino = mFallingTetrimino;
				mFallingTetrimino = tetrimino;
				mAnimationManager.UpdateSoftDropTrailBitmapMap(GetSoftDropTrailAspect(mFallingTetrimino));
			}
			mFallingTetrimino.MoveToStartPosition();
			mHeldTetrimino.ForceDefaultPosition();
			mHeldTetrimino.SetVisible(true);
			mGameStatistics.OnTetriminoHold();
			OnTetriminoHold();
			mGameController.SwapFallingAndHoldTetrinimos();
			mFallingTetrimino.SetAllMinoAspectSize(0);
			mHeldTetrimino.SetAllMinoAspectSize(1);
			mCanHold = false;
			EvaluateAndSetBlockOut();
			if (GetGameOverType() == 0)
			{
				OnNewFallingTetrimino();
				ChangeState(4);
			}
			else
			{
				ChangeState(10);
			}
		}

		public virtual void FinalizeHoldingAnimation()
		{
		}

		public virtual void ResetExtendedLockDownRemainingMoves()
		{
			mExtendedLockDownRemainingMoves = 15;
		}

		public virtual bool IsGameStatisticsUpdateNeeded()
		{
			if (!GameApp.Get().GetReplay().IsPlaying())
			{
				return !GameApp.Get().GetIsDemo();
			}
			return false;
		}

		public virtual void CreateClearLineAnimation()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 39;
			AnimationManager animationManager = mAnimationManager;
			int clearedLineFlags = mWell.GetClearedLineFlags();
			bool flag = false;
			while (num < mClearedLineCount)
			{
				if (Well.IsClearedLineFromFlags(clearedLineFlags, num3))
				{
					num2++;
					num++;
				}
				if (num2 == 4 || (num2 > 0 && (!Well.IsClearedLineFromFlags(clearedLineFlags, num3 - 1) || num >= mClearedLineCount)))
				{
					animationManager.CreateLineClearAnimation(num3, num2, mWell, mLayerComponent);
					num2 = 0;
				}
				num3--;
			}
		}

		public virtual void LevelUp()
		{
			if (mCurrentLevel < 15)
			{
				int num = mCurrentLevel + 1;
				InitSpeed(num);
				mGoalCountDown = 5 * num;
				mCurrentLevel = num;
				mGameStatistics.OnLevelUp(num);
				mGameController.UpdateLevelHUD();
				if (GetCurrentLevel() < 10 && !GetWellObserver().IsWarningActive())
				{
					GetGameController().PrepareFeedbackDisplay(18);
				}
			}
			else
			{
				mHasWon = true;
			}
		}

		public virtual void SynchGhostOption()
		{
			bool flag = GameApp.Get().GetGameSettings().IsGhostEnabled();
			if (mGhostTetrimino == null)
			{
				if (flag && mFallingTetrimino != null && !mFallingTetrimino.IsLocked())
				{
					InitializeGhost();
					AttachGhostToLayer();
				}
			}
			else if (!flag)
			{
				ReleaseGhost();
			}
			else if (mFallingTetrimino != null && !mFallingTetrimino.IsLocked())
			{
				AttachGhostToLayer();
			}
		}

		public virtual void AttachGhostToLayer()
		{
			if (mGhostTetrimino != null)
			{
				mLayerComponent.AttachTetrimino(mGhostTetrimino, 2);
			}
			if (mFallingTetrimino != null)
			{
				mLayerComponent.AttachTetrimino(mFallingTetrimino, 2);
			}
		}

		public virtual Tetrimino GetNextTetrimino()
		{
			return GetNextTetrimino(0);
		}

		public virtual void ChangeState(sbyte ID)
		{
			ChangeState(ID, false);
		}
	}
}
