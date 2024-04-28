namespace ca.jamdat.flight
{
	public class Scene : Viewport
	{
		public const sbyte noTransition = 0;

		public const sbyte dismiss = 1;

		public const sbyte launch = 2;

		public const sbyte launchPopUp = 3;

		public const sbyte transitionIsLaunch = -2;

		public const sbyte lastDismissParam = -3;

		public Scene mReturnScene;

		public override void destruct()
		{
		}

		public static void Launch(Scene nextScene)
		{
			StartTransition(2, nextScene, -2);
		}

		public static void LaunchPopUp(Scene nextScene)
		{
			StartTransition(3, nextScene, -2);
		}

		public static void Dismiss(int param)
		{
			FlApplication instance = FlApplication.GetInstance();
			Scene mCurrentScene = instance.mCurrentScene;
			Scene scene = mCurrentScene.mReturnScene;
			if (scene != null)
			{
				StartTransition(1, scene, param);
				return;
			}
			instance.TakeFocus();
			FlPenManager.Get().Deactivate();
			instance.mCurrentScene = null;
			mCurrentScene.SetViewport(null);
			mCurrentScene.OnDelete();
		}

		public virtual void CompleteTransition()
		{
			FlApplication instance = FlApplication.GetInstance();
			sbyte mCurrentTransitionType = instance.mCurrentTransitionType;
			Scene scene = ((mCurrentTransitionType == 1) ? instance.mCurrentScene : mReturnScene);
			Viewport viewport = null;
			if (scene != null)
			{
				if (GetViewport() == null)
				{
					viewport = scene.GetViewport();
				}
				switch (mCurrentTransitionType)
				{
				case 1:
					SendMsg(scene, -110, -1);
					instance.mCurrentScene = this;
					break;
				case 2:
					scene.SetViewport(null);
					break;
				}
				if (viewport != null)
				{
					SetViewport(viewport);
				}
			}
			instance.ResetDownKeys();
			TakeFocus();
			FlPenManager.Get().Activate();
			instance.mCurrentTransitionType = 0;
		}

		public virtual void DetachAndSkip(Component sourceScene, int msg, int param)
		{
			SetViewport(null);
			mReturnScene.SendMsg(sourceScene, msg, param);
		}

		public virtual void OnDelete()
		{
		}

		public override bool OnDefaultMsg(Component source, int msg, int param)
		{
			switch (msg)
			{
			case -111:
				CompleteTransition();
				return true;
			case -112:
				source.SetViewport(null);
				((Scene)source).OnDelete();
				return true;
			case -110:
			{
				Scene scene = (Scene)source;
				Scene scene2 = null;
				while (scene != this)
				{
					scene2 = scene.mReturnScene;
					scene.mReturnScene = null;
					scene2.SendMsg(scene, -112, -1);
					scene = scene2;
				}
				return true;
			}
			case -109:
				if (mReturnScene != null)
				{
					DetachAndSkip(source, msg, param);
				}
				else
				{
					FlApplication instance = FlApplication.GetInstance();
					SendMsg(instance.mCurrentScene, -110, -1);
					instance.mCurrentScene = this;
					instance.mCurrentTransitionType = 0;
					Dismiss(-3);
				}
				return true;
			default:
				return false;
			}
		}

		public static void StartTransition(sbyte transitionType, Scene nextScene, int param)
		{
			FlApplication instance = FlApplication.GetInstance();
			if (param == -2)
			{
				nextScene.mReturnScene = instance.mCurrentScene;
				instance.mCurrentScene = nextScene;
			}
			instance.mCurrentTransitionType = transitionType;
			instance.TakeFocus();
			FlPenManager.Get().Deactivate();
			nextScene.SendMsg(instance.mCurrentScene, -111, param);
		}

		public static Scene[] InstArrayScene(int size)
		{
			Scene[] array = new Scene[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Scene();
			}
			return array;
		}

		public static Scene[][] InstArrayScene(int size1, int size2)
		{
			Scene[][] array = new Scene[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scene[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scene();
				}
			}
			return array;
		}

		public static Scene[][][] InstArrayScene(int size1, int size2, int size3)
		{
			Scene[][][] array = new Scene[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Scene[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Scene[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Scene();
					}
				}
			}
			return array;
		}
	}
}
