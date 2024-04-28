namespace ca.jamdat.tetrisrevolution
{
	public class Cheat
	{
		public int mId;

		public int[] mCode;

		public int mCodeLength;

		public int mParamLength;

		public bool mActivated;

		public Cheat()
		{
			mId = 26;
		}

		public virtual void destruct()
		{
			mCode = null;
		}

		public virtual void Initialize(int id, int[] code, int codeLength, int paramLength)
		{
			mId = id;
			mCode = new int[codeLength];
			mCodeLength = codeLength;
			mParamLength = paramLength;
			for (int i = 0; i < codeLength; i++)
			{
				mCode[i] = code[i];
			}
		}

		public virtual int GetId()
		{
			return mId;
		}

		public virtual void Activate(int param)
		{
			mActivated = true;
		}

		public virtual void Deactivate()
		{
			mActivated = false;
		}

		public virtual bool IsActivated()
		{
			return mActivated;
		}

		public virtual int[] GetCode()
		{
			return mCode;
		}

		public virtual int GetCodeLength()
		{
			return mCodeLength;
		}

		public virtual int GetParamLength()
		{
			return mParamLength;
		}

		public static Cheat[] InstArrayCheat(int size)
		{
			Cheat[] array = new Cheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Cheat();
			}
			return array;
		}

		public static Cheat[][] InstArrayCheat(int size1, int size2)
		{
			Cheat[][] array = new Cheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Cheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Cheat();
				}
			}
			return array;
		}

		public static Cheat[][][] InstArrayCheat(int size1, int size2, int size3)
		{
			Cheat[][][] array = new Cheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Cheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Cheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Cheat();
					}
				}
			}
			return array;
		}
	}
}
