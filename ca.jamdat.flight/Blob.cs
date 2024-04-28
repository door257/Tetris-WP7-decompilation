namespace ca.jamdat.flight
{
	public class Blob
	{
		public const sbyte typeNumber = 41;

		public const sbyte typeID = 41;

		public const bool supportsDynamicSerialization = false;

		public sbyte[] mData;

		public static Blob Cast(object o, Blob _)
		{
			return (Blob)o;
		}

		public virtual void destruct()
		{
			mData = null;
		}

		public virtual sbyte[] GetData()
		{
			return mData;
		}

		public virtual int GetSize()
		{
			if (mData == null)
			{
				return 0;
			}
			return mData.Length;
		}

		public virtual Blob OnSerialize(Package _package)
		{
			int size = GetSize();
			size = _package.SerializeIntrinsic(size);
			mData = _package.SerializeIntrinsics(mData, size);
			return this;
		}

		public virtual void SetData(int size, sbyte[] data)
		{
			mData = data;
		}

		public static Blob[] InstArrayBlob(int size)
		{
			Blob[] array = new Blob[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Blob();
			}
			return array;
		}

		public static Blob[][] InstArrayBlob(int size1, int size2)
		{
			Blob[][] array = new Blob[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Blob[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Blob();
				}
			}
			return array;
		}

		public static Blob[][][] InstArrayBlob(int size1, int size2, int size3)
		{
			Blob[][][] array = new Blob[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Blob[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Blob[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Blob();
					}
				}
			}
			return array;
		}
	}
}
