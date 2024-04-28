namespace ca.jamdat.flight
{
	public class TypeIDOfType_FileStream
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_FileStream[] InstArrayTypeIDOfType_FileStream(int size)
		{
			TypeIDOfType_FileStream[] array = new TypeIDOfType_FileStream[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FileStream();
			}
			return array;
		}

		public static TypeIDOfType_FileStream[][] InstArrayTypeIDOfType_FileStream(int size1, int size2)
		{
			TypeIDOfType_FileStream[][] array = new TypeIDOfType_FileStream[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FileStream[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FileStream();
				}
			}
			return array;
		}

		public static TypeIDOfType_FileStream[][][] InstArrayTypeIDOfType_FileStream(int size1, int size2, int size3)
		{
			TypeIDOfType_FileStream[][][] array = new TypeIDOfType_FileStream[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FileStream[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FileStream[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FileStream();
					}
				}
			}
			return array;
		}
	}
}
