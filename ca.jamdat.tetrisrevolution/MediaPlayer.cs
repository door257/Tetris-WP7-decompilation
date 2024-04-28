using ca.jamdat.flight;
using Microsoft.Xna.Framework.Media;

namespace ca.jamdat.tetrisrevolution
{
	public class MediaPlayer
	{
		public SoundResourcesHandler mSoundResourcesHandler;

		public bool mIsMenuMusicPlaying;

		public SoundPlayer mSoundPlayer;

		public bool mSoundEnabled;

		public MediaPlayer()
		{
			ReadDeviceSoundProfile();
			mSoundResourcesHandler = new SoundResourcesHandler();
			mSoundPlayer = SoundManager.Get().GetChannelSoundPlayer(0);
			mSoundEnabled = true;
			Reset();
		}

		public virtual void destruct()
		{
			mSoundResourcesHandler = null;
			StopSound();
			mSoundPlayer = null;
		}

		public static MediaPlayer Get()
		{
			return GameApp.Get().GetMediaPlayer();
		}

		public virtual void PlayMusic(int soundId, bool looping, bool fadeOutCurrent)
		{
			PlaySound(soundId, looping, fadeOutCurrent);
		}

		public virtual void StopMusic(bool a6)
		{
			StopSound();
		}

		public virtual SoundResourcesHandler GetSoundResourcesHandler()
		{
			return mSoundResourcesHandler;
		}

		public virtual bool IsMenuMusicPlaying()
		{
			return mIsMenuMusicPlaying;
		}

		public virtual void SetMenuMusicPlaying(bool isPlaying)
		{
			mIsMenuMusicPlaying = isPlaying;
		}

		public virtual bool IsMusicPlaying()
		{
			return mSoundPlayer.IsPlaying();
		}

		public virtual bool IsGameMusicPlaying()
		{
			return false;
		}

		public virtual bool IsUserMusicPlaying()
		{
			return false;
		}

		public virtual void PlaySoundFx(int soundId, bool looping, bool fadeOutCurrent)
		{
			PlaySound(soundId, looping, fadeOutCurrent);
		}

		public virtual void StopSoundFx(bool a7)
		{
			mSoundPlayer.Stop();
			mSoundPlayer.SetSound(null);
		}

		public virtual void Terminate()
		{
			StopSound();
		}

		public virtual void Reset()
		{
			mIsMenuMusicPlaying = false;
			mSoundEnabled = false;
			StopMusic();
		}

		public virtual void ReadDeviceSoundProfile()
		{
		}

		public virtual bool IsDeviceSoundProfileMuted()
		{
			return false;
		}

		public bool IsSong(int soundId)
		{
			if (soundId != 2 && soundId != 3 && soundId != 4)
			{
				return soundId == 0;
			}
			return true;
		}

		public ESong GetSong(int soundId)
		{
			switch (soundId)
			{
			case 2:
				return ESong.GameA;
			case 3:
				return ESong.GameB;
			case 4:
				return ESong.GameC;
			case 0:
				return ESong.Menu;
			default:
				return ESong.None;
			}
		}

		public virtual void PlaySound(int soundId, bool looping, bool fadeOutCurrent)
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				if (mSoundEnabled)
				{
					if (IsSong(soundId))
					{
						SongPlayer.Get.SetSong(GetSong(soundId));
						SongPlayer.Get.Play();
					}
					else
					{
						mSoundPlayer.SetSound(mSoundResourcesHandler.GetSound(soundId));
						mSoundPlayer.SetLooping(looping);
						mSoundPlayer.Play();
					}
				}
			}
			else
			{
				Settings settings = GameApp.Get().GetSettings();
				settings.SetSoundEnabled2(false);
			}
		}

		public virtual void StopSound()
		{
			mSoundPlayer.Stop();
			mSoundPlayer.SetSound(null);
		}

		public virtual void SetSoundEnabled2_(bool enabled)
		{
			mSoundEnabled = enabled;
			if (!enabled)
			{
				StopSound();
			}
		}

		public virtual void SetSoundEnabled_(bool enabled)
		{
			mSoundEnabled = enabled;
			if (!enabled)
			{
				StopSound();
				StopMusic();
			}
		}

		public virtual bool IsSoundEnabled_()
		{
			return mSoundEnabled;
		}

		public virtual void SetSoundVolume_(short volume)
		{
			mSoundPlayer.SetVolume(volume);
		}

		public virtual short GetSoundVolume_()
		{
			return mSoundPlayer.GetVolume();
		}

		public virtual void PlayMusic(int soundId)
		{
			PlayMusic(soundId, false);
		}

		public virtual void PlayMusic(int soundId, bool loop)
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				PlayMusic(soundId, loop, false);
				return;
			}
			Settings settings = GameApp.Get().GetSettings();
			settings.SetSoundEnabled2(false);
		}

		public virtual void StopMusic()
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				SongPlayer.Get.Stop();
			}
		}

		public virtual void PauseMusic()
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				SongPlayer.Get.Pause();
			}
		}

		public virtual void ResumeMusic()
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				SongPlayer.Get.Play();
				return;
			}
			Settings settings = GameApp.Get().GetSettings();
			settings.SetSoundEnabled2(false);
		}

		public virtual void PlaySoundFx(int soundId)
		{
			PlaySoundFx(soundId, false);
		}

		public virtual void PlaySoundFx(int soundId, bool loop)
		{
			PlaySoundFx(soundId, loop, false);
		}

		public virtual void StopSoundFx()
		{
			StopSoundFx(false);
		}
	}
}
