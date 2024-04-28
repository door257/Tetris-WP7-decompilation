namespace ca.jamdat.tetrisrevolution
{
	public class VariantVanilla : TetrisGame
	{
		public VariantVanilla(GameParameter gameParameter)
			: base(gameParameter)
		{
			mPackageId = 720918;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 2;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 66;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 67;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 69;
		}

		public override bool IsGravityEnabled()
		{
			return true;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			GetGameParameters(1);
		}
	}
}
