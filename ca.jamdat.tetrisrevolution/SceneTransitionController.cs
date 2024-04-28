using System;
using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SceneTransitionController : BaseController
	{
		public Controllable mTimeControlled;

		public BaseScene mLoadingScene;

		public BaseScene mUnloadingScene;

		public BaseScene mLoadedScene;

		public BaseScene mPendingTransitionScene;

		public bool mTerminated;

		public Viewport mTransitionViewport;

		public short mLoadingSceneWidth;

		public short mLoadingSceneHeight;

		public Shape mTransitionBackground;

		public SceneTransitionController()
		{
			mTimeControlled = new Controllable();
			mTimeControlled.SetController(this);
			GameApp viewport = GameApp.Get();
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			mTransitionViewport = new Viewport();
			mTransitionViewport.SetRect(screenRect);
			mTransitionViewport.SetViewport(viewport);
			mTransitionBackground = new Shape();
			mTransitionBackground.SetColor(new Color888(0, 0, 0));
			mTransitionBackground.SetRect(screenRect);
			mTransitionBackground.SetViewport(viewport);
			mTransitionBackground.SendToBack();
		}

		public override void destruct()
		{
			mTimeControlled = null;
			mTransitionViewport.SetViewport(null);
			mTransitionViewport = null;
			mTransitionBackground.SetViewport(null);
			mTransitionBackground = null;
		}

		public virtual void EnqueueTransition(BaseScene scene)
		{
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			if (mTransitionViewport.GetRectWidth() != screenRect.GetWidth() || mTransitionViewport.GetRectHeight() != screenRect.GetHeight())
			{
				mTransitionViewport.SetViewport(null);
				mTransitionViewport.SetRect(screenRect);
				mTransitionBackground.SetViewport(GameApp.Get());
				mTransitionBackground.SetRect(screenRect);
				mTransitionBackground.SendToBack();
			}
			if (!mTerminated)
			{
				AcquireScene(scene);
				if (!IsTransiting())
				{
					StartTransition(scene, mLoadedScene);
				}
				else if (HasPendingTransition())
				{
					ReleaseScene(mPendingTransitionScene);
					mPendingTransitionScene = scene;
				}
				else
				{
					mPendingTransitionScene = scene;
				}
			}
			else
			{
				ReleaseScene(scene);
			}
		}

		public virtual void Terminate()
		{
			mTimeControlled.UnRegisterInGlobalTime();
			if (mLoadingScene != null)
			{
				UnloadAndReleaseScene(mLoadingScene);
				mLoadingScene = null;
			}
			if (mUnloadingScene != null)
			{
				UnloadAndReleaseScene(mUnloadingScene);
				mUnloadingScene = null;
				mLoadedScene = null;
			}
			if (mPendingTransitionScene != null)
			{
				ReleaseScene(mPendingTransitionScene);
				mPendingTransitionScene = null;
			}
			if (mLoadedScene != null)
			{
				UnloadAndReleaseScene(mLoadedScene);
				mLoadedScene = null;
			}
			mTerminated = true;
		}

		public override void OnTime(int totalTimeMs, int elapsedTimeMs)
		{
			GameApp gameApp = GameApp.Get();
			Hourglass hourglass = gameApp.GetHourglass();
			if (IsUnloading())
			{
				BaseScene baseScene = mUnloadingScene;
				if (baseScene.GetTransitionState() == 6)
				{
					SceneTransitionListener.SceneUnloadingStarted(baseScene);
					baseScene.CreateClosingAnims(0);
					baseScene.StartClosingAnims();
					baseScene.SetTransitionState(7);
				}
				if (baseScene.GetTransitionState() == 7 && baseScene.IsClosingAnimsEnded() && !hourglass.IsVisible())
				{
					hourglass.SetTopLeft(224, 384);
					hourglass.SetVisible(true);
					GC.Collect();
					if (baseScene.IsTypeOf(2))
					{
						Menu menu = (Menu)baseScene;
						if (menu.GetAnimationShape() != null)
						{
							Viewport viewport = EntryPoint.GetViewport(1310760, 186);
							viewport.GetViewport().PutComponentBehind(viewport, menu.GetAnimationShape());
						}
					}
					baseScene.SetTransitionState(8);
				}
				else if (baseScene.GetTransitionState() == 8)
				{
					baseScene.SerializeObjects();
					baseScene.SetTransitionState(2);
				}
				if (baseScene.GetTransitionState() == 2 && baseScene.SaveFiles(1))
				{
					baseScene.SetTransitionState(9);
				}
				if (baseScene.GetTransitionState() == 9)
				{
					UnloadAndReleaseScene(mUnloadingScene);
					mUnloadingScene = null;
					baseScene = null;
					gameApp.GetLibrary().UnloadIrrelevantPackages();
				}
				if (baseScene == null && HasPendingTransition())
				{
					ReleaseScene(mLoadingScene);
					mLoadingScene = mPendingTransitionScene;
					mPendingTransitionScene = null;
				}
			}
			if (IsLoading() && !IsUnloading())
			{
				BaseScene baseScene2 = mLoadingScene;
				if (baseScene2.GetTransitionState() == 0)
				{
					GameLibrary library = gameApp.GetLibrary();
					bool immediateLoadModeEnabled = library.SetImmediateLoadModeEnabled(true);
					SceneTransitionListener.SceneLoadingStarted(baseScene2);
					baseScene2.Load();
					baseScene2.SetTransitionState(1);
					library.SetImmediateLoadModeEnabled(immediateLoadModeEnabled);
					FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
					mLoadingSceneWidth = screenRect.GetWidth();
					mLoadingSceneHeight = screenRect.GetHeight();
					baseScene2.SerializeObjects();
				}
				if (baseScene2.GetTransitionState() == 1 && baseScene2.IsLoaded())
				{
					baseScene2.SetTransitionState(2);
				}
				if (baseScene2.GetTransitionState() == 2 && baseScene2.SaveFiles(0))
				{
					baseScene2.SetTransitionState(3);
				}
				else if (baseScene2.GetTransitionState() == 3)
				{
					baseScene2.Initialize();
					baseScene2.CreateOpeningAnims();
					baseScene2.SetTransitionState(4);
				}
				else if (baseScene2.GetTransitionState() == 4)
				{
					AttachScene(baseScene2);
					hourglass.SetVisible(false);
					baseScene2.SetTransitionState(5);
					baseScene2.StartOpeningAnims();
				}
				else if (baseScene2.GetTransitionState() == 5 && baseScene2.IsOpeningAnimsEnded())
				{
					baseScene2.OnOpeningAnimsEnded();
					gameApp.ResetDownKeys();
					mLoadedScene = baseScene2;
					mLoadingScene = null;
					baseScene2.SetTransitionState(6);
					if (HasPendingTransition())
					{
						StartTransition(mPendingTransitionScene, baseScene2);
						mPendingTransitionScene = null;
					}
					else
					{
						baseScene2.ReceiveFocus();
						FlPenManager.Get().Activate();
					}
				}
			}
			if (!IsTransiting())
			{
				mTimeControlled.UnRegisterInGlobalTime();
			}
		}

		public virtual bool IsTransiting()
		{
			if (mLoadedScene == null)
			{
				if (!IsLoading())
				{
					return IsUnloading();
				}
				return true;
			}
			return false;
		}

		public virtual void StartTransition(BaseScene loadingScene, BaseScene unloadingScene)
		{
			GameApp.Get().TakeFocus();
			mLoadingScene = loadingScene;
			mUnloadingScene = unloadingScene;
			mLoadedScene = null;
			mTimeControlled.RegisterInGlobalTime();
			FlPenManager.Get().Deactivate();
		}

		public virtual bool HasPendingTransition()
		{
			return mPendingTransitionScene != null;
		}

		public virtual void UnloadAndReleaseScene(BaseScene scene)
		{
			DetachScene(scene);
			scene.Unload();
			scene.SetTransitionState(0);
			scene.DeleteView();
			ReleaseScene(scene);
		}

		public virtual bool IsLoading()
		{
			return mLoadingScene != null;
		}

		public virtual bool IsUnloading()
		{
			return mUnloadingScene != null;
		}

		public virtual void AttachScene(BaseScene scene)
		{
			scene.SetViewport(mTransitionViewport);
			if (mTransitionViewport.GetRectWidth() == mLoadingSceneWidth && mTransitionViewport.GetRectHeight() == mLoadingSceneHeight)
			{
				mTransitionViewport.SetViewport(GameApp.Get());
				mTransitionBackground.SetViewport(null);
			}
			scene.OnSceneAttached();
		}

		public virtual void DetachScene(BaseScene scene)
		{
			scene.SetViewport(null);
		}

		public static void AcquireScene(BaseScene scene)
		{
			scene.AddRef();
		}

		public static void ReleaseScene(BaseScene scene)
		{
			scene.RemoveRef();
		}

		public static SceneTransitionController[] InstArraySceneTransitionController(int size)
		{
			SceneTransitionController[] array = new SceneTransitionController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneTransitionController();
			}
			return array;
		}

		public static SceneTransitionController[][] InstArraySceneTransitionController(int size1, int size2)
		{
			SceneTransitionController[][] array = new SceneTransitionController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneTransitionController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneTransitionController();
				}
			}
			return array;
		}

		public static SceneTransitionController[][][] InstArraySceneTransitionController(int size1, int size2, int size3)
		{
			SceneTransitionController[][][] array = new SceneTransitionController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneTransitionController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneTransitionController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneTransitionController();
					}
				}
			}
			return array;
		}
	}
}
