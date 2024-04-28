namespace ca.jamdat.flight
{
	public class SoundManager
	{
		public J2SESoundManagerImp mImplementor;

		public SoundPlayer[] mChannels;

		public short mMasterVolume;

		public short mScaledMasterVolume;

		public bool mIsMuted;

		public bool mIsPauseSoundsOnInterrupt;

		public SoundManager()
		{
			mMasterVolume = 255;
			mImplementor = new J2SESoundManagerImp();
			mScaledMasterVolume = CalculateScaledMasterVolume();
			FrameworkGlobals.GetInstance().soundManager = this;
			mChannels = SoundPlayer.InstArraySoundPlayer(1);
		}

		public static SoundManager Get()
		{
			if (FrameworkGlobals.GetInstance().soundManager == null)
			{
				FrameworkGlobals.GetInstance().soundManager = new SoundManager();
			}
			return FrameworkGlobals.GetInstance().soundManager;
		}

		public virtual void destruct()
		{
			mChannels = null;
		}

		public virtual SoundPlayer GetChannelSoundPlayer(int channelIndex)
		{
			return mChannels[channelIndex];
		}

		public virtual void SetMuted(bool isMuted)
		{
		}

		public virtual bool IsMuted()
		{
			return mIsMuted;
		}

		public virtual void SetPauseSoundsOnInterrupt()
		{
			mIsPauseSoundsOnInterrupt = true;
		}

		public virtual bool IsPauseSoundsOnInterrupt()
		{
			return mIsPauseSoundsOnInterrupt;
		}

		public virtual void PauseAllSoundPlayers()
		{
			SongPlayer.Get.Pause();
			for (int i = 0; i < 1; i++)
			{
				mChannels[i].Pause();
			}
		}

		public virtual void ResumeAllSoundPlayers()
		{
			SongPlayer.Get.Play();
			for (int i = 0; i < 1; i++)
			{
				if (mChannels[i].IsPaused())
				{
					mChannels[i].Play();
				}
			}
		}

		public virtual void StopAllSoundPlayers()
		{
			SongPlayer.Get.Stop();
			for (int i = 0; i < 1; i++)
			{
				mChannels[i].Stop();
			}
		}

		public virtual void SetMasterVolume(short volume)
		{
			if (volume > 255)
			{
				volume = 255;
			}
			if (volume < 0)
			{
				volume = 0;
			}
			mMasterVolume = volume;
			mScaledMasterVolume = CalculateScaledMasterVolume();
			for (int i = 0; i < 1; i++)
			{
				mChannels[i].SetNativeVolume();
			}
			FrameworkGlobals.GetInstance().application.OnMasterVolumeChange(volume);
		}

		public virtual short GetMasterVolume()
		{
			return mMasterVolume;
		}

		public virtual short GetScaledMasterVolume()
		{
			return mScaledMasterVolume;
		}

		public static void IncMasterVolume()
		{
			SoundManager soundManager = FrameworkGlobals.GetInstance().soundManager;
			soundManager.SetMasterVolume((short)(soundManager.mMasterVolume + 51));
		}

		public static void DecMasterVolume()
		{
			SoundManager soundManager = FrameworkGlobals.GetInstance().soundManager;
			soundManager.SetMasterVolume((short)(soundManager.mMasterVolume - 51));
		}

		public static int CalculateNativePlayerVolume(short aPlayerVolume, int aMaxVolume)
		{
			return aMaxVolume * aPlayerVolume * FrameworkGlobals.GetInstance().soundManager.GetScaledMasterVolume() >> 16;
		}

		public virtual short CalculateScaledMasterVolume()
		{
			if (SpecConstants.GetMasterVolumeScaleFactor() > 0)
			{
				return (short)(mMasterVolume << (int)(short)SpecConstants.GetMasterVolumeScaleFactor());
			}
			if (SpecConstants.GetMasterVolumeScaleFactor() < 0)
			{
				return (short)(mMasterVolume >> (int)(short)(-SpecConstants.GetMasterVolumeScaleFactor()));
			}
			return mMasterVolume;
		}

		public static int CalculateNativePlayerVolume(short aPlayerVolume)
		{
			return CalculateNativePlayerVolume(aPlayerVolume, 100);
		}

		public static SoundManager[] InstArraySoundManager(int size)
		{
			SoundManager[] array = new SoundManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SoundManager();
			}
			return array;
		}

		public static SoundManager[][] InstArraySoundManager(int size1, int size2)
		{
			SoundManager[][] array = new SoundManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundManager();
				}
			}
			return array;
		}

		public static SoundManager[][][] InstArraySoundManager(int size1, int size2, int size3)
		{
			SoundManager[][][] array = new SoundManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SoundManager();
					}
				}
			}
			return array;
		}
	}
}
