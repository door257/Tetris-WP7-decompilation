using ca.jamdat.flight;
using Microsoft.Xna.Framework.Media;

namespace ca.jamdat.tetrisrevolution
{
	public class BaseScene : BaseController
	{
		public const int stateUnloaded = 0;

		public const int stateLoading = 1;

		public const int stateSavingFiles = 2;

		public const int stateInitializing = 3;

		public const int stateAttaching = 4;

		public const int statePlayingOpeningAnim = 5;

		public const int stateReady = 6;

		public const int statePlayingClosingAnim = 7;

		public const int stateSerializingObjects = 8;

		public const int stateUnloading = 9;

		public const int stateCount = 10;

		public const int popupHid = 1;

		public const int ctxLoading = 0;

		public const int ctxUnloading = 1;

		public int mPackageId;

		public int mType;

		public View mView;

		public MetaPackage mMetaPackage;

		public Package mPackage;

		public Viewport mViewport;

		public Softkey mSelectSoftKey;

		public Softkey mClearSoftKey;

		public Cursor mCursor;

		public int mId;

		public int mTransitionState;

		public int mRefCount;

		public BaseScene mPrevScene;

		public BaseScene mNextScene;

		public BaseScene(int sceneId, int packageId)
		{
			mPackageId = packageId;
			mType = 0;
			mId = sceneId;
			mTransitionState = 0;
			mSelectSoftKey = new Softkey();
			mClearSoftKey = new Softkey();
			mCursor = new Cursor();
		}

		public override void destruct()
		{
			mSelectSoftKey = null;
			mClearSoftKey = null;
			mCursor = null;
		}

		public virtual int GetId()
		{
			return mId;
		}

		public virtual bool IsTypeOf(int type)
		{
			return (mType & type) != 0;
		}

		public virtual void AddType(int type)
		{
			mType |= type;
		}

		public virtual void SetViewport(Viewport viewport)
		{
			if (mView != null)
			{
				mView.SetViewport(viewport);
			}
		}

		public virtual void Suspend()
		{
		}

		public virtual void Resume()
		{
			if (Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
			{
				if (GetId() != 40)
				{
					StartMusic();
				}
			}
			else
			{
				Settings settings = GameApp.Get().GetSettings();
				settings.SetSoundEnabled2(false);
			}
		}

		public virtual void StartMusic()
		{
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool result = false;
			if ((msg == -127 || msg == -126 || msg == -128 || msg == -125 || msg == -122) && (msg != -122 || intParam != 0))
			{
				SubtypeHandler.OnSubtype(this, source, msg, intParam);
			}
			switch (msg)
			{
			case -121:
			case -120:
			case -119:
				result = OnKeyMsg(source, msg, intParam);
				break;
			case -125:
				result = OnPushedMsg((Selection)source, msg, intParam);
				break;
			case -118:
			case -117:
			case -116:
				GameApp.Get().GetPenInputController().OnPenMsg(source, msg, intParam);
				break;
			case -127:
				result = OnSelectedMsg((Selection)source, intParam);
				break;
			}
			return result;
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
		}

		public virtual bool OnCommand(int command)
		{
			if (command == 0)
			{
				return false;
			}
			return GameApp.Get().GetCommandHandler().Execute(command);
		}

		public virtual void OnScreenSizeChange()
		{
		}

		public virtual bool OnSelectedMsg(Selection source, int intParam)
		{
			return false;
		}

		public virtual bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return false;
		}

		public virtual void SetTransitionState(int state)
		{
			mTransitionState = state;
		}

		public virtual int GetTransitionState()
		{
			return mTransitionState;
		}

		public virtual void Load()
		{
			mMetaPackage = GameLibrary.GetPackage(mPackageId);
			mPackage = mMetaPackage.GetPackage();
		}

		public virtual bool IsLoaded()
		{
			return GameLibrary.IsPackageLoaded(mMetaPackage);
		}

		public virtual void GetEntryPoints()
		{
			Package package = mPackage;
			mViewport = Viewport.Cast(package.GetEntryPoint(0), null);
			package.SetNextEntryPointIndex(2);
		}

		public virtual void OnSceneAttached()
		{
			if (mViewport != null)
			{
				Viewport viewport = EntryPoint.GetViewport(1310760, 186);
				viewport.SetViewport(mViewport);
				viewport.SendToBack();
			}
		}

		public virtual void CreateView()
		{
			mView = new View();
		}

		public virtual void DeleteView()
		{
			mView = null;
		}

		public virtual View GetView()
		{
			return mView;
		}

		public virtual void RemoveSoftKeysCommand()
		{
			mSelectSoftKey.SetFunction(0, 0);
			mClearSoftKey.SetFunction(1, 0);
		}

		public virtual void Initialize()
		{
			if (mView == null)
			{
				GameApp gameApp = GameApp.Get();
				gameApp.SetRect(DisplayManager.GetMainDisplayContext().GetScreenRect());
				CreateView();
				mView.SetController(this);
				mView.SetRect(gameApp.GetRectLeft(), gameApp.GetRectTop(), gameApp.GetRectWidth(), gameApp.GetRectHeight());
				GetEntryPoints();
				InitializeConsoleWindowFont();
			}
			if (mCursor != null)
			{
				mCursor.Initialize();
			}
			if (mViewport != null)
			{
				Viewport viewport = EntryPoint.GetViewport(mPackage, 1);
				mSelectSoftKey.Initialize(5, (Selection)viewport.GetChild(2));
				mClearSoftKey.Initialize(6, (Selection)viewport.GetChild(3));
				mViewport.SetViewport(mView);
			}
		}

