using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SharedResourcesHandler
	{
		public int mLockedState;

		public SharedResourcesHandler()
		{
			mLockedState = 0;
		}

		public virtual void destruct()
		{
		}

		public virtual void LoadAppResources()
		{
			GameApp gameApp = GameApp.Get();
			MetaPackage package = GameLibrary.GetPackage(1310760);
			Package package2 = package.GetPackage();
			if (!gameApp.GetAnimator().IsValid(0))
			{
				gameApp.GetAnimator().LoadAnimations(package2, 0, 200, 1);
			}
			GameLibrary.ReleasePackage(package);
		}

		public virtual void AcquireAppResources()
		{
			if (BitField.IsBitOff(mLockedState, 1))
			{
				GameApp gameApp = GameApp.Get();
				GameLibrary library = gameApp.GetLibrary();
				library.LockPackageMemory(-2144239522);
				library.LockPackageMemory(3047517);
				library.LockPackageMemory(1310760);
				mLockedState = BitField.SetBitOn(mLockedState, 1);
			}
		}

		public virtual void ReleaseAppResources()
		{
			if (BitField.IsBitOn(mLockedState, 1))
			{
				GameApp gameApp = GameApp.Get();
				GameLibrary library = gameApp.GetLibrary();
				gameApp.GetAnimator().UnloadAnimations(0, 1);
				library.UnlockPackageMemory(-2144239522);
				library.UnlockPackageMemory(3047517);
				library.UnlockPackageMemory(1310760);
				mLockedState = BitField.SetBitOff(mLockedState, 1);
			}
		}

		public virtual void AcquireMenusResources()
		{
			if (BitField.IsBitOff(mLockedState, 2))
			{
				GameApp gameApp = GameApp.Get();
				GameLibrary library = gameApp.GetLibrary();
				library.LockPackageMemory(-2144075681);
				library.LockPackageMemory(557073);
				mLockedState = BitField.SetBitOn(mLockedState, 2);
			}
		}

		public virtual void ReleaseMenusResources()
		{
			if (BitField.IsBitOn(mLockedState, 2))
			{
				GameApp gameApp = GameApp.Get();
				GameLibrary library = gameApp.GetLibrary();
				gameApp.GetAnimator().UnloadAnimations(3, 1);
				library.UnlockPackageMemory(-2144075681);
				MediaPlayer mediaPlayer = gameApp.GetMediaPlayer();
				mediaPlayer.StopMusic();
				library.UnlockPackageMemory(557073);
				mediaPlayer.GetSoundResourcesHandler().UnloadMenuSounds();
				mediaPlayer.SetMenuMusicPlaying(false);
				mLockedState = BitField.SetBitOff(mLockedState, 2);
			}
		}

		public static SharedResourcesHandler[] InstArraySharedResourcesHandler(int size)
		{
			SharedResourcesHandler[] array = new SharedResourcesHandler[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SharedResourcesHandler();
			}
			return array;
		}

		public static SharedResourcesHandler[][] InstArraySharedResourcesHandler(int size1, int size2)
		{
			SharedResourcesHandler[][] array = new SharedResourcesHandler[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SharedResourcesHandler[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SharedResourcesHandler();
				}
			}
			return array;
		}

		public static SharedResourcesHandler[][][] InstArraySharedResourcesHandler(int size1, int size2, int size3)
		{
			SharedResourcesHandler[][][] array = new SharedResourcesHandler[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SharedResourcesHandler[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SharedResourcesHandler[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SharedResourcesHandler();
					}
				}
			}
			return array;
		}
	}
}
