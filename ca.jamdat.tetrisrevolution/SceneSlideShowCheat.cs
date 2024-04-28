namespace ca.jamdat.tetrisrevolution
{
	public class SceneSlideShowCheat : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			base.Activate(param);
			GameApp.Get().GetSceneSlideShowController().Activate();
		}

		public override void Deactivate()
		{
			base.Deactivate();
			GameApp.Get().GetSceneSlideShowController().Deactivate();
		}

		public static SceneSlideShowCheat[] InstArraySceneSlideShowCheat(int size)
		{
			SceneSlideShowCheat[] array = new SceneSlideShowCheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneSlideShowCheat();
			}
			return array;
		}

		public static SceneSlideShowCheat[][] InstArraySceneSlideShowCheat(int size1, int size2)
		{
			SceneSlideShowCheat[][] array = new SceneSlideShowCheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneSlideShowCheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneSlideShowCheat();
				}
			}
			return array;
		}

		public static SceneSlideShowCheat[][][] InstArraySceneSlideShowCheat(int size1, int size2, int size3)
		{
			SceneSlideShowCheat[][][] array = new SceneSlideShowCheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneSlideShowCheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneSlideShowCheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneSlideShowCheat();
					}
				}
			}
			return array;
		}
	}
}
