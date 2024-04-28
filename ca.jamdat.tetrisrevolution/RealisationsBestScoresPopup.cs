using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsBestScoresPopup : Popup
	{
		public Selector mSelector;

		public Text mBestScoreText;

		public Text mBestTimeText;

		public Text mBestTPMText;

		public RealisationsBestScoresPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2916441);
		}

		public override void Unload()
		{
			if (mContentScroller != null)
			{
				VerticalScroller.Uninitialize(mContentScroller);
			}
			mSelector = null;
			mBestScoreText = null;
			mBestTimeText = null;
			mBestTPMText = null;
			base.Unload();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 4);
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
			mBestScoreText = EntryPoint.GetText(package, 5);
			mBestTimeText = EntryPoint.GetText(package, 6);
			mBestTPMText = EntryPoint.GetText(package, 7);
		}

		public override void Initialize()
		{
			base.Initialize();
			HorizontalSelector.Initialize(mSelector, 0);
			VerticalScroller.Initialize(mContentScroller, 0);
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, 4);
			CustomComponentUtilities.DisableLockedVariantSelections(mSelector);
			UpdateScores();
		}

		public override void TakeFocus()
		{
			mSelector.TakeFocus();
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (msg == -127 && intParam == 1)
			{
				UpdateScores();
			}
			return base.OnMsg(source, msg, intParam);
		}

		public virtual void UpdateScores()
		{
			int singleSelection = mSelector.GetSingleSelection();
			int gameType = singleSelection;
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			int statistic = careerStatistics.GetStatistic(gameType, 0);
			int statistic2 = careerStatistics.GetStatistic(gameType, 1);
			int statistic3 = careerStatistics.GetStatistic(gameType, 2);
			FlString caption = StatisticsFormatting.CreateStatisticString(statistic, 0);
			FlString caption2 = StatisticsFormatting.CreateStatisticString(statistic2, 1);
			FlString caption3 = StatisticsFormatting.CreateStatisticString(statistic3, 3);
			mBestScoreText.SetCaption(caption);
			mBestTimeText.SetCaption(caption2);
			mBestTPMText.SetCaption(caption3);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool flag = false;
			if (96 == command || 97 == command)
			{
				int advance = ((96 != command) ? 1 : (-1));
				mSelector.OnScrollEvent(advance);
			}
			if (!flag)
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, popupSelector);
			}
			return true;
		}
	}
}
