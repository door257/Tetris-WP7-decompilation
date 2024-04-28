namespace ca.jamdat.flight
{
	public class FlKeyManager
	{
		public bool mGameAltLock;

		public bool mFlightAltLock;

		public bool mShiftEnabled;

		public static FlKeyManager GetInstance()
		{
			FrameworkGlobals instance = FrameworkGlobals.GetInstance();
			if (instance.mFlKeyManager == null)
			{
				instance.mFlKeyManager = new FlKeyManager();
			}
			return instance.mFlKeyManager;
		}

		public static int ConstToFlightKey(int apiKey)
		{
			return 0;
		}

		public static int FlightKeyToUpper(int flightKey)
		{
			return flightKey;
		}

		public static int FlightKeyToLower(int flightKey)
		{
			return flightKey;
		}

		public static int AsciiToFlightKey(int ascii)
		{
			if (ascii >= 48 && ascii <= 57)
			{
				return ascii - 48 + 17;
			}
			switch (ascii)
			{
			case 8:
				return 10;
			case 35:
				return 15;
			case 42:
				return 16;
			default:
				return 0;
			}
		}

		public static int FlightKeyToAscii(int flightKey)
		{
			if (flightKey >= 17 && flightKey <= 26)
			{
				return flightKey - 17 + 48;
			}
			switch (flightKey)
			{
			case 10:
				return 8;
			case 15:
				return 35;
			case 16:
				return 42;
			default:
				return 0;
			}
		}

		public virtual bool GetKeyAltMode()
		{
			if (!mGameAltLock)
			{
				return mFlightAltLock;
			}
			return true;
		}

		public virtual bool GetKeyShiftMode()
		{
			return mShiftEnabled;
		}

		public virtual void SetKeyAltMode(bool activate)
		{
			mFlightAltLock = activate;
		}

		public virtual void SetKeyShiftMode(bool activate)
		{
			mShiftEnabled = activate;
		}

		public virtual bool GetGameKeyAltMode()
		{
			return mGameAltLock;
		}

		public virtual void SetGameKeyAltMode(bool active)
		{
			mGameAltLock = active;
		}

		public virtual void destruct()
		{
		}

		public FlKeyManager()
		{
			mGameAltLock = true;
			FlApplication.GetInstance().MapKey(7, 5);
			FlApplication.GetInstance().MapKey(13, 5);
			FlApplication.GetInstance().MapKey(9, 6);
			FlApplication.GetInstance().MapKey(10, 6);
			FlApplication.GetInstance().MapKey(14, 6);
		}

		public static FlKeyManager[] InstArrayFlKeyManager(int size)
		{
			FlKeyManager[] array = new FlKeyManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlKeyManager();
			}
			return array;
		}

		public static FlKeyManager[][] InstArrayFlKeyManager(int size1, int size2)
		{
			FlKeyManager[][] array = new FlKeyManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlKeyManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlKeyManager();
				}
			}
			return array;
		}

		public static FlKeyManager[][][] InstArrayFlKeyManager(int size1, int size2, int size3)
		{
			FlKeyManager[][][] array = new FlKeyManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlKeyManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlKeyManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlKeyManager();
					}
				}
			}
			return array;
		}
	}
}
