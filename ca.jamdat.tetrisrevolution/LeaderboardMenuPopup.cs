using System;
using System.IO;
using ca.jamdat.flight;
using Microsoft.Xna.Framework.GamerServices;
using Tetris;
using Tetris.tetrisrevolution;

namespace ca.jamdat.tetrisrevolution
{
	public class LeaderboardMenuPopup : Popup
	{
		public Selector mSelector;

		public int mCurrentlyDisplayedTopic;

		private Sprite mPopupBgSprite;

		private Selection mClonableSelection;

		private Text mYourRankText;

		private Text mYourScoreText;

		private Text mRankText;

		private Text mLevelText;

		private Text mTPMText;

		private Text mLoadingText;

		private Popup mMessagePopup;

		private FlString mErrorString;

		private FlString mLoadingString;

		private FlString mEmptyString;

		private FlString mUseLiveConnectString;

		private LeaderboardReader mReader;

		private bool mRefreshStats;

		private bool mReadLeaderboard;

		private bool mHasExitedScreen;

		private LeaderboardStats[] mStatsArray = new LeaderboardStats[100];

		public LeaderboardMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mCurrentlyDisplayedTopic = -1;
			mAutoResize = false;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1605681);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetSelector(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 4);
			mPopupBgSprite = EntryPoint.GetSprite(package, 6);
			mClonableSelection = EntryPoint.GetSelection(package, 5);
			mYourRankText = EntryPoint.GetText(package, 10);
			mYourScoreText = EntryPoint.GetText(package, 11);
			mRankText = EntryPoint.GetText(package, 7);
			mLevelText = EntryPoint.GetText(package, 8);
			mTPMText = EntryPoint.GetText(package, 9);
			mLoadingText = EntryPoint.GetText(package, 12);
			mErrorString = EntryPoint.GetFlString(-2144239522, 31);
			mLoadingString = EntryPoint.GetFlString(-2144239522, 32);
			mUseLiveConnectString = EntryPoint.GetFlString(-2144239522, 33);
			mEmptyString = EntryPoint.GetFlString(40, 24);
		}

		public override void Initialize()
		{
			base.Initialize();
			mHasExitedScreen = false;
			mCurrentlyDisplayedTopic = -1;
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, 4);
			HorizontalSelector.Initialize(mSelector, 0);
			mRankText.SetCaption(mEmptyString);
			mLevelText.SetCaption(mEmptyString);
			mTPMText.SetCaption(mEmptyString);
			mLoadingText.SetVisible(false);
			mSelector.EventIndexChanged += mSelector_EventIndexChanged;
			LeaderboardImpl.Get.EventError += LeaderboardImpl_EventError;
			LeaderboardImpl.Get.ResetState();
			if (!LiveState.GamerServicesActive)
			{
				ShowMessagePopup(mUseLiveConnectString);
			}
		}

		private void LeaderboardImpl_EventError(object sender, EventArgs e)
		{
			if (!mHasExitedScreen)
			{
				ShowMessagePopup(mErrorString);
			}
		}

		public override void Unload()
		{
			if (mContentScroller != null)
			{
				mContentScroller = null;
			}
			mHasExitedScreen = true;
			FrameworkGlobals.GetInstance().mFlPenManager.Activate();
			base.Unload();
		}

		public int GetLeaderboardIndex()
		{
			if (mSelector != null)
			{
				return mSelector.GetSingleSelection();
			}
			return -1;
		}

		public int GetStatsIndex()
		{
			if (mContentScroller != null)
			{
				return ((Selector)mContentScroller).GetSingleSelection();
			}
			return -1;
		}

		private void mSelector_EventIndexChanged(object sender, EventArgs e)
		{
			mReadLeaderboard = true;
		}

		public override bool HasFocus()
		{
			if (!mSelector.DescendentOrSelfHasFocus())
			{
				return base.HasFocus();
			}
			return true;
		}

		public override void TakeFocus()
		{
			mSelector.SetSingleSelection(0, true);
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (msg == -127)
			{
				flag = OnSelectedMsg((Selection)source, intParam);
			}
			if (!flag && (mContentScroller == null || !mContentScroller.OnDefaultMsg(source, msg, intParam)))
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public virtual bool OnSelectedMsg(Selection source, int intParam)
		{
			bool result = false;
			if (source.GetSubtype() == -20 && mReader != null && mReader.Entries.Count > 0)
			{
				mRefreshStats = true;
			}
			return result;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			if (mRefreshStats)
			{
				UpdateStats();
			}
			if (mReadLeaderboard)
			{
				ReadLeaderboard();
			}
		}

		private void ClearLeaderboard()
		{
			int numSelections = ((Selector)mContentScroller).GetNumSelections();
			if (numSelections > 0)
			{
				for (int i = 1; i < numSelections; i++)
				{
					Utilities.RemoveSelection((Selector)mContentScroller, 1);
				}
				mContentScroller.GetElementAt(0).SetVisible(false);
			}
			mYourRankText.SetCaption(new FlString());
			mYourScoreText.SetCaption(new FlString());
			mRankText.SetCaption(new FlString());
			mLevelText.SetCaption(new FlString());
			mTPMText.SetCaption(new FlString());
		}

		private void ReadLeaderboard()
		{
			try
			{
				if (!GameApp.Get().IsInUpdate())
				{
					ClearLeaderboard();
					mLoadingText.SetVisible(true);
					FrameworkGlobals.GetInstance().mFlPenManager.Deactivate();
					LeaderboardImpl.Get.Read(GetLeaderboardIndex(), LeaderboardReadCallback, false);
					mReadLeaderboard = false;
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected void LeaderboardReadCallback(IAsyncResult result)
		{
			try
			{
				mLoadingText.SetVisible(false);
				UpdateLeaderboard();
				FrameworkGlobals.GetInstance().mFlPenManager.Activate();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				if (!mHasExitedScreen)
				{
					ShowMessagePopup(mErrorString);
				}
			}
		}

		private void UpdateLeaderboard()
		{
			ClearLeaderboard();
			mReader = LeaderboardImpl.Get.GetLeaderboard(GetLeaderboardIndex()).GetReader();
			if (mReader != null && mReader.Entries.Count > 0)
			{
				for (int i = 0; i < mReader.Entries.Count; i++)
				{
					LeaderboardEntry leaderboardEntry = mReader.Entries[i];
					System.IO.Stream valueStream = leaderboardEntry.Columns.GetValueStream("BestScoreBlob");
					int count = (int)valueStream.Length;
					BinaryReader binaryReader = new BinaryReader(valueStream);
					byte[] stats = binaryReader.ReadBytes(count);
					mStatsArray[i] = new LeaderboardStats(stats);
					if (i == 0)
					{
						((Text)mClonableSelection.GetChild(1)).SetCaption(new FlString(leaderboardEntry.Gamer.Gamertag));
						((Text)mClonableSelection.GetChild(2)).SetCaption(new FlString(leaderboardEntry.Rating));
						mClonableSelection.SetEnabledState(true);
						mClonableSelection.SetVisible(true);
					}
					else
					{
						Selection selection = CloneSelection();
						((Text)selection.GetChild(1)).SetCaption(new FlString(leaderboardEntry.Gamer.Gamertag));
						((Text)selection.GetChild(2)).SetCaption(new FlString(leaderboardEntry.Rating));
						Utilities.AddSelection((Selector)mContentScroller, selection, i);
					}
					mContentScroller.SetIsViewportCentered(false);
					if (leaderboardEntry.Gamer.Gamertag == LiveState.Gamer.Gamertag)
					{
						mYourRankText.SetCaption(new FlString(i + 1));
						mYourScoreText.SetCaption(new FlString(leaderboardEntry.Rating));
					}
				}
			}
			else
			{
				((Text)mClonableSelection.GetChild(1)).SetCaption(new FlString());
				((Text)mClonableSelection.GetChild(2)).SetCaption(new FlString());
				mYourRankText.SetCaption(new FlString());
				mYourScoreText.SetCaption(new FlString());
				mClonableSelection.SetEnabledState(false);
				mRankText.SetCaption(new FlString());
				mLevelText.SetCaption(new FlString());
				mTPMText.SetCaption(new FlString());
			}
			if (GetLeaderboardIndex() != mCurrentlyDisplayedTopic)
			{
				mCurrentlyDisplayedTopic = GetLeaderboardIndex();
				VerticalSelector.Initialize((Selector)mContentScroller, 5, 0);
				if (mReader != null && mReader.Entries.Count > 0)
				{
					UpdateStats();
				}
			}
		}

		private void UpdateStats()
		{
			mRefreshStats = false;
			int statsIndex = GetStatsIndex();
			LeaderboardStats leaderboardStats = mStatsArray[statsIndex];
			mRankText.SetCaption(new FlString(statsIndex + 1));
			mLevelText.SetCaption(new FlString(leaderboardStats.Level));
			mTPMText.SetCaption(new FlString(leaderboardStats.TPM));
		}

		private void ShowMessagePopup(FlString errorMsg)
		{
			mPopupBgSprite.SetTopLeft(-21, -46);
			mPopupBgSprite.SetVisible(true);
			mMessagePopup = Popup.CreateLiveConnectPopup(27, mBaseScene, mSelectSoftKey, mClearSoftKey, errorMsg);
			mMessagePopup.SetAnimationType(0);
			mMessagePopup.Load();
			mMessagePopup.GetEntryPoints();
			mMessagePopup.Initialize();
			mMessagePopup.AttachPopupViewport(mPopupViewport);
			mMessagePopup.Show();
		}

		private Selection CloneSelection()
		{
			Selection selection = new Selection();
			Text text = new Text();
			Text text2 = new Text();
			Sprite sprite = new Sprite();
			Text text3 = (Text)mClonableSelection.GetChild(1);
			Text text4 = (Text)mClonableSelection.GetChild(2);
			Sprite sprite2 = (Sprite)mClonableSelection.GetChild(0);
			sprite.SetViewport(selection);
			text.SetViewport(selection);
			text2.SetViewport(selection);
			selection.SetRect(mClonableSelection.GetRectLeft(), mClonableSelection.GetRectTop(), mClonableSelection.GetRectWidth(), mClonableSelection.GetRectHeight());
			selection.SetSubtype(mClonableSelection.GetSubtype());
			selection.SetCommand(mClonableSelection.GetCommand());
			selection.SetEnabledState(true);
			text.SetRect(text3.GetRectLeft(), text3.GetRectTop(), text3.GetRectWidth(), text3.GetRectHeight());
			text.SetAlignment(text3.GetAlignment());
			text.SetFont(text3.GetFont());
			text.SetCaptionLength(text3.GetRectWidth());
			text2.SetRect(text4.GetRectLeft(), text4.GetRectTop(), text4.GetRectWidth(), text4.GetRectHeight());
			text2.SetAlignment(text4.GetAlignment());
			text2.SetFont(text4.GetFont());
			text2.SetCaptionLength(text4.GetRectWidth());
			sprite.SetBitmap(sprite2.GetBitmap());
			sprite.SetVisible(sprite2.IsVisible());
			sprite.SetRect(sprite2.GetRectLeft(), sprite2.GetRectTop(), sprite2.GetRectWidth(), sprite2.GetRectHeight());
			return selection;
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
