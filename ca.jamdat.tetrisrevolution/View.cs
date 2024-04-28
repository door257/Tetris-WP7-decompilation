namespace ca.jamdat.tetrisrevolution
{
	public class View : Controllable
	{
		public override void destruct()
		{
		}

		public static View[] InstArrayView(int size)
		{
			View[] array = new View[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new View();
			}
			return array;
		}

		public static View[][] InstArrayView(int size1, int size2)
		{
			View[][] array = new View[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new View[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new View();
				}
			}
			return array;
		}

		public static View[][][] InstArrayView(int size1, int size2, int size3)
		{
			View[][][] array = new View[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new View[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new View[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new View();
					}
				}
			}
			return array;
		}
	}
}
