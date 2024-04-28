using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class BioStatistics
	{
		public const sbyte bioAggressiveness = 0;

		public const sbyte bioNimbleness = 1;

		public const sbyte bioRotations = 2;

		public const sbyte bioMovements = 3;

		public const sbyte bioOrderDependentCount = 4;

		public const sbyte bioVirtuosity = 4;

		public const sbyte bioWariness = 5;

		public const sbyte bioInitiative = 6;

		public const sbyte bioRiskiness = 7;

		public const sbyte totalBioStatisticsCount = 8;

		public F32[] mBioStatisticsArrayF32;

		public int mNbOfMarathonGamesPlayed;

		public BioStatistics()
		{
			mBioStatisticsArrayF32 = F32.InstArrayF32(8);
			Reset();
		}

		public virtual void destruct()
		{
			mBioStatisticsArrayF32 = null;
		}

		public virtual void Reset()
		{
			for (int i = 0; i < 8; i++)
			{
				mBioStatisticsArrayF32[i] = F32.Zero(16);
			}
			mNbOfMarathonGamesPlayed = 0;
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
			if (fileStream.HasValidData())
			{
				mNbOfMarathonGamesPlayed = fileStream.ReadLong();
				fileStream.ReadF32Array(mBioStatisticsArrayF32, 8);
			}
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
			fileStream.WriteLong(mNbOfMarathonGamesPlayed);
			fileStream.WriteF32Array(mBioStatisticsArrayF32, 8);
			fileStream.SetValidDataFlag(true);
		}

		public virtual F32 GetStatistic(sbyte id)
		{
			return mBioStatisticsArrayF32[id];
		}

		public virtual void Update(GameStatistics gameStatistics)
		{
			mNbOfMarathonGamesPlayed++;
			bool useEMA = mNbOfMarathonGamesPlayed > 10;
			int statistic = gameStatistics.GetStatistic(28);
			F32 f = new F32(F32.FromInt(statistic, 16));
			int num = -1;
			F32 f2 = new F32(F32.Zero(16));
			F32 f3 = new F32(F32.Zero(16));
			for (int i = 0; i < 4; i++)
			{
				num = gameStatistics.GetStatistic((sbyte)(i + 22));
				f2 = F32.FromInt(num, 16);
				f3 = f2.Div(f, 16);
				UpdateMovingAverage((sbyte)i, f3, useEMA);
			}
			UpdateBioVirtuosity(gameStatistics, useEMA);
			UpdateBioWariness(gameStatistics, useEMA);
			UpdateBioInitiative(gameStatistics, useEMA);
			UpdateBioRiskiness(gameStatistics, useEMA);
		}

		public static F32 Normalize(F32 value, sbyte bioStatId)
		{
			F32 f = new F32(F32.FromInt(99, 16).Div(10));
			switch (bioStatId)
			{
			case 2:
			case 3:
			case 6:
				if (value.GreaterThan(f))
				{
					value = f;
				}
				break;
			case 0:
			case 1:
			case 4:
			case 5:
			case 7:
				value = value.Mul(100);
				break;
			}
			return value;
		}

		public virtual void UpdateMovingAverage(sbyte id, F32 currentValueF32, bool useEMA)
		{
			if (useEMA)
			{
				UpdateExponentialMovingAverage(id, currentValueF32);
			}
			else
			{
				UpdateSimpleMovingAverage(id, currentValueF32);
			}
		}

		public virtual void UpdateSimpleMovingAverage(sbyte id, F32 currentTotalF32)
		{
			F32 f = new F32(F32.FromInt(mNbOfMarathonGamesPlayed, 16));
			F32 f2 = new F32(mBioStatisticsArrayF32[id].Mul(mNbOfMarathonGamesPlayed - 1));
			mBioStatisticsArrayF32[id] = f2.Add(currentTotalF32).Div(f, 16);
		}

		public virtual void UpdateExponentialMovingAverage(sbyte id, F32 currentTotalF32)
		{
			F32 other = new F32(mBioStatisticsArrayF32[id]);
			int scalar = 2;
			F32 f = new F32(F32.One(16).Add(F32.FromInt(10, 16)));
			mBioStatisticsArrayF32[id] = currentTotalF32.Sub(other);
			mBioStatisticsArrayF32[id] = mBioStatisticsArrayF32[id].Mul(scalar).Div(f, 16);
			mBioStatisticsArrayF32[id] = mBioStatisticsArrayF32[id].Add(other);
		}

		public virtual void UpdateBioVirtuosity(GameStatistics gameStatistics, bool useEMA)
		{
			int statistic = gameStatistics.GetStatistic(0);
			F32 currentValueF = new F32(F32.Zero(16));
			if (statistic > 0)
			{
				F32 f = new F32(F32.FromInt(statistic, 16));
				int intValue = gameStatistics.GetStatistic(2) * 4 + gameStatistics.GetStatistic(15) + gameStatistics.GetStatistic(9) + gameStatistics.GetStatistic(10) * 2 + gameStatistics.GetStatistic(11) * 3;
				F32 f2 = new F32(F32.FromInt(intValue, 16));
				currentValueF = f2.Div(f, 16);
			}
			UpdateMovingAverage(4, currentValueF, useEMA);
		}

		public virtual void UpdateBioWariness(GameStatistics gameStatistics, bool useEMA)
		{
			int statistic = gameStatistics.GetStatistic(0);
			F32 currentValueF = new F32(F32.Zero(16));
			if (statistic > 0)
			{
				F32 f = new F32(F32.FromInt(statistic, 16));
				int statistic2 = gameStatistics.GetStatistic(5);
				F32 f2 = new F32(F32.FromInt(statistic2, 16));
				currentValueF = f2.Div(f, 16);
			}
			UpdateMovingAverage(5, currentValueF, useEMA);
		}

		public virtual void UpdateBioInitiative(GameStatistics gameStatistics, bool useEMA)
		{
			int statistic = gameStatistics.GetStatistic(28);
			F32 f = new F32(F32.FromInt(statistic, 16));
			int statistic2 = gameStatistics.GetStatistic(26);
			F32 f2 = new F32();
			if (statistic2 >= 32768)
			{
				statistic2 /= 1000;
				f2 = F32.FromInt(statistic2, 16);
			}
			else
			{
				f2 = F32.FromInt(statistic2, 16).Div(F32.FromInt(1000, 16), 16);
			}
			F32 currentValueF = new F32(f2.Div(f, 16));
			UpdateMovingAverage(6, currentValueF, useEMA);
		}

		public virtual void UpdateBioRiskiness(GameStatistics gameStatistics, bool useEMA)
		{
			int statistic = gameStatistics.GetStatistic(28);
			new F32(F32.FromInt(statistic, 16));
			int statistic2 = gameStatistics.GetStatistic(27);
			F32 f = new F32(Utilities.SafeDivide(statistic2, statistic, 16));
			F32 f2 = new F32(F32.FromInt(200, 16));
			F32 currentValueF = new F32(f.Div(f2, 16));
			UpdateMovingAverage(7, currentValueF, useEMA);
		}

		public static BioStatistics[] InstArrayBioStatistics(int size)
		{
			BioStatistics[] array = new BioStatistics[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new BioStatistics();
			}
			return array;
		}

		public static BioStatistics[][] InstArrayBioStatistics(int size1, int size2)
		{
			BioStatistics[][] array = new BioStatistics[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BioStatistics[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BioStatistics();
				}
			}
			return array;
		}

		public static BioStatistics[][][] InstArrayBioStatistics(int size1, int size2, int size3)
		{
			BioStatistics[][][] array = new BioStatistics[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new BioStatistics[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new BioStatistics[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new BioStatistics();
					}
				}
			}
			return array;
		}
	}
}
