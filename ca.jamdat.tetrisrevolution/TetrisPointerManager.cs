namespace ca.jamdat.tetrisrevolution
{
	internal class TetrisPointerManager : PointerManager
	{
		private TouchGSReceiver rec;

		public TetrisPointerManager(TouchGSReceiver receiver, int width, int height, Area hold)
			: base(width, height, hold)
		{
			rec = receiver;
		}

		protected override void DoHoldPiece()
		{
			rec.ProcessCommand(false, 82);
			rec.ProcessCommand(true, 82);
		}

		protected override void DoMoveLeft()
		{
			rec.ProcessCommand(false, 87);
			rec.ProcessCommand(true, 87);
		}

		protected override void DoMoveRight()
		{
			rec.ProcessCommand(false, 85);
			rec.ProcessCommand(true, 85);
		}

		protected override void DoStartSoftDrop()
		{
			rec.ProcessCommand(false, 88);
		}

		protected override void DoStopSoftDrop()
		{
			rec.ProcessCommand(true, 88);
		}

		protected override void DoCWRotation()
		{
			rec.ProcessCommand(false, 86);
			rec.ProcessCommand(true, 86);
		}

		protected override void DoCCWRotation()
		{
			rec.ProcessCommand(false, 84);
			rec.ProcessCommand(true, 84);
		}

		protected override void DoHardDrop()
		{
			rec.ProcessCommand(false, 83);
			rec.ProcessCommand(true, 83);
		}
	}
}
