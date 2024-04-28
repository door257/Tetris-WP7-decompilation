namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlSoundBank
	{
		public const sbyte v = -1;

		public const sbyte w = sbyte.MaxValue;

		public static TypeIDOfType_FlSoundBank[] InstArrayTypeIDOfType_FlSoundBank(int size)
		{
			TypeIDOfType_FlSoundBank[] array = new TypeIDOfType_FlSoundBank[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlSoundBank();
			}
			return array;
		}

		public static TypeIDOfType_FlSoundBank[][] InstArrayTypeIDOfType_FlSoundBank(int size1, int size2)
		{
			TypeIDOfType_FlSoundBank[][] array = new TypeIDOfType_FlSoundBank[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSoundBank[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSoundBank();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlSoundBank[][][] InstArrayTypeIDOfType_FlSoundBank(int size1, int size2, int size3)
		{
			TypeIDOfType_FlSoundBank[][][] array = new TypeIDOfType_FlSoundBank[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlSoundBank[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlSoundBank[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlSoundBank();
					}
				}
			}
			return array;
		}
	}
}
