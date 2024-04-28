using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Hourglass
	{
		public MetaPackage mMetaPackage;

		public virtual void destruct()
		{
		}

		public virtual void Load()
		{
			mMetaPackage = GameLibrary.GetPackage(0);
		}

		public virtual bool IsLoaded()
		{
			return GameLibrary.IsPackageLoaded(mMetaPackage);
		}

		public virtual void Unload()
		{
			if (mMetaPackage != null)
			{
				if (IsVisible())
				{
					SetVisible(false);
				}
				GameLibrary.ReleasePackage(mMetaPackage);
				mMetaPackage = null;
			}
		}

		public virtual void SetVisible(bool visible)
		{
			Package package = mMetaPackage.GetPackage();
			Sprite sprite = null;
			sprite = Sprite.Cast(package.GetEntryPoint(0), null);
			if (visible)
			{
				sprite.SetViewport(GameApp.Get());
			}
			else
			{
				sprite.SetViewport(null);
			}
		}

		public virtual bool IsVisible()
		{
			if (!IsLoaded())
			{
				return false;
			}
			Sprite sprite = null;
			sprite = Sprite.Cast(mMetaPackage.GetPackage().GetEntryPoint(0), null);
			return sprite.GetViewport() == GameApp.Get();
		}

		public virtual void SetTopLeft(short left, short top)
		{
			Sprite sprite = null;
			sprite = Sprite.Cast(mMetaPackage.GetPackage().GetEntryPoint(0), null);
			sprite.SetTopLeft(left, top);
		}

		public static Hourglass[] InstArrayHourglass(int size)
		{
			Hourglass[] array = new Hourglass[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Hourglass();
			}
			return array;
		}

		public static Hourglass[][] InstArrayHourglass(int size1, int size2)
		{
			Hourglass[][] array = new Hourglass[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Hourglass[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Hourglass();
				}
			}
			return array;
		}

		public static Hourglass[][][] InstArrayHourglass(int size1, int size2, int size3)
		{
			Hourglass[][][] array = new Hourglass[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Hourglass[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Hourglass[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Hourglass();
					}
				}
			}
			return array;
		}
	}
}
