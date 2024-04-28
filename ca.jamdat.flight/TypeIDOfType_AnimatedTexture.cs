namespace ca.jamdat.flight
{
	public class TypeIDOfType_AnimatedTexture
	{
		public const sbyte v = 102;

		public const sbyte w = 102;

		public static TypeIDOfType_AnimatedTexture[] InstArrayTypeIDOfType_AnimatedTexture(int size)
		{
			TypeIDOfType_AnimatedTexture[] array = new TypeIDOfType_AnimatedTexture[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_AnimatedTexture();
			}
			return array;
		}

		public static TypeIDOfType_AnimatedTexture[][] InstArrayTypeIDOfType_AnimatedTexture(int size1, int size2)
		{
			TypeIDOfType_AnimatedTexture[][] array = new TypeIDOfType_AnimatedTexture[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AnimatedTexture[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AnimatedTexture();
				}
			}
			return array;
		}

		public static TypeIDOfType_AnimatedTexture[][][] InstArrayTypeIDOfType_AnimatedTexture(int size1, int size2, int size3)
		{
			TypeIDOfType_AnimatedTexture[][][] array = new TypeIDOfType_AnimatedTexture[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_AnimatedTexture[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_AnimatedTexture[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_AnimatedTexture();
					}
				}
			}
			return array;
		}
	}
}
