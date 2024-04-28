namespace ca.jamdat.tetrisrevolution
{
	public class DoublyLinkedList_Mino
	{
		public Mino mRootNode;

		public virtual void destruct()
		{
			mRootNode = null;
		}

		public virtual void AddNodeToList(Mino newNode)
		{
			newNode.SetPrevNode(null);
			Mino mino = mRootNode;
			if (mino != null)
			{
				newNode.SetNextNode(mino);
				mino.SetPrevNode(newNode);
			}
			else
			{
				newNode.SetNextNode(null);
			}
			mRootNode = newNode;
		}

		public virtual void RemoveNodeFromList(Mino currentNode)
		{
			if (currentNode != null)
			{
				Mino prevNode = currentNode.GetPrevNode();
				Mino nextNode = currentNode.GetNextNode();
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

		public virtual void ReleaseNode(Mino currentNode)
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

		public virtual Mino GetRootNode()
		{
			return mRootNode;
		}

		public static DoublyLinkedList_Mino[] InstArrayDoublyLinkedList_Mino(int size)
		{
			DoublyLinkedList_Mino[] array = new DoublyLinkedList_Mino[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new DoublyLinkedList_Mino();
			}
			return array;
		}

		public static DoublyLinkedList_Mino[][] InstArrayDoublyLinkedList_Mino(int size1, int size2)
		{
			DoublyLinkedList_Mino[][] array = new DoublyLinkedList_Mino[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DoublyLinkedList_Mino[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DoublyLinkedList_Mino();
				}
			}
			return array;
		}

		public static DoublyLinkedList_Mino[][][] InstArrayDoublyLinkedList_Mino(int size1, int size2, int size3)
		{
			DoublyLinkedList_Mino[][][] array = new DoublyLinkedList_Mino[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DoublyLinkedList_Mino[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DoublyLinkedList_Mino[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new DoublyLinkedList_Mino();
					}
				}
			}
			return array;
		}
	}
}
