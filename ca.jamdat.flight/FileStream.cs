using System;

namespace ca.jamdat.flight
{
	public class FileStream : Stream
	{
		public FlString mFileName;

		public sbyte mFileMode;

		public int mNativeFileSize;

		public bool mIsCRCed;

		public bool mIsCRCValid;

		public int mBufferSize;

		public sbyte[] mBuffer;

		public int mDataEndPos;

		public int mCurrentPos;

		public sbyte[] mTmpArray = new sbyte[4];

		public FileStream(FlString filename, sbyte filemode, int bufferSize, bool isCRCed)
		{
			mFileMode = filemode;
			mNativeFileSize = -1;
			mIsCRCed = isCRCed;
			mFileName = new FlString(filename);
			switch (mFileMode)
			{
			case 0:
				InitReadMode(bufferSize);
				break;
			case 1:
			case 2:
			{
				int headerSize = GetHeaderSize();
				mBufferSize = bufferSize + headerSize;
				mBuffer = new sbyte[mBufferSize];
				mNativeFileSize = headerSize;
				mCurrentPos = headerSize;
				mDataEndPos = headerSize;
				mIsCRCValid = true;
				break;
			}
			}
		}

		public FileStream(FlString filename)
			: this(filename, 0)
		{
		}

		public FileStream(FlString filename, sbyte filemode)
			: this(filename, filemode, 1024)
		{
		}

		public FileStream(FlString filename, sbyte filemode, int bufferSize)
			: this(filename, filemode, bufferSize, true)
		{
		}

		public static bool FileExists(FlString filename)
		{
			return JavaBasicFileStream.FileExists(filename);
		}

		public static bool IsSpaceAvailable(int size)
		{
			return true;
		}

		public static bool FileDelete(FlString filename)
		{
			return JavaBasicFileStream.FileDelete(filename);
		}

		public override void destruct()
		{
		}

		public override int Read(sbyte[] buffer, int size)
		{
			Array.Copy(mBuffer, mCurrentPos, buffer, 0, size);
			mCurrentPos += size;
			return size;
		}

		public override int Write(sbyte[] buffer, int size)
		{
			Array.Copy(buffer, 0, mBuffer, mCurrentPos, size);
			mCurrentPos += size;
			if (mCurrentPos > mDataEndPos)
			{
				mDataEndPos = mCurrentPos;
				mNativeFileSize = mCurrentPos;
			}
			return size;
		}

		public override void Skip(int size)
		{
			SetPosition(GetPosition() + size);
		}

		public virtual int GetSize()
		{
			return mNativeFileSize - GetHeaderSize();
		}

		public override int GetPosition()
		{
			return mCurrentPos - GetHeaderSize();
		}

		public override void SetPosition(int offset)
		{
			mCurrentPos = GetHeaderSize() + offset;
		}

		public virtual bool IsEndOfFile()
		{
			return mCurrentPos >= mDataEndPos;
		}

		public override bool IsValid()
		{
			return mIsCRCValid;
		}

