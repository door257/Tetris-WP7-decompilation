using System;
using ca.jamdat.fuser;

namespace ca.jamdat.flight
{
	public class FlApplication : Viewport
	{
		public const sbyte ExitTypeNow = 0;

		public const sbyte ExitTypeSoon = 1;

		public const sbyte TimeFormat12H = 0;

		public const sbyte TimeFormat24H = 1;

		public const sbyte gameOrientationCurrent = 0;

		public const sbyte gameOrientation0 = 1;

		public const sbyte gameOrientation90 = 2;

		public const sbyte gameOrientation180 = 4;

		public const sbyte gameOrientation270 = 8;

		public const sbyte gameOrientationSystem = 16;

		public const sbyte softKeyPositionError = 0;

		public const sbyte softKeyPositionLeft = 1;

		public const sbyte softKeyPositionRight = 2;

		public const sbyte softKeyPositionBottom = 16;

		public const sbyte softKeyPositionTop = 32;

		public const sbyte softKeyPositionBottomLeft = 17;

		public const sbyte softKeyPositionBottomRight = 18;

		public const sbyte softKeyPositionTopLeft = 33;

		public const sbyte softKeyPositionTopRight = 34;

		public const sbyte defaultGamingKeyPrimary = 0;

		public const sbyte defaultGamingKeySecondary = 1;

		public const sbyte playViewDefault = 0;

		public const sbyte playViewDeck = 1;

		public const sbyte playViewGames = 2;

		public const sbyte playViewProfile = 3;

		public const sbyte playViewFriends = 4;

		public const sbyte playViewShowRoom = 5;

		public const sbyte playViewArena = 6;

		public const sbyte initialTime = 0;

		public const sbyte lastUserInputTime = 1;

		public const sbyte lastSuspendTime = 2;

		public const sbyte lastLong64 = 3;

		public long[] mLong64Array;

		public bool mIsSuspended;

		public bool mHasFocus;

		public bool mDirty;

		public bool mExitProcessed;

		public int mLastKeyDown;

		public short[] mKeyMap = new short[31];

		public int mKeyRepeatTime;

		public TimeSystem mApplicationTimeSystem;

		public bool mIgnoreSecondKeyDown;

		public Component mComponentWithFocus;

		public Scene mCurrentScene;

		public sbyte mCurrentTransitionType;

		public FlApplication()
		{
			mHasFocus = true;
			mDirty = true;
			mLastKeyDown = 0;
			mIgnoreSecondKeyDown = true;
			mCurrentTransitionType = 0;
			mApplicationTimeSystem = new TimeSystem();
			FrameworkGlobals.GetInstance().application = this;
			mComponentWithFocus = this;
			FlRect videoModeRect = DisplayManager.GetVideoModeRect();
			SetRect(videoModeRect.GetLeft(), videoModeRect.GetTop(), videoModeRect.GetWidth(), videoModeRect.GetHeight());
			FrameworkGlobals.GetInstance().penTracker = this;
			FlKeyManager.GetInstance();
			mLong64Array = new long[3];
			mLong64Array[0] = GetRealTime();
			mLong64Array[1] = mLong64Array[0];
			mLong64Array[2] = 0L;
		}

		public override void destruct()
		{
			FrameworkGlobals.GetInstance().mFlKeyManager = null;
			FrameworkGlobals.GetInstance().mFlLog = null;
			FrameworkGlobals.GetInstance().mFlPenManager = null;
			mComponentWithFocus = null;
			mLong64Array = null;
		}

		public virtual void FocusLost()
		{
		}

		public virtual void FocusGained()
		{
		}

		public virtual bool QuitRequest()
		{
			return true;
		}

		public virtual void SaveGame()
		{
		}

		public virtual void Suspend()
		{
		}

		public virtual void Resume()
		{
		}

		public virtual void OnScreenSizeChange()
		{
		}

		public virtual void OnSliderChange(bool a11)
		{
		}

		public virtual void OnMasterVolumeChange(short a12)
		{
		}

		public virtual void OnFrameLimit(int timeSpentThisFrameInMs)
		{
		}

