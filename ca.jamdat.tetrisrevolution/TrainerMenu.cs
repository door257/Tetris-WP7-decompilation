using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TrainerMenu : RotatingSelectorMenu
	{
		public const int stateSelectMode = 0;

		public const int stateDisplayingGlossaryPopup = 1;

		public const int stateDisplayingMasterReplayPopup = 2;

		public const int stateDisplayingDemoFeatLockedPopup = 3;

		public const int trainerMenuStateCount = 4;

		public const int none = 0;

		public const int glossaryPopUp = 1;

		public const int masterPopUp = 2;

		public int mCurrentState;

		public Popup mCurrentPopup;

		public Popup mHidingPopup;

		public int mPopToLaunch;

		public int mPopUpInitialSelection;

		public int mTrainerInitialSelection;

		public TrainerMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			mCurrentState = 0;
			mPopToLaunch = 0;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			SetInitialSelection(mTrainerInitialSelection);
			base.Initialize();
			ReturnToStartingState();
		}

		public override void Unload()
		{
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			if (mHidingPopup != null)
			{
				mHidingPopup.Unload();
				mHidingPopup = null;
			}
			if (mView != null)
			{
				mView.UnRegisterInGlobalTime();
			}
			base.Unload();
		}

		public override bool OnCommand(int command)
		{
			if (!IsReadyForCommands())
			{
				return true;
			}
			bool flag = false;
			if (mCurrentState == 0)
			{
				switch (command)
				{
				case 40:
					flag = InitializeMenuPopup(50);
					mCurrentState = 1;
					if (mCurrentPopup != null)
					{
						((HorizontalSelectorPopup)mCurrentPopup).SetInitialSelectionIndex(mPopUpInitialSelection);
						mPopUpInitialSelection = 0;
					}
					break;
				case 47:
					flag = InitializeMenuPopup(51);
					mCurrentState = 2;
					if (mCurrentPopup != null)
					{
						((HorizontalSelectorPopup)mCurrentPopup).SetInitialSelectionIndex(mPopUpInitialSelection);
						mPopUpInitialSelection = 0;
					}
					break;
				case -33:
					flag = InitializeMenuPopup(56);
					mCurrentState = 3;
					break;
				}
			}
			else if (command == 4)
			{
				ReturnToStartingState();
				flag = true;
			}
			else
			{
				flag = mCurrentPopup.OnCommand(command);
			}
			if (!flag)
			{
				return base.OnCommand(command);
			}
			return true;
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (msg == 1)
			{
				DeletePopup();
				flag = true;
			}
			if (!flag)
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}

		public override void OnScreenSizeChange()
		{
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			base.OnScreenSizeChange();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			if (mCurrentPopup != null && mCurrentPopup.IsLoaded())
			{
				PreparePopup();
			}
		}

		public override void ReceiveFocus()
		{
			if (mHidingPopup != null)
			{
				GameApp.Get().TakeFocus();
			}
			else if (mCurrentState != 0)
			{
				mCurrentPopup.TakeFocus();
				EnableSelector(false);
			}
			else
			{
				base.ReceiveFocus();
				EnableSelector(true);
			}
			if (mPopToLaunch != 0)
			{
				int command = 0;
				if (mPopToLaunch == 1)
				{
					command = 40;
				}
				else if (mPopToLaunch == 2)
				{
					command = 47;
				}
				mPopToLaunch = 0;
				OnCommand(command);
			}
		}

		public virtual void SetPopUpToLauchOnEntry(int popUpToDisplay, int selectionIndex)
		{
			mPopToLaunch = popUpToDisplay;
			mPopUpInitialSelection = selectionIndex;
			mTrainerInitialSelection = 0;
			if (popUpToDisplay == 2)
			{
				mTrainerInitialSelection = 1;
			}
			if (popUpToDisplay == 1)
			{
				mTrainerInitialSelection = 0;
			}
		}

		public override void StartClosingAnims()
		{
			base.StartClosingAnims();
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Hide();
			}
		}

		public virtual bool InitializeMenuPopup(sbyte trainerMenuPopupId, sbyte popupAnimType)
		{
			bool result = true;
			mFocusedSelectionIndex = mSelector.GetSingleSelection();
			if (mCurrentPopup != null)
			{
				mHidingPopup = mCurrentPopup;
				mHidingPopup.Hide();
				mCurrentPopup = null;
			}
			mCurrentPopup = Popup.CreateTrainerMenuPopup(trainerMenuPopupId, this, mSelectSoftKey, mClearSoftKey);
			mCurrentPopup.SetAnimationType(popupAnimType);
			mCurrentPopup.Load();
			mView.RegisterInGlobalTime();
			return result;
		}

		public virtual void PreparePopup()
		{
			mView.UnRegisterInGlobalTime();
			mCurrentPopup.GetEntryPoints();
			mCurrentPopup.Initialize();
			mCurrentPopup.AttachPopupViewport(mViewport);
			if (mCurrentState != 0)
			{
				mFocusedSelectionIndex = mSelector.GetSingleSelection();
			}
			ManageSoftkeysVisibility(mCurrentPopup, true);
			mCurrentPopup.Show();
			mCurrentPopup.TakeFocus();
		}

		public virtual void DeletePopup()
		{
			if (mHidingPopup == null)
			{
				mCurrentPopup.Unload();
				mCurrentPopup = null;
			}
			else
			{
				mHidingPopup.Unload();
				mHidingPopup = null;
			}
		}

		public virtual void ReturnToStartingState()
		{
			mCurrentState = 0;
			if (mCurrentPopup != null)
			{
				mCurrentPopup.Hide();
			}
			mSelectSoftKey.SetFunction(7, -10);
			mClearSoftKey.SetFunction(1, -12);
			ManageSoftkeysVisibility(mCurrentPopup, false);
		}

		public virtual bool InitializeMenuPopup(sbyte trainerMenuPopupId)
		{
			return InitializeMenuPopup(trainerMenuPopupId, 0);
		}
	}
}
