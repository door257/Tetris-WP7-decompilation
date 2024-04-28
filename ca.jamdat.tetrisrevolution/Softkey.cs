using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Softkey
	{
		public const int functionSelect = 0;

		public const int functionBack = 1;

		public const int functionExit = 2;

		public const int functionPlay = 3;

		public const int functionPause = 4;

		public const int functionMenu = 5;

		public const int functionHint = 6;

		public const int functionDisabled = 7;

		public const int functionOk = 0;

		public const int functionCancel = 2;

		public const int functionSkip = 3;

		public const int functionNext = 3;

		public const int functionReplay = 3;

		public const int typeUndefined = 0;

		public const int typeSelect = 5;

		public const int typeClear = 6;

		public Selection mSoftkey;

		public int mFunction;

		public virtual void destruct()
		{
		}

		public virtual void Initialize(int type, Selection softkey)
		{
			mSoftkey = softkey;
		}

		public virtual void Uninitialize()
		{
			mSoftkey = null;
		}

		public virtual void SetPushed(bool pushed)
		{
			if (mSoftkey != null && mSoftkey.GetEnabledState())
			{
				mSoftkey.SetPushedState(pushed);
			}
		}

		public virtual void SetEnabled(bool enabled)
		{
			mSoftkey.SetEnabledState(enabled);
			if (mFunction == 7 || mFunction == 2 || mFunction == 1 || mFunction == 4)
			{
				mSoftkey.SetVisible(false);
			}
			else
			{
				mSoftkey.SetVisible(enabled);
			}
		}

		public virtual void SetFunction(int fction, int command)
		{
			if (mSoftkey.GetCommand() != command || mFunction != fction)
			{
				IndexedSprite indexedSprite = (IndexedSprite)mSoftkey.GetChild(0);
				bool flag = fction != 7;
				indexedSprite.SetVisible(flag);
				if (flag)
				{
					indexedSprite.SetCurrentFrame(fction);
				}
				mFunction = fction;
				SetEnabled(command != 0);
				mSoftkey.SetCommand((sbyte)command);
			}
		}

		public virtual void UpdatePos(int type)
		{
			FlRect screenRect = DisplayManager.GetMainDisplayContext().GetScreenRect();
			int width = screenRect.GetWidth();
			screenRect.GetHeight();
			int num = width - mSoftkey.GetRectWidth();
			short rectTop = mSoftkey.GetRectTop();
			switch (type)
			{
			case 5:
				mSoftkey.SetTopLeft(0, rectTop);
				break;
			case 6:
				mSoftkey.SetTopLeft((short)num, rectTop);
				break;
			}
		}

		public static Softkey[] InstArraySoftkey(int size)
		{
			Softkey[] array = new Softkey[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Softkey();
			}
			return array;
		}

		public static Softkey[][] InstArraySoftkey(int size1, int size2)
		{
			Softkey[][] array = new Softkey[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Softkey[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Softkey();
				}
			}
			return array;
		}

		public static Softkey[][][] InstArraySoftkey(int size1, int size2, int size3)
		{
			Softkey[][][] array = new Softkey[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Softkey[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Softkey[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Softkey();
					}
				}
			}
			return array;
		}
	}
}
