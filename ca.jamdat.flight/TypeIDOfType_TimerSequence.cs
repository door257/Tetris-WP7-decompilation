namespace ca.jamdat.flight
{
	public class TypeIDOfType_TimerSequence
	{
		public const sbyte v = 86;

		public const sbyte w = 86;

		public static TypeIDOfType_TimerSequence[] InstArrayTypeIDOfType_TimerSequence(int size)
		{
			TypeIDOfType_TimerSequence[] array = new TypeIDOfType_TimerSequence[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_TimerSequence();
			}
			return array;
		}

		public static TypeIDOfType_TimerSequence[][] InstArrayTypeIDOfType_TimerSequence(int size1, int size2)
		{
			TypeIDOfType_TimerSequence[][] array = new TypeIDOfType_TimerSequence[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TimerSequence[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TimerSequence();
				}
			}
			return array;
		}

		public static TypeIDOfType_TimerSequence[][][] InstArrayTypeIDOfType_TimerSequence(int size1, int size2, int size3)
		{
			TypeIDOfType_TimerSequence[][][] array = new TypeIDOfType_TimerSequence[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TimerSequence[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TimerSequence[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_TimerSequence();
					}
				}
			}
			return array;
		}
	}
}
