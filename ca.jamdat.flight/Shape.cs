using System;

namespace ca.jamdat.flight
{
	public class Shape : Component
	{
		public new const sbyte typeNumber = 76;

		public new const sbyte typeID = 76;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte RECTANGLE = 0;

		public Color888 mColor;

		public static Shape Cast(object o, Shape _)
		{
			return (Shape)o;
		}

		public override sbyte GetTypeID()
		{
			return 76;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public Shape()
		{
			mColor = new Color888();
		}

		public Shape(Viewport viewport)
		{
			mColor = new Color888();
			SetViewport(viewport);
		}

		public override void destruct()
		{
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			Draw(displayContext, mRect_left, mRect_top, mRect_width, mRect_height);
		}

		public virtual void SetColor(Color888 color)
		{
			if (!mColor.Equals(color))
			{
				mColor.Assign(color);
				Invalidate();
			}
		}

		public virtual Color888 GetColor()
		{
			return mColor;
		}

		public virtual short GetRed()
		{
			return (short)mColor.GetRed();
		}

		public virtual short GetGreen()
		{
			return (short)mColor.GetGreen();
		}

		public virtual short GetBlue()
		{
			return (short)mColor.GetBlue();
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mColor.OnSerialize(p);
		}

		public override void ControlValue(int valueCode, bool setValue, Controller controller)
		{
			if (valueCode == 8)
			{
				if (setValue)
				{
					SetColor(controller.GetColorValue());
				}
				else
				{
					controller.SetValue(GetColor());
				}
			}
			else
			{
				base.ControlValue(valueCode, setValue, controller);
			}
		}

		public virtual void Draw(DisplayContext displayContext, short inRect_left, short inRect_top, short inRect_width, short inRect_height)
		{
			displayContext.DrawRectangle(inRect_left, inRect_top, inRect_width, inRect_height, true, mColor.GetRed(), mColor.GetGreen(), mColor.GetBlue());
		}

		public static Shape[] InstArrayShape(int size)
		{
			Shape[] array = new Shape[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Shape();
			}
			return array;
		}

		public static Shape[][] InstArrayShape(int size1, int size2)
		{
			Shape[][] array = new Shape[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Shape[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Shape();
				}
			}
			return array;
		}

		public static Shape[][][] InstArrayShape(int size1, int size2, int size3)
		{
			Shape[][][] array = new Shape[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Shape[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Shape[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Shape();
					}
				}
			}
			return array;
		}
	}
}
