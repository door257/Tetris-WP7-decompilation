namespace ca.jamdat.flight
{
	public class TypeIDOfType_IPhoneSoundImp
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_IPhoneSoundImp[] InstArrayTypeIDOfType_IPhoneSoundImp(int size)
		{
			TypeIDOfType_IPhoneSoundImp[] array = new TypeIDOfType_IPhoneSoundImp[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_IPhoneSoundImp();
			}
			return array;
		}

		public static TypeIDOfType_IPhoneSoundImp[][] InstArrayTypeIDOfType_IPhoneSoundImp(int size1, int size2)
		{
			TypeIDOfType_IPhoneSoundImp[][] array = new TypeIDOfType_IPhoneSoundImp[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IPhoneSoundImp[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IPhoneSoundImp();
				}
			}
			return array;
		}

		public static TypeIDOfType_IPhoneSoundImp[][][] InstArrayTypeIDOfType_IPhoneSoundImp(int size1, int size2, int size3)
		{
			TypeIDOfType_IPhoneSoundImp[][][] array = new TypeIDOfType_IPhoneSoundImp[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IPhoneSoundImp[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IPhoneSoundImp[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_IPhoneSoundImp();
					}
				}
			}
			return array;
		}
	}
}
