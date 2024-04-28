using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class AdvancedFeatPopup : Popup
	{
		public const int endOfGameContext = 0;

		public const int highlightsMenuContext = 1;

		public sbyte mAdvancedFeat;

		public int mPopupContext;

		public Text mDescriptionText;

		public Text mCompletedText;

		public Text mIncompleteText;

		public IndexedSprite mUnlockedIndexedSprite;

		public TimeSystem mTimeSystem;

		public AdvancedFeatPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAdvancedFeat = -1;
			mPopupContext = 1;
		}

		public AdvancedFeatPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, sbyte advancedFeat)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAdvancedFeat = advancedFeat;
			mPopupContext = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(393228);
		}

		public override void Unload()
		{
			mDescriptionText = null;
			mCompletedText = null;
			mIncompleteText = null;
			mUnlockedIndexedSprite = null;
			if (mTimeSystem != null)
			{
				mTimeSystem.UnRegisterInGlobalTime();
				mTimeSystem = null;
			}
			base.Unload();
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mCompletedText = EntryPoint.GetText(package, 4);
			mIncompleteText = EntryPoint.GetText(package, 5);
			mUnlockedIndexedSprite = EntryPoint.GetIndexedSprite(package, 8);
			if (!IsRoomForUnlockIcon())
			{
				EntryPoint.GetComponent(package, 7).SetViewport(null);
				EntryPoint.GetComponent(package, 8).SetViewport(null);
			}
			mTimeSystem = EntryPoint.GetTimeSystem(package, 6);
			mDescriptionText = EntryPoint.GetText(package, 3);
			int entryPoint = 9 + mAdvancedFeat;
			FlString flString = EntryPoint.GetFlString(package, entryPoint);
			Text text = EntryPoint.GetText(package, 2);
			text.SetCaption(flString);
		}

		public override void Initialize()
		{
			base.Initialize();
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool flag = featsExpert.IsAdvancedFeatUnlocked(mAdvancedFeat);
			mCompletedText.SetVisible(flag);
			mIncompleteText.SetVisible(!flag);
			mTimeSystem.SetTotalTime(0);
			if (flag)
			{
				if (mPopupContext == 0)
				{
					mTimeSystem.OnTime(0, 0);
					mTimeSystem.RegisterInGlobalTime();
				}
				else
				{
					mTimeSystem.OnTime(467, 467);
				}
				mDescriptionText.SetCaption(Utilities.GetUnlockedAdvancedFeatString(mAdvancedFeat));
			}
			else
			{
				mTimeSystem.OnTime(0, 0);
				int entryPoint = 14 + mAdvancedFeat;
				FlString flString = EntryPoint.GetFlString(mContentMetaPackage.GetPackage(), entryPoint);
				mDescriptionText.SetCaption(flString);
			}
			mTimeSystem.Stop();
			short top = (short)((mDescriptionText.GetViewport().GetRectHeight() - mDescriptionText.GetRectHeight()) / 2);
			mDescriptionText.SetTopLeft(mDescriptionText.GetRectLeft(), top);
		}

		public override void Show()
		{
			base.Show();
			SetSoftKeys();
		}

		public override void OnShowPopup()
		{
			if (IsRoomForUnlockIcon())
			{
				mTimeSystem.Start();
			}
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public override void SetSoftKeys()
		{
			if (mPopupContext == 1)
			{
				mSelectSoftKey.SetFunction(7, 0);
				mClearSoftKey.SetFunction(1, 4);
			}
			else if (mPopupContext == 0)
			{
				mSelectSoftKey.SetFunction(0, 4);
				mClearSoftKey.SetFunction(2, 4);
			}
			base.SetSoftKeys();
		}

		public virtual void SetAdvancedFeat(sbyte advancedFeat)
		{
			mAdvancedFeat = advancedFeat;
		}

		public static bool IsRoomForUnlockIcon()
		{
			return true;
		}

		public override bool TapHidesPopup()
		{
			return true;
		}
	}
}
