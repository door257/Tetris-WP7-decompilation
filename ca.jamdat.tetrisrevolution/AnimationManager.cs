using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class AnimationManager
	{
		public Package mPackage;

		public int[] mTimerSequencesIndexes;

		public TimerSequence[] mTimerSequences;

		public KeyFrameController[] mKeyControllers;

		public int mTimerSequenceCount;

		public int mKeyControllerIndex;

		public int mMaxNbOfWellComponentControllers;

		public Viewport[] mSoftDropTrailViewports;

		public ResizableFrame[] mClearLineResizableFrame;

		public AnimationManager()
		{
			mMaxNbOfWellComponentControllers = 40;
			mTimerSequences = new TimerSequence[10];
			mTimerSequencesIndexes = new int[24];
			for (int i = 0; i < 24; i++)
			{
				mTimerSequencesIndexes[i] = -1;
			}
			mKeyControllers = new KeyFrameController[mMaxNbOfWellComponentControllers];
			for (int j = 0; j < mMaxNbOfWellComponentControllers; j++)
			{
				mKeyControllers[j] = new KeyFrameController();
			}
			mSoftDropTrailViewports = new Viewport[4];
			mClearLineResizableFrame = new ResizableFrame[20];
			CreateCustomTimerSequence(11, 250, 40);
		}

		public virtual void GetEntryPoints(Package _package, LayerComponent layerComponent)
		{
			mPackage = _package;
			for (int i = 0; i < 4; i++)
			{
				mSoftDropTrailViewports[i] = EntryPoint.GetViewport(mPackage, 95 + i);
				layerComponent.Attach(mSoftDropTrailViewports[i], 6);
			}
			AnimationController animator = GameApp.Get().GetAnimator();
			if (!animator.IsValid(20))
			{
				animator.LoadSingleAnimation(_package, 20, 31);
			}
		}

		public virtual bool IsDoingAnimation()
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			if (!animator.IsPlaying(11))
			{
				return animator.IsPlaying(20);
			}
			return true;
		}

		public virtual void Clean()
		{
			UnregisterAllAnimControllers();
			for (int i = 0; i < mMaxNbOfWellComponentControllers; i++)
			{
				KeyFrameController keyFrameController = mKeyControllers[i];
				if (keyFrameController != null)
				{
					keyFrameController = null;
				}
			}
			for (int i = 0; i < 10; i++)
			{
				if (mTimerSequences[i] != null)
				{
					UnregisterAllChildren(mTimerSequences[i]);
					mTimerSequences[i] = null;
				}
			}
			mKeyControllers = null;
			mTimerSequences = null;
			mTimerSequencesIndexes = null;
			for (int i = 0; i < 4; i++)
			{
				mSoftDropTrailViewports[i].SetVisible(false);
				mSoftDropTrailViewports[i].SetViewport(null);
			}
			mSoftDropTrailViewports = null;
			DeleteClearLineResizableFrame();
			AnimationController animator = GameApp.Get().GetAnimator();
			animator.UnloadSingleAnimation(20);
			DestroyLineReboundAnim();
		}

		public virtual void CreateCustomTimerSequence(sbyte animId, int duration, int controllerCount)
		{
			int nextTimerSequenceIndex = GetNextTimerSequenceIndex();
			mTimerSequences[nextTimerSequenceIndex] = new TimerSequence(controllerCount);
			GameApp.Get().GetAnimator().ExternalRegisterAnimation(animId, mTimerSequences[nextTimerSequenceIndex], duration);
			mTimerSequencesIndexes[animId] = nextTimerSequenceIndex;
		}

		public virtual void ReleaseCustomTimerSequence(sbyte animIdToRelease)
		{
			int num = mTimerSequencesIndexes[animIdToRelease];
			if (num >= 0 && mTimerSequences[num] != null)
			{
				UnregisterAllChildren(mTimerSequences[num]);
				GameApp.Get().GetAnimator().UnloadSingleAnimation(animIdToRelease);
				mTimerSequences[num] = null;
				mTimerSequencesIndexes[animIdToRelease] = -1;
				mTimerSequenceCount--;
			}
		}

		public virtual void UnregisterAnimControllers(sbyte animId)
		{
			if (IsAnimUsingControllerPool(animId))
			{
				CleanAnimControllers(animId);
			}
			UnregisterAllChildren(GetTimerSequence(animId));
		}

		public virtual void UnregisterAnimController(sbyte animId, KeyFrameController controller)
		{
			GetTimerSequence(animId).UnRegister(controller);
		}

		public virtual void RegisterMinoSpriteFrameIndexController(sbyte animId, MinoSprite minoSprite, KeyFrameSequence keyFrameSequence, int startTime, int duration)
		{
			KeyFrameController frameIndexController = minoSprite.GetFrameIndexController();
			InitController(frameIndexController, minoSprite, keyFrameSequence, 7);
			RegisterController(frameIndexController, animId, startTime, duration);
		}

		public virtual void RegisterMinoSpriteAspectController(sbyte animId, MinoSprite minoSprite, KeyFrameSequence keyFrameSequence, int startTime, int duration)
		{
			KeyFrameController aspectController = minoSprite.GetAspectController();
			InitController(aspectController, minoSprite, keyFrameSequence, 102);
			RegisterController(aspectController, animId, startTime, duration);
		}

		public virtual void RegisterMinoFrameIndexController(sbyte animId, Mino mino, KeyFrameSequence keyFrameSequence, int startTime, int duration)
		{
			RegisterMinoSpriteFrameIndexController(animId, mino.GetMinoSprite(), keyFrameSequence, startTime, duration);
		}

		public virtual void CreateLineClearAnimation(int firstLineIndex, int lineCount, Well well, LayerComponent layerComponent)
		{
			ResizableFrame clearLineResizableFrame = GetClearLineResizableFrame();
			layerComponent.Attach(clearLineResizableFrame, 3);
			short left = 0;
			short top = (short)((firstLineIndex - 20) * 31);
			clearLineResizableFrame.SetTopLeft(left, top);
			int entryPoint = 521 + lineCount - 1;
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(preLoadedPackage, entryPoint);
			RegisterCustomSequence(11, clearLineResizableFrame, keyFrameSequence, 4, 0, 250, false);
			keyFrameSequence = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 525);
			RegisterCustomSequence(11, clearLineResizableFrame, keyFrameSequence, 6, 0, 250, true);
		}

		public virtual void CleanClearLineAnim()
		{
			CleanAnimControllers(11);
			UnregisterAllChildren(GetTimerSequence(11));
			FreeClearLineResizableFrame();
		}

		public virtual void DeleteClearLineResizableFrame()
		{
			for (int i = 0; i < 20; i++)
			{
				if (mClearLineResizableFrame[i] != null)
				{
					mClearLineResizableFrame[i].SetViewport(null);
					mClearLineResizableFrame[i] = null;
				}
			}
			mClearLineResizableFrame = null;
		}

		public virtual void FreeClearLineResizableFrame()
		{
			for (int i = 0; i < 20; i++)
			{
				if (mClearLineResizableFrame[i] != null)
				{
					mClearLineResizableFrame[i].SetViewport(null);
				}
			}
		}

		public virtual void InitialiseLockDownAnimation(Tetrimino lockTetrimino, LayerComponent layerComponent)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Viewport viewport = null;
			Viewport viewport2 = null;
			Mino mino = null;
			short num = 0;
			short num2 = 0;
			mino = lockTetrimino.GetRootMino();
			for (int i = 0; i < 4; i++)
			{
				viewport2 = mino.GetMinoViewport();
				viewport = EntryPoint.GetViewport(preLoadedPackage, 33 + i);
				if (viewport2.IsVisible())
				{
					num = viewport2.GetRectTop();
					num2 = viewport2.GetRectLeft();
					viewport.SetTopLeft(num2, num);
					layerComponent.Attach(viewport, 2);
				}
				else
				{
					viewport.SetViewport(null);
				}
				mino = mino.GetNextNode();
			}
		}

		public virtual void CleanLockDownAnimation()
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Viewport viewport = null;
			for (int i = 0; i < 4; i++)
			{
				viewport = EntryPoint.GetViewport(preLoadedPackage, 33 + i);
				viewport.SetViewport(null);
			}
		}

		public virtual void CreateLineReboundAnim(Well well)
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			if (!animator.IsValid(22))
			{
				Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
				animator.LoadSingleAnimation(preLoadedPackage, 22, 40);
				TimerSequence timerSequence = EntryPoint.GetTimerSequence(preLoadedPackage, 40);
				ClearLineReboundController timeable = new ClearLineReboundController(well);
				timerSequence.RegisterInterval(timeable, 100, 32767);
				animator.LoadSingleAnimation(preLoadedPackage, 23, 42);
			}
			else
			{
				Package preLoadedPackage2 = GameLibrary.GetPreLoadedPackage(1867833);
				TimerSequence timerSequence2 = EntryPoint.GetTimerSequence(preLoadedPackage2, 40);
				ClearLineReboundController clearLineReboundController = (ClearLineReboundController)timerSequence2.GetChild(0);
				clearLineReboundController.SetWell(well);
			}
		}

		public virtual void DestroyLineReboundAnim()
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			if (animator.IsValid(22))
			{
				Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
				animator.UnloadSingleAnimation(22);
				TimerSequence timerSequence = EntryPoint.GetTimerSequence(preLoadedPackage, 40);
				timerSequence.GetChild(0);
				timerSequence.UnRegisterAll();
				animator.UnloadSingleAnimation(23);
			}
		}

		public virtual void InitializeTSpinLockAnimation(Tetrimino tSpinTetrimino, LayerComponent layerComponent)
		{
		}

		public virtual ResizableFrame GetClearLineResizableFrame()
		{
			int num = 0;
			for (num = 0; num < 20 && mClearLineResizableFrame[num] != null && mClearLineResizableFrame[num].GetViewport() != null; num++)
			{
			}
			if (mClearLineResizableFrame[num] == null)
			{
				Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
				FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(preLoadedPackage, 520);
				mClearLineResizableFrame[num] = new ResizableFrame(5, flBitmapMap);
			}
			else
			{
				mClearLineResizableFrame[num].SetSize(0, 0);
			}
			return mClearLineResizableFrame[num];
		}

		public virtual void RegisterScannerMinoAspectChange(Mino mino, int wellRow)
		{
			int entryPoint = 32 + (mino.GetCurrentAspect() - 7);
			KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(884763, entryPoint);
			int startTime = (40 - wellRow) * 20 + 333;
			RegisterMinoSpriteAspectController(17, mino.GetMinoSprite(), keyFrameSequence, startTime, 1);
		}

		public virtual void RegisterChillMinoAspectChange(Mino mino, int wellColomn, int keyFrameSequenceEntryPoint)
		{
			KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(950301, keyFrameSequenceEntryPoint);
			int startTime = wellColomn * 60 + 333;
			RegisterMinoSpriteAspectController(19, mino.GetMinoSprite(), keyFrameSequence, startTime, 240);
		}

		public virtual void CleanChillMinoAspectChange()
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			animator.Reset(19);
			TimerSequence timerSequence = animator.GetAnimation(19).GetTimerSequence();
			for (int num = 9; num >= 0; num--)
			{
				int totalTime = num * 60 + 333;
				timerSequence.SetTotalTime(totalTime);
				timerSequence.OnTime(totalTime, 0);
			}
			animator.Stop(19);
			UnregisterAnimControllers(19);
		}

		public virtual void RegisterCustomSequence(sbyte animId, TimeControlled controllee, KeyFrameSequence keyFrameSequence, int controlValue, int startTime, int duration, bool absolute)
		{
			KeyFrameController nextKeyFrameController = GetNextKeyFrameController();
			InitController(nextKeyFrameController, controllee, keyFrameSequence, controlValue, absolute);
			RegisterController(nextKeyFrameController, animId, startTime, duration);
		}

		public virtual void HideSoftDropTrail()
		{
			for (int i = 0; i < 4; i++)
			{
				mSoftDropTrailViewports[i].SetVisible(false);
			}
		}

		public virtual void UpdateSoftDropTrailBitmapMap(sbyte aspectIndex)
		{
			FlBitmap flBitmap = null;
			FlBitmapMap flBitmapMap = null;
			flBitmapMap = FlBitmapMap.Cast(mPackage.GetEntryPoint(87), null);
			flBitmap = FlBitmap.Cast(mPackage.GetEntryPoint(88 + aspectIndex), null);
			flBitmapMap.SetReferenceBitmap(flBitmap);
		}

		public virtual void ShowSoftDropTrail(Tetrimino fallingTetrimino)
		{
			HideSoftDropTrail();
			for (Mino mino = fallingTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				if (mino.IsMinoValid())
				{
					int defaultIdx = mino.GetDefaultIdx();
					if (fallingTetrimino.IsFarthestMinoInDirection(defaultIdx, 0, -1))
					{
						((IndexedSprite)mSoftDropTrailViewports[defaultIdx].GetChild(0)).SetCurrentFrame(fallingTetrimino.GetCoreMatrixPosY() & 1);
						short left = (short)(mino.GetMatrixPosX() * 31 + 3);
						short top = (short)((mino.GetMatrixPosY() - 20) * 31 + -90);
						mSoftDropTrailViewports[defaultIdx].SetTopLeft(left, top);
						mSoftDropTrailViewports[defaultIdx].SetVisible(true);
					}
				}
			}
		}

		public virtual void UnregisterAllAnimControllers()
		{
			CleanControllersPool();
			for (int i = 0; i < mTimerSequenceCount; i++)
			{
				if (mTimerSequences[i] != null)
				{
					UnregisterAllChildren(mTimerSequences[i]);
				}
			}
			mTimerSequenceCount = 0;
		}

		public virtual TimerSequence GetTimerSequence(sbyte animId)
		{
			return mTimerSequences[mTimerSequencesIndexes[animId]];
		}

		public virtual void RegisterController(KeyFrameController controller, sbyte animId, int startTime, int duration)
		{
			mTimerSequences[mTimerSequencesIndexes[animId]].RegisterInterval(controller, startTime, duration);
		}

		public virtual void CleanControllersPool()
		{
			for (int i = 0; i < mMaxNbOfWellComponentControllers; i++)
			{
				KeyFrameController keyFrameController = mKeyControllers[i];
				keyFrameController.SetControlParameters(null, 0);
				keyFrameController.SetKeySequence(null);
			}
			mKeyControllerIndex = 0;
		}

		public virtual bool IsAnimUsingControllerPool(sbyte animId)
		{
			return animId == 11;
		}

		public virtual void CleanAnimControllers(sbyte animId)
		{
			TimerSequence timerSequence = GetTimerSequence(animId);
			int i = 0;
			for (int nbChildren = timerSequence.GetNbChildren(); i < nbChildren; i++)
			{
				KeyFrameController keyFrameController = (KeyFrameController)timerSequence.GetChild(i);
				if (keyFrameController == null)
				{
					break;
				}
				keyFrameController.SetIsAbsolute(true);
				keyFrameController.SetControlParameters(null, 0);
				keyFrameController.SetKeySequence(null);
			}
		}

		public virtual int GetNextTimerSequenceIndex()
		{
			int i;
			for (i = 0; i < 10 && mTimerSequences[i] != null; i++)
			{
			}
			mTimerSequenceCount++;
			return i;
		}

		public virtual KeyFrameController GetNextKeyFrameController()
		{
			int i;
			for (i = mKeyControllerIndex; i < mMaxNbOfWellComponentControllers && mKeyControllers[i].GetControllee() != null; i++)
			{
			}
			mKeyControllerIndex++;
			if (mKeyControllerIndex == mMaxNbOfWellComponentControllers)
			{
				mKeyControllerIndex = 0;
			}
			return mKeyControllers[i];
		}

		public virtual void InitController(KeyFrameController controller, TimeControlled controllee, KeyFrameSequence keyFrameSequence, int controlCode, bool absolute)
		{
			controller.SetControlParameters(controllee, controlCode);
			controller.SetKeySequence(keyFrameSequence);
			controller.SetIsAbsolute(absolute);
		}

		public virtual void UnregisterAllChildren(TimerSequence timer)
		{
			int nbChildren = timer.GetNbChildren();
			for (int i = 0; i < nbChildren; i++)
			{
				TimeControlled child = timer.GetChild(i);
				timer.UnRegister(child);
			}
		}

		public virtual void RegisterLineClearAnimControllers(Mino clearedMino, KeyFrameSequence lineClearKeyFrameSequence, int startTime)
		{
			RegisterMinoFrameIndexController(11, clearedMino, lineClearKeyFrameSequence, startTime, 650);
		}

		public virtual void RegisterCustomSequence(sbyte animId, TimeControlled controllee, KeyFrameSequence keyFrameSequence, int controlValue, int startTime, int duration)
		{
			RegisterCustomSequence(animId, controllee, keyFrameSequence, controlValue, startTime, duration, true);
		}

		public virtual void InitController(KeyFrameController controller, TimeControlled controllee, KeyFrameSequence keyFrameSequence, int controlCode)
		{
			InitController(controller, controllee, keyFrameSequence, controlCode, true);
		}

		public static AnimationManager[] InstArrayAnimationManager(int size)
		{
			AnimationManager[] array = new AnimationManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new AnimationManager();
			}
			return array;
		}

		public static AnimationManager[][] InstArrayAnimationManager(int size1, int size2)
		{
			AnimationManager[][] array = new AnimationManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new AnimationManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new AnimationManager();
				}
			}
			return array;
		}

		public static AnimationManager[][][] InstArrayAnimationManager(int size1, int size2, int size3)
		{
			AnimationManager[][][] array = new AnimationManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new AnimationManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new AnimationManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new AnimationManager();
					}
				}
			}
			return array;
		}
	}
}