		public virtual short GetGameLanguage()
		{
			return 11;
		}

		public static FlApplication GetInstance()
		{
			return FrameworkGlobals.GetInstance().application;
		}

		public static FlString GetJamdatBuildString()
		{
			return new FlString(GetVersionString());
		}

		public static void Kill()
		{
		}

		public static bool MemoryCardWasRemoved()
		{
			return false;
		}

		public static void Exit()
		{
			ProcessExit();
			OSExit();
		}

		public static void ProcessExit()
		{
			FlApplication instance = GetInstance();
			if (!instance.mExitProcessed)
			{
				Scene scene = instance.mCurrentScene;
				if (scene != null)
				{
					scene.SendMsg(scene, -109, -1);
				}
				instance.mExitProcessed = true;
			}
		}

		public static bool HasExited()
		{
			return GetInstance().mExitProcessed;
		}

		public static FlString GetTitle()
		{
			return new FlString();
		}

		public static FlString GetDir()
		{
			return new FlString();
		}

		public static FlString GetDataDir()
		{
			return new FlString();
		}

		public static sbyte GetMajorVersion()
		{
			return (sbyte)Constants.ApplicationMajorVersion();
		}

		public static sbyte GetMinorVersion()
		{
			return (sbyte)Constants.ApplicationMinorVersion();
		}

		public static short GetBuildVersion()
		{
			return (sbyte)Constants.ApplicationBuildVersion();
		}

		public static FlString GetVersionString()
		{
			FlString @string = new FlString(StringUtils.CreateString("."));
			FlString flString = new FlString(GetMajorVersion());
			flString.AddAssign(@string);
			flString.AddAssign(new FlString(GetMinorVersion()));
			flString.AddAssign(@string);
			flString.AddAssign(new FlString(GetBuildVersion()));
			return flString;
		}

		public static FlString GetDeviceID()
		{
			return GetPropertyValue(StringUtils.CreateString("DeviceID"));
		}

		public static FlString GetCarrierID()
		{
			return GetPropertyValue(StringUtils.CreateString("CarrierID"));
		}

		public static FlString GetPlatformID()
		{
			return StringUtils.CreateString("3");
		}

		public static FlString GetHardwareKeyString()
		{
			FlString other = new FlString(StringUtils.CreateString(","));
			FlString flString = new FlString(GetCarrierID());
			if (flString.Equals(StringUtils.CreateString("9999")))
			{
				return StringUtils.CreateString("1,2,390");
			}
			return flString.Add(other).Add(GetPlatformID()).Add(other)
				.Add(GetDeviceID());
		}

		public static short GetTCPPort()
		{
			return (short)GetPropertyValue(StringUtils.CreateString("TCPPort")).ToLong();
		}

		public static FlString GetServerAddr()
		{
			return GetPropertyValue(StringUtils.CreateString("ServerAddr"));
		}

		public static FlString GetPropertyValue(FlString propertyName)
		{
			return propertyName;
		}

		public virtual bool IsPropertySet(FlString propValue)
		{
			if (propValue.GetLength() > 0)
			{
				return !propValue.Equals(StringUtils.CreateString("NULL"));
			}
			return false;
		}

		public virtual void SetOSWaitCursorVisible(bool visible)
		{
		}

		public virtual void SetDpadDisable(bool active)
		{
		}

		public virtual void SetDirty(bool dirty)
		{
			mDirty = dirty;
		}

		public virtual bool IsDirty()
		{
			return mDirty;
		}

		public virtual void SendQuitRequestMsg()
		{
			SendMsg(this, -103, 0);
		}

		public virtual void SetIsSuspended(bool parIsSuspended)
		{
			mIsSuspended = parIsSuspended;
		}

		public virtual bool GetIsSuspended()
		{
			return mIsSuspended;
		}

		public virtual void SetHasFocus(bool parHasFocus)
		{
			mHasFocus = parHasFocus;
		}

		public virtual bool HasFocus()
		{
			return mHasFocus;
		}

		public virtual void SetFrameRate(short fps)
		{
		}

