namespace ca.jamdat.flight
{
	public class TypeNumbers
	{
		public const sbyte NBR_0 = 0;

		public const sbyte NBR_1 = 1;

		public const sbyte NBR_2 = 2;

		public const sbyte NBR_3 = 3;

		public const sbyte NBR_4 = 4;

		public const sbyte NBR_5 = 5;

		public const sbyte NBR_6 = 6;

		public const sbyte NBR_7 = 7;

		public const sbyte NBR_8 = 8;

		public const sbyte NBR_9 = 9;

		public const sbyte NBR_10 = 10;

		public const sbyte NBR_11 = 11;

		public const sbyte NBR_12 = 12;

		public const sbyte NBR_13 = 13;

		public const sbyte NBR_14 = 14;

		public const sbyte NBR_15 = 15;

		public const sbyte NBR_16 = 16;

		public const sbyte NBR_17 = 17;

		public const sbyte NBR_18 = 18;

		public const sbyte NBR_19 = 19;

		public const sbyte NBR_20 = 20;

		public const sbyte NBR_21 = 21;

		public const sbyte NBR_22 = 22;

		public const sbyte NBR_23 = 23;

		public const sbyte NBR_24 = 24;

		public const sbyte NBR_25 = 25;

		public const sbyte NBR_26 = 26;

		public const sbyte NBR_27 = 27;

		public const sbyte NBR_28 = 28;

		public const sbyte NBR_29 = 29;

		public const sbyte NBR_30 = 30;

		public const sbyte NBR_31 = 31;

		public const sbyte NBR_32 = 32;

		public const sbyte NBR_33 = 33;

		public const sbyte NBR_34 = 34;

		public const sbyte NBR_35 = 35;

		public const sbyte NBR_36 = 36;

		public const sbyte NBR_37 = 37;

		public const sbyte NBR_38 = 38;

		public const sbyte NBR_39 = 39;

		public const sbyte NBR_40 = 40;

		public const sbyte NBR_41 = 41;

		public const sbyte NBR_42 = 42;

		public const sbyte NBR_43 = 43;

		public const sbyte NBR_44 = 44;

		public const sbyte NBR_45 = 45;

		public const sbyte NBR_46 = 46;

		public const sbyte NBR_47 = 47;

		public const sbyte NBR_48 = 48;

		public const sbyte NBR_49 = 49;

		public const sbyte NBR_50 = 50;

		public const sbyte NBR_51 = 51;

		public const sbyte NBR_52 = 52;

		public const sbyte NBR_53 = 53;

		public const sbyte NBR_54 = 54;

		public const sbyte NBR_55 = 55;

		public const sbyte NBR_56 = 56;

		public const sbyte NBR_57 = 57;

		public const sbyte NBR_58 = 58;

		public const sbyte NBR_59 = 59;

		public const sbyte NBR_60 = 60;

		public const sbyte NBR_61 = 61;

		public const sbyte NBR_62 = 62;

		public const sbyte NBR_63 = 63;

		public const sbyte NBR_64 = 64;

		public const sbyte NBR_65 = 65;

		public const sbyte NBR_66 = 66;

		public const sbyte NBR_67 = 67;

		public const sbyte NBR_68 = 68;

		public const sbyte NBR_69 = 69;

		public const sbyte NBR_70 = 70;

		public const sbyte NBR_71 = 71;

		public const sbyte NBR_72 = 72;

		public const sbyte NBR_73 = 73;

		public const sbyte NBR_74 = 74;

		public const sbyte NBR_75 = 75;

		public const sbyte NBR_76 = 76;

		public const sbyte NBR_77 = 77;

		public const sbyte NBR_78 = 78;

		public const sbyte NBR_79 = 79;

		public const sbyte NBR_80 = 80;

		public const sbyte NBR_81 = 81;

		public const sbyte NBR_82 = 82;

		public const sbyte NBR_83 = 83;

		public const sbyte NBR_84 = 84;

		public const sbyte NBR_85 = 85;

		public const sbyte NBR_86 = 86;

		public const sbyte NBR_87 = 87;

		public const sbyte NBR_88 = 88;

		public const sbyte NBR_89 = 89;

		public const sbyte NBR_90 = 90;

		public const sbyte NBR_91 = 91;

		public const sbyte NBR_92 = 92;

		public const sbyte NBR_93 = 93;

		public const sbyte NBR_94 = 94;

		public const sbyte NBR_95 = 95;

		public const sbyte NBR_96 = 96;

		public const sbyte NBR_97 = 97;

		public const sbyte NBR_98 = 98;

		public const sbyte NBR_99 = 99;

		public const sbyte NBR_100 = 100;

		public const sbyte NBR_101 = 101;

		public const sbyte NBR_102 = 102;

		public const sbyte NBR_103 = 103;

		public const sbyte NBR_104 = 104;

		public const sbyte NBR_105 = 105;

		public const sbyte NBR_106 = 106;

		public const sbyte NBR_107 = 107;

		public const sbyte NBR_108 = 108;

		public const sbyte NBR_109 = 109;

		public const sbyte NBR_110 = 110;

		public const sbyte NBR_111 = 111;

		public const sbyte NBR_112 = 112;

		public const sbyte NBR_113 = 113;

		public const sbyte NBR_114 = 114;

		public const sbyte NBR_115 = 115;

		public const sbyte NBR_116 = 116;

		public const sbyte NBR_117 = 117;

		public const sbyte NBR_118 = 118;

		public const sbyte NBR_119 = 119;

		public const sbyte NBR_120 = 120;

		public const sbyte NBR_121 = 121;

		public const sbyte NBR_122 = 122;

		public const sbyte NBR_123 = 123;

		public const sbyte NBR_124 = 124;

		public const sbyte NBR_125 = 125;

		public const sbyte NBR_126 = 126;

		public const sbyte NBR_127 = -1;

		public static TypeNumbers[] InstArrayTypeNumbers(int size)
		{
			TypeNumbers[] array = new TypeNumbers[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeNumbers();
			}
			return array;
		}

		public static TypeNumbers[][] InstArrayTypeNumbers(int size1, int size2)
		{
			TypeNumbers[][] array = new TypeNumbers[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeNumbers[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeNumbers();
				}
			}
			return array;
		}

		public static TypeNumbers[][][] InstArrayTypeNumbers(int size1, int size2, int size3)
		{
			TypeNumbers[][][] array = new TypeNumbers[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeNumbers[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeNumbers[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeNumbers();
					}
				}
			}
			return array;
		}
	}
}
