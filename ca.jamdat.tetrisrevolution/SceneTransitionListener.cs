namespace ca.jamdat.tetrisrevolution
{
	public class SceneTransitionListener
	{
		public static void SceneLoadingStarted(BaseScene loadingScene)
		{
			GameApp gameApp = GameApp.Get();
			SharedResourcesHandler sharedResourcesHandler = gameApp.GetSharedResourcesHandler();
			if (loadingScene.IsTypeOf(2) || loadingScene.IsTypeOf(1) || loadingScene.IsTypeOf(4))
			{
				sharedResourcesHandler.AcquireAppResources();
				sharedResourcesHandler.AcquireMenusResources();
				sharedResourcesHandler.LoadAppResources();
			}
			else
			{
				sharedResourcesHandler.ReleaseMenusResources();
			}
			SetInputMapping(loadingScene);
		}

		public static void SceneUnloadingStarted(BaseScene unloadingScene)
		{
		}

		public static void SetInputMapping(BaseScene loadedScene)
		{
			InputMapper inputMapper = GameApp.Get().GetInputMapper();
			if (loadedScene.IsTypeOf(2) || loadedScene.IsTypeOf(4))
			{
				inputMapper.ChangeMapping(3);
			}
			else if (loadedScene.IsTypeOf(16))
			{
				inputMapper.ChangeMapping(2);
			}
			else
			{
				inputMapper.ChangeMapping(4);
			}
		}

		public static SceneTransitionListener[] InstArraySceneTransitionListener(int size)
		{
			SceneTransitionListener[] array = new SceneTransitionListener[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneTransitionListener();
			}
			return array;
		}

		public static SceneTransitionListener[][] InstArraySceneTransitionListener(int size1, int size2)
		{
			SceneTransitionListener[][] array = new SceneTransitionListener[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneTransitionListener[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneTransitionListener();
				}
			}
			return array;
		}

		public static SceneTransitionListener[][][] InstArraySceneTransitionListener(int size1, int size2, int size3)
		{
			SceneTransitionListener[][][] array = new SceneTransitionListener[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneTransitionListener[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneTransitionListener[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneTransitionListener();
					}
				}
			}
			return array;
		}
	}
}
