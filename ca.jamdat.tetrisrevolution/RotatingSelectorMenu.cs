using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RotatingSelectorMenu : SelectorMenu
	{
		public RotatingMenu mRotatingMenu;

		public VerticalCompletionViewport mVerticalCompletionViewport;

		public RotatingSelectorMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			AdjustDemoLayout();
			VerticalSelector.Initialize(mSelector, 0, mFocusedSelectionIndex);
			DisableSelectionOnInitialize();
			if (mRotatingMenu == null)
			{
				mRotatingMenu = new RotatingMenu();
			}
			mRotatingMenu.Initialize(mSelector);
			GameApp.Get().GetTouchMenuReceiver().Initialize(this);
			mVerticalCompletionViewport = new VerticalCompletionViewport(mViewport);
		}

		public void AdjustDemoLayout()
		{
			if (!GameApp.Get().GetIsDemo())
			{
				return;
			}
			if (mId == 9)
			{
				int selectionIndexFromCommmand = Utilities.GetSelectionIndexFromCommmand(mSelector, 12);
				Selection selectionAt = mSelector.GetSelectionAt(selectionIndexFromCommmand);
				Utilities.RemoveSelection(mSelector, selectionIndexFromCommmand);
				selectionIndexFromCommmand = Utilities.GetSelectionIndexFromCommmand(mSelector, -28);
				Utilities.RemoveSelection(mSelector, selectionIndexFromCommmand);
				Text text = (Text)selectionAt.GetChild(0);
				FlString flString = EntryPoint.GetFlString(-2144239522, 16);
				text.SetCaption(flString);
				Utilities.AddSelection(mSelector, selectionAt, 0);
				selectionAt = EntryPoint.GetSelection(1507374, 5);
				Utilities.AddSelection(mSelector, selectionAt, 2);
				for (int i = 0; i < mSelector.GetNumSelections(); i++)
				{
					int command = mSelector.GetSelectionAt(i).GetCommand();
					if (command == 35 || command == -24 || command == -22)
					{
						mSelector.GetSelectionAt(i).SetCommand(-33);
					}
				}
			}
			else if (mId == 17)
			{
				for (int j = 0; j < mSelector.GetNumSelections(); j++)
				{
					mSelector.GetSelectionAt(j).SetCommand(-33);
				}
			}
			else if (mId == 12)
			{
				int selectionIndexFromCommmand2 = Utilities.GetSelectionIndexFromCommmand(mSelector, 47);
				mSelector.GetSelectionAt(selectionIndexFromCommmand2).SetCommand(-33);
			}
		}

		public override void Unload()
		{
			DestroyTransitionAnimation();
			if (mRotatingMenu != null)
			{
				mRotatingMenu.Unload();
				mRotatingMenu = null;
			}
			if (mVerticalCompletionViewport != null)
			{
				mVerticalCompletionViewport.Unload();
				mVerticalCompletionViewport = null;
			}
			base.Unload();
		}

		public override void Suspend()
		{
			if (mRotatingMenu != null)
			{
				mRotatingMenu.UnRegisterInGlobalTime();
				mRotatingMenu.EnableRotatingSelector(false);
			}
			base.Suspend();
		}

		public override void Resume()
		{
			if (mRotatingMenu != null)
			{
				Component currentFocus = GameApp.Get().GetCurrentFocus();
				mRotatingMenu.RegisterInGlobalTime();
				mRotatingMenu.EnableRotatingSelector(true);
				GameApp.Get().SetCurrentFocus(currentFocus);
			}
			base.Resume();
		}

		public override void StartOpeningAnims()
		{
			base.StartOpeningAnims();
			mVerticalCompletionViewport.RegisterInGlobalTime();
		}

		public override bool IsOpeningAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !mAnimator.IsPlaying(2);
			}
			return false;
		}

		public override void OnOpeningAnimsEnded()
		{
			base.OnOpeningAnimsEnded();
			mRotatingMenu.OnOpeningAnimEnded();
			mVerticalCompletionViewport.AdjustRect();
			mVerticalCompletionViewport.UnRegisterInGlobalTime();
		}

		public override void StartClosingAnims()
		{
			mSelector.GetNextArrow().GetViewport().SetVisible(false);
			mRotatingMenu.StartClosingAnims();
			base.StartClosingAnims();
		}

		public override bool IsClosingAnimsEnded()
		{
			if (base.IsClosingAnimsEnded())
			{
				return !mAnimator.IsPlaying(2);
			}
			return false;
		}

		public override void CreateOpeningAnims()
		{
			int num = 0;
			num += 333;
			num += 541;
			base.CreateOpeningAnims();
			mVerticalCompletionViewport.CreateOpeningAnims(333, mAnimationTimerSequence);
			KeyFrameController[] array = mRotatingMenu.CreateOpeningAnims();
			int numVisibleSelections = mRotatingMenu.GetNumVisibleSelections();
			for (int i = 0; i < numVisibleSelections; i++)
			{
				mAnimationTimerSequence.RegisterInterval(array[i], num, num + 200);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections], num, num + 200);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections * 2], num, num + 200);
			}
		}

		public override void CreateClosingAnims(int startTime)
		{
			startTime += 450;
			startTime += 200;
			base.CreateClosingAnims(startTime);
			mVerticalCompletionViewport.CreateClosingAnims(200, mAnimationTimerSequence);
			KeyFrameController[] array = mRotatingMenu.CreateClosingAnims();
			int numVisibleSelections = mRotatingMenu.GetNumVisibleSelections();
			for (int i = 0; i < numVisibleSelections; i++)
			{
				mAnimationTimerSequence.RegisterInterval(array[i], 0, 400);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections], 0, 400);
				mAnimationTimerSequence.RegisterInterval(array[i + numVisibleSelections * 2], 0, 400);
			}
		}

		public override int GetNumOpeningAnims()
		{
			int num = 0;
			num += mRotatingMenu.GetNumVisibleSelections() * 3;
			num += 7;
			return base.GetNumOpeningAnims() + num;
		}

		public override int GetNumClosingAnims()
		{
			int num = 0;
			num += mRotatingMenu.GetNumVisibleSelections() * 3;
			num += 7;
			return base.GetNumClosingAnims() + num;
		}

		public override int GetOpeningAnimsDuration()
		{
			int num = 0;
			num += 333;
			num += 1013;
			return FlMath.Maximum(num, base.GetOpeningAnimsDuration());
		}

		public override int GetClosingAnimsDuration()
		{
			int num = 0;
			num += 650;
			num += 333;
			num += 200;
			return FlMath.Maximum(num, base.GetClosingAnimsDuration());
		}

		public override void CreateTouchZones()
		{
			TouchMenuReceiver touchMenuReceiver = GameApp.Get().GetTouchMenuReceiver();
			touchMenuReceiver.CreateZone(0, mRotatingMenu.GetSelectorRect(), 0, 7);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			if (!mRotatingMenu.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition) && !base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition))
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition);
			}
			return true;
		}

		public override void EnableSelector(bool enable)
		{
			mRotatingMenu.EnableRotatingSelector(enable);
		}

		public virtual void DisableSelectionOnInitialize()
		{
		}
	}
}
