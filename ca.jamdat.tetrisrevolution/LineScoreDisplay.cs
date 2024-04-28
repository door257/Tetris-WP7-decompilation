using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LineScoreDisplay : Viewport
	{
		public Text mScoreText;

		public TimerSequence mAnimation;

		public KeyFrameController mPositionController;

		public KeyFrameController mOpacityController;

		public LineScoreDisplay()
		{
			short width = 96;
			short height = 30;
			SetSize(width, height);
			FlFont flFont = EntryPoint.GetFlFont(3047517, 21);
			mScoreText = new Text();
			mScoreText.SetAlignment(1);
			mScoreText.SetFont(flFont);
			mScoreText.SetViewport(this);
			mScoreText.SetSize(width, height);
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(1867833);
			mAnimation = new TimerSequence(2);
			KeyFrameSequence keyFrameSequence = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 29);
			mPositionController = new KeyFrameController();
			mPositionController.SetControllee(this);
			mPositionController.SetKeySequence(keyFrameSequence);
			mPositionController.SetControlledValueCode(1);
			mPositionController.SetIsAbsolute(false);
			mAnimation.RegisterInterval(mPositionController, 0, 750);
			KeyFrameSequence keyFrameSequence2 = EntryPoint.GetKeyFrameSequence(preLoadedPackage, 30);
			mOpacityController = new KeyFrameController();
			mOpacityController.SetControllee(mScoreText);
			mOpacityController.SetKeySequence(keyFrameSequence2);
			mOpacityController.SetControlledValueCode(6);
			mOpacityController.SetIsAbsolute(true);
			mAnimation.RegisterInterval(mOpacityController, 482, 750);
		}

		public override void destruct()
		{
		}

		public virtual void Initialize(int score, TetrisGame tetrisGame)
		{
			SetViewport(tetrisGame.GetGameController().GetLayerComponent().GetLayer(8));
			mScoreText.SetCaption(new FlString(score));
			int num = tetrisGame.GetWell().GetScoreDisplayRow();
			short left = 110;
			short num2 = 0;
			if (num <= 33 && tetrisGame.GetSpecialGameEvent() != -1 && tetrisGame.CanDisplayFeedback())
			{
				num = 34;
			}
			num2 = (short)((num - 20 - 1) * 32 - 30);
			SetTopLeft(left, num2);
			mPositionController.SetControlledValueCode(1);
			mOpacityController.SetControlledValueCode(6);
			mAnimation.SetTotalTime(0);
			mAnimation.RegisterInGlobalTime();
		}

		public virtual void Unload()
		{
			StopAndDetachAnimation();
			if (mScoreText != null)
			{
				mScoreText.SetViewport(null);
				mScoreText = null;
			}
			if (mAnimation != null)
			{
				mAnimation.UnRegisterAll();
				mAnimation = null;
			}
			mPositionController = null;
			mOpacityController = null;
		}

		public virtual bool IsAnimationOver()
		{
			if (mAnimation.IsRegisteredInGlobalTime())
			{
				return mAnimation.GetTotalTime() >= 750;
			}
			return false;
		}

		public virtual void StopAndDetachAnimation()
		{
			SetViewport(null);
			if (mAnimation != null)
			{
				mAnimation.UnRegisterInGlobalTime();
			}
		}

		public static LineScoreDisplay[] InstArrayLineScoreDisplay(int size)
		{
			LineScoreDisplay[] array = new LineScoreDisplay[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LineScoreDisplay();
			}
			return array;
		}

		public static LineScoreDisplay[][] InstArrayLineScoreDisplay(int size1, int size2)
		{
			LineScoreDisplay[][] array = new LineScoreDisplay[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LineScoreDisplay[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LineScoreDisplay();
				}
			}
			return array;
		}

		public static LineScoreDisplay[][][] InstArrayLineScoreDisplay(int size1, int size2, int size3)
		{
			LineScoreDisplay[][][] array = new LineScoreDisplay[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LineScoreDisplay[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LineScoreDisplay[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LineScoreDisplay();
					}
				}
			}
			return array;
		}
	}
}
