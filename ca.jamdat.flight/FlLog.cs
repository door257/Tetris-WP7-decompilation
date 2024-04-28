using System;

namespace ca.jamdat.flight
{
	public class FlLog
	{
		public const short GrpUnsorted = 0;

		public const short GrpDataStore = 1;

		public const short GrpDisplay = 2;

		public const short GrpEvent = 3;

		public const short GrpFile = 4;

		public const short GrpInput = 5;

		public const short GrpJni = 6;

		public const short GrpMedia = 7;

		public const short GrpNetwork = 8;

		public const short GrpProfile = 9;

		public const short GrpGame0 = 10;

		public const short GrpGame1 = 11;

		public const short GrpGame2 = 12;

		public const short GrpGame3 = 13;

		public const short GrpGame4 = 14;

		public const short GrpGame5 = 15;

		public const short GrpGame6 = 16;

		public const short GrpGame7 = 17;

		public const short GrpGame8 = 18;

		public const short GrpGame9 = 19;

		public const short GrpGame10 = 20;

		public const short GrpGame11 = 21;

		public const short GrpGame12 = 22;

		public const short GrpCOUNT = 23;

		public const short LevelInfo = 0;

		public const short LevelWarning = 1;

		public const short LevelError = 2;

		public const short OutputToScreen = 1;

		public const short OutputSaveFile = 2;

		public const short OutputDataCable = 4;

		public const short OutputNativeConsole = 8;

		public const short PrefixCustomStr = 1;

		public const short PrefixLevel = 2;

		public const short PrefixGrp = 4;

		public const short PrefixTimestamp = 8;

		public const short PrefixDate = 16;

		public const short PrefixTime = 32;

		public FlString mMsgStr;

		public FlString mPrefixCustomStr;

		public short mEnabledPrefixes;

		public short mEnabledOutputs;

		public short[] mGrpLevel = new short[23];

		public bool mIsInitialized;

		public FlString mEndStr;

		public FlString mSepStr;

		public FlLog()
		{
			mEnabledPrefixes = 0;
			mEnabledOutputs = 0;
			mEndStr = new FlString(StringUtils.CreateString(": "));
			mSepStr = new FlString(StringUtils.CreateString(", "));
			mMsgStr = new FlString();
			mPrefixCustomStr = new FlString();
			mMsgStr.Assign(StringUtils.CreateString(""));
			EnableOutput(1, true);
			EnableOutput(2, true);
			EnableOutput(4, true);
			EnableOutput(8, true);
			EnablePrefix(16, false);
			EnablePrefix(32, false);
			EnablePrefix(8, false);
			EnablePrefix(2, false);
			EnablePrefix(4, false);
			EnablePrefix(1, false);
			SetPrefixCustomStr(StringUtils.CreateString("_-_-_"));
			SetGrpMinLevel(0, 1);
			SetGrpMinLevel(1, 1);
			SetGrpMinLevel(2, 1);
			SetGrpMinLevel(3, 1);
			SetGrpMinLevel(4, 1);
			SetGrpMinLevel(5, 1);
			SetGrpMinLevel(6, 1);
			SetGrpMinLevel(7, 1);
			SetGrpMinLevel(8, 1);
			SetGrpMinLevel(9, 1);
			SetAllGameGrpMinLevel(1);
			mIsInitialized = true;
		}

		public static FlLog GetInstance()
		{
			FrameworkGlobals instance = FrameworkGlobals.GetInstance();
			if (instance.mFlLog == null)
			{
				instance.mFlLog = new FlLog();
			}
			return instance.mFlLog;
		}

		public virtual void Log(short grp, short level, FlString msgStr)
		{
			if (level < GetGrpMinLevel(grp) || msgStr.IsEmpty())
			{
				return;
			}
			FlString flString = new FlString();
			if (IsPrefixEnabled(1))
			{
				flString.AddAssign(mPrefixCustomStr);
				flString.InsertCharAt(flString.GetLength(), 32);
			}
			if (IsPrefixEnabled(16) || IsPrefixEnabled(32))
			{
				TimeData timeData = new TimeData();
				if (IsPrefixEnabled(16))
				{
					flString.AddAssign(new FlString(timeData.GetYear()));
					flString.InsertCharAt(flString.GetLength(), 45);
					flString.AddAssign(new FlString(timeData.GetMonth()));
					flString.InsertCharAt(flString.GetLength(), 45);
					flString.AddAssign(new FlString(timeData.GetDay()));
					flString.InsertCharAt(flString.GetLength(), 32);
				}
				if (IsPrefixEnabled(32))
				{
					flString.AddAssign(new FlString(timeData.GetHour()));
					flString.InsertCharAt(flString.GetLength(), 58);
					flString.AddAssign(new FlString(timeData.GetMin()));
					flString.InsertCharAt(flString.GetLength(), 58);
					flString.AddAssign(new FlString(timeData.GetSec()));
					flString.InsertCharAt(flString.GetLength(), 32);
				}
			}
			if (IsPrefixEnabled(8))
			{
				flString.AddAssign(new FlString(FlApplication.GetRealTime()));
				flString.InsertCharAt(flString.GetLength(), 32);
			}
			if (IsPrefixEnabled(4))
			{
				flString.InsertCharAt(flString.GetLength(), 71);
				flString.AddAssign(new FlString(grp));
				flString.InsertCharAt(flString.GetLength(), 32);
			}
			if (IsPrefixEnabled(2))
			{
				flString.InsertCharAt(flString.GetLength(), 76);
				flString.AddAssign(new FlString(level));
				flString.InsertCharAt(flString.GetLength(), 32);
			}
			flString.AddAssign(msgStr);
			if (IsOutputEnabled(1))
			{
				LogToScreen(flString);
			}
			if (IsOutputEnabled(4) || IsOutputEnabled(8))
			{
				LogToStdOut(flString);
			}
			if (IsOutputEnabled(2))
			{
				LogToFile(flString);
			}
		}

