using System;
using System.Collections.Generic;
using ca.jamdat.flight;
using ca.jamdat.tetrisrevolution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
	public class XNAGame : Game
	{
		private GraphicsDeviceManager mGraphics;

		private SpriteBatch mSpriteBatch;

		private XNAApp mTestApp;

		public static bool externalMusic = Microsoft.Xna.Framework.Media.MediaPlayer.State == MediaState.Playing || !Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl;

		public static bool displayMusicQuestion = externalMusic;

		public static bool musicQuestionDisplayed = false;

		private GamePadState oldState;

		protected override void BeginRun()
		{
			try
			{
				base.BeginRun();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected override void EndRun()
		{
			try
			{
				base.EndRun();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			try
			{
				base.OnExiting(sender, args);
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			try
			{
				if (FlApplication.GetInstance().GetIsSuspended())
				{
					FlApplication.GetInstance().OnResumeFromOS();
				}
				else
				{
					LoadState();
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			try
			{
				GameApp.Get().SaveGame();
				SaveState();
				GameApp.Get().OnSuspendFromOS();
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		private void SaveState()
		{
		}

		private void LoadState()
		{
		}

		public XNAGame()
		{
			mGraphics = new GraphicsDeviceManager(this);
			base.TargetElapsedTime = TimeSpan.FromTicks(333333L);
			mGraphics.PreferredBackBufferWidth = 480;
			mGraphics.PreferredBackBufferHeight = 800;
			Guide.IsScreenSaverEnabled = false;
			mTestApp = new XNAApp();
		}

		protected override void Initialize()
		{
			base.Initialize();
			LiveState.Get.Initialize(this);
			mTestApp.startApp();
			FrameworkGlobals.GetInstance().GraphicsDeviceManager = mGraphics;
			FrameworkGlobals.GetInstance().ContentManager = new ContentManager(base.Services);
			XNAApp.mScene = new XNAScene();
			XNAScene.repaintScene = false;
			mTestApp.mFirstTime = false;
			InitializeDemo();
		}

		protected void InitializeDemo()
		{
			GameApp.Get().SetIsDemo(LiveState.IsTrial);
		}

		protected override void LoadContent()
		{
			mSpriteBatch = new SpriteBatch(base.GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected void InterruptMusicDialogGetMBResult(IAsyncResult userResult)
		{
			int? num = Guide.EndShowMessageBox(userResult);
			if (num.HasValue && num.Value > 0)
			{
				externalMusic = false;
				Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
			}
			displayMusicQuestion = false;
			musicQuestionDisplayed = false;
		}

		protected override void Update(GameTime gameTime)
		{
			try
			{
				if (mTestApp.mMustQuit)
				{
					Exit();
				}
				if (LiveState.Get.mUpdateStatus != LiveState.EUpdateStatus.Accepted && LiveState.Get.mUpdateStatus != LiveState.EUpdateStatus.UpdateNeeded && !displayMusicQuestion)
				{
					long realTime = FlApplication.GetRealTime();
					mTestApp.ProcessEvents();
					if (mTestApp.mIsPaused)
					{
						if (mTestApp.mResumeNextIsShown)
						{
							mTestApp.mResumeNextIsShown = false;
							mTestApp.start();
						}
					}
					else
					{
						mTestApp.GetGameTime();
						XNAApp.mScene.onTime(mTestApp.GetGameTime());
						FrameworkGlobals.GetInstance().mPackageLoader.LoadQueuedPackages();
					}
					UpdateInput();
				}
				base.Update(gameTime);
				if (displayMusicQuestion && LiveState.Get.mUpdateStatus != LiveState.EUpdateStatus.UpdateNeeded && !musicQuestionDisplayed)
				{
					bool immediateLoadModeEnabled = GameApp.Get().GetLibrary().SetImmediateLoadModeEnabled(true);
					MetaPackage package = GameLibrary.GetPackage(-2144239522);
					Package package2 = package.GetPackage();
					string nativeString = EntryPoint.GetFlString(package2, 13).NativeString;
					string nativeString2 = EntryPoint.GetFlString(package2, 14).NativeString;
					string nativeString3 = EntryPoint.GetFlString(package2, 1).NativeString;
					string nativeString4 = EntryPoint.GetFlString(package2, 0).NativeString;
					GameApp.Get().GetLibrary().SetImmediateLoadModeEnabled(immediateLoadModeEnabled);
					Guide.BeginShowMessageBox(nativeString, nativeString2, new List<string> { nativeString4, nativeString3 }, 1, MessageBoxIcon.Alert, InterruptMusicDialogGetMBResult, null);
					musicQuestionDisplayed = true;
				}
			}
			catch (Exception ex)
			{
				FlLog.Log(ex);
				HandleUpdateException(ex);
			}
		}

		private void HandleUpdateException(Exception pException)
		{
			if (pException is GameUpdateRequiredException)
			{
				LiveState.Get.HandleGameUpdateRequired();
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			base.GraphicsDevice.Clear(Color.Black);
			if (LiveState.Get.mUpdateStatus != LiveState.EUpdateStatus.Accepted && LiveState.Get.mUpdateStatus != LiveState.EUpdateStatus.UpdateNeeded && !musicQuestionDisplayed)
			{
				XNAApp.mScene.paint(mSpriteBatch);
			}
			base.Draw(gameTime);
		}

		private void UpdateInput()
		{
			TouchCollection state = TouchPanel.GetState();
			if (state.Count != 0)
			{
				TouchLocation touchLocation = state[0];
				if (touchLocation.State == TouchLocationState.Moved)
				{
					XNAApp.instance.AddEvent(10, (short)touchLocation.Position.X, (short)touchLocation.Position.Y);
				}
				if (touchLocation.State == TouchLocationState.Pressed)
				{
					XNAApp.instance.AddEvent(9, (short)touchLocation.Position.X, (short)touchLocation.Position.Y);
				}
				if (touchLocation.State == TouchLocationState.Released)
				{
					XNAApp.instance.AddEvent(11, (short)touchLocation.Position.X, (short)touchLocation.Position.Y);
				}
			}
			GamePadState state2 = GamePad.GetState(PlayerIndex.One);
			if (state2.Buttons.Back == ButtonState.Pressed)
			{
				if (oldState.Buttons.Back != ButtonState.Pressed)
				{
					XNAApp.instance.AddEvent(1, -9);
				}
			}
			else if (oldState.Buttons.Back == ButtonState.Pressed)
			{
				XNAApp.instance.AddEvent(2, -9);
			}
			oldState = state2;
		}
	}
}
