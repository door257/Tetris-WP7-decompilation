using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Controllable : Viewport
	{
		public BaseController mController;

		public override void destruct()
		{
		}

		public virtual void SetController(BaseController controller)
		{
			mController = controller;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			return mController.OnMsg(source, msg, intParam);
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			mController.OnTime(totalTimeMs, deltaTimeMs);
		}

		public static Controllable[] InstArrayControllable(int size)
		{
			Controllable[] array = new Controllable[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Controllable();
			}
			return array;
		}

		public static Controllable[][] InstArrayControllable(int size1, int size2)
		{
			Controllable[][] array = new Controllable[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Controllable[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Controllable();
				}
			}
			return array;
		}

		public static Controllable[][][] InstArrayControllable(int size1, int size2, int size3)
		{
			Controllable[][][] array = new Controllable[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Controllable[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Controllable[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Controllable();
					}
				}
			}
			return array;
		}
	}
}
