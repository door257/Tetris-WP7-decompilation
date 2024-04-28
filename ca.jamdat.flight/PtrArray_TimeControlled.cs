using System.Collections.Generic;

namespace ca.jamdat.flight
{
	public class PtrArray_TimeControlled
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public List<object> mVector;

		public PtrArray_TimeControlled()
			: this(0, 32)
		{
		}

		public PtrArray_TimeControlled(int inCapacity, int inDelta)
		{
			mVector = new List<object>(inCapacity);
		}

		public PtrArray_TimeControlled(PtrArray_TimeControlled array)
			: this(array.GetCapacity(), 32)
		{
			Assign(array);
		}

		public static PtrArray_TimeControlled Cast(object o, PtrArray_TimeControlled _)
		{
			return (PtrArray_TimeControlled)o;
		}

		public virtual PtrArray_TimeControlled Assign(PtrArray_TimeControlled array)
		{
			List<object> list = mVector;
			List<object> list2 = array.mVector;
			int num = (list.Capacity = list2.Count);
			for (int i = 0; i < num; i++)
			{
				list[i] = list2[i];
			}
			return this;
		}

		public virtual void destruct()
		{
			for (int i = Start(); i < End(); i++)
			{
				GetAt(i);
			}
		}

		public virtual void Clear()
		{
			mVector.Clear();
		}

		public virtual int Find(TimeControlled element)
		{
			return mVector.IndexOf(element);
		}

		public virtual int Start()
		{
			return 0;
		}

		public virtual int End()
		{
			return mVector.Count;
		}

		public virtual bool IsEmpty()
		{
			return mVector.Count == 0;
		}

		public virtual void SetAt(TimeControlled element, int p)
		{
			mVector[p] = element;
		}

		public virtual TimeControlled GetAt(int p)
		{
			return (TimeControlled)mVector[p];
		}

		public virtual void Insert(TimeControlled element)
		{
			mVector.Add(element);
		}

		public virtual void InsertAt(TimeControlled element, int p)
		{
			mVector.Insert(p, element);
		}

		public virtual void Remove(TimeControlled element)
		{
			mVector.Remove(element);
		}

		public virtual void RemoveAt(int p)
		{
			mVector.RemoveAt(p);
		}

		public virtual bool MoveToEnd(TimeControlled element)
		{
			mVector.Remove(element);
			mVector.Add(element);
			return true;
		}

		public virtual void EnsureCapacity(int inNewSize)
		{
			SetSize(inNewSize);
		}

		public virtual void SetSize(int inNewSize)
		{
			for (int num = mVector.Count - inNewSize; num > 0; num--)
			{
				mVector.RemoveAt(num - 1);
			}
			mVector.Capacity = inNewSize;
			for (int num2 = mVector.Capacity - mVector.Count; num2 > 0; num2--)
			{
				mVector.Add(null);
			}
		}

		public virtual int GetCapacity()
		{
			return mVector.Capacity;
		}

		public virtual void OnSerialize(Package _package)
		{
			int t = 0;
			t = _package.SerializeIntrinsic(t);
			Clear();
			EnsureCapacity(t);
			for (int i = 0; i < t; i++)
			{
				TimeControlled timeControlled = null;
				timeControlled = TimeControlled.Cast(_package.SerializePointer(0, true, false), null);
				Insert(timeControlled);
			}
		}

		public static PtrArray_TimeControlled[] InstArrayPtrArray_TimeControlled(int size)
		{
			PtrArray_TimeControlled[] array = new PtrArray_TimeControlled[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PtrArray_TimeControlled();
			}
			return array;
		}

		public static PtrArray_TimeControlled[][] InstArrayPtrArray_TimeControlled(int size1, int size2)
		{
			PtrArray_TimeControlled[][] array = new PtrArray_TimeControlled[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_TimeControlled[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_TimeControlled();
				}
			}
			return array;
		}

		public static PtrArray_TimeControlled[][][] InstArrayPtrArray_TimeControlled(int size1, int size2, int size3)
		{
			PtrArray_TimeControlled[][][] array = new PtrArray_TimeControlled[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_TimeControlled[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_TimeControlled[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PtrArray_TimeControlled();
					}
				}
			}
			return array;
		}
	}
}