		private void InitializeConsoleWindowFont()
		{
		}

		public virtual void StartOpeningAnims()
		{
		}

		public virtual bool IsOpeningAnimsEnded()
		{
			return true;
		}

		public virtual void OnOpeningAnimsEnded()
		{
		}

		public virtual void CreateOpeningAnims()
		{
		}

		public virtual void CreateClosingAnims(int startTime)
		{
		}

		public virtual void ReceiveFocus()
		{
			mView.TakeFocus();
			GameApp.Get().GetPenInputController().Activate();
		}

		public virtual void StartClosingAnims()
		{
		}

		public virtual bool IsClosingAnimsEnded()
		{
			return true;
		}

		public virtual void Unload()
		{
			if (mView != null)
			{
				mView.UnRegisterInGlobalTime();
			}
			mSelectSoftKey.Uninitialize();
			mClearSoftKey.Uninitialize();
			if (mCursor != null)
			{
				mCursor.Unload();
			}
			if (mViewport != null)
			{
				mViewport.SetViewport(null);
				mViewport = null;
			}
			GameApp.Get().GetPenInputController().Deactivate();
			if (mPackage != null)
			{
				GameLibrary.ReleasePackage(mMetaPackage);
				mMetaPackage = null;
				mPackage = null;
			}
		}

		public virtual bool SaveFiles(int ctx)
		{
			return true;
		}

		public virtual void SerializeObjects()
		{
		}

		public virtual BaseScene GetPreviousScene()
		{
			return mPrevScene;
		}

		public virtual BaseScene GetNextScene()
		{
			return mNextScene;
		}

		public virtual void SetCursor(Cursor newCursor)
		{
			mCursor = newCursor;
		}

		public virtual Cursor GetCursor()
		{
			return mCursor;
		}

		public virtual Viewport GetSoftkeyViewport()
		{
			return EntryPoint.GetViewport(mPackage, 1);
		}

		public virtual void AttachComponentBehindSoftkeyViewport(Component component)
		{
			Viewport viewport = EntryPoint.GetViewport(mPackage, 1);
			viewport.BringToFront();
			component.PutInFront(viewport);
		}

		public virtual bool IsReadyForCommands()
		{
			GameApp gameApp = GameApp.Get();
			return gameApp.GetCurrentFocus() != gameApp;
		}

		public virtual void CreateTouchZones()
		{
		}

		public virtual bool OnKeyDown(int key)
		{
			return false;
		}

		public virtual bool OnKeyUp(int key)
		{
			return false;
		}

		public virtual bool OnKeyDownOrRepeat(int key)
		{
			return false;
		}

		public virtual bool OnPushedMsg(Selection selection, int msg, int intParam)
		{
			bool result = false;
			if (intParam == 0)
			{
				result = OnCommand(selection.GetCommand());
			}
			return result;
		}

		public virtual bool OnKeyMsg(Component source, int msg, int intParam)
		{
			bool flag = msg == -121;
			bool flag2 = false;
			if (msg != -119)
			{
				flag2 = GameApp.Get().GetCheatActivationController().OnKey(GetId(), intParam, flag);
			}
			if (!flag2)
			{
				flag2 = GameApp.Get().GetSceneSlideShowController().OnKey(intParam, flag);
			}
			if (!flag2)
			{
				flag2 = (flag ? OnKeyUp(intParam) : ((msg != -119) ? OnKeyDown(intParam) : OnKeyDownOrRepeat(intParam)));
			}
			if (!flag2)
			{
				switch (intParam)
				{
				case 6:
					mClearSoftKey.SetPushed(!flag);
					return true;
				case 5:
					mSelectSoftKey.SetPushed(!flag);
					return true;
				}
			}
			return flag2;
		}

		public virtual bool IsIdentified()
		{
			if (GetId() != 0)
			{
				return mType != 0;
			}
			return false;
		}

		public virtual void AddRef()
		{
		}

		public virtual void RemoveRef()
		{
		}

		protected void ManageSoftkeysVisibility(Popup currentPopup, bool isVisible)
		{
			Viewport softkeyViewport = GetSoftkeyViewport();
			if (currentPopup != null)
			{
				softkeyViewport.GetChild(2).SetPassThrough(false);
				softkeyViewport.GetChild(0).SetVisible(false);
				softkeyViewport.GetChild(1).SetVisible(false);
				softkeyViewport.GetChild(2).SetVisible(false);
				softkeyViewport.GetChild(3).SetVisible(false);
			}
			else
			{
				softkeyViewport.GetChild(0).SetVisible(isVisible);
				softkeyViewport.GetChild(1).SetVisible(isVisible);
				softkeyViewport.GetChild(2).SetVisible(isVisible);
				softkeyViewport.GetChild(3).SetVisible(isVisible);
				softkeyViewport.GetChild(2).SetPassThrough(true);
				softkeyViewport.GetChild(3).SetPassThrough(true);
			}
		}
	}
}
