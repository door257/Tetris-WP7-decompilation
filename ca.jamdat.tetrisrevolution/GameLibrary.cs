using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class GameLibrary
	{
		public const int pkgVerticalCompletionPackage = 1179684;

		public const int pkgMenuSlideAnimPackage = 1212453;

		public const int pkgPopupVariantIconsPackage = 1245222;

		public const int pkgUnlockAnimationPackage = 1277991;

		public const int pkgMinoPackage = 1146915;

		public const int pkgVariantVanillaPackage = 720918;

		public const int pkgVariantFloodPackage = 753687;

		public const int pkgVariantLedgesPackage = 786456;

		public const int pkgVariantLimboPackage = 819225;

		public const int pkgVariantMagneticPackage = 851994;

		public const int pkgVariantScannerPackage = 884763;

		public const int pkgVariantSplitPackage = 917532;

		public const int pkgVariantChillPackage = 950301;

		public const int pkgVariantFlashlitePackage = 983070;

		public const int pkgVariantTreadmillPackage = 688149;

		public const int pkgVariantMasterPackage = 1015839;

		public const int pkgVariantBasicPackage = 655380;

		public const int pkgEventBarIndicatorPackage = 1048608;

		public const int pkgLineClearPackage = 1933371;

		public const int pkgLoadingMenuPackage = 1998909;

		public const int pkgEffectErrorPackage = 1081377;

		public const int pkgSuccessFailureAnimPackage = 1114146;

		public const int pkgGameSplashPackage = 1343529;

		public const int pkgEnableSoundMenuPackage = 1474605;

		public const int pkgMainMenuPackage = 1507374;

		public const int pkgPlayMenuPackage = 1540143;

		public const int pkgHelixPackage = 1900602;

		public const int pkgHelpMenuPopupPackage = 1572912;

		public const int pkgLeaderboardMenuPopupPackage = 1605681;

		public const int pkgLeaderboardMenuMessagePopupPackage = 1638450;

		public const int pkgAchievementsMenuPopupPackage = 1671219;

		public const int pkgAchievementsMenuMessagePopupPackage = 1703988;

		public const int pkgTrainerMenuPackage = 2064447;

		public const int pkgPauseMenuPackage = 1769526;

		public const int pkgMessageMenuPackage = 1802295;

		public const int pkgPromptMenuPackage = 1835064;

		public const int pkgRealisationsMenuPackage = 1966140;

		public const int pkgMenuPopupRealisationsFeatsPackage = 2850903;

		public const int pkgMenuPopupRealisationsStatsPackage = 2883672;

		public const int pkgMenuPopupRealisationsScoresPackage = 2916441;

		public const int pkgMenuPopupRealisationsVariantsPackage = 2949210;

		public const int pkgOptionsMenuPopupPackage = 1736757;

		public const int pkgMenuPopupSetupGameVariantPackage = 2752596;

		public const int pkgMenuPopupSetupLevelSelectPackage = 2785365;

		public const int pkgMenuPopupLockedGameVariantPackage = 2818134;

		public const int pkgMenuPopupGlossarySelectPackage = 2981979;

		public const int pkgMenuPopupMasterReplaySelectPackage = 3014748;

		public const int pkgFailurePopupPackage = 229383;

		public const int pkgLongHintPopupPackage = 458766;

		public const int pkgRetryPopupPackage = 131076;

		public const int pkgReturnToMainMenuPopupPackage = 163845;

		public const int pkgRetryFromPausePopupPackage = 491535;

		public const int pkgExitAppConfirmationPopupPackage = 32769;

		public const int pkgResetGameConfirmationPopupPackage = 65538;

		public const int pkgDemoConfirmationPopupPackage = 98307;

		public const int pkgSuccessPopupPackage = 196614;

		public const int pkgFeatMasterPopupPackage = 327690;

		public const int pkgMenuPopupCareerFeatsPackage = 360459;

		public const int pkgMenuPopupAdvancedFeatsPackage = 393228;

		public const int pkgUnlockPopupPackage = 294921;

		public const int pkgGameStatsPopupPackage = 425997;

		public const int pkgNewBestPopupPackage = 262152;

		public const int pkgScrollbarPackage = 524304;

		public const int pkgTutorialMenuPackage = 2031678;

		public const int pkgChangeScreenResolutionMenuPackage = 2097216;

		public const int pkgGameScenePackage = 1867833;

		public const int pkgLocalizedFontPackage = 3047517;

		public const int pkgCommonStringsPackage = -2144239522;

		public const int pkgMenuStringsPackage = -2144075681;

		public const int pkgGameStringsPackage = -2143911840;

		public const int pkgLoadingMenuStringsPackage = -2143747999;

		public const int pkgCommonPackage = 1310760;

		public const int pkgHourglassPackage = 0;

		public const int pkgMenuSoundsPackage = 557073;

		public const int pkgGameMusicPackage = 589842;

		public const int pkgWesternFontPackage = 622611;

		public const int pkgLegalSplashPackage = 1376298;

		public const int pkgEASplashPackage = 1409067;

		public const int pkgLanguageMenuPackage = 1441836;

		public const int pkgReplayBackToBackPackage = 2129985;

		public const int pkgReplayHardDropPackage = 2162754;

		public const int pkgReplayLineClearPackage = 2195523;

		public const int pkgReplaySoftDropPackage = 2228292;

		public const int pkgReplayTSpinPackage = 2261061;

		public const int pkgReplayTetrisPackage = 2293830;

		public const int pkgReplayCascadePackage = 2326599;

		public const int pkgReplayOriginPackage = 2359368;

		public const int pkgReplayGravityPackage = 2392137;

		public const int pkgReplayFloodPackage = 2424906;

		public const int pkgReplaySplitPackage = 2457675;

		public const int pkgReplayLedgesPackage = 2490444;

		public const int pkgReplayScannerPackage = 2523213;

		public const int pkgReplayMagneticPackage = 2555982;

		public const int pkgReplayLaserPackage = 2588751;

		public const int pkgReplayChillPackage = 2621520;

		public const int pkgReplayFlashPackage = 2654289;

		public const int pkgReplayTreadmillPackage = 2687058;

		public const int pkgReplayRadicalPackage = 2719827;

		public const int pkgCount = 98;

		public Library mLibrary;

		public bool mSynchronousLoadEnabled;

		public MetaPackage[] mMetaPackages;

		public int[] mLockBitFields;

		public static Package GetPreLoadedPackage(int pkgId)
		{
			MetaPackage package = GetPackage(pkgId);
			Package package2 = package.GetPackage();
			ReleasePackage(package);
			return package2;
		}

		public GameLibrary()
		{
			FlString dir = FlApplication.GetDir();
			FlString other = StringUtils.CreateString("Resources/gamelib");
			FlString filename = dir.Add(other);
			mLibrary = new Library(filename);
			mMetaPackages = new MetaPackage[98];
			mLockBitFields = new int[98];
		}

		public virtual void destruct()
		{
			int num = 98;
			for (int i = 0; i < num; i++)
			{
				if (mMetaPackages[i] != null)
				{
					mMetaPackages[i] = null;
				}
			}
			mMetaPackages = null;
			mLockBitFields = null;
			mLibrary.Close();
			mLibrary = null;
		}

		public virtual bool IsInitialized()
		{
			return true;
		}

		public static MetaPackage GetPackage(int pkgId)
		{
			short applicationLanguage = GameApp.Get().GetSettings().GetApplicationLanguage();
			int resolution = 0;
			return GameApp.Get().GetLibrary().GetPackage(pkgId, applicationLanguage, resolution);
		}

		public static void ReleasePackage(MetaPackage metaPkg)
		{
			GameApp.Get().GetLibrary().ReleasePackageImpl(metaPkg);
		}

		public virtual void LockPackageMemory(int pkgId)
		{
			int packageLockIndex = GetPackageLockIndex(pkgId);
			int num = mLockBitFields[packageLockIndex];
			short applicationLanguage = GameApp.Get().GetSettings().GetApplicationLanguage();
			int num2 = 0;
			bool flag = false;
			if (IsPackageLocked(pkgId))
			{
				int num3 = num & -65536;
				int bitfield = 0;
				bitfield = BitField.SetValue(bitfield, applicationLanguage, 16711680, 16);
				bitfield = BitField.SetValue(bitfield, num2, -16777216, 24);
				flag = bitfield != num3;
			}
			int[] array = new int[22];
			int packageDependencies = GetPackageDependencies(pkgId, array, 22, applicationLanguage, num2);
			for (int i = 0; i < packageDependencies; i++)
			{
				LockPackageMemory(array[i]);
			}
			if (flag)
			{
				short language = (short)BitField.GetValue(num, 16711680, 16);
				int value = BitField.GetValue(num, -16777216, 24);
				packageDependencies = GetPackageDependencies(pkgId, array, 22, language, value);
				for (int j = 0; j < packageDependencies; j++)
				{
					UnlockPackageMemory(array[j]);
				}
			}
			num = BitField.AddValue(num, 1, 65535, 0);
			num = BitField.SetValue(num, applicationLanguage, 16711680, 16);
			num = BitField.SetValue(num, num2, -16777216, 24);
			mLockBitFields[packageLockIndex] = num;
		}

		public virtual void UnlockPackageMemory(int pkgId)
		{
			int packageLockIndex = GetPackageLockIndex(pkgId);
			int bitfield = mLockBitFields[packageLockIndex];
			short language = (short)BitField.GetValue(bitfield, 16711680, 16);
			int resolution = 0;
			bitfield = BitField.AddValue(bitfield, -1, 65535, 0);
			if (BitField.GetValue(bitfield, 65535, 0) == 0)
			{
				int packageArrayIndex = GetPackageArrayIndex(pkgId);
				MetaPackage metaPackage = mMetaPackages[packageArrayIndex];
				if (metaPackage != null && metaPackage.IsValid() && metaPackage.GetRefCount() == 0)
				{
					metaPackage.ReleasePackage();
					mMetaPackages[packageArrayIndex] = null;
					metaPackage = null;
				}
			}
			int[] array = new int[22];
			int packageDependencies = GetPackageDependencies(pkgId, array, 22, language, resolution);
			for (int i = 0; i < packageDependencies; i++)
			{
				UnlockPackageMemory(array[i]);
			}
			mLockBitFields[packageLockIndex] = bitfield;
		}

		public virtual bool IsPackageLoaded(int pkgId)
		{
			short applicationLanguage = GameApp.Get().GetSettings().GetApplicationLanguage();
			int resolution = 0;
			MetaPackage packageFromArray = GetPackageFromArray(pkgId, applicationLanguage, resolution);
			if (packageFromArray != null)
			{
				return packageFromArray.IsLoaded();
			}
			return false;
		}

		public static bool IsPackageLoaded(MetaPackage p)
		{
			if (p != null)
			{
				return p.IsLoaded();
			}
			return false;
		}

		public virtual bool SetImmediateLoadModeEnabled(bool enabled)
		{
			bool result = mSynchronousLoadEnabled;
			mSynchronousLoadEnabled = enabled;
			return result;
		}

		public static bool IsMultilingualPackage(int pkgId)
		{
			return BitField.IsBitOn(pkgId, int.MinValue);
		}

		public static bool IsMultiResolutionPackage(int pkgId)
		{
			return false;
		}

		public virtual void UnloadIrrelevantPackages()
		{
			int num = 98;
			for (int i = 0; i < num; i++)
			{
				MetaPackage metaPackage = mMetaPackages[i];
				if (metaPackage != null && metaPackage.IsValid() && metaPackage.IsLoaded())
				{
					bool flag = false;
					if (metaPackage.IsLanguageDependant() && metaPackage.GetLanguage() != GameApp.Get().GetSettings().GetApplicationLanguage())
					{
						flag = true;
					}
					if (flag)
					{
						metaPackage.ReleasePackage();
					}
				}
			}
		}

		public virtual int GetPackageDependencies(int pkgId, int[] outDepArray, int maxOutDepCount, short language, int resolution)
		{
			int result = 0;
			switch (pkgId)
			{
			case 1540143:
				outDepArray[result++] = -2143911840;
				outDepArray[result++] = 1179684;
				outDepArray[result++] = 1212453;
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 1900602;
				outDepArray[result++] = 2752596;
				outDepArray[result++] = 2785365;
				outDepArray[result++] = 2818134;
				break;
			case 1343529:
			case 1376298:
			case 1409067:
			case 1441836:
			case 1474605:
			case 1802295:
			case 1835064:
			case 2097216:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				break;
			case 1998909:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2143747999;
				outDepArray[result++] = 1245222;
				outDepArray[result++] = 1212453;
				break;
			case 1507374:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 1179684;
				outDepArray[result++] = 1212453;
				outDepArray[result++] = 1736757;
				outDepArray[result++] = 1572912;
				outDepArray[result++] = 1605681;
				outDepArray[result++] = 1638450;
				outDepArray[result++] = 32769;
				outDepArray[result++] = 98307;
				outDepArray[result++] = 1671219;
				outDepArray[result++] = 1703988;
				outDepArray[result++] = 2785365;
				break;
			case 2064447:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 1179684;
				outDepArray[result++] = 1212453;
				outDepArray[result++] = 2981979;
				outDepArray[result++] = 3014748;
				break;
			case 1966140:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 1179684;
				outDepArray[result++] = 1212453;
				outDepArray[result++] = 2850903;
				outDepArray[result++] = 360459;
				outDepArray[result++] = 393228;
				outDepArray[result++] = 294921;
				outDepArray[result++] = 2949210;
				outDepArray[result++] = 2883672;
				outDepArray[result++] = 2916441;
				break;
			case 1179684:
				outDepArray[result++] = -2144075681;
				break;
			case 32769:
			case 65538:
			case 98307:
			case 131076:
			case 163845:
			case 196614:
			case 229383:
			case 262152:
			case 327690:
			case 425997:
			case 458766:
			case 491535:
			case 1769526:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2143911840;
				break;
			case 294921:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = 1245222;
				outDepArray[result++] = 1277991;
				break;
			case 2752596:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 1245222;
				break;
			case 1572912:
			case 1605681:
			case 1671219:
			case 1736757:
			case 2031678:
			case 2785365:
			case 2818134:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				break;
			case 2981979:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 524304;
				break;
			case 3014748:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 524304;
				outDepArray[result++] = -2144239522;
				break;
			case 2850903:
			case 2883672:
			case 2949210:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				outDepArray[result++] = 524304;
				break;
			case 2916441:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2144075681;
				break;
			case 360459:
			case 393228:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = 1277991;
				break;
			case -2144075681:
			case -2143911840:
			case -2143747999:
				outDepArray[result++] = -2144239522;
				break;
			case 3047517:
				outDepArray[result++] = GetLocalizedFontDependency(language);
				break;
			case 1310760:
				outDepArray[result++] = 3047517;
				outDepArray[result++] = -2144239522;
				break;
			case 1867833:
				outDepArray[result++] = 1310760;
				outDepArray[result++] = -2143911840;
				outDepArray[result++] = 1146915;
				outDepArray[result++] = 1933371;
				outDepArray[result++] = 32769;
				outDepArray[result++] = 65538;
				outDepArray[result++] = 98307;
				outDepArray[result++] = 131076;
				outDepArray[result++] = 163845;
				outDepArray[result++] = 491535;
				outDepArray[result++] = 196614;
				outDepArray[result++] = 229383;
				outDepArray[result++] = 262152;
				outDepArray[result++] = 294921;
				outDepArray[result++] = 327690;
				outDepArray[result++] = 360459;
				outDepArray[result++] = 393228;
				outDepArray[result++] = 425997;
				outDepArray[result++] = 458766;
				outDepArray[result++] = 1769526;
				outDepArray[result++] = 1736757;
				outDepArray[result++] = 1572912;
				break;
			case 917532:
				outDepArray[result++] = 1310760;
				break;
			case 753687:
				outDepArray[result++] = 1048608;
				break;
			case 819225:
				outDepArray[result++] = 1081377;
				break;
			case 884763:
				outDepArray[result++] = 1048608;
				break;
			case 950301:
				outDepArray[result++] = 1048608;
				break;
			}
			return result;
		}

		public virtual void ReleasePackageImpl(MetaPackage metaPkg)
		{
			short language = metaPkg.GetLanguage();
			int resolution = 0;
			metaPkg.RemoveRef();
			int refCount = metaPkg.GetRefCount();
			int id = metaPkg.GetId();
			if (refCount == 0 && !IsPackageLocked(id))
			{
				metaPkg.ReleasePackage();
				int packageArrayIndex = GetPackageArrayIndex(id);
				mMetaPackages[packageArrayIndex] = null;
				metaPkg = null;
			}
			if (refCount == 0 && IsPackageLocked(id))
			{
				int num = 0;
				if (metaPkg.IsLanguageDependant() && language != GameApp.Get().GetSettings().GetApplicationLanguage())
				{
					num++;
				}
				if (num != 0)
				{
					metaPkg.ReleasePackage();
				}
			}
			int[] array = new int[22];
			int packageDependencies = GetPackageDependencies(id, array, 22, language, resolution);
			for (int i = 0; i < packageDependencies; i++)
			{
				ReleasePackage(GetPackageFromArray(array[i], language, resolution));
			}
		}

		public virtual int GetLocalizedFontDependency(short language)
		{
			int num = 98;
			return 622611;
		}

		public virtual MetaPackage GetPackage(int pkgId, short language, int resolution)
		{
			int[] array = new int[22];
			int packageDependencies = GetPackageDependencies(pkgId, array, 22, language, resolution);
			MetaPackage metaPackage = GetPackageFromArray(pkgId, language, resolution);
			Package package = null;
			bool flag = metaPackage != null && metaPackage.IsValid();
			if (!flag)
			{
				package = mLibrary.NewPackage(GetPackageLibraryIndex(pkgId, language, resolution));
				package.SetNumDependencies(packageDependencies);
			}
			else
			{
				package = metaPackage.GetPackage();
			}
			int num = 0;
			for (int i = 0; i < packageDependencies; i++)
			{
				MetaPackage package2 = GetPackage(array[i], language, resolution);
				if (!flag)
				{
					package.SetDependency(i, package2.GetPackage());
				}
				if (package2.IsLanguageDependant())
				{
					num++;
				}
			}
			if (!package.IsLoaded() && !package.IsLoading())
			{
				bool asynchronous = !mSynchronousLoadEnabled;
				package.Load(asynchronous);
			}
			if (!flag)
			{
				metaPackage = SetPackageInArray(package, pkgId, language, resolution);
			}
			metaPackage.AddRef();
			if (num > 0)
			{
				metaPackage.MarkAsLanguageDependant();
			}
			return metaPackage;
		}

		public virtual int GetPackageLibraryIndex(int pkgId, short language, int resolution)
		{
			int value = BitField.GetValue(pkgId, 1073709056, 15);
			int num = 0;
			if (IsMultilingualPackage(pkgId))
			{
				num = GameApp.Get().GetLanguageManager().GetLanguageIndex(language);
			}
			return value + num;
		}

		public virtual MetaPackage SetPackageInArray(Package p, int pkgId, short language, int resolution)
		{
			int packageArrayIndex = GetPackageArrayIndex(pkgId);
			MetaPackage metaPackage = mMetaPackages[packageArrayIndex];
			if (metaPackage == null)
			{
				metaPackage = new MetaPackage();
				mMetaPackages[packageArrayIndex] = metaPackage;
			}
			metaPackage.SetPackage(p, pkgId, language, resolution);
			return metaPackage;
		}

		public virtual MetaPackage GetPackageFromArray(int pkgId, short language, int resolution)
		{
			return mMetaPackages[GetPackageArrayIndex(pkgId)];
		}

		public virtual bool IsPackageLocked(int pkgId)
		{
			return GetPackageLockCount(pkgId) > 0;
		}

		public virtual int GetPackageLockCount(int pkgId)
		{
			int bitfield = mLockBitFields[GetPackageLockIndex(pkgId)];
			return BitField.GetValue(bitfield, 65535, 0);
		}

		public static int GetPackageArrayIndex(int pkgId)
		{
			return BitField.GetValue(pkgId, 32767, 0);
		}

		public static bool IsMatching(MetaPackage p, int id, short language, int resolution)
		{
			if (p != null && p.GetId() == id)
			{
				return p.GetLanguage() == language;
			}
			return false;
		}

		public static int GetPackageLockIndex(int pkgId)
		{
			return BitField.GetValue(pkgId, 32767, 0);
		}

		public static GameLibrary[] InstArrayGameLibrary(int size)
		{
			GameLibrary[] array = new GameLibrary[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new GameLibrary();
			}
			return array;
		}

		public static GameLibrary[][] InstArrayGameLibrary(int size1, int size2)
		{
			GameLibrary[][] array = new GameLibrary[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameLibrary[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameLibrary();
				}
			}
			return array;
		}

		public static GameLibrary[][][] InstArrayGameLibrary(int size1, int size2, int size3)
		{
			GameLibrary[][][] array = new GameLibrary[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new GameLibrary[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new GameLibrary[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new GameLibrary();
					}
				}
			}
			return array;
		}
	}
}
