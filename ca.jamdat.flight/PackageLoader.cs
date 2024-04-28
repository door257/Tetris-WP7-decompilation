namespace ca.jamdat.flight
{
	public class PackageLoader
	{
		public PtrArray_Package queue;

		public PackageLoader()
		{
			queue = new PtrArray_Package();
		}

		public virtual void destruct()
		{
		}

		public virtual void LoadQueuedPackages()
		{
			while (!queue.IsEmpty())
			{
				Package at = queue.GetAt(0);
				at.LoadFromQueue();
				if (at.IsLoaded())
				{
					queue.Remove(at);
					continue;
				}
				break;
			}
		}

		public virtual void InsertPackageInQueue(Package _package)
		{
			_package.StartLoading();
			queue.Insert(_package);
		}

		public virtual void RemovePackageFromQueue(Package _package)
		{
			queue.Remove(_package);
		}

		public static PackageLoader[] InstArrayPackageLoader(int size)
		{
			PackageLoader[] array = new PackageLoader[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new PackageLoader();
			}
			return array;
		}

		public static PackageLoader[][] InstArrayPackageLoader(int size1, int size2)
		{
			PackageLoader[][] array = new PackageLoader[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PackageLoader[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PackageLoader();
				}
			}
			return array;
		}

		public static PackageLoader[][][] InstArrayPackageLoader(int size1, int size2, int size3)
		{
			PackageLoader[][][] array = new PackageLoader[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new PackageLoader[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new PackageLoader[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new PackageLoader();
					}
				}
			}
			return array;
		}
	}
}
