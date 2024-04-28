using System;

namespace ca.jamdat.tetrisrevolution
{
	public abstract class PointerManager
	{
		private const int FLICK_MS = 250;

		private const int HORZ_MOVE_THRESHOLD = 100;

		protected bool _isMovingRight;

		protected bool _isMovingLeft;

		protected bool _isSoftDropping;

		protected bool _isDraggingUp;

		protected bool _holding;

		protected bool _wasMinoMoved;

		protected bool _hasMovedHorizontallySinceDragStarted;

		private int scr_w;

		private int scr_h;

		private Area holdArea;

		private int penDownCoordX;

		private int penDownCoordY;

		private int latestPenInputCoordX;

		private int latestPenInputCoordY;

		private int dragAccumulatedDistanceX;

		private int dragAccumulatedDistanceY;

		private long flickStartTimeMs;

		private long timeMsSinceHorzMove;

		public PointerManager(int width, int height, Area hold)
		{
			scr_w = width;
			scr_h = height;
			holdArea = hold;
		}

		protected abstract void DoHoldPiece();

		protected abstract void DoMoveLeft();

		protected abstract void DoMoveRight();

		protected abstract void DoStartSoftDrop();

		protected abstract void DoStopSoftDrop();

		protected abstract void DoCWRotation();

		protected abstract void DoCCWRotation();

		protected abstract void DoHardDrop();

		public void PointerPressed(int x, int y)
		{
			penDownCoordX = x;
			penDownCoordY = y;
			latestPenInputCoordX = penDownCoordX;
			latestPenInputCoordY = penDownCoordY;
			dragAccumulatedDistanceX = (dragAccumulatedDistanceY = 0);
			flickStartTimeMs = DateTime.Now.Ticks / 10000;
			_wasMinoMoved = false;
			_hasMovedHorizontallySinceDragStarted = false;
			if (IsInHoldArea(x, y))
			{
				DoHoldPiece();
				_wasMinoMoved = true;
			}
		}

		public void PointerDragged(int x, int y)
		{
			int num = x - latestPenInputCoordX;
			int num2 = y - latestPenInputCoordY;
			latestPenInputCoordX = x;
			latestPenInputCoordY = y;
			dragAccumulatedDistanceX += num;
			dragAccumulatedDistanceY += num2;
			int dragStepX = GetDragStepX(dragAccumulatedDistanceX);
			int dragStepY = GetDragStepY(dragAccumulatedDistanceY);
			if (dragAccumulatedDistanceX >= dragStepX)
			{
				CancelSwipeActions();
				_isMovingLeft = false;
				_isMovingRight = true;
				DoMoveRight();
				dragAccumulatedDistanceX -= dragStepX;
				dragAccumulatedDistanceY = 0;
			}
			else if (dragAccumulatedDistanceX <= -dragStepX)
			{
				CancelSwipeActions();
				_isMovingLeft = true;
				_isMovingRight = false;
				DoMoveLeft();
				dragAccumulatedDistanceX += dragStepX;
				dragAccumulatedDistanceY = 0;
			}
			else if (!_isSoftDropping && dragAccumulatedDistanceY >= dragStepY)
			{
				flickStartTimeMs = DateTime.Now.Ticks / 10000;
				_isMovingLeft = (_isMovingRight = false);
				_isSoftDropping = true;
				DoStartSoftDrop();
			}
			else if (!_isDraggingUp && dragAccumulatedDistanceY <= -dragStepY)
			{
				_isDraggingUp = true;
				flickStartTimeMs = DateTime.Now.Ticks / 10000;
			}
			if (!_hasMovedHorizontallySinceDragStarted)
			{
				_hasMovedHorizontallySinceDragStarted = _isMovingLeft || _isMovingRight;
				if (_hasMovedHorizontallySinceDragStarted)
				{
					timeMsSinceHorzMove = DateTime.Now.Ticks / 10000;
				}
			}
			if (!_wasMinoMoved)
			{
				_wasMinoMoved = _isMovingLeft || _isMovingRight || _isSoftDropping || _isDraggingUp;
			}
		}

		public void PointerReleased(int x, int y)
		{
			int num = x - latestPenInputCoordX;
			int num2 = y - latestPenInputCoordY;
			dragAccumulatedDistanceX += num;
			dragAccumulatedDistanceY += num2;
			int dragStepY = GetDragStepY(dragAccumulatedDistanceY);
			if (!_wasMinoMoved && Math.Abs(dragAccumulatedDistanceX) < 20 && Math.Abs(dragAccumulatedDistanceY) < 20)
			{
				if (IsClickRotationCW(x))
				{
					DoCWRotation();
				}
				else if (IsClickRotationCCW(x))
				{
					DoCCWRotation();
				}
			}
			long num3 = DateTime.Now.Ticks / 10000;
			long num4 = num3 - flickStartTimeMs;
			long num5 = num3 - timeMsSinceHorzMove;
			bool flag = num4 < 250;
			bool flag2 = _hasMovedHorizontallySinceDragStarted && num5 < 100;
			if (_isSoftDropping)
			{
				_isSoftDropping = false;
				DoStopSoftDrop();
				if (flag && !flag2)
				{
					DoHardDrop();
				}
			}
			else if (_isDraggingUp)
			{
				_isDraggingUp = false;
				if (flag && !flag2)
				{
					DoHoldPiece();
				}
			}
			else if (!flag2 && flag && dragAccumulatedDistanceY >= dragStepY)
			{
				DoHardDrop();
			}
			_hasMovedHorizontallySinceDragStarted = false;
			_wasMinoMoved = (_isMovingLeft = (_isMovingRight = (_isSoftDropping = false)));
		}

		private void CancelSwipeActions()
		{
			if (_isSoftDropping)
			{
				_isSoftDropping = false;
				DoStopSoftDrop();
			}
			_isDraggingUp = false;
		}

		private bool IsInHoldArea(int x, int y)
		{
			return PointContainedIn(x, y, holdArea);
		}

		private bool PointContainedIn(int x, int y, Area area)
		{
			if (x >= area.x && x <= area.x + area.width && y >= area.y)
			{
				return y <= area.y + area.height;
			}
			return false;
		}

		private int GetDragStepX(int accumulatedDistanceX)
		{
			int result = scr_w / 13;
			int num = Math.Abs(accumulatedDistanceX);
			if (num > scr_w / 5)
			{
				result = scr_w / 32;
			}
			else if (num > scr_w / 6)
			{
				result = scr_w / 16;
			}
			return result;
		}

		private int GetDragStepY(int accumulatedDistanceY)
		{
			return scr_h / 14;
		}

		private bool IsClickRotationCW(int ptX)
		{
			return ptX > scr_w / 2;
		}

		private bool IsClickRotationCCW(int ptX)
		{
			return ptX < scr_w / 2;
		}
	}
}
