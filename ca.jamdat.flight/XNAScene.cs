using System;
using Microsoft.Xna.Framework.Graphics;

namespace ca.jamdat.flight
{
	public class XNAScene
	{
		public const int SCREEN_WIDTH = 480;

		public const int SCREEN_HEIGHT = 800;

		public const int ACCEPT_KEY_CODE = -6;

		public const int DECLINE_KEY_CODE = -7;

		public const int KEY_POUND_KEY_CODE = 35;

		public const int KEY_STAR_KEY_CODE = 42;

		public const int FIRE_KEY_CODE = -5;

		public const int MENU_KEY_CODE = 99999;

		public static bool repaintScene = true;

		protected internal static XNAApp mApp;

		public short mScreenRect_left;

		public short mScreenRect_top;

		public short mScreenRect_width;

		public short mScreenRect_height;

		public bool mScreenRectInitialized;

		private MIDPDisplayContextImp mDisplayContextImp;

		public int mLastGameTime;

		public XNAScene()
		{
			DisplayContext mainDisplayContext = DisplayManager.GetMainDisplayContext();
			mDisplayContextImp = (MIDPDisplayContextImp)mainDisplayContext;
		}

		public virtual void destruct()
		{
		}

		protected internal void hideNotify()
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("hideNotify()"));
			mApp.pause();
			mApp.mResumeNextIsShown = true;
		}

		protected internal void showNotify()
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("shownotify()"));
			mApp.start();
			mApp.AddEvent(7);
		}

		public int getOsWidth()
		{
			return 480;
		}

		public int getOsHeight()
		{
			return 800;
		}

		public virtual void sizeChanged(int width, int height)
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("sizeChangedEvent"), width, height);
			mApp.AddEvent(12, (short)width, (short)height);
		}

		public virtual void UpdateDisplayContext(short width, short height)
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("UpdateDisplayContext"), width, height);
			DisplayContext mainDisplayContext = DisplayManager.GetMainDisplayContext();
			mDisplayContextImp = (MIDPDisplayContextImp)mainDisplayContext;
		}

		public virtual void updateSoftKey(int softKey, string label)
		{
		}

		protected internal void pointerDragged(int x, int y)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("J2ME pointerDragged"), x, y);
			mApp.AddEvent(10, (short)x, (short)y);
		}

		protected internal void pointerPressed(int x, int y)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("J2ME pointerPressed"), x, y);
			mApp.AddEvent(9, (short)x, (short)y);
		}

		protected internal void pointerReleased(int x, int y)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("J2ME pointerReleased"), x, y);
			mApp.AddEvent(11, (short)x, (short)y);
		}

		protected internal void keyPressed(int keyCode)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("J2ME keyPressed"), keyCode);
			mApp.AddEvent(1, keyCode);
		}

		protected internal void keyReleased(int keyCode)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("J2ME keyReleased"), keyCode);
			mApp.AddEvent(2, keyCode);
		}

		public virtual void paint(SpriteBatch g)
		{
			try
			{
				g.Begin();
				onDraw(g);
				g.End();
				repaintScene = true;
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected internal virtual void OnFlightKeyPressed(int flightKey)
		{
			if (XNAApp.mApplication.mLastKeyDown != flightKey)
			{
				XNAApp.mApplication.OnKeyFromOS(flightKey, false);
			}
		}

		protected internal virtual void OnFlightKeyReleased(int flightKey)
		{
			XNAApp.mApplication.OnKeyFromOS(flightKey, true);
		}

		protected internal virtual void OnKeyPressed(int key)
		{
			OnFlightKeyPressed(TranslateKey(key, false));
		}

		protected internal virtual void OnKeyReleased(int key)
		{
			OnFlightKeyReleased(TranslateKey(key, true));
		}

		protected internal virtual void onDraw(SpriteBatch g)
		{
			FlRect videoModeRect = DisplayManager.GetVideoModeRect();
			mScreenRect_left = videoModeRect.GetLeft();
			mScreenRect_top = videoModeRect.GetTop();
			mScreenRect_width = videoModeRect.GetWidth();
			mScreenRect_height = videoModeRect.GetHeight();
			mScreenRectInitialized = true;
			mDisplayContextImp.SetClippingRect(mScreenRect_left, mScreenRect_top, mScreenRect_width, mScreenRect_height);
			mDisplayContextImp.SetGraphics(g);
			DisplayManager.RenderApplication();
		}

		public virtual void onTime(int time)
		{
			FlApplication.GetInstance().Iteration(time, time - mLastGameTime);
			mLastGameTime = time;
			repaintScene = true;
		}

		public virtual int TranslateKey(int javaKey, bool keyUp)
		{
			if (javaKey == SpecConstants.GetKeyCodeUp())
			{
				return 1;
			}
			if (javaKey == SpecConstants.GetKeyCodeDown())
			{
				return 2;
			}
			if (javaKey == SpecConstants.GetKeyCodeLeft())
			{
				return 3;
			}
			if (javaKey == SpecConstants.GetKeyCodeRight())
			{
				return 4;
			}
			if (javaKey == SpecConstants.GetKeyCodeBack())
			{
				return 9;
			}
			if (javaKey == SpecConstants.GetKeyCodeBackspace())
			{
				return 10;
			}
			if (javaKey == SpecConstants.GetKeyCodeStar())
			{
				return 16;
			}
			if (javaKey == SpecConstants.GetKeyCodePound())
			{
				return 15;
			}
			if (javaKey == SpecConstants.GetFlightKeyCodeFire())
			{
				return 7;
			}
			if (javaKey == SpecConstants.GetKeyCodeAccept())
			{
				return 13;
			}
			if (javaKey == SpecConstants.GetKeyCodeDecline())
			{
				return 14;
			}
			if (javaKey == SpecConstants.GetKeyCodeSend())
			{
				return 0;
			}
			if (javaKey == SpecConstants.GetKeyCodeEnd())
			{
				return 0;
			}
			if (javaKey == SpecConstants.GetKeyCodeVolumeUp())
			{
				return 0;
			}
			if (javaKey == SpecConstants.GetKeyCodeVolumeDown())
			{
				return 0;
			}
			if (javaKey == SpecConstants.GetKeyCodeMediaPlayer())
			{
				return 0;
			}
			if (javaKey == SpecConstants.GetKeyCodeUnused1())
			{
				return 0;
			}
			return FlKeyManager.ConstToFlightKey(javaKey);
		}
	}
}
