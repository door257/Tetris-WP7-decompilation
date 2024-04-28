using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Splash : BaseScene
	{
		public int mSplashDuration;

		public bool mNeedToShowSplash;

		public int mTimeElapsed;

		public Splash(int sceneId, int packageId, int splashDuration)
			: base(sceneId, packageId)
		{
			mSplashDuration = splashDuration;
			mNeedToShowSplash = true;
			mType = 1;
		}

		public override void destruct()
		{
		}

		public override void Unload()
		{
			mTimeElapsed = 0;
			if (mNeedToShowSplash)
			{
				mNeedToShowSplash = false;
				AnimationController animator = GameApp.Get().GetAnimator();
				sbyte animId = -1;
				bool flag = false;
				if (GetId() == 4)
				{
					animId = 4;
					flag = true;
				}
				if (flag)
				{
					animator.UnloadSingleAnimation(animId);
				}
			}
			base.Unload();
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(7, 0);
			GetSoftkeyViewport().SetVisible(false);
			if (mNeedToShowSplash)
			{
				if (mSplashDuration != 0)
				{
					mView.RegisterInGlobalTime();
				}
				if (GetId() == 2)
				{
					Text text = null;
					text = Text.Cast(mPackage.GetEntryPoint(2), null);
					text.CenterInRect(0, 0, 480, 800);
				}
				AnimationController animator = GameApp.Get().GetAnimator();
				sbyte animId = -1;
				int timeSystemEntryPoint = -1;
				bool flag = false;
				if (GetId() == 4)
				{
					animId = 4;
					timeSystemEntryPoint = 2;
					flag = true;
				}
				if (flag)
				{
					animator.LoadSingleAnimation(mMetaPackage.GetPackage(), animId, timeSystemEntryPoint);
					animator.StartMenuAnimation(animId);
				}
			}
			else
			{
				OnCommand(-2);
			}
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			mTimeElapsed += deltaTimeMs;
			if (mTimeElapsed > mSplashDuration)
			{
				mView.UnRegisterInGlobalTime();
				OnCommand(-2);
			}
		}

		public override void OnSceneAttached()
		{
			if (GetId() == 5 || GetId() == 2)
			{
				base.OnSceneAttached();
			}
		}

		public override void OnScreenSizeChange()
		{
			base.OnScreenSizeChange();
			if (mView != null)
			{
				mView.UnRegisterInGlobalTime();
			}
		}

		public override void StartMusic()
		{
		}

		public override bool OnKeyUp(int key)
		{
			if (0 == 0)
			{
				return base.OnKeyUp(key);
			}
			return true;
		}
	}
}
