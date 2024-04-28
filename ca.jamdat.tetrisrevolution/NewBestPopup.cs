using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class NewBestPopup : Popup
	{
		public Text mTitleText;

		public Text mText;

		public sbyte mStatId;

		public NewBestPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, sbyte id)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mStatId = id;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(262152);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mTitleText = EntryPoint.GetText(package, 0);
			mText = EntryPoint.GetText(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
		}

		public override void Show()
		{
			GameController gameController = (GameController)mBaseScene;
			TetrisGame game = gameController.GetGame();
			FlString flString = new FlString("_");
			flString.AddAssign(Utilities.GetGameVariantString(game.GetVariant()));
			mTitleText.SetCaption(flString);
			FlString flString2 = new FlString(Utilities.GetStringFromPackage(5, 262152));
			if (mStatId == 0)
			{
				flString2.AddAssign(Utilities.GetStringFromPackage(6, 262152));
			}
			else if (mStatId == 1)
			{
				flString2.AddAssign(Utilities.GetStringFromPackage(8, 262152));
			}
			else if (mStatId == 2)
			{
				flString2.AddAssign(Utilities.GetStringFromPackage(7, 262152));
			}
			flString2.AddAssign("\n");
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			int variant = game.GetVariant();
			StatisticsFormatting.AddStatisticToString(flString2, careerStatistics.GetStatistic(variant, mStatId), StatisticsFormatting.GetCareerHighestStatisticType(mStatId));
			VerticalTextScroller.Initialize(mContentScroller, flString2);
			SetSoftKeys();
			base.Show();
		}

		public override void OnShowPopup()
		{
			mContentScroller.TakeFocus();
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override void SetSoftKeys()
		{
			mSelectSoftKey.SetFunction(0, 4);
			mClearSoftKey.SetFunction(2, 4);
			base.SetSoftKeys();
		}

		public override bool TapHidesPopup()
		{
			return true;
		}
	}
}
