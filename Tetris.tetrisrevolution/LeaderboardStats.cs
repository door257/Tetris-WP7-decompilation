namespace Tetris.tetrisrevolution
{
	internal class LeaderboardStats
	{
		private byte levelData;

		private byte tpmData;

		private int timeData;

		public byte Level
		{
			get
			{
				return levelData;
			}
			set
			{
				levelData = value;
			}
		}

		public byte TPM
		{
			get
			{
				return tpmData;
			}
			set
			{
				tpmData = value;
			}
		}

		public int Time
		{
			get
			{
				return timeData;
			}
			set
			{
				timeData = value;
			}
		}

		public LeaderboardStats(byte[] stats)
		{
			if (stats.Length > 0)
			{
				levelData = stats[1];
				tpmData = stats[0];
				timeData = stats[0];
			}
		}
	}
}
