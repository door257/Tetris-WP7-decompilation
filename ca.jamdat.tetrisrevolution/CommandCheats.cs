namespace ca.jamdat.tetrisrevolution
{
	public class CommandCheats : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			GameApp gameApp = GameApp.Get();
			gameApp.GetCommandHandler().Execute(param);
		}

		public override void Deactivate()
		{
		}

		public static CommandCheats[] InstArrayCommandCheats(int size)
		{
			CommandCheats[] array = new CommandCheats[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CommandCheats();
			}
			return array;
		}

		public static CommandCheats[][] InstArrayCommandCheats(int size1, int size2)
		{
			CommandCheats[][] array = new CommandCheats[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CommandCheats[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CommandCheats();
				}
			}
			return array;
		}

		public static CommandCheats[][][] InstArrayCommandCheats(int size1, int size2, int size3)
		{
			CommandCheats[][][] array = new CommandCheats[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CommandCheats[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CommandCheats[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CommandCheats();
					}
				}
			}
			return array;
		}
	}
}
