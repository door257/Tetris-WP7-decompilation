namespace ca.jamdat.tetrisrevolution
{
	public class VariantBasic : TetrisGame
	{
		public VariantBasic(GameParameter gameParameter)
			: base(gameParameter)
		{
			mPackageId = 655380;
		}

		public override void destruct()
		{
		}

		public override int GetVariant()
		{
			return 0;
		}

		public override int GetGameTitleStringEntryPoint()
		{
			return 58;
		}

		public override int GetQuickHintStringEntryPoint()
		{
			return 59;
		}

		public override int GetLongHintStringEntryPoint()
		{
			return 61;
		}

		public override bool IsGravityEnabled()
		{
			return false;
		}

		public override void GetEntryPoints()
		{
			base.GetEntryPoints();
			GetGameParameters(1);
		}
	}
}
