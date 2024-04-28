namespace ca.jamdat.flight
{
	public class TypeIDOfType_IndexBuffer
	{
		public const sbyte v = 50;

		public const sbyte w = 50;

		public static TypeIDOfType_IndexBuffer[] InstArrayTypeIDOfType_IndexBuffer(int size)
		{
			TypeIDOfType_IndexBuffer[] array = new TypeIDOfType_IndexBuffer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_IndexBuffer();
			}
			return array;
		}

		public static TypeIDOfType_IndexBuffer[][] InstArrayTypeIDOfType_IndexBuffer(int size1, int size2)
		{
			TypeIDOfType_IndexBuffer[][] array = new TypeIDOfType_IndexBuffer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexBuffer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexBuffer();
				}
			}
			return array;
		}

		public static TypeIDOfType_IndexBuffer[][][] InstArrayTypeIDOfType_IndexBuffer(int size1, int size2, int size3)
		{
			TypeIDOfType_IndexBuffer[][][] array = new TypeIDOfType_IndexBuffer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_IndexBuffer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_IndexBuffer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_IndexBuffer();
					}
				}
			}
			return array;
		}
	}
}
