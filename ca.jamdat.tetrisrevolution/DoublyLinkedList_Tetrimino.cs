namespace ca.jamdat.tetrisrevolution
{
	public class DoublyLinkedList_Tetrimino
	{
		public Tetrimino mRootNode;

		public virtual void destruct()
		{
			mRootNode = null;
		}

		public virtual void AddNodeToList(Tetrimino newNode)
		{
			newNode.SetPrevNode(null);
			Tetrimino tetrimino = mRootNode;
			if (tetrimino != null)
			{
				newNode.SetNextNode(tetrimino);
				tetrimino.SetPrevNode(newNode);
			}
			else
			{
				newNode.SetNextNode(null);
			}
			mRootNode = newNode;
		}

		public virtual void RemoveNodeFromList(Tetrimino currentNode)
		{
			if (currentNode != null)
			{
				Tetrimino prevNode = currentNode.GetPrevNode();
				Tetrimino nextNode = currentNode.GetNextNode();
				if (nextNode != null)
				{
					nextNode.SetPrevNode(prevNode);
				}
				if (prevNode != null)
				{
					prevNode.SetNextNode(nextNode);
				}
				else
				{
					mRootNode = nextNode;
				}
			}
		}

		public virtual void ReleaseNode(Tetrimino currentNode)
		{
			if (currentNode != null)
			{
				currentNode.Unload();
				RemoveNodeFromList(currentNode);
				currentNode = null;
			}
		}

		public virtual void ReleaseAllNodes()
		{
			while (mRootNode != null)
			{
				ReleaseNode(mRootNode);
			}
		}

		public virtual Tetrimino GetRootNode()
		{
			return mRootNode;
		}

		public static DoublyLinkedList_Tetrimino[] InstArrayDoublyLinkedList_Tetrimino(int size)
		{
			DoublyLinkedList_Tetrimino[] array = new DoublyLinkedList_Tetrimino[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new DoublyLinkedList_Tetrimino();
			}
			return array;
		}

		public static DoublyLinkedList_Tetrimino[][] InstArrayDoublyLinkedList_Tetrimino(int size1, int size2)
		{
			DoublyLinkedList_Tetrimino[][] array = new DoublyLinkedList_Tetrimino[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DoublyLinkedList_Tetrimino[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DoublyLinkedList_Tetrimino();
				}
			}
			return array;
		}

		public static DoublyLinkedList_Tetrimino[][][] InstArrayDoublyLinkedList_Tetrimino(int size1, int size2, int size3)
		{
			DoublyLinkedList_Tetrimino[][][] array = new DoublyLinkedList_Tetrimino[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DoublyLinkedList_Tetrimino[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DoublyLinkedList_Tetrimino[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new DoublyLinkedList_Tetrimino();
					}
				}
			}
			return array;
		}
	}
}
