namespace ca.jamdat.tetrisrevolution
{
	public class BootAppProcess
	{
		public const int stateInitializingLibrary = 0;

		public const int stateLoadingAccelerometerSkin = 1;

		public const int stateLoadingHourglass = 2;

		public const int stateLoadingConsoleWindow = 3;

		public const int stateHandlingFiles = 4;

		public const int stateSettingLanguage = 5;

		public const int stateSettingMoreGames15 = 6;

		public const int stateSettingMoreGames16 = 7;

		public const int stateLaunchingFirstScene = 8;

		public const int stateTerminated = 9;

		public const int stateCount = 10;

		public int[] mStateStack;

		public int mStateStackTopIdx;

		public bool mLoading;

		public BootAppProcess()
		{
			mStateStackTopIdx = -1;
			mStateStack = new int[10];
			PushState(9);
			PushState(8);
			PushState(5);
			PushState(4);
			PushState(2);
		}

		public virtual void destruct()
		{
			mStateStack = null;
		}

		public virtual bool OnTime()
		{
			GameApp gameApp = GameApp.Get();
			int num = GetState();
			switch (num)
			{
			case 2:
			{
				Hourglass hourglass = gameApp.GetHourglass();
				if (!mLoading)
				{
					hourglass.Load();
					mLoading = true;
				}
				if (hourglass.IsLoaded())
				{
					hourglass.SetVisible(true);
					PopStateAndResetLoadingFlag();
				}
				break;
			}
			case 4:
			{
				int num2 = gameApp.GetFileManager().OnLoad();
				if (num2 == 3 || num2 == 2)
				{
					num = PopStateAndResetLoadingFlag();
				}
				break;
			}
			}
			if (num == 5)
			{
				short num3 = gameApp.GetLanguageManager().QueryLanguage();
				Settings settings = gameApp.GetSettings();
				if (num3 != 2 && num3 != 1 && num3 != settings.GetApplicationLanguage())
				{
					settings.SetApplicationLanguage(num3);
				}
				num = PopStateAndResetLoadingFlag();
			}
			if (num == 8)
			{
				gameApp.GetCommandHandler().Execute(-1);
				num = PopStateAndResetLoadingFlag();
			}
			return num == 9;
		}

		public virtual int GetState()
		{
			return mStateStack[mStateStackTopIdx];
		}

		public virtual void PushState(int s)
		{
			mStateStack[++mStateStackTopIdx] = s;
		}

		public virtual int PopStateAndResetLoadingFlag()
		{
			mLoading = false;
			mStateStackTopIdx--;
			return GetState();
		}

		public static BootAppProcess[] InstArrayBootAppProcess(int size)
		{
			BootAppProcess[] array = new BootAppProcess[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new BootAppProcess();
			}
			return array;
		}

		public static BootAppProcess[][] InstArrayBootAppProcess(int size1, int size2)
		{
			BootAppProcess[][] array = new BootAppProcess[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BootAppProcess[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BootAppProcess();
				}
			}
			return array;
		}

		public static BootAppProcess[][][] InstArrayBootAppProcess(int size1, int size2, int size3)
		{
			BootAppProcess[][][] array = new BootAppProcess[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BootAppProcess[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BootAppProcess[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new BootAppProcess();
					}
				}
			}
			return array;
		}
	}
}
