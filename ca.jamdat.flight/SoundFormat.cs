namespace ca.jamdat.flight
{
	public class SoundFormat
	{
		public const short Wave = 0;

		public const short Midi = 1;

		public const short Spf = 2;

		public const short Qcp = 3;

		public const short Aac = 4;

		public const short Tone = 5;

		public const short Mmf = 6;

		public const short Imy = 7;

		public const short Mp3 = 8;

		public const short Pmd = 9;

		public const short Amr = 10;

		public const short M4a = 11;

		public const short Ogg = 12;

		public const short Unknown = 13;

		public static SoundFormat[] InstArraySoundFormat(int size)
		{
			SoundFormat[] array = new SoundFormat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new SoundFormat();
			}
			return array;
		}

		public static SoundFormat[][] InstArraySoundFormat(int size1, int size2)
		{
			SoundFormat[][] array = new SoundFormat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundFormat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundFormat();
				}
			}
			return array;
		}

		public static SoundFormat[][][] InstArraySoundFormat(int size1, int size2, int size3)
		{
			SoundFormat[][][] array = new SoundFormat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new SoundFormat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new SoundFormat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new SoundFormat();
					}
				}
			}
			return array;
		}
	}
}
