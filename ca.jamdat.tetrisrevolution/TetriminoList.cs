using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TetriminoList
	{
		public DoublyLinkedList_Tetrimino mDoublyLinkedList;

		public TetriminoList()
		{
			mDoublyLinkedList = new DoublyLinkedList_Tetrimino();
		}

		public virtual void destruct()
		{
			mDoublyLinkedList = null;
		}

		public virtual Tetrimino CreateTetrimino(sbyte newPieceType, Well well)
		{
			Tetrimino tetrimino = CreateTetriminoObject(newPieceType, well);
			AddTetriminoToList(tetrimino);
			return tetrimino;
		}

		public virtual void UpdateAllTetriminosAspect()
		{
			for (Tetrimino tetrimino = GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				for (Mino mino = tetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
				{
					sbyte currentAspect = mino.GetCurrentAspect();
					FlBitmapMap bitmapForMinoSpriteAspect = MinoSprite.GetBitmapForMinoSpriteAspect(currentAspect, mino.GetCurrentAspectSize());
					mino.SetCurrentAspect(currentAspect, bitmapForMinoSpriteAspect);
				}
			}
		}

		public virtual void AddTetriminoToList(Tetrimino newTetrimino)
		{
			mDoublyLinkedList.AddNodeToList(newTetrimino);
		}

		public virtual void RemoveTetriminoFromList(Tetrimino tetrimino)
		{
			mDoublyLinkedList.RemoveNodeFromList(tetrimino);
		}

		public virtual void ReleaseTetrimino(Tetrimino tetrimino)
		{
			mDoublyLinkedList.ReleaseNode(tetrimino);
		}

		public virtual void ReleaseAllTetriminos()
		{
			mDoublyLinkedList.ReleaseAllNodes();
		}

		public virtual void UnloadAllTetriminos()
		{
			for (Tetrimino tetrimino = GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				tetrimino.GetMinoList().UnloadAllMinos();
			}
		}

		public virtual Tetrimino GetRootTetrimino()
		{
			return mDoublyLinkedList.GetRootNode();
		}

		public static Tetrimino CreateTetriminoObject(sbyte newPieceType, Well well)
		{
			Tetrimino tetrimino = null;
			switch (newPieceType)
			{
			case -2:
				tetrimino = new SpecialMino(well);
				break;
			case 0:
				tetrimino = new TetriminoI(well);
				break;
			case 1:
				tetrimino = new TetriminoJ(well);
				break;
			case 2:
				tetrimino = new TetriminoL(well);
				break;
			case 3:
				tetrimino = new TetriminoO(well);
				break;
			case 4:
				tetrimino = new TetriminoS(well);
				break;
			case 5:
				tetrimino = new TetriminoT(well);
				break;
			case 6:
				tetrimino = new TetriminoZ(well);
				break;
			}
			tetrimino.InitMarker();
			tetrimino.UpdateSticky();
			return tetrimino;
		}

		public static TetriminoList[] InstArrayTetriminoList(int size)
		{
			TetriminoList[] array = new TetriminoList[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TetriminoList();
			}
			return array;
		}

		public static TetriminoList[][] InstArrayTetriminoList(int size1, int size2)
		{
			TetriminoList[][] array = new TetriminoList[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TetriminoList[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TetriminoList();
				}
			}
			return array;
		}

		public static TetriminoList[][][] InstArrayTetriminoList(int size1, int size2, int size3)
		{
			TetriminoList[][][] array = new TetriminoList[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TetriminoList[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TetriminoList[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TetriminoList();
					}
				}
			}
			return array;
		}
	}
}
