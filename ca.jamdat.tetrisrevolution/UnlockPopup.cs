using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class UnlockPopup : Popup
	{
		public const int endOfGameContext = 0;

		public const int highlightsMenuContext = 1;

		public int mGameVariant;

		public Text mNameText;

		public Text mDescriptionText;

		public Text mCompletedText;

		public IndexedSprite mUnlockedIndexedSprite;

		public TimeSystem mTimeSystem;

		public int mPopupContext;

		public UnlockPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mGameVariant = -1;
			mPopupContext = 1;
		}

		public UnlockPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, int gameVariant)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mGameVariant = gameVariant;
			mPopupContext = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(294921);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mNameText = EntryPoint.GetText(package, 2);
			mDescriptionText = EntryPoint.GetText(package, 3);
			mCompletedText = EntryPoint.GetText(package, 4);
			mUnlockedIndexedSprite = EntryPoint.GetIndexedSprite(package, 7);
			mTimeSystem = EntryPoint.GetTimeSystem(package, 10);
			if (!IsRoomForUnlockIcon())
			{
				mUnlockedIndexedSprite.SetViewport(null);
				EntryPoint.GetComponent(package, 8).SetViewport(null);
				EntryPoint.GetComponent(package, 9).SetViewport(null);
			}
			Text text = EntryPoint.GetText(package, 0);
			FlString caption = null;
			if (mPopupContext == 0)
			{
				caption = EntryPoint.GetFlString(package, 6);
			}
			else if (mPopupContext == 1)
			{
				caption = EntryPoint.GetFlString(package, 5);
			}
			text.SetCaption(caption);
		}

		public override void Initialize()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool flag = featsExpert.IsGameVariantUnlocked(mGameVariant);
			mTimeSystem.SetTotalTime(0);
			if (flag)
			{
				if (mPopupContext == 0 && IsRoomForUnlockIcon())
				{
					mTimeSystem.OnTime(0, 0);
					mTimeSystem.RegisterInGlobalTime();
				}
				else
				{
					mTimeSystem.OnTime(508, 508);
				}
				mNameText.SetCaption(Utilities.GetGameVariantString(mGameVariant));
				mDescriptionText.SetCaption(Utilities.GetUnlockedVariantString(mGameVariant));
			}
			else
			{
				mTimeSystem.OnTime(0, 0);
				FlString flString = EntryPoint.GetFlString(-2144239522, 151);
				mNameText.SetCaption(flString);
				mDescriptionText.SetCaption(Utilities.GetFeatDescriptionString(mGameVariant));
			}
			mTimeSystem.Stop();
			int entryPoint = (flag ? 123 : 124);
			mCompletedText.SetCaption(Utilities.GetStringFromPackage(entryPoint, -2144239522));
			short top = (short)((mDescriptionText.GetViewport().GetRectHeight() - mDescriptionText.GetRectHeight()) / 2);
			mDescriptionText.SetTopLeft(mDescriptionText.GetRectLeft(), top);
			mUnlockedIndexedSprite.SetCurrentFrame(Utilities.GetVariantIconFrameIndex(mGameVariant));
			base.Initialize();
		}

		public override void Unload()
		{
			mCompletedText = null;
			mNameText = null;
			mDescriptionText = null;
			mCompletedText = null;
			mUnlockedIndexedSprite = null;
			if (mTimeSystem != null)
			{
				mTimeSystem.UnRegisterInGlobalTime();
				mTimeSystem = null;
			}
			base.Unload();
		}

		public override void Show()
		{
			base.Show();
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
		}

		public override void OnShowPopup()
		{
			mTimeSystem.Start();
			base.OnShowPopup();
			AddTouchToContinueZone();
		}

		public virtual void SetVariant(int gameVariant)
		{
			mGameVariant = gameVariant;
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
