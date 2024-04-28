namespace ca.jamdat.flight
{
	public class TypeIDOfType_KeyFrameSequence
	{
		public const sbyte v = 89;

		public const sbyte w = 89;

		public static TypeIDOfType_KeyFrameSequence[] InstArrayTypeIDOfType_KeyFrameSequence(int size)
		{
			TypeIDOfType_KeyFrameSequence[] array = new TypeIDOfType_KeyFrameSequence[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameSequence();
			}
			return array;
		}

		public static TypeIDOfType_KeyFrameSequence[][] InstArrayTypeIDOfType_KeyFrameSequence(int size1, int size2)
		{
			TypeIDOfType_KeyFrameSequence[][] array = new TypeIDOfType_KeyFrameSequence[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameSequence[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_KeyFrameSequence();
				}
			}
			return array;
		}

		public static TypeIDOfType_KeyFrameSequence[][][] InstArrayTypeIDOfType_KeyFrameSequence(int size1, int size2, int size3)
		{
			TypeIDOfType_KeyFrameSequence[][][] array = new TypeIDOfType_KeyFrameSequence[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_KeyFrameSequence[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_KeyFrameSequence[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_KeyFrameSequence();
					}
				}
			}
			return array;
		}
	}
}
