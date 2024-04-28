using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Cursor : Viewport
	{
		public override void destruct()
		{
		}

		public virtual void Initialize()
		{
			SetViewport(null);
			Shape shape = new Shape();
			shape.SetColor(new Color888(12, 98, 195));
			shape.SetViewport(this);
		}

		public virtual void Unload()
		{
			SetViewport(null);
		}

		public virtual void SetSelectedItem(Selection item)
		{
			SetSize(item.GetSize());
			GetChild(0).SetSize(item.GetSize());
			SetViewport(item);
			int subtype = item.GetSubtype();
			if (subtype == -4 || subtype == -18)
			{
				item.PutComponentBehind(this, item.GetChild(0));
			}
			else
			{
				SendToBack();
			}
		}

		public static Cursor[] InstArrayCursor(int size)
		{
			Cursor[] array = new Cursor[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Cursor();
			}
			return array;
		}

		public static Cursor[][] InstArrayCursor(int size1, int size2)
		{
			Cursor[][] array = new Cursor[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Cursor[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Cursor();
				}
			}
			return array;
		}

		public static Cursor[][][] InstArrayCursor(int size1, int size2, int size3)
		{
			Cursor[][][] array = new Cursor[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Cursor[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Cursor[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Cursor();
					}
				}
			}
			return array;
		}
	}
}
