using System;

namespace ca.jamdat.flight
{
	public class TimeControlled
	{
		public const sbyte typeNumber = 0;

		public const sbyte typeID = 0;

		public const bool supportsDynamicSerialization = true;

		public const short componentPosition = 1;

		public const short componentSize = 2;

		public const short componentSizeCentered = 3;

		public const short componentRect = 4;

		public const short componentVisibility = 5;

		public const short componentOpacity = 6;

		public const short indexedSpriteFrame = 7;

		public const short brushColor = 8;

		public const short nodeTranslation = 9;

		public const short nodeRotation = 10;

		public const short nodeScaling = 11;

		public const short viewportRotation = 12;

		public const short viewportScaling = 13;

		public const short viewportRectNoMoveChildren = 14;

		public const short lastFrameworkControlCode = 100;

		public static TimeControlled Cast(object o, TimeControlled _)
		{
			return (TimeControlled)o;
		}

		public virtual sbyte GetTypeID()
		{
			return 0;
		}

		public static Type AsClass()
		{
			return null;
		}

		public virtual void OnTime(int totalTime, int deltaTime)
		{
		}

		public virtual void ControlValue(int valueControlCode, bool setValue, Controller controller)
		{
		}

		public virtual void destruct()
		{
		}

		public virtual void RegisterInGlobalTime()
		{
			FlApplication.GetGlobalTimeSystem().Register(this);
		}

		public virtual void UnRegisterInGlobalTime()
		{
			FlApplication.GetGlobalTimeSystem().UnRegister(this);
		}

		public virtual bool IsRegisteredInGlobalTime()
		{
			return FlApplication.GetGlobalTimeSystem().IsRegistered(this);
		}

		public virtual void OnSerialize(Package p)
		{
		}

		public static TimeControlled[] InstArrayTimeControlled(int size)
		{
			TimeControlled[] array = new TimeControlled[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TimeControlled();
			}
			return array;
		}

		public static TimeControlled[][] InstArrayTimeControlled(int size1, int size2)
		{
			TimeControlled[][] array = new TimeControlled[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeControlled[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeControlled();
				}
			}
			return array;
		}

		public static TimeControlled[][][] InstArrayTimeControlled(int size1, int size2, int size3)
		{
			TimeControlled[][][] array = new TimeControlled[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TimeControlled[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TimeControlled[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TimeControlled();
					}
				}
			}
			return array;
		}
	}
}
