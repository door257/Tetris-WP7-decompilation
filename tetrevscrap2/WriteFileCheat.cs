namespace ca.jamdat.tetrisrevolution
{
	public class WriteFileCheat : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			GameApp gameApp = GameApp.Get();
			gameApp.GetFileManager().GetOutputSegmentStream(0).ForceModifiedFlag();
			Timer timer = new Timer();
			timer.Start();
			gameApp.SaveGame();
		}

		public override void Deactivate()
		{
		}

		public static WriteFileCheat[] InstArrayWriteFileCheat(int size)
		{
			WriteFileCheat[] array = new WriteFileCheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new WriteFileCheat();
			}
			return array;
		}

		public static WriteFileCheat[][] InstArrayWriteFileCheat(int size1, int size2)
		{
			WriteFileCheat[][] array = new WriteFileCheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new WriteFileCheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new WriteFileCheat();
				}
			}
			return array;
		}

		public static WriteFileCheat[][][] InstArrayWriteFileCheat(int size1, int size2, int size3)
		{
			WriteFileCheat[][][] array = new WriteFileCheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new WriteFileCheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new WriteFileCheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new WriteFileCheat();
					}
				}
			}
			return array;
		}
	}
}
