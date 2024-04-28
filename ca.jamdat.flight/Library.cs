namespace ca.jamdat.flight
{
	public class Library
	{
		public const sbyte typeNumber = -1;

		public const sbyte typeID = sbyte.MaxValue;

		public const bool supportsDynamicSerialization = false;

		public FlString mFilename;

		public int[] mFilePositions;

		public short mNumFilePositions;

		public LibraryStream mStream;

		public bool mUserAllocatedStream;

		public int mLibrarySize;

		public Library(FlString filename)
		{
			mFilename = new FlString(filename);
			Open();
		}

		public static Library Cast(object o, Library _)
		{
			return (Library)o;
		}

		public virtual void OnSerialize(Package a6)
		{
		}

		public virtual void destruct()
		{
			mFilePositions = null;
			if (!mUserAllocatedStream)
			{
				mStream = null;
			}
		}

		public virtual Package NewPackage(int i)
		{
			Package package = new Package();
			package.mLibrary = this;
			package.mFilePositionIndex = (short)i;
			return package;
		}

		public virtual int GetPackageCount()
		{
			return mNumFilePositions;
		}

		public virtual void Init()
		{
			Package package = new Package();
			package.StartImmediateStreamReading(mStream);
			int t = 0;
			t = package.SerializeIntrinsic(t);
			mLibrarySize = 0;
			mLibrarySize = package.SerializeIntrinsic(mLibrarySize);
			short t2 = 0;
			t2 = (mNumFilePositions = package.SerializeIntrinsic(t2));
			mFilePositions = new int[t2 + 1];
			for (int i = 0; i < t2; i++)
			{
				int t3 = 0;
				t3 = package.SerializeIntrinsic(t3);
				mFilePositions[i] = t3;
			}
		}

		public virtual sbyte GetFileErrorState()
		{
			return 0;
		}

		public virtual sbyte GetLicenseState()
		{
			return 0;
		}

		public virtual void ManageLicense(sbyte action)
		{
		}

		public virtual void Close()
		{
			if (!mUserAllocatedStream)
			{
				if (mStream != null)
				{
					mStream.Close();
				}
				mStream = null;
			}
			else
			{
				mStream = null;
			}
		}

		public virtual bool IsValid()
		{
			if (mStream == null)
			{
				return false;
			}
			if (!mStream.IsValid())
			{
				return false;
			}
			return true;
		}

		public virtual void Open()
		{
			if (mStream == null)
			{
				mStream = new LibraryStream(mFilename);
				FirstTimeStreamInitialization();
			}
		}

		public virtual void FirstTimeStreamInitialization()
		{
			if (GetFileErrorState() == 0)
			{
				Init();
			}
		}
	}
}
