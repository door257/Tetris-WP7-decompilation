using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class SubtypeHandler
	{
		public const sbyte arrowFrameEnabledIndex = 0;

		public const sbyte arrowFramePushedIndex = 1;

		public const sbyte arrowFrameDisabledIndex = 2;

		public static void OnSubtype(BaseScene receiver, Component source, int msg, int intParam)
		{
			if (!(source is Selection))
			{
				return;
			}
			Selection selection = (Selection)source;
			int subtype = selection.GetSubtype();
			if (subtype == -1)
			{
				UpdateTextColor(selection);
			}
			else if (-5 == subtype)
			{
				UpdateCheckBoxSelection(selection);
			}
			else if (-3 == subtype || -4 == subtype)
			{
				UpdateOptionSelection(selection);
			}
			else
			{
				switch (subtype)
				{
				case -7:
				case -6:
					UpdateUpDownArrowsVisual(selection, msg, intParam);
					break;
				case -9:
				case -8:
					UpdateLeftRightArrowsVisual(selection, msg, intParam);
					break;
				case -20:
					UpdateLeaderboardSelection(selection, msg, intParam);
					break;
				case -22:
					UpdateLevelSelectSelection(selection, msg, intParam);
					break;
				case -23:
					UpdatePromptButtonSelection(selection, msg, intParam);
					break;
				}
			}
			UpdateCursor(receiver, selection, msg, intParam);
		}

		public virtual void destruct()
		{
		}

		public static void UpdateTextColor(Selection selection)
		{
			int entryPoint = 2;
			if (selection.GetEnabledState() && selection.GetSelectedState())
			{
				entryPoint = 3;
			}
			FlFont flFont = EntryPoint.GetFlFont(3047517, entryPoint);
			Text text = (Text)selection.GetChild(0);
			text.SetFont(flFont);
		}

		public static void UpdateOptionSelection(Selection selection)
		{
		}

		public static void UpdateCheckBoxSelection(Selection source)
		{
			UpdateOptionSelection(source);
		}

		public static void UpdateUpDownArrowsVisual(Selection selection, int msg, int intParam)
		{
			Sprite sprite = (Sprite)selection.GetChild(0);
			if (selection.GetEnabledState())
			{
				sprite.SetVisible(true);
			}
			else
			{
				sprite.SetVisible(false);
			}
		}

		public static void UpdateLeftRightArrowsVisual(Selection selection, int msg, int intParam)
		{
			IndexedSprite indexedSprite = (IndexedSprite)selection.GetChild(0);
			int currentFrame = 2;
			if (selection.GetEnabledState())
			{
				currentFrame = (selection.GetPushedState() ? 1 : 0);
			}
			indexedSprite.SetCurrentFrame(currentFrame);
		}

		public static void UpdateCursor(BaseScene receiver, Selection selection, int msg, int intParam)
		{
			int subtype = selection.GetSubtype();
			if (msg == -128 && (-3 == subtype || -5 == subtype || -4 == subtype))
			{
				if (intParam == 1)
				{
					receiver.GetCursor().SetSelectedItem(selection);
				}
				else
				{
					receiver.GetCursor().SetViewport(null);
				}
			}
		}

		public static void UpdateLeaderboardSelection(Selection selection, int msg, int intParam)
		{
			if (selection.GetSelectedState())
			{
				selection.GetChild(0).SetVisible(true);
			}
			else
			{
				selection.GetChild(0).SetVisible(false);
			}
		}

		public static void UpdateLevelSelectSelection(Selection selection, int msg, int intParam)
		{
			if (selection.GetPushedState())
			{
				((IndexedSprite)selection.GetChild(0)).SetCurrentFrame(6);
			}
			else
			{
				((IndexedSprite)selection.GetChild(0)).SetCurrentFrame(5);
			}
		}

		public static void UpdatePromptButtonSelection(Selection selection, int msg, int intParam)
		{
			Viewport viewport = (Viewport)selection.GetChild(0);
			IndexedSprite indexedSprite = (IndexedSprite)viewport.GetChild(0);
			IndexedSprite indexedSprite2 = (IndexedSprite)viewport.GetChild(1);
			if (selection.GetPushedState())
			{
				indexedSprite.SetCurrentFrame(0);
				indexedSprite2.SetCurrentFrame(0);
			}
			else
			{
				indexedSprite.SetCurrentFrame(1);
				indexedSprite2.SetCurrentFrame(1);
			}
			indexedSprite.CenterInRect(viewport.GetRectLeft(), viewport.GetRectTop(), viewport.GetRectWidth(), viewport.GetRectHeight());
			indexedSprite2.CenterInRect(viewport.GetRectLeft(), viewport.GetRectTop(), viewport.GetRectWidth(), viewport.GetRectHeight());
		}

		public static SubtypeHandler[] InstArraySubtypeHandler(int size)
		{
			SubtypeHandler[] array = new SubtypeHandler[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SubtypeHandler();
			}
			return array;
		}

		public static SubtypeHandler[][] InstArraySubtypeHandler(int size1, int size2)
		{
			SubtypeHandler[][] array = new SubtypeHandler[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SubtypeHandler[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SubtypeHandler();
				}
			}
			return array;
		}

		public static SubtypeHandler[][][] InstArraySubtypeHandler(int size1, int size2, int size3)
		{
			SubtypeHandler[][][] array = new SubtypeHandler[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SubtypeHandler[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SubtypeHandler[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SubtypeHandler();
					}
				}
			}
			return array;
		}
	}
}
