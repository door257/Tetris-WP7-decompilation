namespace ca.jamdat.tetrisrevolution
{
	public class SceneStack
	{
		public SceneList mList;

		public SceneStack()
		{
			mList = new SceneList();
		}

		public virtual void destruct()
		{
			mList = null;
		}

		public virtual BaseScene GetTop()
		{
			return mList.GetLast();
		}

		public virtual void Push(BaseScene s)
		{
			mList.AddLast(s);
		}

		public virtual void Pop()
		{
			mList.RemoveLast();
		}

		public virtual void PopTo(int sceneId)
		{
			do
			{
				mList.RemoveLast();
			}
			while (GetTop() != null && GetTop().GetId() != sceneId);
		}

		public virtual void PopAll()
		{
			PopTo(0);
		}

		public virtual bool IsEmpty()
		{
			return mList.IsEmpty();
		}

		public virtual int GetCount()
		{
			return mList.GetCount();
		}

		public static SceneStack[] InstArraySceneStack(int size)
		{
			SceneStack[] array = new SceneStack[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneStack();
			}
			return array;
		}

		public static SceneStack[][] InstArraySceneStack(int size1, int size2)
		{
			SceneStack[][] array = new SceneStack[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneStack[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneStack();
				}
			}
			return array;
		}

		public static SceneStack[][][] InstArraySceneStack(int size1, int size2, int size3)
		{
			SceneStack[][][] array = new SceneStack[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneStack[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneStack[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneStack();
					}
				}
			}
			return array;
		}
	}
}
