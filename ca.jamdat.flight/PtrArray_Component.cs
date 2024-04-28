using System.Collections.Generic;

namespace ca.jamdat.flight
{
	public class PtrArray_Component
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public List<object> mVector;

		public PtrArray_Component()
			: this(0, 32)
		{
		}

		public PtrArray_Component(int inCapacity, int inDelta)
		{
			mVector = new List<object>(inCapacity);
		}

		public PtrArray_Component(PtrArray_Component array)
			: this(array.GetCapacity(), 32)
		{
			Assign(array);
		}

		public static PtrArray_Component Cast(object o, PtrArray_Component _)
		{
			return (PtrArray_Component)o;
		}

		public virtual PtrArray_Component Assign(PtrArray_Component array)
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

		public virtual int Find(Component element)
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

		public virtual void SetAt(Component element, int p)
		{
			mVector[p] = element;
		}

		public virtual Component GetAt(int p)
		{
			return (Component)mVector[p];
		}

		public virtual void Insert(Component element)
		{
			mVector.Add(element);
		}

		public virtual void InsertAt(Component element, int p)
		{
			mVector.Insert(p, element);
		}

		public virtual void Remove(Component element)
		{
			mVector.Remove(element);
		}

		public virtual void RemoveAt(int p)
		{
			mVector.RemoveAt(p);
		}

		public virtual bool MoveToEnd(Component element)
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
				Component component = null;
				component = Component.Cast(_package.SerializePointer(67, true, false), null);
				Insert(component);
			}
		}

		public static PtrArray_Component[] InstArrayPtrArray_Component(int size)
		{
			PtrArray_Component[] array = new PtrArray_Component[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PtrArray_Component();
			}
			return array;
		}

		public static PtrArray_Component[][] InstArrayPtrArray_Component(int size1, int size2)
		{
			PtrArray_Component[][] array = new PtrArray_Component[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_Component[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_Component();
				}
			}
			return array;
		}

		public static PtrArray_Component[][][] InstArrayPtrArray_Component(int size1, int size2, int size3)
		{
			PtrArray_Component[][][] array = new PtrArray_Component[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PtrArray_Component[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PtrArray_Component[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PtrArray_Component();
					}
				}
			}
			return array;
		}
	}
}
