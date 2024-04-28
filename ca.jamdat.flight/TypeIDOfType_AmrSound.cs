namespace ca.jamdat.flight
{
	public class TypeIDOfType_AmrSound
	{
		public const sbyte v = 78;

		public const sbyte w = 78;

		public static TypeIDOfType_AmrSound[] InstArrayTypeIDOfType_AmrSound(int size)
		{
			TypeIDOfType_AmrSound[] array = new TypeIDOfType_AmrSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_AmrSound();
			}
			return array;
		}

		public static TypeIDOfType_AmrSound[][] InstArrayTypeIDOfType_AmrSound(int size1, int size2)
		{
			TypeIDOfType_AmrSound[][] array = new TypeIDOfType_AmrSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AmrSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AmrSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_AmrSound[][][] InstArrayTypeIDOfType_AmrSound(int size1, int size2, int size3)
		{
			TypeIDOfType_AmrSound[][][] array = new TypeIDOfType_AmrSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AmrSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AmrSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_AmrSound();
					}
				}
			}
			return array;
		}
	}
}
