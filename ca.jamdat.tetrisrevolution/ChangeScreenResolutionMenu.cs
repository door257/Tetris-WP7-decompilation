using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ChangeScreenResolutionMenu : BaseScene
	{
		public ChangeScreenResolutionMenu(int stringEntryPoint)
			: base(24, 2097216)
		{
			mType = 4;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			Scroller scroller = null;
			Text text = null;
			Shape shape = null;
			Viewport viewport = null;
			scroller = Scroller.Cast(mPackage.GetEntryPoint(2), null);
			text = Text.Cast(mPackage.GetEntryPoint(3), null);
			shape = Shape.Cast(mPackage.GetEntryPoint(4), null);
			viewport = Viewport.Cast(mPackage.GetEntryPoint(1), null);
			mViewport.SetRect(screenRect);
			scroller.SetRect(screenRect);
			scroller.GetScrollerViewport().SetRect(screenRect);
			text.SetRect(screenRect);
			scroller.ResetScroller();
			shape.SetRect(screenRect);
			viewport.SetTopLeft(0, (short)(screenRect.GetHeight() - viewport.GetRectHeight()));
			viewport.SetSize(screenRect.GetWidth(), viewport.GetRectHeight());
			Component child = viewport.GetChild(1);
			child.SetTopLeft((short)(screenRect.GetWidth() - child.GetRectWidth()), child.GetRectTop());
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(2, -11);
			mSelectSoftKey.UpdatePos(5);
			mClearSoftKey.UpdatePos(6);
		}

		public override void StartMusic()
		{
		}
	}
}
