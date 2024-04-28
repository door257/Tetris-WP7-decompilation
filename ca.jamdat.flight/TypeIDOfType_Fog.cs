namespace ca.jamdat.flight
{
	public class TypeIDOfType_Fog
	{
		public const sbyte v = 47;

		public const sbyte w = 47;

		public static TypeIDOfType_Fog[] InstArrayTypeIDOfType_Fog(int size)
		{
			TypeIDOfType_Fog[] array = new TypeIDOfType_Fog[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Fog();
			}
			return array;
		}

		public static TypeIDOfType_Fog[][] InstArrayTypeIDOfType_Fog(int size1, int size2)
		{
			TypeIDOfType_Fog[][] array = new TypeIDOfType_Fog[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Fog[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Fog();
				}
			}
			return array;
		}

		public static TypeIDOfType_Fog[][][] InstArrayTypeIDOfType_Fog(int size1, int size2, int size3)
		{
			TypeIDOfType_Fog[][][] array = new TypeIDOfType_Fog[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Fog[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Fog[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Fog();
					}
				}
			}
			return array;
		}
	}
}
