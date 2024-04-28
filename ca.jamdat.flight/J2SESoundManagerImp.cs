namespace ca.jamdat.flight
{
	public class J2SESoundManagerImp
	{
		public virtual void destruct()
		{
		}

		public virtual void OnAppPaused()
		{
			SoundManager.Get().PauseAllSoundPlayers();
		}

		public virtual void OnAppResumed()
		{
			SoundManager.Get().ResumeAllSoundPlayers();
		}

		public static J2SESoundManagerImp[] InstArrayJ2SESoundManagerImp(int size)
		{
			J2SESoundManagerImp[] array = new J2SESoundManagerImp[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new J2SESoundManagerImp();
			}
			return array;
		}

		public static J2SESoundManagerImp[][] InstArrayJ2SESoundManagerImp(int size1, int size2)
		{
			J2SESoundManagerImp[][] array = new J2SESoundManagerImp[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new J2SESoundManagerImp[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new J2SESoundManagerImp();
				}
			}
			return array;
		}

		public static J2SESoundManagerImp[][][] InstArrayJ2SESoundManagerImp(int size1, int size2, int size3)
		{
			J2SESoundManagerImp[][][] array = new J2SESoundManagerImp[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new J2SESoundManagerImp[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new J2SESoundManagerImp[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new J2SESoundManagerImp();
					}
				}
			}
			return array;
		}
	}
}
