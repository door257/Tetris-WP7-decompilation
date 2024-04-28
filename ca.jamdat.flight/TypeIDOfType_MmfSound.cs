namespace ca.jamdat.flight
{
	public class TypeIDOfType_MmfSound
	{
		public const sbyte v = 62;

		public const sbyte w = 62;

		public static TypeIDOfType_MmfSound[] InstArrayTypeIDOfType_MmfSound(int size)
		{
			TypeIDOfType_MmfSound[] array = new TypeIDOfType_MmfSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_MmfSound();
			}
			return array;
		}

		public static TypeIDOfType_MmfSound[][] InstArrayTypeIDOfType_MmfSound(int size1, int size2)
		{
			TypeIDOfType_MmfSound[][] array = new TypeIDOfType_MmfSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MmfSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MmfSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_MmfSound[][][] InstArrayTypeIDOfType_MmfSound(int size1, int size2, int size3)
		{
			TypeIDOfType_MmfSound[][][] array = new TypeIDOfType_MmfSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MmfSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MmfSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_MmfSound();
					}
				}
			}
			return array;
		}
	}
}
