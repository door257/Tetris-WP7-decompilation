namespace ca.jamdat.flight
{
	public class TypeIDOfType_M4aSound
	{
		public const sbyte v = 104;

		public const sbyte w = 104;

		public static TypeIDOfType_M4aSound[] InstArrayTypeIDOfType_M4aSound(int size)
		{
			TypeIDOfType_M4aSound[] array = new TypeIDOfType_M4aSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_M4aSound();
			}
			return array;
		}

		public static TypeIDOfType_M4aSound[][] InstArrayTypeIDOfType_M4aSound(int size1, int size2)
		{
			TypeIDOfType_M4aSound[][] array = new TypeIDOfType_M4aSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_M4aSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_M4aSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_M4aSound[][][] InstArrayTypeIDOfType_M4aSound(int size1, int size2, int size3)
		{
			TypeIDOfType_M4aSound[][][] array = new TypeIDOfType_M4aSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_M4aSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_M4aSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_M4aSound();
					}
				}
			}
			return array;
		}
	}
}
