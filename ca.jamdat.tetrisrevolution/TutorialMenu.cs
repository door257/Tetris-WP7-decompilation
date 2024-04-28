using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class TutorialMenu : HorizontalSelectorPopup
	{
		public FlString[] mContentStrings;

		public Sprite mHeaderSprite;

		public FlBitmap[] mHeaderBitmaps;

		public TutorialMenu(BaseScene baseScene, Softkey selectSoftKey, Softkey clearSoftKey)
			: base(baseScene, selectSoftKey, clearSoftKey)
		{
			mAutoResize = true;
		}

		public override void destruct()
		{
		}

		public override void Load()
		{
			base.Load();
			mContentMetaPackage = GameLibrary.GetPackage(2031678);
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			Package package = mContentMetaPackage.GetPackage();
			mContentStrings = new FlString[6];
			mHeaderBitmaps = new FlBitmap[6];
			mPopupViewport = EntryPoint.GetViewport(package, 1);
			mSelector = EntryPoint.GetSelector(package, 4);
			mContentScroller = EntryPoint.GetScroller(package, 2);
			mScrollbarViewport = EntryPoint.GetViewport(package, 3);
			int num = 6;
			if (GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad())
			{
				num--;
			}
			for (int i = 0; i < num; i++)
			{
				if (!GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad())
				{
					mContentStrings[i] = EntryPoint.GetFlString(package, 5 + i);
					if (i == 0)
					{
						mHeaderBitmaps[i] = null;
					}
					else
					{
						mHeaderBitmaps[i] = EntryPoint.GetFlBitmap(package, 16 + i);
					}
				}
				else
				{
					mContentStrings[i] = EntryPoint.GetFlString(package, 11 + i);
					mHeaderBitmaps[i] = EntryPoint.GetFlBitmap(package, 22 + i);
				}
			}
			mHeaderSprite = EntryPoint.GetSprite(package, 16);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Unload()
		{
			for (int i = 0; i < 6; i++)
			{
				mContentStrings[i] = null;
				mHeaderBitmaps[i] = null;
			}
			mContentStrings = null;
			mHeaderBitmaps = null;
			mHeaderSprite = null;
			base.Unload();
		}

		public override void OnShowPopup()
		{
			base.OnShowPopup();
			AddTouchToContinueZone();
			if (mBaseScene.GetId() == 40)
			{
				mSelectSoftKey.SetFunction(7, 4);
				mClearSoftKey.SetFunction(7, 4);
			}
			else
			{
				mSelectSoftKey.SetFunction(7, 32);
				mClearSoftKey.SetFunction(7, 35);
			}
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition)
		{
			return OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, null);
		}

		public override bool OnTouchCommand(int command, int zoneId, Vector2_short firstPenPosition, Vector2_short lastPenPosition, Selector popupSelector)
		{
			bool flag = false;
			short absoluteTop = mSelector.GetNextArrow().GetViewport().GetAbsoluteTop();
			short num = (short)(absoluteTop + mSelector.GetNextArrow().GetViewport().GetRectHeight());
			if (lastPenPosition.GetY() > absoluteTop && lastPenPosition.GetY() < num)
			{
				return false;
			}
			if (command == 98)
			{
				flag = ((mBaseScene.GetId() != 40) ? GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(32) : GameApp.Get().GetCommandHandler().GetCurrentScene()
					.OnCommand(4));
			}
			if (!flag)
			{
				return base.OnTouchCommand(command, zoneId, firstPenPosition, lastPenPosition, popupSelector);
			}
			return true;
		}

		public override void Show()
		{
			mSelectSoftKey.SetFunction(7, 0);
			mClearSoftKey.SetFunction(7, 0);
			base.Show();
		}

		public override void Update()
		{
			mHeaderSprite.SetBitmap(mHeaderBitmaps[mFocusedSelectionIndex]);
			VerticalTextScroller.InitializeWithHeader(mContentScroller, new FlString(mContentStrings[mFocusedSelectionIndex]));
			Text text = (Text)mContentScroller.GetElementAt(1);
			text.SetAlignment(1);
			short x = text.GetTopLeft().GetX();
			text.GetTopLeft().GetY();
			if (!GameApp.Get().GetGameSettings().IsTouchModeVirtualDPad() && mFocusedSelectionIndex == 0)
			{
				text.SetTopLeft(x, 80);
			}
			else
			{
				text.SetTopLeft(x, 150);
			}
		}
	}
}
