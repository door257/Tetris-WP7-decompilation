using ca.jamdat.fuser;

namespace ca.jamdat.flight
{
	public class Package
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public const int notInFile = -1;

		public const int fromPackage = -2;

		public const sbyte unloaded = 0;

		public const sbyte initializingFileStream = 1;

		public const sbyte uncompressing = 2;

		public const sbyte initializingEntryPoints = 3;

		public const sbyte loadingEntryPoints = 4;

		public const sbyte loadingSoundFX = 5;

		public const sbyte loaded = 6;

		public const bool staticbool = false;

		public const sbyte staticByte = 0;

		public const short staticshort = 0;

		public const int staticlong = 0;

		public static readonly F32 staticF32 = new F32();

		public Library mLibrary;

		public LibraryStream mStream;

		public short mFilePositionIndex;

		public short mPackageType;

		public int mNumPackageDependencies;

		public Package[] mPackageDependencies;

		public int mNumEntryPoints;

		public sbyte mLoadingState;

		public object[] mEntryPoints;

		public object[] mInternalObjects;

		public int mLoadingEntryPointIndex;

		public int mNextObjectNumber;

		public int mNumInternalObjects;

		public int mNextEntryPointIdx;

		public bool mIsBeingUnloaded;

		public Package()
		{
			mLoadingState = 0;
		}

		public Package(Package other)
		{
			mLoadingState = other.mLoadingState;
			mStream = other.mStream;
			mFilePositionIndex = other.mFilePositionIndex;
			mPackageType = other.mPackageType;
			mNumPackageDependencies = other.mNumPackageDependencies;
			mPackageDependencies = other.mPackageDependencies;
			mNumEntryPoints = other.mNumEntryPoints;
			mEntryPoints = other.mEntryPoints;
			mNextEntryPointIdx = other.mNextEntryPointIdx;
			mLoadingEntryPointIndex = other.mLoadingEntryPointIndex;
			mNextObjectNumber = other.mNextObjectNumber;
			mNumInternalObjects = other.mNumInternalObjects;
			mInternalObjects = other.mInternalObjects;
			mIsBeingUnloaded = other.mIsBeingUnloaded;
		}

		public static Package Cast(object o, Package _)
		{
			return (Package)o;
		}

		public virtual void OnSerialize(Package a130)
		{
		}

		public virtual void destruct()
		{
		}

		public virtual void StartImmediateStreamReading(LibraryStream stream)
		{
			mStream = stream;
		}

		public virtual void SetNumDependencies(int numDependencies)
		{
			mNumPackageDependencies = numDependencies;
			mPackageDependencies = new Package[mNumPackageDependencies];
		}

		public virtual void SetDependency(int dependencyNum, Package dependency)
		{
			mPackageDependencies[dependencyNum] = dependency;
		}

		public virtual void Load(bool asynchronous)
		{
			if (asynchronous)
			{
				FrameworkGlobals.GetInstance().mPackageLoader.InsertPackageInQueue(this);
				return;
			}
			SetLoadingState(1);
			LoadFromQueue();
		}

		public virtual void Load(LibraryStream stream)
		{
			SetLoadingState(1);
			SyncLoad(stream);
		}

		public virtual void SetNextEntryPointIndex(int index)
		{
			mNextEntryPointIdx = index;
		}

		public virtual int GetNextEntryPointIndex()
		{
			return mNextEntryPointIdx;
		}

		public virtual void Unload()
		{
			if (IsLoading())
			{
				FrameworkGlobals.GetInstance().mPackageLoader.RemovePackageFromQueue(this);
				SetLoadingState(0);
			}
			else
			{
				mEntryPoints = null;
				SetLoadingState(0);
			}
		}

		public virtual bool IsLoaded()
		{
			return GetLoadingState() == 6;
		}

		public virtual bool IsLoading()
		{
			if (mLoadingState != 6)
			{
				return mLoadingState != 0;
			}
			return false;
		}

		public virtual bool IsWriting()
		{
			return false;
		}

		public virtual bool IsReading()
		{
			return true;
		}

		public virtual int GetFilePosition()
		{
			return mStream.GetPosition();
		}

		public virtual void SkipBytes(int numBytes)
		{
			mStream.Skip(numBytes);
		}

		public virtual object GetEntryPoint(int index)
		{
			if (index == -1)
			{
				index = mNextEntryPointIdx++;
			}
			return mEntryPoints[index];
		}

