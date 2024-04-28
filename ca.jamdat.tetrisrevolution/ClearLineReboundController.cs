using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ClearLineReboundController : TimeControlled
	{
		public const sbyte minoSolid = 0;

		public const sbyte minoMoveable = 1;

		public const sbyte minoPushable = 1;

		public Well mWell;

		public int mClearLineIndex;

		public F32 mMinoReboundCurrentSpeed;

		public F32 mMinoReboundGravity;

		public int mNbLineToUpdate;

		public int mPosOffsetAccumulator;

		public int mLineCross;

		public int mLineDropTarget;

		public bool mMinosHaveMoved;

		public int[] mWellMinoMovable;

		public int[] mWellMinoPushable;

		public ClearLineReboundController(Well well)
		{
			mWell = well;
			mMinoReboundGravity = new F32(484, 16);
			mMinoReboundCurrentSpeed = new F32();
			mWellMinoMovable = new int[10];
			mWellMinoPushable = new int[10];
			Reset();
		}

		public override void destruct()
		{
			mWellMinoMovable = null;
			mWellMinoPushable = null;
		}

		public override void OnTime(int a6, int deltaTime)
		{
			if (Update(deltaTime))
			{
				return;
			}
			if (mNbLineToUpdate > 0)
			{
				mWell.CollapseLineUntil(mClearLineIndex);
			}
			if (IsThereLineToAnimate())
			{
				Init();
				if (mNbLineToUpdate <= 0 || !IsThereMinoToMove())
				{
					Terminate();
				}
			}
			else
			{
				Terminate();
			}
		}

		public virtual void SetWell(Well well)
		{
			mWell = well;
		}

		public virtual void Init()
		{
			Well well = mWell;
			int wellTop = well.GetWellTop();
			int clearedLineFlags = well.GetClearedLineFlags();
			int firstClearedLineIndexFromFlags = Well.GetFirstClearedLineIndexFromFlags(clearedLineFlags, wellTop);
			int consecutiveClearedLineCountFromFlag = Well.GetConsecutiveClearedLineCountFromFlag(clearedLineFlags, firstClearedLineIndexFromFlags);
			mNbLineToUpdate = firstClearedLineIndexFromFlags - wellTop;
			mLineDropTarget = consecutiveClearedLineCountFromFlag;
			mPosOffsetAccumulator = 0;
			mLineCross = 0;
			mClearLineIndex = firstClearedLineIndexFromFlags + consecutiveClearedLineCountFromFlag - 1;
			ResetMinoFlags();
			GenerateMovableFlag(mWell, mClearLineIndex);
			GeneratePushableFlag(mWell, firstClearedLineIndexFromFlags);
		}

		public virtual bool Update(int deltaTimeMs)
		{
			Well well = mWell;
			bool result = false;
			if (mNbLineToUpdate <= 0)
			{
				return result;
			}
			if (deltaTimeMs > 300)
			{
				deltaTimeMs = 300;
			}
			short minoOffset = GetMinoOffset(deltaTimeMs);
			UpdateCurrentSpeed(deltaTimeMs);
			mPosOffsetAccumulator += minoOffset;
			short num = minoOffset;
			if (mPosOffsetAccumulator > mLineCross * 31)
			{
				if (mLineCross == 0)
				{
					InitMoveDownFlag();
				}
				int num2 = mPosOffsetAccumulator - minoOffset;
				short num3 = (short)(mLineCross * 31 - num2);
				UpdateMinoPos(num3);
				mLineCross++;
				GenerateCanMoveDownFlag(well, mClearLineIndex, mLineCross);
				num = (short)(mPosOffsetAccumulator - num3);
				while (num > 31 && mLineCross < mLineDropTarget)
				{
					num = (short)(num - 31);
					UpdateMinoPos(31);
					mLineCross++;
					GenerateCanMoveDownFlag(well, mClearLineIndex, mLineCross);
				}
				if (mLineCross == mLineDropTarget)
				{
					return false;
				}
			}
			UpdateMinoPos(num);
			return true;
		}

		public virtual void Terminate()
		{
			AnimationController animator = GameApp.Get().GetAnimator();
			if (mMinosHaveMoved)
			{
				animator.StartGameAnimation(23);
			}
			Reset();
			animator.Stop(22);
		}

		public virtual void Reset()
		{
			mClearLineIndex = 0;
			mNbLineToUpdate = 0;
			mPosOffsetAccumulator = 0;
			mLineCross = 0;
			mLineDropTarget = 0;
			mMinosHaveMoved = false;
			mMinoReboundCurrentSpeed = new F32(-32264, 16);
			ResetMinoFlags();
		}

		public virtual bool IsThereLineToAnimate()
		{
			Well well = mWell;
			return Well.GetClearedLineCountFromFlags(well.GetClearedLineFlags()) > 0;
		}

		public virtual short GetMinoOffset(int deltaTimeMs)
		{
			return (short)mMinoReboundCurrentSpeed.Mul(deltaTimeMs).Add(mMinoReboundGravity.Mul(deltaTimeMs * deltaTimeMs).DivPower2(1)).Round(16)
				.ToInt(16);
		}

		public virtual void UpdateCurrentSpeed(int deltaTimeMs)
		{
			mMinoReboundCurrentSpeed = mMinoReboundCurrentSpeed.Add(mMinoReboundGravity.Mul(deltaTimeMs));
		}

		public virtual void OffsetMinoSprite(Mino mino, short posOffsetY)
		{
			Viewport minoViewport = mino.GetMinoViewport();
			short top = (short)(minoViewport.GetRectTop() + posOffsetY);
			short rectLeft = minoViewport.GetRectLeft();
			minoViewport.SetTopLeft(rectLeft, top);
		}

		public virtual void SetWellUpdateFlag(int[] arrayToSet, sbyte flag, int row, int column)
		{
			if (row > 16)
			{
				int num = row - 16;
				if (flag == 1)
				{
					arrayToSet[column] |= flag << num;
				}
				else if (GetWellUpdateFlag(arrayToSet, row, column) != 0)
				{
					arrayToSet[column] ^= 1 << num;
				}
			}
		}

		public virtual bool IsMinoMovableFromMinoFlag(int row, int column)
		{
			if (row <= 16)
			{
				return true;
			}
			return GetWellUpdateFlag(mWellMinoMovable, row, column) == 1;
		}

		public virtual sbyte GetWellUpdateFlag(int[] arrayToGet, int row, int column)
		{
			if (row <= 16 || row >= 40)
			{
				return 0;
			}
			int num = row - 16;
			return (sbyte)((arrayToGet[column] >> num) & 1);
		}

		public virtual void ResetMinoFlags()
		{
			for (int i = 0; i < 10; i++)
			{
				mWellMinoMovable[i] = -1;
				mWellMinoPushable[i] = 0;
			}
		}

		public virtual void SetFlagForTetrimino(int[] arrayToSet, Tetrimino tetrimino, sbyte flag)
		{
			for (Mino mino = tetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				SetWellUpdateFlag(arrayToSet, flag, mino.GetMatrixPosY(), mino.GetMatrixPosX());
			}
		}

		public virtual void GenerateMovableFlag(Well well, int lineCount)
		{
			int wellTop = well.GetWellTop();
			bool flag = false;
			Tetrimino tetrimino = null;
			for (int i = wellTop; i < wellTop + lineCount; i++)
			{
				do
				{
					flag = false;
					for (int j = 0; j < 10; j++)
					{
						if (!IsThereAHoleInMatrice(well, i, j) && IsMinoMovableFromMinoFlag(i, j))
						{
							tetrimino = well.GetLine(i).GetLockedMino(j).GetTetrimino();
							if (!tetrimino.IsGravitySensitive() || !IsMinoMovableFromMinoFlag(i - 1, j))
							{
								SetFlagForTetrimino(mWellMinoMovable, tetrimino, 0);
								flag = true;
							}
						}
					}
				}
				while (flag);
			}
		}

		public virtual bool IsThereAHoleInMatrice(Well well, int lineIndex, int minoIndex)
		{
			if (lineIndex >= 40)
			{
				return false;
			}
			WellLine line = well.GetLine(lineIndex);
			return !line.IsThereLockedMino(minoIndex);
		}

		public virtual bool IsMinoPushableFromFlag(int lineIndex, int minoIndex)
		{
			return GetWellUpdateFlag(mWellMinoPushable, lineIndex, minoIndex) == 1;
		}

		public virtual void GeneratePushableFlag(Well well, int firstLineClearIndex)
		{
			int wellTop = well.GetWellTop();
			bool flag = false;
			Tetrimino tetrimino = null;
			for (int i = 0; i < 10; i++)
			{
				if (IsThereAHoleInMatrice(well, firstLineClearIndex, i) && !IsThereAHoleInMatrice(well, firstLineClearIndex - 1, i))
				{
					tetrimino = well.GetLine(firstLineClearIndex - 1).GetLockedMino(i).GetTetrimino();
					if (tetrimino.IsGravitySensitive())
					{
						SetFlagForTetrimino(mWellMinoPushable, tetrimino, 1);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			for (int num = firstLineClearIndex - 2; num >= wellTop; num--)
			{
				do
				{
					flag2 = false;
					for (int j = 0; j < 10; j++)
					{
						if (!IsThereAHoleInMatrice(well, num, j) && !IsMinoPushableFromFlag(num, j))
						{
							tetrimino = well.GetLine(num).GetLockedMino(j).GetTetrimino();
							if (tetrimino.IsGravitySensitive() && IsMinoPushableFromFlag(num + 1, j))
							{
								SetFlagForTetrimino(mWellMinoPushable, tetrimino, 1);
								flag2 = true;
							}
						}
					}
				}
				while (flag2);
			}
		}

		public virtual bool IsThereMinoToMove()
		{
			for (int i = 0; i < 10; i++)
			{
				if ((mWellMinoMovable[i] & mWellMinoPushable[i]) != 0)
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool IsUpdatableMino(int lineIndex, int minoIndex)
		{
			if (IsMinoMovableFromMinoFlag(lineIndex, minoIndex))
			{
				return IsMinoPushableFromFlag(lineIndex, minoIndex);
			}
			return false;
		}

		public virtual void GenerateCanMoveDownFlag(Well well, int lastClearLineIndex, int nbLineDown)
		{
			int wellTop = well.GetWellTop();
			bool flag = false;
			Tetrimino tetrimino = null;
			int num = ((nbLineDown > 0) ? 1 : 0);
			for (int num2 = lastClearLineIndex; num2 >= wellTop; num2--)
			{
				do
				{
					flag = false;
					for (int i = 0; i < 10; i++)
					{
						if (!IsThereAHoleInMatrice(well, num2, i) && IsMinoMovableFromMinoFlag(num2, i))
						{
							tetrimino = well.GetLine(num2).GetLockedMino(i).GetTetrimino();
							if (!tetrimino.IsGravitySensitive() || !IsMinoMovableFromMinoFlag(num2 + nbLineDown, i) || !IsMinoMovableFromMinoFlag(num2 + num, i) || num2 == lastClearLineIndex)
							{
								SetFlagForTetrimino(mWellMinoMovable, tetrimino, 0);
								flag = true;
							}
						}
					}
				}
				while (flag);
			}
		}

		public virtual void UpdateMinoPos(short offset)
		{
			if (offset == 0)
			{
				return;
			}
			Well well = mWell;
			int wellTop = mWell.GetWellTop();
			WellLine wellLine = null;
			for (int i = wellTop; i <= mClearLineIndex; i++)
			{
				wellLine = well.GetLine(i);
				for (int j = 0; j < 10; j++)
				{
					if (IsUpdatableMino(i, j) && !IsThereAHoleInMatrice(well, i, j))
					{
						Mino lockedMino = wellLine.GetLockedMino(j);
						OffsetMinoSprite(lockedMino, offset);
						mMinosHaveMoved = true;
					}
				}
			}
		}

		public virtual void InitMoveDownFlag()
		{
			for (int i = 0; i < 10; i++)
			{
				mWellMinoMovable[i] = -1;
			}
			GenerateCanMoveDownFlag(mWell, mClearLineIndex, mLineCross);
		}

		public virtual void LogArray(int[] arrayToLog)
		{
			FlString flString = new FlString();
			for (int i = 20; i < 40; i++)
			{
				flString.AddAssign(new FlString(i));
				flString.AddAssign(new FlString("|"));
				for (int j = 0; j < 10; j++)
				{
					if (GetWellUpdateFlag(arrayToLog, i, j) == 0)
					{
						flString.AddAssign(new FlString("0|"));
					}
					else
					{
						flString.AddAssign(new FlString("1|"));
					}
				}
				flString.Assign(new FlString());
			}
			flString = null;
		}
	}
}
