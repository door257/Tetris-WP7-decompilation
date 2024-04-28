using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FileSegmentStream
	{
		public const int modeRead = 0;

		public const int modeWrite = 1;

		public int mByteArrayCapacity;

		public sbyte[] mByteArray;

		public int mPosition;

		public bool mValidData;

		public bool mModified;

		public int mMode;

		public FileSegmentStream(int capacityInBytes)
		{
			mByteArrayCapacity = capacityInBytes;
			mMode = 0;
			mByteArray = new sbyte[capacityInBytes];
		}

		public virtual void destruct()
		{
			mByteArray = null;
		}

		public virtual void SetValidDataFlag(bool valid)
		{
			mModified = mValidData != valid || mModified;
			mValidData = valid;
		}

		public virtual bool HasValidData()
		{
			return mValidData;
		}

		public virtual bool IsModified()
		{
			return mModified;
		}

		public virtual void WriteByte(sbyte value)
		{
			int num = mPosition;
			if (mByteArray[num] != value)
			{
				mByteArray[num] = value;
				mModified = true;
			}
			mPosition = num + 1;
		}

		public virtual void WriteBoolean(bool value)
		{
			if (value)
			{
				WriteByte(1);
			}
			else
			{
				WriteByte(0);
			}
		}

		public virtual void WriteShort(short value)
		{
			WriteByte((sbyte)(value >> 8));
			WriteByte((sbyte)(value & 0xFF));
		}

		public virtual void WriteLong(int value)
		{
			WriteShort((short)(value >> 16));
			WriteShort((short)(value & 0xFFFF));
		}

		public virtual void WritePaddedString(FlString s, int maxLength)
		{
			int length = s.GetLength();
			int num = maxLength - length;
			FlString flString = new FlString(s);
			for (int i = 0; i < num; i++)
			{
				flString.AddAssign(StringUtils.CreateString("0"));
			}
			WriteLong(length);
			WriteString(flString);
		}

		public virtual void WriteF32(F32 fp)
		{
			WriteLong(fp.ToFixedPoint(16));
		}

		public virtual void WriteFVec3(FVec3T_F32 vec3)
		{
			WriteF32(vec3.x);
			WriteF32(vec3.y);
			WriteF32(vec3.z);
		}

		public virtual void WriteByteArray(sbyte[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteByte(values[i]);
			}
		}

		public virtual void WriteBooleanArray(bool[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteBoolean(values[i]);
			}
		}

		public virtual void WriteShortArray(short[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteShort(values[i]);
			}
		}

		public virtual void WriteLongArray(int[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteLong(values[i]);
			}
		}

		public virtual void WriteF32Array(F32[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteF32(values[i]);
			}
		}

		public virtual void WriteFVec3Array(FVec3T_F32[] values, int count)
		{
			for (int i = 0; i < count; i++)
			{
				WriteFVec3(values[i]);
			}
		}

		public virtual sbyte ReadByte()
		{
			return mByteArray[mPosition++];
		}

		public virtual bool ReadBoolean()
		{
			return ReadByte() == 1;
		}

		public virtual short ReadShort()
		{
			sbyte b = ReadByte();
			sbyte b2 = ReadByte();
			int num = (b << 8) | (b2 & 0xFF);
			return (short)num;
		}

		public virtual int ReadLong()
		{
			short num = ReadShort();
			short num2 = ReadShort();
			return (num << 16) | (num2 & 0xFFFF);
		}

		public virtual FlString ReadPaddedString()
		{
			int length = ReadLong();
			FlString flString = new FlString();
			ReadString(flString);
			return flString.Substring(0, length);
		}

		public virtual F32 ReadF32()
		{
			return new F32(ReadLong(), 16);
		}

		public virtual FVec3T_F32 ReadFVec3()
		{
			F32 i = new F32(ReadF32());
			F32 j = new F32(ReadF32());
			F32 k = new F32(ReadF32());
			return new FVec3T_F32(i, j, k);
		}

		public virtual void ReadByteArray(sbyte[] outValues, int count)
		{
			for (int i = 0; i < count; i++)
			{
				outValues[i] = ReadByte();
			}
		}

		public virtual void ReadBooleanArray(bool[] outValues, int count)
		{
			for (int i = 0; i < count; i++)
			{
				outValues[i] = ReadBoolean();
			}
		}

		public virtual void ReadShortArray(short[] outValues, int count)
		{
			for (int i = 0; i < count; i++)
			{
				outValues[i] = ReadShort();
			}
		}

		public virtual void ReadLongArray(int[] outValues, int count)
		{
			for (int i = 0; i < count; i++)
			{
				outValues[i] = ReadLong();
			}
		}

		public virtual void ReadF32Array(F32[] outValues, int count)
		{
			for (int i = 0; i < count; i++)
			{
				outValues[i] = ReadF32();
			}
		}

		public virtual void ReadFVec3Array(FVec3T_F32[] outValues, int count)
		{
			ReadLong();
			for (int i = 0; i < count; i++)
			{
				outValues[i].Assign(ReadFVec3());
			}
		}

		public static void Test()
		{
		}

		public virtual void ForceModifiedFlag()
		{
			mModified = true;
		}

		public virtual void SetMode(int ioMode)
		{
			mMode = ioMode;
			SetPosition(0);
		}

		public virtual void SetPosition(int position)
		{
			mPosition = position;
		}

		public virtual int GetPosition()
		{
			return mPosition;
		}

		public virtual int GetSize()
		{
			return 4 + GetCapacity();
		}

		public virtual int GetCapacity()
		{
			return mByteArrayCapacity;
		}

		public virtual void Read(FileStream stream)
		{
			SetMode(0);
			SetValidDataFlag(stream.ReadLong() == 1);
			stream.Read(mByteArray, GetCapacity());
			mModified = false;
		}

		public virtual void Write(FileStream stream)
		{
			SetMode(1);
			stream.WriteLong(HasValidData() ? 1 : 0);
			stream.Write(mByteArray, GetCapacity());
			mModified = false;
		}

		public virtual void ReadString(FlString sRet)
		{
			int num = ReadLong();
			for (int i = 0; i < num; i++)
			{
				sRet.AddAssign(FlString.FromChar((sbyte)ReadShort()));
			}
		}

		public virtual void WriteString(FlString s)
		{
			int length = s.GetLength();
			WriteLong(length);
			for (int i = 0; i < length; i++)
			{
				WriteShort(s.GetCharAt(i));
			}
		}
	}
}
