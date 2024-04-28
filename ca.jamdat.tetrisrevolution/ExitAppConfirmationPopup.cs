using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ExitAppConfirmationPopup : Popup
	{
		public ExitAppConfirmationPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mPopupDuration = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(32769);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mContentScroller = EntryPoint.GetScroller(package, 4);
			mScrollbarViewport = EntryPoint.GetViewport(package, 5);
		}

		public override void Initialize()
		{
			base.Initialize();
			Package package = mContentMetaPackage.GetPackage();
			if (mBaseScene.IsTypeOf(16))
			{
				Selection selection = EntryPoint.GetSelection(package, 3);
				selection.SetCommand(-19);
			}
			EntryPoint.GetText(package, 6);
		}

		public override void Show()
		{
			SetSoftKeys();
			base.Show();
		}

		public override void SetSoftKeys()
		{
			mSelectSoftKey.SetFunction(0, -10);
			if (mBaseScene.GetId() == 9)
			{
				mClearSoftKey.SetFunction(1, 4);
			}
			else if (mBaseScene.GetId() == 40)
			{
				mClearSoftKey.SetFunction(1, -19);
			}
			base.SetSoftKeys();
		}

		public override bool OnCommand(int command)
		{
			bool flag = false;
			if (command == -11)
			{
				flag = GameApp.Get().GetCommandHandler().Execute(-11);
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}
	}
}
