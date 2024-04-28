using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameRenderer : View
	{
		public override void destruct()
		{
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			base.OnDraw(displayContext);
		}

		public static GameRenderer[] InstArrayGameRenderer(int size)
		{
			GameRenderer[] array = new GameRenderer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameRenderer();
			}
			return array;
		}

		public static GameRenderer[][] InstArrayGameRenderer(int size1, int size2)
		{
			GameRenderer[][] array = new GameRenderer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameRenderer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameRenderer();
				}
			}
			return array;
		}

		public static GameRenderer[][][] InstArrayGameRenderer(int size1, int size2, int size3)
		{
			GameRenderer[][][] array = new GameRenderer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameRenderer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameRenderer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameRenderer();
					}
				}
			}
			return array;
		}
	}
}
