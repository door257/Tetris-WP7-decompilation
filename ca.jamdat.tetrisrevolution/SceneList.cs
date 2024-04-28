namespace ca.jamdat.tetrisrevolution
{
	public class SceneList
	{
		public BaseScene mFirst;

		public BaseScene mLast;

		public int mCount;

		public virtual void destruct()
		{
		}

		public virtual bool IsEmpty()
		{
			if (mFirst == null)
			{
				return mLast == null;
			}
			return false;
		}

		public virtual void AddLast(BaseScene baseScene)
		{
			baseScene.AddRef();
			if (mFirst == null)
			{
				mFirst = baseScene;
				mLast = baseScene;
			}
			else
			{
				mLast.mNextScene = baseScene;
				baseScene.mPrevScene = mLast;
				mLast = baseScene;
			}
			mCount++;
		}

		public virtual BaseScene GetLast()
		{
			return mLast;
		}

		public virtual void RemoveLast()
		{
			BaseScene baseScene = mLast;
			if (baseScene != null)
			{
				if (baseScene == mFirst)
				{
					mLast = null;
					mFirst = null;
				}
				else
				{
					mLast = baseScene.mPrevScene;
					baseScene.mPrevScene = null;
					mLast.mNextScene = null;
				}
				baseScene.RemoveRef();
				mCount--;
			}
		}

		public virtual BaseScene Find(int sceneId)
		{
			BaseScene mNextScene = mFirst;
			while (mNextScene != null && mNextScene.GetId() != sceneId)
			{
				mNextScene = mNextScene.mNextScene;
			}
			return mNextScene;
		}

		public virtual void InsertAfter(BaseScene node, BaseScene newNode)
		{
			newNode.mPrevScene = node;
			newNode.mNextScene = node.mNextScene;
			node.mNextScene = newNode;
			if (newNode.mNextScene != null)
			{
				newNode.mNextScene.mPrevScene = newNode;
			}
			if (node == mLast)
			{
				mLast = newNode;
			}
			newNode.AddRef();
			mCount++;
		}

		public virtual int GetCount()
		{
			return mCount;
		}

		public static SceneList[] InstArraySceneList(int size)
		{
			SceneList[] array = new SceneList[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SceneList();
			}
			return array;
		}

		public static SceneList[][] InstArraySceneList(int size1, int size2)
		{
			SceneList[][] array = new SceneList[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneList[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneList();
				}
			}
			return array;
		}

		public static SceneList[][][] InstArraySceneList(int size1, int size2, int size3)
		{
			SceneList[][][] array = new SceneList[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SceneList[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SceneList[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SceneList();
					}
				}
			}
			return array;
		}
	}
}
