namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoO : Tetrimino
	{
		public TetriminoO(Well pWell, int initialNbOfMinos)
			: base(pWell, initialNbOfMinos)
		{
			Mino rootMino = mMinoList.GetRootMino();
			rootMino.SetType(3);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(3);
			rootMino.SetRelativePosX(1);
			rootMino.SetRelativePosY(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(3);
			rootMino.SetRelativePosX(0);
			rootMino.SetRelativePosY(-1);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(3);
			rootMino.SetRelativePosX(1);
			rootMino.SetRelativePosY(-1);
		}

		public override bool CanRotate(int adjX, int adjY, int newFacingDir)
		{
			int num = mCoreMatrixPosX + adjX;
			int num2 = mCoreMatrixPosY + adjY;
			if (num <= mWell.GetLeftLimit() || num > mWell.GetRightLimit() - 1 - 1 || num2 > mWell.GetBottomLimit() - 1 || mWell.IsMaskUsed(3, num, num2 - 1) || mWell.IsMaskUsed(3, num, num2))
			{
				return false;
			}
			return true;
		}

		public override sbyte GetTetriminoType()
		{
			return 3;
		}

		public override bool ChangeDir(int newFacing)
		{
			return true;
		}

		public TetriminoO(Well pWell)
			: this(pWell, 4)
		{
		}
	}
}
