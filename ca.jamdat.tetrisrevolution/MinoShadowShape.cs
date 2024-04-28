using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class MinoShadowShape
	{
		public const int borderNorth = 0;

		public const int borderSouth = 1;

		public const int borderEast = 2;

		public const int borderWest = 3;

		public const int borderCount = 4;

		public Shape[] mMinoBorder;

		public MinoShadowShape()
		{
			mMinoBorder = new Shape[4];
			for (int i = 0; i < 4; i++)
			{
				mMinoBorder[i] = new Shape(null);
			}
			mMinoBorder[0].SetSize(32, 1);
			mMinoBorder[0].SetTopLeft(0, 0);
			mMinoBorder[1].SetSize(32, 1);
			mMinoBorder[1].SetTopLeft(0, 31);
			mMinoBorder[2].SetSize(1, 32);
			mMinoBorder[2].SetTopLeft(31, 0);
			mMinoBorder[3].SetSize(1, 32);
			mMinoBorder[3].SetTopLeft(0, 0);
		}

		public virtual void destruct()
		{
			for (int i = 0; i < 4; i++)
			{
				mMinoBorder[i].SetViewport(null);
				mMinoBorder[i] = null;
			}
			mMinoBorder = null;
		}

		public virtual void SetViewport(Viewport viewport)
		{
			for (int i = 0; i < 4; i++)
			{
				mMinoBorder[i].SetViewport(viewport);
			}
		}

		public virtual void Unload()
		{
			SetViewport(null);
		}

		public virtual void SetMinoShadowShapeAspect(sbyte minoAspect)
		{
			SetBorderShapeColor(GetColorFromAspect(minoAspect));
		}

		public virtual void SetMinoShadowBorders(sbyte sticky)
		{
			mMinoBorder[0].SetVisible((sticky & 8) == 0);
			mMinoBorder[1].SetVisible((sticky & 2) == 0);
			mMinoBorder[2].SetVisible((sticky & 4) == 0);
			mMinoBorder[3].SetVisible((sticky & 1) == 0);
		}

		public virtual Color888 GetColorFromAspect(sbyte minoAspect)
		{
			return EntryPoint.GetFlColor(1146915, 20 + minoAspect);
		}

		public virtual void SetBorderShapeColor(Color888 color)
		{
			for (int i = 0; i < 4; i++)
			{
				mMinoBorder[i].SetColor(color);
			}
		}

		public static MinoShadowShape[] InstArrayMinoShadowShape(int size)
		{
			MinoShadowShape[] array = new MinoShadowShape[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new MinoShadowShape();
			}
			return array;
		}

		public static MinoShadowShape[][] InstArrayMinoShadowShape(int size1, int size2)
		{
			MinoShadowShape[][] array = new MinoShadowShape[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoShadowShape[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoShadowShape();
				}
			}
			return array;
		}

		public static MinoShadowShape[][][] InstArrayMinoShadowShape(int size1, int size2, int size3)
		{
			MinoShadowShape[][][] array = new MinoShadowShape[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MinoShadowShape[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MinoShadowShape[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new MinoShadowShape();
					}
				}
			}
			return array;
		}
	}
}
