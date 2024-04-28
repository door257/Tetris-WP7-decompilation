using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Scrollbar : Viewport
	{
		public Scroller mScroller;

		public MetaPackage mMetaPackage;

		public ResizableFrame mScrollbarBackground;

		public ResizableFrame mKnob;

		public Viewport mScrollbarViewport;

		public override void destruct()
		{
		}

		public virtual void Load()
		{
			mMetaPackage = GameLibrary.GetPackage(524304);
		}

		public virtual bool IsLoaded()
		{
			if (mMetaPackage != null)
			{
				return mMetaPackage.IsLoaded();
			}
			return false;
		}

		public virtual void Initialize(Scroller scroller, Viewport scrollbarParent)
		{
			Package package = mMetaPackage.GetPackage();
			mScrollbarViewport = EntryPoint.GetViewport(package, 0);
			Viewport childrenContainer = (Viewport)mScrollbarViewport.GetChild(0);
			mScrollbarBackground = ResizableFrame.Create(childrenContainer);
			mScrollbarBackground.SetViewport(scrollbarParent);
			childrenContainer = (Viewport)mScrollbarViewport.GetChild(0);
			mKnob = ResizableFrame.Create(childrenContainer);
			mKnob.SetViewport(scrollbarParent);
			Reinitialize(scroller);
		}

		public virtual void Reinitialize(Scroller scroller)
		{
			Viewport viewport = mScrollbarBackground.GetViewport();
			mScroller = scroller;
			short height = (short)(scroller.GetScrollerViewport().GetRectHeight() + scroller.GetScrollerViewport().GetRectTop());
			viewport.SetSize(viewport.GetRectWidth(), height);
			viewport.SetTopLeft(viewport.GetRectLeft(), scroller.GetRectTop());
			SetSize(viewport.GetSize());
			SetViewport(viewport);
			Viewport viewport2 = (Viewport)viewport.GetChild(0);
			viewport2.SetSize(viewport2.GetRectWidth(), viewport.GetRectHeight());
			Viewport viewport3 = (Viewport)mScrollbarBackground.GetChild(0);
			viewport3.SetSize(viewport3.GetRectWidth(), viewport.GetRectHeight());
			UpdateKnobHeight();
			UpdateKnobPosition();
		}

		public virtual void Unload()
		{
			if (mScrollbarBackground != null)
			{
				mScrollbarBackground.SetViewport(mScrollbarViewport);
				CustomComponentUtilities.Detach(mScrollbarBackground);
				mScrollbarBackground.Unload();
				mScrollbarBackground = null;
			}
			if (mKnob != null)
			{
				mKnob.SetViewport(mScrollbarViewport);
				CustomComponentUtilities.Detach(mKnob);
				mKnob.Unload();
				mKnob = null;
			}
			SetViewport(null);
			if (mScrollbarViewport != null)
			{
				mScroller = null;
				mScrollbarViewport.SetViewport(null);
				mScrollbarViewport = null;
			}
			if (mMetaPackage != null)
			{
				GameLibrary.ReleasePackage(mMetaPackage);
				mMetaPackage = null;
			}
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			if (-105 == msg)
			{
				UpdateKnobHeight();
				UpdateKnobPosition();
			}
			return false;
		}

		public virtual void UpdateKnobPosition()
		{
			int rectHeight = mKnob.GetRectHeight();
			int num = mScrollbarBackground.GetRectHeight() - rectHeight;
			short top = 0;
			if (mScroller != null)
			{
				top = ((mScroller.GetScrollingPosition() <= mScroller.GetTotalScrollingSize() - mScroller.GetRectHeight()) ? ((short)mScroller.GetScrollbarRatio().Mul(num).ToInt(16)) : ((short)num));
			}
			mKnob.SetTopLeft(mKnob.GetRectLeft(), top);
		}

		public virtual void UpdateKnobHeight()
		{
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short rectHeight = mScrollbarBackground.GetRectHeight();
			if (mScroller != null)
			{
				num2 = mScroller.GetTotalScrollingSize();
				num3 = mScroller.GetRectHeight();
				if (num3 >= num2)
				{
					mScrollbarBackground.SetVisible(false);
					mKnob.SetVisible(false);
				}
				else
				{
					num = (short)F32.FromInt(num3, 16).Div(num2).Mul(rectHeight)
						.ToInt(16);
					mScrollbarBackground.SetVisible(true);
					mKnob.SetVisible(true);
				}
			}
			mKnob.SetSize(mKnob.GetRectWidth(), (short)((num < 6) ? 6 : num));
			mKnob.GetChild(0).SetSize(mKnob.GetSize());
		}

		public static Scrollbar[] InstArrayScrollbar(int size)
		{
			Scrollbar[] array = new Scrollbar[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Scrollbar();
			}
			return array;
		}

		public static Scrollbar[][] InstArrayScrollbar(int size1, int size2)
		{
			Scrollbar[][] array = new Scrollbar[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scrollbar[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scrollbar();
				}
			}
			return array;
		}

		public static Scrollbar[][][] InstArrayScrollbar(int size1, int size2, int size3)
		{
			Scrollbar[][][] array = new Scrollbar[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scrollbar[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scrollbar[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Scrollbar();
					}
				}
			}
			return array;
		}
	}
}
