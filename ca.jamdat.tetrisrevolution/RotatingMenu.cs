using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RotatingMenu : Viewport
	{
		public const short inAnimationInterval = 100;

		public const short inAnimationEndTime = 200;

		public const short outAnimationInterval = 300;

		public const short outAnimationEndTime = 400;

		public const int kUp = -1;

		public const int kNone = 0;

		public const int kDown = 1;

		private int selectorInitialSelection = -1;

		private short penDownY = -1;

		public bool mIsMainMenu;

		public RepeatingViewport mTopRepeatingVP;

		public RepeatingViewport mMiddleRepeatingVP;

		public RepeatingViewport mBottomRepeatingVP;

		public Selector mSelector;

		public int mNumTopSelections;

		public int mNumBottomSelections;

		public int mNumVisibleSelections;

		public bool mIsScrollingWithTouch;

		public int mScrollDirection;

		public int mPreviousSelectionIdx;

		public short mPreviousSelectionCoordY;

		public short mRemainingOffsetY;

		public short mHeight;

		public int mInitialTime;

		private int mCumulativeDeltaY;

		public ResizableFrame mCursor;

		public bool mUsingCursor;

		public bool mInterrupted;

		public bool mFinishedAnimating;

		public int mAnimationShapesIdx;

		public Text mSelectedText;

		public Text[] mVisibleTopTexts;

		public Text[] mVisibleBottomTexts;

		public Shape[] mAnimationShapes;

		public Shape mSelectedShape;

		public KeyFrameController[] mAnimationControllers;

		public RotatingMenu(bool useCursor)
		{
			mNumTopSelections = -1;
			mNumBottomSelections = -1;
			mNumVisibleSelections = -1;
			mScrollDirection = 0;
			mPreviousSelectionCoordY = 0;
			mRemainingOffsetY = 0;
			mUsingCursor = useCursor;
			mClipChildren = true;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(Selector selector)
		{
			mSelector = selector;
			Viewport scrollerViewport = selector.GetScrollerViewport();
			SetViewport(scrollerViewport.GetViewport());
			short rectHeight = mSelector.GetSelectionAt(0).GetRectHeight();
			int numSelections = mSelector.GetNumSelections();
			if (numSelections < 2)
			{
				mNumTopSelections = 1;
				mNumBottomSelections = 1;
				mNumVisibleSelections = 3;
				mHeight = (short)(rectHeight * 2);
			}
			else if (numSelections == 2)
			{
				mNumTopSelections = 0;
				mNumBottomSelections = 1;
				mNumVisibleSelections = 2;
				mHeight = (short)(rectHeight * 2);
			}
			else
			{
				mHeight = mSelector.GetRectHeight();
				int num = 0;
				for (int i = 0; i < mSelector.GetNumSelections(); i++)
				{
					if (mSelector.GetSelectionAt(i).GetEnabledState())
					{
						num++;
					}
				}
				int num2 = mHeight / rectHeight;
				int num3 = (num2 - 1) / 2;
				int num4 = num2 - 1 - num3;
				short num5 = (short)(num * rectHeight);
				if (num5 < mHeight)
				{
					mHeight = num5;
					mNumTopSelections = (num - 1) / 2;
					mNumBottomSelections = num - 1 - mNumTopSelections;
				}
				else
				{
					mNumTopSelections = num3;
					mNumBottomSelections = num4;
				}
				mNumVisibleSelections = mNumTopSelections + mNumBottomSelections + 1;
			}
			InitializeRepeatingViewports();
			Viewport viewport = mSelector.GetNextArrow().GetViewport();
			viewport.SetSize(viewport.GetRectWidth(), mSelector.GetRectHeight());
			if (mUsingCursor)
			{
				mCursor = CreateCursorViewport();
			}
			scrollerViewport.SetVisible(false);
			RegisterInGlobalTime();
			mVisibleTopTexts = new Text[mNumTopSelections];
			mVisibleBottomTexts = new Text[mNumBottomSelections];
			mAnimationShapes = new Shape[mNumVisibleSelections];
			CopySelectionsToRepeatableViewports();
			FindAnimatedSelections();
			short rectTop = mSelector.GetSelectionAt(mSelector.GetSingleSelection()).GetRectTop();
			mTopRepeatingVP.MoveTo(0, (short)(-rectTop + mNumTopSelections * rectHeight));
			mMiddleRepeatingVP.MoveTo(0, (short)(-rectTop));
			mBottomRepeatingVP.MoveTo(0, (short)(-rectTop - rectHeight));
			mPreviousSelectionCoordY = rectTop;
			mSelector.GetScrollerViewport().SetSize(0, 0);
			mSelector.UpdateScroller();
			SetContentsVisible(false);
			mSelector.GetNextArrow().GetViewport().SetVisible(false);
		}

		public virtual void Unload()
		{
			DestroyAnimations();
			if (mAnimationShapes != null)
			{
				mAnimationShapes = null;
				mAnimationShapes = null;
			}
			if (mVisibleTopTexts != null)
			{
				mVisibleTopTexts = null;
				mVisibleTopTexts = null;
			}
			if (mVisibleBottomTexts != null)
			{
				mVisibleBottomTexts = null;
				mVisibleBottomTexts = null;
			}
			if (mCursor != null)
			{
				mCursor.Unload();
				mCursor.SetViewport(null);
				mCursor = null;
				Viewport viewport = EntryPoint.GetViewport(1310760, 195);
				viewport.SetViewport(null);
			}
			UnRegisterInGlobalTime();
			SetViewport(null);
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (msg == -120 || msg == -119)
			{
				switch (intParam)
				{
				case 1:
					mScrollDirection = -1;
					break;
				case 2:
					mScrollDirection = 1;
					break;
				}
			}
			return base.OnMsg(source, msg, intParam);
		}

		public override bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			Selection selectionAt = mSelector.GetSelectionAt(mSelector.GetSingleSelection());
			return selectionAt.OnDefaultMsg(selectionAt, msg, intParam);
		}

		public virtual void OnOpeningAnimEnded()
		{
			DestroyAnimations();
			mSelector.GetNextArrow().GetViewport().SetVisible(false);
			if (mUsingCursor)
			{
				mCursor.SetVisible(true);
			}
		}

		public virtual void StartClosingAnims()
		{
			if (mUsingCursor)
			{
				mCursor.SetVisible(false);
			}
		}

		public virtual KeyFrameController[] CreateOpeningAnims()
		{
			int num = 0;
			FindAnimatedSelections();
			mAnimationControllers = new KeyFrameController[mNumVisibleSelections * 3];
			for (num = 0; num < mNumTopSelections; num++)
			{
				mTopRepeatingVP.AddComponent(CreateShapeOpeningAnimation(mVisibleTopTexts[num], false));
			}
			for (num = 0; num < mNumBottomSelections; num++)
			{
				mBottomRepeatingVP.AddComponent(CreateShapeOpeningAnimation(mVisibleBottomTexts[num], false));
			}
			mMiddleRepeatingVP.AddComponent(CreateShapeOpeningAnimation(mSelectedText, true));
			return mAnimationControllers;
		}

		public virtual KeyFrameController[] CreateClosingAnims()
		{
			int num = 0;
			FindAnimatedSelections();
			mAnimationControllers = new KeyFrameController[mNumVisibleSelections * 3];
			for (num = 0; num < mNumTopSelections; num++)
			{
				mTopRepeatingVP.AddComponent(CreateShapeClosingAnimation(mVisibleTopTexts[num], false));
			}
			for (num = 0; num < mNumBottomSelections; num++)
			{
				mBottomRepeatingVP.AddComponent(CreateShapeClosingAnimation(mVisibleBottomTexts[num], false));
			}
			mMiddleRepeatingVP.AddComponent(CreateShapeClosingAnimation(mSelectedText, true));
			return mAnimationControllers;
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			base.OnTime(totalTime, deltaTime);
			if (GameApp.Get().GetAnimator().IsPlaying(2))
			{
				Invalidate();
			}
			if (AdjustOffsetToSelectedSelection() || mInterrupted)
			{
				mInitialTime = totalTime;
				if (mInterrupted)
				{
					mInterrupted = false;
				}
			}
			bool flag = false;
			if (GameApp.Get().GetCommandHandler().GetCurrentScene()
				.GetView() != null)
			{
				flag = GameApp.Get().GetCommandHandler().GetCurrentScene()
					.GetView()
					.DescendentOrSelfHasFocus();
			}
			if (mRemainingOffsetY != 0 && (flag || GameApp.Get().GetAnimator().IsPlaying(2)))
			{
				int num = -mRemainingOffsetY;
				int num2 = totalTime - mInitialTime;
				if (FlMath.Absolute(mRemainingOffsetY) > 2)
				{
					if (num > -10000 && num < 10000)
					{
						num = num2 * num / 750;
					}
				}
				else
				{
					num /= FlMath.Absolute(num);
				}
				mTopRepeatingVP.MoveBy(0, (short)num);
				mMiddleRepeatingVP.MoveBy(0, (short)num);
				mBottomRepeatingVP.MoveBy(0, (short)num);
				mRemainingOffsetY = (short)(mRemainingOffsetY + num);
			}
			else
			{
				int singleSelection = mSelector.GetSingleSelection();
				mPreviousSelectionCoordY = mSelector.GetSelectionAt(singleSelection).GetRectTop();
				mPreviousSelectionIdx = singleSelection;
				mScrollDirection = 0;
			}
		}

		public int GetHitComponentIndex(Vector2_short lastPenPosition, short textH)
		{
			int result = -1;
			if (lastPenPosition.GetY() >= mMiddleRepeatingVP.GetAbsoluteTop() && lastPenPosition.GetY() < mMiddleRepeatingVP.GetAbsoluteTop() + textH)
			{
				result = mSelector.GetSingleSelection();
			}
			else if (lastPenPosition.GetY() >= mBottomRepeatingVP.GetAbsoluteTop() && lastPenPosition.GetY() <= mBottomRepeatingVP.GetAbsoluteTop() + mNumBottomSelections * textH)
			{
				result = FindHitComponentIdx(mBottomRepeatingVP, lastPenPosition);
				mScrollDirection = 1;
			}
			else if (lastPenPosition.GetY() >= mTopRepeatingVP.GetAbsoluteTop() && lastPenPosition.GetY() <= mTopRepeatingVP.GetAbsoluteTop() + mNumTopSelections * textH)
			{
				result = FindHitComponentIdx(mTopRepeatingVP, lastPenPosition);
				mScrollDirection = -1;
			}
			return result;
		}

		public void HandlePenMsg(int msg, int intParam)
		{
		}

		public virtual bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			bool result = true;
			short y = firstPenPosition.GetY();
			short y2 = lastPenPosition.GetY();
			switch (command)
			{
			case 98:
				if (mScrollDirection == 0)
				{
					bool forceSelectionActivation = false;
					switch (zoneId)
					{
					case 2:
					case 5:
					{
						mScrollDirection = 1;
						short rectHeight = mSelector.GetSelectionAt(0).GetRectHeight();
						TouchSwipe(rectHeight);
						mIsScrollingWithTouch = true;
						result = true;
						break;
					}
					case 3:
					case 4:
					{
						mScrollDirection = -1;
						short deltaY = (short)(-mSelector.GetSelectionAt(0).GetRectHeight());
						TouchSwipe(deltaY);
						mIsScrollingWithTouch = true;
						result = true;
						break;
					}
					case 1:
						forceSelectionActivation = true;
						break;
					default:
						mIsScrollingWithTouch = true;
						break;
					}
					TouchTap(lastPenPosition, forceSelectionActivation);
				}
				result = true;
				break;
			case 91:
				mScrollDirection = -1;
				UpdateDelta(y, y2);
				TouchSwipe();
				mIsScrollingWithTouch = true;
				result = true;
				break;
			case 90:
				mScrollDirection = 1;
				UpdateDelta(y, y2);
				TouchSwipe();
				mIsScrollingWithTouch = true;
				result = true;
				break;
			case 94:
				mScrollDirection = 1;
				UpdateDelta(y, y2);
				TouchSwipe();
				mIsScrollingWithTouch = true;
				result = true;
				break;
			case 95:
				mScrollDirection = -1;
				UpdateDelta(y, y2);
				TouchSwipe();
				mIsScrollingWithTouch = true;
				result = true;
				break;
			}
			return result;
		}

		public virtual KeyFrameController CreateSelectedDifficultyTextAnimation(bool @out)
		{
			Viewport viewport = (Viewport)mMiddleRepeatingVP.GetChild(0);
			Component child = viewport.GetChild(mSelector.GetSingleSelection() * 2 + 1);
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(child);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence.SetInterpolator(0);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(5);
			int[] array = new int[1] { 0 };
			if (@out)
			{
				keyFrameSequence.SetKeyFrame(0, 200, array);
			}
			else
			{
				keyFrameSequence.SetKeyFrame(0, 0, array);
			}
			array[0] = 1;
			if (@out)
			{
				keyFrameSequence.SetKeyFrame(1, 400, array);
			}
			else
			{
				keyFrameSequence.SetKeyFrame(1, 200, array);
			}
			return keyFrameController;
		}

		public virtual void DestroyAnimations()
		{
			for (int i = 0; i < mNumVisibleSelections; i++)
			{
				if (mAnimationShapes[i] != null)
				{
					mAnimationShapes[i].SetViewport(null);
					mAnimationShapes[i] = null;
					mAnimationShapes[i] = null;
				}
			}
			mSelectedShape = null;
			if (mAnimationControllers != null)
			{
				mAnimationControllers = null;
				mAnimationControllers = null;
			}
			mAnimationShapesIdx = 0;
		}

		public virtual void FindAnimatedSelections()
		{
			int numSelections = mSelector.GetNumSelections();
			int singleSelection = mSelector.GetSingleSelection();
			Viewport viewport = (Viewport)mTopRepeatingVP.GetChild(0);
			Viewport viewport2 = (Viewport)mMiddleRepeatingVP.GetChild(0);
			Viewport viewport3 = (Viewport)mBottomRepeatingVP.GetChild(0);
			if (mSelector.GetSelectionAt(singleSelection).GetSubtype() == -2)
			{
				mSelectedText = (Text)viewport2.GetChild(singleSelection * 2);
			}
			else
			{
				mSelectedText = (Text)viewport2.GetChild(singleSelection);
			}
			int num = 0;
			int num2 = 0;
			for (num = 0; num < mNumTopSelections; num++)
			{
				num2 = singleSelection - (num + 1);
				if (num2 < 0)
				{
					num2 = numSelections + num2;
				}
				mVisibleTopTexts[num] = (Text)viewport.GetChild(num2);
			}
			for (num = 0; num < mNumBottomSelections; num++)
			{
				num2 = singleSelection + (num + 1);
				if (num2 >= numSelections)
				{
					num2 = singleSelection + (num + 1) - numSelections;
				}
				mVisibleBottomTexts[num] = (Text)viewport3.GetChild(num2);
			}
		}

		public virtual int FindHitComponentIdx(RepeatingViewport contactViewport, Vector2_short ptIn)
		{
			short x = ptIn.GetX();
			short y = ptIn.GetY();
			return contactViewport.GetHitComponentIdx(x, y);
		}

		public virtual FlRect GetSelectorRect()
		{
			return mSelector.GetRect();
		}

		public virtual int GetNumVisibleSelections()
		{
			return mNumVisibleSelections;
		}

		public virtual void EnableRotatingSelector(bool enable)
		{
			for (int i = 0; i < mSelector.GetNumSelections(); i++)
			{
				mSelector.GetSelectionAt(i).SetEnabledState(enable);
			}
			if (enable && IsAttached())
			{
				TakeFocus();
			}
			else if (!enable)
			{
				mInterrupted = true;
			}
		}

		public virtual bool AdjustOffsetToSelectedSelection()
		{
			bool result = false;
			int numSelections = mSelector.GetNumSelections();
			int singleSelection = mSelector.GetSingleSelection();
			Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
			if (selectionAt.GetRectTop() != mPreviousSelectionCoordY)
			{
				short rectHeight = selectionAt.GetRectHeight();
				short rectTop = selectionAt.GetRectTop();
				int num = rectTop - mPreviousSelectionCoordY;
				if (FlMath.Absolute(num) > rectHeight || numSelections < 3)
				{
					int num2 = num / rectHeight;
					if (!mIsScrollingWithTouch)
					{
						if (singleSelection == 0 && mPreviousSelectionIdx == numSelections - 1)
						{
							mScrollDirection = 1;
						}
						else
						{
							mScrollDirection = -1;
						}
					}
					mIsScrollingWithTouch = false;
					if (num > 0)
					{
						if (mScrollDirection == 1)
						{
							num = rectHeight * num2;
						}
						else if (mScrollDirection == -1)
						{
							num = rectHeight * (num2 - numSelections);
						}
					}
					else if (mScrollDirection == 1)
					{
						num = rectHeight * (numSelections + num2);
					}
					else if (mScrollDirection == -1)
					{
						num = rectHeight * num2;
					}
				}
				mRemainingOffsetY = (short)(mRemainingOffsetY + num);
				mPreviousSelectionCoordY = selectionAt.GetRectTop();
				mPreviousSelectionIdx = singleSelection;
				result = true;
			}
			return result;
		}

		public virtual void CopySelectionsToRepeatableViewports()
		{
			for (int i = 0; i < mSelector.GetNumSelections(); i++)
			{
				Selection selectionAt = mSelector.GetSelectionAt(i);
				selectionAt.SetForwardFocus(this);
				if (!selectionAt.GetEnabledState())
				{
					continue;
				}
				if (selectionAt.GetSubtype() == -1)
				{
					mTopRepeatingVP.AddComponent(CopyTextSelectionItem(selectionAt, false));
					mMiddleRepeatingVP.AddComponent(CopyTextSelectionItem(selectionAt, true));
					mBottomRepeatingVP.AddComponent(CopyTextSelectionItem(selectionAt, false));
				}
				else if (selectionAt.GetSubtype() == -2)
				{
					Text text = CopyTextSelectionItem(selectionAt, false);
					Text text2 = CopyTextSelectionItem(selectionAt, true);
					Text text3 = CopyTextSelectionItem(selectionAt, false);
					mTopRepeatingVP.AddComponent(text);
					mMiddleRepeatingVP.AddComponent(text2);
					mBottomRepeatingVP.AddComponent(text3);
					text.SetSize((short)(mTopRepeatingVP.GetRectWidth() - 25 - 10), text2.GetRectHeight());
					text2.SetSize((short)(mMiddleRepeatingVP.GetRectWidth() - 25 - 10), text2.GetRectHeight());
					text3.SetSize((short)(mBottomRepeatingVP.GetRectWidth() - 25 - 10), text2.GetRectHeight());
					FlFont flFont = EntryPoint.GetFlFont(3047517, 6);
					int num = flFont.GetGlowOffset() / 2;
					FeatsExpert featsExpert = FeatsExpert.Get();
					if (!featsExpert.IsGameVariantUnlocked(i))
					{
						FlString flString = EntryPoint.GetFlString(-2144239522, 151);
						text.SetCaption(flString);
						text2.SetCaption(flString);
						text3.SetCaption(flString);
						FlBitmap flBitmap = EntryPoint.GetFlBitmap(1540143, 4);
						Sprite sprite = new Sprite();
						sprite.SetBitmap(flBitmap);
						sprite.SetTopLeft((short)(mMiddleRepeatingVP.GetRectWidth() - sprite.GetRectWidth()), (short)(text2.GetRectTop() - 7 - num + (text2.GetRectHeight() / 2 - sprite.GetRectHeight() / 2)));
						mMiddleRepeatingVP.AddComponent(sprite);
					}
					else
					{
						FlString flString2 = new FlString();
						Utilities.GetRomanNumeral(i + 1, flString2);
						flString2.AddAssign(" ");
						flString2.AddAssign(text2.GetCaption());
						text2.SetCaption(flString2);
						ProgressionExpert progressionExpert = ProgressionExpert.Get();
						Text text4 = CreateDifficultySelectionItem(selectionAt);
						text4.SetCaption(new FlString(FlMath.Maximum(progressionExpert.GetHighestLevelDone(i), 0)));
						text4.SetTopLeft((short)(mMiddleRepeatingVP.GetRectWidth() - text4.GetRectWidth()), (short)(text2.GetRectTop() - 7 - num + (text2.GetRectHeight() / 2 - text4.GetRectHeight() / 2)));
						mMiddleRepeatingVP.AddComponent(text4);
					}
				}
			}
		}

		public virtual Text CopyTextSelectionItem(Selection selection, bool isSelected)
		{
			Text text = (Text)selection.GetChild(0);
			FlFont flFont = EntryPoint.GetFlFont(3047517, 6);
			FlFont flFont2 = EntryPoint.GetFlFont(3047517, 4);
			if (selection.GetCommand() == -32)
			{
				flFont2 = EntryPoint.GetFlFont(3047517, 5);
			}
			int num = flFont.GetGlowOffset() / 2;
			int num2 = (flFont.GetLineHeight() - flFont2.GetLineHeight()) / 2;
			int rectWidth = mSelector.GetNextArrow().GetViewport().GetRectWidth();
			Text text2 = new Text();
			if (isSelected)
			{
				text2.SetRect((short)(selection.GetRectLeft() + (mUsingCursor ? 25 : 0)), (short)(selection.GetRectTop() + 15), (short)(selection.GetRectWidth() + rectWidth), (short)(selection.GetRectHeight() - 15));
			}
			else
			{
				text2.SetRect((short)(selection.GetRectLeft() + num + (mUsingCursor ? 25 : 0)), (short)(selection.GetRectTop() + num + num2 + 15), (short)(selection.GetRectWidth() - num + rectWidth), (short)(selection.GetRectHeight() - num - num2 - 15));
			}
			text2.SetCaption(new FlString(text.GetCaption()));
			text2.SetFont(isSelected ? flFont : flFont2);
			text2.SetAlignment(text.GetAlignment());
			return text2;
		}

		public virtual Text CreateDifficultySelectionItem(Selection selection)
		{
			FlFont flFont = EntryPoint.GetFlFont(3047517, 20);
			Text text = new Text();
			text.SetFont(flFont);
			text.SetSize(25, (short)flFont.GetLineHeight());
			text.SetAlignment(1);
			return text;
		}

		public virtual void InitializeRepeatingViewports()
		{
			mTopRepeatingVP = new RepeatingViewport(this);
			mTopRepeatingVP.Initialize();
			mMiddleRepeatingVP = new RepeatingViewport(this);
			mMiddleRepeatingVP.Initialize();
			mBottomRepeatingVP = new RepeatingViewport(this);
			mBottomRepeatingVP.Initialize();
			short rectHeight = mSelector.GetSelectionAt(0).GetRectHeight();
			Viewport viewport = mSelector.GetNextArrow().GetViewport();
			short rectWidth = viewport.GetRectWidth();
			short rectWidth2 = mSelector.GetRectWidth();
			mTopRepeatingVP.SetSize(rectWidth2, (short)(mNumTopSelections * rectHeight));
			mMiddleRepeatingVP.SetSize(rectWidth2, rectHeight);
			mBottomRepeatingVP.SetSize(rectWidth2, (short)(mNumBottomSelections * rectHeight));
			mTopRepeatingVP.SetTopLeft(0, 0);
			mMiddleRepeatingVP.SetTopLeft(0, (short)(mTopRepeatingVP.GetRectTop() + mTopRepeatingVP.GetRectHeight()));
			mBottomRepeatingVP.SetTopLeft(0, (short)(mMiddleRepeatingVP.GetRectTop() + mMiddleRepeatingVP.GetRectHeight()));
			viewport.SetSize(rectWidth, mHeight);
			mSelector.GetNextArrow().SetTopLeft(0, (short)(mHeight - mSelector.GetPreviousArrow().GetRectHeight()));
			mSelector.GetPreviousArrow().SetTopLeft(0, 0);
			SetSize(rectWidth2, mHeight);
			mSelector.SetTopLeft(mSelector.GetRectLeft(), (short)(mSelector.GetRectTop() + mSelector.GetRectHeight() - mHeight));
			mSelector.SetSize(rectWidth2, mHeight);
		}

		public virtual Shape CreateShapeOpeningAnimation(Text text, bool selected)
		{
			mAnimationShapes[mAnimationShapesIdx] = new Shape();
			mAnimationShapes[mAnimationShapesIdx].SetColor(new Color888(255, 255, 255));
			int num = text.GetRectHeight() / 4;
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mAnimationShapes[mAnimationShapesIdx]);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int[] array = new int[4];
			int num2;
			int num3;
			if (mSelector.GetSelectionAt(0).GetSubtype() == -2 && selected)
			{
				FlFont flFont = EntryPoint.GetFlFont(3047517, 20);
				int lineWidth = flFont.GetLineWidth(StringUtils.CreateString("88"));
				num2 = ((text.GetAlignment() != 2) ? text.GetRectLeft() : (text.GetRectLeft() + (text.GetRectWidth() + 10 + lineWidth)));
				num3 = text.GetLineWidth() + 10 + lineWidth;
			}
			else
			{
				num2 = ((text.GetAlignment() != 2) ? text.GetRectLeft() : (text.GetRectLeft() + text.GetRectWidth()));
				num3 = text.GetLineWidth();
			}
			array[0] = num2;
			array[1] = text.GetRectTop() + text.GetLineHeight() / 2 - num / 2;
			array[2] = 0;
			array[3] = num;
			keyFrameSequence.SetKeyFrame(0, 0, array);
			if (text.GetAlignment() == 2)
			{
				array[0] = text.GetRectWidth() - text.GetLineWidth();
			}
			array[2] = num3;
			keyFrameSequence.SetKeyFrame(1, 100, array);
			array[1] = text.GetRectTop();
			array[3] = text.GetLineHeight();
			keyFrameSequence.SetKeyFrame(2, 200, array);
			mAnimationControllers[mAnimationShapesIdx] = keyFrameController;
			KeyFrameController keyFrameController2 = new KeyFrameController();
			keyFrameController2.SetControllee(text);
			KeyFrameSequence keyFrameSequence2 = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence2.SetInterpolator(0);
			keyFrameController2.SetKeySequence(keyFrameSequence2);
			keyFrameController2.SetControlledValueCode(5);
			int[] array2 = new int[1] { 0 };
			keyFrameSequence2.SetKeyFrame(0, 0, array2);
			array2[0] = 1;
			keyFrameSequence2.SetKeyFrame(1, 200, array2);
			KeyFrameController keyFrameController3 = new KeyFrameController();
			keyFrameController3.SetControllee(mAnimationShapes[mAnimationShapesIdx]);
			KeyFrameSequence keyFrameSequence3 = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence3.SetInterpolator(0);
			keyFrameController3.SetKeySequence(keyFrameSequence3);
			keyFrameController3.SetControlledValueCode(5);
			array2[0] = 1;
			keyFrameSequence3.SetKeyFrame(0, 0, array2);
			array2[0] = 0;
			keyFrameSequence3.SetKeyFrame(1, 200, array2);
			mAnimationControllers[mAnimationShapesIdx + mNumVisibleSelections] = keyFrameController3;
			mAnimationControllers[mAnimationShapesIdx + mNumVisibleSelections * 2] = keyFrameController2;
			mAnimationShapesIdx++;
			return mAnimationShapes[mAnimationShapesIdx - 1];
		}

		public virtual Shape CreateShapeClosingAnimation(Text text, bool selected)
		{
			mAnimationShapes[mAnimationShapesIdx] = new Shape();
			mAnimationShapes[mAnimationShapesIdx].SetColor(new Color888(255, 255, 255));
			int num = text.GetRectHeight() / 4;
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(mAnimationShapes[mAnimationShapesIdx]);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(3, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			int[] array = new int[4];
			int num2;
			int num3;
			if (mSelector.GetSelectionAt(0).GetSubtype() == -2 && selected)
			{
				FlFont flFont = EntryPoint.GetFlFont(3047517, 20);
				int lineWidth = flFont.GetLineWidth(StringUtils.CreateString("88"));
				num2 = text.GetLineWidth() + 10 + lineWidth;
				num3 = ((text.GetAlignment() != 2) ? text.GetRectLeft() : (mRect_left + (mRect_width - num2)));
			}
			else
			{
				num3 = ((text.GetAlignment() != 2) ? text.GetRectLeft() : (text.GetRectLeft() + (text.GetRectWidth() - text.GetLineWidth())));
				num2 = text.GetLineWidth();
			}
			array[0] = num3;
			array[1] = text.GetRectTop();
			array[2] = num2;
			array[3] = text.GetLineHeight();
			if (selected)
			{
				keyFrameSequence.SetKeyFrame(0, 200, array);
			}
			else
			{
				keyFrameSequence.SetKeyFrame(0, 0, array);
			}
			array[1] = text.GetRectTop() + text.GetLineHeight() / 2 - num / 2;
			array[3] = num;
			if (selected)
			{
				keyFrameSequence.SetKeyFrame(1, 300, array);
			}
			else
			{
				keyFrameSequence.SetKeyFrame(1, 100, array);
			}
			if (text.GetAlignment() == 2)
			{
				array[0] = mRect_width;
			}
			array[2] = 0;
			if (selected)
			{
				keyFrameSequence.SetKeyFrame(2, 400, array);
			}
			else
			{
				keyFrameSequence.SetKeyFrame(2, 200, array);
			}
			mAnimationControllers[mAnimationShapesIdx] = keyFrameController;
			KeyFrameController keyFrameController2 = new KeyFrameController();
			keyFrameController2.SetControllee(text);
			KeyFrameSequence keyFrameSequence2 = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence2.SetInterpolator(0);
			keyFrameController2.SetKeySequence(keyFrameSequence2);
			keyFrameController2.SetControlledValueCode(5);
			int[] array2 = new int[1];
			if (selected)
			{
				array2[0] = 1;
			}
			else
			{
				array2[0] = 0;
			}
			keyFrameSequence2.SetKeyFrame(0, 0, array2);
			if (selected)
			{
				array2[0] = 0;
			}
			keyFrameSequence2.SetKeyFrame(1, 200, array2);
			KeyFrameController keyFrameController3 = new KeyFrameController();
			keyFrameController3.SetControllee(mAnimationShapes[mAnimationShapesIdx]);
			KeyFrameSequence keyFrameSequence3 = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence3.SetInterpolator(0);
			keyFrameController3.SetKeySequence(keyFrameSequence3);
			keyFrameController3.SetControlledValueCode(5);
			if (selected)
			{
				array2[0] = 0;
			}
			else
			{
				array2[0] = 1;
			}
			keyFrameSequence3.SetKeyFrame(0, 0, array2);
			if (selected)
			{
				array2[0] = 1;
			}
			else
			{
				array2[0] = 0;
			}
			keyFrameSequence3.SetKeyFrame(1, 200, array2);
			mAnimationControllers[mAnimationShapesIdx + mNumVisibleSelections] = keyFrameController3;
			mAnimationControllers[mAnimationShapesIdx + mNumVisibleSelections * 2] = keyFrameController2;
			mAnimationShapesIdx++;
			return mAnimationShapes[mAnimationShapesIdx - 1];
		}

		public virtual void SetContentsVisible(bool visible)
		{
			int num = 0;
			for (num = 0; num < mNumTopSelections; num++)
			{
				mVisibleTopTexts[num].SetVisible(visible);
			}
			for (num = 0; num < mNumBottomSelections; num++)
			{
				mVisibleBottomTexts[num].SetVisible(visible);
			}
			int singleSelection = mSelector.GetSingleSelection();
			if (mSelector.GetSelectionAt(singleSelection).GetSubtype() == -2)
			{
				Viewport viewport = (Viewport)mMiddleRepeatingVP.GetChild(0);
				viewport.GetChild(singleSelection * 2 + 1).SetVisible(visible);
			}
			mSelectedText.SetVisible(visible);
			if (mUsingCursor)
			{
				mCursor.SetVisible(visible);
			}
		}

		public virtual void TouchSwipe()
		{
			int numSelections = mSelector.GetNumSelections();
			int singleSelection = mSelector.GetSingleSelection();
			int num = 80;
			if (mIsMainMenu)
			{
				num = mSelector.GetRectHeight() / 10;
			}
			int num2 = mCumulativeDeltaY / num;
			int num3 = singleSelection + num2;
			if (FlMath.Absolute(num2) > 0)
			{
				mCumulativeDeltaY = 0;
			}
			while (num3 < 0)
			{
				num3 = numSelections + num2;
				num2 += numSelections;
			}
			while (num3 >= numSelections)
			{
				num3 = 0;
				num2 -= numSelections;
			}
			mSelector.SetSingleSelection(num3, true);
		}

		private void UpdateDelta(short lastPenPositionY, short firstPenPositionY)
		{
			mCumulativeDeltaY += (short)(lastPenPositionY - firstPenPositionY);
		}

		public virtual void TouchSwipe(short deltaY)
		{
			int numSelections = mSelector.GetNumSelections();
			int singleSelection = mSelector.GetSingleSelection();
			int num = deltaY / mSelector.GetSelectionAt(singleSelection).GetRectHeight();
			int num2 = singleSelection + num;
			while (num2 < 0)
			{
				num2 = numSelections + num;
				num += numSelections;
			}
			while (num2 >= numSelections)
			{
				num2 = 0;
				num -= numSelections;
			}
			mSelector.SetSingleSelection(num2, true);
		}

		public virtual void TouchTap(Vector2_short lastPenPosition, bool forceSelectionActivation)
		{
			bool flag = false;
			short y = lastPenPosition.GetY();
			Selector selector = mSelector;
			FindAnimatedSelections();
			int num = -1;
			short rectHeight = selector.GetSelectionAt(0).GetRectHeight();
			if (forceSelectionActivation || (y >= mMiddleRepeatingVP.GetAbsoluteTop() && y < mMiddleRepeatingVP.GetAbsoluteTop() + rectHeight))
			{
				num = mSelector.GetSingleSelection();
				flag = true;
			}
			else if (y >= mBottomRepeatingVP.GetAbsoluteTop() && y <= mBottomRepeatingVP.GetAbsoluteTop() + mNumBottomSelections * rectHeight)
			{
				num = FindHitComponentIdx(mBottomRepeatingVP, lastPenPosition);
				mScrollDirection = 1;
			}
			else if (y >= mTopRepeatingVP.GetAbsoluteTop() && y <= mTopRepeatingVP.GetAbsoluteTop() + mNumTopSelections * rectHeight)
			{
				num = FindHitComponentIdx(mTopRepeatingVP, lastPenPosition);
				mScrollDirection = -1;
			}
			if (num != -1)
			{
				selector.SetSingleSelection(num, true);
				if (flag)
				{
					Selection selectionAt = selector.GetSelectionAt(num);
					selectionAt.SendMsg(selectionAt, -120, 5);
					selectionAt.SendMsg(selectionAt, -121, 5);
				}
			}
		}

		public virtual ResizableFrame CreateCursorViewport()
		{
			Viewport viewport = EntryPoint.GetViewport(1310760, 195);
			viewport.SetSize(15, mMiddleRepeatingVP.GetRectHeight());
			ResizableFrame resizableFrame = null;
			resizableFrame = ResizableFrame.Create(viewport);
			resizableFrame.SetRect(mMiddleRepeatingVP.GetRectLeft(), mMiddleRepeatingVP.GetRectTop(), 15, mMiddleRepeatingVP.GetRectHeight());
			resizableFrame.SetViewport(this);
			BringComponentToFront(resizableFrame);
			return resizableFrame;
		}

		public RotatingMenu()
			: this(true)
		{
		}

		public static RotatingMenu[] InstArrayRotatingMenu(int size)
		{
			RotatingMenu[] array = new RotatingMenu[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new RotatingMenu();
			}
			return array;
		}

		public static RotatingMenu[][] InstArrayRotatingMenu(int size1, int size2)
		{
			RotatingMenu[][] array = new RotatingMenu[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RotatingMenu[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RotatingMenu();
				}
			}
			return array;
		}

		public static RotatingMenu[][][] InstArrayRotatingMenu(int size1, int size2, int size3)
		{
			RotatingMenu[][][] array = new RotatingMenu[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RotatingMenu[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RotatingMenu[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new RotatingMenu();
					}
				}
			}
			return array;
		}
	}
}
