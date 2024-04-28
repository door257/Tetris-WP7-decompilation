namespace ca.jamdat.tetrisrevolution
{
	public class StateWaitingForInitialize : GameState
	{
		public bool mCanGotoNextGameState;

		public StateWaitingForInitialize(TetrisGame game)
			: base(game)
		{
			mCanGotoNextGameState = true;
		}

		public override void destruct()
		{
		}

		public override sbyte GetID()
		{
			return 0;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mCanGotoNextGameState)
			{
				SetNextGameState(1);
				mCanGotoNextGameState = false;
			}
			base.OnTime(totalTimeMs, deltaTimeMs);
		}

		public override void OnExit()
		{
			mCanGotoNextGameState = true;
		}
	}
}
