namespace ca.jamdat.flight
{
	public class TypeIDOfType_Texture
	{
		public const sbyte v = 48;

		public const sbyte w = 48;

		public static TypeIDOfType_Texture[] InstArrayTypeIDOfType_Texture(int size)
		{
			TypeIDOfType_Texture[] array = new TypeIDOfType_Texture[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Texture();
			}
			return array;
		}

		public static TypeIDOfType_Texture[][] InstArrayTypeIDOfType_Texture(int size1, int size2)
		{
			TypeIDOfType_Texture[][] array = new TypeIDOfType_Texture[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Texture[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Texture();
				}
			}
			return array;
		}

		public static TypeIDOfType_Texture[][][] InstArrayTypeIDOfType_Texture(int size1, int size2, int size3)
		{
			TypeIDOfType_Texture[][][] array = new TypeIDOfType_Texture[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Texture[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Texture[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Texture();
					}
				}
			}
			return array;
		}
	}
}
