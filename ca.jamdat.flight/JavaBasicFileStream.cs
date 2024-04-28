using System;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;

namespace ca.jamdat.flight
{
	public class JavaBasicFileStream
	{
		public static int WriteFile(FlString filename, sbyte[] data, int size, sbyte fileMode)
		{
			IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
			IsolatedStorageFileStream isolatedStorageFileStream = null;
			try
			{
				isolatedStorageFileStream = userStoreForApplication.OpenFile(filename.NativeString, FileMode.OpenOrCreate);
				if (fileMode == 2)
				{
					isolatedStorageFileStream.Seek(0L, SeekOrigin.End);
				}
				byte[] array = new byte[data.Length];
				array = (byte[])(object)data;
				isolatedStorageFileStream.Write(array, 0, size);
				isolatedStorageFileStream.Close();
				return size;
			}
			catch (IsolatedStorageException exception)
			{
				FlLog.Log(exception);
				return -1;
			}
		}

		public static sbyte[] ReadFile(FlString filename)
		{
			try
			{
				IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
				IsolatedStorageFileStream isolatedStorageFileStream = null;
				isolatedStorageFileStream = userStoreForApplication.OpenFile(filename.NativeString, FileMode.OpenOrCreate);
				int num = (int)isolatedStorageFileStream.Length;
				sbyte[] array = new sbyte[num];
				int i = 0;
				for (int num2 = 0; i < num; i += num2)
				{
					if (num2 == -1)
					{
						break;
					}
					byte[] array2 = new byte[array.Length];
					array2 = (byte[])(object)array;
					num2 = isolatedStorageFileStream.Read(array2, i, num - i);
				}
				isolatedStorageFileStream.Close();
				return array;
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				return null;
			}
		}

		public static int GetFileSize(FlString filename)
		{
			try
			{
				System.IO.Stream stream = TitleContainer.OpenStream(filename.NativeString);
				int result = (int)stream.Length;
				stream.Close();
				return result;
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				return -1;
			}
		}

		public static bool FileExists(FlString filename)
		{
			bool flag = false;
			System.IO.Stream resourceAsStream = LibraryStream.GetResourceAsStream(filename.NativeString);
			if (resourceAsStream == null)
			{
				flag = false;
			}
			else
			{
				flag = true;
				try
				{
					resourceAsStream.Close();
				}
				catch (Exception exception)
				{
					FlLog.Log(exception);
				}
			}
			return flag;
		}

		public static bool FileDelete(FlString filename)
		{
			try
			{
				File.Delete(filename.NativeString);
				return true;
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
				return false;
			}
		}

		public static JavaBasicFileStream[] InstArrayJavaBasicFileStream(int size)
		{
			JavaBasicFileStream[] array = new JavaBasicFileStream[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new JavaBasicFileStream();
			}
			return array;
		}

		public static JavaBasicFileStream[][] InstArrayJavaBasicFileStream(int size1, int size2)
		{
			JavaBasicFileStream[][] array = new JavaBasicFileStream[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new JavaBasicFileStream[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new JavaBasicFileStream();
				}
			}
			return array;
		}

		public static JavaBasicFileStream[][][] InstArrayJavaBasicFileStream(int size1, int size2, int size3)
		{
			JavaBasicFileStream[][][] array = new JavaBasicFileStream[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new JavaBasicFileStream[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new JavaBasicFileStream[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new JavaBasicFileStream();
					}
				}
			}
			return array;
		}
	}
}
