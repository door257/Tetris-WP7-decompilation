using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class MetaPackage
	{
		public Package mPackage;

		public int mId;

		public int mStateBitField;

		public MetaPackage()
		{
			mId = 98;
		}

		public virtual void destruct()
		{
		}

		public virtual Package GetPackage()
		{
			return mPackage;
		}

		public virtual int GetId()
		{
			return mId;
		}

		public virtual bool IsLoaded()
		{
			if (mPackage != null)
			{
				return mPackage.IsLoaded();
			}
			return false;
		}

		public virtual short GetLanguage()
		{
			return (short)BitField.GetValue(mStateBitField, -65536, 16);
		}

		public virtual int GetResolution()
		{
			return 0;
		}

		public virtual void ReleasePackage()
		{
			Package package = mPackage;
			if (package != null)
			{
				if (package.IsLoaded() || package.IsLoading())
				{
					package.Unload();
				}
				mPackage = null;
			}
			mStateBitField = 0;
		}

		public virtual void SetPackage(Package p, int pkgId, short langId, int resId)
		{
			mPackage = p;
			mId = pkgId;
			SetLanguage(langId);
			if (GameLibrary.IsMultilingualPackage(pkgId))
			{
				MarkAsLanguageDependant();
			}
		}

		public virtual void AddRef()
		{
			mStateBitField = BitField.AddValue(mStateBitField, 1, 16383, 0);
		}

		public virtual void RemoveRef()
		{
			mStateBitField = BitField.AddValue(mStateBitField, -1, 16383, 0);
		}

		public virtual bool IsValid()
		{
			return mPackage != null;
		}

		public virtual int GetRefCount()
		{
			return BitField.GetValue(mStateBitField, 16383, 0);
		}

		public virtual void SetLanguage(short lang)
		{
			mStateBitField = BitField.SetValue(mStateBitField, lang, -65536, 16);
		}

		public virtual bool IsLanguageDependant()
		{
			return BitField.IsBitOn(mStateBitField, 16384);
		}

		public virtual void MarkAsLanguageDependant()
		{
			mStateBitField = BitField.SetBitOn(mStateBitField, 16384);
		}

		public virtual void SetResolution(int res)
		{
		}

		public virtual bool IsResolutionDependant()
		{
			return false;
		}

		public virtual void MarkAsResolutionDependant()
		{
		}

		public static MetaPackage[] InstArrayMetaPackage(int size)
		{
			MetaPackage[] array = new MetaPackage[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new MetaPackage();
			}
			return array;
		}

		public static MetaPackage[][] InstArrayMetaPackage(int size1, int size2)
		{
			MetaPackage[][] array = new MetaPackage[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MetaPackage[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MetaPackage();
				}
			}
			return array;
		}

		public static MetaPackage[][][] InstArrayMetaPackage(int size1, int size2, int size3)
		{
			MetaPackage[][][] array = new MetaPackage[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new MetaPackage[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new MetaPackage[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new MetaPackage();
					}
				}
			}
			return array;
		}
	}
}