		public virtual bool GetEntryPoint(int index, bool[] a133)
		{
			return ((bool?)GetEntryPoint(index)).Value;
		}

		public virtual sbyte GetEntryPoint(int index, sbyte[] a135)
		{
			return ((sbyte?)GetEntryPoint(index)).Value;
		}

		public virtual short GetEntryPoint(int index, short[] a137)
		{
			return ((short?)GetEntryPoint(index)).Value;
		}

		public virtual int GetEntryPoint(int index, int[] a139)
		{
			return ((int?)GetEntryPoint(index)).Value;
		}

		public virtual int GetEntryPointCount()
		{
			return mNumEntryPoints;
		}

		public virtual object SerializePointer(sbyte staticTypeID, bool dynamic, bool entryPoint)
		{
			return ReadObjectPointer(staticTypeID, dynamic, entryPoint);
		}

		public virtual sbyte ReadByte()
		{
			return mStream.ReadByte();
		}

		public virtual int ReadLong()
		{
			return mStream.ReadLong();
		}

		public virtual bool SerializeIntrinsic(bool t)
		{
			return SerializeIntrinsicBool();
		}

		public virtual bool SerializeIntrinsicBool()
		{
			sbyte b = ReadByte();
			return b != 0;
		}

		public virtual F32 SerializeIntrinsic(F32 t)
		{
			return new F32(ReadLong());
		}

		public virtual short SerializeShortWithEncoding()
		{
			sbyte t = 0;
			t = SerializeIntrinsic(t);
			int num = t >> 1;
			if (((uint)t & (true ? 1u : 0u)) != 0)
			{
				t = SerializeIntrinsic(t);
				num = (num & 0x7F) | ((t << 6) & -128);
				if (((uint)t & (true ? 1u : 0u)) != 0)
				{
					t = SerializeIntrinsic(t);
					num = (num & 0x3FFF) | ((t << 13) & -16384);
				}
			}
			return (short)num;
		}

		public virtual sbyte SerializeIntrinsic(sbyte t)
		{
			return SerializeIntrinsicByte();
		}

		public virtual sbyte SerializeIntrinsicByte()
		{
			return ReadByte();
		}

		public virtual short SerializeIntrinsic(short t)
		{
			return SerializeIntrinsicShort();
		}

		public virtual short SerializeIntrinsicShort()
		{
			return SerializeShortWithEncoding();
		}

		public virtual int SerializeIntrinsic(int t)
		{
			return SerializeIntrinsicLong();
		}

		public virtual int SerializeIntrinsicLong()
		{
			return ReadLong();
		}

		public virtual bool[] SerializeIntrinsics(bool[] t, int count)
		{
			return SerializeIntrinsicsBools(count);
		}

		public virtual sbyte[] SerializeIntrinsics(sbyte[] t, int count)
		{
			return SerializeIntrinsicsBytes(count);
		}

		public virtual short[] SerializeIntrinsics(short[] t, int count)
		{
			return SerializeIntrinsicsShorts(count);
		}

		public virtual int[] SerializeIntrinsics(int[] t, int count)
		{
			return SerializeIntrinsicsLongs(count);
		}

		public virtual bool[] SerializeIntrinsicsBools(int count)
		{
			sbyte[] array = new sbyte[count];
			mStream.Read(array, 0, count);
			bool[] array2 = new bool[count];
			for (int i = 0; i < count; i++)
			{
				array2[i] = array[i] != 0;
			}
			array = null;
			return array2;
		}

		public virtual sbyte[] SerializeIntrinsicsBytes(int count)
		{
			sbyte[] array = new sbyte[count];
			mStream.Read(array, 0, count);
			return array;
		}

		public virtual short[] SerializeIntrinsicsShorts(int count)
		{
			short[] array = new short[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = SerializeShortWithEncoding();
			}
			return array;
		}

		public virtual int[] SerializeIntrinsicsLongs(int count)
		{
			sbyte[] array = new sbyte[count * 4];
			mStream.Read(array, 0, count * 4);
			int[] array2 = new int[count];
			int num = 0;
			int num2 = 0;
			int num3 = count << 2;
			while (num2 < num3)
			{
				array2[num] = (array[num2++] << 24) | ((array[num2++] & 0xFF) << 16) | ((array[num2++] & 0xFF) << 8) | (array[num2++] & 0xFF);
				num++;
			}
			array = null;
			return array2;
		}

