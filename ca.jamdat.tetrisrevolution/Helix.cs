using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Helix : Viewport
	{
		public const sbyte background = 0;

		public const sbyte controllable = 1;

		public sbyte mType;

		public MetaPackage mMetaPackage;

		public Package mPackage;

		public Selector mSelector;

		public Viewport mHelixViewport;

		public TimerSequence mHelixTimerSequence;

		public int mLastSelectionIdx;

		public int mTarget;

		public HelixObserver mObserver;

		public F32 mIndexTimeStepF32;

		public Array_int mIndexToTimeArray;

		public bool mIsOnTarget;

		public Helix(sbyte type)
		{
			mType = type;
			mIndexTimeStepF32 = new F32(F32.Zero(16));
		}

		public override void destruct()
		{
		}

		public virtual void Load()
		{
			mMetaPackage = GameLibrary.GetPackage(1900602);
			mPackage = mMetaPackage.GetPackage();
		}

		public virtual bool IsLoaded()
		{
			return GameLibrary.IsPackageLoaded(mMetaPackage);
		}

		public virtual void Initialize(Viewport parent, Selector selector)
		{
			mSelector = selector;
			Package package = mPackage;
			mHelixViewport = EntryPoint.GetViewport(package, 0);
			mHelixViewport.SetViewport(parent);
			CustomComponentUtilities.Attach(this, mHelixViewport);
			mHelixTimerSequence = EntryPoint.GetTimerSequence(package, 1);
			FlBitmapMap flBitmapMap = null;
			FlBitmapMap flBitmapMap2 = null;
			flBitmapMap = EntryPoint.GetFlBitmapMap(package, 2);
			flBitmapMap2 = EntryPoint.GetFlBitmapMap(package, 3);
			int num = 4;
			FeatsExpert featsExpert = FeatsExpert.Get();
			int num2 = 0;
			while (num2 < 12)
			{
				IndexedSprite indexedSprite = EntryPoint.GetIndexedSprite(package, num);
				indexedSprite.SetBitmapMap(featsExpert.IsGameVariantUnlocked(num2) ? flBitmapMap2 : flBitmapMap);
				num2++;
				num++;
			}
			mIndexTimeStepF32 = new F32(21845334, 16);
			mIndexToTimeArray = Array_int.Cast(package.GetEntryPoint(16), null);
			if (mType == 1)
			{
				mLastSelectionIdx = mSelector.GetSingleSelection();
				int num3 = LoopOnRange(mLastSelectionIdx + -3, 0, 12, 12);
				mTarget = num3 - -3;
				mHelixTimerSequence.OnTime(0, mIndexToTimeArray.GetAt(num3 + 12));
				RegisterInGlobalTime();
			}
		}

		public virtual void Unload()
		{
			CustomComponentUtilities.Detach(this);
			if (mType == 1)
			{
				UnRegisterInGlobalTime();
			}
			if (mHelixViewport != null)
			{
				mHelixViewport.SetViewport(null);
				mHelixViewport = null;
			}
			if (mHelixTimerSequence != null)
			{
				mHelixTimerSequence = null;
			}
			mObserver = null;
			mIndexToTimeArray = null;
			if (mPackage != null)
			{
				GameLibrary.ReleasePackage(mMetaPackage);
				mMetaPackage = null;
				mPackage = null;
			}
		}

		public virtual void Start()
		{
			mHelixTimerSequence.RegisterInGlobalTime();
			mHelixTimerSequence.Start();
		}

		public virtual void Stop()
		{
			if (mHelixTimerSequence != null)
			{
				mHelixTimerSequence.Stop();
				mHelixTimerSequence.UnRegisterInGlobalTime();
			}
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			int singleSelection = mSelector.GetSingleSelection();
			int num = FindShorterPath(mLastSelectionIdx, singleSelection);
			mLastSelectionIdx = singleSelection;
			if (num != 0)
			{
				if (mIsOnTarget)
				{
					mObserver.LeaveTarget();
					mIsOnTarget = false;
				}
				mTarget += num;
				mTarget = LoopOnRange(mTarget, -12, 24, 12);
			}
			int at = mIndexToTimeArray.GetAt(mTarget + 12);
			int totalTime2 = mHelixTimerSequence.GetTotalTime();
			if (at == totalTime2)
			{
				return;
			}
			int num2;
			if (totalTime2 < at)
			{
				num2 = totalTime2 + deltaTime;
				if (num2 > at)
				{
					num2 = at;
				}
			}
			else
			{
				num2 = totalTime2 - deltaTime;
				if (num2 < at)
				{
					num2 = at;
				}
			}
			int num3 = LoopOnRange(num2, 0, 4000, 4000);
			if (num2 != num3)
			{
				mTarget += ((num3 > num2) ? 12 : (-12));
			}
			else if (FlMath.Absolute(num3 - at) > 4333)
			{
				mTarget += ((num3 > at) ? 12 : (-12));
			}
			mHelixTimerSequence.SetTotalTime(num3);
			mHelixTimerSequence.OnTime(0, 0);
			if (num2 == at)
			{
				mObserver.OnTarget(mTarget);
				mIsOnTarget = true;
				return;
			}
			F32 f = new F32(F32.FromInt(totalTime2, 16));
			int num4 = f.Div(mIndexTimeStepF32, 16).ToInt(16);
			F32 f2 = new F32(F32.FromInt(num3, 16));
			int num5 = f2.Div(mIndexTimeStepF32, 16).ToInt(16);
			if (num4 != num5)
			{
				int num6 = (((num4 != 0 || num5 != 11) && (num4 != 11 || num5 != 0)) ? ((num4 < num5) ? num5 : num4) : 0);
				int at2 = mIndexToTimeArray.GetAt(num6 + 12);
				if (totalTime2 < at)
				{
					mObserver.PassThroughVariant(num6, 0, num3 - at2);
				}
				else
				{
					mObserver.PassThroughVariant(num6, 1, (num6 != 0) ? (at2 - num3) : (num3 - at2));
				}
			}
		}

		public virtual void SetObserver(HelixObserver observer)
		{
			mObserver = observer;
		}

		public virtual bool IsOnTarget()
		{
			return mSelector.GetSingleSelection() == mLastSelectionIdx;
		}

		public static int FindShorterPath(int lastIdx, int newIdx)
		{
			int result = 0;
			int num = newIdx - lastIdx;
			if (num != 0)
			{
				int num2 = FlMath.Absolute(num);
				int num3 = 12 - num2;
				result = ((num2 < num3) ? num : ((lastIdx > newIdx) ? num3 : (-num3)));
			}
			return result;
		}

		public static int LoopOnRange(int value, int minValue, int maxValue, int cycleLength)
		{
			while (value < minValue)
			{
				value += cycleLength;
			}
			while (value >= maxValue)
			{
				value -= cycleLength;
			}
			return value;
		}
	}
}
