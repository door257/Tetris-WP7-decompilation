namespace ca.jamdat.flight
{
	public class TypeIDOfType_MidiSound
	{
		public const sbyte v = 58;

		public const sbyte w = 58;

		public static TypeIDOfType_MidiSound[] InstArrayTypeIDOfType_MidiSound(int size)
		{
			TypeIDOfType_MidiSound[] array = new TypeIDOfType_MidiSound[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_MidiSound();
			}
			return array;
		}

		public static TypeIDOfType_MidiSound[][] InstArrayTypeIDOfType_MidiSound(int size1, int size2)
		{
			TypeIDOfType_MidiSound[][] array = new TypeIDOfType_MidiSound[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MidiSound[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MidiSound();
				}
			}
			return array;
		}

		public static TypeIDOfType_MidiSound[][][] InstArrayTypeIDOfType_MidiSound(int size1, int size2, int size3)
		{
			TypeIDOfType_MidiSound[][][] array = new TypeIDOfType_MidiSound[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_MidiSound[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_MidiSound[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_MidiSound();
					}
				}
			}
			return array;
		}
	}
}
