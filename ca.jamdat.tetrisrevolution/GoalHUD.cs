using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GoalHUD : HUD
	{
		public Text mNumeratorText;

		public int mLastValue;

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			mNumeratorText = EntryPoint.GetText(mPackage, 80);
			mHudTitleText = EntryPoint.GetText(mPackage, 79);
		}

		public override void Initialize(GameController game)
		{
			FlString flString = EntryPoint.GetFlString(-2143911840, mGame.GetGoalHUDStringEntryPoint());
			mHudTitleText.SetCaption(flString);
			Update();
			base.Initialize(game);
		}

		public override void Update()
		{
			int goalHUDValue = mGame.GetGoalHUDValue();
			if (goalHUDValue != mLastValue)
			{
				mNumeratorText.SetCaption(new FlString(goalHUDValue));
				mLastValue = goalHUDValue;
			}
		}

		public static GoalHUD[] InstArrayGoalHUD(int size)
		{
			GoalHUD[] array = new GoalHUD[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GoalHUD();
			}
			return array;
		}

		public static GoalHUD[][] InstArrayGoalHUD(int size1, int size2)
		{
			GoalHUD[][] array = new GoalHUD[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GoalHUD[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GoalHUD();
				}
			}
			return array;
		}

		public static GoalHUD[][][] InstArrayGoalHUD(int size1, int size2, int size3)
		{
			GoalHUD[][][] array = new GoalHUD[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GoalHUD[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GoalHUD[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GoalHUD();
					}
				}
			}
			return array;
		}
	}
}
