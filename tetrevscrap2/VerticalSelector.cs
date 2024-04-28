using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class VerticalSelector
	{
		public static void Initialize(Selector selector, int offsetBetweenSelections, int selectedItemIdx, bool singleSelectionDisplayed)
		{
			VerticalScroller.Initialize(selector, offsetBetweenSelections);
			selector.SetSingleSelection(selectedItemIdx, false);
		}

		public static void Uninitialize(Selector selector)
		{
			VerticalScroller.Uninitialize(selector);
		}

		public static void Initialize(Selector selector, int offsetBetweenSelections, int selectedItemIdx)
		{
			Initialize(selector, offsetBetweenSelections, selectedItemIdx, false);
		}

		public static VerticalSelector[] InstArrayVerticalSelector(int size)
		{
			VerticalSelector[] array = new VerticalSelector[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new VerticalSelector();
			}
			return array;
		}

		public static VerticalSelector[][] InstArrayVerticalSelector(int size1, int size2)
		{
			VerticalSelector[][] array = new VerticalSelector[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalSelector[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalSelector();
				}
			}
			return array;
		}

		public static VerticalSelector[][][] InstArrayVerticalSelector(int size1, int size2, int size3)
		{
			VerticalSelector[][][] array = new VerticalSelector[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new VerticalSelector[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new VerticalSelector[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new VerticalSelector();
					}
				}
			}
			return array;
		}
	}
}
