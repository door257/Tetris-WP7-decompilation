namespace ca.jamdat.tetrisrevolution
{
	public class IStringConstants
	{
		public const int CHUNK_HDR = 0;

		public const int LANGUAGE_NAME_DISPLAYED = 0;

		public const int LSM_MENU_TITLE = 1;

		public const int LSM_SELECT = 2;

		public const int LSM_BACK = 3;

		public const int TS2_UTF16BE_SPECIFIC_CHARS = 4;

		public const int P2_LONG_HEADER_STRING = 5;

		public const int CHUNK_TS2_ENCODINGS = 1;

		public const int TS2_ENCODING_SPECIFIC_CHARS = 6;

		public const int CHUNK_TS3_WRAPSTRING = 2;

		public const int TS3_WRAPSTRING_TC1 = 7;

		public const int TS3_WRAPSTRING_TC3 = 8;

		public const int TS3_WRAPSTRING_TC4 = 9;

		public const int TS3_WRAPSTRING_TC5 = 10;

		public const int CHUNK_TEST_MOREGAMES = 3;

		public const int TESTMG_TITLE = 11;

		public const int TESTMG_SELECT = 12;

		public const int TESTMG_DONE = 13;

		public const int CHUNK_TS4_SDKSTRING = 4;

		public const int TS4_COMPARE_1 = 14;

		public const int TS4_HAYSTACK_1 = 15;

		public const int CHUNK_TS5_MULTILANGUAGE = 5;

		public const int TS5_SIMPLE_STRING = 16;

		public const int TS5_COMPLEX_STRING = 17;

		public const int CHUNK_TS6_TRANSFORMS = 6;

		public const int TS6_SIMPLE_STRING = 18;

		public const int CHUNK_TS7_REPLACE = 7;

		public const int TS7_REPLACE_TC1 = 19;

		public const int TS7_REPLACE_TC2 = 20;

		public const int TS7_REPLACE_TC3 = 21;

		public const int TS7_REPLACE_TC4 = 22;

		public const int TS7_REPLACE_TC5 = 23;

		public const int TS7_COMPLEX_REPLACE = 24;

		public const int TS7_ORDER_REPLACE = 25;

		public const int TS7_BIG_REPLACE = 26;

		public const int CHUNK_P1_SDKSTRING_PERF = 8;

		public const int P1_TRIM_SHORT_STRING = 27;

		public const int P1_CONCAT_STRING_A = 28;

		public const int P1_CONCAT_STRING_B = 29;

		public const int P1_HAYSTACK = 30;

		public const int P1_COMPARE_STRING_A = 31;

		public const int P1_COMPARE_STRING_B = 32;

		public const int P1_SUBSTRING = 33;

		public const int CHUNK_P2_SDKTEXTUTILS_PERF = 9;

		public const int P2_SHORT_STRING = 34;

		public const int P2_LONG_STRING = 35;

		public const int CHUNK_P2_CHUNK_A = 10;

		public const int P2_CHUNK_A_STRING_A = 36;

		public const int P2_CHUNK_A_STRING_B = 37;

		public const int P2_CHUNK_A_STRING_C = 38;

		public const int P2_CHUNK_A_STRING_D = 39;

		public const int P2_CHUNK_A_STRING_E = 40;

		public const int P2_CHUNK_A_STRING_F = 41;

		public const int P2_CHUNK_A_STRING_G = 42;

		public const int P2_CHUNK_A_STRING_H = 43;

		public const int P2_CHUNK_A_STRING_I = 44;

		public const int P2_CHUNK_A_STRING_J = 45;

		public const int CHUNK_P2_CHUNK_B = 11;

		public const int P2_CHUNK_B_STRING_A = 46;

		public const int P2_CHUNK_B_STRING_B = 47;

		public const int P2_CHUNK_B_STRING_C = 48;

		public const int P2_CHUNK_B_STRING_D = 49;

		public const int P2_CHUNK_B_STRING_E = 50;

		public const int P2_CHUNK_B_STRING_F = 51;

		public const int P2_CHUNK_B_STRING_G = 52;

		public const int P2_CHUNK_B_STRING_H = 53;

		public const int P2_CHUNK_B_STRING_I = 54;

		public const int P2_CHUNK_B_STRING_J = 55;

		public const int CHUNK_MG = 12;

		public const int MG_NAME_TET = 56;

		public const int MG_CTG_TET = 57;

		public const int MG_TAG_TET = 58;

		public const int MG_NAME_MHA = 59;

		public const int MG_CTG_MHA = 60;

		public const int MG_TAG_MHA = 61;

		public const int MG_NAME_FNR = 62;

		public const int MG_CTG_FNR = 63;

		public const int MG_TAG_FNR = 64;

		public const int MG_STATIC = 65;

		public const int MG_GENERIC = 66;

		public const int MG_GENERIC_BTN = 67;

		public const int MG_GENERIC_NAME = 68;

		public const int MG_CONFIRM = 69;

		public const int MG_SELECT = 70;

		public const int MG_BACK = 71;

		public const int MG_YES = 72;

		public const int MG_NO = 73;

		public const int MG_TITLE = 74;

		public const int MG_BUY = 75;

		public const int MG_NAME_F08 = 76;

		public const int MG_NAME_MHN = 77;

		public const int MG_NAME_SM2 = 78;

		public const int MG_NAME_NFS = 79;

		public const int MG_NAME_MONOWW = 80;

		public const int MG_NAME_SPORE1 = 81;

		public const int MAX_STRING_ARRAY_SIZE = 82;

		public const int NUM_CHUNKS = 12;

		public static IStringConstants[] InstArrayIStringConstants(int size)
		{
			IStringConstants[] array = new IStringConstants[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new IStringConstants();
			}
			return array;
		}

		public static IStringConstants[][] InstArrayIStringConstants(int size1, int size2)
		{
			IStringConstants[][] array = new IStringConstants[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new IStringConstants[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new IStringConstants();
				}
			}
			return array;
		}

		public static IStringConstants[][][] InstArrayIStringConstants(int size1, int size2, int size3)
		{
			IStringConstants[][][] array = new IStringConstants[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new IStringConstants[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new IStringConstants[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new IStringConstants();
					}
				}
			}
			return array;
		}
	}
}
