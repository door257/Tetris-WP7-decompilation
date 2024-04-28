using System;
using ca.jamdat.fuser;

namespace ca.jamdat.flight
{
	public class XNAApp
	{
		private const int MAX_EVENTS = 8;

		private const int MAX_KEY_EVENTS = 6;

		public const short EVT_INVALID = 0;

		public const short EVT_KEY_PRESSED = 1;

		public const short EVT_KEY_RELEASED = 2;

		public const short EVT_FLIGHT_KEY_PRESSED = 3;

		public const short EVT_FLIGHT_KEY_RELEASED = 4;

		public const short EVT_PAUSE = 5;

		public const short EVT_RESUME = 6;

		public const short EVT_REPAINT = 7;

		public const short EVT_EXIT = 8;

		public const short EVT_PEN_DOWN = 9;

		public const short EVT_PEN_MOVED = 10;

		public const short EVT_PEN_UP = 11;

		public const short EVT_SIZE_CHANGED = 12;

		public static FrameworkGlobals mFrameworkGlobals;

		public static object paintLock = new object();

		public static XNAApp instance;

		public static FlApplication mApplication;

		public static XNAScene mScene;

		private long mTotalTimePaused = FlApplication.GetRealTime();

		private long mStopTimeStamp = -1L;

		private long[] mEvtQueue = new long[8];

		private int mEvtCount;

		public volatile bool mIsPaused;

		private bool mForceRepaintBeforeNextEvent;

		public bool mFirstTime;

		private volatile bool mPauseInQueue;

		private bool mIsSoundManagerMuted;

		private bool mLaunchBrowserOnExit;

		private string mExitURI = "";

		public long mLastResumeTimeStamp = FlApplication.GetRealTime();

		public volatile bool mMustQuit;

		public bool mDoublePaint;

		public bool mResumeNextIsShown;

		public XNAApp()
		{
			mFirstTime = true;
			XNAScene.mApp = (instance = this);
		}

		public virtual void destruct()
		{
		}

		public virtual void setLaunchBrowserOnExit(bool launch)
		{
			mLaunchBrowserOnExit = launch;
		}

		public virtual void setExitURI(string uri)
		{
			mExitURI = uri;
		}

		public virtual bool callPlatformRequest(string uri)
		{
			return false;
		}

		protected internal virtual void startApp()
		{
			if (!mFirstTime)
			{
				FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("startApp()"));
			}
			start();
		}

