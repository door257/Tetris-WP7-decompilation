namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlVideo
	{
		public const sbyte v = 115;

		public const sbyte w = 115;

		public static TypeIDOfType_FlVideo[] InstArrayTypeIDOfType_FlVideo(int size)
		{
			TypeIDOfType_FlVideo[] array = new TypeIDOfType_FlVideo[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlVideo();
			}
			return array;
		}

		public static TypeIDOfType_FlVideo[][] InstArrayTypeIDOfType_FlVideo(int size1, int size2)
		{
			TypeIDOfType_FlVideo[][] array = new TypeIDOfType_FlVideo[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlVideo[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlVideo();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlVideo[][][] InstArrayTypeIDOfType_FlVideo(int size1, int size2, int size3)
		{
			TypeIDOfType_FlVideo[][][] array = new TypeIDOfType_FlVideo[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlVideo[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlVideo[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlVideo();
					}
				}
			}
			return array;
		}
	}
}
