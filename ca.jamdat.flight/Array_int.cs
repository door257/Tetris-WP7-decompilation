namespace ca.jamdat.flight
{
	public class Array_int
	{
		public const sbyte typeNumber = 93;

		public const sbyte typeID = 93;

		public const bool supportsDynamicSerialization = false;

		public int[] mElements;

		public int mElementCount;

		public int mDelta;

		public Array_int()
		{
			mDelta = 32;
		}

		public Array_int(int inCapacity, int inDelta)
		{
			mDelta = inDelta;
			if (inCapacity != 0)
			{
				mElements = new int[inCapacity];
			}
		}

		public Array_int(Array_int array)
		{
			mDelta = 32;
			Assign(array);
		}

		public static Array_int Cast(object o, Array_int _)
		{
			return (Array_int)o;
		}

		public virtual void destruct()
		{
			mElements = null;
			mElementCount = 0;
		}

		public virtual Array_int Assign(Array_int array)
		{
			mElementCount = 0;
			mDelta = array.mDelta;
			mElements = null;
			if (array.GetCapacity() > 0)
			{
				mElements = new int[array.GetCapacity()];
			}
			for (int i = 0; i < array.End(); i++)
			{
				mElements[i] = array.GetAt(i);
				mElementCount++;
			}
			return this;
		}

		public virtual int Find(int c)
		{
			int num = mElementCount - 1;
			while (num >= 0 && mElements[num] != c)
			{
				num--;
			}
			return num;
		}

		public virtual int Start()
		{
			return 0;
		}

		public virtual int End()
		{
			return mElementCount;
		}

		public virtual int Size()
		{
			return End() - Start();
		}

		public virtual bool IsEmpty()
		{
			return mElementCount == 0;
		}

		public virtual int GetAt(int p)
		{
			return mElements[p];
		}

		public virtual int[] GetData()
		{
			return mElements;
		}

		public virtual void CopyInto(int[] array, int length)
		{
			for (int i = 0; i < mElementCount; i++)
			{
				array[i] = mElements[i];
			}
		}

		public virtual void SetAt(int element, int index)
		{
			mElements[index] = element;
		}

		public virtual int Insert(int element)
		{
			int num = mElementCount;
			int inNewSize = num + 1;
			EnsureCapacity(inNewSize);
			mElements[num] = element;
			mElementCount = inNewSize;
			return num;
		}

		public virtual void InsertAt(int element, int index)
		{
			if (index >= 0 && index <= mElementCount)
			{
				EnsureCapacity(mElementCount + 1);
				for (int num = mElementCount; num > index; num--)
				{
					mElements[num] = mElements[num - 1];
				}
				mElementCount++;
				mElements[index] = element;
			}
		}

		public virtual bool MoveToEnd(int element)
		{
			int num = Find(element);
			if (num < 0)
			{
				return false;
			}
			int num2 = mElements[num];
			for (int i = num; i < mElementCount - 1; i++)
			{
				mElements[i] = mElements[i + 1];
			}
			mElements[mElementCount - 1] = num2;
			return true;
		}

		public virtual void Remove(int element)
		{
			int num = Find(element);
			if (num >= 0)
			{
				RemoveAt(num);
			}
		}

		public virtual void RemoveAt(int index)
		{
			if (index >= 0 && index < mElementCount)
			{
				if (mElementCount > 0)
				{
					mElementCount--;
				}
				for (int i = index; i < mElementCount; i++)
				{
					mElements[i] = mElements[i + 1];
				}
			}
		}

		public virtual void Clear()
		{
			mElementCount = 0;
		}

		public virtual void SetSize(int inNewSize)
		{
			EnsureCapacity(inNewSize);
			mElementCount = inNewSize;
		}

		public virtual void EnsureCapacity(int inNewSize)
		{
			int capacity = GetCapacity();
			if (capacity < inNewSize)
			{
				if (inNewSize - capacity < mDelta)
				{
					inNewSize = capacity + mDelta;
				}
				int[] array = new int[inNewSize];
				for (int i = 0; i < mElementCount; i++)
				{
					array[i] = mElements[i];
				}
				mElements = array;
			}
		}

		public virtual int GetCapacity()
		{
			if (mElements != null)
			{
				return mElements.Length;
			}
			return 0;
		}

		public virtual void OnSerialize(Package pack)
		{
			OnSerializeIntrinsics(pack);
		}

		public virtual void OnSerializeIntrinsics(Package _package)
		{
			mElementCount = _package.SerializeIntrinsic(mElementCount);
			mElements = _package.SerializeIntrinsics(mElements, mElementCount);
		}

		public virtual void OnSerializeObjects(Package _package)
		{
		}

		public static Array_int[] InstArrayArray_int(int size)
		{
			Array_int[] array = new Array_int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Array_int();
			}
			return array;
		}

		public static Array_int[][] InstArrayArray_int(int size1, int size2)
		{
			Array_int[][] array = new Array_int[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Array_int[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Array_int();
				}
			}
			return array;
		}

		public static Array_int[][][] InstArrayArray_int(int size1, int size2, int size3)
		{
			Array_int[][][] array = new Array_int[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Array_int[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Array_int[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Array_int();
					}
				}
			}
			return array;
		}
	}
}
