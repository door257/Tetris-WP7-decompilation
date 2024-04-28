using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VerticalTextScroller
	{
		public static void Initialize(Scroller scroller, FlString @string)
		{
			Viewport viewport = (Viewport)scroller.GetChild(0);
			int rectWidth = scroller.GetRectWidth();
			int num = scroller.GetRectHeight();
			scroller.SetScrollerViewport(viewport);
			Text text = (Text)scroller.GetElementAt(0);
			text.SetViewport(viewport);
			text.SetCaption(@string);
			int lineHeight = text.GetLineHeight();
			int num2 = num / lineHeight;
			int nbLines = text.GetNbLines();
			if (nbLines > num2)
			{
				num2 = num / lineHeight;
				num = lineHeight * num2;
			}
			viewport.SetSize((short)rectWidth, (short)num);
			scroller.ResetScroller();
		}

		public static void InitializeWithHeader(Scroller scroller, FlString @string)
		{
			Viewport viewport = (Viewport)scroller.GetChild(0);
			int rectWidth = scroller.GetRectWidth();
			int num = scroller.GetRectHeight();
			scroller.SetScrollerViewport(viewport);
			Component elementAt = scroller.GetElementAt(0);
			elementAt.SetViewport(viewport);
			elementAt.SetTopLeft((short)((viewport.GetRectWidth() - elementAt.GetRectWidth()) / 2), 0);
			Text text = (Text)scroller.GetElementAt(1);
			text.SetViewport(viewport);
			text.SetCaption(@string);
			int lineHeight = text.GetLineHeight();
			int num2 = (elementAt.IsVisible() ? ((elementAt.GetRectHeight() / lineHeight + 1) * lineHeight) : 0);
			int num3 = (num - num2) / lineHeight;
			int nbLines = text.GetNbLines();
			text.SetTopLeft(text.GetRectLeft(), (short)num2);
			if (nbLines > num3)
			{
				num3 = num / lineHeight;
				num = lineHeight * num3;
			}
			viewport.SetSize((short)rectWidth, (short)num);
			scroller.ResetScroller();
		}

		public static VerticalTextScroller[] InstArrayVerticalTextScroller(int size)
		{
			VerticalTextScroller[] array = new VerticalTextScroller[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new VerticalTextScroller();
			}
			return array;
		}

		public static VerticalTextScroller[][] InstArrayVerticalTextScroller(int size1, int size2)
		{
			VerticalTextScroller[][] array = new VerticalTextScroller[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalTextScroller[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalTextScroller();
				}
			}
			return array;
		}

		public static VerticalTextScroller[][][] InstArrayVerticalTextScroller(int size1, int size2, int size3)
		{
			VerticalTextScroller[][][] array = new VerticalTextScroller[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalTextScroller[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalTextScroller[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new VerticalTextScroller();
					}
				}
			}
			return array;
		}
	}
}
