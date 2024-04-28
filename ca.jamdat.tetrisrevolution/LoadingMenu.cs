using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LoadingMenu : Menu
	{
		public bool mCyclingTipFlag;

		public int mLoadingDuration;

		public int mTimeElapsed;

		public LoadingMenu()
			: base(19, 1998909)
		{
			mType = 2;
		}

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			GameApp gameApp = GameApp.Get();
			int tipsStringIdx = gameApp.GetTipChooser().GetTipsStringIdx();
			InitializeTipText(tipsStringIdx);
		}

		public override void Unload()
		{
			if (mView != null)
			{
				mView.UnRegisterInGlobalTime();
			}
			base.Unload();
		}

		public override void Initialize()
		{
			base.Initialize();
			mLoadingDuration = 2000;
			mView.RegisterInGlobalTime();
			mClearSoftKey.SetFunction(7, 0);
			mSelectSoftKey.SetFunction(3, -41);
		}

		public override void OnSceneAttached()
		{
			base.OnSceneAttached();
			GameApp.Get().GetHourglass().SetTopLeft(224, 702);
		}

		public override void SerializeObjects()
		{
			GameApp.Get().GetFileManager().WriteObject(5);
		}

		public override void OnOpeningAnimsEnded()
		{
			base.OnOpeningAnimsEnded();
			mClearSoftKey.SetEnabled(false);
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			mTimeElapsed += deltaTimeMs;
			if (mTimeElapsed > mLoadingDuration)
			{
				mView.UnRegisterInGlobalTime();
				OnCommand(-41);
			}
		}

		public override bool OnKeyDown(int key)
		{
			bool result = true;
			if (mCyclingTipFlag && key == 4)
			{
				CycleToNextTip();
			}
			else
			{
				result = base.OnKeyDown(key);
			}
			return result;
		}

		public virtual void ForceCyclingTipFlag()
		{
			mCyclingTipFlag = true;
		}

		public override void StartMusic()
		{
		}

		public virtual void InitializeTipText(int gameNameShortHintIdx)
		{
			GameApp gameApp = GameApp.Get();
			FlString flString = null;
			Text text = EntryPoint.GetText(mPackage, 2);
			Text text2 = EntryPoint.GetText(mPackage, 3);
			int gameVariant = gameApp.GetGameSettings().GetGameVariant();
			int entryPoint = 51 + gameVariant;
			flString = ((gameApp.GetGameSettings().GetCurrentGameMode() != 0) ? EntryPoint.GetFlString(-2144239522, entryPoint) : EntryPoint.GetFlString(-2144239522, 10));
			text.SetCaption(flString);
			flString = EntryPoint.GetFlString(mPackage, gameNameShortHintIdx);
			text2.SetCaption(flString);
			Viewport viewport = text2.GetViewport();
			viewport.SetSize(viewport.GetRectWidth(), (short)(text2.GetBottom() + 1));
			short num = 734;
			short top = (short)((num - viewport.GetRectHeight()) / 2);
			viewport.SetTopLeft(viewport.GetRectLeft(), top);
			IndexedSprite indexedSprite = null;
			indexedSprite = IndexedSprite.Cast(mPackage.GetEntryPoint(5), null);
			indexedSprite.SetCurrentFrame(Utilities.GetVariantIconFrameIndex(GameApp.Get().GetGameSettings().GetGameVariant()));
		}

		public virtual void CycleToNextTip()
		{
			GameApp gameApp = GameApp.Get();
			InitializeTipText(gameApp.GetTipChooser().GetNextCyclingTipsStringIdx());
		}

		public static LoadingMenu[] InstArrayLoadingMenu(int size)
		{
			LoadingMenu[] array = new LoadingMenu[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LoadingMenu();
			}
			return array;
		}

		public static LoadingMenu[][] InstArrayLoadingMenu(int size1, int size2)
		{
			LoadingMenu[][] array = new LoadingMenu[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LoadingMenu[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LoadingMenu();
				}
			}
			return array;
		}

		public static LoadingMenu[][][] InstArrayLoadingMenu(int size1, int size2, int size3)
		{
			LoadingMenu[][][] array = new LoadingMenu[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LoadingMenu[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LoadingMenu[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LoadingMenu();
					}
				}
			}
			return array;
		}
	}
}
