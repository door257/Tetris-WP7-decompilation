namespace ca.jamdat.tetrisrevolution
{
	public class CheatContainer
	{
		public Cheat[] mCheats;

		public CheatContainer()
		{
			mCheats = new Cheat[26];
		}

		public virtual void destruct()
		{
			for (int i = 0; i < 26; i++)
			{
				mCheats[i] = null;
			}
			mCheats = null;
		}

		public static CheatContainer Get()
		{
			return GameApp.Get().GetCheatContainer();
		}

		public virtual void AddCheat(Cheat cheat)
		{
			mCheats[cheat.GetId()] = cheat;
		}

		public virtual void RemoveCheat(int id)
		{
			mCheats[id] = null;
		}

		public virtual Cheat GetCheat(int id)
		{
			return mCheats[id];
		}

		public static CheatContainer[] InstArrayCheatContainer(int size)
		{
			CheatContainer[] array = new CheatContainer[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CheatContainer();
			}
			return array;
		}

		public static CheatContainer[][] InstArrayCheatContainer(int size1, int size2)
		{
			CheatContainer[][] array = new CheatContainer[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatContainer[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatContainer();
				}
			}
			return array;
		}

		public static CheatContainer[][][] InstArrayCheatContainer(int size1, int size2, int size3)
		{
			CheatContainer[][][] array = new CheatContainer[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatContainer[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatContainer[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CheatContainer();
					}
				}
			}
			return array;
		}
	}
}
