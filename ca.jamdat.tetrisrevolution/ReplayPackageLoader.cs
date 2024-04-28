using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class ReplayPackageLoader
	{
		public const int noReplay = -1;

		public const int backToBackReplay = 0;

		public const int hardDropReplay = 1;

		public const int lineClearReplay = 2;

		public const int softDropReplay = 3;

		public const int tSpinReplay = 4;

		public const int tetrisReplay = 5;

		public const int cascadeReplay = 6;

		public const int replayGlossaryCount = 7;

		public const int originReplay = 7;

		public const int gravityReplay = 8;

		public const int floodReplay = 9;

		public const int splitReplay = 10;

		public const int ledgesReplay = 11;

		public const int scannerReplay = 12;

		public const int magneticReplay = 13;

		public const int laserReplay = 14;

		public const int chillReplay = 15;

		public const int flashLightReplay = 16;

		public const int treadmillReplay = 17;

		public const int radicalReplay = 18;

		public const int masterReplayCount = 19;

		public const int totalReplayCount = 20;

		public int mPackageReplayToLoad;

		public MetaPackage mReplayMetaPackage;

		public ReplayPackageLoader()
		{
			mPackageReplayToLoad = -1;
		}

		public virtual void destruct()
		{
			if (mReplayMetaPackage != null)
			{
				Unload();
			}
		}

		public static bool IsGlossaryReplay(int replayId)
		{
			if (replayId > -1)
			{
				return replayId < 7;
			}
			return false;
		}

		public static bool IsMasterReplay(int replayId)
		{
			if (replayId != -1 && replayId >= 7)
			{
				return replayId < 19;
			}
			return false;
		}

		public static int GetRandomReplayMasterId()
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			int unlockVariantCount = featsExpert.GetUnlockVariantCount();
			int num = GameRandom.Random(0, unlockVariantCount - 1);
			int num2 = 0;
			for (int i = 0; i < 12; i++)
			{
				if (featsExpert.IsGameVariantUnlocked(i))
				{
					if (num2 == num)
					{
						return GameVariantToMasterReplayId(i);
					}
					num2++;
				}
			}
			return 7;
		}

		public static int GetRandomDemoReplayMasterId()
		{
			int gameVariant = GameRandom.Random(0, 11);
			return GameVariantToMasterReplayId(gameVariant);
		}

		public static ReplayPackageLoader Get()
		{
			return GameApp.Get().GetReplayLoader();
		}

		public virtual void SetSimulationToLoad(int simulationToLoad)
		{
			Replay.Get().SetReplayMode(1);
			mPackageReplayToLoad = simulationToLoad;
		}

		public virtual void Load()
		{
			if (!IsThereASimulationToLoad())
			{
				return;
			}
			int num = 98;
			switch (mPackageReplayToLoad)
			{
			case 2:
				num = 2195523;
				break;
			case 5:
				num = 2293830;
				break;
			case 0:
				num = 2129985;
				break;
			case 4:
				num = 2261061;
				break;
			case 1:
				num = 2162754;
				break;
			case 3:
				num = 2228292;
				break;
			case 6:
				num = 2326599;
				break;
			case 7:
				num = 2359368;
				break;
			case 8:
				num = 2392137;
				break;
			case 9:
				num = 2424906;
				break;
			case 10:
				num = 2457675;
				break;
			case 11:
				num = 2490444;
				break;
			case 12:
				num = 2523213;
				break;
			case 13:
				num = 2555982;
				break;
			case 14:
				num = 2588751;
				break;
			case 15:
				num = 2621520;
				break;
			case 16:
				num = 2654289;
				break;
			case 17:
				num = 2687058;
				break;
			case 18:
				num = 2719827;
				break;
			}
			if (mReplayMetaPackage == null || mReplayMetaPackage.GetId() != num)
			{
				if (mReplayMetaPackage != null)
				{
					GameLibrary.ReleasePackage(mReplayMetaPackage);
				}
				mReplayMetaPackage = GameLibrary.GetPackage(num);
			}
		}

		public virtual bool IsLoaded()
		{
			bool result = true;
			if (IsThereASimulationToLoad())
			{
				result = mReplayMetaPackage != null && mReplayMetaPackage.IsLoaded();
			}
			return result;
		}

		public virtual void GetEntryPoints()
		{
			if (IsThereASimulationToLoad())
			{
				LoadReplayFromBlob();
			}
		}

		public virtual void Unload()
		{
			if (mReplayMetaPackage != null)
			{
				GameLibrary.ReleasePackage(mReplayMetaPackage);
				mReplayMetaPackage = null;
			}
			mPackageReplayToLoad = -1;
		}

		public virtual void InitGameSettings()
		{
			MetaPackage package = GameLibrary.GetPackage(1310760);
			Package package2 = package.GetPackage();
			int @int = EntryPoint.GetInt(package2, 207 + mPackageReplayToLoad * 5);
			sbyte gameMode = (sbyte)EntryPoint.GetInt(package2, 204 + mPackageReplayToLoad * 5);
			int int2 = EntryPoint.GetInt(package2, 205 + mPackageReplayToLoad * 5);
			int int3 = EntryPoint.GetInt(package2, 206 + mPackageReplayToLoad * 5);
			GameLibrary.ReleasePackage(package);
			GameSettings gameSettings = GameApp.Get().GetGameSettings();
			gameSettings.SetGameVariant(@int);
			gameSettings.SetGameMode(gameMode);
			gameSettings.SetGameDifficulty(int2);
			gameSettings.SetLineLimit(int3);
			gameSettings.SetTimeLimit(0);
		}

		public virtual bool IsReplayIsGlossary()
		{
			return IsGlossaryReplay(mPackageReplayToLoad);
		}

		public virtual bool IsReplayIsMaster()
		{
			return IsMasterReplay(mPackageReplayToLoad);
		}

		public virtual bool IsThereAReplaySet()
		{
			return IsThereASimulationToLoad();
		}

		public virtual int GetReplayId()
		{
			return mPackageReplayToLoad;
		}

		public virtual bool OnLoadReplayCommand(int command)
		{
			int num = -1;
			bool result = false;
			switch (command)
			{
			case 41:
				num = 2;
				break;
			case 42:
				num = 5;
				break;
			case 43:
				num = 0;
				break;
			case 44:
				num = 4;
				break;
			case 45:
				num = 1;
				break;
			case 46:
				num = 3;
				break;
			case 61:
				num = 6;
				break;
			case 48:
				num = 7;
				break;
			case 49:
				num = 8;
				break;
			case 50:
				num = 9;
				break;
			case 51:
				num = 10;
				break;
			case 52:
				num = 11;
				break;
			case 53:
				num = 12;
				break;
			case 54:
				num = 13;
				break;
			case 55:
				num = 14;
				break;
			case 56:
				num = 15;
				break;
			case 57:
				num = 16;
				break;
			case 58:
				num = 17;
				break;
			case 59:
				num = 18;
				break;
			}
			if (num != -1)
			{
				SetSimulationToLoad(num);
				InitGameSettings();
				result = true;
			}
			return result;
		}

		public virtual bool IsThereASimulationToLoad()
		{
			return mPackageReplayToLoad != -1;
		}

		public virtual void LoadReplayFromBlob()
		{
			Blob blob = EntryPoint.GetBlob(mReplayMetaPackage.GetPackage(), 0);
			Replay.Get().LoadReplayFromBlob(blob);
		}

		public static int GameVariantToMasterReplayId(int gameVariant)
		{
			switch (gameVariant)
			{
			case 0:
				return 7;
			case 1:
				return 17;
			case 2:
				return 8;
			case 3:
				return 9;
			case 4:
				return 11;
			case 5:
				return 14;
			case 6:
				return 13;
			case 7:
				return 12;
			case 8:
				return 10;
			case 9:
				return 15;
			case 10:
				return 16;
			case 11:
				return 18;
			default:
				return 7;
			}
		}

		public static ReplayPackageLoader[] InstArrayReplayPackageLoader(int size)
		{
			ReplayPackageLoader[] array = new ReplayPackageLoader[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new ReplayPackageLoader();
			}
			return array;
		}

		public static ReplayPackageLoader[][] InstArrayReplayPackageLoader(int size1, int size2)
		{
			ReplayPackageLoader[][] array = new ReplayPackageLoader[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ReplayPackageLoader[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ReplayPackageLoader();
				}
			}
			return array;
		}

		public static ReplayPackageLoader[][][] InstArrayReplayPackageLoader(int size1, int size2, int size3)
		{
			ReplayPackageLoader[][][] array = new ReplayPackageLoader[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new ReplayPackageLoader[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new ReplayPackageLoader[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new ReplayPackageLoader();
					}
				}
			}
			return array;
		}
	}
}
