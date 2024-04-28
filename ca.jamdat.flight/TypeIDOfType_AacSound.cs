namespace ca.jamdat.flight
{
	public class TypeIDOfType_AacSound
	{
		public const sbyte v = 81;

		public const sbyte w = 81;

		public static TypeIDOfType_AacSound[] InstArrayTypeIDOfType_AacSound(int size)
		{
			TypeIDOfType_AacSound[] array = new TypeIDOfType_AacSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_AacSound();
			}
			return array;
		}

		public static TypeIDOfType_AacSound[][] InstArrayTypeIDOfType_AacSound(int size1, int size2)
		{
			TypeIDOfType_AacSound[][] array = new TypeIDOfType_AacSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AacSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AacSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_AacSound[][][] InstArrayTypeIDOfType_AacSound(int size1, int size2, int size3)
		{
			TypeIDOfType_AacSound[][][] array = new TypeIDOfType_AacSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AacSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AacSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_AacSound();
					}
				}
			}
			return array;
		}
	}
}
