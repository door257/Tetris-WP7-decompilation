namespace ca.jamdat.tetrisrevolution
{
	public class TouchPopupReceiver : TouchReceiver
	{
		public const sbyte kScroller = 0;

		public const sbyte kTapSelect = 1;

		public const sbyte kZoneCount = 2;

		public Popup mCurrentPopup;

		public override void destruct()
		{
		}

		public virtual void Initialize(Popup currentPopup)
		{
			base.Initialize(GameApp.Get().GetCommandHandler().GetCurrentScene());
			mCurrentPopup = currentPopup;
			mTouchZones = new ZoneRect[2];
			CreateZone(0, currentPopup.GetVisualRect(), 0, 1);
			Register();
		}

		public override void Unload()
		{
			UnRegister();
			mCurrentPopup = null;
			if (mTouchZones != null)
			{
				for (int i = 0; i < 2; i++)
				{
					mTouchZones[i] = null;
				}
				mTouchZones = null;
			}
		}

		public override bool IsRegistered()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			return penInputController.IsExclusiveRegistered();
		}

		public override void Register()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			if (!IsRegistered())
			{
				penInputController.RegisterExclusiveReceiver(this);
			}
		}

		public override void UnRegister()
		{
			PenInputController penInputController = GameApp.Get().GetPenInputController();
			if (IsRegistered())
			{
				penInputController.UnRegisterExclusiveReceiver();
			}
		}

		public override void SendTouchCommand(int command, int zoneId)
		{
			mCurrentPopup.OnTouchCommand(command, zoneId, mFirstPenInput, mLastPenInput);
		}

		public override void OnDragNorth()
		{
			ForwardDragOrSwipeCommand(90, 2, 2);
		}

		public override void OnDragSouth()
		{
			ForwardDragOrSwipeCommand(91, 2, 2);
		}

		public override void OnSwipeNorth()
		{
			ForwardDragOrSwipeCommand(94, 2, 4);
		}

		public override void OnSwipeSouth()
		{
			ForwardDragOrSwipeCommand(95, 2, 4);
		}

		public override void OnSwipeEast()
		{
			ForwardDragOrSwipeCommand(96, 2, 4);
		}

		public override void OnSwipeWest()
		{
			ForwardDragOrSwipeCommand(97, 2, 4);
		}

		public override void OnPenTap(short xCoord, short yCoord)
		{
			for (int i = 0; i < 2; i++)
			{
				if (mTouchZones[i] != null && mTouchZones[i].IsTappableType() && mTouchZones[i].IsInside(xCoord, yCoord))
				{
					SendTouchCommand(98, i);
				}
			}
		}

		public static TouchPopupReceiver[] InstArrayTouchPopupReceiver(int size)
		{
			TouchPopupReceiver[] array = new TouchPopupReceiver[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TouchPopupReceiver();
			}
			return array;
		}

		public static TouchPopupReceiver[][] InstArrayTouchPopupReceiver(int size1, int size2)
		{
			TouchPopupReceiver[][] array = new TouchPopupReceiver[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchPopupReceiver[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchPopupReceiver();
				}
			}
			return array;
		}

		public static TouchPopupReceiver[][][] InstArrayTouchPopupReceiver(int size1, int size2, int size3)
		{
			TouchPopupReceiver[][][] array = new TouchPopupReceiver[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TouchPopupReceiver[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TouchPopupReceiver[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TouchPopupReceiver();
					}
				}
			}
			return array;
		}
	}
}
