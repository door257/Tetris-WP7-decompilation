using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LanguageMenu : SelectorMenu
	{
		public LanguageMenu(int sceneId, int pkgId)
			: base(sceneId, pkgId)
		{
			mType = 4;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			short applicationLanguage = GameApp.Get().GetSettings().GetApplicationLanguage();
			mFocusedSelectionIndex = GetLanguageSelectionIndex(applicationLanguage);
			HorizontalSelector.Initialize(mSelector, mFocusedSelectionIndex);
			mSelectSoftKey.SetFunction(0, -10);
			if (GetId() == 15)
			{
				mClearSoftKey.SetFunction(1, -12);
			}
			else
			{
				mClearSoftKey.SetFunction(2, -11);
			}
		}

		public override void Unload()
		{
			if (GetId() == 15 && mSelector != null)
			{
				mSelector.SetViewport(mViewport);
				mSelector.SendToBack();
			}
			GameApp.Get().GetSharedResourcesHandler().ReleaseMenusResources();
			GameApp.Get().GetSharedResourcesHandler().ReleaseAppResources();
			base.Unload();
		}

		public override bool OnCommand(int command)
		{
			bool flag = base.OnCommand(command);
			if (!flag && command <= -90 && command >= -111)
			{
				GameApp gameApp = GameApp.Get();
				Settings settings = gameApp.GetSettings();
				short num = (short)GetLanguageFromCommand(command);
				settings.SetApplicationLanguage(num);
				settings.SetUserSelectedLanguage(num);
				flag = base.OnCommand(-2);
			}
			return flag;
		}

		public override void StartOpeningAnims()
		{
		}

		public override void StartClosingAnims()
		{
		}

		public override void CreateOpeningAnims()
		{
		}

		public override void CreateClosingAnims(int startTime)
		{
		}

		public virtual int GetLanguageFromCommand(int command)
		{
			switch (command)
			{
			case -94:
				return 11;
			case -95:
				return 14;
			case -90:
				return 9;
			case -96:
				return 19;
			case -92:
				return 12;
			default:
				return 1;
			}
		}

		public virtual int GetLanguageSelectionIndex(int language)
		{
			int languageCount = LanguageManager.GetLanguageCount();
			for (int i = 0; i < languageCount; i++)
			{
				Selection selectionAt = mSelector.GetSelectionAt(i);
				int command = selectionAt.GetCommand();
				int languageFromCommand = GetLanguageFromCommand(command);
				if (language == languageFromCommand)
				{
					return i;
				}
			}
			return 0;
		}
	}
}