		public virtual bool IsOpen()
		{
			return mBuffer != null;
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

		public override void Close()
		{
			if (mFileMode != 0)
			{
				AssignCRC();
				JavaBasicFileStream.WriteFile(mFileName, mBuffer, mDataEndPos, mFileMode);
			}
			mBuffer = null;
			mFileName = null;
		}

		public override sbyte ReadByte()
		{
			return mBuffer[mCurrentPos++];
		}

		public override short ReadShort()
		{
			short result = (short)(((mBuffer[mCurrentPos] & 0xFF) << 8) + (mBuffer[mCurrentPos + 1] & 0xFF));
			mCurrentPos += 2;
			return result;
		}

		public override int ReadLong()
		{
			sbyte[] array = mBuffer;
			int num = mCurrentPos;
			int result = ((array[num] & 0xFF) << 24) | ((array[num + 1] & 0xFF) << 16) | ((array[num + 2] & 0xFF) << 8) | (array[num + 3] & 0xFF);
			mCurrentPos += 4;
			return result;
		}

		public override FlString ReadString()
		{
			short num = ReadShort();
			sbyte[] array = new sbyte[num + 1];
			if (num != 0)
			{
				Read(array, num);
			}
			array[num] = 0;
			FlString result = new FlString(array);
			array = null;
			return result;
		}

		public override void WriteByte(sbyte data)
		{
			mTmpArray[0] = data;
			Write(mTmpArray, 1);
		}

		public override void WriteShort(short data)
		{
			mTmpArray[0] = (sbyte)(0xFF & (data >> 8));
			mTmpArray[1] = (sbyte)(0xFF & data);
			Write(mTmpArray, 2);
		}

		public override void WriteLong(int data)
		{
			sbyte[] array = mTmpArray;
			array[0] = (sbyte)(0xFF & (data >> 24));
			array[1] = (sbyte)(0xFF & (data >> 16));
			array[2] = (sbyte)(0xFF & (data >> 8));
			array[3] = (sbyte)(0xFF & data);
			Write(array, 4);
		}

		public override void WriteString(FlString data)
		{
			data.Write(this);
		}

		public override void WriteText(FlString data)
		{
			data.Write(this, false);
		}

		public virtual bool CanRead()
		{
			return mFileMode == 0;
		}

		public virtual bool CanWrite()
		{
			return mFileMode != 0;
		}

		public virtual FlString ReadLine()
		{
			sbyte b = -1;
			sbyte b2 = -1;
			sbyte[] array = new sbyte[2048];
			int num = 0;
			b = ReadByte();
			if (b != 10)
			{
				while (!IsEndOfFile() && num < 2046)
				{
					b2 = ReadByte();
					if (b2 == 10)
					{
						if (b != 13)
						{
							array[num++] = b;
						}
						array[num++] = 0;
						return StringUtils.CreateString(array);
					}
					array[num++] = b;
					b = b2;
				}
				array[num++] = b;
			}
			array[num++] = 0;
			return StringUtils.CreateString(array);
		}

		public virtual void InitReadMode(int bufferSize)
		{
			GetNativeFileSize();
			mBufferSize = mNativeFileSize;
			if (mBufferSize > 0)
			{
				mBuffer = new sbyte[mBufferSize];
			}
			if (mBuffer == null || RefreshBuffer(0) == -1)
			{
				mIsCRCValid = false;
			}
			else
			{
				mIsCRCValid = VerifyCRC();
			}
		}

		public virtual bool VerifyCRC()
		{
			if (mIsCRCed)
			{
				if (mNativeFileSize > GetHeaderSize())
				{
					int num = (int)Memory.CalculateCRC(mBuffer, GetHeaderSize(), mDataEndPos - GetHeaderSize());
					mCurrentPos = 0;
					int num2 = ReadLong();
					mCurrentPos = GetHeaderSize();
					return num == num2;
				}
				return false;
			}
			return true;
		}

		public virtual void AssignCRC()
		{
			if (mIsCRCed)
			{
				int data = (int)Memory.CalculateCRC(mBuffer, GetHeaderSize(), mDataEndPos - GetHeaderSize());
				mCurrentPos = 0;
				WriteLong(data);
			}
		}

		public virtual int RefreshBuffer(int startPositionInFile)
		{
			int num = 0;
			mBuffer = JavaBasicFileStream.ReadFile(mFileName);
			if (mBuffer == null)
			{
				return -1;
			}
			mBufferSize = mBuffer.Length;
			mNativeFileSize = mBufferSize;
			num = (mDataEndPos = mBufferSize);
			mCurrentPos = GetHeaderSize();
			return num;
		}

		public virtual int GetNativeFileSize()
		{
			if (mNativeFileSize < 0)
			{
				int num = 0;
				num = JavaBasicFileStream.GetFileSize(mFileName);
				if (num > 0)
				{
					mNativeFileSize = num;
				}
			}
			return mNativeFileSize;
		}

		public virtual int GetCRCSize()
		{
			if (!mIsCRCed)
			{
				return 0;
			}
			return 4;
		}

		public virtual int GetHeaderSize()
		{
			return GetCRCSize();
		}

		public virtual void InitReadMode()
		{
			InitReadMode(1024);
		}
	}
}
