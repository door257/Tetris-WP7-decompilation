namespace ca.jamdat.flight
{
	public class TypeIDOfType_ImySound
	{
		public const sbyte v = 63;

		public const sbyte w = 63;

		public static TypeIDOfType_ImySound[] InstArrayTypeIDOfType_ImySound(int size)
		{
			TypeIDOfType_ImySound[] array = new TypeIDOfType_ImySound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_ImySound();
			}
			return array;
		}

		public static TypeIDOfType_ImySound[][] InstArrayTypeIDOfType_ImySound(int size1, int size2)
		{
			TypeIDOfType_ImySound[][] array = new TypeIDOfType_ImySound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ImySound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ImySound();
				}
			}
			return array;
		}

		public static TypeIDOfType_ImySound[][][] InstArrayTypeIDOfType_ImySound(int size1, int size2, int size3)
		{
			TypeIDOfType_ImySound[][][] array = new TypeIDOfType_ImySound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ImySound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ImySound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_ImySound();
					}
				}
			}
			return array;
		}
	}
}
