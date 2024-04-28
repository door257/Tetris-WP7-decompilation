namespace ca.jamdat.tetrisrevolution
{
	public class KeyHandler
	{
		public const sbyte keyLeft = 0;

		public const sbyte keyRight = 1;

		public const sbyte keySoftDrop = 2;

		public const sbyte keyHardDrop = 3;

		public const sbyte keyRotateCW = 4;

		public const sbyte keyRotateCCW = 5;

		public const sbyte keyHold = 6;

		public TetrisGame mGame;

		public int mKeyHoldTimeMs;

		public int mFirstKeyPressed;

		public KeyHandler(TetrisGame game)
		{
			mGame = game;
			mKeyHoldTimeMs = 300;
			mFirstKeyPressed = 0;
		}

		public virtual void destruct()
		{
			mGame = null;
		}

		public virtual void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			int num = mFirstKeyPressed;
			if (num != 0)
			{
				mKeyHoldTimeMs -= deltaTimeMs;
				if (mKeyHoldTimeMs < 0)
				{
					OnKeyHolding(num);
					mKeyHoldTimeMs += 33;
				}
			}
		}

		public virtual bool OnKeyUp(int key)
		{
			bool flag = true;
			if (key == mFirstKeyPressed)
			{
				mFirstKeyPressed = 0;
			}
			if (key == 2 || key == 19)
			{
				return OnKeyGameAction(2, false);
			}
			return false;
		}

		public virtual bool OnKeyDown(int key)
		{
			mFirstKeyPressed = key;
			mKeyHoldTimeMs = 300;
			TetrisGame tetrisGame = mGame;
			if (!tetrisGame.CanMoveFallingTetrimino())
			{
				return false;
			}
			bool flag = true;
			switch (key)
			{
			case 7:
			case 15:
			case 18:
			case 22:
			case 26:
				return OnKeyGameAction(4, true);
			case 16:
			case 20:
			case 24:
				return OnKeyGameAction(5, true);
			case 3:
			case 21:
				return OnKeyGameAction(0, true);
			case 4:
			case 23:
				return OnKeyGameAction(1, true);
			case 1:
			case 25:
				return OnKeyGameAction(3, true);
			case 2:
			case 19:
				return OnKeyGameAction(2, true);
			case 17:
				return OnKeyGameAction(6, true);
			default:
				return false;
			}
		}

		public virtual void OnKeyHolding(int key)
		{
			TetrisGame tetrisGame = mGame;
			if (tetrisGame.CanMoveFallingTetrimino())
			{
				switch (key)
				{
				case 3:
				case 21:
					OnKeyGameAction(0, true);
					break;
				case 4:
				case 23:
					OnKeyGameAction(1, true);
					break;
				}
			}
		}

		public virtual void ResetKeyPressed()
		{
			mFirstKeyPressed = 0;
		}

		public virtual bool OnKeyGameAction(sbyte keyAction, bool press)
		{
			TetrisGame tetrisGame = mGame;
			bool result = true;
			Replay replay = GameApp.Get().GetReplay();
			if (replay.IsPlaying() || !tetrisGame.CanRecordKeyEvents())
			{
				return false;
			}
			int currentTimeIndex = replay.GetCurrentTimeIndex();
			switch (keyAction)
			{
			case 0:
				replay.AddEvent(2, currentTimeIndex);
				tetrisGame.DoPieceAction(0);
				break;
			case 1:
				replay.AddEvent(3, currentTimeIndex);
				tetrisGame.DoPieceAction(1);
				break;
			case 2:
			{
				bool flag = press;
				replay.AddEvent(flag ? 4 : 5, currentTimeIndex);
				if (tetrisGame.CanSoftDrop())
				{
					tetrisGame.SetSoftDropActive(flag);
				}
				break;
			}
			case 3:
				replay.AddEvent(6, currentTimeIndex);
				if (tetrisGame.CanHardDrop())
				{
					tetrisGame.HardDropFallingTetrimino();
				}
				break;
			case 4:
				replay.AddEvent(0, currentTimeIndex);
				tetrisGame.DoPieceAction(2);
				break;
			case 5:
				replay.AddEvent(1, currentTimeIndex);
				tetrisGame.DoPieceAction(3);
				break;
			case 6:
				replay.AddEvent(7, currentTimeIndex);
				tetrisGame.PrepareHoldTetrimino();
				break;
			default:
				result = false;
				break;
			}
			return result;
		}
	}
}
