using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VerticalScroller
	{
		public static short GetLowestPosition(Scroller scroller)
		{
			Viewport scrollerViewport = scroller.GetScrollerViewport();
			Component child = scrollerViewport.GetChild(scrollerViewport.GetChildCount() - 1);
			return (short)(child.GetBottom() + 1);
		}

		public static void Initialize(Scroller scroller, int offsetBetweenElements)
		{
			Viewport viewport = (Viewport)scroller.GetChild(0);
			bool flag = scroller.GetSubtype() == -17;
			short num = (short)(flag ? 1 : 0);
			int rectWidth = scroller.GetRectWidth();
			int rectHeight = scroller.GetRectHeight();
			int num2 = rectWidth;
			int num3 = rectHeight - num;
			int numElements = scroller.GetNumElements();
			int num4 = scroller.GetElementAt(0).GetRectHeight() + num;
			int num5 = numElements * num4 + (numElements - 1) * offsetBetweenElements;
			scroller.SetScrollerViewport(viewport);
			int num6 = 0;
			int num7 = 0;
			if (SubtypeHasArrows(scroller.GetSubtype()))
			{
				Viewport viewport2 = (Viewport)scroller.GetChild(1);
				Selection selection = (Selection)viewport2.GetChild(0);
				Selection selection2 = (Selection)viewport2.GetChild(1);
				int rectWidth2 = selection.GetRectWidth();
				int rectHeight2 = selection.GetRectHeight();
				int num8 = FlMath.Minimum(num5, num3);
				viewport.SetTopLeft((short)rectWidth2, (short)(rectHeight - num8));
				viewport.SetSize((short)(num2 - rectWidth2), (short)num8);
				scroller.SetNextArrow(selection2);
				scroller.SetPreviousArrow(selection);
				viewport2.SetSize((short)rectWidth2, (short)num8);
				selection.SetTopLeft(0, 0);
				selection2.SetTopLeft(0, (short)(num8 - rectHeight2));
				num7 = rectWidth2;
			}
			else
			{
				viewport.SetTopLeft(0, num);
				if (num5 > num3)
				{
					int num9 = (num3 + offsetBetweenElements) / (num4 + offsetBetweenElements);
					num3 = num9 * num4 + (num9 - 1) * offsetBetweenElements;
				}
				viewport.SetSize((short)num2, (short)num3);
			}
			if (flag)
			{
				AddTopAndBottomDividerShapes(scroller);
			}
			for (int i = 0; i < numElements; i++)
			{
				Component elementAt = scroller.GetElementAt(i);
				if (elementAt is Selection)
				{
					Selection selection3 = (Selection)elementAt;
					if (selection3.GetEnabledState())
					{
						selection3.SetTopLeft(0, (short)num6);
						Text text = null;
						text = ((selection3.GetSubtype() != -4 && selection3.GetSubtype() != -20 && selection3.GetSubtype() != -21) ? ((Text)selection3.GetChild(0)) : ((Text)selection3.GetChild(1)));
						selection3.SetSize(viewport.GetRectWidth(), selection3.GetRectHeight());
						text.SetTopLeft((short)(num7 + text.GetRectLeft()), text.GetRectTop());
						if (SubtypeHasArrows(scroller.GetSubtype()))
						{
							text.SetSize((short)(text.GetRectWidth() - num7), text.GetRectHeight());
						}
						selection3.SetViewport(viewport);
						num6 += num4 + offsetBetweenElements;
					}
					selection3.SetVisible(selection3.GetEnabledState());
				}
				else
				{
					elementAt.SetTopLeft((short)num7, (short)num6);
					elementAt.SetViewport(viewport);
					num6 += num4 + offsetBetweenElements;
				}
				if (flag)
				{
					AddElementDividerShape(scroller, i);
				}
			}
			scroller.ResetScroller();
		}

		public static void Uninitialize(Scroller scroller)
		{
			if (scroller.GetSubtype() == -17)
			{
				scroller.GetScrollerViewport().SetSize(scroller.GetSize());
				RemoveTopAndBottomDividerShapes(scroller);
				for (int i = 0; i < scroller.GetNumElements(); i++)
				{
					RemoveElementDividerShape(scroller, i);
				}
			}
		}

		public static void AddElement(Scroller scroller, Component element, int index)
		{
			element.SetVisible(true);
			int numElements = scroller.GetNumElements();
			scroller.SetNumElements(numElements + 1);
			short rectHeight = element.GetRectHeight();
			for (int num = numElements; num > index; num--)
			{
				scroller.SetElementAt(num, scroller.GetElementAt(num - 1));
				scroller.GetElementAt(num).SetTopLeft(0, (short)(num * rectHeight));
			}
			element.SetTopLeft(0, (short)(index * rectHeight));
			scroller.SetElementAt(index, element);
			scroller.UpdateScroller();
		}

		public static void RemoveElement(Scroller scroller, int index)
		{
			Component elementAt = scroller.GetElementAt(index);
			elementAt.SetVisible(false);
			int numElements = scroller.GetNumElements();
			short rectHeight = elementAt.GetRectHeight();
			for (int i = index; i < numElements - 1; i++)
			{
				scroller.SetElementAt(i, scroller.GetElementAt(i + 1));
				scroller.GetElementAt(i).SetTopLeft(0, (short)(i * rectHeight));
			}
			scroller.SetNumElements(numElements - 1);
			scroller.UpdateScroller();
		}

		public static void RemoveAllElements(Scroller scroller)
		{
			int numElements = scroller.GetNumElements();
			for (int i = 0; i < numElements; i++)
			{
				RemoveElement(scroller, 0);
			}
		}

		public static void ResetBottomDividerPosition(Scroller scroller)
		{
			Component child = scroller.GetChild(scroller.GetChildCount() - 1);
			Viewport scrollerViewport = scroller.GetScrollerViewport();
			short top = (short)(scrollerViewport.GetRectTop() + scrollerViewport.GetRectHeight() - 1);
			child.SetTopLeft(0, top);
		}

		public static bool DoesLastComponentHaveLowestPosition(Scroller scroller)
		{
			Viewport scrollerViewport = scroller.GetScrollerViewport();
			Component component = scrollerViewport.GetChild(scrollerViewport.GetChildCount() - 1);
			short bottom = component.GetBottom();
			bool result = true;
			while ((component = component.GetPreviousSiblingComponent()) != null)
			{
				short bottom2 = component.GetBottom();
				if (bottom2 > bottom)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public static void AddTopAndBottomDividerShapes(Scroller scroller)
		{
			Color888 color = new Color888(51, 149, 215);
			Shape shape = new Shape();
			shape.SetColor(color);
			shape.SetTopLeft(0, 0);
			shape.SetSize(scroller.GetRectWidth(), 1);
			shape.SetViewport(scroller);
			shape.BringToFront();
			Shape shape2 = new Shape();
			shape2.SetSize(scroller.GetRectWidth(), 1);
			shape2.SetColor(color);
			shape2.SetViewport(scroller);
			shape2.BringToFront();
			ResetBottomDividerPosition(scroller);
		}

		public static void AddElementDividerShape(Scroller scroller, int elementIndex)
		{
			Shape shape = new Shape();
			shape.SetColor(new Color888(51, 149, 215));
			Viewport viewport = (Viewport)scroller.GetElementAt(elementIndex);
			viewport.SetSize(viewport.GetRectWidth(), (short)(viewport.GetRectHeight() + 1));
			short left = 0;
			short top = (short)(viewport.GetRectHeight() - 1);
			shape.SetSize(viewport.GetRectWidth(), 1);
			shape.SetTopLeft(left, top);
			shape.SetViewport(viewport);
			shape.BringToFront();
		}

		public static void RemoveTopAndBottomDividerShapes(Scroller scroller)
		{
			Component child = scroller.GetChild(scroller.GetChildCount() - 2);
			Component child2 = scroller.GetChild(scroller.GetChildCount() - 1);
			child.SetViewport(null);
			child2.SetViewport(null);
			child2 = null;
			child = null;
		}

		public static void RemoveElementDividerShape(Scroller scroller, int elementIndex)
		{
			Component elementAt = scroller.GetElementAt(elementIndex);
			elementAt.SetSize(elementAt.GetRectWidth(), (short)(elementAt.GetRectHeight() - 1));
			Component child = ((Viewport)elementAt).GetChild(((Viewport)elementAt).GetChildCount() - 1);
			child.SetViewport(null);
			child = null;
		}

		public static bool SubtypeHasArrows(int subtype)
		{
			if (-15 != subtype)
			{
				return -19 == subtype;
			}
			return true;
		}

		public static VerticalScroller[] InstArrayVerticalScroller(int size)
		{
			VerticalScroller[] array = new VerticalScroller[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new VerticalScroller();
			}
			return array;
		}

		public static VerticalScroller[][] InstArrayVerticalScroller(int size1, int size2)
		{
			VerticalScroller[][] array = new VerticalScroller[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalScroller[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalScroller();
				}
			}
			return array;
		}

		public static VerticalScroller[][][] InstArrayVerticalScroller(int size1, int size2, int size3)
		{
			VerticalScroller[][][] array = new VerticalScroller[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalScroller[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalScroller[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new VerticalScroller();
					}
				}
			}
			return array;
		}
	}
}
