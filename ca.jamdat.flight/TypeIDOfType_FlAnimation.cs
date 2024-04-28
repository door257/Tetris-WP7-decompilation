namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlAnimation
	{
		public const sbyte v = 106;

		public const sbyte w = 106;

		public static TypeIDOfType_FlAnimation[] InstArrayTypeIDOfType_FlAnimation(int size)
		{
			TypeIDOfType_FlAnimation[] array = new TypeIDOfType_FlAnimation[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlAnimation();
			}
			return array;
		}

		public static TypeIDOfType_FlAnimation[][] InstArrayTypeIDOfType_FlAnimation(int size1, int size2)
		{
			TypeIDOfType_FlAnimation[][] array = new TypeIDOfType_FlAnimation[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlAnimation[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlAnimation();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlAnimation[][][] InstArrayTypeIDOfType_FlAnimation(int size1, int size2, int size3)
		{
			TypeIDOfType_FlAnimation[][][] array = new TypeIDOfType_FlAnimation[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlAnimation[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlAnimation[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlAnimation();
					}
				}
			}
			return array;
		}
	}
}
