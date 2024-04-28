namespace ca.jamdat.flight
{
	public class DisplayManager
	{
		public static DisplayContext displayContext;

		public virtual void destruct()
		{
			displayContext = null;
			displayContext = null;
		}

		public static DisplayManager GetInstance()
		{
			return null;
		}

		public static FlRect GetVideoModeRectProxy()
		{
			return GetVideoModeRect();
		}

		public static void RenderApplication()
		{
			displayContext.RenderApplication();
		}

		public static DisplayContext GetMainDisplayContext()
		{
			return displayContext;
		}

		public static bool Initialize(VideoMode parWindowVideoMode, VideoMode resVideoMode, bool alwayOnTop)
		{
			DisplayContext displayContext = DisplayContext.CreateContext(resVideoMode);
			DisplayManager.displayContext = null;
			DisplayManager.displayContext = displayContext;
			return DisplayManager.displayContext != null;
		}

		public static VideoMode GetWindowVideoMode()
		{
			return GetVideoMode();
		}

		public virtual void UpdateOrientation(VideoMode newVideoMode)
		{
			displayContext.UpdateOrientation(newVideoMode);
		}

		public static sbyte GetDisplayAPI()
		{
			return displayContext.GetDisplayAPI();
		}

		public static VideoMode GetVideoMode()
		{
			return displayContext.GetVideoMode();
		}

		public static FlRect GetVideoModeRect()
		{
			return displayContext.GetScreenRect();
		}

		public static bool Initialize(VideoMode windowVideoMode, VideoMode resVideoMode)
		{
			return Initialize(windowVideoMode, resVideoMode, false);
		}

		public static DisplayManager[] InstArrayDisplayManager(int size)
		{
			DisplayManager[] array = new DisplayManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new DisplayManager();
			}
			return array;
		}

		public static DisplayManager[][] InstArrayDisplayManager(int size1, int size2)
		{
			DisplayManager[][] array = new DisplayManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DisplayManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DisplayManager();
				}
			}
			return array;
		}

		public static DisplayManager[][][] InstArrayDisplayManager(int size1, int size2, int size3)
		{
			DisplayManager[][][] array = new DisplayManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new DisplayManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new DisplayManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new DisplayManager();
					}
				}
			}
			return array;
		}
	}
}
