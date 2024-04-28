namespace ca.jamdat.flight
{
	public class TypeIDOfType_Blob
	{
		public const sbyte v = 41;

		public const sbyte w = 41;

		public static TypeIDOfType_Blob[] InstArrayTypeIDOfType_Blob(int size)
		{
			TypeIDOfType_Blob[] array = new TypeIDOfType_Blob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Blob();
			}
			return array;
		}

		public static TypeIDOfType_Blob[][] InstArrayTypeIDOfType_Blob(int size1, int size2)
		{
			TypeIDOfType_Blob[][] array = new TypeIDOfType_Blob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Blob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Blob();
				}
			}
			return array;
		}

		public static TypeIDOfType_Blob[][][] InstArrayTypeIDOfType_Blob(int size1, int size2, int size3)
		{
			TypeIDOfType_Blob[][][] array = new TypeIDOfType_Blob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Blob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Blob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Blob();
					}
				}
			}
			return array;
		}
	}
}
