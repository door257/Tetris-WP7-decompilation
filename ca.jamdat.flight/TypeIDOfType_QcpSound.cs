namespace ca.jamdat.flight
{
	public class TypeIDOfType_QcpSound
	{
		public const sbyte v = 60;

		public const sbyte w = 60;

		public static TypeIDOfType_QcpSound[] InstArrayTypeIDOfType_QcpSound(int size)
		{
			TypeIDOfType_QcpSound[] array = new TypeIDOfType_QcpSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_QcpSound();
			}
			return array;
		}

		public static TypeIDOfType_QcpSound[][] InstArrayTypeIDOfType_QcpSound(int size1, int size2)
		{
			TypeIDOfType_QcpSound[][] array = new TypeIDOfType_QcpSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_QcpSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_QcpSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_QcpSound[][][] InstArrayTypeIDOfType_QcpSound(int size1, int size2, int size3)
		{
			TypeIDOfType_QcpSound[][][] array = new TypeIDOfType_QcpSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_QcpSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_QcpSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_QcpSound();
					}
				}
			}
			return array;
		}
	}
}
