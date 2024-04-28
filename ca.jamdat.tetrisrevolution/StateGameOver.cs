namespace ca.jamdat.tetrisrevolution
{
	public class StateGameOver : GameState
	{
		public StateGameOver(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 10;
		}

		public override void OnEntry()
		{
			base.OnEntry();
			mGame.OnGameOver();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			mGame.OnTimeStateGameOver();
			if (!mGame.IsDoingAnimation())
			{
				mGame.GetGameController().ManageGameOver();
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}
	}
}
