using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsStatsPopup : RealisationsScrollerPopup
	{
		public RealisationsStatsPopup(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2883672);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			BioStatistics bioStatistics = GameApp.Get().GetBioStatistics();
			Package package = mContentMetaPackage.GetPackage();
			Text text = null;
			FlString flString = null;
			int num = 0;
			for (int i = 0; i <= 13; i++)
			{
				text = EntryPoint.GetText(package, 8 + i);
				num = careerStatistics.GetStatistic((sbyte)i);
				flString = StatisticsFormatting.CreateStatisticString(num, StatisticsFormatting.GetCareerStatisticType((sbyte)i));
				text.SetCaption(flString);
			}
			for (int j = 0; j < 8; j++)
			{
				text = EntryPoint.GetText(package, 22 + j);
				F32 value = new F32(bioStatistics.GetStatistic((sbyte)j));
				value = BioStatistics.Normalize(value, (sbyte)j);
				int statisticValue = StatisticsFormatting.ConvertF32Toint(value, 16);
				sbyte bioStatisticType = StatisticsFormatting.GetBioStatisticType((sbyte)j);
				FlString caption = StatisticsFormatting.CreateStatisticString(statisticValue, bioStatisticType);
				text.SetCaption(caption);
			}
			Setint(30, 18);
			Setint(31, 14);
			Setint(32, 17);
			Setint(33, 16);
			Setint(34, 21);
			Setint(35, 22);
			Setint(36, 24);
			Setint(37, 25);
			text = EntryPoint.GetText(package, 38);
			num = careerStatistics.GetStatistic(26) + careerStatistics.GetStatistic(19);
			flString = StatisticsFormatting.CreateStatisticString(num, 0);
			text.SetCaption(flString);
		}

		public override void Initialize()
		{
			base.Initialize();
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(1, 4);
		}

		public virtual void Setint(int textEntryPoint, sbyte statId)
		{
			CareerStatistics careerStatistics = GameApp.Get().GetCareerStatistics();
			Text text = EntryPoint.GetText(mContentMetaPackage.GetPackage(), textEntryPoint);
			int statistic = careerStatistics.GetStatistic(statId);
			sbyte careerStatisticType = StatisticsFormatting.GetCareerStatisticType(statId);
			FlString caption = StatisticsFormatting.CreateStatisticString(statistic, careerStatisticType);
			text.SetCaption(caption);
		}
	}
}
