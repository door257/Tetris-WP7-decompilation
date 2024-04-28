using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TouchGSReceiver : TouchReceiver
	{
		public const sbyte kHold = 0;

		public const sbyte kLeft = 1;

		public const sbyte kRight = 2;

		public const sbyte kZoneCount = 3;

		public static short mHoldZoneTop = 0;

		public static short mHoldZoneLeft = 0;

		public static short mHoldZoneHeight = 250;

		public static short mHoldZoneWidth = 100;

		private TetrisPointerManager mPointerManager;

		public TouchGSReceiver()
		{
			Area hold = new Area
			{
				x = mHoldZoneLeft,
				y = mHoldZoneTop,
				width = mHoldZoneWidth,
				height = mHoldZoneHeight
			};
			mPointerManager = new TetrisPointerManager(this, 480, 800, hold);
		}

		public override void destruct()
		{
			UnRegister();
		}

		public virtual void Initialize(BaseScene gameScene, Viewport holdZone)
		{
			base.Initialize(gameScene);
			mTouchZones = new ZoneRect[3];
			Register();
		}

		public override void Unload()
		{
			UnRegister();
			for (int i = 0; i < 3; i++)
			{
				mTouchZones[i] = null;
			}
			mTouchZones = null;
			base.Unload();
		}

		public override void OnPenDown(short x, short y, sbyte penIndex)
		{
			if (!GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad())
			{
				mPointerManager.PointerPressed(x, y);
			}
		}

		public override void OnPenDrag(short x, short y, sbyte penIndex)
		{
			if (!GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad())
			{
				mPointerManager.PointerDragged(x, y);
			}
		}

		public override void OnPenUp(short x, short y, sbyte penIndex)
		{
			if (!GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad())
			{
				mPointerManager.PointerReleased(x, y);
			}
		}

		public override void ProcessCommand(bool keyUp, int command)
		{
			if (GameApp.Get().GetGameSettings().GetTouchMode() != 0)
			{
				command = 0;
			}
			switch (command)
			{
			case 82:
				SendKey(keyUp, 17);
				break;
			case 84:
				SendKey(keyUp, 24);
				break;
			case 86:
				SendKey(keyUp, 7);
				break;
			case 87:
				SendKey(keyUp, 3);
				break;
			case 85:
				SendKey(keyUp, 4);
				break;
			case 88:
				SendKey(keyUp, 2);
				break;
			case 83:
				SendKey(keyUp, 1);
				break;
			case -19:
				mCurrentScene.OnCommand(-19);
				break;
			}
		}

		public static TouchGSReceiver[] InstArrayTouchGSReceiver(int size)
		{
			TouchGSReceiver[] array = new TouchGSReceiver[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TouchGSReceiver();
			}
			return array;
		}

		public static TouchGSReceiver[][] InstArrayTouchGSReceiver(int size1, int size2)
		{
			TouchGSReceiver[][] array = new TouchGSReceiver[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchGSReceiver[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchGSReceiver();
				}
			}
			return array;
		}

		public static TouchGSReceiver[][][] InstArrayTouchGSReceiver(int size1, int size2, int size3)
		{
			TouchGSReceiver[][][] array = new TouchGSReceiver[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchGSReceiver[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchGSReceiver[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TouchGSReceiver();
					}
				}
			}
			return array;
		}
	}
}
