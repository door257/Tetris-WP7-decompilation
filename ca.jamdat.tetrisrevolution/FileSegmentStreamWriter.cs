namespace ca.jamdat.tetrisrevolution
{
	public class FileSegmentStreamWriter : FileWriter
	{
		public FileSegmentStream mFile;

		public FileSegmentStreamWriter(FileSegmentStream file)
		{
			mFile = file;
		}

		public override void destruct()
		{
		}

		public override void WriteByte(sbyte byteToWrite)
		{
			mFile.WriteByte(byteToWrite);
		}

		public override void WriteShort(short shortToWrite)
		{
			mFile.WriteShort(shortToWrite);
		}

		public override void WriteLong(int longToWrite)
		{
			mFile.WriteLong(longToWrite);
		}

		public override void WriteArrayShort(short[] arrayToWrite, int length)
		{
			mFile.WriteShortArray(arrayToWrite, length);
		}

		public override void WriteArrayByte(sbyte[] arrayToWrite, int length)
		{
			mFile.WriteByteArray(arrayToWrite, length);
		}
	}
}
