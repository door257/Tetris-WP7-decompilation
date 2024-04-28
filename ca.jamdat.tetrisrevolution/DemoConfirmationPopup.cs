using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class DemoConfirmationPopup : Popup
	{
		private sbyte mDemoPopupType;

		public DemoConfirmationPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey, sbyte demoPopupType)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mDemoPopupType = demoPopupType;
			mPopupDuration = 0;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(98307);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			Selector selector = EntryPoint.GetSelector(package, 2);
			mContentScroller = EntryPoint.GetScroller(package, 3);
			mScrollbarViewport = EntryPoint.GetViewport(package, 7);
			Text text = EntryPoint.GetText(package, 8);
			Text text2 = EntryPoint.GetText(package, 0);
			Selection selection = EntryPoint.GetSelection(package, 6);
			FlString caption = null;
			FlString caption2 = null;
			short command = 0;
			if (mDemoPopupType == 55)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 17);
				caption = EntryPoint.GetFlString(-2144239522, 18);
				EntryPoint.GetFlString(-2144239522, 24);
				command = 4;
			}
			else if (mDemoPopupType == 56)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 19);
				caption = EntryPoint.GetFlString(-2144239522, 20);
				EntryPoint.GetFlString(-2144239522, 24);
				command = 4;
			}
			else if (mDemoPopupType == 57)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 121);
				caption = EntryPoint.GetFlString(-2144239522, 18);
				EntryPoint.GetFlString(-2144239522, 8);
				command = -11;
			}
			else if (mDemoPopupType == 58)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 21);
				caption = EntryPoint.GetFlString(-2144239522, 18);
				EntryPoint.GetFlString(-2144239522, 8);
				command = -11;
			}
			else if (mDemoPopupType == 59)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 17);
				caption = EntryPoint.GetFlString(-2144239522, 18);
				EntryPoint.GetFlString(-2144239522, 24);
				command = -17;
			}
			else if (mDemoPopupType == 60)
			{
				caption2 = EntryPoint.GetFlString(-2144239522, 17);
				caption = EntryPoint.GetFlString(-2144239522, 18);
				EntryPoint.GetFlString(-2144239522, 24);
				command = 81;
			}
			text.SetCaption(caption);
			text2.SetCaption(caption2);
			selection.SetCommand(command);
			Viewport viewport = selector.GetViewport();
			viewport.SetTopLeft(selector.GetRectLeft(), (short)(text.GetRectTop() + text.GetRectHeight()));
			Viewport viewport2 = EntryPoint.GetViewport(package, 4);
			viewport2.SetTopLeft(viewport2.GetRectLeft(), (short)(text.GetRectTop() + text.GetRectHeight() - 11));
		}

		public override void Show()
		{
			if (mDemoPopupType == 57 && mBaseScene.GetId() == 40)
			{
				mClearSoftKey.SetFunction(1, -19);
			}
			else if (mDemoPopupType == 58)
			{
				mClearSoftKey.SetFunction(7, 0);
			}
			else if (mDemoPopupType == 59)
			{
				mClearSoftKey.SetFunction(1, -17);
			}
			else if (mDemoPopupType == 59)
			{
				mClearSoftKey.SetFunction(1, 81);
			}
			else
			{
				mClearSoftKey.SetFunction(1, 4);
			}
			base.Show();
		}
	}
}
