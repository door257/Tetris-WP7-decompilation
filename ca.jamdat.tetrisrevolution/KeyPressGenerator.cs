using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class KeyPressGenerator : BaseController
	{
		public const int keyPressSequenceNone = -1;

		public const int keyPressSequenceRandom = 0;

		public const int keyPressSequenceCount = 1;

		public Controllable mTimeControlled;

		public bool mActivated;

		public bool mKeyDown;

		public int mTimer;

		public int mCurrentSequence;

		public int mCurrentKeyIndex;

		public int[][] mKeyPressSequences;

		public int[] mAvailableKeys;

		public KeyPressGenerator()
		{
			mCurrentSequence = -1;
			mKeyPressSequences = RectangularArrays.ReturnRectangularIntArray(1, 128);
			mAvailableKeys = new int[6];
			int num = 0;
			mAvailableKeys[num++] = 1;
			mAvailableKeys[num++] = 2;
			mAvailableKeys[num++] = 3;
			mAvailableKeys[num++] = 4;
			mAvailableKeys[num++] = 5;
			mAvailableKeys[num++] = 6;
			mTimeControlled = new Controllable();
			mTimeControlled.SetController(this);
		}

		public override void destruct()
		{
			for (int i = 0; i < 1; i++)
			{
				mKeyPressSequences[i] = null;
			}
			mKeyPressSequences = null;
			mAvailableKeys = null;
			mTimeControlled.UnRegisterInGlobalTime();
			mTimeControlled = null;
		}

		public static KeyPressGenerator Get()
		{
			return GameApp.Get().GetKeyPressGenerator();
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			if (!mActivated)
			{
				return;
			}
			mTimer -= deltaTime;
			if (mTimer >= 0)
			{
				return;
			}
			Component currentFocus = GameApp.Get().GetCurrentFocus();
			int intParam = mKeyPressSequences[mCurrentSequence][mCurrentKeyIndex];
			if (mKeyDown)
			{
				currentFocus.SendMsg(currentFocus, -121, intParam);
				mKeyDown = false;
				mCurrentKeyIndex++;
			}
			else
			{
				currentFocus.SendMsg(currentFocus, -119, intParam);
				mKeyDown = true;
			}
			UpdateDelay();
			if (mCurrentKeyIndex >= 128)
			{
				if (mCurrentSequence == 0)
				{
					Initialize();
				}
				else
				{
					Deactivate();
				}
			}
		}

		public virtual void Activate(int seq)
		{
			mActivated = true;
			mCurrentSequence = seq;
			Initialize();
			mTimeControlled.RegisterInGlobalTime();
		}

		public virtual void Deactivate()
		{
			mActivated = false;
			mCurrentSequence = -1;
			mTimeControlled.UnRegisterInGlobalTime();
		}

		public virtual bool IsActive()
		{
			return mActivated;
		}

		public virtual void Initialize()
		{
			mCurrentKeyIndex = 0;
			UpdateDelay();
			RandomizeSequence();
		}

		public virtual void RandomizeSequence()
		{
			GameApp.InitializeRandomSeed();
			for (int i = 0; i < 128; i++)
			{
				int num = mAvailableKeys[FlMath.Random((short)1, (short)6) - 1];
				mKeyPressSequences[0][i] = num;
			}
		}

		public virtual void UpdateDelay()
		{
			mTimer = FlMath.Random((short)0, (short)100);
		}

		public static KeyPressGenerator[] InstArrayKeyPressGenerator(int size)
		{
			KeyPressGenerator[] array = new KeyPressGenerator[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new KeyPressGenerator();
			}
			return array;
		}

		public static KeyPressGenerator[][] InstArrayKeyPressGenerator(int size1, int size2)
		{
			KeyPressGenerator[][] array = new KeyPressGenerator[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyPressGenerator[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyPressGenerator();
				}
			}
			return array;
		}

		public static KeyPressGenerator[][][] InstArrayKeyPressGenerator(int size1, int size2, int size3)
		{
			KeyPressGenerator[][][] array = new KeyPressGenerator[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyPressGenerator[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyPressGenerator[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new KeyPressGenerator();
					}
				}
			}
			return array;
		}
	}
}
