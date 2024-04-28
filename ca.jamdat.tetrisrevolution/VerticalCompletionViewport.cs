using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VerticalCompletionViewport : Viewport
	{
		public const short numOpeningAnims = 7;

		public const short numClosingAnims = 7;

		public int mFullBarHeight;

		public int mExtendedBarHeight;

		public int mFillPadding;

		public VerticalText mTitleVerticalText;

		public VerticalText mPercentVerticalText;

		public ResizableFrame mBarBackgroundResizableFrame;

		public Viewport mBarBackgroundViewport;

		public Viewport mBarViewport;

		public VerticalCompletionViewport(Viewport vp)
		{
			FlFont flFont = EntryPoint.GetFlFont(3047517, 8);
			FlFont flFont2 = EntryPoint.GetFlFont(3047517, 7);
			SetViewport(vp);
			mTitleVerticalText = new VerticalText(0, 0, flFont2);
			mPercentVerticalText = new VerticalText(0, 0, flFont);
			mTitleVerticalText.SetCaption(EntryPoint.GetFlString(-2144075681, 71));
			FlString flString = new FlString(100);
			flString.AddAssign("%");
			mPercentVerticalText.SetCaption(flString);
			mTitleVerticalText.Initialize(this);
			mPercentVerticalText.Initialize(this);
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(1179684, 1);
			mBarBackgroundViewport = EntryPoint.GetViewport(1179684, 3);
			mBarViewport = EntryPoint.GetViewport(1179684, 2);
			mBarBackgroundViewport.SetViewport(this);
			mFullBarHeight = mBarViewport.GetRectHeight();
			mExtendedBarHeight = mFullBarHeight + 64;
			mFillPadding = flBitmapMap.GetSourceHeightAt(0);
			mBarBackgroundResizableFrame = new ResizableFrame(0, flBitmapMap, 0, 1, 2);
			mBarBackgroundResizableFrame.Initialize(mBarBackgroundViewport);
			mBarViewport.SetViewport(mBarBackgroundResizableFrame);
			mBarBackgroundViewport.SetVisible(false);
			AdjustRect();
			mTitleVerticalText.SetVisible(false);
			mPercentVerticalText.SetVisible(false);
			mBarViewport.SetVisible(false);
			mBarBackgroundResizableFrame.SetVisible(false);
			mPercentVerticalText.SetViewport(this);
			mClipChildren = true;
		}

		public override void destruct()
		{
		}

		public virtual void Unload()
		{
			UnRegisterInGlobalTime();
			mBarBackgroundResizableFrame.UnRegisterInGlobalTime();
			mTitleVerticalText.Unload();
			mPercentVerticalText.Unload();
			mBarViewport.SetViewport(null);
			mBarBackgroundResizableFrame.Unload();
			if (mTitleVerticalText != null)
			{
				mTitleVerticalText = null;
			}
			if (mPercentVerticalText != null)
			{
				mPercentVerticalText = null;
			}
			if (mBarBackgroundResizableFrame != null)
			{
				mBarBackgroundResizableFrame = null;
			}
			mBarBackgroundViewport.SetViewport(null);
			SetViewport(null);
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			UpdatePercentText();
			base.OnTime(totalTime, deltaTime);
		}

		public virtual void CreateOpeningAnims(int startTime, TimerSequence timerSequence)
		{
			KeyFrameController timeable = CreateExtendOpeningAnim();
			timerSequence.RegisterInterval(timeable, startTime, startTime + 300);
			timeable = CreateTitleOpeningAnim();
			timerSequence.RegisterInterval(timeable, startTime + 180, startTime + 180 + 150);
			timeable = CreateFillOpeningAnim();
			timerSequence.RegisterInterval(timeable, startTime + 180, startTime + 180 + 833);
			timeable = EntryPoint.GetKeyFrameController(1179684, 4);
			timeable.SetControllee(mTitleVerticalText);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 180);
			timeable = EntryPoint.GetKeyFrameController(1179684, 5);
			timeable.SetControllee(mPercentVerticalText);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 300);
			timeable = EntryPoint.GetKeyFrameController(1179684, 6);
			timeable.SetControllee(mBarViewport);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 180);
			timeable = EntryPoint.GetKeyFrameController(1179684, 7);
			timeable.SetControllee(mBarBackgroundResizableFrame);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 180);
			mBarViewport.SetVisible(false);
		}

		public virtual void CreateClosingAnims(int startTime, TimerSequence timerSequence)
		{
			KeyFrameController timeable = CreateExtendClosingAnim();
			timerSequence.RegisterInterval(timeable, startTime + 150, 450);
			timeable = CreateFillClosingAnim();
			timerSequence.RegisterInterval(timeable, startTime, 450);
			timeable = CreateTitleClosingAnim();
			timerSequence.RegisterInterval(timeable, startTime, 450);
			timeable = EntryPoint.GetKeyFrameController(1179684, 10);
			timeable.SetControllee(mBarViewport);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 150);
			timeable = EntryPoint.GetKeyFrameController(1179684, 8);
			timeable.SetControllee(mPercentVerticalText);
			timerSequence.RegisterInterval(timeable, startTime, startTime + 150 + 300);
			timeable = EntryPoint.GetKeyFrameController(1179684, 9);
			timeable.SetControllee(mBarBackgroundResizableFrame);
			timerSequence.RegisterInterval(timeable, startTime + 150 + 300, startTime + 150 + 300 + 333);
		}

		public virtual void AdjustRect()
		{
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			FlString flString = new FlString(progressionExpert.GetGameCompletion());
			flString.AddAssign("%");
			mPercentVerticalText.SetCaption(flString);
			int rectHeight = mTitleVerticalText.GetRectHeight();
			int gameCompletion = progressionExpert.GetGameCompletion();
			int num = (mFullBarHeight - mFillPadding * 2) * gameCompletion / 100;
			int num2 = num + mFillPadding;
			if (gameCompletion >= 100)
			{
				num2 = mFullBarHeight;
			}
			else if (gameCompletion < 1)
			{
				num2 = 0;
			}
			int num3 = FlMath.Maximum(mFullBarHeight, rectHeight);
			int num4 = mTitleVerticalText.GetRectWidth() + mBarBackgroundResizableFrame.GetRectWidth();
			int rectWidth = mPercentVerticalText.GetRectWidth();
			int rectHeight2 = mPercentVerticalText.GetRectHeight();
			int num5 = 14;
			mTitleVerticalText.SetViewport(this);
			mBarBackgroundResizableFrame.SetViewport(this);
			mPercentVerticalText.SetViewport(this);
			SetSize((short)FlMath.Maximum(rectWidth, num4), (short)(num3 + rectHeight2 + num5));
			SetTopLeft((short)(480 - num4 - 5), (short)(676 - num3 - rectHeight2));
			if (rectWidth < num4)
			{
				mPercentVerticalText.SetX((num4 - rectWidth) / 2);
				mPercentVerticalText.SetY(rectHeight2);
			}
			else
			{
				mPercentVerticalText.SetY(rectHeight2);
				mPercentVerticalText.SetX(0);
			}
			mPercentVerticalText.AdjustRect();
			if (mFullBarHeight > rectHeight)
			{
				mTitleVerticalText.SetY(rectHeight2 + num5 + (mFullBarHeight - rectHeight) / 2 + rectHeight);
				mBarBackgroundResizableFrame.SetTopLeft(mTitleVerticalText.GetRectWidth(), (short)(rectHeight2 + num5));
			}
			else
			{
				mTitleVerticalText.SetY(mRect_height);
				mBarBackgroundResizableFrame.SetTopLeft(mTitleVerticalText.GetRectWidth(), (short)(num5 + rectHeight2 + (rectHeight - mFullBarHeight) / 2));
			}
			mTitleVerticalText.AdjustRect();
			mBarBackgroundResizableFrame.SetSize(mBarBackgroundResizableFrame.GetRectWidth(), (short)mFullBarHeight);
			mBarViewport.SetSize(mBarViewport.GetRectWidth(), (short)num2);
			mBarViewport.SetTopLeft(mBarViewport.GetRectLeft(), (short)(mBarBackgroundResizableFrame.GetRectHeight() - mBarViewport.GetRectHeight()));
			Component child = mBarViewport.GetChild(0);
			child.SetTopLeft(0, (short)(num2 - mFullBarHeight));
			mBarBackgroundResizableFrame.SetClipChildren(false);
		}

		public virtual KeyFrameController CreateFillOpeningAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mBarViewport);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(4, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(14);
			int rectHeight = mBarViewport.GetRectHeight();
			int[] array = new int[4]
			{
				mBarViewport.GetRectLeft(),
				mExtendedBarHeight,
				mBarViewport.GetRectWidth(),
				0
			};
			keyFrameSequence.SetKeyFrame(0, 0, array);
			int num = rectHeight * 8 / 10;
			array[1] = mFullBarHeight - num;
			array[3] = num;
			keyFrameSequence.SetKeyFrame(1, 333, array);
			num = rectHeight * 95 / 100;
			array[1] = mFullBarHeight - num;
			array[3] = num;
			keyFrameSequence.SetKeyFrame(2, 541, array);
			array[1] = mFullBarHeight - rectHeight;
			array[3] = rectHeight;
			keyFrameSequence.SetKeyFrame(3, 833, array);
			return keyFrameController;
		}

		public virtual KeyFrameController CreateExtendOpeningAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mBarBackgroundResizableFrame);
			mBarBackgroundResizableFrame.SetClipChildren(true);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(4, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int absoluteLeft = mBarBackgroundResizableFrame.GetAbsoluteLeft();
			int absoluteTop = mBarBackgroundResizableFrame.GetAbsoluteTop();
			int rectHeight = mBarBackgroundResizableFrame.GetRectHeight();
			mBarBackgroundResizableFrame.SetViewport(GetViewport());
			int[] array = new int[4]
			{
				absoluteLeft,
				absoluteTop + rectHeight / 2 - 10,
				mBarBackgroundResizableFrame.GetRectWidth(),
				20
			};
			keyFrameSequence.SetKeyFrame(0, 0, array);
			int num = mExtendedBarHeight / 3;
			array[1] = absoluteTop + rectHeight / 2 - num / 2;
			array[3] = num;
			keyFrameSequence.SetKeyFrame(1, 150, array);
			array[1] = absoluteTop - 32;
			array[3] = mExtendedBarHeight;
			keyFrameSequence.SetKeyFrame(2, 180, array);
			array[1] = absoluteTop;
			array[3] = mFullBarHeight;
			keyFrameSequence.SetKeyFrame(3, 300, array);
			return keyFrameController;
		}

		public virtual KeyFrameController CreateTitleOpeningAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mTitleVerticalText);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int[] array = new int[4];
			int absoluteLeft = mTitleVerticalText.GetAbsoluteLeft();
			int absoluteTop = mTitleVerticalText.GetAbsoluteTop();
			mTitleVerticalText.SetViewport(GetViewport());
			int rectHeight = mTitleVerticalText.GetRectHeight();
			array[0] = absoluteLeft + mTitleVerticalText.GetRectWidth();
			array[1] = absoluteTop;
			array[2] = 0;
			array[3] = rectHeight;
			keyFrameSequence.SetKeyFrame(0, 0, array);
			array[0] = absoluteLeft + mTitleVerticalText.GetRectWidth() - 12;
			array[2] = 12;
			keyFrameSequence.SetKeyFrame(1, 60, array);
			array[0] = absoluteLeft;
			array[2] = mTitleVerticalText.GetRectWidth();
			keyFrameSequence.SetKeyFrame(2, 150, array);
			return keyFrameController;
		}

		public virtual KeyFrameController CreateFillClosingAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mBarViewport);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(14);
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			int num = mFullBarHeight * progressionExpert.GetGameCompletion() / 100;
			int[] array = new int[4]
			{
				mBarViewport.GetRectLeft(),
				mFullBarHeight - num,
				mBarViewport.GetRectWidth(),
				num
			};
			keyFrameSequence.SetKeyFrame(0, 0, array);
			int num2 = num * 8 / 10;
			array[1] = mFullBarHeight - num2;
			array[3] = num2;
			keyFrameSequence.SetKeyFrame(1, 60, array);
			array[1] = mExtendedBarHeight;
			array[3] = 0;
			keyFrameSequence.SetKeyFrame(2, 150, array);
			return keyFrameController;
		}

		public virtual KeyFrameController CreateExtendClosingAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mBarBackgroundResizableFrame);
			mBarBackgroundResizableFrame.SetClipChildren(true);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(4, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int num = FlMath.Maximum((short)mExtendedBarHeight, mTitleVerticalText.GetRectHeight());
			int[] array = new int[4]
			{
				mBarBackgroundResizableFrame.GetRectLeft(),
				mRect_height - num + (num - mFullBarHeight) / 2,
				mBarBackgroundResizableFrame.GetRectWidth(),
				mFullBarHeight
			};
			keyFrameSequence.SetKeyFrame(0, 0, array);
			int num2 = mExtendedBarHeight / 3;
			if (mExtendedBarHeight == num)
			{
				array[1] = mRect_height - mExtendedBarHeight;
			}
			else
			{
				array[1] = mRect_height - mExtendedBarHeight - (num - mExtendedBarHeight);
			}
			array[3] = mExtendedBarHeight;
			keyFrameSequence.SetKeyFrame(1, 90, array);
			array[1] = mRect_height - num / 2 - num2 / 2;
			array[3] = num2;
			keyFrameSequence.SetKeyFrame(2, 180, array);
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(1179684, 1);
			int num3 = flBitmapMap.GetSourceHeightAt(0) + flBitmapMap.GetSourceHeightAt(1) + flBitmapMap.GetSourceHeightAt(2);
			array[1] = mRect_height - num / 2;
			array[3] = num3;
			keyFrameSequence.SetKeyFrame(3, 300, array);
			return keyFrameController;
		}

		public virtual KeyFrameController CreateTitleClosingAnim()
		{
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mTitleVerticalText);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int[] array = new int[4];
			int rectHeight = mTitleVerticalText.GetRectHeight();
			array[0] = 0;
			if (mFullBarHeight < rectHeight)
			{
				array[1] = mRect_height - rectHeight;
			}
			else
			{
				array[1] = mBarBackgroundResizableFrame.GetRectTop() + (mFullBarHeight - rectHeight) / 2;
			}
			array[2] = mTitleVerticalText.GetRectWidth();
			array[3] = rectHeight;
			keyFrameSequence.SetKeyFrame(0, 0, array);
			array[0] = mTitleVerticalText.GetRectWidth() - 12;
			array[2] = 12;
			keyFrameSequence.SetKeyFrame(1, 60, array);
			array[0] = mTitleVerticalText.GetRectWidth();
			array[2] = 0;
			keyFrameSequence.SetKeyFrame(2, 150, array);
			return keyFrameController;
		}

		public virtual void UpdatePercentText()
		{
			int a = (mBarViewport.GetRectHeight() - mFillPadding) * 100 / mFullBarHeight;
			a = FlMath.Maximum(a, 0);
			FlString flString = new FlString(a);
			flString.AddAssign("%");
			mPercentVerticalText.SetCaption(flString);
		}
	}
}
