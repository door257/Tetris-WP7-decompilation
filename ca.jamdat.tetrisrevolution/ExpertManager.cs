namespace ca.jamdat.tetrisrevolution
{
	public class ExpertManager
	{
		public const sbyte progressionExpert = 0;

		public const sbyte featsExpert = 1;

		public const sbyte expertCount = 2;

		public Expert[] mExpertArray;

		public ExpertManager()
		{
			mExpertArray = new Expert[2];
			mExpertArray[0] = new ProgressionExpert();
			mExpertArray[1] = new FeatsExpert();
		}

		public virtual void destruct()
		{
			if (mExpertArray != null)
			{
				for (int i = 0; i < 2; i++)
				{
					mExpertArray[i] = null;
				}
			}
			mExpertArray = null;
		}

		public virtual void Update(GameStatistics gameStatistics, CareerStatistics careerStatistics)
		{
			for (int i = 0; i < 2; i++)
			{
				mExpertArray[i].Update(gameStatistics, careerStatistics);
			}
		}

		public virtual void Reset()
		{
			for (int i = 0; i < 2; i++)
			{
				mExpertArray[i].Reset();
			}
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
			if (fileStream.HasValidData())
			{
				for (int i = 0; i < 2; i++)
				{
					mExpertArray[i].Read(fileStream);
				}
			}
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
			for (int i = 0; i < 2; i++)
			{
				mExpertArray[i].Write(fileStream);
			}
			fileStream.SetValidDataFlag(true);
		}

		public virtual Expert GetExpert(sbyte id)
		{
			return mExpertArray[id];
		}

		public static ExpertManager[] InstArrayExpertManager(int size)
		{
			ExpertManager[] array = new ExpertManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ExpertManager();
			}
			return array;
		}

		public static ExpertManager[][] InstArrayExpertManager(int size1, int size2)
		{
			ExpertManager[][] array = new ExpertManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ExpertManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ExpertManager();
				}
			}
			return array;
		}

		public static ExpertManager[][][] InstArrayExpertManager(int size1, int size2, int size3)
		{
			ExpertManager[][][] array = new ExpertManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ExpertManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ExpertManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ExpertManager();
					}
				}
			}
			return array;
		}
	}
}
