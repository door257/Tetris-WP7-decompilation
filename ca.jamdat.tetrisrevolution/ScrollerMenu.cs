using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ScrollerMenu : Menu
	{
		public Scroller mScroller;

		public ScrollerMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			VerticalScroller.Initialize(mScroller, 0);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mPackage;
			mScroller = Scroller.Cast(package.GetEntryPoint(-1), null);
		}

		public override void ReceiveFocus()
		{
			base.ReceiveFocus();
			mScroller.TakeFocus();
		}

		public override void Unload()
		{
			mScroller = null;
			base.Unload();
		}
	}
}
