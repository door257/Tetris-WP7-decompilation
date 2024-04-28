namespace ca.jamdat.flight
{
	public class TypeIDOfType_F32
	{
		public const sbyte v = 14;

		public const sbyte w = 14;

		public static TypeIDOfType_F32[] InstArrayTypeIDOfType_F32(int size)
		{
			TypeIDOfType_F32[] array = new TypeIDOfType_F32[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_F32();
			}
			return array;
		}

		public static TypeIDOfType_F32[][] InstArrayTypeIDOfType_F32(int size1, int size2)
		{
			TypeIDOfType_F32[][] array = new TypeIDOfType_F32[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_F32[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_F32();
				}
			}
			return array;
		}

		public static TypeIDOfType_F32[][][] InstArrayTypeIDOfType_F32(int size1, int size2, int size3)
		{
			TypeIDOfType_F32[][][] array = new TypeIDOfType_F32[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_F32[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_F32[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_F32();
					}
				}
			}
			return array;
		}
	}
}
