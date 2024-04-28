namespace ca.jamdat.flight
{
	public class TypeIDOfType_SDLSoundImp
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_SDLSoundImp[] InstArrayTypeIDOfType_SDLSoundImp(int size)
		{
			TypeIDOfType_SDLSoundImp[] array = new TypeIDOfType_SDLSoundImp[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_SDLSoundImp();
			}
			return array;
		}

		public static TypeIDOfType_SDLSoundImp[][] InstArrayTypeIDOfType_SDLSoundImp(int size1, int size2)
		{
			TypeIDOfType_SDLSoundImp[][] array = new TypeIDOfType_SDLSoundImp[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SDLSoundImp[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SDLSoundImp();
				}
			}
			return array;
		}

		public static TypeIDOfType_SDLSoundImp[][][] InstArrayTypeIDOfType_SDLSoundImp(int size1, int size2, int size3)
		{
			TypeIDOfType_SDLSoundImp[][][] array = new TypeIDOfType_SDLSoundImp[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_SDLSoundImp[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_SDLSoundImp[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_SDLSoundImp();
					}
				}
			}
			return array;
		}
	}
}
