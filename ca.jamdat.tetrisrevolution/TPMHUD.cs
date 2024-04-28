using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TPMHUD : HUD
	{
		public Text mTPMText;

		public int mPreviousTPMValue;

		public TPMHUD()
		{
			mPreviousTPMValue = -2;
		}

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mTPMText = EntryPoint.GetText(mPackage, 85);
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			Update();
		}

		public override void Reset()
		{
			mPreviousTPMValue = -2;
			Update();
		}

		public override void Update()
		{
			base.Update();
			int num = mGame.GetGameStatistics().GetCurrentTPM(mGame.GetPlayTimeMs());
			int num2 = num / 100;
			if (num2 != mPreviousTPMValue)
			{
				if (num == -1)
				{
					num = 0;
				}
				mTPMText.SetCaption(StatisticsFormatting.CreateStatisticString(num, 3));
				mPreviousTPMValue = num2;
			}
		}

		public static TPMHUD[] InstArrayTPMHUD(int size)
		{
			TPMHUD[] array = new TPMHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TPMHUD();
			}
			return array;
		}

		public static TPMHUD[][] InstArrayTPMHUD(int size1, int size2)
		{
			TPMHUD[][] array = new TPMHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TPMHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TPMHUD();
				}
			}
			return array;
		}

		public static TPMHUD[][][] InstArrayTPMHUD(int size1, int size2, int size3)
		{
			TPMHUD[][][] array = new TPMHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TPMHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TPMHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TPMHUD();
					}
				}
			}
			return array;
		}
	}
}
