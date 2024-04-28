namespace ca.jamdat.tetrisrevolution
{
	public abstract class PenMsgReceiver
	{
		public PenMsgReceiver()
		{
		}

		public virtual void destruct()
		{
		}

		public abstract void OnPenDown(short a4, short a3, sbyte a2);

		public abstract void OnPenUp(short a7, short a6, sbyte a5);

		public abstract void OnPenDrag(short a10, short a9, sbyte a8);

		public abstract void OnPenDragRepeat(short a13, short a12, sbyte a11);
	}
}
