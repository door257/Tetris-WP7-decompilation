using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class BaseController
	{
		public virtual void destruct()
		{
		}

		public virtual bool OnMsg(Component source, int msg, int intParam)
		{
			return false;
		}

		public virtual void OnTime(int totalTimeMs, int deltaTimeMs)
		{
		}

		public static BaseController[] InstArrayBaseController(int size)
		{
			BaseController[] array = new BaseController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new BaseController();
			}
			return array;
		}

		public static BaseController[][] InstArrayBaseController(int size1, int size2)
		{
			BaseController[][] array = new BaseController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BaseController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BaseController();
				}
			}
			return array;
		}

		public static BaseController[][][] InstArrayBaseController(int size1, int size2, int size3)
		{
			BaseController[][][] array = new BaseController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BaseController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BaseController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new BaseController();
					}
				}
			}
			return array;
		}
	}
}
