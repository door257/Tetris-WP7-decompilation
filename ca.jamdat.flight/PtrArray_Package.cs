using System.Collections.Generic;

namespace ca.jamdat.flight
{
	public class PtrArray_Package
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public List<object> mVector;

		public PtrArray_Package()
			: this(0, 32)
		{
		}

		public PtrArray_Package(int inCapacity, int inDelta)
		{
			mVector = new List<object>(inCapacity);
		}

		public PtrArray_Package(PtrArray_Package array)
			: this(array.GetCapacity(), 32)
		{
			Assign(array);
		}

		public static PtrArray_Package Cast(object o, PtrArray_Package _)
		{
			return (PtrArray_Package)o;
		}

		public virtual PtrArray_Package Assign(PtrArray_Package array)
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

		public virtual int Find(Package element)
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

		public virtual void SetAt(Package element, int p)
		{
			mVector[p] = element;
		}

		public virtual Package GetAt(int p)
		{
			return (Package)mVector[p];
		}

		public virtual void Insert(Package element)
		{
			mVector.Add(element);
		}

		public virtual void InsertAt(Package element, int p)
		{
			mVector.Insert(p, element);
		}

		public virtual void Remove(Package element)
		{
			mVector.Remove(element);
		}

		public virtual void RemoveAt(int p)
		{
			mVector.RemoveAt(p);
		}

		public virtual bool MoveToEnd(Package element)
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
				mVector.RemoveAt(mVector.Count - 1);
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
				Package package = null;
				package = Package.Cast(_package.SerializePointer(-1, false, false), null);
				Insert(package);
			}
		}

		public static PtrArray_Package[] InstArrayPtrArray_Package(int size)
		{
			PtrArray_Package[] array = new PtrArray_Package[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PtrArray_Package();
			}
			return array;
		}

		public static PtrArray_Package[][] InstArrayPtrArray_Package(int size1, int size2)
		{
			PtrArray_Package[][] array = new PtrArray_Package[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_Package[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_Package();
				}
			}
			return array;
		}

		public static PtrArray_Package[][][] InstArrayPtrArray_Package(int size1, int size2, int size3)
		{
			PtrArray_Package[][][] array = new PtrArray_Package[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_Package[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_Package[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PtrArray_Package();
					}
				}
			}
			return array;
		}
	}
}
