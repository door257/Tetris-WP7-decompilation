namespace ca.jamdat.flight
{
	public abstract class Stream
	{
		public Stream()
		{
		}

		public virtual void destruct()
		{
		}

		public abstract void Skip(int a3);

		public abstract int Read(sbyte[] a5, int a4);

		public abstract int Write(sbyte[] a7, int a6);

		public virtual sbyte ReadByte()
		{
			return 0;
		}

		public virtual short ReadShort()
		{
			return 0;
		}

		public virtual int ReadLong()
		{
			return 0;
		}

		public virtual float ReadReal()
		{
			return 0f;
		}

		public virtual FlString ReadString()
		{
			return new FlString(this);
		}

		public virtual void WriteByte(sbyte data)
		{
		}

		public virtual void WriteShort(short data)
		{
		}

		public virtual void WriteLong(int data)
		{
		}

		public virtual void WriteReal(float data)
		{
		}

		public virtual void WriteString(FlString data)
		{
			data.Write(this);
		}

		public virtual void WriteText(FlString data)
		{
			data.Write(this, false);
		}

		public abstract int GetPosition();

		public abstract void SetPosition(int a8);

		public virtual bool IsValid()
		{
			return true;
		}

		public virtual void Close()
		{
		}

		public virtual void SetBufferSize(int a3)
		{
		}
	}
}
