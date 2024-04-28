namespace ca.jamdat.flight
{
	public class RectSide
	{
		public const sbyte TOP = 1;

		public const sbyte BOTTOM = 2;

		public const sbyte RIGHT = 4;

		public const sbyte LEFT = 8;

		public static RectSide[] InstArrayRectSide(int size)
		{
			RectSide[] array = new RectSide[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new RectSide();
			}
			return array;
		}

		public static RectSide[][] InstArrayRectSide(int size1, int size2)
		{
			RectSide[][] array = new RectSide[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RectSide[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RectSide();
				}
			}
			return array;
		}

		public static RectSide[][][] InstArrayRectSide(int size1, int size2, int size3)
		{
			RectSide[][][] array = new RectSide[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new RectSide[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new RectSide[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new RectSide();
					}
				}
			}
			return array;
		}
	}
}