		public void start()
		{
			StartTime();
			try
			{
				if (mFirstTime)
				{
					InitializeFrameworkApplication();
					InitializeNetworkThreads();
				}
				else if (mPauseInQueue || mIsPaused)
				{
					AddEvent(6);
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected internal virtual void pauseApp()
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("pauseApp()"));
			pause();
		}

		public virtual void pause()
		{
			StopTime();
			if (!mPauseInQueue && !mIsPaused && !mFirstTime && !mMustQuit)
			{
				AddEvent(5);
			}
		}

		protected internal virtual void destroyApp(bool unconditionnal)
		{
			try
			{
				if (!mMustQuit)
				{
					mMustQuit = true;
					mApplication.SaveGame();
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		public virtual void OSExit()
		{
			mMustQuit = true;
			mApplication.SaveGame();
			GC.Collect();
			if (mLaunchBrowserOnExit)
			{
				try
				{
					callPlatformRequest(mExitURI);
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
			}
		}

		public void forceRepaintBeforeNextEvent()
		{
			mForceRepaintBeforeNextEvent = true;
		}

		public virtual void run()
		{
		}

		internal virtual void InitializeNetworkThreads()
		{
		}

		public virtual void ProcessEvents()
		{
			short num = 0;
			int num2 = 0;
			short num3 = 0;
			short num4 = 0;
			lock (paintLock)
			{
				while (mEvtCount > 0 && !mForceRepaintBeforeNextEvent)
				{
					lock (mEvtQueue)
					{
						long[] array = mEvtQueue;
						num = (short)((ulong)array[0] >> 48);
						num2 = (int)((array[0] & 0xFFFFFFFF0000L) >> 16);
						num3 = (short)((array[0] & 0xFFFF00000000L) >> 32);
						num4 = (short)((array[0] & 0xFFFF0000u) >> 16);
						long num5 = array[0];
						if (mEvtCount > 1)
						{
							for (int i = 0; i < 7; i++)
							{
								array[i] = array[i + 1];
							}
						}
						mEvtCount--;
					}
					switch (num)
					{
					case 1:
						mScene.OnKeyPressed(num2);
						break;
					case 2:
						mScene.OnKeyReleased(num2);
						break;
					case 3:
						mScene.OnFlightKeyPressed(num2);
						break;
					case 4:
						mScene.OnFlightKeyReleased(num2);
						break;
					case 5:
						mPauseInQueue = false;
						mIsPaused = true;
						mFrameworkGlobals.application.OnFocusLostFromOS();
						mFrameworkGlobals.application.OnSuspendFromOS();
						break;
					case 6:
						mLastResumeTimeStamp = FlApplication.GetRealTime();
						if (mIsPaused)
						{
							mFrameworkGlobals.application.OnFocusGainedFromOS();
							mFrameworkGlobals.application.OnResumeFromOS();
							mIsPaused = false;
							mDoublePaint = true;
						}
						break;
					case 7:
						XNAScene.repaintScene = true;
						FlApplication.GetInstance().Invalidate();
						break;
					case 8:
						FlApplication.Exit();
						break;
					case 10:
						FlPenManager.Get().OnPenDrag(num3, num4);
						break;
					case 9:
						FlPenManager.Get().OnPenDown(num3, num4);
						break;
					case 11:
						FlPenManager.Get().OnPenUp(num3, num4);
						break;
					case 12:
						FlApplication.GetInstance().OnScreenSizeChangedFromOS(num3, num4);
						break;
					}
				}
				mForceRepaintBeforeNextEvent = false;
			}
		}

		public virtual void AddEvent(short type)
		{
			AddEvent(type, 0, 0, 0);
		}

		public virtual void AddEvent(short type, int detailInt)
		{
			short detail = (short)((uint)detailInt >> 16);
			short detail2 = (short)(detailInt & 0xFFFF);
			AddEvent(type, detail, detail2, 0);
		}

		public virtual void AddEvent(short type, short detail0, short detail1)
		{
			AddEvent(type, detail0, detail1, 0);
		}

		public virtual void AddEvent(short type, short detail0, short detail1, short detail2)
		{
			if ((mPauseInQueue || mIsPaused) && type != 6)
			{
				return;
			}
			switch (type)
			{
			case 5:
				mPauseInQueue = true;
				PauseSoundsOnInterrupts();
				lock (mEvtQueue)
				{
					mEvtCount = 0;
				}
				break;
			case 6:
				ResumeSoundsOnInterrupts();
				break;
			}
			bool flag = true;
			lock (mEvtQueue)
			{
				switch (type)
				{
				case 1:
				case 9:
					if (mEvtCount >= 5)
					{
						flag = false;
					}
					break;
				case 5:
				case 6:
					if (mEvtCount >= 8)
					{
						flag = false;
					}
					break;
				default:
					if (mEvtCount >= 6)
					{
						flag = false;
					}
					break;
				}
				if (flag)
				{
					mEvtQueue[mEvtCount++] = ((long)type << 48) | (((long)detail0 << 32) & 0xFFFF00000000L) | (((long)detail1 << 16) & 0xFFFF0000u) | ((long)detail2 & 0xFFFFL);
				}
			}
		}

		private void PauseSoundsOnInterrupts()
		{
		}

		private void ResumeSoundsOnInterrupts()
		{
		}

		public void StartTime()
		{
			if (mStopTimeStamp != -1)
			{
				mTotalTimePaused += FlApplication.GetRealTime() - mStopTimeStamp;
				mStopTimeStamp = -1L;
			}
		}

		public void StopTime()
		{
			StopTime(FlApplication.GetRealTime());
		}

		public void StopTime(long timestamp)
		{
			if (mStopTimeStamp == -1)
			{
				mStopTimeStamp = timestamp;
			}
		}

		public virtual int GetGameTime()
		{
			if (mStopTimeStamp == -1)
			{
				return (int)(FlApplication.GetRealTime() - mTotalTimePaused);
			}
			return (int)(mStopTimeStamp - mTotalTimePaused);
		}

		public static XNAScene GetCurrentScene()
		{
			return mScene;
		}

		public virtual void InitializeFrameworkApplication()
		{
			mFrameworkGlobals = new FrameworkGlobals();
			FrameworkGlobals.GetInstance().xnaApp = this;
			VideoMode videoMode = new VideoMode(SpecConstants.GetUsedWidth(), SpecConstants.GetUsedHeight(), Constants.ScreenColorDepth());
			DisplayManager.Initialize(videoMode, videoMode);
			mApplication = ApplicationStarter.NewFlightApp();
		}
	}
}
