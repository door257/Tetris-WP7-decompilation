namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoQueueWithBag : TetriminoQueue
	{
		public sbyte[] mNextTetriminosBag = new sbyte[7];

		public int mNextTetriminoInBagIdx;

		public TetriminoQueueWithBag(TetrisGame game)
			: base(game)
		{
		}

		public override void destruct()
		{
		}

		public override void CreateQueue(int size)
		{
			base.CreateQueue(size);
			RefillNextTetriminosBag();
			for (int i = 0; i < size; i++)
			{
				mNextTetriminosQueue[i] = GetNextTetriminoInBag();
			}
		}

		public override Tetrimino PopNextTetrimino()
		{
			Tetrimino tetrimino = null;
			if (mForceSameTetrimino != 2)
			{
				tetrimino = ((mForceSameTetrimino != 0) ? CreateTetrimino(3) : CreateTetrimino(0));
			}
			else
			{
				tetrimino = mNextTetriminosQueue[ConvertToCircularQueueIndex(mNextTetriminoIndex)];
				mNextTetriminosQueue[ConvertToCircularQueueIndex(mNextTetriminoIndex + mNextTetriminosQueueLength)] = GetNextTetriminoInBag();
				mNextTetriminoIndex++;
			}
			return tetrimino;
		}

		public virtual Tetrimino GetNextTetriminoInBag()
		{
			Tetrimino result = CreateTetrimino(mNextTetriminosBag[mNextTetriminoInBagIdx]);
			mNextTetriminoInBagIdx++;
			if (mNextTetriminoInBagIdx == 7)
			{
				RefillNextTetriminosBag();
				mNextTetriminoInBagIdx = 0;
			}
			return result;
		}

		public virtual void RefillNextTetriminosBag()
		{
			for (int i = 0; i < 7; i++)
			{
				mNextTetriminosBag[i] = (sbyte)(i % 7);
			}
			int num = 0;
			for (int i = 0; i < 1; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int num2 = num + RandomSequencePos();
					sbyte b = mNextTetriminosBag[num + j];
					mNextTetriminosBag[num + j] = mNextTetriminosBag[num2];
					mNextTetriminosBag[num2] = b;
				}
				num += 7;
			}
		}

		public virtual int RandomSequencePos()
		{
			return GameRandom.Random(0, 6);
		}

		public virtual Tetrimino CreateTetrimino(sbyte tetriminoType)
		{
			sbyte newPieceType = tetriminoType;
			return mGame.CreateTetrimino(newPieceType);
		}
	}
}
