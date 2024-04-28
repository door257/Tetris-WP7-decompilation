using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SetupLevelSelectMenuPopup : Popup
	{
		public const sbyte setupLevelSelectPlayMenu = 0;

		public const sbyte setupLevelSelectMarathon = 1;

		public sbyte mId;

		public int mVariant;

		private int mSelectedLevel;

		private Text mTitleText;

		private Text mSelectedLevelText;

		private FlFont mNormalFont;

		private FlFont mHighlightFont;

		private Selection mSelectedLevelSelection;

		private Selection mPreviousSelection;

		private Selection mCurrentSelection;

		private IndexedSprite mPlayButtonSprite;

		public SetupLevelSelectMenuPopup(sbyte id, BaseScene baseScene, int variant, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mId = id;
			mVariant = variant;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2785365);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mTitleText = EntryPoint.GetText(package, 0);
			mSelectedLevelText = EntryPoint.GetText(package, 4);
			mNormalFont = EntryPoint.GetFlFont(package, 5);
			mHighlightFont = EntryPoint.GetFlFont(package, 6);
			mPlayButtonSprite = EntryPoint.GetIndexedSprite(package, 3);
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mScrollbarViewport = EntryPoint.GetViewport(package, 2);
			for (int i = 0; i < 15; i++)
			{
				mSelectedLevelSelection = EntryPoint.GetSelection(package, 8 + i);
				IndexedSprite indexedSprite = (IndexedSprite)mSelectedLevelSelection.GetChild(0);
				indexedSprite.SetCurrentFrame(1);
				indexedSprite.SetTopLeft(0, 0);
				Text text = (Text)mSelectedLevelSelection.GetChild(1);
				text.SetFont(mNormalFont);
				text.SetTopLeft((short)(mSelectedLevelSelection.GetRectWidth() / 2 - text.GetRectWidth() / 2), 15);
				text.SetAlignment(1);
				mSelectedLevelSelection.mSelected = false;
			}
		}

		public int GetSelectedLevel()
		{
			return mSelectedLevel;
		}

		public void SetSelectedLevel(int lvl)
		{
			mSelectedLevel = lvl;
		}

		public override void Initialize()
		{
			base.Initialize();
			ProgressionExpert progressionExpert = ProgressionExpert.Get();
			FlString flString = new FlString("_");
			if (mId == 0)
			{
				int num = FlMath.Maximum(progressionExpert.GetHighestLevelDone(mVariant) - 1, 0);
				mSelectedLevelText.SetCaption(new FlString(num + 1));
				SetSelectedLevel(num);
				flString.AddAssign(Utilities.GetGameVariantString(mVariant));
				mSelectSoftKey.SetFunction(3, 36);
				mClearSoftKey.SetFunction(1, 34);
				mSelectedLevelSelection = EntryPoint.GetSelection(2785365, 8 + num);
				IndexedSprite indexedSprite = (IndexedSprite)mSelectedLevelSelection.GetChild(0);
				indexedSprite.SetCurrentFrame(3);
				indexedSprite.SetTopLeft(-15, -12);
				Text text = (Text)mSelectedLevelSelection.GetChild(1);
				text.SetFont(mHighlightFont);
				text.SetTopLeft((short)(mSelectedLevelSelection.GetRectWidth() / 2 - text.GetRectWidth() / 2), 2);
				text.SetAlignment(1);
				mSelectedLevelSelection.mSelected = true;
				mCurrentSelection = mSelectedLevelSelection;
			}
			else
			{
				int lastMarathonDifficultyPlayed = progressionExpert.GetLastMarathonDifficultyPlayed();
				mSelectedLevelText.SetCaption(new FlString(lastMarathonDifficultyPlayed + 1));
				SetSelectedLevel(lastMarathonDifficultyPlayed);
				flString.AddAssign(EntryPoint.GetFlString(-2144239522, 10));
				mClearSoftKey.SetFunction(1, 4);
				mSelectedLevelSelection = EntryPoint.GetSelection(2785365, 8 + lastMarathonDifficultyPlayed);
				IndexedSprite indexedSprite2 = (IndexedSprite)mSelectedLevelSelection.GetChild(0);
				indexedSprite2.SetCurrentFrame(3);
				indexedSprite2.SetTopLeft(-15, -12);
				Text text2 = (Text)mSelectedLevelSelection.GetChild(1);
				text2.SetFont(mHighlightFont);
				text2.SetTopLeft((short)(mSelectedLevelSelection.GetRectWidth() / 2 - text2.GetRectWidth() / 2), 2);
				text2.SetAlignment(1);
				mSelectedLevelSelection.mSelected = true;
				mCurrentSelection = mSelectedLevelSelection;
			}
			mTitleText.SetCaption(flString);
			mPlayButtonSprite.SetCurrentFrame(13);
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (msg == -127)
			{
				Selection selection = (Selection)source;
				if (selection.GetCommand() != 36)
				{
					mPreviousSelection = mCurrentSelection;
					mCurrentSelection = selection;
					if (mCurrentSelection.GetCommand() == 37)
					{
						IndexedSprite indexedSprite = (IndexedSprite)mCurrentSelection.GetChild(0);
						indexedSprite.SetCurrentFrame(3);
						indexedSprite.SetTopLeft(-15, -12);
						Text text = (Text)mCurrentSelection.GetChild(1);
						text.SetFont(mHighlightFont);
						text.SetTopLeft((short)(mCurrentSelection.GetRectWidth() / 2 - text.GetRectWidth() / 2), 2);
						text.SetAlignment(1);
						FlString flString = new FlString(text.GetCaption());
						mSelectedLevelText.SetCaption(flString);
						SetSelectedLevel(flString.ToLong() - 1);
					}
					if (mPreviousSelection != null)
					{
						mPreviousSelection.mSelected = false;
						Text text2 = (Text)mPreviousSelection.GetChild(1);
						text2.SetFont(mNormalFont);
						text2.SetTopLeft((short)(mPreviousSelection.GetRectWidth() / 2 - text2.GetRectWidth() / 2), 15);
						text2.SetAlignment(1);
						IndexedSprite indexedSprite2 = (IndexedSprite)mPreviousSelection.GetChild(0);
						indexedSprite2.SetCurrentFrame(1);
						indexedSprite2.SetTopLeft(0, 0);
					}
				}
				flag = true;
			}
			if (!flag)
			{
				return base.OnMsg(source, msg, intParam);
			}
			return true;
		}
	}
}
