using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class MessageMenu : BaseScene
	{
		public int mMessageStringEntryIdx;

		public int mSelectSoftkeyCmd;

		public int mClearSoftkeyCmd;

		public int mSelectSoftkeyFunction;

		public int mClearSoftkeyFunction;

		public MessageMenu(int sceneId, int stringEntryPointIdx, int selectSoftkeyCmd, int selectSoftkeyFunction, int clearSoftkeyCmd, int clearSoftkeyFunction)
			: base(sceneId, 1802295)
		{
			mMessageStringEntryIdx = stringEntryPointIdx;
			mSelectSoftkeyCmd = selectSoftkeyCmd;
			mClearSoftkeyCmd = clearSoftkeyCmd;
			mSelectSoftkeyFunction = selectSoftkeyFunction;
			mClearSoftkeyFunction = clearSoftkeyFunction;
			mType = 4;
		}

		public override void destruct()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			Text text = null;
			FlString flString = null;
			text = Text.Cast(mPackage.GetEntryPoint(3), null);
			flString = FlString.Cast(mPackage.GetEntryPoint(mMessageStringEntryIdx), null);
			text.SetCaption(flString);
			mSelectSoftKey.SetFunction(mSelectSoftkeyFunction, mSelectSoftkeyCmd);
			mClearSoftKey.SetFunction(mClearSoftkeyFunction, mClearSoftkeyCmd);
		}

		public override void StartMusic()
		{
		}
	}
}
