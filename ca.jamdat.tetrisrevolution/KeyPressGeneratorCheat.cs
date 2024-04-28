namespace ca.jamdat.tetrisrevolution
{
	public class KeyPressGeneratorCheat : Cheat
	{
		public override void destruct()
		{
		}

		public override void Activate(int param)
		{
			base.Activate(param);
			GameApp.Get().GetKeyPressGenerator().Activate(0);
		}

		public override void Deactivate()
		{
			base.Deactivate();
			GameApp.Get().GetKeyPressGenerator().Deactivate();
		}

		public static KeyPressGeneratorCheat[] InstArrayKeyPressGeneratorCheat(int size)
		{
			KeyPressGeneratorCheat[] array = new KeyPressGeneratorCheat[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new KeyPressGeneratorCheat();
			}
			return array;
		}

		public static KeyPressGeneratorCheat[][] InstArrayKeyPressGeneratorCheat(int size1, int size2)
		{
			KeyPressGeneratorCheat[][] array = new KeyPressGeneratorCheat[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyPressGeneratorCheat[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyPressGeneratorCheat();
				}
			}
			return array;
		}

		public static KeyPressGeneratorCheat[][][] InstArrayKeyPressGeneratorCheat(int size1, int size2, int size3)
		{
			KeyPressGeneratorCheat[][][] array = new KeyPressGeneratorCheat[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyPressGeneratorCheat[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyPressGeneratorCheat[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new KeyPressGeneratorCheat();
					}
				}
			}
			return array;
		}
	}
}
