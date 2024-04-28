namespace ca.jamdat.tetrisrevolution
{
	public abstract class HelixObserver
	{
		public const sbyte forward = 0;

		public const sbyte backward = 1;

		public HelixObserver()
		{
		}

		public virtual void destruct()
		{
		}

		public abstract void PassThroughVariant(int a5, sbyte a4, int a3);

		public abstract void OnTarget(int a6);

		public abstract void LeaveTarget();
	}
}
