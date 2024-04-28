using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace ca.jamdat.flight
{
	public class SoundPlayer
	{
		public short mVolume;

		public bool mIsLooping;

		public bool mIsMuted;

		public bool mIsPaused;

		internal SoundEffect mSoundEffect;

		internal SoundEffectInstance mSndEffectInst;

		public Sound mSound;

		public SoundPlayer()
		{
			mVolume = 255;
			mIsMuted = SoundManager.Get().IsMuted();
		}

		public SoundPlayer(Sound sound)
		{
			SetSound(sound);
		}

		public virtual void SetSound(Sound sound)
		{
			mSound = sound;
			if (sound == null)
			{
				return;
			}
			try
			{
				if (sound.GetSoundFormat() == "audio/x-wav")
				{
					byte[] array = new byte[mSound.mDataBlob.GetData().Length];
					Buffer.BlockCopy(mSound.mDataBlob.GetData(), 0, array, 0, array.Length);
					MemoryStream stream = new MemoryStream(array);
					mSoundEffect = SoundEffect.FromStream(stream);
					mSndEffectInst = mSoundEffect.CreateInstance();
					mSndEffectInst.IsLooped = mIsLooping;
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void SetVolume(short volume)
		{
			mVolume = volume;
			if (mSound != null)
			{
				SetNativeVolume();
			}
		}

		public virtual short GetVolume()
		{
			return mVolume;
		}

		public virtual void SetLooping(bool isLooping)
		{
			mIsLooping = isLooping;
			if (mSndEffectInst != null)
			{
				mSndEffectInst.IsLooped = mIsLooping;
			}
		}

		public virtual bool IsLooping()
		{
			return mIsLooping;
		}

		public virtual void SetMuted(bool isMuted)
		{
		}

		public virtual bool IsMuted()
		{
			return mIsMuted;
		}

		public virtual void SetNativeVolume()
		{
		}

		public virtual Sound GetSound()
		{
			return mSound;
		}

		public virtual void Play()
		{
			try
			{
				if (mIsPaused)
				{
					mIsPaused = false;
					if (mSound.GetSoundFormat() == "audio/x-wav")
					{
						mSndEffectInst.Resume();
					}
				}
				else if (mSound.GetSoundFormat() == "audio/x-wav")
				{
					mSndEffectInst.Play();
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void Pause()
		{
			if (mSound != null)
			{
				mIsPaused = true;
				if (mSound.GetSoundFormat() == "audio/x-wav")
				{
					mSndEffectInst.Pause();
				}
			}
		}

		public virtual void Stop()
		{
			mIsPaused = false;
			if (mSound != null && mSound.GetSoundFormat() == "audio/x-wav")
			{
				mSndEffectInst.Stop();
			}
		}

		public virtual bool IsPaused()
		{
			return mIsPaused;
		}

		public virtual bool IsPlaying()
		{
			if (mSound == null)
			{
				return false;
			}
			return !mIsPaused;
		}

		public static SoundPlayer[] InstArraySoundPlayer(int size)
		{
			SoundPlayer[] array = new SoundPlayer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SoundPlayer();
			}
			return array;
		}

		public static SoundPlayer[][] InstArraySoundPlayer(int size1, int size2)
		{
			SoundPlayer[][] array = new SoundPlayer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundPlayer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundPlayer();
				}
			}
			return array;
		}

		public static SoundPlayer[][][] InstArraySoundPlayer(int size1, int size2, int size3)
		{
			SoundPlayer[][][] array = new SoundPlayer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundPlayer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundPlayer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SoundPlayer();
					}
				}
			}
			return array;
		}
	}
}
