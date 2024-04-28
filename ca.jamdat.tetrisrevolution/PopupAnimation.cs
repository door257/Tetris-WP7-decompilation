using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class PopupAnimation
	{
		public const short red = 0;

		public const short white = 1;

		public const short animationEndTime = 208;

		public const short animationMaxColorValue = 255;

		public const short animationFrameOneTime = 0;

		public const short animationFrameOneRedColorR = 153;

		public const short animationFrameOneRedColorG = 0;

		public const short animationFrameOneRedColorB = 0;

		public const short animationFrameOneWhiteColorR = 255;

		public const short animationFrameOneWhiteColorG = 255;

		public const short animationFrameOneWhiteColorB = 255;

		public const short animationFrameTwoTime = 42;

		public const short animationFrameTwoPencentW = 10;

		public const short animationFrameTwoPencentH = 70;

		public const short animationFrameTwoRedColorR = 204;

		public const short animationFrameTwoRedColorG = 0;

		public const short animationFrameTwoRedColorB = 0;

		public const short animationFrameTwoWhiteColorR = 218;

		public const short animationFrameTwoWhiteColorG = 230;

		public const short animationFrameTwoWhiteColorB = 240;

		public const short animationFrameThreeTime = 83;

		public const short animationFrameThreePencentW = 20;

		public const short animationFrameThreePencentH = 122;

		public const short animationFrameThreeRedColorR = 255;

		public const short animationFrameThreeRedColorG = 0;

		public const short animationFrameThreeRedColorB = 0;

		public const short animationFrameThreeWhiteColorR = 181;

		public const short animationFrameThreeWhiteColorG = 206;

		public const short animationFrameThreeWhiteColorB = 225;

		public const short animationFrameFourTime = 125;

		public const short animationFrameFourPencentW = 64;

		public const short animationFrameFourPencentH = 110;

		public const short animationFrameFourRedColorR = 117;

		public const short animationFrameFourRedColorG = 39;

		public const short animationFrameFourRedColorB = 69;

		public const short animationFrameFourWhiteColorR = 84;

		public const short animationFrameFourWhiteColorG = 132;

		public const short animationFrameFourWhiteColorB = 169;

		public const short animationFrameFiveTime = 167;

		public const short animationFrameFivePencentW = 83;

		public const short animationFrameFivePencentH = 104;

		public const short animationFrameFiveRedColorR = 34;

		public const short animationFrameFiveRedColorG = 59;

		public const short animationFrameFiveRedColorB = 103;

		public const short animationFrameFiveWhiteColorR = 45;

		public const short animationFrameFiveWhiteColorG = 104;

		public const short animationFrameFiveWhiteColorB = 151;

		public const int shapeRectKeyFrameSequenceId = 0;

		public const int shapeColorKeyFrameSequenceId = 1;

		public const int shapeVisibilityKeyFrameSequenceId = 2;

		public const int popupVisibilityKeyFrameSequenceId = 3;

		public const int keyFrameSequenceIdCount = 4;

		public static TimerSequence CreateTimerSequence(Shape controlledShape, ResizableFrame popup)
		{
			TimerSequence timerSequence = new TimerSequence(4);
			KeyFrameController keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(controlledShape);
			KeyFrameSequence keyFrameSequence = new KeyFrameSequence(6, 2, 0, 4);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(4);
			timerSequence.RegisterInterval(keyFrameController, 0, 208);
			keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(controlledShape);
			keyFrameSequence = new KeyFrameSequence(5, 2, 0, 3);
			keyFrameSequence.SetInterpolator(1);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(8);
			timerSequence.RegisterInterval(keyFrameController, 0, 208);
			keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(controlledShape);
			keyFrameSequence = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence.SetInterpolator(0);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(5);
			timerSequence.RegisterInterval(keyFrameController, 0, 208);
			keyFrameController = new KeyFrameController();
			keyFrameController.SetControllee(popup);
			keyFrameSequence = new KeyFrameSequence(2, 1, 0, 1);
			keyFrameSequence.SetInterpolator(0);
			keyFrameController.SetKeySequence(keyFrameSequence);
			keyFrameController.SetControlledValueCode(5);
			timerSequence.RegisterInterval(keyFrameController, 0, 208);
			return timerSequence;
		}

		public static void InitializeTimerSequence(TimerSequence timerSequence, short color, ResizableFrame popup)
		{
			short num = 5;
			short num2 = 5;
			short num3 = (short)((480 - num) / 2);
			short num4 = (short)((734 - num2) / 2);
			short firstRect_left = num3;
			short firstRect_top = num4;
			short firstRect_width = num;
			short firstRect_height = num2;
			short absoluteLeft = popup.GetAbsoluteLeft();
			short absoluteTop = popup.GetAbsoluteTop();
			short rectWidth = popup.GetRectWidth();
			short rectHeight = popup.GetRectHeight();
			short lastRect_left = absoluteLeft;
			short lastRect_top = absoluteTop;
			short lastRect_width = rectWidth;
			short lastRect_height = rectHeight;
			InitializeTimerSequence(timerSequence, color, firstRect_left, firstRect_top, firstRect_width, firstRect_height, lastRect_left, lastRect_top, lastRect_width, lastRect_height);
		}

		public static void InitializeTimerSequence(TimerSequence timerSequence, short color, IndexedSprite sprite, ResizableFrame popup)
		{
			short num = 17;
			short num2 = 17;
			short num3 = (short)(sprite.GetAbsoluteLeft() + (sprite.GetRectWidth() - 17) / 2);
			short num4 = (short)(sprite.GetAbsoluteTop() + (sprite.GetRectHeight() - 17) / 2);
			short firstRect_left = num3;
			short firstRect_top = num4;
			short firstRect_width = num;
			short firstRect_height = num2;
			short absoluteLeft = popup.GetAbsoluteLeft();
			short absoluteTop = popup.GetAbsoluteTop();
			short rectWidth = popup.GetRectWidth();
			short rectHeight = popup.GetRectHeight();
			short lastRect_left = absoluteLeft;
			short lastRect_top = absoluteTop;
			short lastRect_width = rectWidth;
			short lastRect_height = rectHeight;
			InitializeTimerSequence(timerSequence, color, firstRect_left, firstRect_top, firstRect_width, firstRect_height, lastRect_left, lastRect_top, lastRect_width, lastRect_height);
		}

		public static void CleanTimerSequence(TimerSequence timerSequence)
		{
			for (int i = 0; i < 4; i++)
			{
				KeyFrameController keyFrameController = (KeyFrameController)timerSequence.GetChild(i);
				keyFrameController.GetKeySequence();
				keyFrameController = null;
			}
			timerSequence.UnRegisterAll();
		}

		public static void DeleteTimerSequence(TimerSequence timerSequence)
		{
			timerSequence = null;
		}

		public virtual void destruct()
		{
		}

		public static void InitializeTimerSequence(TimerSequence timerSequence, short color, short firstRect_left, short firstRect_top, short firstRect_width, short firstRect_height, short lastRect_left, short lastRect_top, short lastRect_width, short lastRect_height)
		{
			KeyFrameController keyFrameController = (KeyFrameController)timerSequence.GetChild(0);
			KeyFrameSequence keySequence = keyFrameController.GetKeySequence();
			InitializeShapeRectSequence(keySequence, firstRect_left, firstRect_top, firstRect_width, firstRect_height, lastRect_left, lastRect_top, lastRect_width, lastRect_height);
			keyFrameController = (KeyFrameController)timerSequence.GetChild(1);
			keySequence = keyFrameController.GetKeySequence();
			InitializeShapeColorSequence(keySequence, color);
			keyFrameController = (KeyFrameController)timerSequence.GetChild(2);
			keySequence = keyFrameController.GetKeySequence();
			InitializeShapeVisibilitySequence(keySequence);
			keyFrameController = (KeyFrameController)timerSequence.GetChild(3);
			keySequence = keyFrameController.GetKeySequence();
			InitializePopupVisibilitySequence(keySequence);
		}

		public static void InitializeShapeRectSequence(KeyFrameSequence keyFrameSequence, short firstRect_left, short firstRect_top, short firstRect_width, short firstRect_height, short lastRect_left, short lastRect_top, short lastRect_width, short lastRect_height)
		{
			int[] array = new int[4] { firstRect_left, firstRect_top, firstRect_width, firstRect_height };
			keyFrameSequence.SetKeyFrame(0, 0, array);
			array[2] = lastRect_width * 10 / 100;
			array[3] = lastRect_height * 70 / 100;
			array[0] = firstRect_left + (firstRect_width - array[2]) / 2;
			array[1] = firstRect_top + (firstRect_height - array[3]) / 2;
			keyFrameSequence.SetKeyFrame(1, 42, array);
			array[2] = lastRect_width * 20 / 100;
			array[3] = lastRect_height * 122 / 100;
			array[0] = firstRect_left + (firstRect_width - array[2]) / 2;
			array[1] = firstRect_top + (firstRect_height - array[3]) / 2;
			keyFrameSequence.SetKeyFrame(2, 83, array);
			array[2] = lastRect_width * 64 / 100;
			array[3] = lastRect_height * 110 / 100;
			array[0] = firstRect_left + (firstRect_width - array[2]) / 2;
			array[1] = firstRect_top + (firstRect_height - array[3]) / 2;
			keyFrameSequence.SetKeyFrame(3, 125, array);
			array[2] = lastRect_width * 83 / 100;
			array[3] = lastRect_height * 104 / 100;
			array[0] = firstRect_left + (firstRect_width - array[2]) / 2;
			array[1] = firstRect_top + (firstRect_height - array[3]) / 2;
			keyFrameSequence.SetKeyFrame(4, 167, array);
			array[0] = lastRect_left;
			array[1] = lastRect_top;
			array[2] = lastRect_width;
			array[3] = lastRect_height;
			keyFrameSequence.SetKeyFrame(5, 208, array);
		}

		public static void InitializeShapeColorSequence(KeyFrameSequence keyFrameSequence, short colorType)
		{
			F32[] array = F32.InstArrayF32(3);
			if (colorType == 0)
			{
				array[0] = F32.FromInt(153, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(0, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(0, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(0, 0, array, 16);
				array[0] = F32.FromInt(204, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(0, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(0, 16);
				array[2].Div(255);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(1, 42, array, 16);
				array[0] = F32.FromInt(255, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(0, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(0, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(2, 83, array, 16);
				array[0] = F32.FromInt(117, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(39, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(69, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(3, 125, array, 16);
				array[0] = F32.FromInt(34, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(59, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(103, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(4, 167, array, 16);
			}
			else
			{
				array[0] = F32.FromInt(255, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(255, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(255, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(0, 0, array, 16);
				array[0] = F32.FromInt(218, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(230, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(240, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(1, 42, array, 16);
				array[0] = F32.FromInt(181, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(206, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(225, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(2, 83, array, 16);
				array[0] = F32.FromInt(84, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(132, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(169, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(3, 125, array, 16);
				array[0] = F32.FromInt(45, 16);
				array[0].Div(255);
				array[1] = F32.FromInt(104, 16);
				array[1].Div(255);
				array[2] = F32.FromInt(151, 16);
				array[2].Div(255);
				keyFrameSequence.SetKeyFrame(4, 167, array, 16);
			}
		}

		public static void InitializeShapeVisibilitySequence(KeyFrameSequence keyFrameSequence)
		{
			int[] array = new int[1] { 1 };
			keyFrameSequence.SetKeyFrame(0, 0, array);
			array[0] = 0;
			keyFrameSequence.SetKeyFrame(1, 208, array);
		}

		public static void InitializePopupVisibilitySequence(KeyFrameSequence keyFrameSequence)
		{
			int[] array = new int[1] { 0 };
			keyFrameSequence.SetKeyFrame(0, 0, array);
			array[0] = 1;
			keyFrameSequence.SetKeyFrame(1, 208, array);
		}

		public static PopupAnimation[] InstArrayPopupAnimation(int size)
		{
			PopupAnimation[] array = new PopupAnimation[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PopupAnimation();
			}
			return array;
		}

		public static PopupAnimation[][] InstArrayPopupAnimation(int size1, int size2)
		{
			PopupAnimation[][] array = new PopupAnimation[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PopupAnimation[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PopupAnimation();
				}
			}
			return array;
		}

		public static PopupAnimation[][][] InstArrayPopupAnimation(int size1, int size2, int size3)
		{
			PopupAnimation[][][] array = new PopupAnimation[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PopupAnimation[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PopupAnimation[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PopupAnimation();
					}
				}
			}
			return array;
		}
	}
}
