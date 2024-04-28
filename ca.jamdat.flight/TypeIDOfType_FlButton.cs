namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlButton
	{
		public const sbyte v = 70;

		public const sbyte w = 70;

		public static TypeIDOfType_FlButton[] InstArrayTypeIDOfType_FlButton(int size)
		{
			TypeIDOfType_FlButton[] array = new TypeIDOfType_FlButton[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlButton();
			}
			return array;
		}

		public static TypeIDOfType_FlButton[][] InstArrayTypeIDOfType_FlButton(int size1, int size2)
		{
			TypeIDOfType_FlButton[][] array = new TypeIDOfType_FlButton[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlButton[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlButton();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlButton[][][] InstArrayTypeIDOfType_FlButton(int size1, int size2, int size3)
		{
			TypeIDOfType_FlButton[][][] array = new TypeIDOfType_FlButton[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlButton[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlButton[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlButton();
					}
				}
			}
			return array;
		}
	}
}
