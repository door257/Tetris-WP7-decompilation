using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameRandom
	{
		public int mCurrentGameRandomState;

		public virtual void destruct()
		{
		}

		public static int Random(int a, int b)
		{
			return Get().GenerateRandom(a, b);
		}

		public static void InitSeed(int seed)
		{
			Get().SetSeed(seed);
		}

		public static int GetCurrentRandomState()
		{
			return Get().GetGameRandomState();
		}

		public virtual int GenerateRandom(int a, int b)
		{
			int randomState = FrameworkGlobals.GetInstance().randomState;
			FrameworkGlobals.GetInstance().randomState = mCurrentGameRandomState;
			int result = FlMath.Random(a, b);
			mCurrentGameRandomState = FrameworkGlobals.GetInstance().randomState;
			FrameworkGlobals.GetInstance().randomState = randomState;
			return result;
		}

		public virtual void SetSeed(int seed)
		{
			mCurrentGameRandomState = seed;
		}

		public virtual int GetGameRandomState()
		{
			return mCurrentGameRandomState;
		}

		public static GameRandom Get()
		{
			return GameApp.Get().GetGameRandom();
		}

		public static GameRandom[] InstArrayGameRandom(int size)
		{
			GameRandom[] array = new GameRandom[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameRandom();
			}
			return array;
		}

		public static GameRandom[][] InstArrayGameRandom(int size1, int size2)
		{
			GameRandom[][] array = new GameRandom[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameRandom[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameRandom();
				}
			}
			return array;
		}

		public static GameRandom[][][] InstArrayGameRandom(int size1, int size2, int size3)
		{
			GameRandom[][][] array = new GameRandom[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameRandom[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameRandom[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameRandom();
					}
				}
			}
			return array;
		}
	}
}
