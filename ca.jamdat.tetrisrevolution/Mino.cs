using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Mino
	{
		public const sbyte minoTypeNone = -1;

		public const sbyte minoTypeTetriminoI = 0;

		public const sbyte minoTypeTetriminoJ = 1;

		public const sbyte minoTypeTetriminoL = 2;

		public const sbyte minoTypeTetriminoO = 3;

		public const sbyte minoTypeTetriminoS = 4;

		public const sbyte minoTypeTetriminoT = 5;

		public const sbyte minoTypeTetriminoZ = 6;

		public const sbyte minoTypeGhost = 7;

		public const sbyte minoTypeGrey = 8;

		public const sbyte minoTypeDestroyed = 9;

		public const sbyte minoTypeLedges = 10;

		public const sbyte minoTypeLineClear = 11;

		public const sbyte minoTypeHot = 12;

		public const sbyte minoTypeIce = 13;

		public const sbyte minoTypeCount = 14;

		public const sbyte minoTypeColorNum = 7;

		public Tetrimino mTetrimino;

		public Well mWell;

		public sbyte mMinoType;

		public int mRelativePosX;

		public int mRelativePosY;

		public int mDefaultIdx;

		public Mino mNextMino;

		public Mino mPrevMino;

		public int mMatrixPosX;

		public int mMatrixPosY;

		public Viewport mMinoViewport;

		public MinoSprite mMinoSprite;

		public MinoShadowShape mMinoShadowShape;

		public Mino()
		{
		}

		public Mino(Well well, Tetrimino tetrimino)
		{
			mTetrimino = tetrimino;
			mWell = well;
			mMinoType = -1;
			mMinoViewport = new Viewport();
			mMinoViewport.SetSize(32, 32);
		}

		public virtual void destruct()
		{
			mMinoViewport = null;
		}

		public virtual void Unload()
		{
			if (mMinoSprite != null)
			{
				mMinoSprite.Unload();
				mMinoSprite = null;
			}
			if (mMinoShadowShape != null)
			{
				mMinoShadowShape.Unload();
				mMinoShadowShape = null;
			}
			mMinoViewport.SetViewport(null);
		}

		public virtual void SetType(sbyte type)
		{
			if (type != 9)
			{
				if (type == 7)
				{
					if (mMinoShadowShape == null)
					{
						mMinoShadowShape = new MinoShadowShape();
					}
					if (mMinoSprite != null)
					{
						mMinoSprite.Unload();
						mMinoSprite = null;
					}
				}
				if (type != 7)
				{
					if (mMinoShadowShape != null)
					{
						mMinoShadowShape.SetViewport(null);
						mMinoShadowShape.Unload();
						mMinoShadowShape = null;
					}
					if (mMinoSprite == null)
					{
						mMinoSprite = new MinoSprite();
					}
					sbyte defaultAspect = GetDefaultAspect(type);
					FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(defaultAspect);
					SetCurrentAspect(defaultAspect, bitmapForMinoSpriteAspect);
					SynchViewportAndSpriteSize();
				}
			}
			mMinoType = type;
		}

		public virtual sbyte GetTetriminoType()
		{
			return mMinoType;
		}

		public virtual void SetMinoGhostAspect(sbyte aspect)
		{
			mMinoShadowShape.SetMinoShadowShapeAspect(aspect);
		}

		public virtual void SetMinoBorders()
		{
			mMinoShadowShape.SetMinoShadowBorders(mTetrimino.GetSticky(mDefaultIdx, mTetrimino.GetFacingDir()));
		}

		public virtual void SetCurrentAspect(sbyte aspect, FlBitmapMap bitmapMap)
		{
			mMinoSprite.SetMinoSpriteAspect(aspect, bitmapMap);
		}

		public virtual sbyte GetCurrentAspect()
		{
			if (mMinoSprite != null)
			{
				return mMinoSprite.GetMinoSpriteAspect();
			}
			return -1;
		}

		public virtual void SetCurrentAspectSize(sbyte aspectSize)
		{
			mMinoSprite.SetMinoSpriteAspectSize(aspectSize);
			SynchViewportAndSpriteSize();
			if (mTetrimino.IsInWell())
			{
				UpdateMinoSpritePosition();
			}
		}

		public virtual sbyte GetCurrentAspectSize()
		{
			return mMinoSprite.GetMinoSpriteAspectSize();
		}

		public static sbyte GetDefaultAspect(sbyte minoType)
		{
			sbyte result = -1;
			switch (minoType)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
				result = minoType;
				break;
			case 10:
				result = 17;
				break;
			case 8:
				result = 17;
				break;
			case 11:
				result = -1;
				break;
			case 13:
				result = 16;
				break;
			case 12:
				result = 1;
				break;
			}
			return result;
		}

		public virtual bool IsMinoValid()
		{
			if (mMinoType != -1 && mMinoType != 9)
			{
				return mMinoType != 11;
			}
			return false;
		}

		public virtual bool CanMove(int dirX, int dirY)
		{
			bool flag = false;
			int num = mWell.GetBottomLimit();
			int num2 = mMatrixPosX + dirX;
			int num3 = mMatrixPosY + dirY;
			if (dirY == -1)
			{
				num = 19;
			}
			bool flag2 = num2 != mWell.GetRightLimit() && num2 != mWell.GetLeftLimit();
			bool flag3 = num3 != num;
			if ((dirX != 0 && !flag2) || (dirY != 0 && !flag3) || (num3 < 40 && mWell.IsThereLockedMino(num2, num3)))
			{
				return false;
			}
			return true;
		}

		public virtual void SynchViewportAndSpriteSize()
		{
			if (mMinoViewport.GetRectWidth() != mMinoSprite.GetRectWidth())
			{
				mMinoViewport.SetSize(mMinoSprite.GetRectWidth(), mMinoSprite.GetRectHeight());
			}
		}

		public virtual void SetRelativePosX(int posX)
		{
			mRelativePosX = posX;
		}

		public virtual void SetRelativePosY(int posY)
		{
			mRelativePosY = posY;
		}

		public virtual int GetBaseRelativePosX()
		{
			return mRelativePosX;
		}

		public virtual int GetBaseRelativePosY()
		{
			return mRelativePosY;
		}

		public virtual int GetRelativePosX()
		{
			switch (mTetrimino.GetFacingDir())
			{
			case 0:
				return mRelativePosX;
			case 1:
				return -mRelativePosY;
			case 2:
				return -mRelativePosX;
			case 3:
				return mRelativePosY;
			default:
				return 0;
			}
		}

		public virtual int GetRelativePosY()
		{
			switch (mTetrimino.GetFacingDir())
			{
			case 0:
				return mRelativePosY;
			case 1:
				return mRelativePosX;
			case 2:
				return -mRelativePosY;
			case 3:
				return -mRelativePosX;
			default:
				return 0;
			}
		}

		public virtual int GetMatrixPosX()
		{
			return mMatrixPosX;
		}

		public virtual int GetMatrixPosY()
		{
			return mMatrixPosY;
		}

		public virtual void SetMatrixPos(int posX, int posY)
		{
			mMatrixPosX = posX;
			mMatrixPosY = posY;
			UpdateMinoSpritePosition();
		}

		public virtual void Move(int dirX, int dirY, bool updateSprite)
		{
			mMatrixPosX += dirX;
			mMatrixPosY += dirY;
			if (updateSprite)
			{
				UpdateMinoSpritePosition();
			}
		}

		public virtual void Clear(sbyte clearingType, bool updateOrphan)
		{
			Tetrimino tetrimino = mTetrimino;
			Well well = mWell;
			tetrimino.NoticeRemovedMino();
			if (IsMinoValid())
			{
				if (clearingType == 11)
				{
					SetAsTypeLineClear();
				}
				else
				{
					SetType(clearingType);
				}
				well.MoveMinoToDestroyList(this);
			}
			tetrimino.UpdateSticky();
			if (updateOrphan)
			{
				well.TetriminoUpdated(tetrimino);
			}
		}

		public virtual Tetrimino GetTetrimino()
		{
			return mTetrimino;
		}

		public virtual Mino GetNextNode()
		{
			return mNextMino;
		}

		public virtual void SetNextNode(Mino nextMino)
		{
			mNextMino = nextMino;
		}

		public virtual Mino GetPrevNode()
		{
			return mPrevMino;
		}

		public virtual void SetPrevNode(Mino prevMino)
		{
			mPrevMino = prevMino;
		}

		public virtual void SetDefaultIdx(int defaultIdx)
		{
			mDefaultIdx = defaultIdx;
		}

		public virtual int GetDefaultIdx()
		{
			return mDefaultIdx;
		}

		public virtual Viewport GetMinoParentViewport()
		{
			return mMinoViewport.GetViewport();
		}

		public virtual Viewport GetMinoViewport()
		{
			return mMinoViewport;
		}

		public virtual MinoSprite GetMinoSprite()
		{
			return mMinoSprite;
		}

		public virtual void AttachMinoComponent()
		{
			if (mMinoType == 7 && mMinoShadowShape != null)
			{
				mMinoShadowShape.SetViewport(mMinoViewport);
			}
			else if (mMinoSprite != null)
			{
				mMinoSprite.SetViewport(mMinoViewport);
			}
		}

		public virtual void UpdateMinoSpritePosition()
		{
			int matrixPosX = GetMatrixPosX();
			int num = GetMatrixPosY() - 20;
			bool flag = num >= 0 && num < 20;
			if (flag)
			{
				mMinoViewport.SetTopLeft((short)(matrixPosX * 31), (short)(num * 31));
			}
			mMinoViewport.SetVisible(flag);
		}

		public virtual void SetAsTypeLineClear()
		{
			mMinoType = 11;
			mMinoViewport.SetVisible(false);
		}

		public virtual void Move(int dirX, int dirY)
		{
			Move(dirX, dirY, true);
		}

		public static Mino[] InstArrayMino(int size)
		{
			Mino[] array = new Mino[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Mino();
			}
			return array;
		}

		public static Mino[][] InstArrayMino(int size1, int size2)
		{
			Mino[][] array = new Mino[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Mino[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Mino();
				}
			}
			return array;
		}

		public static Mino[][][] InstArrayMino(int size1, int size2, int size3)
		{
			Mino[][][] array = new Mino[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Mino[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Mino[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Mino();
					}
				}
			}
			return array;
		}
	}
}
