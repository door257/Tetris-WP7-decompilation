namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoI : Tetrimino
	{
		public TetriminoI(Well pWell, int initialNbOfMinos)
			: base(pWell, initialNbOfMinos)
		{
			Mino rootMino = mMinoList.GetRootMino();
			rootMino.SetType(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(0);
			rootMino.SetRelativePosX(1);
			rootMino.SetRelativePosY(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(0);
			rootMino.SetRelativePosX(2);
			rootMino.SetRelativePosY(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(0);
			rootMino.SetRelativePosX(-1);
			rootMino.SetRelativePosY(0);
		}

		public override bool CanRotate(int adjX, int adjY, int newFacingDir)
		{
			Well well = mWell;
			int num = mCoreMatrixPosX + adjX;
			int num2 = mCoreMatrixPosY + adjY;
			switch (newFacingDir)
			{
			case 0:
				if (num <= well.GetLeftLimit() + 1 || num >= well.GetRightLimit() - 2 || num2 >= well.GetBottomLimit() || well.IsMaskUsed(15, num - 1, num2))
				{
					return false;
				}
				break;
			case 1:
				if (num <= well.GetLeftLimit() || num >= well.GetRightLimit() || num2 >= well.GetBottomLimit() - 2 || well.IsThereLockedMino(num, num2 - 1) || well.IsThereLockedMino(num, num2) || well.IsThereLockedMino(num, num2 + 1) || well.IsThereLockedMino(num, num2 + 2))
				{
					return false;
				}
				break;
			case 2:
				if (num <= well.GetLeftLimit() + 2 || num >= well.GetRightLimit() - 1 || num2 >= well.GetBottomLimit() || well.IsMaskUsed(15, num - 2, num2))
				{
					return false;
				}
				break;
			case 3:
				if (num <= well.GetLeftLimit() || num >= well.GetRightLimit() || num2 >= well.GetBottomLimit() - 1 || well.IsThereLockedMino(num, num2 - 2) || well.IsThereLockedMino(num, num2 - 1) || well.IsThereLockedMino(num, num2) || well.IsThereLockedMino(num, num2 + 1))
				{
					return false;
				}
				break;
			}
			return true;
		}

		public override sbyte GetTetriminoType()
		{
			return 0;
		}

		public TetriminoI(Well pWell)
			: this(pWell, 4)
		{
		}
	}
}
