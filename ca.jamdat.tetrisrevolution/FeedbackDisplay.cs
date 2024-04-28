using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FeedbackDisplay : Viewport
	{
		public int mCurrentFeedbackDisplayIdx;

		public TimerSequence mFeedbackDisplayAnim;

		public Text[] mFeedbackText;

		public Viewport[] mFeedbackViewport;

		public int mHideShapeIdx;

		public int mShowShapeIdx;

		public int mHideControllerIdx;

		public int mShowControllerIdx;

		public TimerSequence mCommonTimerSequence;

		public KeyFrameController mCommonPositionController;

		public KeyFrameController mCommonVisibilityController;

		public KeyFrameController mCommonOpacityController;

		public Shape[] mFeedbackShowShape;

		public Shape[] mFeedbackHideShape;

		public KeyFrameController[] mFeedbackHideViewportController;

		public KeyFrameController[] mFeedbackHideTextController;

		public KeyFrameController[] mFeedbackHideShapeController;

		public KeyFrameController[] mFeedbackShowViewportController;

		public KeyFrameController[] mFeedbackShowShapeController1;

		public KeyFrameController[] mFeedbackShowShapeController2;

		public FeedbackDisplay(LayerComponent layerComponent)
		{
			mCurrentFeedbackDisplayIdx = -1;
			SetViewport(layerComponent.GetLayer(8));
			SetRect(3, 248, 311, 90);
			SetClipChildren(true);
			InitializeComponents();
		}

		public override void destruct()
		{
		}

		public virtual void Unload()
		{
			UnRegisterInGlobalTime();
			SetViewport(null);
			if (mFeedbackDisplayAnim != null)
			{
				CleanFeedbackDisplayAnim();
				mFeedbackDisplayAnim = null;
			}
			if (mCommonTimerSequence != null)
			{
				mCommonTimerSequence.UnRegisterAll();
				mCommonTimerSequence = null;
			}
			if (mCommonPositionController != null)
			{
				mCommonPositionController = null;
			}
			if (mCommonVisibilityController != null)
			{
				mCommonVisibilityController = null;
			}
			if (mCommonOpacityController != null)
			{
				mCommonOpacityController = null;
			}
			for (int i = 0; i < 7; i++)
			{
				if (mFeedbackHideShape[i] != null)
				{
					mFeedbackHideShape[i].SetViewport(null);
					mFeedbackHideShape[i] = null;
				}
				if (mFeedbackShowShape[i] != null)
				{
					mFeedbackShowShape[i].SetViewport(null);
					mFeedbackShowShape[i] = null;
				}
				if (mFeedbackHideViewportController[i] != null)
				{
					mFeedbackHideViewportController[i] = null;
				}
				if (mFeedbackHideTextController[i] != null)
				{
					mFeedbackHideTextController[i] = null;
				}
				if (mFeedbackHideShapeController[i] != null)
				{
					mFeedbackHideShapeController[i] = null;
				}
				if (mFeedbackShowViewportController[i] != null)
				{
					mFeedbackShowViewportController[i] = null;
				}
				if (mFeedbackShowShapeController1[i] != null)
				{
					mFeedbackShowShapeController1[i] = null;
				}
				if (mFeedbackShowShapeController2[i] != null)
				{
					mFeedbackShowShapeController2[i] = null;
				}
			}
			mFeedbackShowShape = null;
			mFeedbackHideShape = null;
			mFeedbackHideViewportController = null;
			mFeedbackHideTextController = null;
			mFeedbackHideShapeController = null;
			mFeedbackShowViewportController = null;
			mFeedbackShowShapeController1 = null;
			mFeedbackShowShapeController2 = null;
			for (int j = 0; j < 7; j++)
			{
				if (mFeedbackText[j] != null)
				{
					mFeedbackText[j].SetViewport(null);
					mFeedbackText[j] = null;
				}
				if (mFeedbackViewport[j] != null)
				{
					mFeedbackViewport[j].SetViewport(null);
					mFeedbackViewport[j] = null;
				}
			}
			mFeedbackText = null;
			mFeedbackViewport = null;
		}

		public virtual void PrepareFeedbackDisplay(int feedbackType, GameScore gameScore)
		{
			SetupFeedbackDisplay(99 + feedbackType, gameScore);
		}

		public virtual void DisplayFeedback()
		{
			if (mCurrentFeedbackDisplayIdx != -1)
			{
				CreateAndRegisterUniqueControllers();
				SetTopLeft(3, 248);
				mFeedbackDisplayAnim.SetTotalTime(0);
				mCommonTimerSequence.SetTotalTime(0);
				mFeedbackDisplayAnim.RegisterInGlobalTime();
				RegisterInGlobalTime();
			}
		}

		public virtual void CleanFeedbackDisplayAnim()
		{
			mFeedbackDisplayAnim.UnRegisterInGlobalTime();
			mFeedbackDisplayAnim.UnRegisterAll();
			while (mCurrentFeedbackDisplayIdx >= 0)
			{
				if (mFeedbackViewport[mCurrentFeedbackDisplayIdx] != null)
				{
					mFeedbackViewport[mCurrentFeedbackDisplayIdx].SetViewport(null);
				}
				mCurrentFeedbackDisplayIdx--;
			}
			mHideControllerIdx = 0;
			mShowControllerIdx = 0;
			mHideShapeIdx = 0;
			mShowShapeIdx = 0;
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			if (IsAnimationOver())
			{
				CleanFeedbackDisplayAnim();
				UnRegisterInGlobalTime();
			}
		}

		public virtual bool IsAnimationOver()
		{
			if (mFeedbackDisplayAnim.IsRegisteredInGlobalTime())
			{
				return mFeedbackDisplayAnim.GetTotalTime() >= 2500;
			}
			return false;
		}

		public virtual bool IsAnimationPlaying()
		{
			int totalTime = mFeedbackDisplayAnim.GetTotalTime();
			if (mFeedbackDisplayAnim.IsRegisteredInGlobalTime() && totalTime > 0)
			{
				return totalTime < 2500;
			}
			return false;
		}

		public virtual void InitializeComponents()
		{
			mFeedbackViewport = new Viewport[7];
			for (int i = 0; i < mFeedbackViewport.Length; i++)
			{
				mFeedbackViewport[i] = null;
			}
			mFeedbackText = new Text[7];
			for (int j = 0; j < mFeedbackText.Length; j++)
			{
				mFeedbackText[j] = null;
			}
			mFeedbackDisplayAnim = new TimerSequence(43);
			CreateAndRegisterCommonControllers();
			mFeedbackShowShape = new Shape[7];
			for (int k = 0; k < mFeedbackShowShape.Length; k++)
			{
				mFeedbackShowShape[k] = null;
			}
			mFeedbackHideShape = new Shape[7];
			for (int l = 0; l < mFeedbackHideShape.Length; l++)
			{
				mFeedbackHideShape[l] = null;
			}
			mFeedbackHideViewportController = new KeyFrameController[7];
			for (int m = 0; m < mFeedbackHideViewportController.Length; m++)
			{
				mFeedbackHideViewportController[m] = null;
			}
			mFeedbackHideTextController = new KeyFrameController[7];
			for (int n = 0; n < mFeedbackHideTextController.Length; n++)
			{
				mFeedbackHideTextController[n] = null;
			}
			mFeedbackHideShapeController = new KeyFrameController[7];
			for (int num = 0; num < mFeedbackHideShapeController.Length; num++)
			{
				mFeedbackHideShapeController[num] = null;
			}
			mFeedbackShowViewportController = new KeyFrameController[7];
			for (int num2 = 0; num2 < mFeedbackShowViewportController.Length; num2++)
			{
				mFeedbackShowViewportController[num2] = null;
			}
			mFeedbackShowShapeController1 = new KeyFrameController[7];
			for (int num3 = 0; num3 < mFeedbackShowShapeController1.Length; num3++)
			{
				mFeedbackShowShapeController1[num3] = null;
			}
			mFeedbackShowShapeController2 = new KeyFrameController[7];
			for (int num4 = 0; num4 < mFeedbackShowShapeController2.Length; num4++)
			{
				mFeedbackShowShapeController2[num4] = null;
			}
		}

		public virtual void SetupFeedbackDisplay(int stringEntryPoint, GameScore gameScore)
		{
			CreateFeedbackDisplayComponents();
			FlString flString = new FlString();
			flString.AddAssign(EntryPoint.GetFlString(1867833, stringEntryPoint));
			if ((stringEntryPoint == 105 || stringEntryPoint == 110) && gameScore.GetGame().GetClearedLineCount() >= 5)
			{
				flString.AddAssign(" +");
			}
			mFeedbackText[mCurrentFeedbackDisplayIdx].SetCaption(flString);
			AddFeedbackDisplayBarShape();
			SetStartingVisibility();
		}

		public virtual void CreateFeedbackDisplayComponents()
		{
			mCurrentFeedbackDisplayIdx++;
			if (mCurrentFeedbackDisplayIdx < 7)
			{
				if (mFeedbackViewport[mCurrentFeedbackDisplayIdx] == null)
				{
					mFeedbackViewport[mCurrentFeedbackDisplayIdx] = new Viewport();
					mFeedbackViewport[mCurrentFeedbackDisplayIdx].SetClipChildren(true);
				}
				mFeedbackViewport[mCurrentFeedbackDisplayIdx].SetViewport(this);
				mFeedbackViewport[mCurrentFeedbackDisplayIdx].SetSize(GetDisplayViewportSize());
				if (mFeedbackText[mCurrentFeedbackDisplayIdx] == null)
				{
					FlFont flFont = EntryPoint.GetFlFont(3047517, 21);
					mFeedbackText[mCurrentFeedbackDisplayIdx] = new Text();
					mFeedbackText[mCurrentFeedbackDisplayIdx].SetFont(flFont);
					mFeedbackText[mCurrentFeedbackDisplayIdx].SetAlignment(1);
					mFeedbackText[mCurrentFeedbackDisplayIdx].SetSize(311, 90);
					mFeedbackText[mCurrentFeedbackDisplayIdx].SetMultiline(true);
					mFeedbackText[mCurrentFeedbackDisplayIdx].SetViewport(mFeedbackViewport[mCurrentFeedbackDisplayIdx]);
				}
				mFeedbackViewport[mCurrentFeedbackDisplayIdx].SetTopLeft(0, 0);
				mFeedbackText[mCurrentFeedbackDisplayIdx].SetTopLeft(0, 0);
			}
			else
			{
				mCurrentFeedbackDisplayIdx--;
			}
		}

		public virtual void AddFeedbackDisplayBarShape()
		{
			if (mCurrentFeedbackDisplayIdx > 0)
			{
				int idx = mCurrentFeedbackDisplayIdx;
				int idx2 = mCurrentFeedbackDisplayIdx - 1;
				CreateFeedbackDisplayShowShape(idx);
				CreateFeedbackDisplayHideShape(idx2);
			}
		}

		public virtual void CreateFeedbackDisplayHideShape(int idx)
		{
			if (mFeedbackHideShape[mHideShapeIdx] == null)
			{
				mFeedbackHideShape[mHideShapeIdx] = new Shape();
			}
			SetupFeedbackDisplayShape(mFeedbackHideShape[mHideShapeIdx], idx);
			mHideShapeIdx++;
		}

		public virtual void CreateFeedbackDisplayShowShape(int idx)
		{
			if (mFeedbackShowShape[mShowShapeIdx] == null)
			{
				mFeedbackShowShape[mShowShapeIdx] = new Shape();
			}
			SetupFeedbackDisplayShape(mFeedbackShowShape[mShowShapeIdx], idx);
			mShowShapeIdx++;
		}

		public virtual void SetupFeedbackDisplayShape(Shape displayShape, int idx)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			Color888 flColor = EntryPoint.GetFlColor(preLoadedPackage, 17);
			displayShape.SetViewport(mFeedbackViewport[idx]);
			displayShape.SetColor(flColor);
			short lineWidth = mFeedbackText[idx].GetLineWidth(0);
			short num = lineWidth;
			if (mFeedbackText[idx].GetNbLines() > 1)
			{
				short lineWidth2 = mFeedbackText[idx].GetLineWidth(1);
				if (lineWidth2 > lineWidth)
				{
					num = lineWidth2;
				}
			}
			short rect_left = (short)((311 - num) / 2);
			displayShape.SetRect(rect_left, 0, num, 2);
			displayShape.SetVisible(false);
		}

		public virtual void CreateAndRegisterCommonControllers()
		{
			mCommonTimerSequence = new TimerSequence(3);
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 18);
			mCommonPositionController = new KeyFrameController();
			mCommonPositionController.SetKeySequence(keyFrameSequence);
			mCommonPositionController.SetControlledValueCode(1);
			mCommonPositionController.SetIsAbsolute(false);
			mCommonPositionController.SetControllee(this);
			mCommonTimerSequence.RegisterInterval(mCommonPositionController, 0, 2500);
			KeyFrameSequence keyFrameSequence2 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 19);
			mCommonVisibilityController = new KeyFrameController();
			mCommonVisibilityController.SetKeySequence(keyFrameSequence2);
			mCommonVisibilityController.SetControlledValueCode(5);
			mCommonVisibilityController.SetControllee(this);
			mCommonTimerSequence.RegisterInterval(mCommonVisibilityController, 0, 2500);
			KeyFrameSequence keyFrameSequence3 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 20);
			mCommonOpacityController = new KeyFrameController();
			mCommonOpacityController.SetKeySequence(keyFrameSequence3);
			mCommonOpacityController.SetControlledValueCode(6);
			mCommonOpacityController.SetControllee(this);
			mCommonTimerSequence.RegisterInterval(mCommonOpacityController, 0, 2500);
		}

		public virtual void CreateAndRegisterUniqueControllers()
		{
			mFeedbackDisplayAnim.RegisterInterval(mCommonTimerSequence, 0, 2500);
			int num = mCurrentFeedbackDisplayIdx + 1;
			if (num <= 1)
			{
				return;
			}
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			int num2 = -1;
			num2 = ((num <= 3) ? (2500 / num - (num - 1) * 160) : 175);
			for (int i = 0; i < num; i++)
			{
				bool flag = mFeedbackText[i].GetNbLines() == 1;
				if (i != mCurrentFeedbackDisplayIdx)
				{
					int startTime = (i + 1) * num2 + i * 160;
					if (mFeedbackHideViewportController[mHideControllerIdx] == null)
					{
						KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 21);
						mFeedbackHideViewportController[mHideControllerIdx] = new KeyFrameController();
						mFeedbackHideViewportController[mHideControllerIdx].SetKeySequence(keyFrameSequence);
						mFeedbackHideViewportController[mHideControllerIdx].SetControlledValueCode(1);
						mFeedbackHideViewportController[mHideControllerIdx].SetIsAbsolute(false);
					}
					mFeedbackHideViewportController[mHideControllerIdx].SetControllee(mFeedbackViewport[i]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackHideViewportController[mHideControllerIdx], startTime, 80);
					if (mFeedbackHideTextController[mHideControllerIdx] == null)
					{
						KeyFrameSequence keyFrameSequence2 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 22);
						mFeedbackHideTextController[mHideControllerIdx] = new KeyFrameController();
						mFeedbackHideTextController[mHideControllerIdx].SetKeySequence(keyFrameSequence2);
						mFeedbackHideTextController[mHideControllerIdx].SetControlledValueCode(1);
						mFeedbackHideTextController[mHideControllerIdx].SetIsAbsolute(false);
					}
					mFeedbackHideTextController[mHideControllerIdx].SetControllee(mFeedbackText[i]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackHideTextController[mHideControllerIdx], startTime, 80);
					if (mFeedbackHideShapeController[mHideControllerIdx] == null)
					{
						mFeedbackHideShapeController[mHideControllerIdx] = new KeyFrameController();
						mFeedbackHideShapeController[mHideControllerIdx].SetControlledValueCode(5);
					}
					int entryPoint = 24;
					if (flag)
					{
						entryPoint = 23;
					}
					KeyFrameSequence keyFrameSequence3 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, entryPoint);
					mFeedbackHideShapeController[mHideControllerIdx].SetKeySequence(keyFrameSequence3);
					mFeedbackHideShapeController[mHideControllerIdx].SetControllee(mFeedbackHideShape[mHideControllerIdx]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackHideShapeController[mHideControllerIdx], startTime, 80);
					mHideControllerIdx++;
				}
				if (i != 0)
				{
					int startTime2 = i * (num2 + 80) + (i - 1) * 80;
					if (mFeedbackShowViewportController[mShowControllerIdx] == null)
					{
						KeyFrameSequence keyFrameSequence4 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 25);
						mFeedbackShowViewportController[mShowControllerIdx] = new KeyFrameController();
						mFeedbackShowViewportController[mShowControllerIdx].SetKeySequence(keyFrameSequence4);
						mFeedbackShowViewportController[mShowControllerIdx].SetControlledValueCode(2);
					}
					mFeedbackShowViewportController[mShowControllerIdx].SetControllee(mFeedbackViewport[i]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackShowViewportController[mShowControllerIdx], startTime2, 80);
					if (mFeedbackShowShapeController1[mShowControllerIdx] == null)
					{
						KeyFrameSequence keyFrameSequence5 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 26);
						mFeedbackShowShapeController1[mShowControllerIdx] = new KeyFrameController();
						mFeedbackShowShapeController1[mShowControllerIdx].SetControlledValueCode(5);
						mFeedbackShowShapeController1[mShowControllerIdx].SetKeySequence(keyFrameSequence5);
					}
					mFeedbackShowShapeController1[mShowControllerIdx].SetControllee(mFeedbackShowShape[mShowControllerIdx]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackShowShapeController1[mShowControllerIdx], startTime2, 80);
					if (mFeedbackShowShapeController2[mShowControllerIdx] == null)
					{
						mFeedbackShowShapeController2[mShowControllerIdx] = new KeyFrameController();
						mFeedbackShowShapeController2[mShowControllerIdx].SetControlledValueCode(1);
						mFeedbackShowShapeController2[mShowControllerIdx].SetIsAbsolute(false);
					}
					int entryPoint2 = 28;
					if (flag)
					{
						entryPoint2 = 27;
					}
					KeyFrameSequence keyFrameSequence6 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, entryPoint2);
					mFeedbackShowShapeController2[mShowControllerIdx].SetKeySequence(keyFrameSequence6);
					mFeedbackShowShapeController2[mShowControllerIdx].SetControllee(mFeedbackShowShape[mShowControllerIdx]);
					mFeedbackDisplayAnim.RegisterInterval(mFeedbackShowShapeController2[mShowControllerIdx], startTime2, 80);
					mShowControllerIdx++;
				}
			}
		}

		public virtual Vector2_short GetDisplayViewportSize()
		{
			Vector2_short vector2_short = new Vector2_short();
			vector2_short.SetX(311);
			if (mCurrentFeedbackDisplayIdx == 0)
			{
				vector2_short.SetY(90);
			}
			else
			{
				vector2_short.SetY(0);
			}
			return vector2_short;
		}

		public virtual void SetStartingVisibility()
		{
			SetVisible(false);
		}
	}
}
