namespace ca.jamdat.tetrisrevolution
{
	public class SceneSlideShowController
	{
		public bool mActivated;

		public int[] mCommands;

		public bool[] mOnStack;

		public int mCurrentCommandIndex;

		public int mCommandCount;

		public SceneSlideShowController()
		{
			mCommands = new int[200];
			mOnStack = new bool[200];
			mCommands[mCommandCount++] = -3;
			mCommands[mCommandCount++] = -6;
			mCommands[mCommandCount++] = -5;
			mCommands[mCommandCount++] = -9;
			mCommands[mCommandCount++] = -7;
			mCommands[mCommandCount++] = -8;
			mCommands[mCommandCount++] = -46;
			mCommands[mCommandCount++] = -18;
			mCommands[mCommandCount++] = -21;
			mCommands[mCommandCount++] = -22;
			mCommands[mCommandCount++] = -20;
			mCommands[mCommandCount++] = -47;
			mCommands[mCommandCount++] = -19;
			mCommands[mCommandCount++] = -57;
			mCommands[mCommandCount++] = -58;
			mCommands[mCommandCount++] = -60;
			mCommands[mCommandCount++] = -49;
			mCommands[mCommandCount++] = -28;
			mCommands[mCommandCount++] = -29;
			mCommands[mCommandCount++] = -30;
			mCommands[mCommandCount++] = -31;
			mCommands[mCommandCount++] = -39;
		}

		public virtual void destruct()
		{
			mCommands = null;
			mOnStack = null;
		}

		public virtual void Activate()
		{
			mActivated = true;
			mCurrentCommandIndex = 0;
			GameApp.Get().GetCommandHandler().ExecuteSlideShowCommand(-26);
			OnKey(23, true);
		}

		public virtual void Deactivate()
		{
			mActivated = false;
			GameApp.Get().GetCommandHandler().Execute(-17);
		}

		public virtual bool OnKey(int key, bool up)
		{
			if (!mActivated)
			{
				return false;
			}
			GameApp gameApp = GameApp.Get();
			CommandHandler commandHandler = gameApp.GetCommandHandler();
			LanguageManager languageManager = gameApp.GetLanguageManager();
			Settings settings = gameApp.GetSettings();
			int languageIndex = languageManager.GetLanguageIndex(settings.GetApplicationLanguage());
			int languageCount = LanguageManager.GetLanguageCount();
			bool flag = true;
			if (up)
			{
				switch (key)
				{
				case 21:
					if (mCurrentCommandIndex > 1)
					{
						do
						{
							mCurrentCommandIndex--;
						}
						while (!mOnStack[mCurrentCommandIndex] && mCurrentCommandIndex > 0);
						if (mOnStack[mCurrentCommandIndex])
						{
							commandHandler.ExecuteSlideShowCommand(-12);
							mOnStack[mCurrentCommandIndex] = false;
						}
					}
					break;
				case 23:
					flag = false;
					while (!flag && mCurrentCommandIndex < mCommandCount)
					{
						flag = commandHandler.ExecuteSlideShowCommand(mCommands[mCurrentCommandIndex]);
						if (flag)
						{
							mOnStack[mCurrentCommandIndex] = true;
						}
						else
						{
							mOnStack[mCurrentCommandIndex] = false;
						}
						mCurrentCommandIndex++;
					}
					break;
				case 19:
					if (languageIndex < languageCount - 1)
					{
						settings.SetApplicationLanguage(languageManager.GetLanguageFromIndex(languageIndex + 1));
						commandHandler.ExecuteSlideShowCommand(-27);
					}
					break;
				case 25:
					if (languageIndex > 0)
					{
						settings.SetApplicationLanguage(languageManager.GetLanguageFromIndex(languageIndex - 1));
						commandHandler.ExecuteSlideShowCommand(-27);
					}
					break;
				default:
					flag = false;
					break;
				case 22:
					break;
				}
			}
			return flag;
		}

		public static SceneSlideShowController[] InstArraySceneSlideShowController(int size)
		{
			SceneSlideShowController[] array = new SceneSlideShowController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneSlideShowController();
			}
			return array;
		}

		public static SceneSlideShowController[][] InstArraySceneSlideShowController(int size1, int size2)
		{
			SceneSlideShowController[][] array = new SceneSlideShowController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneSlideShowController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneSlideShowController();
				}
			}
			return array;
		}

		public static SceneSlideShowController[][][] InstArraySceneSlideShowController(int size1, int size2, int size3)
		{
			SceneSlideShowController[][][] array = new SceneSlideShowController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneSlideShowController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneSlideShowController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneSlideShowController();
					}
				}
			}
			return array;
		}
	}
}
