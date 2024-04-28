using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class BlobReader : FileReader
	{
		public Blob mBlob;

		public int mDataIndex;

		public BlobReader(Blob blobFile)
		{
			mBlob = blobFile;
			mDataIndex = 0;
		}

		public override void destruct()
		{
		}

		public override void Start()
		{
			mDataIndex = 0;
		}

		public override sbyte ReadByte()
		{
			sbyte[] data = mBlob.GetData();
			return data[mDataIndex++];
		}

		public override short ReadShort()
		{
			sbyte b = ReadByte();
			sbyte b2 = ReadByte();
			int num = (b2 << 8) | (b & 0xFF);
			return (short)num;
		}

		public override int ReadLong()
		{
			short num = ReadShort();
			short num2 = ReadShort();
			return (num2 << 16) | (num & 0xFFFF);
		}

		public override void ReadArrayShort(short[] shortArray, int length)
		{
			for (int i = 0; i < length; i++)
			{
				shortArray[i] = ReadShort();
			}
		}

		public override void ReadArrayByte(sbyte[] byteArray, int length)
		{
			for (int i = 0; i < length; i++)
			{
				byteArray[i] = ReadByte();
			}
		}
	}
}
