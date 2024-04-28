namespace ca.jamdat.flight
{
	public class TypeIDOfType_ToneSound
	{
		public const sbyte v = 61;

		public const sbyte w = 61;

		public static TypeIDOfType_ToneSound[] InstArrayTypeIDOfType_ToneSound(int size)
		{
			TypeIDOfType_ToneSound[] array = new TypeIDOfType_ToneSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_ToneSound();
			}
			return array;
		}

		public static TypeIDOfType_ToneSound[][] InstArrayTypeIDOfType_ToneSound(int size1, int size2)
		{
			TypeIDOfType_ToneSound[][] array = new TypeIDOfType_ToneSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ToneSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ToneSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_ToneSound[][][] InstArrayTypeIDOfType_ToneSound(int size1, int size2, int size3)
		{
			TypeIDOfType_ToneSound[][][] array = new TypeIDOfType_ToneSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_ToneSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_ToneSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_ToneSound();
					}
				}
			}
			return array;
		}
	}
}
