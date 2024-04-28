using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	internal class EnableSoundMenu : BaseScene
	{
		private int mYesCmd;

		private int mNoCmd;

		public EnableSoundMenu(int sceneId, int pkgId, int yesCmd, int noCmd)
			: base(sceneId, pkgId)
		{
			mYesCmd = yesCmd;
			mNoCmd = noCmd;
		}

		public override void Initialize()
		{
			base.Initialize();
			Package p = mPackage;
			FlString flString = EntryPoint.GetFlString(p, 7);
			Scroller scroller = EntryPoint.GetScroller(p, 2);
			Viewport viewport = EntryPoint.GetViewport(p, 6);
			int num = 0;
			Selection selection = EntryPoint.GetSelection(p, 4);
			Selection selection2 = EntryPoint.GetSelection(p, 5);
			selection.SetCommand((sbyte)mYesCmd);
			selection2.SetCommand((sbyte)mNoCmd);
			mClearSoftKey.SetFunction(2, -11);
			num = viewport.GetRectHeight();
			Text text = (Text)scroller.GetElementAt(0);
			VerticalTextScroller.Initialize(scroller, flString);
			if (text.GetRectHeight() >= num)
			{
				Viewport viewport2 = (Viewport)scroller.GetChild(1);
				int rectWidth = viewport.GetRectWidth();
				int rectHeight = viewport2.GetRectHeight();
				viewport2.SetCenter((short)(rectWidth / 2), (short)(num - rectHeight / 2));
				scroller.GetScrollerViewport().SetSize((short)rectWidth, (short)(num - rectHeight));
				scroller.SetSize((short)rectWidth, (short)num);
				scroller.SetTopLeft(0, 0);
			}
			else
			{
				short rectWidth2 = viewport.GetRectWidth();
				int num2 = viewport.GetRectHeight() / 2;
				int num3 = 0;
				num3 = scroller.GetTotalScrollingSize() / 2;
				int totalScrollingSize = scroller.GetTotalScrollingSize();
				Viewport viewport3 = (Viewport)scroller.GetChild(1);
				viewport3.SetTopLeft(0, 0);
				viewport3.SetVisible(false);
				scroller.GetScrollerViewport().SetSize(rectWidth2, (short)totalScrollingSize);
				scroller.SetSize(rectWidth2, (short)totalScrollingSize);
				scroller.SetTopLeft(0, (short)(num2 - num3));
			}
			scroller.ResetScroller();
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (!flag)
			{
				base.OnMsg(source, msg, intParam);
			}
			return flag;
		}

		public override bool OnCommand(int command)
		{
			bool result = base.OnCommand(command);
			if (command == -75 || command == -76)
			{
				result = base.OnCommand(-2);
			}
			return result;
		}
	}
}
