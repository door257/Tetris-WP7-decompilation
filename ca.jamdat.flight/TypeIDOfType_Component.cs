namespace ca.jamdat.flight
{
	public class TypeIDOfType_Component
	{
		public const sbyte v = 67;

		public const sbyte w = 67;

		public static TypeIDOfType_Component[] InstArrayTypeIDOfType_Component(int size)
		{
			TypeIDOfType_Component[] array = new TypeIDOfType_Component[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_Component();
			}
			return array;
		}

		public static TypeIDOfType_Component[][] InstArrayTypeIDOfType_Component(int size1, int size2)
		{
			TypeIDOfType_Component[][] array = new TypeIDOfType_Component[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Component[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Component();
				}
			}
			return array;
		}

		public static TypeIDOfType_Component[][][] InstArrayTypeIDOfType_Component(int size1, int size2, int size3)
		{
			TypeIDOfType_Component[][][] array = new TypeIDOfType_Component[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_Component[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_Component[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_Component();
					}
				}
			}
			return array;
		}
	}
}
