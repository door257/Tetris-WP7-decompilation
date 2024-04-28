using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Ticker : Viewport
	{
		public const int movementLoopLeftRight = 0;

		public const int movementLoopTopDown = 1;

		public const int movementLeftRight = 2;

		public const int movementTopDown = 3;

		public const int stateIdle = 0;

		public const int statePaused = 1;

		public const int stateTicking = 2;

		public int mState;

		public int mTickMovement;

		public Component mTickingComponent;

		public int mPos1x;

		public int mPos2x;

		public int mPos1y;

		public int mPos2y;

		public int mPauseTimeMs;

		public int mPauseTimeElapsed;

		public int mTickPixelPerSeconds;

		public int mDeltaTimeElapsed;

		public bool mTickingRequired;

		public bool mTickingForced;

		public bool mTickingUserForced;

		public bool mGoingForward;

		public bool mInitialWait;

		public short mInitialTickingComponentLeftPosition;

		public short mInitialTickingComponentTopPosition;

		public int mTickingStartPositionOffset;

		public Ticker()
		{
			mState = 0;
			mTickMovement = 0;
			mDeltaTimeElapsed = 50;
			mGoingForward = true;
			mInitialWait = true;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(Component tickingComponent, Component clippingViewport, int tickPixelPerSeconds, int tickMovement, int pauseTimeMs, int tickingStartPositionOffset, bool forceTicking)
		{
			int rectWidth = tickingComponent.GetRectWidth();
			int rectHeight = tickingComponent.GetRectHeight();
			int rectWidth2 = clippingViewport.GetRectWidth();
			int rectHeight2 = clippingViewport.GetRectHeight();
			mInitialTickingComponentLeftPosition = tickingComponent.GetRectLeft();
			mInitialTickingComponentTopPosition = tickingComponent.GetAbsoluteTop();
			mTickMovement = tickMovement;
			mPauseTimeMs = pauseTimeMs;
			mTickPixelPerSeconds = tickPixelPerSeconds;
			mTickingUserForced = forceTicking;
			mTickingStartPositionOffset = tickingStartPositionOffset;
			SetSize((short)rectWidth2, (short)rectHeight2);
			SetTopLeft(clippingViewport.GetRectLeft(), clippingViewport.GetRectTop());
			SetClipChildren(true);
			mTickingComponent = tickingComponent;
			CheckIfTickingForced();
			if (mTickingForced)
			{
				mGoingForward = false;
			}
			switch (tickMovement)
			{
			case 0:
				mPos2x = -(rectWidth + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds));
				break;
			case 1:
				mPos2y = -(rectHeight + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds));
				break;
			}
			PositionTickingComponent();
		}

		public virtual void Start()
		{
			if (mInitialWait)
			{
				mState = 1;
			}
			else
			{
				mState = 2;
			}
			RegisterInGlobalTime();
		}

		public virtual void Stop()
		{
			mState = 0;
			UnRegisterInGlobalTime();
		}

		public virtual void Reset()
		{
			mGoingForward = !mTickingForced;
			mInitialWait = true;
			mPos1x = 0;
			mPos1y = 0;
			if (mTickMovement == 0)
			{
				mPos2x = -(mTickingComponent.GetRectWidth() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds));
			}
			else if (mTickMovement == 1)
			{
				mPos2y = -(mTickingComponent.GetRectHeight() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds));
			}
			Invalidate();
		}

		public virtual bool IsTicking()
		{
			return mState != 0;
		}

		public virtual bool IsTickingRequired()
		{
			return mTickingRequired;
		}

		public virtual void OnChildUpdated()
		{
			CheckIfTickingForced();
			PositionTickingComponent();
			if (mTickingRequired)
			{
				Reset();
			}
			Invalidate();
		}

		public virtual void ChangeInitialTickingComponentLeftPosition(short tickingComponentLeftPosition)
		{
			mInitialTickingComponentLeftPosition = tickingComponentLeftPosition;
		}

		public virtual void ChangeInitialTickingComponentTopPosition(short tickingComponentTopPosition)
		{
			mInitialTickingComponentTopPosition = tickingComponentTopPosition;
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			short clippingRectLeft = displayContext.GetClippingRectLeft();
			short clippingRectTop = displayContext.GetClippingRectTop();
			short clippingRectWidth = displayContext.GetClippingRectWidth();
			short clippingRectHeight = displayContext.GetClippingRectHeight();
			short num = GetRectLeft();
			short num2 = GetRectTop();
			short rectWidth = GetRectWidth();
			short rectHeight = GetRectHeight();
			int num3 = clippingRectLeft + clippingRectWidth;
			int num4 = num + rectWidth;
			int num5 = clippingRectTop + clippingRectHeight;
			int num6 = num2 + rectHeight;
			if (clippingRectLeft > num)
			{
				num = clippingRectLeft;
			}
			if (clippingRectTop > num2)
			{
				num2 = clippingRectTop;
			}
			if (num3 < num4)
			{
				num4 = num3;
			}
			if (num5 < num6)
			{
				num6 = num5;
			}
			rectWidth = (short)(num4 - num);
			rectHeight = (short)(num6 - num2);
			if (rectWidth <= 0 || rectHeight <= 0)
			{
				return;
			}
			displayContext.SetClippingRect(num, num2, rectWidth, rectHeight);
			if (mTickingRequired)
			{
				displayContext.OffsetBy((short)(-mPos1x), (short)(-mPos1y));
				mTickingComponent.OnDraw(displayContext);
				displayContext.OffsetBy((short)mPos1x, (short)mPos1y);
				if (mTickMovement == 0 || mTickMovement == 1)
				{
					displayContext.OffsetBy((short)(-mPos2x), (short)(-mPos2y));
					mTickingComponent.OnDraw(displayContext);
					displayContext.OffsetBy((short)mPos2x, (short)mPos2y);
				}
			}
			else
			{
				displayContext.OffsetBy(GetRectLeft(), 0);
				mTickingComponent.OnDraw(displayContext);
				displayContext.OffsetBy((short)(-GetRectLeft()), 0);
			}
			displayContext.SetClippingRect(clippingRectLeft, clippingRectTop, clippingRectWidth, clippingRectHeight);
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (!IsTicking() || !mTickingRequired)
			{
				return;
			}
			if (mState == 2)
			{
				if (mDeltaTimeElapsed >= 50)
				{
					int offsetTicker = CalculateTickingDistance(mDeltaTimeElapsed, mTickPixelPerSeconds);
					if (mTickMovement == 0)
					{
						CalculateTickerPositionsForLoopLeftRight(offsetTicker);
					}
					else if (mTickMovement == 2)
					{
						CalculateTickerPositionsForLeftRight(offsetTicker);
					}
					else if (mTickMovement == 1)
					{
						CalculateTickerPositionsForLoopTopDown(offsetTicker);
					}
					else if (mTickMovement == 3)
					{
						CalculateTickerPositionsForTopDown(offsetTicker);
					}
					mDeltaTimeElapsed = 0;
					Invalidate();
				}
				else
				{
					mDeltaTimeElapsed += deltaTimeMs;
				}
			}
			else
			{
				if (mState != 1)
				{
					return;
				}
				mPauseTimeElapsed += deltaTimeMs;
				if (mPauseTimeElapsed >= mPauseTimeMs)
				{
					mPauseTimeElapsed = 0;
					mState = 2;
					if (mInitialWait)
					{
						mInitialWait = false;
					}
					else
					{
						mGoingForward = !mGoingForward;
					}
				}
			}
		}

		public virtual int CalculateTickingDistance(int timeMs, int tickPixelPerSeconds)
		{
			return timeMs * tickPixelPerSeconds / 1000;
		}

		public virtual void CalculateTickerPositionsForLoopLeftRight(int offsetTicker)
		{
			mPos1x += offsetTicker;
			mPos2x += offsetTicker;
			if (mPos1x > 2 * (mTickingComponent.GetRectWidth() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds)) - (GetRectWidth() - mTickingStartPositionOffset))
			{
				mPos1x = -(GetRectWidth() - mTickingStartPositionOffset);
			}
			if (mPos2x > 2 * (mTickingComponent.GetRectWidth() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds)) - (GetRectWidth() - mTickingStartPositionOffset))
			{
				mPos2x = -(GetRectWidth() - mTickingStartPositionOffset);
			}
		}

		public virtual void CalculateTickerPositionsForLeftRight(int offsetTicker)
		{
			int num = 0;
			if (mGoingForward)
			{
				mPos1x += offsetTicker;
				if (!mTickingForced)
				{
					num = mTickingComponent.GetRectWidth() - GetRectWidth();
				}
				if (mPos1x >= num)
				{
					mPos1x = num;
					mState = 1;
				}
			}
			else
			{
				mPos1x -= offsetTicker;
				if (mTickingForced)
				{
					num = -GetRectWidth() + mTickingComponent.GetRectWidth();
				}
				if (mPos1x <= num)
				{
					mPos1x = num;
					mState = 1;
				}
			}
		}

		public virtual void CalculateTickerPositionsForLoopTopDown(int offsetTicker)
		{
			mPos1y += offsetTicker;
			mPos2y += offsetTicker;
			if (mPos1y > 2 * (mTickingComponent.GetRectHeight() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds)) - (GetRectHeight() - mTickingStartPositionOffset))
			{
				mPos1y = -(GetRectHeight() - mTickingStartPositionOffset);
			}
			if (mPos2y > 2 * (mTickingComponent.GetRectHeight() + CalculateTickingDistance(mPauseTimeMs, mTickPixelPerSeconds)) - (GetRectHeight() - mTickingStartPositionOffset))
			{
				mPos2y = -(GetRectHeight() - mTickingStartPositionOffset);
			}
		}

		public virtual void CalculateTickerPositionsForTopDown(int offsetTicker)
		{
			int num = 0;
			if (mGoingForward)
			{
				mPos1y += offsetTicker;
				if (!mTickingForced)
				{
					num = mTickingComponent.GetRectHeight() - GetRectHeight();
				}
				if (mPos1y >= num)
				{
					mPos1y = num;
					mState = 1;
				}
			}
			else
			{
				mPos1y -= offsetTicker;
				if (mTickingForced)
				{
					num = -GetRectHeight() + mTickingComponent.GetRectHeight();
				}
				if (mPos1y <= num)
				{
					mPos1y = num;
					mState = 1;
				}
			}
		}

		public virtual void PositionTickingComponent()
		{
			if (mTickingRequired)
			{
				if (mTickMovement == 0)
				{
					mTickingComponent.SetTopLeft((short)(GetRectLeft() + mTickingStartPositionOffset), mInitialTickingComponentTopPosition);
				}
				else if (mTickMovement == 2 || mTickMovement == 3)
				{
					mTickingComponent.SetTopLeft(GetRectLeft(), mInitialTickingComponentTopPosition);
				}
				else if (mTickMovement == 1)
				{
					mTickingComponent.SetTopLeft(GetRectLeft(), (short)(GetRectTop() + mTickingStartPositionOffset));
				}
			}
			else
			{
				mTickingComponent.SetTopLeft(mInitialTickingComponentLeftPosition, mInitialTickingComponentTopPosition);
			}
		}

		public virtual void CheckIfTickingForced()
		{
			int rectWidth = mTickingComponent.GetRectWidth();
			int rectHeight = mTickingComponent.GetRectHeight();
			if (mTickMovement == 0 || mTickMovement == 2)
			{
				mTickingForced = mTickingUserForced && rectWidth + mTickingComponent.GetAbsoluteLeft() <= GetRectWidth() + GetAbsoluteLeft();
				mTickingRequired = rectWidth + mTickingComponent.GetAbsoluteLeft() > GetRectWidth() + GetAbsoluteLeft() || mTickingUserForced;
			}
			else
			{
				mTickingForced = mTickingUserForced && rectHeight - mInitialTickingComponentTopPosition <= GetRectHeight() - GetAbsoluteTop();
				mTickingRequired = rectHeight - mInitialTickingComponentTopPosition > GetRectHeight() - GetAbsoluteTop() || mTickingUserForced;
			}
		}

		public virtual void Initialize(Component tickingComponent, Component clippingViewport, int tickPixelPerSeconds, int tickMovement, int pauseTimeMs, int tickingStartPositionOffset)
		{
			Initialize(tickingComponent, clippingViewport, tickPixelPerSeconds, tickMovement, pauseTimeMs, tickingStartPositionOffset, false);
		}

		public static Ticker[] InstArrayTicker(int size)
		{
			Ticker[] array = new Ticker[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Ticker();
			}
			return array;
		}

		public static Ticker[][] InstArrayTicker(int size1, int size2)
		{
			Ticker[][] array = new Ticker[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Ticker[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Ticker();
				}
			}
			return array;
		}

		public static Ticker[][][] InstArrayTicker(int size1, int size2, int size3)
		{
			Ticker[][][] array = new Ticker[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Ticker[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Ticker[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Ticker();
					}
				}
			}
			return array;
		}
	}
}
