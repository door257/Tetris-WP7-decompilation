using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FileSerializationFactory
	{
		public virtual void destruct()
		{
		}

		public static FileReader CreateReader(FileSegmentStream fileSegStream)
		{
			return new FileSegmentStreamReader(fileSegStream);
		}

		public static FileReader CreateReader(Blob blobFile)
		{
			return new BlobReader(blobFile);
		}

		public static FileWriter CreateWriter(FileSegmentStream fileSegStream)
		{
			return new FileSegmentStreamWriter(fileSegStream);
		}

		public static FileSerializationFactory[] InstArrayFileSerializationFactory(int size)
		{
			FileSerializationFactory[] array = new FileSerializationFactory[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FileSerializationFactory();
			}
			return array;
		}

		public static FileSerializationFactory[][] InstArrayFileSerializationFactory(int size1, int size2)
		{
			FileSerializationFactory[][] array = new FileSerializationFactory[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileSerializationFactory[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileSerializationFactory();
				}
			}
			return array;
		}

		public static FileSerializationFactory[][][] InstArrayFileSerializationFactory(int size1, int size2, int size3)
		{
			FileSerializationFactory[][][] array = new FileSerializationFactory[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileSerializationFactory[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileSerializationFactory[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FileSerializationFactory();
					}
				}
			}
			return array;
		}
	}
}
