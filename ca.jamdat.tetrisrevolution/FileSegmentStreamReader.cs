namespace ca.jamdat.tetrisrevolution
{
	public class FileSegmentStreamReader : FileReader
	{
		public FileSegmentStream mFile;

		public FileSegmentStreamReader(FileSegmentStream file)
		{
			mFile = file;
		}

		public override void destruct()
		{
		}

		public override sbyte ReadByte()
		{
			return mFile.ReadByte();
		}

		public override short ReadShort()
		{
			return mFile.ReadShort();
		}

		public override int ReadLong()
		{
			return mFile.ReadLong();
		}

		public override void ReadArrayShort(short[] shortArray, int length)
		{
			mFile.ReadShortArray(shortArray, length);
		}

		public override void ReadArrayByte(sbyte[] byteArray, int length)
		{
			mFile.ReadByteArray(byteArray, length);
		}
	}
}
