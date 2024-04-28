using System;
using ca.jamdat.flight;
using Microsoft.Xna.Framework.GamerServices;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public class AchievementsMenuPopup : Popup
	{
		private object mAchievementLockObject = new object();

		private Sprite mPopupBgSprite;

		private Popup mMessagePopup;

		private FlString mHeader1String;

		private FlString mHeader2String;

		private FlString mUseLiveConnectString;

		private Text mHeader1Text;

		private Text mHeader2Text;

		private FlFont mNameFont;

		private FlFont mDescFont;

		public AchievementsMenuPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAutoResize = false;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(1671219);
		}

		public override void GetEntryPoints()
		{
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			Selector selector = EntryPoint.GetSelector(package, 2);
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
			mPopupBgSprite = EntryPoint.GetSprite(package, 4);
			mUseLiveConnectString = EntryPoint.GetFlString(-2144239522, 33);
			mHeader1Text = EntryPoint.GetText(package, 5);
			mHeader2Text = EntryPoint.GetText(package, 6);
			mHeader1String = EntryPoint.GetFlString(-2144239522, 34);
			mHeader2String = EntryPoint.GetFlString(-2144239522, 35);
			mNameFont = EntryPoint.GetFlFont(package, 7);
			mDescFont = EntryPoint.GetFlFont(package, 8);
			VerticalSelector.Initialize(selector, 0, 0);
			mContentScroller = selector;
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(0, 0);
			mClearSoftKey.SetFunction(1, 4);
			((Selector)mContentScroller).IsSelectionScrollerEnabled = true;
			if (LiveState.GamerServicesActive)
			{
				LiveState.Gamer.BeginGetAchievements(GetAchievementsCallback, LiveState.Gamer);
			}
			else
			{
				ShowMessagePopup(mUseLiveConnectString);
			}
		}

		public override void Unload()
		{
			if (mContentScroller != null)
			{
				VerticalSelector.Uninitialize((Selector)mContentScroller);
			}
			base.Unload();
		}

		public override void TakeFocus()
		{
			mContentScroller.TakeFocus();
		}

		protected void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer == null)
			{
				return;
			}
			lock (mAchievementLockObject)
			{
				UpdateAchievements();
			}
		}

		private void UpdateAchievements()
		{
			AchievementCollection achievements = LiveState.Gamer.GetAchievements();
			int count = achievements.Count;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				Achievement achievement = achievements[i];
				int gamerScore = achievement.GamerScore;
				bool isEarned = achievement.IsEarned;
				int indexFromAchievementKey = GetIndexFromAchievementKey(achievement.Key);
				Text text = (Text)((Selection)mContentScroller.GetElementAt(indexFromAchievementKey)).GetChild(3);
				FlString flString = new FlString(gamerScore);
				flString.AddAssign(" G");
				text.SetCaption(flString);
				if (isEarned)
				{
					text.SetFont(mNameFont);
					num++;
					num2 += gamerScore;
					Viewport viewport = (Viewport)((Selection)mContentScroller.GetElementAt(indexFromAchievementKey)).GetChild(2);
					Text text2 = (Text)((Selection)mContentScroller.GetElementAt(indexFromAchievementKey)).GetChild(1);
					text2.SetFont(mNameFont);
					Text text3 = (Text)viewport.GetChild(0);
					text3.SetFont(mDescFont);
					IndexedSprite indexedSprite = (IndexedSprite)viewport.GetChild(2);
					if (indexedSprite.GetCurrentFrame() >= 11)
					{
						indexedSprite.SetCurrentFrame(indexedSprite.GetCurrentFrame() - count);
					}
					Sprite sprite = (Sprite)viewport.GetChild(1);
					sprite.SetVisible(false);
				}
			}
			FlString flString2 = new FlString(num2);
			flString2.AddAssign(mHeader1String);
			mHeader1Text.SetCaption(flString2);
			flString2 = new FlString(num);
			flString2.AddAssign(mHeader2String);
			mHeader2Text.SetCaption(flString2);
		}

		private int GetIndexFromAchievementKey(string key)
		{
			int result = 0;
			switch (key)
			{
			case "Tetriminator":
				result = 0;
				break;
			case "Millenium":
				result = 1;
				break;
			case "Waterfall":
				result = 2;
				break;
			case "Spinmaster":
				result = 3;
				break;
			case "Tenacious":
				result = 4;
				break;
			case "Master":
				result = 5;
				break;
			case "WayOver":
				result = 6;
				break;
			case "Expert":
				result = 7;
				break;
			case "Dodecadent":
				result = 8;
				break;
			case "OnARoll":
				result = 9;
				break;
			case "Frenzy":
				result = 10;
				break;
			}
			return result;
		}

		private void ShowMessagePopup(FlString errorMsg)
		{
			mPopupBgSprite.SetTopLeft(-21, -46);
			mPopupBgSprite.SetVisible(true);
			mMessagePopup = Popup.CreateLiveConnectPopup(99, mBaseScene, mSelectSoftKey, mClearSoftKey, errorMsg);
			mMessagePopup.SetAnimationType(0);
			mMessagePopup.Load();
			mMessagePopup.GetEntryPoints();
			mMessagePopup.Initialize();
			mMessagePopup.AttachPopupViewport(mPopupViewport);
			mMessagePopup.Show();
		}
	}
}
