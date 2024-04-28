namespace ca.jamdat.tetrisrevolution
{
	public class SpecialMino : Tetrimino
	{
		public const int kAttributeNone = 0;

		public const int kAttributeFloating = 4;

		public const int kAttributeGravitySensitive = 8;

		public int mAttribute;

		public SpecialMino(Well well, int initialNbOfMinos)
			: base(well, initialNbOfMinos)
		{
			mAttribute = 0;
		}

		public override void destruct()
		{
		}

		public override sbyte GetTetriminoType()
		{
			return -2;
		}

		public override bool CanRotate(int adjX, int adjY, int newFacingDir)
		{
			return true;
		}

		public virtual void SetSpecialType(sbyte minoType)
		{
			mMinoList.GetRootMino().SetType(minoType);
			mAttribute = GetMinoAttributes(minoType);
		}

		public override bool IsGravitySensitive()
		{
			return (mAttribute & 8) != 0;
		}

		public override bool IsFloating()
		{
			return (mAttribute & 4) != 0;
		}

		public virtual int GetMinoAttributes(sbyte minoType)
		{
			int result = 0;
			switch (minoType)
			{
			case 10:
				result = 4;
				break;
			case 13:
				result = 4;
				break;
			case 12:
				result = 8;
				break;
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
				result = 8;
				break;
			}
			return result;
		}

		public SpecialMino(Well pWell)
			: this(pWell, 1)
		{
		}
	}
}
