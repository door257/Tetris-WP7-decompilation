using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class FileManager
	{
		public const int idSettings = 0;

		public const int idGameSettings = 1;

		public const int idBioStatistics = 2;

		public const int idCareerStatistics = 3;

		public const int idExpertManager = 4;

		public const int idHintChooser = 5;

		public const int idCount = 6;

		public const int statusIdle = 0;

		public const int statusWorking = 1;

		public const int statusNotEnoughSpace = 2;

		public const int statusBootCompleted = 3;

		public FileHandler mFile;

		public FileHandler mFileBackup;

		public FileManager()
		{
			FlString flString = new FlString(FlApplication.GetDataDir().Add(StringUtils.CreateString("a")));
			mFile = new FileHandler(flString.Add(new FlString(1)), AllocateSegments(), 6);
			mFileBackup = new FileHandler(flString.Add(new FlString(2)), AllocateSegments(), 6);
		}

		public virtual void destruct()
		{
			mFile = null;
			mFileBackup = null;
		}

		public virtual int OnLoad()
		{
			mFile.OnSerialize(0);
			mFileBackup.OnSerialize(0);
			SelectMostRecentFileAndAlternateBackup();
			ReadAllObjects();
			return 3;
		}

		public virtual bool OnSave()
		{
			return mFile.OnSerialize(1);
		}

		public virtual void WriteObject(int objectId)
		{
			FileHandler fileHandler = mFile;
			GameApp gameApp = GameApp.Get();
			switch (objectId)
			{
			case 0:
				gameApp.GetSettings().Write(fileHandler.GetSegmentStream(0, 1));
				break;
			case 1:
				gameApp.GetGameSettings().Write(fileHandler.GetSegmentStream(1, 1));
				break;
			case 2:
				gameApp.GetBioStatistics().Write(fileHandler.GetSegmentStream(2, 1));
				break;
			case 3:
				gameApp.GetCareerStatistics().Write(fileHandler.GetSegmentStream(3, 1));
				break;
			case 4:
				gameApp.GetExpertManager().Write(fileHandler.GetSegmentStream(4, 1));
				break;
			case 5:
				gameApp.GetTipChooser().Write(fileHandler.GetSegmentStream(5, 1));
				break;
			}
		}

		public virtual void WriteAllObjects()
		{
			for (int i = 0; i < 6; i++)
			{
				WriteObject(i);
			}
		}

		public virtual void ReadObject(int objectId)
		{
			FileHandler fileHandler = mFile;
			GameApp gameApp = GameApp.Get();
			switch (objectId)
			{
			case 0:
				gameApp.GetSettings().Read(fileHandler.GetSegmentStream(0, 0));
				break;
			case 1:
				gameApp.GetGameSettings().Read(fileHandler.GetSegmentStream(1, 0));
				break;
			case 2:
				gameApp.GetBioStatistics().Read(fileHandler.GetSegmentStream(2, 0));
				break;
			case 3:
				gameApp.GetCareerStatistics().Read(fileHandler.GetSegmentStream(3, 0));
				break;
			case 4:
				gameApp.GetExpertManager().Read(fileHandler.GetSegmentStream(4, 0));
				break;
			case 5:
				gameApp.GetTipChooser().Read(fileHandler.GetSegmentStream(5, 0));
				break;
			}
		}

		public virtual void ReadAllObjects()
		{
			for (int i = 0; i < 6; i++)
			{
				ReadObject(i);
			}
		}

		public virtual FileSegmentStream GetInputSegmentStream(int segmentId)
		{
			return mFile.GetSegmentStream(segmentId, 0);
		}

		public virtual FileSegmentStream GetOutputSegmentStream(int segmentId)
		{
			return mFile.GetSegmentStream(segmentId, 1);
		}

		public virtual void ResetSegmentStream()
		{
			mFile.ResetSerializableData();
		}

		public virtual FileSegmentStream[] AllocateSegments()
		{
			FileSegmentStream[] array = new FileSegmentStream[6];
			array[1] = new FileSegmentStream(8);
			array[0] = new FileSegmentStream(18);
			array[2] = new FileSegmentStream(40);
			array[3] = new FileSegmentStream(260);
			array[4] = new FileSegmentStream(38);
			array[5] = new FileSegmentStream(13);
			return array;
		}

		public virtual void LoadOrCreateSynchronousFile(FileHandler file, bool fileExist)
		{
			if (fileExist)
			{
				file.OnSerialize(0);
			}
			else
			{
				file.OnSerialize(1);
			}
		}

		public virtual void SelectMostRecentFileAndAlternateBackup()
		{
			if (mFileBackup.GetWritingCount() > mFile.GetWritingCount())
			{
				mFileBackup.SetPathname(mFile.GetPathname());
				mFile = null;
				mFile = mFileBackup;
				mFileBackup = null;
			}
			else
			{
				mFile.SetPathname(mFileBackup.GetPathname());
				mFileBackup = null;
			}
		}

		public static FileManager[] InstArrayFileManager(int size)
		{
			FileManager[] array = new FileManager[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FileManager();
			}
			return array;
		}

		public static FileManager[][] InstArrayFileManager(int size1, int size2)
		{
			FileManager[][] array = new FileManager[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileManager[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileManager();
				}
			}
			return array;
		}

		public static FileManager[][][] InstArrayFileManager(int size1, int size2, int size3)
		{
			FileManager[][][] array = new FileManager[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileManager[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileManager[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FileManager();
					}
				}
			}
			return array;
		}
	}
}
