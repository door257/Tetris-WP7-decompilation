namespace ca.jamdat.flight
{
	public class TypeIDOfType_PtrArray_KeyFrameSequence
	{
		public const sbyte v = 105;

		public const sbyte w = 105;

		public static TypeIDOfType_PtrArray_KeyFrameSequence[] InstArrayTypeIDOfType_PtrArray_KeyFrameSequence(int size)
		{
			TypeIDOfType_PtrArray_KeyFrameSequence[] array = new TypeIDOfType_PtrArray_KeyFrameSequence[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_KeyFrameSequence();
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_KeyFrameSequence[][] InstArrayTypeIDOfType_PtrArray_KeyFrameSequence(int size1, int size2)
		{
			TypeIDOfType_PtrArray_KeyFrameSequence[][] array = new TypeIDOfType_PtrArray_KeyFrameSequence[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_KeyFrameSequence[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_KeyFrameSequence();
				}
			}
			return array;
		}

		public static TypeIDOfType_PtrArray_KeyFrameSequence[][][] InstArrayTypeIDOfType_PtrArray_KeyFrameSequence(int size1, int size2, int size3)
		{
			TypeIDOfType_PtrArray_KeyFrameSequence[][][] array = new TypeIDOfType_PtrArray_KeyFrameSequence[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_PtrArray_KeyFrameSequence[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_PtrArray_KeyFrameSequence[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_PtrArray_KeyFrameSequence();
					}
				}
			}
			return array;
		}
	}
}
