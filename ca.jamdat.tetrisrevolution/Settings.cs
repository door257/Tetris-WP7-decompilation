namespace ca.jamdat.tetrisrevolution
{
	public class Settings
	{
		public virtual void destruct()
		{
		}

		public static Settings Get()
		{
			return GameApp.Get().GetSettings();
		}

		public virtual void Read(FileSegmentStream inputStream)
		{
			if (inputStream.HasValidData())
			{
				bool soundEnabled_ = inputStream.ReadBoolean();
				int num = inputStream.ReadLong();
				int num2 = inputStream.ReadLong();
				MediaPlayer mediaPlayer = GameApp.Get().GetMediaPlayer();
				mediaPlayer.SetSoundEnabled_(soundEnabled_);
				SetApplicationLanguage((short)num);
				SetUserSelectedLanguage((short)num2);
			}
		}

		public virtual void Write(FileSegmentStream outputStream)
		{
			MediaPlayer mediaPlayer = GameApp.Get().GetMediaPlayer();
			bool value = mediaPlayer.IsSoundEnabled_();
			int applicationLanguage = GetApplicationLanguage();
			int userSelectedLanguage = GetUserSelectedLanguage();
			outputStream.WriteBoolean(value);
			outputStream.WriteLong(applicationLanguage);
			outputStream.WriteLong(userSelectedLanguage);
			outputStream.SetValidDataFlag(true);
		}

		public virtual void Reset()
		{
			GameApp.Get().GetMediaPlayer().Reset();
		}

		public virtual void SetSoundEnabled(bool enabled)
		{
			GameApp.Get().GetMediaPlayer().SetSoundEnabled_(enabled);
		}

		public virtual void SetSoundEnabled2(bool enabled)
		{
			GameApp.Get().GetMediaPlayer().SetSoundEnabled2_(enabled);
		}

		public virtual bool IsSoundEnabled()
		{
			return GameApp.Get().GetMediaPlayer().IsSoundEnabled_();
		}

		public virtual void SetApplicationLanguage(short language)
		{
			GameApp.Get().GetLanguageManager().SetLanguage(language);
		}

		public virtual short GetApplicationLanguage()
		{
			return GameApp.Get().GetLanguageManager().GetLanguage();
		}

		public virtual void SetUserSelectedLanguage(short language)
		{
			GameApp.Get().GetLanguageManager().SetUserSelectedLanguage(language);
		}

		public virtual short GetUserSelectedLanguage()
		{
			return GameApp.Get().GetLanguageManager().GetUserSelectedLanguage();
		}

		public static Settings[] InstArraySettings(int size)
		{
			Settings[] array = new Settings[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Settings();
			}
			return array;
		}

		public static Settings[][] InstArraySettings(int size1, int size2)
		{
			Settings[][] array = new Settings[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Settings[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Settings();
				}
			}
			return array;
		}

		public static Settings[][][] InstArraySettings(int size1, int size2, int size3)
		{
			Settings[][][] array = new Settings[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Settings[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Settings[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Settings();
					}
				}
			}
			return array;
		}
	}
}
