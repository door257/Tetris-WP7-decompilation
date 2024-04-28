namespace ca.jamdat.flight
{
	public class TypeIDOfType_Material
	{
		public const sbyte v = 45;

		public const sbyte w = 45;

		public static TypeIDOfType_Material[] InstArrayTypeIDOfType_Material(int size)
		{
			TypeIDOfType_Material[] array = new TypeIDOfType_Material[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Material();
			}
			return array;
		}

		public static TypeIDOfType_Material[][] InstArrayTypeIDOfType_Material(int size1, int size2)
		{
			TypeIDOfType_Material[][] array = new TypeIDOfType_Material[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Material[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Material();
				}
			}
			return array;
		}

		public static TypeIDOfType_Material[][][] InstArrayTypeIDOfType_Material(int size1, int size2, int size3)
		{
			TypeIDOfType_Material[][][] array = new TypeIDOfType_Material[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Material[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Material[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Material();
					}
				}
			}
			return array;
		}
	}
}
