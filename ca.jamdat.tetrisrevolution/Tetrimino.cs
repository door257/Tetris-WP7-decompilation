using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public abstract class Tetrimino
	{
		public const sbyte kSpecialMino = -2;

		public const sbyte kTetriminoNone = -1;

		public const sbyte kTetriminoI = 0;

		public const sbyte kTetriminoJ = 1;

		public const sbyte kTetriminoL = 2;

		public const sbyte kTetriminoO = 3;

		public const sbyte kTetriminoS = 4;

		public const sbyte kTetriminoT = 5;

		public const sbyte kTetriminoZ = 6;

		public const sbyte kTetriminoCount = 7;

		public const int kPointNone = -1;

		public const int kPointOne = 0;

		public const int kPointTwo = 1;

		public const int kPointThree = 2;

		public const int kPointFour = 3;

		public const int kPointFive = 4;

		public const int kPieceFaceN = 0;

		public const int kPieceFaceE = 1;

		public const int kPieceFaceS = 2;

		public const int kPieceFaceW = 3;

		public const int kPieceFaceCount = 4;

		public const int kNeighborWest = 1;

		public const int kNeighborSouth = 2;

		public const int kNeighborEast = 4;

		public const int kNeighborNorth = 8;

		public const int kNeighborNorthWest = 9;

		public const int kNeighborSouthWest = 3;

		public const int kNeighborNorthEast = 12;

		public const int kNeighborSouthEast = 6;

		public Well mWell;

		public int mCoreMatrixPosX;

		public int mCoreMatrixPosY;

		public MinoList mMinoList;

		public int mLastRotationPt;

		public int mFacingDir;

		public int[] mpMarkerArray;

		public static int[] mTetriminoJLSTZMarks = new int[40]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1, 0, 1, 1, 0, -2, 1, -2,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, -1, 0, -1, 1, 0, -2, -1, -2
		};

		public static int[] mTetriminoIMarks = new int[40]
		{
			0, 0, -1, 0, 2, 0, -1, 0, 2, 0,
			-1, 0, 0, 0, 0, 0, 0, -1, 0, 2,
			-1, -1, 1, -1, -2, -1, 1, 0, -2, 0,
			0, -1, 0, -1, 0, -1, 0, 1, 0, -2
		};

		public sbyte mCurrentZone;

		public bool mLocked;

		public bool mIsCorePosUpdated;

		public int mVisibleMinosCount;

		public Tetrimino mNextTetrimino;

		public Tetrimino mPrevTetrimino;

		public int mModeSpecificValue;

		public bool mModeSpecificFlag;

		public sbyte[] mSticky = new sbyte[4];

		public Tetrimino(Well well, int initialNbOfMinos)
		{
			mWell = well;
			mCoreMatrixPosX = -1;
			mCoreMatrixPosY = -1;
			mLastRotationPt = -1;
			mFacingDir = 0;
			mCurrentZone = -1;
			mVisibleMinosCount = initialNbOfMinos;
			InitializeMinos(initialNbOfMinos);
			mMinoList.GetRootMino().SetRelativePosX(0);
			mMinoList.GetRootMino().SetRelativePosY(0);
		}

		public virtual void destruct()
		{
		}

		public virtual void InitMarker()
		{
			mpMarkerArray = ((GetTetriminoType() == 0) ? mTetriminoIMarks : mTetriminoJLSTZMarks);
		}

		public virtual void Unload()
		{
			ReleaseAllMinos();
		}

		public virtual void InitializeMinos(int initialNbOfMinos)
		{
			mMinoList = new MinoList();
			Mino mino = null;
			for (int num = initialNbOfMinos - 1; num >= 0; num--)
			{
				mino = mMinoList.CreateMino(mWell, this);
				mino.SetDefaultIdx(num);
			}
		}

		public virtual void ReleaseAllMinos()
		{
			mMinoList.ReleaseAllMinos();
			mMinoList = null;
		}

		public virtual void Assign(Tetrimino sourceTetrimino)
		{
			mWell = sourceTetrimino.mWell;
			mFacingDir = sourceTetrimino.mFacingDir;
			SetCoreMatrixPos(sourceTetrimino.mCoreMatrixPosX, sourceTetrimino.mCoreMatrixPosY);
			mVisibleMinosCount = sourceTetrimino.mVisibleMinosCount;
			mLocked = sourceTetrimino.mLocked;
			mCurrentZone = sourceTetrimino.mCurrentZone;
			for (Mino mino = sourceTetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				int defaultIdx = mino.GetDefaultIdx();
				Mino mino2 = mMinoList.GetMino(defaultIdx);
				mSticky[defaultIdx] = sourceTetrimino.mSticky[defaultIdx];
				sbyte currentAspect = mino.GetCurrentAspect();
				FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(currentAspect);
				mino2.SetCurrentAspect(currentAspect, bitmapForMinoSpriteAspect);
				mino2.GetMinoViewport().SetViewport(mino.GetMinoParentViewport());
				mino2.AttachMinoComponent();
			}
		}

		public virtual Tetrimino GetNextNode()
		{
			return mNextTetrimino;
		}

		public virtual void SetNextNode(Tetrimino nextTetrimino)
		{
			mNextTetrimino = nextTetrimino;
		}

		public virtual Tetrimino GetPrevNode()
		{
			return mPrevTetrimino;
		}

		public virtual void SetPrevNode(Tetrimino prevTetrimino)
		{
			mPrevTetrimino = prevTetrimino;
		}

		public virtual bool IsGravitySensitive()
		{
			return true;
		}

		public virtual bool IsFloating()
		{
			return false;
		}

		public virtual sbyte GetTetriminoType()
		{
			return -1;
		}

		public abstract bool CanRotate(int a8, int a7, int a6);

		public virtual bool CanMove(int dirX, int dirY)
		{
			if (mMinoList == null)
			{
				return false;
			}
			Mino mino = mMinoList.GetRootMino();
			if (mino == null)
			{
				return false;
			}
			while (mino != null)
			{
				if (!mino.CanMove(dirX, dirY) && IsFarthestMinoInDirection(mino.GetDefaultIdx(), dirX, dirY))
				{
					return false;
				}
				mino = mino.GetNextNode();
			}
			return true;
		}

		public virtual MinoList GetMinoList()
		{
			return mMinoList;
		}

		public virtual Mino GetRootMino()
		{
			return mMinoList.GetRootMino();
		}

		public virtual sbyte GetRootMinoType()
		{
			return mMinoList.GetRootMino().GetTetriminoType();
		}

		public virtual Mino GetMino(int minoIdx)
		{
			return mMinoList.GetMino(minoIdx);
		}

		public virtual int GetCoreMatrixPosX()
		{
			return mCoreMatrixPosX;
		}

		public virtual int GetCoreMatrixPosY()
		{
			return mCoreMatrixPosY;
		}

		public virtual void SetCoreMatrixPos(int posX, int posY)
		{
			mCoreMatrixPosX = posX;
			mCoreMatrixPosY = posY;
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.SetMatrixPos(posX + mino.GetRelativePosX(), posY + mino.GetRelativePosY());
			}
		}

		public virtual bool HasMinoAtCoordinates(int posX, int posY)
		{
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				if (mino.GetMatrixPosX() == posX && mino.GetMatrixPosY() == posY)
				{
					return true;
				}
			}
			return false;
		}

		public virtual sbyte GetSticky(int mino, int facing)
		{
			return (sbyte)((mSticky[mino] >> facing) & 0xF);
		}

		public virtual bool IndexInBound(int x, int y)
		{
			if (x >= 0 && x < 4)
			{
				if (y >= 0)
				{
					return y < 4;
				}
				return false;
			}
			return false;
		}

		public virtual int TranslateDirectionToNeighbor(int dirX, int dirY)
		{
			if (dirX < 0 && dirY < 0)
			{
				return 9;
			}
			if (dirX > 0 && dirY < 0)
			{
				return 12;
			}
			if (dirX < 0 && dirY > 0)
			{
				return 3;
			}
			if (dirX > 0 && dirY > 0)
			{
				return 6;
			}
			if (dirX > 0)
			{
				return 4;
			}
			if (dirX < 0)
			{
				return 1;
			}
			if (dirY > 0)
			{
				return 2;
			}
			if (dirY < 0)
			{
				return 8;
			}
			return 0;
		}

		public virtual int GetLastRotationPt()
		{
			return mLastRotationPt;
		}

		public virtual int GetFacingDir()
		{
			return mFacingDir;
		}

		public virtual void SetFacingDir(int facing)
		{
			mFacingDir = facing;
		}

		public virtual void ForceDefaultPosition()
		{
			mFacingDir = 0;
		}

		public virtual bool MoveIfPossible(int dirX, int dirY)
		{
			bool result = false;
			if (CanMove(dirX, dirY))
			{
				Move(dirX, dirY);
				SetCorePosUpdated(true);
				result = true;
			}
			return result;
		}

		public virtual void Move(int dirX, int dirY, bool updateSprite)
		{
			mCoreMatrixPosX += dirX;
			mCoreMatrixPosY += dirY;
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.Move(dirX, dirY, updateSprite);
			}
		}

		public virtual bool Rotate(bool clockWise)
		{
			return clockWise ? ChangeDir((mFacingDir < 3) ? (mFacingDir + 1) : 0) : ChangeDir((mFacingDir <= 0) ? 3 : (mFacingDir - 1));
		}

		public virtual bool Lock()
		{
			bool result = true;
			mLocked = true;
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				int matrixPosY = mino.GetMatrixPosY();
				if (matrixPosY < 40)
				{
					int matrixPosX = mino.GetMatrixPosX();
					mWell.AddMino(matrixPosY, matrixPosX, mino);
					if (matrixPosY >= 20)
					{
						result = false;
					}
				}
			}
			return result;
		}

		public virtual void MoveToStartPosition()
		{
			SetCoreMatrixPos(mWell.GetTetriminoStartPositionX(), 19);
			mLocked = false;
		}

		public virtual bool IsLocked()
		{
			return mLocked;
		}

		public virtual void SetCorePosUpdated(bool isCorePosUpdated)
		{
			mIsCorePosUpdated = isCorePosUpdated;
		}

		public virtual bool CanUpdateCorePos(bool applyingGravity)
		{
			if (!mIsCorePosUpdated && !IsFloating())
			{
				if (applyingGravity)
				{
					return IsGravitySensitive();
				}
				return true;
			}
			return false;
		}

		public virtual void UpdateOrphan()
		{
			Mino mino = mMinoList.GetRootMino();
			while (mino != null && mVisibleMinosCount > 1)
			{
				Mino nextNode = mino.GetNextNode();
				if (IsOrphan(mino))
				{
					int matrixPosY = mino.GetMatrixPosY();
					if (!Well.IsClearedLineFromFlags(mWell.GetClearedLineFlags(), matrixPosY))
					{
						Tetrimino orphanTetrimino = mWell.CopyTetrimino(this);
						Split(orphanTetrimino, mino);
					}
				}
				mino = nextNode;
			}
		}

		public virtual void NoticeRemovedMino()
		{
			mVisibleMinosCount--;
		}

		public virtual int GetVisibleMinosCount()
		{
			return mVisibleMinosCount;
		}

		public virtual void UpdateSticky()
		{
			bool[][] array = RectangularArrays.ReturnRectangularBoolArray(4, 4);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					array[i][j] = false;
				}
			}
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				int num = mino.GetBaseRelativePosX() + 1;
				int num2 = mino.GetBaseRelativePosY() + 1;
				if (mino.IsMinoValid())
				{
					array[num][num2] = true;
				}
			}
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				int defaultIdx = mino.GetDefaultIdx();
				if (!mino.IsMinoValid())
				{
					mSticky[defaultIdx] = 0;
				}
				else
				{
					int num = mino.GetBaseRelativePosX() + 1;
					int num2 = mino.GetBaseRelativePosY() + 1;
					sbyte b = 0;
					if (IndexInBound(num, num2 - 1) && array[num][num2 - 1])
					{
						b = (sbyte)(b | -120);
					}
					if (IndexInBound(num + 1, num2) && array[num + 1][num2])
					{
						b = (sbyte)(b | 0x44);
					}
					if (IndexInBound(num, num2 + 1) && array[num][num2 + 1])
					{
						b = (sbyte)(b | 0x22);
					}
					if (IndexInBound(num - 1, num2) && array[num - 1][num2])
					{
						b = (sbyte)(b | 0x11);
					}
					mSticky[defaultIdx] = b;
				}
			}
		}

		public virtual bool IsFarthestMinoInDirection(int minoID, int dirX, int dirY)
		{
			int num = TranslateDirectionToNeighbor(dirX, dirY);
			return (GetSticky(minoID, mFacingDir) & num) == 0;
		}

		public virtual Mino GetFarthestMinoInDirection(int dirX, int dirY)
		{
			Mino result = null;
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				if (IsFarthestMinoInDirection(mino.GetDefaultIdx(), dirX, dirY))
				{
					result = mino;
				}
			}
			return result;
		}

		public virtual void SetModeSpecificValue(int value)
		{
			mModeSpecificValue = value;
		}

		public virtual int GetModeSpecificValue()
		{
			return mModeSpecificValue;
		}

		public virtual void SetModeSpecificFlag(bool flag)
		{
			mModeSpecificFlag = flag;
		}

		public virtual bool GetModeSpecificFlag()
		{
			return mModeSpecificFlag;
		}

		public virtual void SetZone(sbyte zone)
		{
			mCurrentZone = zone;
		}

		public virtual sbyte GetZone()
		{
			return mCurrentZone;
		}

		public virtual bool IsInZone(sbyte zone)
		{
			return mCurrentZone == zone;
		}

		public virtual void SetAllMinoAspect(sbyte minoAspect)
		{
			Mino mino = GetRootMino();
			FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(minoAspect, mino.GetCurrentAspectSize());
			while (mino != null)
			{
				mino.SetCurrentAspect(minoAspect, bitmapForMinoSpriteAspect);
				mino = mino.GetNextNode();
			}
		}

		public virtual void SetAllMinoAspectSize(sbyte minoAspectSize)
		{
			for (Mino mino = GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.SetCurrentAspectSize(minoAspectSize);
			}
		}

		public virtual void SetVisible(bool visible)
		{
			for (Mino mino = mMinoList.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.GetMinoViewport().SetVisible(visible);
			}
		}

		public virtual bool ChangeDir(int newFacing)
		{
			bool result = false;
			for (int i = 0; i < 5; i++)
			{
				int rotationMarkerX = GetRotationMarkerX(i, mFacingDir);
				int rotationMarkerY = GetRotationMarkerY(i, mFacingDir);
				int rotationMarkerX2 = GetRotationMarkerX(i, newFacing);
				int rotationMarkerY2 = GetRotationMarkerY(i, newFacing);
				int num = rotationMarkerX - rotationMarkerX2;
				int num2 = rotationMarkerY - rotationMarkerY2;
				if (CanRotate(num, num2, newFacing))
				{
					mFacingDir = newFacing;
					SetCoreMatrixPos(mCoreMatrixPosX + num, mCoreMatrixPosY + num2);
					mLastRotationPt = i;
					result = true;
					break;
				}
			}
			return result;
		}

		public virtual bool IsInWell()
		{
			if (!IsInZone(2))
			{
				return IsInZone(3);
			}
			return true;
		}

		public virtual int GetRotationMarkerX(int mark_num, int facing)
		{
			return mpMarkerArray[(facing * 5 + mark_num) * 2];
		}

		public virtual int GetRotationMarkerY(int mark_num, int facing)
		{
			return mpMarkerArray[(facing * 5 + mark_num) * 2 + 1];
		}

		public virtual void IsolateOrphan(int orphanIndex)
		{
			mVisibleMinosCount = 1;
			Mino mino = mMinoList.GetRootMino();
			while (mino != null)
			{
				Mino nextNode = mino.GetNextNode();
				if (mino.GetDefaultIdx() != orphanIndex)
				{
					mMinoList.ReleaseMino(mino);
				}
				mino = nextNode;
			}
			UpdateSticky();
			Mino mino2 = GetMino(orphanIndex);
			int matrixPosX = mino2.GetMatrixPosX();
			int matrixPosY = mino2.GetMatrixPosY();
			mWell.GetLine(matrixPosY).SetLockedMino(matrixPosX, mino2);
		}

		public virtual bool IsOrphan(Mino mino)
		{
			if (mino.IsMinoValid())
			{
				return mSticky[mino.GetDefaultIdx()] == 0;
			}
			return false;
		}

		public virtual void Split(Tetrimino orphanTetrimino, Mino orphanMino)
		{
			orphanTetrimino.IsolateOrphan(orphanMino.GetDefaultIdx());
			mMinoList.ReleaseMino(orphanMino);
			NoticeRemovedMino();
			UpdateSticky();
		}

		public virtual void Move(int dirX, int dirY)
		{
			Move(dirX, dirY, true);
		}
	}
}
