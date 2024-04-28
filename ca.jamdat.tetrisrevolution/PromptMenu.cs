using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PromptMenu : SelectorMenu
	{
		public int mYesCmd;

		public int mNoCmd;

		public int mQuestionStringEntryPointId;

		public PromptMenu(int sceneId, int pkgId, int yesCmd, int noCmd, int questionStringEntryPoint)
			: base(sceneId, pkgId)
		{
			mYesCmd = yesCmd;
			mNoCmd = noCmd;
			mQuestionStringEntryPointId = questionStringEntryPoint;
			mType = 4;
		}

		public override void destruct()
		{
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
		}

		public override void Initialize()
		{
			base.Initialize();
			Package package = mPackage;
			Selection selection = null;
			Selection selection2 = null;
			selection = Selection.Cast(package.GetEntryPoint(5), null);
			selection2 = Selection.Cast(package.GetEntryPoint(6), null);
			selection.SetCommand((sbyte)mYesCmd);
			selection2.SetCommand((sbyte)mNoCmd);
			HorizontalSelector.Initialize(mSelector, mFocusedSelectionIndex);
			FlString flString = EntryPoint.GetFlString(package, mQuestionStringEntryPointId);
			Scroller scroller = EntryPoint.GetScroller(package, 3);
			Viewport viewport = EntryPoint.GetViewport(package, 7);
			int num = 0;
			num = viewport.GetRectHeight() - mSelector.GetRectHeight();
			Text text = (Text)scroller.GetElementAt(0);
			VerticalTextScroller.Initialize(scroller, flString);
			if (text.GetRectHeight() >= num)
			{
				Viewport viewport2 = (Viewport)scroller.GetChild(1);
				int rectWidth = viewport.GetRectWidth();
				int rectHeight = viewport2.GetRectHeight();
				viewport2.SetCenter((short)(rectWidth / 2), (short)(num - rectHeight / 2));
				scroller.GetScrollerViewport().SetSize((short)rectWidth, (short)(num - rectHeight));
				scroller.SetSize((short)rectWidth, (short)num);
				scroller.SetTopLeft(0, 0);
				mSelector.SetCenter((short)(rectWidth / 2), (short)(viewport.GetRectHeight() - mSelector.GetRectHeight() / 2));
			}
			else
			{
				short rectWidth2 = viewport.GetRectWidth();
				int num2 = viewport.GetRectHeight() / 2;
				int num3 = 0;
				num3 = (scroller.GetTotalScrollingSize() + mSelector.GetRectHeight()) / 2;
				int totalScrollingSize = scroller.GetTotalScrollingSize();
				Viewport viewport3 = (Viewport)scroller.GetChild(1);
				viewport3.SetTopLeft(0, 0);
				viewport3.SetVisible(false);
				scroller.GetScrollerViewport().SetSize(rectWidth2, (short)totalScrollingSize);
				scroller.SetSize(rectWidth2, (short)totalScrollingSize);
				scroller.SetTopLeft(0, (short)(num2 - num3));
				mSelector.SetTopLeft(0, (short)(num2 - num3 + scroller.GetRectHeight()));
			}
			scroller.ResetScroller();
			if (GetId() == 31)
			{
				mSelectSoftKey.SetFunction(0, -11);
				mClearSoftKey.SetFunction(1, -12);
			}
			else if (GetId() == 7)
			{
				mSelectSoftKey.SetFunction(0, -10);
				mClearSoftKey.SetFunction(2, -11);
			}
			else
			{
				mSelectSoftKey.SetFunction(0, -10);
				mClearSoftKey.SetFunction(1, -12);
			}
		}

		public override void StartOpeningAnims()
		{
		}

		public override void StartClosingAnims()
		{
		}

		public override void CreateOpeningAnims()
		{
		}

		public override void CreateClosingAnims(int startTime)
		{
		}

		public override bool OnMsg(Component source, int msg, int intParam)
		{
			bool flag = false;
			if (!flag)
			{
				Scroller scroller = null;
				scroller = Scroller.Cast(mPackage.GetEntryPoint(3), null);
				flag = scroller.OnDefaultMsg(source, msg, intParam) || base.OnMsg(source, msg, intParam);
			}
			return flag;
		}

		public override bool OnCommand(int command)
		{
			bool result = base.OnCommand(command);
			if (command == -75 || command == -76)
			{
				result = base.OnCommand(-2);
			}
			return result;
		}
	}
}
