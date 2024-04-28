using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VariantChill : TetrisGame
	{
		public const sbyte hotBlock = 0;

		public const sbyte coldBlock = 1;

		public const sbyte iceBlock = 2;

		public const sbyte blockTemperatureCount = 3;

		public int mFrequency;

		public EventBarIndicator mIndicator;

		public VariantChill(GameParameter gameParameter)
			: base(gameParameter)
		{
			mFrequency = -1;
			mPackageId = 950301;
			mIndicator = new EventBarIndicator();
		}

		public override void destruct()
		{
			mIndicator = null;
		}

		public override int GetVariant()
		{
			return 9;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 94;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 95;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 97;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mMetaPackage.GetPackage();
			GetGameParameters(2);
			mFrequency = mGameParameter.GetFromPackage(package, 2, 1);
			mAnimator.LoadSingleAnimation(package, 18, 30);
			mIndicator.GetEntryPoints();
		}

		public override void InitializeGame()
		{
			base.InitializeGame();
			mIndicator.Initialize(mFrequency);
		}

		public override void ReleaseGame()
		{
			mIndicator.ReleaseBar();
			base.ReleaseGame();
		}

		public override void InitializeComponents(GameController gameController)
		{
			base.InitializeComponents(gameController);
			SetTetriminoTemperatureValue(mTetriminoQueue.GetTetriminoAt(0), 0);
			mLayerComponent.Attach(mIndicator.GetViewport(), 7);
			mAnimationManager.CreateCustomTimerSequence(19, 1173, 200);
			UpdateBlockAfterWind();
			AttachChillBar();
		}

		public override void Unload()
		{
			if (mAnimationManager != null)
			{
				if (mAnimator.IsStarted(19))
				{
					mAnimationManager.CleanChillMinoAspectChange();
				}
				mAnimationManager.ReleaseCustomTimerSequence(19);
			}
			ReleaseChillBar();
			if (mAnimator.IsValid(18))
			{
				mAnimator.UnloadSingleAnimation(18);
			}
			mIndicator.Unload();
			base.Unload();
		}

		public override void OnResume()
		{
			base.OnResume();
			mIndicator.Resume();
		}

		public override void OnModeEndTurn()
		{
			base.OnModeEndTurn();
			if (mIndicator.IsFull())
			{
				mIndicator.Reset();
			}
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			sbyte currentStateID = GetCurrentStateID();
			if (currentStateID == 9 && mIndicator.IsFull() && !mIndicator.IsDoingEvent())
			{
				mIndicator.Vanish();
				mAnimator.StartGameAnimation(18);
				InitChillSwitchAspectAnimation();
				mAnimator.StartGameAnimation(19);
			}
			if (mAnimator.IsOver(19))
			{
				TerminateChillSwitchAspectAnimation();
			}
			if (mAnimator.IsOver(18))
			{
				mAnimator.Stop(18);
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnInitializeFallingTetrimino()
		{
			SetTetriminoTemperatureValue(mTetriminoQueue.GetTetriminoAt(0), 0);
			base.OnInitializeFallingTetrimino();
		}

		public override bool IsDoingAnimation()
		{
			if (!mAnimator.IsPlaying(18) && !mAnimator.IsPlaying(19) && !mIndicator.IsDoingAnimation())
			{
				return base.IsDoingAnimation();
			}
			return true;
		}

		public override bool TryingToDestroyFloatingMino(Mino mino)
		{
			((SpecialMino)mino.GetTetrimino()).SetSpecialType(12);
			return false;
		}

		public override bool UseFloatingMinos()
		{
			return true;
		}

		public override bool NeedToCheckFloatingMinoForBravo()
		{
			return true;
		}

		public override void OnEntryToStateApplyingSpecifics()
		{
			base.OnEntryToStateApplyingSpecifics();
			Tetrimino tetrimino = mWell.GetSpecialMinoList().GetRootTetrimino();
			bool flag = false;
			while (tetrimino != null)
			{
				if (tetrimino.GetRootMino() != null && tetrimino.IsLocked() && tetrimino.GetRootMino().GetTetriminoType() == 12)
				{
					Mino rootMino = tetrimino.GetRootMino();
					flag |= mWell.GetLine(rootMino.GetMatrixPosY()).ConvertFloatingMinoToNonFloating(rootMino.GetMatrixPosX(), rootMino);
				}
				tetrimino = tetrimino.GetNextNode();
			}
			if (flag)
			{
				SetGravityUpdateNeeded(true);
			}
		}

		public override void OnInitializeNewTetrimino(Tetrimino tetrimino)
		{
			SetTetriminoTemperatureValue(tetrimino, 0);
		}

		public virtual bool UpdateBlockAfterWind()
		{
			if (mIndicator.IsFull())
			{
				IncreaseBlocksTemperature(mWell.GetTetriminoList());
				IncreaseBlocksTemperature(mWell.GetSpecialMinoList());
			}
			SetGravityUpdateNeeded(true);
			return false;
		}

		public virtual void AttachChillBar()
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(950301);
			Viewport viewport = null;
			for (int i = 0; i < 3; i++)
			{
				viewport = EntryPoint.GetViewport(preLoadedPackage, 34 + i);
				mLayerComponent.Attach(viewport, 1);
			}
		}

		public virtual void ReleaseChillBar()
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(950301);
			Viewport viewport = null;
			for (int i = 0; i < 3; i++)
			{
				viewport = EntryPoint.GetViewport(preLoadedPackage, 34 + i);
				viewport.SetViewport(null);
			}
		}

		public virtual void SetTetriminoTemperatureValue(Tetrimino tetrimino, sbyte temperature)
		{
			switch (temperature)
			{
			case 0:
				tetrimino.SetAllMinoAspect(1);
				break;
			case 1:
				tetrimino.SetAllMinoAspect(0);
				break;
			case 2:
				ReplaceTetriminoByConcreteMino(tetrimino);
				break;
			}
		}

		public static sbyte GetBlockTemperature(Tetrimino tetrimino)
		{
			switch (tetrimino.GetRootMino().GetCurrentAspect())
			{
			case 1:
				return 0;
			case 0:
				return 1;
			default:
				return 3;
			}
		}

		public virtual void ReplaceTetriminoByConcreteMino(Tetrimino tetrimino)
		{
			Mino mino = tetrimino.GetRootMino();
			while (mino != null)
			{
				Mino nextNode = mino.GetNextNode();
				mLayerComponent.Detach(mino.GetMinoViewport());
				mWell.RemoveMino(mino.GetMatrixPosY(), mino.GetMatrixPosX());
				SpecialMino specialMino = CreateSpecialMino(13);
				specialMino.SetCoreMatrixPos(mino.GetMatrixPosX(), mino.GetMatrixPosY());
				specialMino.Lock();
				mLayerComponent.AttachTetrimino(specialMino, 3);
				mino = nextNode;
			}
			ReleaseTetrimino(tetrimino);
		}

		public override sbyte GetSoftDropTrailAspect(Tetrimino tetrimino)
		{
			return tetrimino.GetRootMino().GetCurrentAspect();
		}

		public override void OnTetriminoLock()
		{
			base.OnTetriminoLock();
			mIndicator.Increment();
		}

		public virtual void InitChillSwitchAspectAnimation()
		{
			Well well = mWell;
			int wellTop = well.GetWellTop();
			WellLine wellLine = null;
			Mino mino = null;
			AnimationManager animationManager = mAnimationManager;
			for (int i = wellTop; i < 40; i++)
			{
				wellLine = well.GetLine(i);
				if (!wellLine.HasNonFloatingLockedMinos())
				{
					continue;
				}
				for (int j = 0; j < 10; j++)
				{
					if (wellLine.IsThereLockedMino(j))
					{
						mino = wellLine.GetLockedMino(j);
						if (mino.GetCurrentAspect() != 16)
						{
							int keyFrameSequenceEntryPoint = ((GetBlockTemperature(mino.GetTetrimino()) == 0) ? 32 : 33);
							animationManager.RegisterChillMinoAspectChange(mino, j, keyFrameSequenceEntryPoint);
						}
					}
				}
			}
		}

		public virtual void TerminateChillSwitchAspectAnimation()
		{
			mAnimationManager.CleanChillMinoAspectChange();
			UpdateBlockAfterWind();
		}

		public virtual void IncreaseBlocksTemperature(TetriminoList tetriminoList)
		{
			Tetrimino tetrimino = tetriminoList.GetRootTetrimino();
			while (tetrimino != null)
			{
				Tetrimino nextNode = tetrimino.GetNextNode();
				if (tetrimino.GetRootMino() != null && tetrimino.IsLocked() && tetrimino.GetRootMino().GetTetriminoType() != 13)
				{
					sbyte temperature = (sbyte)(GetBlockTemperature(tetrimino) + 1);
					SetTetriminoTemperatureValue(tetrimino, temperature);
				}
				tetrimino = nextNode;
			}
		}
	}
}
