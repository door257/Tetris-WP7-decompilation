using System;

namespace ca.jamdat.flight
{
	public class Sound
	{
		public const sbyte typeNumber = 83;

		public const sbyte typeID = 83;

		public const bool supportsDynamicSerialization = true;

		public string mSoundFormat;

		public Blob mDataBlob;

		public static Sound Cast(object o, Sound _)
		{
			return (Sound)o;
		}

		public virtual sbyte GetTypeID()
		{
			return 83;
		}

		public static Type AsClass()
		{
			return null;
		}

		public Sound()
		{
			mDataBlob = new Blob();
		}

		public Sound(Blob dataBlob, string format)
		{
			mDataBlob = null;
			SetSoundFormat(format);
			SetDataBlob(dataBlob);
		}

		public virtual void destruct()
		{
			mDataBlob = null;
		}

		public virtual Blob GetDataBlob()
		{
			return mDataBlob;
		}

		public virtual void OnSerialize(Package _package)
		{
			int t = 0;
			t = _package.SerializeIntrinsic(t);
			string text = ConvertSoundFormat(t);
			SerializeForJ2me(_package);
			mSoundFormat = text;
		}

		public virtual void SerializeForJ2me(Package _package)
		{
			mDataBlob = mDataBlob.OnSerialize(_package);
		}

		public virtual string GetSoundFormat()
		{
			return mSoundFormat;
		}

		public virtual void SetSoundFormat(string soundFormat)
		{
			mSoundFormat = soundFormat;
		}

		public virtual string ConvertSoundFormat(int soundFormat)
		{
			switch (soundFormat)
			{
			case 0:
				return "audio/x-wav";
			case 1:
				return "audio/midi";
			case 2:
				return "Undefined";
			case 3:
				return "audio/vnd.qcelp";
			case 4:
				return "audio/aac";
			case 5:
				return "audio/x-tone-seq";
			case 6:
				return "application/x-smaf";
			case 7:
				return "text/x-imelody";
			case 8:
				return "audio/mpeg";
			case 10:
				return "audio/amr";
			case 9:
				return "Undefined";
			case 11:
				return "Undefined";
			case 12:
				return "Undefined";
			default:
				return "Undefined";
			}
		}

		public virtual void SetDataBlob(Blob dataBlob)
		{
			mDataBlob = null;
			mDataBlob = dataBlob;
		}

		public static Sound[] InstArraySound(int size)
		{
			Sound[] array = new Sound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Sound();
			}
			return array;
		}

		public static Sound[][] InstArraySound(int size1, int size2)
		{
			Sound[][] array = new Sound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Sound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Sound();
				}
			}
			return array;
		}

		public static Sound[][][] InstArraySound(int size1, int size2, int size3)
		{
			Sound[][][] array = new Sound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Sound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Sound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Sound();
					}
				}
			}
			return array;
		}
	}
}
