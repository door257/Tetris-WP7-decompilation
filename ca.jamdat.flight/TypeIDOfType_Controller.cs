namespace ca.jamdat.flight
{
	public class TypeIDOfType_Controller
	{
		public const sbyte v = 87;

		public const sbyte w = 87;

		public static TypeIDOfType_Controller[] InstArrayTypeIDOfType_Controller(int size)
		{
			TypeIDOfType_Controller[] array = new TypeIDOfType_Controller[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Controller();
			}
			return array;
		}

		public static TypeIDOfType_Controller[][] InstArrayTypeIDOfType_Controller(int size1, int size2)
		{
			TypeIDOfType_Controller[][] array = new TypeIDOfType_Controller[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Controller[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Controller();
				}
			}
			return array;
		}

		public static TypeIDOfType_Controller[][][] InstArrayTypeIDOfType_Controller(int size1, int size2, int size3)
		{
			TypeIDOfType_Controller[][][] array = new TypeIDOfType_Controller[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Controller[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Controller[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Controller();
					}
				}
			}
			return array;
		}
	}
}
