using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RealisationsMenu : RotatingSelectorMenu
	{
		public const int stateSelectMode = 0;

		public const int stateDisplayingFeatsPopup = 1;

		public const int stateDisplayingCareerFeatsPopup = 2;

		public const int stateDisplayingAdvancedFeatsPopup = 3;

		public const int stateDisplayingStatsPopup = 4;

		public const int stateDisplayingScorePopup = 5;

		public const int stateDisplayingVariantsPopup = 6;

		public const int stateDisplayingUnlockedPopup = 7;

		public const int stateDisplayingDemoFeatLockedPopup = 8;

		public const int realisationsMenuStateCount = 9;

		public int mCurrentState;

		public Popup mCurrentPopup;

		public Popup mHidingPopup;

		public int mSelectionIndex;

		public int mCategoryIndex;

		public RealisationsMenu(int sceneId, int packageId)
			: base(sceneId, packageId)
		{
			mCurrentState = 0;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
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
				case 68:
					flag = InitializeMenuPopup(0);
					mCurrentState = 1;
					break;
				case 71:
					flag = InitializeMenuPopup(3);
					mCurrentState = 4;
					break;
				case 72:
					flag = InitializeMenuPopup(4);
					mCurrentState = 5;
					break;
				case 73:
					flag = InitializeMenuPopup(5);
					mCurrentState = 6;
					break;
				case -33:
					flag = InitializeMenuPopup(56);
					mCurrentState = 8;
					break;
				}
			}
			else if (mCurrentState == 1)
			{
				switch (command)
				{
				case 69:
				{
					mSelectionIndex = ((RealisationsFeatsPopup)mCurrentPopup).GetSelectionIndex();
					mCategoryIndex = ((RealisationsFeatsPopup)mCurrentPopup).GetCategoryIndex();
					sbyte careerFeat = (sbyte)mSelectionIndex;
					flag = InitializeMenuPopup(1);
					((CareerFeatPopup)mCurrentPopup).SetCareerFeat(careerFeat);
					mCurrentState = 2;
					break;
				}
				case 70:
				{
					mSelectionIndex = ((RealisationsFeatsPopup)mCurrentPopup).GetSelectionIndex();
					mCategoryIndex = ((RealisationsFeatsPopup)mCurrentPopup).GetCategoryIndex();
					sbyte advancedFeat = (sbyte)mSelectionIndex;
					flag = InitializeMenuPopup(2);
					((AdvancedFeatPopup)mCurrentPopup).SetAdvancedFeat(advancedFeat);
					mCurrentState = 3;
					break;
				}
				}
			}
			else if (mCurrentState == 6)
			{
				if (command == 29)
				{
					mSelectionIndex = ((RealisationsVariantsPopup)mCurrentPopup).GetSelectionIndex();
					int variant = mSelectionIndex;
					flag = InitializeMenuPopup(6);
					((UnlockPopup)mCurrentPopup).SetVariant(variant);
					mCurrentState = 7;
				}
			}
			else if (mCurrentState == 2 || mCurrentState == 3)
			{
				if (command == 4)
				{
					flag = InitializeMenuPopup(0);
					mCurrentState = 1;
				}
			}
			else if (mCurrentState == 7 && command == 4)
			{
				flag = InitializeMenuPopup(5);
				mCurrentState = 6;
			}
			if (!flag)
			{
				switch (command)
				{
				case 4:
					ReturnToStartingState();
					flag = true;
					break;
				case -10:
					if (mCurrentPopup == null)
					{
						int singleSelection = mSelector.GetSingleSelection();
						Selection selectionAt = mSelector.GetSelectionAt(singleSelection);
						flag = OnCommand(selectionAt.GetCommand());
					}
					else
					{
						flag = mCurrentPopup.OnCommand(command);
					}
					break;
				}
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

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			base.OnTime(totalTimeMs, deltaTimeMs);
			if (mHidingPopup == null && mCurrentPopup != null && mCurrentPopup.IsLoaded())
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
		}

		public virtual bool InitializeMenuPopup(sbyte menuPopupId)
		{
			bool result = true;
			if (mCurrentPopup != null)
			{
				mHidingPopup = mCurrentPopup;
				mHidingPopup.Hide();
				mCurrentPopup = null;
			}
			mCurrentPopup = Popup.CreateRealisationsMenuPopup(menuPopupId, this, mSelectSoftKey, mClearSoftKey);
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
			ManageSoftkeysVisibility(mCurrentPopup, false);
			mCurrentPopup.Show();
			if (mCurrentState == 1)
			{
				((RealisationsFeatsPopup)mCurrentPopup).SetCategoryIndex(mCategoryIndex);
				((RealisationsFeatsPopup)mCurrentPopup).SetSelectionIndex(mSelectionIndex);
			}
			else if (mCurrentState == 6)
			{
				((RealisationsVariantsPopup)mCurrentPopup).SetSelectionIndex(mSelectionIndex);
			}
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
			mCategoryIndex = 0;
			mSelectionIndex = 0;
			mSelectSoftKey.SetFunction(7, -10);
			mClearSoftKey.SetFunction(1, -12);
			ManageSoftkeysVisibility(mCurrentPopup, false);
		}
	}
}
