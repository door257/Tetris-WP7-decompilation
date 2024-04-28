using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace ca.jamdat.flight
{
	public class LibraryStream : Stream
	{
		public const sbyte kSizeOfReadByte = 1;

		public const sbyte kSizeOfReadInt = 4;

		public const sbyte kSizeOfReadChar = 2;

		public const sbyte kSizeOfReadShort = 2;

		public const sbyte kEofValue = -1;

		public System.IO.Stream mInputStream;

		public FlString mFileName;

		public int mError;

		public int mSegmentSize;

		public int mLibrarySize;

		public int mCurrentSegment;

		public int mPositionInSegment;

		public int mSizeOfCurrentSegment;

		public int mCurrentDecompressionSegIndex;

		public sbyte[] mBuffer = new sbyte[4];

		public LibraryStream(FlString fileName)
		{
			mFileName = new FlString();
			Create(fileName);
		}

		public override void destruct()
		{
			Close();
		}

		public static bool FileExists(FlString fileName)
		{
			bool flag = false;
			System.IO.Stream resourceAsStream = GetResourceAsStream(fileName.NativeString);
			if (resourceAsStream == null)
			{
				flag = false;
			}
			else
			{
				flag = true;
				try
				{
					resourceAsStream.Close();
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
			}
			return flag;
		}

		public static System.IO.Stream GetResourceAsStream(string filename)
		{
			try
			{
				return TitleContainer.OpenStream(filename);
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
			return null;
		}

		public static sbyte[] GetResourceAsByteArray(string resourceName)
		{
			System.IO.Stream resourceAsStream = GetResourceAsStream("/" + resourceName);
			Array_byte array_byte = new Array_byte();
			int num;
			try
			{
				num = resourceAsStream.ReadByte();
			}
			catch (IOException exception)
			{
				FlLog.Log(exception);
				num = -1;
			}
			while (num != -1)
			{
				array_byte.Insert((sbyte)num);
				try
				{
					num = resourceAsStream.ReadByte();
				}
				catch (Exception exception2)
				{
					FlLog.Log(exception2);
				}
			}
			sbyte[] array = new sbyte[array_byte.End()];
			array_byte.CopyInto(array, array_byte.End());
			return array;
		}

		public override void Skip(int size)
		{
			SetPosition(GetPosition() + size);
		}

		public virtual int Read(sbyte[] buffer, int offset, int size)
		{
			int num = size;
			int num2 = offset + size;
			byte[] array = new byte[buffer.Length];
			array = (byte[])(object)buffer;
			do
			{
				int num3 = mSizeOfCurrentSegment - mPositionInSegment;
				if (num3 <= 0)
				{
					CloseSegment();
					mCurrentSegment++;
					OpenSegment(mCurrentSegment);
					num3 = mSizeOfCurrentSegment;
				}
				int num4 = FlMath.Minimum(num, num3);
				try
				{
					int num5 = 0;
					while (true)
					{
						num5 += mInputStream.Read(array, num2 - num + num5, num4 - num5);
						if (num5 != num4)
						{
							if (num5 < 0)
							{
								throw new IOException();
							}
							continue;
						}
						break;
					}
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
				mPositionInSegment += num4;
				num -= num4;
			}
			while (num > 0);
			return size;
		}

		public override int Read(sbyte[] buffer, int size)
		{
			return Read(buffer, GetPosition(), size);
		}

		public override int Write(sbyte[] buffer, int size)
		{
			return 0;
		}

		public override int ReadLong()
		{
			sbyte[] array = mBuffer;
			Read(array, 0, 4);
			return ((array[0] & 0xFF) << 24) | ((array[1] & 0xFF) << 16) | ((array[2] & 0xFF) << 8) | (array[3] & 0xFF);
		}

		public static int ReadLong(sbyte[] data, int index)
		{
			return ((data[index] & 0xFF) << 24) | ((data[index + 1] & 0xFF) << 16) | ((data[index + 2] & 0xFF) << 8) | (data[index + 3] & 0xFF);
		}

		public override short ReadShort()
		{
			sbyte[] array = mBuffer;
			Read(array, 0, 2);
			return (short)((array[0] << 8) | (array[1] & 0xFF));
		}

		public virtual sbyte ReadChar()
		{
			return ReadByte();
		}

		public override sbyte ReadByte()
		{
			if (mPositionInSegment >= mSizeOfCurrentSegment)
			{
				CloseSegment();
				mCurrentSegment++;
				OpenSegment(mCurrentSegment);
			}
			sbyte[] array = mBuffer;
			byte[] array2 = new byte[array.Length];
			array2 = (byte[])(object)array;
			try
			{
				while (true)
				{
					int num = mInputStream.Read(array2, 0, 1);
					if (num < 1)
					{
						if (num < 0)
						{
							throw new IOException();
						}
						continue;
					}
					break;
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
			mPositionInSegment++;
			return array[0];
		}

		public virtual int GetSize()
		{
			return mLibrarySize;
		}

		public override void SetPosition(int offset)
		{
			int size = GetSize();
			int num = 0;
			int num2 = mSegmentSize;
			if (offset >= size)
			{
				num = (size - 1) / num2;
				offset = size - num * num2;
			}
			else
			{
				num = offset / num2;
				offset %= num2;
			}
			if (num != mCurrentSegment)
			{
				mCurrentSegment = num;
				CloseSegment();
				OpenSegment(mCurrentSegment);
			}
			int num3 = 0;
			try
			{
				num3 = (int)mInputStream.Seek(offset, SeekOrigin.Begin);
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				mError = 1;
			}
			mPositionInSegment = num3;
		}

		public override int GetPosition()
		{
			return mPositionInSegment + mCurrentSegment * mSegmentSize;
		}

		public virtual bool IsEndOfFile()
		{
			return GetPosition() == GetSize();
		}

		public virtual bool IsOpen()
		{
			return mInputStream != null;
		}

		public override bool IsValid()
		{
			if (mInputStream == null)
			{
				return false;
			}
			return true;
		}

		public virtual int GetLastError()
		{
			return mError;
		}

		public override void Close()
		{
			CloseSegment();
		}

		public virtual void Create(FlString fileName)
		{
			mFileName.Assign(fileName.Add(FlString.FromChar(46)));
			OpenSegment(0);
			if (IsOpen())
			{
				int num = 0;
				int num2 = 0;
				mLibrarySize = 4096;
				mSizeOfCurrentSegment = mLibrarySize;
				num = ReadLong();
				num2 = ReadLong();
				mPositionInSegment += 8;
				if (num == 0 || num2 < num)
				{
					num = num2;
				}
				mSizeOfCurrentSegment = num;
				mLibrarySize = num2;
				mSegmentSize = num;
				SetPosition(0);
			}
		}

		public virtual void CloseSegment()
		{
			try
			{
				mInputStream.Close();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				mError = 1;
			}
			mInputStream = null;
		}

		public virtual void OpenSegment(int segmentIndex)
		{
			int num = mSegmentSize;
			if (num > 0)
			{
				int num2 = mLibrarySize / num;
				mSizeOfCurrentSegment = ((segmentIndex < num2) ? num : (mLibrarySize - num2 * num));
			}
			mPositionInSegment = 0;
			System.IO.Stream inputStreamOnSegment = GetInputStreamOnSegment(segmentIndex);
			if (inputStreamOnSegment == null)
			{
				mInputStream = null;
				mError = 1;
			}
			else
			{
				mInputStream = inputStreamOnSegment;
				mError = 0;
			}
		}

		public virtual System.IO.Stream GetInputStreamOnSegment(int segmentIndex)
		{
			string nativeString = mFileName.Add(new FlString(segmentIndex)).NativeString;
			return GetResourceAsStream(nativeString);
		}
	}
}
