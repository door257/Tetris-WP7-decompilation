using System;
using System.Collections.Generic;
using ca.jamdat.flight;
using ca.jamdat.tetrisrevolution;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Tetris
{
	public class LiveState
	{
		public enum ESigninStatus
		{
			None,
			SigningIn,
			Local,
			LIVE,
			Error,
			UpdateNeeded
		}

		public enum EUpdateStatus
		{
			None,
			UpdateNeeded,
			Refused,
			Accepted
		}

		public GamerServicesComponent mGamerService;

		public EUpdateStatus mUpdateStatus;

		public ESigninStatus mSigninStatus;

		public bool mIsTrial;

		public bool mDisplayTitleUpdateMessage;

		private static LiveState mInstance = new LiveState();

		public static LiveState Get
		{
			get
			{
				return mInstance;
			}
		}

		public static SignedInGamer Gamer
		{
			get
			{
				return Microsoft.Xna.Framework.GamerServices.Gamer.SignedInGamers[PlayerIndex.One];
			}
		}

		public static EUpdateStatus UpdateStatus
		{
			get
			{
				return Get.mUpdateStatus;
			}
		}

		public static bool IsTrial
		{
			get
			{
				return Get.mIsTrial;
			}
		}

		public static ESigninStatus SigninStatus
		{
			get
			{
				return Get.mSigninStatus;
			}
		}

		public static bool DisplayingUpdateMessage
		{
			get
			{
				return Get.mDisplayTitleUpdateMessage;
			}
		}

		public static bool GamerServicesActive
		{
			get
			{
				if (Get.mGamerService != null)
				{
					return Get.mGamerService.Enabled;
				}
				return false;
			}
		}

		private LiveState()
		{
		}

		public void Initialize(XNAGame pGame)
		{
			UpdateIsTrial();
			SignedInGamer.SignedIn += GamerSignedInCallback;
			try
			{
				mGamerService = new GamerServicesComponent(pGame);
				pGame.Components.Add(mGamerService);
				mGamerService.Initialize();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				pGame.Components.Remove(mGamerService);
			}
		}

		public void UpdateIsTrial()
		{
			mIsTrial = Guide.IsTrialMode;
		}

		public void HandleGameUpdateRequired()
		{
			mDisplayTitleUpdateMessage = true;
			mSigninStatus = ESigninStatus.UpdateNeeded;
			mUpdateStatus = EUpdateStatus.UpdateNeeded;
			if (mGamerService != null)
			{
				mGamerService.Enabled = false;
			}
			PromptUpdate();
		}

		public void PromptUpdate()
		{
			if (mDisplayTitleUpdateMessage && !Guide.IsVisible)
			{
				GameApp.Get().SetIsInUpdate(true);
				mDisplayTitleUpdateMessage = false;
				bool immediateLoadModeEnabled = GameApp.Get().GetLibrary().SetImmediateLoadModeEnabled(true);
				MetaPackage package = GameLibrary.GetPackage(-2144239522);
				Package package2 = package.GetPackage();
				string nativeString = EntryPoint.GetFlString(package2, 11).NativeString;
				string nativeString2 = EntryPoint.GetFlString(package2, 12).NativeString;
				string nativeString3 = EntryPoint.GetFlString(package2, 1).NativeString;
				string nativeString4 = EntryPoint.GetFlString(package2, 0).NativeString;
				GameApp.Get().GetLibrary().SetImmediateLoadModeEnabled(immediateLoadModeEnabled);
				Guide.BeginShowMessageBox(nativeString, nativeString2, new List<string> { nativeString4, nativeString3 }, 1, MessageBoxIcon.Alert, UpdateDialogGetMBResult, null);
			}
		}

		public bool UpdateApp()
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			if (UpdateStatus == EUpdateStatus.Accepted)
			{
				UpdateIsTrial();
				if (IsTrial)
				{
					Guide.ShowMarketplace(PlayerIndex.One);
				}
				else
				{
					MarketplaceDetailTask val = new MarketplaceDetailTask();
					val.ContentType = (MarketplaceContentType)1;
					val.Show();
				}
				mUpdateStatus = EUpdateStatus.Refused;
				return true;
			}
			return false;
		}

		protected void UpdateDialogGetMBResult(IAsyncResult userResult)
		{
			int? num = Guide.EndShowMessageBox(userResult);
			if (num.HasValue)
			{
				mUpdateStatus = ((num > 0) ? EUpdateStatus.Accepted : EUpdateStatus.Refused);
				UpdateApp();
			}
			else
			{
				mUpdateStatus = EUpdateStatus.Refused;
			}
		}

		protected void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			if (gamer != null)
			{
				FrameworkGlobals.GetInstance().gamer = gamer;
			}
		}
	}
}
