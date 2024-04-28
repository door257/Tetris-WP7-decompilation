using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class AnimationController
	{
		public const sbyte dirBackward = -1;

		public const sbyte noDirection = 0;

		public const sbyte dirForward = 1;

		public Animation[] mAnimations = new Animation[24];

		public AnimationController()
		{
			for (int i = 0; i < mAnimations.Length; i++)
			{
				mAnimations[i] = null;
			}
		}

		public virtual void destruct()
		{
		}

		public virtual void LoadAnimations(Package _package, sbyte firstId, int firstEntryPoint, int length)
		{
			_package.SetNextEntryPointIndex(firstEntryPoint);
			for (int i = firstId; i < firstId + length; i++)
			{
				mAnimations[i] = InternalRegisterAnimation(_package);
			}
		}

		public virtual void LoadSingleAnimation(Package _package, sbyte animId, int timeSystemEntryPoint)
		{
			_package.SetNextEntryPointIndex(timeSystemEntryPoint);
			mAnimations[animId] = InternalRegisterAnimation(_package);
		}

		public virtual void ExternalRegisterAnimation(sbyte animId, TimerSequence timerSequence, int duration)
		{
			Animation animation = new Animation();
			animation.SetTimerSequence(timerSequence);
			animation.SetDuration(duration);
			mAnimations[animId] = animation;
		}

		public virtual void UnloadAnimations(sbyte firstId, int length)
		{
			for (int i = firstId; i < firstId + length; i++)
			{
				if (mAnimations[i] != null)
				{
					InternalUnregisterAnimation((sbyte)i);
				}
			}
		}

		public virtual void UnloadSingleAnimation(sbyte animId)
		{
			InternalUnregisterAnimation(animId);
		}

		public virtual void CleanControllers(sbyte animId)
		{
			TimerSequence timerSequence = mAnimations[animId].GetTimerSequence();
			for (int i = 0; i < timerSequence.GetNbChildren(); i++)
			{
				if (timerSequence.GetChild(i) != null)
				{
					((KeyFrameController)timerSequence.GetChild(i)).SetControllee(null);
				}
			}
		}

		public virtual void StartGameAnimation(sbyte animId, sbyte dir, int speedFactorPercent)
		{
			Start(animId, dir, true, speedFactorPercent);
		}

		public virtual void StartMenuAnimation(sbyte animId, sbyte dir, int speedFactorPercent)
		{
			Start(animId, dir, false, speedFactorPercent);
		}

		public virtual void Stop(sbyte animId)
		{
			TimerSequence timerSequence = mAnimations[animId].GetTimerSequence();
			timerSequence.Stop();
			GameTimeSystem.UnRegister(timerSequence);
			Reset(animId);
		}

		public virtual void Pause(sbyte animId)
		{
			mAnimations[animId].GetTimerSequence().Pause();
		}

		public virtual void Skip(sbyte animId)
		{
			Animation animation = mAnimations[animId];
			TimerSequence timerSequence = animation.GetTimerSequence();
			if (timerSequence.GetTimeFlowSpeed().IsPositive())
			{
				int duration = animation.GetDuration();
				timerSequence.SetTotalTime(duration);
				timerSequence.OnTime(duration, 0);
			}
			else
			{
				timerSequence.SetTotalTime(0);
				timerSequence.OnTime(0, 0);
			}
			Stop(animId);
		}

		public virtual void SkipAnimations()
		{
			for (int i = 0; i < 24; i++)
			{
				if (mAnimations[i] != null && IsPlaying((sbyte)i))
				{
					Skip((sbyte)i);
				}
			}
		}

		public virtual bool IsValid(sbyte animId)
		{
			return mAnimations[animId] != null;
		}

		public virtual bool IsOver(sbyte animId)
		{
			if (!IsStarted(animId))
			{
				return false;
			}
			Animation animation = mAnimations[animId];
			TimerSequence timerSequence = animation.GetTimerSequence();
			int totalTime = timerSequence.GetTotalTime();
			if (GameTimeSystem.IsRegistered(timerSequence))
			{
				if (totalTime > 0)
				{
					return totalTime >= animation.GetDuration();
				}
				return true;
			}
			return false;
		}

		public virtual bool IsStarted(sbyte animId)
		{
			if (mAnimations[animId] == null)
			{
				return false;
			}
			return GameTimeSystem.IsRegistered(mAnimations[animId].GetTimerSequence());
		}

		public virtual bool IsPlaying(sbyte animId)
		{
			if (mAnimations[animId] == null)
			{
				return false;
			}
			Animation animation = mAnimations[animId];
			TimerSequence timerSequence = animation.GetTimerSequence();
			int totalTime = timerSequence.GetTotalTime();
			if (GameTimeSystem.IsRegistered(timerSequence) && totalTime > 0)
			{
				return totalTime < animation.GetDuration();
			}
			return false;
		}

		public virtual void Reset(sbyte animId)
		{
			Animation animation = mAnimations[animId];
			TimerSequence timerSequence = animation.GetTimerSequence();
			int currentTime = ((!timerSequence.GetTimeFlowSpeed().IsPositive()) ? animation.GetDuration() : 0);
			SetChildrenRecursively(timerSequence, currentTime);
		}

		public virtual sbyte PlayingDirection(sbyte animId)
		{
			if (!mAnimations[animId].GetTimerSequence().GetTimeFlowSpeed().IsPositive())
			{
				return -1;
			}
			return 1;
		}

		public virtual Animation GetAnimation(sbyte animId)
		{
			return mAnimations[animId];
		}

		public virtual void Start(sbyte animId, sbyte dir, bool gameAnimation, int speedFactorPercent)
		{
			Animation animation = mAnimations[animId];
			TimerSequence timerSequence = animation.GetTimerSequence();
			int totalTime = ((dir != 1) ? animation.GetDuration() : 0);
			F32 f = new F32(F32.One(16));
			if (speedFactorPercent != 100)
			{
				F32 f2 = new F32(F32.FromInt(100, 16));
				F32 f3 = new F32(F32.FromInt(speedFactorPercent, 16));
				f = f3.Div(f2, 16);
			}
			timerSequence.SetTimeFlowSpeed(f.Mul(dir));
			timerSequence.SetTotalTime(totalTime);
			GameTimeSystem.Register(timerSequence, gameAnimation);
			timerSequence.Start();
			timerSequence.OnTime(totalTime, 1);
		}

		public virtual Animation InternalRegisterAnimation(Package _package)
		{
			TimerSequence timerSequence = EntryPoint.GetTimerSequence(_package, -1);
			int entryPoint = _package.GetEntryPoint(-1, (int[])null);
			Animation animation = new Animation();
			animation.SetTimerSequence(timerSequence);
			animation.SetDuration(entryPoint);
			return animation;
		}

		public virtual void InternalUnregisterAnimation(sbyte anim)
		{
			if (mAnimations[anim] != null)
			{
				Stop(anim);
				mAnimations[anim] = null;
			}
		}

		public virtual void SetChildrenRecursively(TimeControlled tc, int currentTime, int deltaTime, sbyte dir)
		{
			if (tc != null && tc is TimeSystem)
			{
				TimeSystem timeSystem = (TimeSystem)tc;
				if (dir != 0)
				{
					F32 timeFlowSpeed = new F32(F32.FromInt(dir, 16));
					timeSystem.SetTimeFlowSpeed(timeFlowSpeed);
				}
				timeSystem.SetTotalTime(currentTime);
				timeSystem.OnTime(currentTime, deltaTime);
				for (int i = 0; i < timeSystem.GetNbChildren(); i++)
				{
					TimeControlled child = timeSystem.GetChild(i);
					SetChildrenRecursively(child, currentTime, dir);
				}
			}
		}

		public virtual void StartGameAnimation(sbyte animId)
		{
			StartGameAnimation(animId, 1);
		}

		public virtual void StartGameAnimation(sbyte animId, sbyte dir)
		{
			StartGameAnimation(animId, dir, 100);
		}

		public virtual void StartMenuAnimation(sbyte animId)
		{
			StartMenuAnimation(animId, 1);
		}

		public virtual void StartMenuAnimation(sbyte animId, sbyte dir)
		{
			StartMenuAnimation(animId, dir, 100);
		}

		public virtual void SetChildrenRecursively(TimeControlled tc, int currentTime)
		{
			SetChildrenRecursively(tc, currentTime, 0);
		}

		public virtual void SetChildrenRecursively(TimeControlled tc, int currentTime, int deltaTime)
		{
			SetChildrenRecursively(tc, currentTime, deltaTime, 0);
		}

		public static AnimationController[] InstArrayAnimationController(int size)
		{
			AnimationController[] array = new AnimationController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new AnimationController();
			}
			return array;
		}

		public static AnimationController[][] InstArrayAnimationController(int size1, int size2)
		{
			AnimationController[][] array = new AnimationController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new AnimationController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new AnimationController();
				}
			}
			return array;
		}

		public static AnimationController[][][] InstArrayAnimationController(int size1, int size2, int size3)
		{
			AnimationController[][][] array = new AnimationController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new AnimationController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new AnimationController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new AnimationController();
					}
				}
			}
			return array;
		}
	}
}
