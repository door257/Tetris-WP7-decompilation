namespace ca.jamdat.tetrisrevolution
{
	public class SceneType
	{
		public const int typeUndefined = 0;

		public const int typeSplash = 1;

		public const int typeMenu = 2;

		public const int typePrompt = 4;

		public const int typeGame = 16;

		public const int typeBootSeqMember = 32;

		public static SceneType[] InstArraySceneType(int size)
		{
			SceneType[] array = new SceneType[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneType();
			}
			return array;
		}

		public static SceneType[][] InstArraySceneType(int size1, int size2)
		{
			SceneType[][] array = new SceneType[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneType[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneType();
				}
			}
			return array;
		}

		public static SceneType[][][] InstArraySceneType(int size1, int size2, int size3)
		{
			SceneType[][][] array = new SceneType[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneType[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneType[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneType();
					}
				}
			}
			return array;
		}
	}
}