		public virtual Package[] GetPackageDependencies()
		{
			return mPackageDependencies;
		}

		public virtual int GetNumPackageDependencies()
		{
			return mNumPackageDependencies;
		}

		public virtual void StartLoading()
		{
			mLoadingState = 1;
		}

		public virtual void SyncLoad(LibraryStream stream)
		{
			mStream = stream;
			bool t = false;
			if (GetLoadingState() == 1)
			{
				t = SerializeIntrinsic(t);
				SetLoadingState((sbyte)(t ? 2 : 3));
			}
			int num = 0;
			if (GetLoadingState() == 3)
			{
				int t2 = 0;
				t2 = SerializeIntrinsic(t2);
				int num2 = t2;
				for (int i = 0; i < num2; i++)
				{
					short t3 = 0;
					t3 = SerializeIntrinsic(t3);
				}
				num = SerializeIntrinsic(num);
				SetNumEntryPoints(num);
				mNumInternalObjects = SerializeIntrinsic(mNumInternalObjects);
				mInternalObjects = new object[mNumInternalObjects];
				mNextObjectNumber = 0;
				mLoadingEntryPointIndex = 0;
				SetLoadingState(4);
			}
			if (GetLoadingState() == 4)
			{
				while (mLoadingEntryPointIndex < num)
				{
					mEntryPoints[mLoadingEntryPointIndex] = SerializePointer(0, true, true);
					mLoadingEntryPointIndex++;
				}
				mNextEntryPointIdx = 0;
				mInternalObjects = null;
				SetLoadingState(6);
			}
			mStream = null;
		}

		public virtual void LoadFromQueue()
		{
			Library library = mLibrary;
			library.Open();
			LibraryStream libraryStream = library.mStream;
			if (GetLoadingState() == 1)
			{
				libraryStream.SetPosition(library.mFilePositions[mFilePositionIndex]);
			}
			SyncLoad(libraryStream);
		}

		public virtual object ReadObjectPointer(sbyte staticTypeID, bool dynamic, bool entryPoint)
		{
			short t = 0;
			t = SerializeIntrinsic(t);
			switch (t)
			{
			case 0:
				return ReadObject(staticTypeID, dynamic, entryPoint);
			case 1:
				return null;
			default:
			{
				t = (short)(t - 2);
				int num = mNumPackageDependencies;
				for (int i = 0; i < num; i++)
				{
					Package package = mPackageDependencies[i];
					if (package != null)
					{
						if (t < package.mNumEntryPoints)
						{
							return package.mEntryPoints[t];
						}
						t = (short)(t - package.mNumEntryPoints);
					}
				}
				if (t < mNumInternalObjects)
				{
					return mInternalObjects[t];
				}
				return null;
			}
			}
		}

		public virtual object ReadObject(sbyte staticTypeID, bool dynamicLoad, bool entryPoint)
		{
			sbyte t = staticTypeID;
			if (dynamicLoad)
			{
				t = SerializeIntrinsic(t);
			}
			int num = mNextObjectNumber++;
			return mInternalObjects[num] = ObjectReader.Read(this, t);
		}

		public virtual bool IsBeingUnloaded()
		{
			return mIsBeingUnloaded;
		}

		public virtual sbyte GetLoadingState()
		{
			return mLoadingState;
		}

		public virtual void SetLoadingState(sbyte loadingState)
		{
			mLoadingState = loadingState;
		}

		public virtual void SetNumEntryPoints(int numEntryPoints)
		{
			mNumEntryPoints = numEntryPoints;
			mEntryPoints = new object[numEntryPoints];
		}

		public virtual int ReadBufferAtOffset(sbyte[] buffer, int offset, int size)
		{
			return mStream.Read(buffer, offset, size);
		}

		public virtual void Load()
		{
			Load(false);
		}

		public static Package[] InstArrayPackage(int size)
		{
			Package[] array = new Package[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Package();
			}
			return array;
		}

		public static Package[][] InstArrayPackage(int size1, int size2)
		{
			Package[][] array = new Package[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Package[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Package();
				}
			}
			return array;
		}

		public static Package[][][] InstArrayPackage(int size1, int size2, int size3)
		{
			Package[][][] array = new Package[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Package[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Package[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Package();
					}
				}
			}
			return array;
		}
	}
}