		public virtual void ReturnToOSGameMenu()
		{
		}

		public virtual void OnKeyFromOS(int physicalKey, bool up)
		{
			switch (physicalKey)
			{
			case 0:
				return;
			case 12:
				if (!up)
				{
					SoundManager.DecMasterVolume();
				}
				return;
			case 11:
				if (!up)
				{
					SoundManager.IncMasterVolume();
				}
				return;
			}
			ResetLastUserInputTime();
			int translatedKey = GetTranslatedKey(physicalKey);
			int num = mLastKeyDown;
			Component component = mComponentWithFocus;
			if (up)
			{
				if (num == physicalKey)
				{
					mLastKeyDown = 0;
					SendKeyUpMsg(translatedKey);
				}
				else if (mIgnoreSecondKeyDown)
				{
					FlLog.GetInstance().Log(5, 1, StringUtils.CreateString("Wrong keyUp from OS. Send right keyUp"));
					OnKeyFromOS(num, true);
				}
			}
			else if (num != 0)
			{
				FlLog.GetInstance().Log(5, 1, StringUtils.CreateString("2 OS keyDown. Send keyUp"));
				OnKeyFromOS(num, true);
				if (!mIgnoreSecondKeyDown)
				{
					OnKeyFromOS(physicalKey, false);
				}
			}
			else
			{
				mLastKeyDown = physicalKey;
				mKeyRepeatTime = int.MaxValue;
				SendKeyDownMsg(translatedKey);
				if (mComponentWithFocus == component)
				{
					SendKeyRepeatMsg(translatedKey);
				}
			}
		}

		public virtual void SendKeyUpToLastKeyDown()
		{
			OnKeyFromOS(mLastKeyDown, true);
		}

		public virtual bool KeyIsDown(int translatedKey)
		{
			if ((mKeyMap[mLastKeyDown] == 0 && translatedKey == mLastKeyDown) || translatedKey == mKeyMap[mLastKeyDown])
			{
				return translatedKey != 0;
			}
			return false;
		}

		public virtual void MapKey(int sourceKey, int translatedKeyOrGroup)
		{
			ResetDownKeys();
			mKeyMap[sourceKey] = (short)translatedKeyOrGroup;
		}

		public virtual void ResetDownKeys()
		{
			mLastKeyDown = 0;
		}

		public virtual int GetTranslatedKey(int physicalKey)
		{
			if (mKeyMap[physicalKey] != 0)
			{
				physicalKey = mKeyMap[physicalKey];
			}
			return physicalKey;
		}

		public virtual void UpdateSoftKey(int softKey, FlString label)
		{
			XNAApp.GetCurrentScene().updateSoftKey(softKey, label.NativeString);
		}

		public virtual int GetDefaultGamingKey(sbyte actionKey, sbyte orientation)
		{
			return 0;
		}

		public virtual sbyte GetSoftKeyPosition(int softKey, sbyte orientation)
		{
			return 0;
		}

		public virtual bool IsNativeResolution(int width, int height)
		{
			VideoMode videoMode = new VideoMode(DisplayManager.GetVideoMode());
			if (videoMode.GetWidth() == width)
			{
				return videoMode.GetHeight() == height;
			}
			return false;
		}

		public virtual int GetAllowedOrientations()
		{
			return 0;
		}

		public virtual int GetAllowedNativeOrientations()
		{
			return 0;
		}

		public virtual sbyte GetCurrentGameOrientation()
		{
			return 0;
		}

		public virtual bool SetGameOrientation(sbyte gameOrientation, bool smoothTransition)
		{
			return true;
		}

		public virtual bool GetIsSystemSelectedOrientation()
		{
			return true;
		}

		public virtual void SetCurrentFocus(Component newFocus)
		{
			newFocus = newFocus.ForwardFocus();
			Component component = mComponentWithFocus;
			mComponentWithFocus = newFocus;
			MoveFocusForNotifications(component, newFocus, newFocus.GetDepth() - component.GetDepth());
		}

		public virtual Component GetCurrentFocus()
		{
			return mComponentWithFocus;
		}

