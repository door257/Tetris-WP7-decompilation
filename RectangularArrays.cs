internal static class RectangularArrays
{
	internal static int[][] ReturnRectangularIntArray(int Size1, int Size2)
	{
		int[][] array = new int[Size1][];
		for (int i = 0; i < Size1; i++)
		{
			array[i] = new int[Size2];
		}
		return array;
	}

	internal static bool[][] ReturnRectangularBoolArray(int Size1, int Size2)
	{
		bool[][] array = new bool[Size1][];
		for (int i = 0; i < Size1; i++)
		{
			array[i] = new bool[Size2];
		}
		return array;
	}
}
