using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class WellObserver : TimeControlled
	{
		public Well mWell;

		public bool mWarningActive;

		public bool mWarningOver;

		public WellObserver(Well well)
		{
			mWell = well;
		}

		public override void destruct()
		{
		}

		public virtual void OnModeEndTurn()
		{
			if (IsInDanger())
			{
				if (!mWarningActive)
				{
					ActivateWarning();
				}
			}
			else if (mWarningActive)
			{
				mWarningOver = true;
				DeactivateWarning();
			}
		}

		public virtual bool IsWarningActive()
		{
			return mWarningActive;
		}

		public virtual bool IsInDanger()
		{
			bool flag = false;
			int num = CalculateWellFillPercent();
			return num <= 30;
		}

		public virtual int CalculateWellFillPercent()
		{
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				for (int j = 20; j < 40 && !mWell.GetLine(j).IsThereLockedMino(i); j++)
				{
					num++;
				}
			}
			F32 f = new F32(F32.FromInt(num * 100, 16));
			F32 f2 = new F32(F32.FromInt(200, 16));
			return f.Div(f2, 16).Floor(16).ToInt(16);
		}

		public virtual void ActivateWarning()
		{
			mWarningActive = true;
		}

		public virtual void DeactivateWarning()
		{
			mWarningOver = false;
			mWarningActive = false;
		}
	}
}
