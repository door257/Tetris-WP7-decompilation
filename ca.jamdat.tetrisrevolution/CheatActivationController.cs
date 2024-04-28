using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class CheatActivationController : BaseController
	{
		public const int statusUnmatched = 0;

		public const int statusMatching = 1;

		public const int statusMatched = 2;

		public Controllable mTimeControlled;

		public int mHeldKey;

		public int mInputCheatActivationTimerMs;

		public int mInputCheatDeactivationTimerMs;

		public bool mCheatInputActivated;

		public int[] mInputKeys;

		public int mInputCount;

		public int mOldKeyMapping;

		public CheatActivationController()
		{
			mHeldKey = 0;
			mTimeControlled = new Controllable();
			mTimeControlled.SetController(this);
			mInputKeys = new int[13];
		}

		public override void destruct()
		{
			mTimeControlled.UnRegisterInGlobalTime();
			mTimeControlled = null;
			mInputKeys = null;
		}

		public static CheatActivationController Get()
		{
			return GameApp.Get().GetCheatActivationController();
		}

		public override void OnTime(int totalTimeMs, int deltaTimeMs)
		{
			if (mHeldKey == 17 && !mCheatInputActivated)
			{
				mInputCheatActivationTimerMs += deltaTimeMs;
				if (mInputCheatActivationTimerMs >= 3000)
				{
					ActivateInput();
				}
			}
			else if (mHeldKey != 17 && !mCheatInputActivated)
			{
				mTimeControlled.UnRegisterInGlobalTime();
			}
			else if (mHeldKey != 17)
			{
				mInputCheatDeactivationTimerMs += deltaTimeMs;
				if (mInputCheatDeactivationTimerMs >= 3000)
				{
					DeactivateInput();
				}
			}
		}

		public virtual bool OnKey(int sceneId, int key, bool up)
		{
			bool result = false;
			if (!up)
			{
				if (mCheatInputActivated)
				{
					mInputCheatDeactivationTimerMs = 0;
					DetectCheatActivation(key);
					result = true;
				}
				else if (key == 17 && mHeldKey == 0)
				{
					mHeldKey = key;
					mInputCheatActivationTimerMs = 0;
					mTimeControlled.RegisterInGlobalTime();
				}
			}
			else
			{
				mHeldKey = 0;
			}
			return result;
		}

		public virtual void Terminate()
		{
			mTimeControlled.UnRegisterInGlobalTime();
		}

		public virtual void DisableAllCheats()
		{
			CheatContainer cheatContainer = GameApp.Get().GetCheatContainer();
			for (int i = 0; i < 26; i++)
			{
				Cheat cheat = cheatContainer.GetCheat(i);
				if (cheat != null)
				{
					cheat.Deactivate();
				}
			}
		}

		public virtual void ActivateInput()
		{
			mOldKeyMapping = GameApp.Get().GetInputMapper().ChangeMapping(1);
			mCheatInputActivated = true;
			mInputCount = 0;
			mInputCheatDeactivationTimerMs = 0;
		}

		public virtual void DeactivateInput()
		{
			if (mCheatInputActivated)
			{
				InputMapper inputMapper = GameApp.Get().GetInputMapper();
				if (inputMapper.GetMapping() == 1)
				{
					mOldKeyMapping = inputMapper.ChangeMapping(mOldKeyMapping);
				}
				mCheatInputActivated = false;
			}
			mHeldKey = 0;
			mTimeControlled.UnRegisterInGlobalTime();
		}

		public virtual void DetectCheatActivation(int key)
		{
			bool flag = true;
			if (key >= 17 && key <= 26)
			{
				mInputKeys[mInputCount++] = key;
				CheatContainer cheatContainer = GameApp.Get().GetCheatContainer();
				for (int i = 0; i < 26; i++)
				{
					Cheat cheat = cheatContainer.GetCheat(i);
					if (cheat == null)
					{
						continue;
					}
					switch (GetMatchStatus(cheat, mInputKeys, mInputCount))
					{
					case 2:
						if (cheat.IsActivated())
						{
							cheat.Deactivate();
						}
						else
						{
							cheat.Activate(ExtractCheatParam(cheat, mInputKeys, mInputCount));
						}
						DeactivateInput();
						return;
					case 1:
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				DeactivateInput();
			}
		}

		public static int GetMatchStatus(Cheat cheat, int[] inputKeys, int inputCount)
		{
			int[] code = cheat.GetCode();
			int codeLength = cheat.GetCodeLength();
			int paramLength = cheat.GetParamLength();
			int num = FlMath.Minimum(inputCount, codeLength);
			for (int i = 0; i < num; i++)
			{
				if (inputKeys[i] != code[i])
				{
					return 0;
				}
			}
			if (inputCount == codeLength + paramLength)
			{
				return 2;
			}
			return 1;
		}

		public static int ExtractCheatParam(Cheat cheat, int[] inputKeys, int inputCount)
		{
			int paramLength = cheat.GetParamLength();
			int num = 0;
			for (int num2 = paramLength - 1; num2 >= 0; num2--)
			{
				num *= 10;
				num += Utilities.GetKeyValue(inputKeys[inputCount - 1 - num2]);
			}
			return num;
		}

		public static CheatActivationController[] InstArrayCheatActivationController(int size)
		{
			CheatActivationController[] array = new CheatActivationController[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CheatActivationController();
			}
			return array;
		}

		public static CheatActivationController[][] InstArrayCheatActivationController(int size1, int size2)
		{
			CheatActivationController[][] array = new CheatActivationController[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatActivationController[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatActivationController();
				}
			}
			return array;
		}

		public static CheatActivationController[][][] InstArrayCheatActivationController(int size1, int size2, int size3)
		{
			CheatActivationController[][][] array = new CheatActivationController[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CheatActivationController[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CheatActivationController[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CheatActivationController();
					}
				}
			}
			return array;
		}
	}
}
