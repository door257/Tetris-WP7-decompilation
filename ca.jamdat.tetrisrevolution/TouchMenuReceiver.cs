namespace ca.jamdat.tetrisrevolution
{
	public class TouchMenuReceiver : TouchReceiver
	{
		public const sbyte kSelector = 0;

		public const sbyte kTapSelect = 1;

		public const sbyte kTapUp = 2;

		public const sbyte kTapDown = 3;

		public const sbyte kTapLeft = 4;

		public const sbyte kTapRight = 5;

		public const sbyte kZoneCount = 6;

		public override void destruct()
		{
		}

		public override void Initialize(BaseScene currentScene)
		{
			base.Initialize(currentScene);
			mTouchZones = new ZoneRect[6];
			currentScene.CreateTouchZones();
			Register();
		}

		public override void Unload()
		{
			if (IsRegistered())
			{
				UnRegister();
				for (int i = 0; i < 6; i++)
				{
					mTouchZones[i] = null;
				}
				mTouchZones = null;
				base.Unload();
			}
		}

		public override void OnDragNorth()
		{
			ForwardDragOrSwipeCommand(90, 6, 2);
		}

		public override void OnDragSouth()
		{
			ForwardDragOrSwipeCommand(91, 6, 2);
		}

		public override void OnSwipeNorth()
		{
			ForwardDragOrSwipeCommand(94, 6, 4);
		}

		public override void OnSwipeSouth()
		{
			ForwardDragOrSwipeCommand(95, 6, 4);
		}

		public override void OnPenTap(short xCoord, short yCoord)
		{
			for (int i = 0; i < 6; i++)
			{
				if (mTouchZones[i] != null && mTouchZones[i].IsTappableType() && mTouchZones[i].IsInside(xCoord, yCoord))
				{
					SendTouchCommand(98, i);
				}
			}
		}

		public static TouchMenuReceiver[] InstArrayTouchMenuReceiver(int size)
		{
			TouchMenuReceiver[] array = new TouchMenuReceiver[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TouchMenuReceiver();
			}
			return array;
		}

		public static TouchMenuReceiver[][] InstArrayTouchMenuReceiver(int size1, int size2)
		{
			TouchMenuReceiver[][] array = new TouchMenuReceiver[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchMenuReceiver[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchMenuReceiver();
				}
			}
			return array;
		}

		public static TouchMenuReceiver[][][] InstArrayTouchMenuReceiver(int size1, int size2, int size3)
		{
			TouchMenuReceiver[][][] array = new TouchMenuReceiver[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchMenuReceiver[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchMenuReceiver[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TouchMenuReceiver();
					}
				}
			}
			return array;
		}
	}
}
