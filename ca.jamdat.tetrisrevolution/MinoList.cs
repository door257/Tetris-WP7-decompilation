namespace ca.jamdat.tetrisrevolution
{
	public class MinoList
	{
		public DoublyLinkedList_Mino mDoublyLinkedList;

		public MinoList()
		{
			mDoublyLinkedList = new DoublyLinkedList_Mino();
		}

		public virtual void destruct()
		{
			mDoublyLinkedList = null;
		}

		public virtual Mino CreateMino(Well well, Tetrimino parentTetrimino)
		{
			Mino mino = new Mino(well, parentTetrimino);
			AddMinoToList(mino);
			return mino;
		}

		public virtual Mino GetMino(int uniqueId)
		{
			Mino mino = GetRootMino();
			while (mino != null && mino.GetDefaultIdx() != uniqueId)
			{
				mino = mino.GetNextNode();
			}
			return mino;
		}

		public virtual void AddMinoToList(Mino newMino)
		{
			mDoublyLinkedList.AddNodeToList(newMino);
		}

		public virtual void RemoveMinoFromList(Mino mino)
		{
			mDoublyLinkedList.RemoveNodeFromList(mino);
		}

		public virtual void ReleaseMino(Mino mino)
		{
			mDoublyLinkedList.ReleaseNode(mino);
		}

		public virtual void ReleaseAllMinos()
		{
			mDoublyLinkedList.ReleaseAllNodes();
		}

		public virtual void UnloadAllMinos()
		{
			for (Mino mino = GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.Unload();
			}
		}

		public virtual Mino GetRootMino()
		{
			return mDoublyLinkedList.GetRootNode();
		}

		public static MinoList[] InstArrayMinoList(int size)
		{
			MinoList[] array = new MinoList[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new MinoList();
			}
			return array;
		}

		public static MinoList[][] InstArrayMinoList(int size1, int size2)
		{
			MinoList[][] array = new MinoList[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoList[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoList();
				}
			}
			return array;
		}

		public static MinoList[][][] InstArrayMinoList(int size1, int size2, int size3)
		{
			MinoList[][][] array = new MinoList[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoList[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoList[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new MinoList();
					}
				}
			}
			return array;
		}
	}
}
