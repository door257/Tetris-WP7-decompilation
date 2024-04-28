using System;
using ca.jamdat.flight;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class GameStatsPopup : Popup
	{
		public GameStatsPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(425997);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentScroller = EntryPoint.GetScroller(package, 2);
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
			VerticalScroller.Initialize(mContentScroller, 0);
			GameStatistics gameStatistics = GameFactory.GetTetrisGame().GetGameStatistics();
			SetGameStatisticTextValue(gameStatistics, 0, 5);
			SetGameStatisticTextValue(gameStatistics, 19, 6);
			SetGameStatisticTextValue(gameStatistics, 2, 7);
			if (!GameFactory.GetTetrisGame().IsGravityEnabled())
			{
				Component viewport = EntryPoint.GetViewport(package, 4);
				int num = 0;
				for (num = 0; num < mContentScroller.GetNumElements(); num++)
				{
					if (mContentScroller.GetElementAt(num) == viewport)
					{
						VerticalScroller.RemoveElement(mContentScroller, num);
						break;
					}
				}
			}
			else
			{
				SetGameStatisticTextValue(gameStatistics, 15, 8);
			}
			Text text = EntryPoint.GetText(package, 9);
			text.SetCaption(StatisticsFormatting.CreateStatisticString(gameStatistics.GetAverageTPM(), 3));
			if (LiveState.GamerServicesActive)
			{
				try
				{
					LeaderboardImpl.Get.Write(gameStatistics, GameFactory.GetTetrisGame().GetGameScore());
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
			}
		}

		public override void Show()
		{
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

		public virtual void SetGameStatisticTextValue(GameStatistics gameStats, sbyte statID, int textEntryPoint)
		{
			Text text = EntryPoint.GetText(mContentMetaPackage.GetPackage(), textEntryPoint);
			int statistic = gameStats.GetStatistic(statID);
			sbyte gameStatisticType = StatisticsFormatting.GetGameStatisticType(statID);
			text.SetCaption(StatisticsFormatting.CreateStatisticString(statistic, gameStatisticType));
		}
	}
}
