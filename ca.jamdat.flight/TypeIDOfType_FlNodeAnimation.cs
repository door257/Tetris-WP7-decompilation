namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlNodeAnimation
	{
		public const sbyte v = 107;

		public const sbyte w = 107;

		public static TypeIDOfType_FlNodeAnimation[] InstArrayTypeIDOfType_FlNodeAnimation(int size)
		{
			TypeIDOfType_FlNodeAnimation[] array = new TypeIDOfType_FlNodeAnimation[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlNodeAnimation();
			}
			return array;
		}

		public static TypeIDOfType_FlNodeAnimation[][] InstArrayTypeIDOfType_FlNodeAnimation(int size1, int size2)
		{
			TypeIDOfType_FlNodeAnimation[][] array = new TypeIDOfType_FlNodeAnimation[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlNodeAnimation[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlNodeAnimation();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlNodeAnimation[][][] InstArrayTypeIDOfType_FlNodeAnimation(int size1, int size2, int size3)
		{
			TypeIDOfType_FlNodeAnimation[][][] array = new TypeIDOfType_FlNodeAnimation[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlNodeAnimation[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlNodeAnimation[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlNodeAnimation();
					}
				}
			}
			return array;
		}
	}
}
