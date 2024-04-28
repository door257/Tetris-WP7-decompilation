using System;
using Microsoft.Xna.Framework.Media;

namespace ca.jamdat.flight
{
	public class SongPlayer
	{
		private static SongPlayer mInstance = new SongPlayer();

		internal Song mSongInstance;

		public static SongPlayer Get
		{
			get
			{
				return mInstance;
			}
		}

		private SongPlayer()
		{
		}

		public virtual void SetSong(ESong pSong)
		{
			if (pSong != 0)
			{
				try
				{
					mSongInstance = FrameworkGlobals.GetInstance().ContentManager.Load<Song>("Content/" + pSong);
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
			}
		}

		public virtual void Play()
		{
			if (mSongInstance == null)
			{
				return;
			}
			try
			{
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Play(mSongInstance);
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void Resume()
		{
			if (mSongInstance == null)
			{
				return;
			}
			try
			{
				MediaPlayer.Resume();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void Pause()
		{
			if (mSongInstance == null)
			{
				return;
			}
			try
			{
				MediaPlayer.Pause();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void Stop()
		{
			if (mSongInstance == null)
			{
				return;
			}
			try
			{
				MediaPlayer.Stop();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}
	}
}
