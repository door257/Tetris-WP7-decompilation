namespace ca.jamdat.tetrisrevolution
{
	public class InputMapper
	{
		public const int mappingNone = 0;

		public const int mappingCheat = 1;

		public const int mappingInGame = 2;

		public const int mappingInMenu = 3;

		public const int mappingInSplash = 4;

		public int mCurrentMapping;

		public InputMapper()
		{
			mCurrentMapping = 0;
			ChangeMapping(3);
		}

		public virtual void destruct()
		{
		}

		public virtual int ChangeMapping(int newMapping)
		{
			int num = mCurrentMapping;
			mCurrentMapping = newMapping;
			if (newMapping != num)
			{
				ResetMapping();
				switch (newMapping)
				{
				case 3:
					MapInMenu();
					break;
				case 2:
					MapInGame();
					break;
				case 4:
					MapInSplash();
					break;
				case 1:
					MapInCheats();
					break;
				}
			}
			return num;
		}

		public virtual int GetMapping()
		{
			return mCurrentMapping;
		}

		public virtual void ResetMapping()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.MapKey(14, 6);
			gameApp.MapKey(13, 5);
		}

		public virtual void MapInMenu()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.MapKey(7, 5);
		}

		public virtual void MapInGame()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.MapKey(19, 19);
			gameApp.MapKey(21, 21);
			gameApp.MapKey(22, 22);
			gameApp.MapKey(23, 23);
			gameApp.MapKey(25, 25);
			gameApp.MapKey(7, 0);
		}

		public virtual void MapInCheats()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.MapKey(17, 17);
			gameApp.MapKey(18, 18);
			gameApp.MapKey(19, 19);
			gameApp.MapKey(20, 20);
			gameApp.MapKey(21, 21);
			gameApp.MapKey(22, 22);
			gameApp.MapKey(23, 23);
			gameApp.MapKey(24, 24);
			gameApp.MapKey(25, 25);
			gameApp.MapKey(26, 26);
		}

		public virtual void MapInSplash()
		{
			GameApp gameApp = GameApp.Get();
			gameApp.MapKey(13, 0);
			gameApp.MapKey(14, 0);
		}

		public static InputMapper[] InstArrayInputMapper(int size)
		{
			InputMapper[] array = new InputMapper[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new InputMapper();
			}
			return array;
		}

		public static InputMapper[][] InstArrayInputMapper(int size1, int size2)
		{
			InputMapper[][] array = new InputMapper[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new InputMapper[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new InputMapper();
				}
			}
			return array;
		}

		public static InputMapper[][][] InstArrayInputMapper(int size1, int size2, int size3)
		{
			InputMapper[][][] array = new InputMapper[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new InputMapper[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new InputMapper[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new InputMapper();
					}
				}
			}
			return array;
		}
	}
}
