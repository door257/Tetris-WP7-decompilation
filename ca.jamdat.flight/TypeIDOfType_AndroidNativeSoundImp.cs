namespace ca.jamdat.flight
{
	public class TypeIDOfType_AndroidNativeSoundImp
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_AndroidNativeSoundImp[] InstArrayTypeIDOfType_AndroidNativeSoundImp(int size)
		{
			TypeIDOfType_AndroidNativeSoundImp[] array = new TypeIDOfType_AndroidNativeSoundImp[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_AndroidNativeSoundImp();
			}
			return array;
		}

		public static TypeIDOfType_AndroidNativeSoundImp[][] InstArrayTypeIDOfType_AndroidNativeSoundImp(int size1, int size2)
		{
			TypeIDOfType_AndroidNativeSoundImp[][] array = new TypeIDOfType_AndroidNativeSoundImp[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AndroidNativeSoundImp[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AndroidNativeSoundImp();
				}
			}
			return array;
		}

		public static TypeIDOfType_AndroidNativeSoundImp[][][] InstArrayTypeIDOfType_AndroidNativeSoundImp(int size1, int size2, int size3)
		{
			TypeIDOfType_AndroidNativeSoundImp[][][] array = new TypeIDOfType_AndroidNativeSoundImp[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AndroidNativeSoundImp[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AndroidNativeSoundImp[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_AndroidNativeSoundImp();
					}
				}
			}
			return array;
		}
	}
}
