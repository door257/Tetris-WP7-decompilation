namespace ca.jamdat.flight
{
	public class FileStreamEnums
	{
		public const sbyte fileModeRead = 0;

		public const sbyte fileModeWrite = 1;

		public const sbyte fileModeAppend = 2;

		public const sbyte streamSeekOriginStart = 0;

		public const sbyte streamSeekOriginEnd = 1;

		public const sbyte streamSeekOriginCurrent = 2;

		public const sbyte fileErrorNoError = 0;

		public const sbyte fileErrorNotFound = 1;

		public const sbyte fileErrorAlreadyExist = 2;

		public const sbyte fileErrorErrorSeeking = 3;

		public const sbyte fileErrorUnexpectedError = 4;

		public const sbyte fileErrorPermissionDenied = 5;

		public const sbyte fileErrorInActivation = 6;

		public const sbyte fileErrorAccessDenied = 7;

		public const sbyte fileErrorReadOnly = 8;

		public const sbyte fileErrorOutOfMemory = 9;

		public const sbyte licenseActionActivate = 0;

		public const sbyte licenseActionConsumeStart = 1;

		public const sbyte licenseActionConsumePause = 2;

		public const sbyte licenseActionConsumeStop = 3;

		public const sbyte licenseStateNone = 0;

		public const sbyte licenseStateNotAvailable = 1;

		public const sbyte licenseStateValid = 2;

		public const sbyte licenseStateExpired = 3;

		public const sbyte licenseStateMimeTypeNotSupported = 4;

		public const short fileBufferDefault = 512;

		public static FileStreamEnums[] InstArrayFileStreamEnums(int size)
		{
			FileStreamEnums[] array = new FileStreamEnums[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FileStreamEnums();
			}
			return array;
		}

		public static FileStreamEnums[][] InstArrayFileStreamEnums(int size1, int size2)
		{
			FileStreamEnums[][] array = new FileStreamEnums[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileStreamEnums[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileStreamEnums();
				}
			}
			return array;
		}

		public static FileStreamEnums[][][] InstArrayFileStreamEnums(int size1, int size2, int size3)
		{
			FileStreamEnums[][][] array = new FileStreamEnums[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FileStreamEnums[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FileStreamEnums[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FileStreamEnums();
					}
				}
			}
			return array;
		}
	}
}
