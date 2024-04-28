using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class HorizontalSelector
	{
		public static void Initialize(Selector selector, int selectedItemIdx)
		{
			Initialize(selector, selectedItemIdx, true, true);
		}

		public static void Initialize(Selector selector, int selectedItemIdx, bool arrowAttachedInSelector, bool centerTextInSelection)
		{
			Viewport viewport = (Viewport)selector.GetChild(0);
			bool flag = selector.GetNumSelections() <= 0;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (!flag)
			{
				Selection selectionAt = selector.GetSelectionAt(0);
				num = selectionAt.GetRectWidth();
				num2 = selectionAt.GetRectHeight();
				num3 = selectionAt.GetRectTop();
			}
			int rectWidth = selector.GetRectWidth();
			int rectHeight = selector.GetRectHeight();
			int num4 = 25;
			int num5 = num4;
			if (arrowAttachedInSelector)
			{
				Viewport viewport2 = (Viewport)selector.GetChild(1);
				Selection selection = (Selection)viewport2.GetChild(0);
				Selection selection2 = (Selection)viewport2.GetChild(1);
				selection.GetRectWidth();
				int rectHeight2 = selection.GetRectHeight();
				int num6 = (num2 - rectHeight2) / 2;
				selector.SetNextArrow(selection2);
				selector.SetPreviousArrow(selection);
				viewport2.SetSize((short)rectWidth, (short)rectHeight);
				viewport2.SetTopLeft(0, 0);
				selection.SetTopLeft(1, (short)num6);
				selection2.SetTopLeft((short)(rectWidth - selection2.GetRectWidth() - 1), (short)num6);
				viewport2.SetVisible(selector.GetNumSelections() > 1);
			}
			viewport.SetTopLeft((short)num5, 0);
			viewport.SetSize((short)num, (short)num2);
			selector.SetScrollerViewport(viewport);
			int numSelections = selector.GetNumSelections();
			int num7 = 0;
			for (int i = 0; i < numSelections; i++)
			{
				Selection selectionAt2 = selector.GetSelectionAt(i);
				selectionAt2.SetViewport(viewport);
				selectionAt2.SetTopLeft((short)num7, (short)num3);
				num7 += num;
				if (selectionAt2.GetChild(0) is Text && centerTextInSelection)
				{
					Text text = (Text)selectionAt2.GetChild(0);
					int rectHeight3 = text.GetRectHeight();
					text.SetTopLeft(text.GetRectLeft(), (short)((num2 - rectHeight3) / 2));
				}
			}
			selector.ResetScroller();
			if (!flag)
			{
				selector.SetSingleSelection(selectedItemIdx, false);
			}
		}

		public static HorizontalSelector[] InstArrayHorizontalSelector(int size)
		{
			HorizontalSelector[] array = new HorizontalSelector[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new HorizontalSelector();
			}
			return array;
		}

		public static HorizontalSelector[][] InstArrayHorizontalSelector(int size1, int size2)
		{
			HorizontalSelector[][] array = new HorizontalSelector[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new HorizontalSelector[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new HorizontalSelector();
				}
			}
			return array;
		}

		public static HorizontalSelector[][][] InstArrayHorizontalSelector(int size1, int size2, int size3)
		{
			HorizontalSelector[][][] array = new HorizontalSelector[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new HorizontalSelector[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new HorizontalSelector[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new HorizontalSelector();
					}
				}
			}
			return array;
		}
	}
}
