namespace ca.jamdat.tetrisrevolution
{
	public abstract class Expert
	{
		public Expert()
		{
		}

		public virtual void destruct()
		{
		}

		public abstract void Update(GameStatistics a3, CareerStatistics a2);

		public abstract void Reset();

		public abstract void Read(FileSegmentStream a4);

		public abstract void Write(FileSegmentStream a5);
	}
}
