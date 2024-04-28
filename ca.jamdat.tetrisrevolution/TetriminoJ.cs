namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoJ : Tetrimino
	{
		public TetriminoJ(Well pWell, int initialNbOfMinos)
			: base(pWell, initialNbOfMinos)
		{
			Mino rootMino = mMinoList.GetRootMino();
			rootMino.SetType(1);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(1);
			rootMino.SetRelativePosX(-1);
			rootMino.SetRelativePosY(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(1);
			rootMino.SetRelativePosX(1);
			rootMino.SetRelativePosY(0);
			rootMino = rootMino.GetNextNode();
			rootMino.SetType(1);
			rootMino.SetRelativePosX(-1);
			rootMino.SetRelativePosY(-1);
		}

		public override bool CanRotate(int adjX, int adjY, int newFacingDir)
		{
			Well well = mWell;
			int num = mCoreMatrixPosX + adjX;
			int num2 = mCoreMatrixPosY + adjY;
			switch (newFacingDir)
			{
			case 0:
				if (num <= well.GetLeftLimit() + 1 || num >= well.GetRightLimit() - 1 || num2 > well.GetBottomLimit() || well.IsThereLockedMino(num - 1, num2 - 1) || well.IsMaskUsed(7, num - 1, num2))
				{
					return false;
				}
				break;
			case 1:
				if (num <= well.GetLeftLimit() || num >= well.GetRightLimit() - 1 || num2 >= well.GetBottomLimit() - 1 || well.IsMaskUsed(3, num, num2 - 1) || well.IsThereLockedMino(num, num2) || well.IsThereLockedMino(num, num2 + 1))
				{
					return false;
				}
				break;
			case 2:
				if (num <= well.GetLeftLimit() + 1 || num >= well.GetRightLimit() - 1 || num2 >= well.GetBottomLimit() - 1 || well.IsMaskUsed(7, num - 1, num2) || well.IsThereLockedMino(num + 1, num2 + 1))
				{
					return false;
				}
				break;
			case 3:
				if (num <= well.GetLeftLimit() + 1 || num > well.GetRightLimit() - 1 || num2 >= well.GetBottomLimit() - 1 || well.IsThereLockedMino(num, num2 - 1) || well.IsThereLockedMino(num, num2) || well.IsMaskUsed(3, num - 1, num2 + 1))
				{
					return false;
				}
				break;
			}
			return true;
		}

		public override sbyte GetTetriminoType()
		{
			return 1;
		}

		public TetriminoJ(Well pWell)
			: this(pWell, 4)
		{
		}
	}
}
