namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoQueue
	{
		public const int kTetriminoI = 0;

		public const int kTetriminoO = 1;

		public const int kTetriminoNormal = 2;

		public TetrisGame mGame;

		public Tetrimino[] mNextTetriminosQueue;

		public int mNextTetriminosQueueLength;

		public int mNextTetriminoIndex;

		public int mForceSameTetrimino;

		public TetriminoQueue(TetrisGame game)
		{
			mGame = game;
			mForceSameTetrimino = 2;
		}

		public virtual void destruct()
		{
			mNextTetriminosQueue = null;
		}

		public virtual void CreateQueue(int size)
		{
			mNextTetriminosQueueLength = size;
			mNextTetriminosQueue = new Tetrimino[size];
			for (int i = 0; i < mNextTetriminosQueueLength; i++)
			{
				mNextTetriminosQueue[i] = null;
			}
		}

		public virtual Tetrimino PopNextTetrimino()
		{
			return null;
		}

		public virtual Tetrimino GetTetriminoAt(int index)
		{
			return mNextTetriminosQueue[ConvertToCircularQueueIndex(mNextTetriminoIndex + index)];
		}

		public virtual void ForceNextTetriminoI()
		{
			mForceSameTetrimino = 0;
		}

		public virtual void ForceNextTetriminoO()
		{
			mForceSameTetrimino = 1;
		}

		public virtual int ConvertToCircularQueueIndex(int index)
		{
			return index % mNextTetriminosQueueLength;
		}
	}
}
