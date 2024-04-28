namespace ca.jamdat.flight
{
	public class TypeIDOfType_VertexBuffer
	{
		public const sbyte v = 28;

		public const sbyte w = 28;

		public static TypeIDOfType_VertexBuffer[] InstArrayTypeIDOfType_VertexBuffer(int size)
		{
			TypeIDOfType_VertexBuffer[] array = new TypeIDOfType_VertexBuffer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_VertexBuffer();
			}
			return array;
		}

		public static TypeIDOfType_VertexBuffer[][] InstArrayTypeIDOfType_VertexBuffer(int size1, int size2)
		{
			TypeIDOfType_VertexBuffer[][] array = new TypeIDOfType_VertexBuffer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_VertexBuffer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_VertexBuffer();
				}
			}
			return array;
		}

		public static TypeIDOfType_VertexBuffer[][][] InstArrayTypeIDOfType_VertexBuffer(int size1, int size2, int size3)
		{
			TypeIDOfType_VertexBuffer[][][] array = new TypeIDOfType_VertexBuffer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_VertexBuffer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_VertexBuffer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_VertexBuffer();
					}
				}
			}
			return array;
		}
	}
}
