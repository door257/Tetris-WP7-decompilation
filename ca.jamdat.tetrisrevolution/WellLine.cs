namespace ca.jamdat.tetrisrevolution
{
	public class WellLine
	{
		public short mBlockFlags;

		public short mFloatingBlockFlags;

		public bool mGameSpecificCompleteCondition;

		public Mino[] mBlocks;

		public WellLine()
		{
			mGameSpecificCompleteCondition = true;
			mBlocks = new Mino[10];
			for (int i = 0; i < 10; i++)
			{
				mBlocks[i] = null;
			}
		}

		public virtual void destruct()
		{
			mBlocks = null;
		}

		public virtual void Clear(TetrisGame game, sbyte newMinoType)
		{
			for (int i = 0; i < 10; i++)
			{
				bool flag = true;
				if (mFloatingBlockFlags != 0 && mBlocks[i].GetTetrimino().IsFloating())
				{
					flag = game.TryingToDestroyFloatingMino(mBlocks[i]);
				}
				if (flag)
				{
					RemoveMino(i, newMinoType);
				}
			}
		}

		public virtual void Assign(WellLine src)
		{
			mBlockFlags = src.mBlockFlags;
			mFloatingBlockFlags = src.mFloatingBlockFlags;
			for (int i = 0; i < 10; i++)
			{
				mBlocks[i] = src.mBlocks[i];
				Mino mino = mBlocks[i];
			}
		}

		public virtual void MoveLockedMino(WellLine srcLine, Mino mino, int dirX, int dirY)
		{
			int matrixPosX = mino.GetMatrixPosX();
			int dirX2 = -dirX;
			int dirY2 = -dirY;
			SetLockedMino(matrixPosX, mino);
			if (mino.GetTetrimino().IsFarthestMinoInDirection(mino.GetDefaultIdx(), dirX2, dirY2))
			{
				int column = matrixPosX;
				if (dirX != 0)
				{
					column = ((dirX > 0) ? (matrixPosX - 1) : (matrixPosX + 1));
				}
				srcLine.RemoveMino(column);
			}
		}

		public virtual bool IsThereLockedMino(int column)
		{
			return ((mBlockFlags | mFloatingBlockFlags) & (1 << column)) != 0;
		}

		public virtual bool IsMaskUsed(int mask)
		{
			if ((mBlockFlags & mask) == 0)
			{
				return (mFloatingBlockFlags & mask) != 0;
			}
			return true;
		}

		public virtual bool IsComplete()
		{
			if ((mBlockFlags | mFloatingBlockFlags) == 1023)
			{
				return IsGameSpecificCompleteCondition();
			}
			return false;
		}

		public virtual void TetriminosPosUpdated()
		{
			for (int i = 0; i < 10; i++)
			{
				if (IsThereLockedMino(i))
				{
					mBlocks[i].GetTetrimino().SetCorePosUpdated(false);
				}
			}
		}

		public virtual void SetLockedMino(int column, Mino mino)
		{
			mBlocks[column] = mino;
			if (mino.GetTetrimino().IsFloating())
			{
				mFloatingBlockFlags |= (short)(1 << column);
			}
			else
			{
				mBlockFlags |= (short)(1 << column);
			}
		}

		public virtual bool ConvertFloatingMinoToNonFloating(int column, Mino mino)
		{
			bool flag = BitField.IsBitOn(mFloatingBlockFlags, 1 << column);
			if (flag)
			{
				mFloatingBlockFlags &= (short)(~(1 << column));
				mBlockFlags |= (short)(1 << column);
			}
			return flag;
		}

		public virtual void RemoveMino(int column, sbyte newMinoType)
		{
			if (mBlocks[column] != null)
			{
				if (newMinoType != -1)
				{
					mBlocks[column].Clear(newMinoType, true);
				}
				mFloatingBlockFlags &= (short)(~(1 << column));
				mBlockFlags &= (short)(~(1 << column));
				mBlocks[column] = null;
			}
		}

		public virtual Mino GetLockedMino(int column)
		{
			return mBlocks[column];
		}

		public virtual bool HasNonFloatingLockedMinos()
		{
			return mBlockFlags != 0;
		}

		public virtual bool HasLockedMinos()
		{
			if (mBlockFlags == 0)
			{
				return mFloatingBlockFlags != 0;
			}
			return true;
		}

		public virtual bool IsGameSpecificCompleteCondition()
		{
			return mGameSpecificCompleteCondition;
		}

		public virtual void SetGameSpecificCompleteCondition(bool condition)
		{
			mGameSpecificCompleteCondition = condition;
		}

		public virtual void ShiftLineLeft()
		{
			mBlockFlags = 0;
			Mino mino = mBlocks[0];
			for (int i = 1; i < 10; i++)
			{
				mBlocks[i - 1] = mBlocks[i];
			}
			mBlocks[9] = mino;
			for (int i = 0; i < 10; i++)
			{
				if (mBlocks[i] != null)
				{
					mBlocks[i].SetMatrixPos(i, mBlocks[i].GetMatrixPosY());
					mBlockFlags |= (short)(1 << i);
				}
			}
		}

		public virtual void Clear(TetrisGame game)
		{
			Clear(game, -1);
		}

		public virtual void RemoveMino(int column)
		{
			RemoveMino(column, -1);
		}

		public static WellLine[] InstArrayWellLine(int size)
		{
			WellLine[] array = new WellLine[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new WellLine();
			}
			return array;
		}

		public static WellLine[][] InstArrayWellLine(int size1, int size2)
		{
			WellLine[][] array = new WellLine[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new WellLine[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new WellLine();
				}
			}
			return array;
		}

		public static WellLine[][][] InstArrayWellLine(int size1, int size2, int size3)
		{
			WellLine[][][] array = new WellLine[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new WellLine[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new WellLine[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new WellLine();
					}
				}
			}
			return array;
		}
	}
}
