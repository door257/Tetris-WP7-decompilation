using System;

namespace ca.jamdat.flight
{
	public class KeyFrameController : Controller
	{
		public new const sbyte typeNumber = 88;

		public new const sbyte typeID = 88;

		public new const bool supportsDynamicSerialization = true;

		public KeyFrameSequence mKeySequence;

		public static KeyFrameController Cast(object o, KeyFrameController _)
		{
			return (KeyFrameController)o;
		}

		public KeyFrameController()
		{
		}

		public KeyFrameController(KeyFrameSequence seq)
		{
			mKeySequence = seq;
		}

		public override sbyte GetTypeID()
		{
			return 88;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			mKeySequence = null;
		}

		public override void OnTime(int totalTime, int deltaTime)
		{
			if (mControllee != null)
			{
				mKeySequence.GetObjectValue(totalTime, mValueBuffer);
				DefaultOnTime(totalTime, deltaTime);
			}
		}

		public virtual KeyFrameSequence GetKeySequence()
		{
			return mKeySequence;
		}

		public virtual void SetKeySequence(KeyFrameSequence val)
		{
			mKeySequence = val;
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mKeySequence = (KeyFrameSequence)p.SerializePointer(89, false, false);
		}

		public static KeyFrameController[] InstArrayKeyFrameController(int size)
		{
			KeyFrameController[] array = new KeyFrameController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new KeyFrameController();
			}
			return array;
		}

		public static KeyFrameController[][] InstArrayKeyFrameController(int size1, int size2)
		{
			KeyFrameController[][] array = new KeyFrameController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyFrameController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyFrameController();
				}
			}
			return array;
		}

		public static KeyFrameController[][][] InstArrayKeyFrameController(int size1, int size2, int size3)
		{
			KeyFrameController[][][] array = new KeyFrameController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyFrameController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyFrameController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new KeyFrameController();
					}
				}
			}
			return array;
		}
	}
}
