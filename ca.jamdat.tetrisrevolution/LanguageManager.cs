using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LanguageManager
	{
		public short mLanguage;

		public short[] mSupportedLanguages;

		public short mUserSelectedLanguage;

		public LanguageManager()
		{
			mLanguage = 11;
			mUserSelectedLanguage = 2;
			short[] array = new short[5] { 9, 11, 12, 14, 19 };
			mSupportedLanguages = new short[5];
			for (int i = 0; i < 5; i++)
			{
				mSupportedLanguages[i] = array[i];
			}
		}

		public virtual void destruct()
		{
			mSupportedLanguages = null;
		}

		public static int GetLanguageCount()
		{
			return 5;
		}

		public virtual int GetLanguageIndex(short language)
		{
			for (int i = 0; i < 5; i++)
			{
				if (language == mSupportedLanguages[i])
				{
					return i;
				}
			}
			return -1;
		}

		public virtual short GetLanguageFromIndex(int index)
		{
			return mSupportedLanguages[index];
		}

		public virtual short GetLanguage()
		{
			return mLanguage;
		}

		public virtual short QueryLanguage()
		{
			Settings settings = GameApp.Get().GetSettings();
			short bestLang = FlLang.GetBestLang();
			short num = ((settings.GetApplicationLanguage() != settings.GetUserSelectedLanguage()) ? settings.GetUserSelectedLanguage() : settings.GetApplicationLanguage());
			if (num == 2)
			{
				return bestLang;
			}
			return num;
		}

		public virtual short[] GetSupportedLanguage()
		{
			return mSupportedLanguages;
		}

		public virtual void SetUserSelectedLanguage(short lang)
		{
			mUserSelectedLanguage = lang;
		}

		public virtual short GetUserSelectedLanguage()
		{
			return mUserSelectedLanguage;
		}

		public virtual void SetLanguage(short language)
		{
			if (GetLanguageIndex(language) != -1)
			{
				mLanguage = language;
			}
			else
			{
				mLanguage = 11;
			}
		}

		public virtual bool IsLanguageSupported(short language)
		{
			return GetLanguageIndex(language) != -1;
		}

		public static LanguageManager[] InstArrayLanguageManager(int size)
		{
			LanguageManager[] array = new LanguageManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LanguageManager();
			}
			return array;
		}

		public static LanguageManager[][] InstArrayLanguageManager(int size1, int size2)
		{
			LanguageManager[][] array = new LanguageManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LanguageManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LanguageManager();
				}
			}
			return array;
		}

		public static LanguageManager[][][] InstArrayLanguageManager(int size1, int size2, int size3)
		{
			LanguageManager[][][] array = new LanguageManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LanguageManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LanguageManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LanguageManager();
					}
				}
			}
			return array;
		}
	}
}
