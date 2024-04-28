using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SoundResourcesHandler
	{
		public MetaPackage mMenuSoundsPackage;

		public MetaPackage mGameSoundsPackage;

		public virtual void destruct()
		{
		}

		public virtual Sound GetSound(int soundId)
		{
			Package package = null;
			int packageEntryPointIndex = SoundId.GetPackageEntryPointIndex(soundId);
			if (IsAMenuSound(soundId))
			{
				package = mMenuSoundsPackage.GetPackage();
			}
			else if (IsAGameSound(soundId))
			{
				package = mGameSoundsPackage.GetPackage();
			}
			Sound sound = null;
			return Sound.Cast(package.GetEntryPoint(packageEntryPointIndex), null);
		}

		public virtual void LoadMenuSounds()
		{
			if (mMenuSoundsPackage == null)
			{
				mMenuSoundsPackage = GameLibrary.GetPackage(557073);
			}
		}

		public virtual void UnloadMenuSounds()
		{
			if (mMenuSoundsPackage != null)
			{
				GameLibrary.ReleasePackage(mMenuSoundsPackage);
				mMenuSoundsPackage = null;
			}
		}

		public virtual bool AreMenuSoundsLoaded()
		{
			if (mMenuSoundsPackage != null)
			{
				return mMenuSoundsPackage.IsLoaded();
			}
			return false;
		}

		public virtual void LoadGameSounds(int gameMusicPackageId)
		{
			if (mGameSoundsPackage == null)
			{
				mGameSoundsPackage = GameLibrary.GetPackage(gameMusicPackageId);
			}
		}

		public virtual void UnloadGameSounds()
		{
			if (mGameSoundsPackage != null)
			{
				GameLibrary.ReleasePackage(mGameSoundsPackage);
				mGameSoundsPackage = null;
			}
		}

		public virtual bool AreGameSoundsLoaded()
		{
			if (mGameSoundsPackage != null)
			{
				return mGameSoundsPackage.IsLoaded();
			}
			return false;
		}

		public static bool IsAGameSound(int id)
		{
			if (id >= 1)
			{
				return id < 5;
			}
			return false;
		}

		public virtual void Terminate()
		{
			UnloadMenuSounds();
			UnloadGameSounds();
		}

		public static bool IsAMenuSound(int id)
		{
			if (id >= 0)
			{
				return id < 1;
			}
			return false;
		}

		public static SoundResourcesHandler[] InstArraySoundResourcesHandler(int size)
		{
			SoundResourcesHandler[] array = new SoundResourcesHandler[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SoundResourcesHandler();
			}
			return array;
		}

		public static SoundResourcesHandler[][] InstArraySoundResourcesHandler(int size1, int size2)
		{
			SoundResourcesHandler[][] array = new SoundResourcesHandler[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundResourcesHandler[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundResourcesHandler();
				}
			}
			return array;
		}

		public static SoundResourcesHandler[][][] InstArraySoundResourcesHandler(int size1, int size2, int size3)
		{
			SoundResourcesHandler[][][] array = new SoundResourcesHandler[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundResourcesHandler[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundResourcesHandler[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SoundResourcesHandler();
					}
				}
			}
			return array;
		}
	}
}