		public virtual void Log(short grp, short level, FlString msgStr, int int1)
		{
			if (level >= GetGrpMinLevel(grp) && !msgStr.IsEmpty())
			{
				mMsgStr.ReplaceCharAt(0, 0);
				mMsgStr.AddAssign(msgStr);
				mMsgStr.AddAssign(mEndStr);
				mMsgStr.AddAssign(new FlString(int1));
				Log(grp, level, mMsgStr);
			}
		}

		public virtual void Log(short grp, short level, FlString msgStr, int int1, int int2)
		{
			if (level >= GetGrpMinLevel(grp) && !msgStr.IsEmpty())
			{
				mMsgStr.ReplaceCharAt(0, 0);
				mMsgStr.AddAssign(msgStr);
				mMsgStr.AddAssign(mEndStr);
				mMsgStr.AddAssign(new FlString(int1));
				mMsgStr.AddAssign(mSepStr);
				mMsgStr.AddAssign(new FlString(int2));
				Log(grp, level, mMsgStr);
			}
		}

		public virtual void Log(short grp, short level, FlString msgStr, int int1, int int2, int int3)
		{
			if (level >= GetGrpMinLevel(grp) && !msgStr.IsEmpty())
			{
				mMsgStr.ReplaceCharAt(0, 0);
				mMsgStr.AddAssign(msgStr);
				mMsgStr.AddAssign(mEndStr);
				mMsgStr.AddAssign(new FlString(int1));
				mMsgStr.AddAssign(mSepStr);
				mMsgStr.AddAssign(new FlString(int2));
				mMsgStr.AddAssign(mSepStr);
				mMsgStr.AddAssign(new FlString(int3));
				Log(grp, level, mMsgStr);
			}
		}

		public virtual bool IsOutputEnabled(short output)
		{
			return (mEnabledOutputs & output) == output;
		}

		public virtual void EnableOutput(short output, bool enable)
		{
			if (enable)
			{
				if (mIsInitialized && output == 2)
				{
					Log(0, 1, StringUtils.CreateString("LogsSaveFile inactive."));
				}
				mEnabledOutputs |= output;
			}
			else
			{
				mEnabledOutputs = (short)(mEnabledOutputs & ~output);
			}
		}

		public virtual short GetGrpMinLevel(short grp)
		{
			return mGrpLevel[grp];
		}

		public virtual void SetGrpMinLevel(short grp, short level)
		{
			mGrpLevel[grp] = level;
		}

		public virtual void SetAllGameGrpMinLevel(short level)
		{
			for (int i = 10; i < 23; i++)
			{
				mGrpLevel[i] = level;
			}
		}

		public virtual bool IsPrefixEnabled(short prefix)
		{
			return (mEnabledPrefixes & prefix) == prefix;
		}

		public virtual void EnablePrefix(short prefix, bool enable)
		{
			if (enable)
			{
				mEnabledPrefixes |= prefix;
			}
			else
			{
				mEnabledPrefixes = (short)(mEnabledPrefixes & ~prefix);
			}
		}

		public virtual void SetPrefixCustomStr(FlString str)
		{
			mPrefixCustomStr.Assign(str);
		}

		public static void Log(string msg)
		{
		}

		public static void Log(Exception exception)
		{
		}

		public virtual void LogToStdOut(FlString msg)
		{
		}

		public virtual void LogToScreen(FlString msg)
		{
			GetInstance();
		}

		public virtual void LogToFile(FlString msg)
		{
		}

		public static FlLog[] InstArrayFlLog(int size)
		{
			FlLog[] array = new FlLog[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FlLog();
			}
			return array;
		}

		public static FlLog[][] InstArrayFlLog(int size1, int size2)
		{
			FlLog[][] array = new FlLog[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlLog[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlLog();
				}
			}
			return array;
		}

		public static FlLog[][][] InstArrayFlLog(int size1, int size2, int size3)
		{
			FlLog[][][] array = new FlLog[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FlLog[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FlLog[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FlLog();
					}
				}
			}
			return array;
		}
	}
}
