namespace ca.jamdat.tetrisrevolution
{
	public class ReplayEventFactory
	{
		public virtual void destruct()
		{
		}

		public static ReplayEvent CreateReplayEvent(int replayEventType, int timeRecorded)
		{
			return new ReplayEvent(replayEventType, timeRecorded);
		}

		public static ReplayEventFactory[] InstArrayReplayEventFactory(int size)
		{
			ReplayEventFactory[] array = new ReplayEventFactory[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ReplayEventFactory();
			}
			return array;
		}

		public static ReplayEventFactory[][] InstArrayReplayEventFactory(int size1, int size2)
		{
			ReplayEventFactory[][] array = new ReplayEventFactory[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ReplayEventFactory[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ReplayEventFactory();
				}
			}
			return array;
		}

		public static ReplayEventFactory[][][] InstArrayReplayEventFactory(int size1, int size2, int size3)
		{
			ReplayEventFactory[][][] array = new ReplayEventFactory[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ReplayEventFactory[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ReplayEventFactory[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ReplayEventFactory();
					}
				}
			}
			return array;
		}
	}
}
