namespace ca.jamdat.tetrisrevolution
{
	public class ReplayEvent
	{
		public const int eventRotateClockwise = 0;

		public const int eventRotateCounterClockwise = 1;

		public const int eventLeft = 2;

		public const int eventRight = 3;

		public const int eventSoftDropActive = 4;

		public const int eventSoftDropInactive = 5;

		public const int eventHardDrop = 6;

		public const int eventHold = 7;

		public int mType;

		public int mRecordedTime;

		public static bool IsKeyEvent(int @event)
		{
			return @event <= 7;
		}

		public ReplayEvent(int replayEventType, int recordedTime)
		{
			mType = replayEventType;
			mRecordedTime = recordedTime;
		}

		public virtual void destruct()
		{
		}

		public new virtual int GetType()
		{
			return mType;
		}

		public virtual int GetTime()
		{
			return mRecordedTime;
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
		}

		public static int TetriminoPositionToData(int posX, int posY, int facingDir)
		{
			int num = 0;
			num = posX;
			num |= posY << 4;
			return num | (facingDir << 12);
		}

		public static int GetPosXFromData(int data)
		{
			return 0xF & data;
		}

		public static int GetPosYFromData(int data)
		{
			return (data >> 4) & 0xFF;
		}

		public static int GetFacingDirFromData(int data)
		{
			return 3 & (data >> 12);
		}
	}
}
