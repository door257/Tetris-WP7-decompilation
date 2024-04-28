using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FileHandler
	{
		public const int opRead = 0;

		public const int opWrite = 1;

		public FlString mPathname;

		public int mSegmentCount;

		public int mWritingCount;

		public FileSegmentStream[] mSegmentStreams;

		public FileHandler(FlString pathname, FileSegmentStream[] streams, int segmentCount)
		{
			mPathname = new FlString(pathname);
			mSegmentCount = segmentCount;
			mSegmentStreams = streams;
		}

		public virtual void destruct()
		{
			for (int i = 0; i < mSegmentCount; i++)
			{
				mSegmentStreams[i] = null;
			}
			mSegmentStreams = null;
			mPathname = null;
		}

		public virtual void Terminate()
		{
		}

		public virtual void SetPathname(FlString pathname)
		{
			mPathname.Assign(pathname);
		}

		public virtual FlString GetPathname()
		{
			return mPathname;
		}

		public virtual int GetSize()
		{
			int num = 8;
			for (int i = 0; i < mSegmentCount; i++)
			{
				num += mSegmentStreams[i].GetSize();
			}
			return num;
		}

		public virtual int GetWritingCount()
		{
			return mWritingCount;
		}

		public virtual bool OnSerialize(int op)
		{
			bool flag = false;
			if (op == 0)
			{
				return OnReadSync();
			}
			return OnWriteSync();
		}

		public virtual bool IsValid()
		{
			FileStream fileStream = new FileStream(GetPathname(), 0, GetSize());
			bool result = fileStream.IsValid();
			fileStream.Close();
			return result;
		}

		public virtual FileSegmentStream GetSegmentStream(int index, int mode)
		{
			FileSegmentStream fileSegmentStream = mSegmentStreams[index];
			fileSegmentStream.SetMode(mode);
			return fileSegmentStream;
		}

		public virtual void ReadFromFileStream(FileStream fs)
		{
			ResetSerializableData();
			if (!fs.IsValid())
			{
				return;
			}
			int num = fs.ReadLong();
			if (num == 1610698210)
			{
				mWritingCount = fs.ReadLong();
				for (int i = 0; i < mSegmentCount; i++)
				{
					mSegmentStreams[i].Read(fs);
				}
			}
		}

		public virtual void WriteToFileStream(FileStream fs)
		{
			fs.WriteLong(1610698210);
			fs.WriteLong(++mWritingCount);
			for (int i = 0; i < mSegmentCount; i++)
			{
				mSegmentStreams[i].Write(fs);
			}
		}

		public virtual void ResetSerializableData()
		{
			for (int i = 0; i < mSegmentCount; i++)
			{
				mSegmentStreams[i].SetValidDataFlag(false);
			}
		}

		public virtual bool AreSegmentsModified()
		{
			for (int i = 0; i < mSegmentCount; i++)
			{
				if (mSegmentStreams[i].IsModified())
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool OnReadSync()
		{
			if (FileStream.FileExists(GetPathname()))
			{
				FileStream fileStream = new FileStream(GetPathname(), 0, GetSize());
				ReadFromFileStream(fileStream);
				fileStream.Close();
			}
			return true;
		}

		public virtual bool OnWriteSync()
		{
			if (AreSegmentsModified())
			{
				FileStream fileStream = new FileStream(GetPathname(), 1, GetSize());
				WriteToFileStream(fileStream);
				fileStream.Close();
			}
			return true;
		}
	}
}
