namespace ca.jamdat.tetrisrevolution
{
	public class TipChooser
	{
		public const sbyte teachingTip1 = 0;

		public const sbyte teachingTip3 = 1;

		public const sbyte teachingTip4 = 2;

		public const sbyte teachingTip5 = 3;

		public const sbyte teachingTip6 = 4;

		public const sbyte teachingTip7 = 5;

		public const sbyte teachingTip8 = 6;

		public const sbyte teachingTip9 = 7;

		public const sbyte teachingTip10 = 8;

		public const sbyte teachingTipCount = 9;

		public const sbyte kGeneralTipsList = 0;

		public const sbyte kTeachingTipsList = 1;

		public const sbyte kVariantTipsList = 2;

		public const sbyte kTipsListCount = 3;

		public sbyte mCurrentList;

		public int mGeneralListTipIdx;

		public int mTeachingListTipIdx;

		public int mVariantListTipIdx;

		public int mCyclingListTipIdx;

		public TipChooser()
		{
			mCurrentList = 0;
		}

		public virtual void destruct()
		{
		}

		public virtual int GetTipsStringIdx()
		{
			GameApp gameApp = GameApp.Get();
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			GameSettings gameSettings = gameApp.GetGameSettings();
			int gameVariant = gameSettings.GetGameVariant();
			int num = -1;
			switch (gameSettings.GetPlayMode())
			{
			case 0:
			case 3:
				num = 47;
				break;
			case 1:
			case 2:
			case 5:
			case 6:
				num = 48;
				break;
			default:
				if (gameSettings.GetCurrentGameMode() != 0 && progressionExpert.GetHighestLevelDone(gameVariant) == -1)
				{
					num = 35 + gameVariant;
					break;
				}
				while (num == -1)
				{
					if (mCurrentList == 0)
					{
						num = GetGeneralTipsStringIdx();
						mCurrentList = 1;
						continue;
					}
					if (mCurrentList == 1)
					{
						num = GetTeachingTipsStringIdx();
						mCurrentList = 2;
						continue;
					}
					if (gameSettings.GetCurrentGameMode() != 0)
					{
						num = GetVariantTipsStringIdx();
					}
					mCurrentList = 0;
				}
				break;
			}
			return num;
		}

		public virtual int GetNextCyclingTipsStringIdx()
		{
			int result;
			if (mCurrentList == 0)
			{
				result = 7 + mCyclingListTipIdx;
				mCyclingListTipIdx++;
				if (mCyclingListTipIdx == 19)
				{
					mCyclingListTipIdx = 0;
					mCurrentList = 1;
				}
			}
			else if (mCurrentList == 1)
			{
				result = 26 + mCyclingListTipIdx;
				mCyclingListTipIdx++;
				if (mCyclingListTipIdx == 9)
				{
					mCyclingListTipIdx = 0;
					mCurrentList = 2;
				}
			}
			else
			{
				result = 35 + mCyclingListTipIdx;
				mCyclingListTipIdx++;
				if (mCyclingListTipIdx == 12)
				{
					mCyclingListTipIdx = 0;
					mCurrentList = 0;
				}
			}
			return result;
		}

		public virtual void Reset()
		{
			mCurrentList = 0;
			mGeneralListTipIdx = 0;
			mTeachingListTipIdx = 0;
			mVariantListTipIdx = 0;
			mCyclingListTipIdx = 0;
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
			if (fileStream.HasValidData())
			{
				mCurrentList = fileStream.ReadByte();
				mGeneralListTipIdx = fileStream.ReadLong();
				mTeachingListTipIdx = fileStream.ReadLong();
				mVariantListTipIdx = fileStream.ReadLong();
			}
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
			fileStream.WriteByte(mCurrentList);
			fileStream.WriteLong(mGeneralListTipIdx);
			fileStream.WriteLong(mTeachingListTipIdx);
			fileStream.WriteLong(mVariantListTipIdx);
			fileStream.SetValidDataFlag(true);
		}

		public virtual int GetGeneralTipsStringIdx()
		{
			int result = 7 + mGeneralListTipIdx;
			mGeneralListTipIdx++;
			if (mGeneralListTipIdx == 19)
			{
				mGeneralListTipIdx = 0;
			}
			return result;
		}

		public virtual int GetTeachingTipsStringIdx()
		{
			int result = -1;
			bool flag = false;
			FeatsExpert featsExpert = FeatsExpert.Get();
			GameApp gameApp = GameApp.Get();
			CareerStatistics careerStatistics = gameApp.GetCareerStatistics();
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			switch (mTeachingListTipIdx)
			{
			case 0:
				if (careerStatistics.GetStatistic(1) < 100)
				{
					flag = true;
				}
				break;
			case 1:
				if (careerStatistics.GetStatistic(1) * 4 * 100 < careerStatistics.GetStatistic(0) * 20)
				{
					flag = true;
				}
				break;
			case 2:
				if (careerStatistics.GetStatistic(0) < 1000)
				{
					flag = true;
				}
				break;
			case 3:
				if ((careerStatistics.GetStatistic(5) + careerStatistics.GetStatistic(6) + careerStatistics.GetStatistic(7) + careerStatistics.GetStatistic(8) + careerStatistics.GetStatistic(9) + careerStatistics.GetStatistic(10)) * 100 < careerStatistics.GetStatistic(0))
				{
					flag = true;
				}
				break;
			case 4:
				if (careerStatistics.GetStatistic(24) * 100 < careerStatistics.GetStatistic(0) * 2)
				{
					flag = true;
				}
				break;
			case 5:
				if (careerStatistics.GetStatistic(18) < 3000)
				{
					flag = true;
				}
				break;
			case 6:
				if (!featsExpert.IsAdvancedFeatUnlocked(2))
				{
					flag = true;
				}
				break;
			case 7:
				if (progressionExpert.GetGameCompletion() < 100)
				{
					flag = true;
				}
				break;
			case 8:
				if (careerStatistics.GetStatistic(4) < 35)
				{
					flag = true;
				}
				break;
			}
			if (flag)
			{
				result = 26 + mTeachingListTipIdx;
			}
			mTeachingListTipIdx++;
			if (mTeachingListTipIdx == 9)
			{
				mTeachingListTipIdx = 0;
			}
			return result;
		}

		public virtual int GetVariantTipsStringIdx()
		{
			int result = -1;
			FeatsExpert featsExpert = FeatsExpert.Get();
			if (featsExpert.IsGameVariantUnlocked(mVariantListTipIdx))
			{
				result = 35 + mVariantListTipIdx;
			}
			mVariantListTipIdx++;
			if (mVariantListTipIdx == 12)
			{
				mVariantListTipIdx = 0;
			}
			return result;
		}

		public static TipChooser[] InstArrayTipChooser(int size)
		{
			TipChooser[] array = new TipChooser[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TipChooser();
			}
			return array;
		}

		public static TipChooser[][] InstArrayTipChooser(int size1, int size2)
		{
			TipChooser[][] array = new TipChooser[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TipChooser[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TipChooser();
				}
			}
			return array;
		}

		public static TipChooser[][][] InstArrayTipChooser(int size1, int size2, int size3)
		{
			TipChooser[][][] array = new TipChooser[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TipChooser[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TipChooser[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TipChooser();
					}
				}
			}
			return array;
		}
	}
}
