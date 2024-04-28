namespace ca.jamdat.tetrisrevolution
{
	public abstract class FileReader
	{
		public FileReader()
		{
		}

		public virtual void destruct()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void End()
		{
		}

		public abstract sbyte ReadByte();

		public abstract short ReadShort();

		public abstract int ReadLong();

		public abstract void ReadArrayShort(short[] a4, int a3);

		public abstract void ReadArrayByte(sbyte[] a6, int a5);
	}
}
