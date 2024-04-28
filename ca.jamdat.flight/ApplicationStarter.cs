using ca.jamdat.tetrisrevolution;

namespace ca.jamdat.flight
{
	public class ApplicationStarter
	{
		public static FlApplication NewFlightApp()
		{
			return new GameApp();
		}

		public static ApplicationStarter[] InstArrayApplicationStarter(int size)
		{
			ApplicationStarter[] array = new ApplicationStarter[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ApplicationStarter();
			}
			return array;
		}

		public static ApplicationStarter[][] InstArrayApplicationStarter(int size1, int size2)
		{
			ApplicationStarter[][] array = new ApplicationStarter[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ApplicationStarter[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ApplicationStarter();
				}
			}
			return array;
		}

		public static ApplicationStarter[][][] InstArrayApplicationStarter(int size1, int size2, int size3)
		{
			ApplicationStarter[][][] array = new ApplicationStarter[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ApplicationStarter[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ApplicationStarter[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ApplicationStarter();
					}
				}
			}
			return array;
		}
	}
}
