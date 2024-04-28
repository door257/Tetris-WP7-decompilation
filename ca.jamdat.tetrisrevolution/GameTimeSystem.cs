using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameTimeSystem
	{
		public TimeSystem mGameTimeSystem;

		public int mDeltaTimeMsAccumulator;

		public static void Register(TimeControlled timeControlled, bool gameAnimation)
		{
			if (gameAnimation)
			{
				Get().GetTimeSystem().Register(timeControlled);
			}
			else
			{
				timeControlled.RegisterInGlobalTime();
			}
		}

		public static void UnRegister(TimeControlled timeControlled)
		{
			Get().GetTimeSystem().UnRegister(timeControlled);
			timeControlled.UnRegisterInGlobalTime();
		}

		public static bool IsRegistered(TimeControlled timeControlled)
		{
			bool flag = false;
			return timeControlled.IsRegisteredInGlobalTime() || Get().GetTimeSystem().IsRegistered(timeControlled);
		}

		public static void Reset()
		{
			Get().GetTimeSystem().SetTotalTime(0);
			Get().mDeltaTimeMsAccumulator = 0;
		}

		public static void OnTime(GameController sender, int totalGameTimeMs, int deltaTimeMs, bool recordOnTime)
		{
			GameTimeSystem gameTimeSystem = Get();
			Replay replay = Replay.Get();
			TimeSystem timeSystem = gameTimeSystem.GetTimeSystem();
			bool flag = replay.IsRecording();
			bool flag2 = replay.IsPlaying();
			int timeAccumulator = gameTimeSystem.GetTimeAccumulator();
			if (recordOnTime)
			{
				timeAccumulator += deltaTimeMs;
				if (flag || !flag2)
				{
					timeSystem.OnTime(0, deltaTimeMs);
					sender.OnGameOnTime(0, deltaTimeMs);
					replay.AddOnTimeEvent(deltaTimeMs, totalGameTimeMs);
				}
				else
				{
					int num = 0;
					while (!replay.IsTimeQueueEmpty() && timeAccumulator - replay.PeekDeltaTime() >= 0)
					{
						num = replay.PeekDeltaTime();
						timeAccumulator -= num;
						sender.OnKeyReplay();
						timeSystem.OnTime(0, num);
						sender.OnGameOnTime(0, num);
						replay.PopDeltaTime();
					}
					if (replay.IsTimeQueueEmpty())
					{
						sender.OnReplayEnd();
					}
				}
				gameTimeSystem.SetTimeAccumulator(timeAccumulator);
			}
			else
			{
				sender.OnGameOnTime(0, deltaTimeMs);
			}
		}

		public GameTimeSystem()
		{
			mGameTimeSystem = new TimeSystem();
		}

		public virtual void destruct()
		{
		}

		public static GameTimeSystem Get()
		{
			return GameApp.Get().GetGameTimeSystem();
		}

		public virtual TimeSystem GetTimeSystem()
		{
			return mGameTimeSystem;
		}

		public virtual int GetTimeAccumulator()
		{
			return mDeltaTimeMsAccumulator;
		}

		public virtual void SetTimeAccumulator(int timeMs)
		{
			mDeltaTimeMsAccumulator = timeMs;
		}

		public static void OnTime(GameController sender, int totalGameTimeMs, int deltaTimeMs)
		{
			OnTime(sender, totalGameTimeMs, deltaTimeMs, true);
		}

		public static GameTimeSystem[] InstArrayGameTimeSystem(int size)
		{
			GameTimeSystem[] array = new GameTimeSystem[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameTimeSystem();
			}
			return array;
		}

		public static GameTimeSystem[][] InstArrayGameTimeSystem(int size1, int size2)
		{
			GameTimeSystem[][] array = new GameTimeSystem[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameTimeSystem[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameTimeSystem();
				}
			}
			return array;
		}

		public static GameTimeSystem[][][] InstArrayGameTimeSystem(int size1, int size2, int size3)
		{
			GameTimeSystem[][][] array = new GameTimeSystem[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameTimeSystem[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameTimeSystem[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameTimeSystem();
					}
				}
			}
			return array;
		}
	}
}
