using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PlayMenuHelixObserver : HelixObserver
	{
		public const sbyte noAnimation = -1;

		public const sbyte blopRedUp = 0;

		public const sbyte blopRedDown = 1;

		public const sbyte blopRedCenter = 2;

		public const sbyte blopWhiteUp = 3;

		public const sbyte blopWhiteDown = 4;

		public const sbyte blopWhiteCenter = 5;

		public const sbyte blopCount = 6;

		public PlayMenu mPlayMenuScene;

		public Viewport mBlopViewport;

		public TimeSystem mPopRedUpTimeSystem;

		public TimeSystem mPopRedDownTimeSystem;

		public TimeSystem mPopRedCenterTimeSystem;

		public TimeSystem mPopWhiteUpTimeSystem;

		public TimeSystem mPopWhiteDownTimeSystem;

		public TimeSystem mPopWhiteCenterTimeSystem;

		public sbyte mCurrentAnim;

		public PlayMenuHelixObserver(PlayMenu playMenuScene)
		{
			mPlayMenuScene = playMenuScene;
			mCurrentAnim = -1;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(Viewport blopViewport, Package _package, int firstTimeSystemEntryPoint)
		{
			mBlopViewport = blopViewport;
			mPopRedUpTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint++);
			mPopRedDownTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint++);
			mPopRedCenterTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint++);
			mPopWhiteUpTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint++);
			mPopWhiteDownTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint++);
			mPopWhiteCenterTimeSystem = EntryPoint.GetTimeSystem(_package, firstTimeSystemEntryPoint);
		}

		public virtual void Unload()
		{
			if (mCurrentAnim != -1)
			{
				StopBlopAnimation();
			}
			if (mBlopViewport != null)
			{
				mBlopViewport = null;
			}
			if (mPopRedUpTimeSystem != null)
			{
				mPopRedUpTimeSystem = null;
			}
			if (mPopRedDownTimeSystem != null)
			{
				mPopRedDownTimeSystem = null;
			}
			if (mPopRedCenterTimeSystem != null)
			{
				mPopRedCenterTimeSystem = null;
			}
			if (mPopWhiteUpTimeSystem != null)
			{
				mPopWhiteUpTimeSystem = null;
			}
			if (mPopWhiteDownTimeSystem != null)
			{
				mPopWhiteDownTimeSystem = null;
			}
			if (mPopWhiteCenterTimeSystem != null)
			{
				mPopWhiteCenterTimeSystem = null;
			}
		}

		public override void PassThroughVariant(int variant, sbyte direction, int deltaTimeExceeded)
		{
			PlayDownUpBlopAnimation(variant, direction, deltaTimeExceeded);
		}

		public override void OnTarget(int variant)
		{
			PlayCenterBlopAnimation(variant);
			mPlayMenuScene.OnHelixOnTarget();
		}

		public override void LeaveTarget()
		{
			mPlayMenuScene.OnHelixLeaveTarget();
		}

		public virtual void PlayDownUpBlopAnimation(int variant, sbyte direction, int deltaTimeExceeded)
		{
			if (mCurrentAnim != -1)
			{
				StopBlopAnimation();
			}
			FeatsExpert featsExpert = FeatsExpert.Get();
			sbyte popAnimId = (sbyte)(featsExpert.IsGameVariantUnlocked(variant) ? ((direction == 0) ? 3 : 4) : ((direction != 0) ? 1 : 0));
			StartBlopAnimation(popAnimId, deltaTimeExceeded);
		}

		public virtual void PlayCenterBlopAnimation(int variant)
		{
			if (mCurrentAnim != -1)
			{
				StopBlopAnimation();
			}
			FeatsExpert featsExpert = FeatsExpert.Get();
			sbyte popAnimId = (sbyte)((!featsExpert.IsGameVariantUnlocked(variant)) ? 2 : 5);
			StartBlopAnimation(popAnimId, 0);
		}

		public virtual void StartBlopAnimation(sbyte popAnimId, int startTime)
		{
			TimeSystem timeSystem = null;
			switch (popAnimId)
			{
			case 0:
				timeSystem = mPopRedUpTimeSystem;
				break;
			case 1:
				timeSystem = mPopRedDownTimeSystem;
				break;
			case 2:
				timeSystem = mPopRedCenterTimeSystem;
				break;
			case 3:
				timeSystem = mPopWhiteUpTimeSystem;
				break;
			case 4:
				timeSystem = mPopWhiteDownTimeSystem;
				break;
			case 5:
				timeSystem = mPopWhiteCenterTimeSystem;
				break;
			}
			mBlopViewport.SetVisible(true);
			timeSystem.RegisterInGlobalTime();
			timeSystem.OnTime(0, startTime);
			timeSystem.Start();
			mCurrentAnim = popAnimId;
		}

		public virtual void StopBlopAnimation()
		{
			TimeSystem timeSystem = null;
			switch (mCurrentAnim)
			{
			case 0:
				timeSystem = mPopRedUpTimeSystem;
				break;
			case 1:
				timeSystem = mPopRedDownTimeSystem;
				break;
			case 2:
				timeSystem = mPopRedCenterTimeSystem;
				break;
			case 3:
				timeSystem = mPopWhiteUpTimeSystem;
				break;
			case 4:
				timeSystem = mPopWhiteDownTimeSystem;
				break;
			case 5:
				timeSystem = mPopWhiteCenterTimeSystem;
				break;
			}
			mBlopViewport.SetVisible(false);
			timeSystem.Stop();
			timeSystem.UnRegisterInGlobalTime();
			mCurrentAnim = -1;
		}
	}
}
