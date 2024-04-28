using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Animation
	{
		public const sbyte animIdNone = -1;

		public const sbyte firstCommonAnim = 0;

		public const sbyte commonAnimCount = 1;

		public const sbyte popupAnim = 1;

		public const sbyte menuTransitionAnim = 2;

		public const sbyte tmpCommonRuntimeAnimCount = 3;

		public const sbyte commonRuntimeAnimCount = 2;

		public const sbyte firstMenuAnim = 3;

		public const sbyte tmpMenuAnimCount = 4;

		public const sbyte menuAnimCount = 1;

		public const sbyte eaSplashAnim = 4;

		public const sbyte firstGameAnim = 4;

		public const sbyte countdownAnim = 5;

		public const sbyte nextHudUpdateAnim = 6;

		public const sbyte tSpinLockAnim = 7;

		public const sbyte tSpinFeedbackAnim = 8;

		public const sbyte gameSceneConstructionAnim = 9;

		public const sbyte replayNoticeAnim = 10;

		public const sbyte tmpGameAnimCount = 11;

		public const sbyte gameAnimCount = 7;

		public const sbyte firstCustomGameSceneAnim = 10;

		public const sbyte lineClearAnim = 11;

		public const sbyte eventBarIndicatorVanishAnim = 12;

		public const sbyte eventBarIndicatorBlinkAnim = 13;

		public const sbyte modeLimboBarEffectAnim = 14;

		public const sbyte modeMagneticMagnetAnim = 15;

		public const sbyte modeScannerMoveAnim = 16;

		public const sbyte modeScannerAspectSwitchAnim = 17;

		public const sbyte modeChillMoveAnim = 18;

		public const sbyte modeChillAspectSwitchAnim = 19;

		public const sbyte tetriminoLockdownAnim = 20;

		public const sbyte showSuccessFailureAnim = 21;

		public const sbyte minoReboundAnim = 22;

		public const sbyte wellReboundAnim = 23;

		public const sbyte totalAnimCount = 24;

		public TimerSequence mTimerSequence;

		public int mDuration;

		public virtual void destruct()
		{
		}

		public virtual void SetTimerSequence(TimerSequence ts)
		{
			mTimerSequence = ts;
		}

		public virtual void SetDuration(int duration)
		{
			mDuration = duration;
		}

		public virtual TimerSequence GetTimerSequence()
		{
			return mTimerSequence;
		}

		public virtual int GetDuration()
		{
			return mDuration;
		}

		public static Animation[] InstArrayAnimation(int size)
		{
			Animation[] array = new Animation[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Animation();
			}
			return array;
		}

		public static Animation[][] InstArrayAnimation(int size1, int size2)
		{
			Animation[][] array = new Animation[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Animation[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Animation();
				}
			}
			return array;
		}

		public static Animation[][][] InstArrayAnimation(int size1, int size2, int size3)
		{
			Animation[][][] array = new Animation[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Animation[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Animation[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Animation();
					}
				}
			}
			return array;
		}
	}
}
