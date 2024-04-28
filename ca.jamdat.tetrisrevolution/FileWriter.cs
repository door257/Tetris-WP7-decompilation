namespace ca.jamdat.tetrisrevolution
{
	public abstract class FileWriter
	{
		public FileWriter()
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

		public abstract void WriteByte(sbyte a3);

		public abstract void WriteShort(short a4);

		public abstract void WriteLong(int a5);

		public abstract void WriteArrayShort(short[] a7, int a6);

		public abstract void WriteArrayByte(sbyte[] a9, int a8);
	}
}
