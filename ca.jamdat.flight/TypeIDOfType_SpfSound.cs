namespace ca.jamdat.flight
{
	public class TypeIDOfType_SpfSound
	{
		public const sbyte v = 59;

		public const sbyte w = 59;

		public static TypeIDOfType_SpfSound[] InstArrayTypeIDOfType_SpfSound(int size)
		{
			TypeIDOfType_SpfSound[] array = new TypeIDOfType_SpfSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_SpfSound();
			}
			return array;
		}

		public static TypeIDOfType_SpfSound[][] InstArrayTypeIDOfType_SpfSound(int size1, int size2)
		{
			TypeIDOfType_SpfSound[][] array = new TypeIDOfType_SpfSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SpfSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SpfSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_SpfSound[][][] InstArrayTypeIDOfType_SpfSound(int size1, int size2, int size3)
		{
			TypeIDOfType_SpfSound[][][] array = new TypeIDOfType_SpfSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SpfSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SpfSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_SpfSound();
					}
				}
			}
			return array;
		}
	}
}
