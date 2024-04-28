namespace ca.jamdat.flight
{
	public class Array_byte
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public sbyte[] mElements;

		public int mElementCount;

		public int mDelta;

		public Array_byte()
		{
			mDelta = 32;
		}

		public Array_byte(int inCapacity, int inDelta)
		{
			mDelta = inDelta;
			if (inCapacity != 0)
			{
				mElements = new sbyte[inCapacity];
			}
		}

		public Array_byte(Array_byte array)
		{
			mDelta = 32;
			Assign(array);
		}

		public static Array_byte Cast(object o, Array_byte _)
		{
			return (Array_byte)o;
		}

		public virtual void destruct()
		{
			mElements = null;
			mElementCount = 0;
		}

		public virtual Array_byte Assign(Array_byte array)
		{
			mElementCount = 0;
			mDelta = array.mDelta;
			mElements = null;
			if (array.GetCapacity() > 0)
			{
				mElements = new sbyte[array.GetCapacity()];
			}
			for (int i = 0; i < array.End(); i++)
			{
				mElements[i] = array.GetAt(i);
				mElementCount++;
			}
			return this;
		}

		public virtual int Find(sbyte c)
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

		public virtual sbyte GetAt(int p)
		{
			return mElements[p];
		}

		public virtual sbyte[] GetData()
		{
			return mElements;
		}

		public virtual void CopyInto(sbyte[] array, int length)
		{
			for (int i = 0; i < mElementCount; i++)
			{
				array[i] = mElements[i];
			}
		}

		public virtual void SetAt(sbyte element, int index)
		{
			mElements[index] = element;
		}

		public virtual int Insert(sbyte element)
		{
			int num = mElementCount;
			int inNewSize = num + 1;
			EnsureCapacity(inNewSize);
			mElements[num] = element;
			mElementCount = inNewSize;
			return num;
		}

		public virtual void InsertAt(sbyte element, int index)
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

		public virtual bool MoveToEnd(sbyte element)
		{
			int num = Find(element);
			if (num < 0)
			{
				return false;
			}
			sbyte b = mElements[num];
			for (int i = num; i < mElementCount - 1; i++)
			{
				mElements[i] = mElements[i + 1];
			}
			mElements[mElementCount - 1] = b;
			return true;
		}

		public virtual void Remove(sbyte element)
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
				sbyte[] array = new sbyte[inNewSize];
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

		public static Array_byte[] InstArrayArray_byte(int size)
		{
			Array_byte[] array = new Array_byte[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Array_byte();
			}
			return array;
		}

		public static Array_byte[][] InstArrayArray_byte(int size1, int size2)
		{
			Array_byte[][] array = new Array_byte[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Array_byte[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Array_byte();
				}
			}
			return array;
		}

		public static Array_byte[][][] InstArrayArray_byte(int size1, int size2, int size3)
		{
			Array_byte[][][] array = new Array_byte[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Array_byte[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Array_byte[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Array_byte();
					}
				}
			}
			return array;
		}
	}
}
