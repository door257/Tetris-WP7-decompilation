using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Menu : BaseScene
	{
		public AnimationController mAnimator;

		public MetaPackage mCommonMetaPackage;

		public TimerSequence mAnimationTimerSequence;

		public Shape mAnimationShape;

		public Menu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			mAnimator = GameApp.Get().GetAnimator();
			mType = 2;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mCommonMetaPackage = GameLibrary.GetPackage(1310760);
			if (!IsSilentMenu())
			{
				GameApp.Get().GetMediaPlayer().GetSoundResourcesHandler()
					.LoadMenuSounds();
			}
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
		}

		public override void Initialize()
		{
			base.Initialize();
			StartMusic();
			mClearSoftKey.SetEnabled(false);
			mSelectSoftKey.SetEnabled(false);
		}

		public override bool IsLoaded()
		{
			bool flag = mCommonMetaPackage != null && mCommonMetaPackage.IsLoaded();
			if (base.IsLoaded() && flag)
			{
				if (!IsSilentMenu())
				{
					return GameApp.Get().GetMediaPlayer().GetSoundResourcesHandler()
						.AreMenuSoundsLoaded();
				}
				return true;
			}
			return false;
		}

		public override void Unload()
		{
			DestroyTransitionAnimation();
			GameApp.Get().GetTouchMenuReceiver().Unload();
			if (mCommonMetaPackage != null)
			{
				GameLibrary.ReleasePackage(mCommonMetaPackage);
				mCommonMetaPackage = null;
			}
			base.Unload();
		}

		public override void Suspend()
		{
			base.Suspend();
			if (GameApp.Get().GetSettings().IsSoundEnabled())
			{
				MediaPlayer mediaPlayer = GameApp.Get().GetMediaPlayer();
				mediaPlayer.StopMusic();
				mediaPlayer.SetMenuMusicPlaying(false);
			}
		}

		public override void StartMusic()
		{
			GameApp gameApp = GameApp.Get();
			MediaPlayer mediaPlayer = gameApp.GetMediaPlayer();
			if (gameApp.GetSettings().IsSoundEnabled() && !mediaPlayer.IsMenuMusicPlaying() && !IsSilentMenu() && mediaPlayer.GetSoundResourcesHandler().AreMenuSoundsLoaded())
			{
				mediaPlayer.SetMenuMusicPlaying(true);
				mediaPlayer.PlayMusic(0, true);
			}
		}

		public override void StartOpeningAnims()
		{
			base.StartOpeningAnims();
			mAnimator.StartMenuAnimation(2);
		}

		public override void StartClosingAnims()
		{
			base.StartClosingAnims();
			mAnimator.StartMenuAnimation(2);
		}

		public override bool IsOpeningAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !mAnimator.IsPlaying(2);
			}
			return false;
		}

		public override bool IsClosingAnimsEnded()
		{
			if (base.IsOpeningAnimsEnded())
			{
				return !mAnimator.IsPlaying(2);
			}
			return false;
		}

		public override void OnOpeningAnimsEnded()
		{
			int id = GetId();
			if (id != 22)
			{
				mSelectSoftKey.SetEnabled(true);
			}
			DestroyTransitionAnimation();
		}

		public override void CreateOpeningAnims()
		{
			mAnimationTimerSequence = new TimerSequence(GetNumOpeningAnims());
			mAnimationShape = new Shape();
			mAnimationShape.SetViewport(mViewport);
			mViewport.PutComponentBehind(GetSoftkeyViewport(), mAnimationShape);
			KeyFrameController keyFrameController = EntryPoint.GetKeyFrameController(1212453, 0);
			keyFrameController.SetControllee(mAnimationShape);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, 333);
			keyFrameController = EntryPoint.GetKeyFrameController(1212453, 1);
			keyFrameController.SetControllee(mAnimationShape);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, 0, 333);
			mAnimator.ExternalRegisterAnimation(2, mAnimationTimerSequence, GetOpeningAnimsDuration());
		}

		public override void CreateClosingAnims(int startTime)
		{
			mAnimationTimerSequence = new TimerSequence(GetNumClosingAnims());
			mAnimationShape = new Shape();
			mAnimationShape.SetViewport(mViewport);
			mViewport.PutComponentBehind(GetSoftkeyViewport(), mAnimationShape);
			KeyFrameController keyFrameController = EntryPoint.GetKeyFrameController(1212453, 2);
			keyFrameController.SetControllee(mAnimationShape);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, startTime, 333);
			keyFrameController = EntryPoint.GetKeyFrameController(1212453, 3);
			keyFrameController.SetControllee(mAnimationShape);
			mAnimationTimerSequence.RegisterInterval(keyFrameController, startTime, 333);
			mAnimator.ExternalRegisterAnimation(2, mAnimationTimerSequence, GetClosingAnimsDuration());
		}

		public virtual int GetNumOpeningAnims()
		{
			return 2;
		}

		public virtual int GetNumClosingAnims()
		{
			return 2;
		}

		public virtual int GetOpeningAnimsDuration()
		{
			return 333;
		}

		public virtual int GetClosingAnimsDuration()
		{
			return 333;
		}

		public virtual void DestroyTransitionAnimation()
		{
			if (mAnimationTimerSequence != null)
			{
				for (int i = 0; i < mAnimationTimerSequence.GetNbChildren(); i++)
				{
					KeyFrameController keyFrameController = (KeyFrameController)mAnimationTimerSequence.GetChild(i);
					if (keyFrameController != null)
					{
						keyFrameController.GetKeySequence();
						keyFrameController = null;
					}
				}
				mAnimationTimerSequence.UnRegisterAll();
				GameApp.Get().GetAnimator().UnloadSingleAnimation(2);
				mAnimationTimerSequence = null;
				mAnimationTimerSequence = null;
			}
			if (mAnimationShape != null)
			{
				mAnimationShape.SetViewport(null);
				mAnimationShape = null;
				mAnimationShape = null;
			}
		}

		public virtual Shape GetAnimationShape()
		{
			return mAnimationShape;
		}

		public virtual bool IsSilentMenu()
		{
			int id = GetId();
			if (id != 19 && id != 6 && id != 7)
			{
				return id == 8;
			}
			return true;
		}
	}
}