		public virtual void OnFocusLostFromOS()
		{
			if (HasFocus())
			{
				SetHasFocus(false);
				Invalidate();
				ReleaseAllInput();
				FocusLost();
			}
		}

		public virtual void OnFocusGainedFromOS()
		{
			if (!HasFocus())
			{
				SetHasFocus(true);
				Invalidate();
				FocusGained();
			}
		}

		public virtual void OnSuspendFromOS()
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("OnSuspendFromOS"));
			if (!GetIsSuspended())
			{
				SetIsSuspended(true);
				mLong64Array[2] = GetRealTime();
				Suspend();
			}
		}

		public virtual void OnResumeFromOS()
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("OnResumeFromOS()"));
			if (GetIsSuspended())
			{
				SetIsSuspended(false);
				mLong64Array[0] += GetRealTime() - mLong64Array[2];
				mLong64Array[2] = 0L;
				ResetLastUserInputTime();
				Resume();
				int osWidth = XNAApp.GetCurrentScene().getOsWidth();
				int osHeight = XNAApp.GetCurrentScene().getOsHeight();
				if (!IsNativeResolution(osWidth, osHeight))
				{
					FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("Resolution change detected after interrupt."));
					OnScreenSizeChangedFromOS(osWidth, osHeight);
				}
			}
		}

		public virtual void ReleaseAllInput()
		{
			SendKeyUpToLastKeyDown();
			FrameworkGlobals.GetInstance().penTracker.ReleasePen();
		}

		public virtual void Iteration(int totalTime, int delta)
		{
			int num = mLastKeyDown;
			int num2 = mKeyRepeatTime;
			if (num != 0)
			{
				if (num2 == int.MaxValue)
				{
					num2 = -350;
				}
				else
				{
					num2 += delta;
					if (num2 > 150)
					{
						SendKeyRepeatMsg(GetTranslatedKey(num));
						num2 = 0;
					}
				}
			}
			mKeyRepeatTime = num2;
			OnTime(totalTime, delta);
			mApplicationTimeSystem.OnTime(totalTime, delta);
		}

		public virtual sbyte GetTimeFormat()
		{
			return 1;
		}

		public virtual FlString[] GetCommandLineArgs()
		{
			return null;
		}

		public virtual int GetCommandLineArgsCount()
		{
			return 0;
		}

		public virtual bool GetGameKeyAltMode()
		{
			return FlKeyManager.GetInstance().GetGameKeyAltMode();
		}

		public virtual void SetGameKeyAltMode(bool active)
		{
			FlKeyManager.GetInstance().SetGameKeyAltMode(active);
		}

		public static long GetRunTime()
		{
			if (GetInstance().GetIsSuspended())
			{
				return GetInstance().mLong64Array[2] - GetInstance().mLong64Array[0];
			}
			return GetRealTime() - GetInstance().mLong64Array[0];
		}

		public static long GetRealTime()
		{
			return (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
		}

		public static TimeSystem GetGlobalTimeSystem()
		{
			return GetInstance().mApplicationTimeSystem;
		}

		public static string GetPropertyValue(string propertyName)
		{
			return propertyName;
		}

		public virtual void SetIgnoreSecondKeyDown(bool ignoreSecondKeyDown)
		{
			mIgnoreSecondKeyDown = ignoreSecondKeyDown;
		}

		public override Component GetHitTestComponent(short ptX, short ptY)
		{
			return base.GetHitTestComponent(ptX, ptY);
		}

		public virtual void OnScreenSizeChangedFromOS(int newWidth, int newHeight, bool forwardToApplication)
		{
			FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("OnScreenSizeChangedFromOS"), newWidth, newHeight);
			VideoMode videoMode = DisplayManager.GetMainDisplayContext().GetVideoMode();
			if (newWidth != videoMode.GetWidth() || newHeight != videoMode.GetHeight())
			{
				VideoMode resVideoMode = new VideoMode(newWidth, newHeight, videoMode.GetBpp());
				VideoMode videoMode2 = new VideoMode(DisplayManager.GetWindowVideoMode());
				videoMode2.SetWidth(newWidth);
				videoMode2.SetHeight(newHeight);
				DisplayManager.Initialize(videoMode2, resVideoMode, false);
				SetSize((short)newWidth, (short)newHeight);
				XNAApp.GetCurrentScene().UpdateDisplayContext((short)newWidth, (short)newHeight);
				if (forwardToApplication && !GetIsSuspended())
				{
					FlLog.GetInstance().Log(3, 0, StringUtils.CreateString("Send OnScreenSizeChange()"));
					OnScreenSizeChange();
				}
			}
		}

		public virtual void ResetLastUserInputTime()
		{
			mLong64Array[1] = GetRealTime();
		}

		public virtual long GetLastUserInputTime()
		{
			return mLong64Array[1];
		}

		public virtual int GetDeltaTimeSinceLastUserInput()
		{
			return (int)(GetRealTime() - mLong64Array[1]);
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			base.OnDraw(displayContext);
			DrawDevelopmentTag(displayContext);
		}

		public virtual Scene GetCurrentScene()
		{
			return mCurrentScene;
		}

		public virtual bool IsInSceneTransition()
		{
			return mCurrentTransitionType != 0;
		}

		public virtual void DrawDevelopmentTag(DisplayContext displayContext)
		{
		}

		public static void OSExit()
		{
			XNAApp.instance.OSExit();
		}

		public virtual void SendKeyDownMsg(int flightKey)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("Sending message keyDownMsg"));
			mComponentWithFocus.SendMsg(mComponentWithFocus, -120, flightKey);
		}

		public virtual void SendKeyUpMsg(int flightKey)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("Sending message keyUpMsg"));
			mComponentWithFocus.SendMsg(mComponentWithFocus, -121, flightKey);
		}

		public virtual void SendKeyRepeatMsg(int flightKey)
		{
			FlLog.GetInstance().Log(5, 0, StringUtils.CreateString("Sending message key repeat"));
			mComponentWithFocus.SendMsg(mComponentWithFocus, -119, flightKey);
		}

		public static void MoveFocusForNotifications(Component oldFocus, Component newFocus, int newDepthMinusOldDepth)
		{
			if (oldFocus == newFocus)
			{
				return;
			}
			int num = newDepthMinusOldDepth;
			Component component = null;
			if (newDepthMinusOldDepth <= 0)
			{
				oldFocus.OnFocusChange(false);
				oldFocus = oldFocus.GetViewport();
				num++;
				if (oldFocus == newFocus)
				{
					newFocus.OnFocusChange(true);
				}
			}
			if (newDepthMinusOldDepth >= 0)
			{
				component = newFocus;
				newFocus = newFocus.GetViewport();
				num--;
			}
			MoveFocusForNotifications(oldFocus, newFocus, num);
			if (component != null)
			{
				component.OnFocusChange(true);
			}
		}

		public virtual int GetDefaultGamingKey(sbyte actionKey)
		{
			return GetDefaultGamingKey(actionKey, 0);
		}

		public virtual sbyte GetSoftKeyPosition(int softKey)
		{
			return GetSoftKeyPosition(softKey, 0);
		}

		public virtual bool SetGameOrientation(sbyte gameOrientation)
		{
			return SetGameOrientation(gameOrientation, true);
		}

		public virtual void OnScreenSizeChangedFromOS(int newWidth, int newHeigth)
		{
			OnScreenSizeChangedFromOS(newWidth, newHeigth, true);
		}

		public static FlApplication[] InstArrayFlApplication(int size)
		{
			FlApplication[] array = new FlApplication[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlApplication();
			}
			return array;
		}

		public static FlApplication[][] InstArrayFlApplication(int size1, int size2)
		{
			FlApplication[][] array = new FlApplication[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlApplication[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlApplication();
				}
			}
			return array;
		}

		public static FlApplication[][][] InstArrayFlApplication(int size1, int size2, int size3)
		{
			FlApplication[][][] array = new FlApplication[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlApplication[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlApplication[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlApplication();
					}
				}
			}
			return array;
		}
	}
}
