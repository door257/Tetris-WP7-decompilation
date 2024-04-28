namespace ca.jamdat.flight
{
	public class Key
	{
		public const int NO_KEY = -1;

		public const int UNDEFINED = 0;

		public const int UP = 1;

		public const int DOWN = 2;

		public const int LEFT = 3;

		public const int RIGHT = 4;

		public const int SELECT = 5;

		public const int CLEAR = 6;

		public const int FIRE = 7;

		public const int MENU = 8;

		public const int BACK = 9;

		public const int BACKSPACE = 10;

		public const int VOLUME_UP = 11;

		public const int VOLUME_DOWN = 12;

		public const int SOFT_SELECT = 13;

		public const int SOFT_CLEAR = 14;

		public const int SOFT_LEFT = 13;

		public const int SOFT_RIGHT = 14;

		public const int FIRSTLOOPKEY = 15;

		public const int POUND = 15;

		public const int STAR = 16;

		public const int DIGIT0 = 17;

		public const int DIGIT1 = 18;

		public const int DIGIT2 = 19;

		public const int DIGIT3 = 20;

		public const int DIGIT4 = 21;

		public const int DIGIT5 = 22;

		public const int DIGIT6 = 23;

		public const int DIGIT7 = 24;

		public const int DIGIT8 = 25;

		public const int DIGIT9 = 26;

		public const int LASTLOOPKEY = 27;

		public const int ALT = 28;

		public const int SHIFT = 29;

		public const int SYMBOL = 30;

		public const int COUNT = 31;

		public static Key[] InstArrayKey(int size)
		{
			Key[] array = new Key[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Key();
			}
			return array;
		}

		public static Key[][] InstArrayKey(int size1, int size2)
		{
			Key[][] array = new Key[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Key[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Key();
				}
			}
			return array;
		}

		public static Key[][][] InstArrayKey(int size1, int size2, int size3)
		{
			Key[][][] array = new Key[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Key[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Key[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Key();
					}
				}
			}
			return array;
		}
	}
}
