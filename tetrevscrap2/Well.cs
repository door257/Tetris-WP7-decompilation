namespace ca.jamdat.tetrisrevolution
{
	public class Well
	{
		public const int kAddLines = 0;

		public const int kApplyVerticalGravity = 1;

		public const int kApplyLateralGravity = 2;

		public const int kMoveMatrixEast = 3;

		public const int kCollapseLine = 4;

		public WellLine[] mWellLines;

		public int mClearedLineFlags;

		public int mWellTop;

		public int mScoreDisplayRow;

		public TetriminoList mSpecialMinoList;

		public TetriminoList mTetriminoList;

		public MinoList mMinosToDestroyList;

		public TetrisGame mGame;

		public Well(TetrisGame game)
		{
			mWellTop = 40;
			mScoreDisplayRow = 40;
			mGame = game;
			mSpecialMinoList = new TetriminoList();
			mTetriminoList = new TetriminoList();
			mMinosToDestroyList = new MinoList();
			mWellLines = new WellLine[40];
			for (int i = 0; i < 40; i++)
			{
				mWellLines[i] = new WellLine();
			}
		}

		public virtual void destruct()
		{
			for (int i = 0; i < 40; i++)
			{
				mWellLines[i] = null;
			}
			mWellLines = null;
			mSpecialMinoList.ReleaseAllTetriminos();
			mSpecialMinoList = null;
			mTetriminoList.ReleaseAllTetriminos();
			mTetriminoList = null;
			mMinosToDestroyList.ReleaseAllMinos();
			mMinosToDestroyList = null;
		}

		public virtual void Unload()
		{
			mSpecialMinoList.UnloadAllTetriminos();
			mTetriminoList.UnloadAllTetriminos();
			mMinosToDestroyList.UnloadAllMinos();
		}

		public virtual void CollapseLineUntil(int lineIndex)
		{
			for (int i = mWellTop; i <= lineIndex; i++)
			{
				if (IsClearedLineFromFlags(mClearedLineFlags, i))
				{
					CollapseLine(i);
					mClearedLineFlags = ClearLineAtRow(i, mClearedLineFlags);
				}
			}
			mGame.SetGravityUpdateNeeded(true);
		}

		public virtual bool IsEmpty()
		{
			bool flag = true;
			int num = 39;
			while (flag && num >= 0)
			{
				flag = ((!mGame.NeedToCheckFloatingMinoForBravo()) ? (!mWellLines[num].HasNonFloatingLockedMinos()) : (!mWellLines[num].HasLockedMinos()));
				num--;
			}
			return flag;
		}

		public virtual bool IsThereLockedMino(int column, int row)
		{
			return mWellLines[row].IsThereLockedMino(column);
		}

		public virtual bool IsMaskUsed(int pieceMask, int pieceCol, int row)
		{
			return mWellLines[row].IsMaskUsed(pieceMask << pieceCol);
		}

		public virtual TetriminoList GetTetriminoList()
		{
			return mTetriminoList;
		}

		public virtual TetriminoList GetSpecialMinoList()
		{
			return mSpecialMinoList;
		}

		public virtual Tetrimino CopyTetrimino(Tetrimino source)
		{
			Tetrimino tetrimino = CreateTetrimino(source.GetTetriminoType());
			tetrimino.Assign(source);
			return tetrimino;
		}

		public virtual Tetrimino CreateTetrimino(sbyte newPieceType)
		{
			TetriminoList tetriminoList = mTetriminoList;
			if (newPieceType == -2)
			{
				tetriminoList = mSpecialMinoList;
			}
			return tetriminoList.CreateTetrimino(newPieceType, this);
		}

		public virtual void ReleaseTetrimino(Tetrimino tetrimino)
		{
			TetriminoList tetriminoList = mTetriminoList;
			if (tetrimino != null && tetrimino.GetTetriminoType() == -2)
			{
				tetriminoList = mSpecialMinoList;
			}
			tetriminoList.ReleaseTetrimino(tetrimino);
		}

		public virtual void TetriminoUpdated(Tetrimino updatedtetrimino)
		{
			if (mGame.IsCheckForCollisionNeeded())
			{
				updatedtetrimino.UpdateOrphan();
			}
		}

		public virtual int GetClearedLineFlags()
		{
			return mClearedLineFlags;
		}

		public virtual int ComputeCompletedLineFlag()
		{
			int num = 0;
			for (int i = mWellTop; i < 40; i++)
			{
				if (mWellLines[i].IsComplete())
				{
					int num2 = i - 16;
					if (num2 >= 0)
					{
						num |= 1 << num2;
					}
				}
			}
			return num;
		}

		public virtual void UpdateClearedLineFlag()
		{
			if (mGame.IsLineClearUsed())
			{
				mClearedLineFlags = ComputeCompletedLineFlag();
			}
			else
			{
				mClearedLineFlags = 0;
			}
		}

		public static int GetClearedLineCountFromFlags(int clearedLineFlags, int firstRow, int lastRow)
		{
			firstRow -= 16;
			lastRow -= 16;
			int num = 0;
			for (int i = firstRow; i < lastRow; i++)
			{
				if ((clearedLineFlags & (1 << i)) != 0)
				{
					num++;
				}
			}
			return num;
		}

		public static bool IsClearedLineFromFlags(int clearedLineFlags, int row)
		{
			if (row <= 16)
			{
				return false;
			}
			int num = row - 16;
			return (clearedLineFlags & (1 << num)) != 0;
		}

		public static int GetFirstClearedLineIndexFromFlags(int clearLineFlags, int startSearchLineIndex)
		{
			for (int i = startSearchLineIndex; i < 40; i++)
			{
				if (IsClearedLineFromFlags(clearLineFlags, i))
				{
					return i;
				}
			}
			return -1;
		}

		public static int GetConsecutiveClearedLineCountFromFlag(int clearLineFlags, int firstClearedLineIndex)
		{
			int num = 0;
			for (int i = firstClearedLineIndex; i < 40 && IsClearedLineFromFlags(clearLineFlags, i); i++)
			{
				num++;
			}
			return num;
		}

		public virtual void ShiftMatrix(int dirX)
		{
			int num = -dirX;
			if (num < 0)
			{
				num += 10;
			}
			for (int i = 0; i < num; i++)
			{
				for (int num2 = 39; num2 >= mWellTop; num2--)
				{
					mWellLines[num2].ShiftLineLeft();
				}
			}
		}

		public virtual WellLine GetLine(int row)
		{
			return mWellLines[row];
		}

		public virtual int ComputeLineAdjacency(bool needToMarkLines)
		{
			int clearedLineCount = mGame.GetClearedLineCount();
			int num = 39;
			int num2 = GetClearedLineFlags();
			bool flag = false;
			int num3 = 8388608;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			do
			{
				if ((num2 & num3) != 0)
				{
					num4++;
					num6++;
				}
				else if (num4 > 0)
				{
					if (num5 < num4)
					{
						num5 = num4;
					}
					num4 = 0;
					if (num6 == clearedLineCount)
					{
						break;
					}
				}
				num2 <<= 1;
				num7++;
				num--;
			}
			while (num7 != 20);
			return num5;
		}

		public virtual void ClearLines()
		{
			mScoreDisplayRow = 40;
			int scoreDisplayRow = 0;
			for (int i = mWellTop; i < 40; i++)
			{
				if (mWellLines[i].IsComplete())
				{
					ClearLine(i);
					scoreDisplayRow = i;
				}
			}
			SetScoreDisplayRow(scoreDisplayRow);
			mGame.OnLinesCleared();
		}

		public virtual void CollapseLines()
		{
			for (int i = mWellTop; i < 40; i++)
			{
				if (IsClearedLineFromFlags(mClearedLineFlags, i))
				{
					CollapseLine(i);
				}
			}
			mGame.SetClearedLineCount(0);
			mGame.SetGravityUpdateNeeded(true);
		}

		public virtual void Clear()
		{
			for (int i = 0; i < 40; i++)
			{
				mWellLines[i].Clear(mGame);
			}
			mWellTop = 40;
		}

		public virtual void DecrementWellTop()
		{
			mWellTop--;
		}

		public virtual int GetWellTop()
		{
			return mWellTop;
		}

		public virtual int FindWellTop()
		{
			for (int i = 20; i < 40; i++)
			{
				if (mWellLines[i].HasLockedMinos())
				{
					return i;
				}
			}
			return 40;
		}

		public virtual void SetScoreDisplayRow(int row)
		{
			mScoreDisplayRow = row;
		}

		public virtual int GetScoreDisplayRow()
		{
			return mScoreDisplayRow;
		}

		public virtual void AddMino(int row, int column, Mino newMino)
		{
			mWellLines[row].SetLockedMino(column, newMino);
			NoticeAddedMino(column, row);
		}

		public virtual void RemoveMino(int row, int column, sbyte newMinoType)
		{
			mWellLines[row].RemoveMino(column, newMinoType);
		}

		public virtual void OnLineAdded()
		{
			DecrementWellTop();
			UpdateTetriminoPos(0);
		}

		public virtual bool TryToAddLine()
		{
			if (mWellLines[0].HasNonFloatingLockedMinos())
			{
				return true;
			}
			for (int i = mWellTop - 1; i < 39; i++)
			{
				mWellLines[i].Assign(mWellLines[i + 1]);
			}
			return false;
		}

		public virtual int GetLeftLimit()
		{
			return mGame.GetLeftWellLimit();
		}

		public virtual int GetRightLimit()
		{
			return mGame.GetRightWellLimit();
		}

		public virtual int GetBottomLimit()
		{
			return mGame.GetBottomWellLimit();
		}

		public virtual int GetTetriminoStartPositionX()
		{
			return mGame.GetTetriminoStartPositionX();
		}

		public virtual MinoList GetMinoToDestroyList()
		{
			return mMinosToDestroyList;
		}

		public virtual void CleanUpLists()
		{
			mMinosToDestroyList.ReleaseAllMinos();
			CleanUpTetriminoList(mSpecialMinoList);
			CleanUpTetriminoList(mTetriminoList);
		}

		public virtual void CleanUpTetriminoList(TetriminoList tetriminoList)
		{
			Tetrimino tetrimino = tetriminoList.GetRootTetrimino();
			while (tetrimino != null)
			{
				Tetrimino nextNode = tetrimino.GetNextNode();
				if (tetrimino.GetRootMino() == null)
				{
					mGame.ReleaseTetrimino(tetrimino);
				}
				tetrimino = nextNode;
			}
		}

		public virtual void MoveMinoToDestroyList(Mino minoToDestroy)
		{
			minoToDestroy.GetTetrimino().GetMinoList().RemoveMinoFromList(minoToDestroy);
			mMinosToDestroyList.AddMinoToList(minoToDestroy);
		}

		public virtual void MoveLockedTetrimino(Tetrimino tetrimino, int dirX, int dirY)
		{
			bool flag = dirY != 0;
			Mino mino = tetrimino.GetRootMino();
			while (mino != null)
			{
				Mino nextNode = mino.GetNextNode();
				int matrixPosY = mino.GetMatrixPosY();
				if (flag)
				{
					if (matrixPosY < 40)
					{
						mWellLines[matrixPosY].MoveLockedMino(mWellLines[matrixPosY - dirY], mino, dirX, dirY);
					}
					else
					{
						if (tetrimino.IsFarthestMinoInDirection(mino.GetDefaultIdx(), 0, -1))
						{
							RemoveMino(39, mino.GetMatrixPosX());
						}
						mino.Clear(9, false);
						mino.GetMinoViewport().SetVisible(false);
					}
				}
				else
				{
					mWellLines[matrixPosY].MoveLockedMino(mWellLines[matrixPosY], mino, dirX, dirY);
				}
				mino = nextNode;
			}
		}

		public virtual void GravityOver()
		{
			mWellTop = FindWellTop();
			mGame.OnGravityOver();
			if (mGame.IsLineClearActive())
			{
				int clearedLineFlags = ComputeCompletedLineFlag();
				int clearedLineCountFromFlags = GetClearedLineCountFromFlags(clearedLineFlags);
				if (clearedLineCountFromFlags > 0)
				{
					mClearedLineFlags = clearedLineFlags;
					mGame.SetClearedLineCount(clearedLineCountFromFlags);
					mGame.SetNextGameState(6);
				}
			}
		}

		public virtual bool UpdateTetriminoPos(int updateType, int startRow)
		{
			bool flag = false;
			bool flag2 = mGame.IsCheckForCollisionNeeded();
			for (int num = startRow; num >= mWellTop; num--)
			{
				WellLine wellLine = mWellLines[num];
				bool flag3 = true;
				while (flag3 && wellLine.HasNonFloatingLockedMinos())
				{
					flag3 = false;
					for (int i = 0; i < 10; i++)
					{
						Mino lockedMino = wellLine.GetLockedMino(i);
						if (lockedMino == null)
						{
							continue;
						}
						Tetrimino tetrimino = lockedMino.GetTetrimino();
						if (!tetrimino.CanUpdateCorePos(updateType == 1))
						{
							continue;
						}
						if (updateType == 4 && !flag2)
						{
							for (Mino mino = tetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
							{
								if (mino.GetMatrixPosY() <= startRow)
								{
									mino.Move(0, 1);
								}
							}
							flag = true;
							tetrimino.SetCorePosUpdated(true);
							continue;
						}
						switch (updateType)
						{
						case 1:
						case 2:
						case 4:
						{
							int gravityDirection = mGame.GetGravityDirection(tetrimino, updateType);
							int dirX = 0;
							int dirY = 0;
							if (gravityDirection == 1)
							{
								dirX = 1;
							}
							if (gravityDirection == 3)
							{
								dirX = -1;
							}
							if (gravityDirection == 2)
							{
								dirY = 1;
							}
							if (tetrimino.MoveIfPossible(dirX, dirY))
							{
								MoveLockedTetrimino(tetrimino, dirX, dirY);
								flag = true;
								flag3 = true;
							}
							break;
						}
						case 0:
							mGame.ShowShadowTrailUnderFallingTetrimino(false);
							tetrimino.Move(0, -1);
							mGame.ShowShadowTrailUnderFallingTetrimino(true);
							tetrimino.SetCorePosUpdated(true);
							flag = true;
							break;
						case 3:
							if (tetrimino.IsLocked())
							{
								MoveLockedTetrimino(tetrimino, 1, 0);
								flag = true;
							}
							break;
						}
					}
					if (updateType == 4 && !flag2)
					{
						mWellLines[num + 1].Assign(mWellLines[num]);
					}
				}
			}
			if (flag)
			{
				for (int num2 = 39; num2 > 16; num2--)
				{
					mWellLines[num2].TetriminosPosUpdated();
				}
			}
			return flag;
		}

		public virtual void NoticeAddedMino(int column, int row)
		{
			if (row < mWellTop)
			{
				mWellTop = row;
			}
		}

		public virtual int ClearLineAtRow(int row, int clearLineFlags)
		{
			if (row <= 16)
			{
				return clearLineFlags;
			}
			int num = row - 16;
			if (IsClearedLineFromFlags(clearLineFlags, row))
			{
				return clearLineFlags ^= 1 << num;
			}
			return clearLineFlags;
		}

		public virtual void ClearLine(int row)
		{
			sbyte newMinoType = 11;
			mWellLines[row].Clear(mGame, newMinoType);
		}

		public virtual void CollapseLine(int collapsedRow)
		{
			UpdateTetriminoPos(4, collapsedRow - 1);
			if (mGame.IsCheckForCollisionNeeded())
			{
				mWellTop = FindWellTop();
				return;
			}
			mWellLines[mWellTop].Clear(mGame);
			mWellTop++;
		}

		public static int GetClearedLineCountFromFlags(int clearedLineFlags)
		{
			return GetClearedLineCountFromFlags(clearedLineFlags, 16);
		}

		public static int GetClearedLineCountFromFlags(int clearedLineFlags, int firstRow)
		{
			return GetClearedLineCountFromFlags(clearedLineFlags, firstRow, 40);
		}

		public static int GetFirstClearedLineIndexFromFlags(int clearLineFlags)
		{
			return GetFirstClearedLineIndexFromFlags(clearLineFlags, 0);
		}

		public virtual void RemoveMino(int row, int column)
		{
			RemoveMino(row, column, -1);
		}

		public virtual bool UpdateTetriminoPos(int updateType)
		{
			return UpdateTetriminoPos(updateType, 39);
		}
	}
}
