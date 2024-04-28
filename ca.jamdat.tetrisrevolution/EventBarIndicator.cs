using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class EventBarIndicator : TimeControlled
	{
		public Viewport mViewport;

		public Viewport mOnViewport;

		public Viewport mVanishViewport;

		public int mFrequency;

		public int mFrequencyCounter;

		public bool mIsFull;

		public bool mIsDoingEvent;

		public short[] mSegmentSizes;

		public AnimationController mAnimationController;

		public EventBarIndicator()
		{
			mAnimationController = GameApp.Get().GetAnimator();
		}

		public virtual void Initialize(int frequency)
		{
			mFrequency = frequency;
			int num = frequency;
			int num2 = 309 / num;
			int num3 = 309 - num2 * num;
			num++;
			mSegmentSizes = new short[num];
			mSegmentSizes[0] = 0;
			for (int i = 1; i < num; i++)
			{
				mSegmentSizes[i] = (short)((i > 0) ? mSegmentSizes[i - 1] : 0);
				mSegmentSizes[i] = (short)(mSegmentSizes[i] + num2);
				mSegmentSizes[i] += (short)((i < num3) ? 1 : 0);
			}
			Reset();
		}

		public virtual void ReleaseBar()
		{
			mSegmentSizes = null;
		}

		public virtual void GetEntryPoints()
		{
			MetaPackage package = GameLibrary.GetPackage(1048608);
			Package package2 = package.GetPackage();
			mViewport = Viewport.Cast(package2.GetEntryPoint(0), null);
			mOnViewport = Viewport.Cast(package2.GetEntryPoint(5), null);
			mVanishViewport = Viewport.Cast(package2.GetEntryPoint(6), null);
			mAnimationController.LoadSingleAnimation(package2, 12, 1);
			mAnimationController.LoadSingleAnimation(package2, 13, 3);
			GameLibrary.ReleasePackage(package);
		}

		public virtual Viewport GetViewport()
		{
			return mViewport;
		}

		public virtual void Unload()
		{
			GameTimeSystem.UnRegister(this);
			if (mViewport != null)
			{
				mViewport.SetViewport(null);
			}
			if (mAnimationController.IsValid(12))
			{
				mAnimationController.UnloadSingleAnimation(12);
				mAnimationController.UnloadSingleAnimation(13);
			}
		}

		public virtual void Resume()
		{
			UpdateBarSize();
			if (mIsFull && !mIsDoingEvent)
			{
				Blink();
			}
		}

		public virtual void Increment()
		{
			if (!mIsFull)
			{
				mFrequencyCounter++;
				if (mFrequencyCounter <= mFrequency)
				{
					UpdateBarSize();
				}
				if (mFrequencyCounter >= mFrequency)
				{
					OnFullBar();
				}
			}
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mIsFull && mAnimationController.IsOver(12))
			{
				mAnimationController.Stop(12);
				GameTimeSystem.UnRegister(this);
			}
		}

		public virtual void Reset()
		{
			mFrequencyCounter = 0;
			mIsFull = false;
			mIsDoingEvent = false;
			mVanishViewport.SetVisible(false);
			UpdateBarSize();
		}

		public virtual bool IsFull()
		{
			return mIsFull;
		}

		public virtual bool IsDoingEvent()
		{
			return mIsDoingEvent;
		}

		public virtual void Vanish()
		{
			Unblink();
			mIsDoingEvent = true;
			mVanishViewport.SetVisible(true);
			if (!mAnimationController.IsStarted(12))
			{
				mAnimationController.StartGameAnimation(12);
			}
		}

		public virtual bool IsDoingAnimation()
		{
			return mAnimationController.IsPlaying(12);
		}

		public virtual void OnFullBar()
		{
			Blink();
			GameTimeSystem.Register(this, true);
			mIsFull = true;
		}

		public virtual void Blink()
		{
			if (!mAnimationController.IsStarted(13))
			{
				mAnimationController.StartGameAnimation(13);
			}
		}

		public virtual void Unblink()
		{
			if (mAnimationController.IsStarted(13))
			{
				mAnimationController.Stop(13);
				mOnViewport.SetVisible(true);
			}
		}

		public virtual void UpdateBarSize()
		{
			mOnViewport.SetSize(mSegmentSizes[mFrequencyCounter], 3);
		}

		public static EventBarIndicator[] InstArrayEventBarIndicator(int size)
		{
			EventBarIndicator[] array = new EventBarIndicator[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new EventBarIndicator();
			}
			return array;
		}

		public static EventBarIndicator[][] InstArrayEventBarIndicator(int size1, int size2)
		{
			EventBarIndicator[][] array = new EventBarIndicator[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EventBarIndicator[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EventBarIndicator();
				}
			}
			return array;
		}

		public static EventBarIndicator[][][] InstArrayEventBarIndicator(int size1, int size2, int size3)
		{
			EventBarIndicator[][][] array = new EventBarIndicator[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EventBarIndicator[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EventBarIndicator[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new EventBarIndicator();
					}
				}
			}
			return array;
		}
	}
}
