using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class CareerFeatPopup : Popup
	{
		public const int endOfGameContext = 0;

		public const int highlightsMenuContext = 1;

		public sbyte mCurrentFeat;

		public int mPopupContext;

		public Text mDescriptionText;

		public Viewport mProgressBarBackViewport;

		public Viewport mProgressBarFrontViewport;

		public ResizableFrame mProgressBarBack;

		public ResizableFrame mProgressBarFront;

		public TimerSequence mTimerSequence;

		public KeyFrameController mProgressBarController;

		public CareerFeatPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mCurrentFeat = -1;
			mPopupContext = 1;
		}

		public CareerFeatPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, sbyte careerFeat)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mCurrentFeat = careerFeat;
			mPopupContext = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(360459);
		}

		public override void Unload()
		{
			mDescriptionText = null;
			if (mProgressBarBack != null)
			{
				mProgressBarBack.Unload();
				mProgressBarBack = null;
			}
			if (mProgressBarFront != null)
			{
				mProgressBarFront.Unload();
				mProgressBarFront = null;
			}
			if (mTimerSequence != null)
			{
				mTimerSequence.UnRegisterInGlobalTime();
				mTimerSequence.StopRecursively();
				mTimerSequence = null;
			}
			mProgressBarController = null;
			mProgressBarBackViewport = null;
			mProgressBarFrontViewport = null;
			base.Unload();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mProgressBarBackViewport = EntryPoint.GetViewport(package, 5);
			mProgressBarFrontViewport = EntryPoint.GetViewport(package, 6);
			mTimerSequence = EntryPoint.GetTimerSequence(package, 9);
			mProgressBarController = EntryPoint.GetKeyFrameController(package, 10);
			mDescriptionText = EntryPoint.GetText(package, 3);
			Text text = EntryPoint.GetText(package, 4);
			int entryPoint = 11 + mCurrentFeat;
			FlString flString = EntryPoint.GetFlString(package, entryPoint);
			Text text2 = EntryPoint.GetText(package, 2);
			text2.SetCaption(flString);
			FeatsExpert featsExpert = FeatsExpert.Get();
			int careerFeatPercent = featsExpert.GetCareerFeatPercent(mCurrentFeat);
			FlString caption = StatisticsFormatting.CreateStatisticString(careerFeatPercent, 5);
			text.SetCaption(caption);
			FlBitmapMap flBitmapMap = EntryPoint.GetFlBitmapMap(package, 7);
			FlBitmapMap flBitmapMap2 = EntryPoint.GetFlBitmapMap(package, 8);
			mProgressBarBack = new ResizableFrame(5, flBitmapMap);
			mProgressBarFront = new ResizableFrame(5, flBitmapMap2);
		}

		public override void Initialize()
		{
			base.Initialize();
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool flag = featsExpert.GetCareerFeatPercent(mCurrentFeat) == 100;
			mProgressBarBack.Initialize(mProgressBarBackViewport);
			TimeSystem timeSystem = (TimeSystem)mTimerSequence.GetChild(0);
			timeSystem.SetTotalTime(0);
			mTimerSequence.StartRecursively();
			if (flag && mPopupContext == 1)
			{
				timeSystem.OnTime(467, 467);
			}
			else
			{
				timeSystem.OnTime(0, 0);
				mTimerSequence.RegisterInGlobalTime();
				if (!flag)
				{
					timeSystem.Pause();
				}
			}
			int entryPoint = 17 + mCurrentFeat;
			FlString caption = (flag ? Utilities.GetUnlockedCareerFeatString(mCurrentFeat) : EntryPoint.GetFlString(mContentMetaPackage.GetPackage(), entryPoint));
			mDescriptionText.SetCaption(caption);
			short top = (short)((mDescriptionText.GetViewport().GetRectHeight() - mDescriptionText.GetRectHeight()) / 2);
			mDescriptionText.SetTopLeft(mDescriptionText.GetRectLeft(), top);
			mTimerSequence.Stop();
			InitializeAnimation();
		}

		public override void Show()
		{
			SetSoftKeys();
			base.Show();
		}

		public override void OnShowPopup()
		{
			mTimerSequence.Start();
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override void SetSoftKeys()
		{
			if (mPopupContext == 0)
			{
				mSelectSoftKey.SetFunction(0, 4);
				mClearSoftKey.SetFunction(2, 4);
			}
			else if (mPopupContext == 1)
			{
				mSelectSoftKey.SetFunction(7, 0);
				mClearSoftKey.SetFunction(1, 4);
			}
			base.SetSoftKeys();
		}

		public virtual void SetCareerFeat(sbyte careerFeat)
		{
			mCurrentFeat = careerFeat;
		}

		public override bool TapHidesPopup()
		{
			return true;
		}

		public virtual void InitializeAnimation()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			int careerFeatPercent = featsExpert.GetCareerFeatPercent(mCurrentFeat);
			int rectWidth = mProgressBarBackViewport.GetRectWidth();
			int num = 4;
			int num2 = 2;
			mProgressBarFront.SetVisible(careerFeatPercent > 0);
			mProgressBarController.SetControllee(mProgressBarFront);
			if (careerFeatPercent > 0)
			{
				mProgressBarFrontViewport.SetSize((short)num, mProgressBarFrontViewport.GetRectHeight());
				mProgressBarFront.Initialize(mProgressBarFrontViewport);
				int num3 = ((careerFeatPercent > num2) ? (careerFeatPercent * rectWidth / 100) : num);
				KeyFrameSequence keySequence = mProgressBarController.GetKeySequence();
				int[] array = new int[2]
				{
					num,
					mProgressBarFront.GetRectHeight()
				};
				keySequence.SetKeyFrame(0, 0, array);
				array[0] = num3;
				keySequence.SetKeyFrame(1, 300, array);
			}
		}
	}
}
