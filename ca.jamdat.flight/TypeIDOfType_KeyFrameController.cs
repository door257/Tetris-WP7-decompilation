namespace ca.jamdat.flight
{
	public class TypeIDOfType_KeyFrameController
	{
		public const sbyte v = 88;

		public const sbyte w = 88;

		public static TypeIDOfType_KeyFrameController[] InstArrayTypeIDOfType_KeyFrameController(int size)
		{
			TypeIDOfType_KeyFrameController[] array = new TypeIDOfType_KeyFrameController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameController();
			}
			return array;
		}

		public static TypeIDOfType_KeyFrameController[][] InstArrayTypeIDOfType_KeyFrameController(int size1, int size2)
		{
			TypeIDOfType_KeyFrameController[][] array = new TypeIDOfType_KeyFrameController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_KeyFrameController();
				}
			}
			return array;
		}

		public static TypeIDOfType_KeyFrameController[][][] InstArrayTypeIDOfType_KeyFrameController(int size1, int size2, int size3)
		{
			TypeIDOfType_KeyFrameController[][][] array = new TypeIDOfType_KeyFrameController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_KeyFrameController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_KeyFrameController();
					}
				}
			}
			return array;
		}
	}
}
