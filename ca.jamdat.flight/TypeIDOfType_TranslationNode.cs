namespace ca.jamdat.flight
{
	public class TypeIDOfType_TranslationNode
	{
		public const sbyte v = 26;

		public const sbyte w = 26;

		public static TypeIDOfType_TranslationNode[] InstArrayTypeIDOfType_TranslationNode(int size)
		{
			TypeIDOfType_TranslationNode[] array = new TypeIDOfType_TranslationNode[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new TypeIDOfType_TranslationNode();
			}
			return array;
		}

		public static TypeIDOfType_TranslationNode[][] InstArrayTypeIDOfType_TranslationNode(int size1, int size2)
		{
			TypeIDOfType_TranslationNode[][] array = new TypeIDOfType_TranslationNode[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TranslationNode[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TranslationNode();
				}
			}
			return array;
		}

		public static TypeIDOfType_TranslationNode[][][] InstArrayTypeIDOfType_TranslationNode(int size1, int size2, int size3)
		{
			TypeIDOfType_TranslationNode[][][] array = new TypeIDOfType_TranslationNode[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new TypeIDOfType_TranslationNode[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new TypeIDOfType_TranslationNode[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new TypeIDOfType_TranslationNode();
					}
				}
			}
			return array;
		}
	}
}
