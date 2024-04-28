namespace ca.jamdat.tetrisrevolution
{
	public class LanguageCheat : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			GameApp gameApp = GameApp.Get();
			if (param < LanguageManager.GetLanguageCount())
			{
				gameApp.GetSettings().SetApplicationLanguage(gameApp.GetLanguageManager().GetLanguageFromIndex(param));
				gameApp.GetCommandHandler().Execute(-27);
			}
		}

		public override void Deactivate()
		{
		}

		public static LanguageCheat[] InstArrayLanguageCheat(int size)
		{
			LanguageCheat[] array = new LanguageCheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LanguageCheat();
			}
			return array;
		}

		public static LanguageCheat[][] InstArrayLanguageCheat(int size1, int size2)
		{
			LanguageCheat[][] array = new LanguageCheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LanguageCheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LanguageCheat();
				}
			}
			return array;
		}

		public static LanguageCheat[][][] InstArrayLanguageCheat(int size1, int size2, int size3)
		{
			LanguageCheat[][][] array = new LanguageCheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LanguageCheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LanguageCheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LanguageCheat();
					}
				}
			}
			return array;
		}
	}
}
