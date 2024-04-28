namespace ca.jamdat.flight
{
	public class TypeIDOfType_FlTransformViewport
	{
		public const sbyte v = 116;

		public const sbyte w = 116;

		public static TypeIDOfType_FlTransformViewport[] InstArrayTypeIDOfType_FlTransformViewport(int size)
		{
			TypeIDOfType_FlTransformViewport[] array = new TypeIDOfType_FlTransformViewport[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_FlTransformViewport();
			}
			return array;
		}

		public static TypeIDOfType_FlTransformViewport[][] InstArrayTypeIDOfType_FlTransformViewport(int size1, int size2)
		{
			TypeIDOfType_FlTransformViewport[][] array = new TypeIDOfType_FlTransformViewport[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlTransformViewport[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlTransformViewport();
				}
			}
			return array;
		}

		public static TypeIDOfType_FlTransformViewport[][][] InstArrayTypeIDOfType_FlTransformViewport(int size1, int size2, int size3)
		{
			TypeIDOfType_FlTransformViewport[][][] array = new TypeIDOfType_FlTransformViewport[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_FlTransformViewport[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_FlTransformViewport[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_FlTransformViewport();
					}
				}
			}
			return array;
		}
	}
}
