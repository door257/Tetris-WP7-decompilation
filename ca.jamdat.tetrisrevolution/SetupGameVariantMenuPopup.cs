using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SetupGameVariantMenuPopup : Popup
	{
		public int mVariant;

		public IndexedSprite mIconSprite;

		public Text mTitleText;

		public Text mTimeText;

		public Text mScoreText;

		public Viewport mStatsViewport;

		public Text mHintText;

		public SetupGameVariantMenuPopup(BaseScene baseScene, int variant, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mVariant = variant;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2752596);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mTitleText = EntryPoint.GetText(package, 0);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			mTimeText = EntryPoint.GetText(package, 6);
			mScoreText = EntryPoint.GetText(package, 7);
			mStatsViewport = EntryPoint.GetViewport(package, 8);
			mHintText = EntryPoint.GetText(package, 2);
			mIconSprite = EntryPoint.GetIndexedSprite(package, 5);
		}

		public override void Initialize()
		{
			base.Initialize();
			FlString flString = new FlString("_");
			flString.AddAssign(Utilities.GetGameVariantString(mVariant));
			mTitleText.SetCaption(flString);
			mIconSprite.SetCurrentFrame(Utilities.GetVariantIconFrameIndex(mVariant));
			mHintText.SetCaption(new FlString(Utilities.GetGameStringFromPackage(59 + 4 * mVariant)));
			int rectHeight = mHintText.GetRectHeight();
			int rectHeight2 = mIconSprite.GetRectHeight();
			if (rectHeight < rectHeight2)
			{
				mHintText.SetTopLeft(mHintText.GetRectLeft(), (short)FlMath.Absolute((rectHeight2 - rectHeight) / 2));
			}
			else
			{
				mHintText.SetTopLeft(mHintText.GetRectLeft(), 0);
			}
			int rectHeight3 = mTimeText.GetViewport().GetRectHeight();
			int lineHeight = mTimeText.GetLineHeight();
			mTimeText.SetCaption(Utilities.CreateMSTimeStringFromMillisecond(GameApp.Get().GetCareerStatistics().GetStatistic(mVariant, 1)));
			mTimeText.SetSize(mTimeText.GetRectWidth(), (short)lineHeight);
			mTimeText.SetTopLeft(mTimeText.GetRectLeft(), (short)((rectHeight3 - lineHeight) / 2));
			mScoreText.SetCaption(new FlString(GameApp.Get().GetCareerStatistics().GetStatistic(mVariant, 0)));
			mScoreText.SetSize(mScoreText.GetRectWidth(), (short)lineHeight);
			mScoreText.SetTopLeft(mScoreText.GetRectLeft(), (short)((rectHeight3 - lineHeight) / 2));
			UseDefaultLayout();
			if (StatsViewportOverflows())
			{
				UseNoPaddingLayout();
				if (StatsViewportOverflows())
				{
					UseNoIconLayout();
				}
			}
			mSelectSoftKey.SetFunction(3, 35);
			mClearSoftKey.SetFunction(1, 4);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool flag = false;
			if (command == 98)
			{
				flag = GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(35);
			}
			if (!flag)
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, popupSelector);
			}
			return true;
		}

		public virtual bool StatsViewportOverflows()
		{
			return mStatsViewport.GetBottom() + 1 > mContentScroller.GetRectHeight();
		}

		public virtual void UseDefaultLayout()
		{
			mIconSprite.SetVisible(true);
			mHintText.SetSize(298, mHintText.GetRectHeight());
			mHintText.SetTopLeft(104, 0);
			mHintText.OnRectChange();
			short num = ((mIconSprite.GetRectHeight() > mHintText.GetRectHeight()) ? mIconSprite.GetRectHeight() : mHintText.GetRectHeight());
			short top = (short)(num + 14);
			mStatsViewport.SetTopLeft(mStatsViewport.GetRectLeft(), top);
			mContentScroller.SetTopLeft(mContentScroller.GetRectLeft(), 86);
			mContentScroller.SetSize(mContentScroller.GetRectWidth(), 532);
			mContentScroller.GetScrollerViewport().SetSize(mContentScroller.GetRectWidth(), 532);
		}

		public virtual void UseNoPaddingLayout()
		{
			mStatsViewport.SetTopLeft(mStatsViewport.GetRectLeft(), mHintText.GetRectHeight());
			mContentScroller.SetTopLeft(mContentScroller.GetRectLeft(), 72);
			mContentScroller.SetSize(mContentScroller.GetRectWidth(), 560);
			mContentScroller.GetScrollerViewport().SetSize(mContentScroller.GetRectWidth(), 560);
		}

		public virtual void UseNoIconLayout()
		{
			mIconSprite.SetVisible(false);
			mHintText.SetTopLeft(0, 0);
			mHintText.SetSize(mContentScroller.GetRectWidth(), mHintText.GetRectHeight());
			mHintText.OnRectChange();
			short top = (short)(mHintText.GetLineHeight() * mHintText.GetNbLines() + 14);
			mStatsViewport.SetTopLeft(mStatsViewport.GetRectLeft(), top);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public override void OnShowPopup()
		{
			base.OnShowPopup();
			AddTouchToContinueZone();
		}
	}
}
