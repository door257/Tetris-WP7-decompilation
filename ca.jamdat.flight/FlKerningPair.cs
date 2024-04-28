namespace ca.jamdat.flight
{
	public class FlKerningPair
	{
		public const sbyte typeNumber = 111;

		public const sbyte typeID = 111;

		public const bool supportsDynamicSerialization = false;

		public const sbyte kPairHashCode = 0;

		public const sbyte KPairTypeAdvanceOffset = 1;

		public const sbyte KPairTypeMemberSize = 2;

		public int[] mData;

		public static FlKerningPair Cast(object o, FlKerningPair _)
		{
			return (FlKerningPair)o;
		}

		public virtual void destruct()
		{
			mData = null;
		}

		public override int GetHashCode()
		{
			return mData[0];
		}

		public virtual int GetAdvanceOffset()
		{
			return mData[1];
		}

		public virtual FlKerningPair OnSerialize(Package _package)
		{
			mData = _package.SerializeIntrinsics(mData, 2);
			return this;
		}

		public static FlKerningPair[] InstArrayFlKerningPair(int size)
		{
			FlKerningPair[] array = new FlKerningPair[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlKerningPair();
			}
			return array;
		}

		public static FlKerningPair[][] InstArrayFlKerningPair(int size1, int size2)
		{
			FlKerningPair[][] array = new FlKerningPair[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlKerningPair[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlKerningPair();
				}
			}
			return array;
		}

		public static FlKerningPair[][][] InstArrayFlKerningPair(int size1, int size2, int size3)
		{
			FlKerningPair[][][] array = new FlKerningPair[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlKerningPair[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlKerningPair[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlKerningPair();
					}
				}
			}
			return array;
		}
	}
}
