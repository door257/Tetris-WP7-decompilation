using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class LayerComponent
	{
		public const sbyte noneLayer = -1;

		public const sbyte borderLayer = 0;

		public const sbyte backLayer = 1;

		public const sbyte minoLayer = 2;

		public const sbyte specialMinoLayer = 3;

		public const sbyte nextHudLayer = 4;

		public const sbyte holdHudLayer = 5;

		public const sbyte effectLayer = 6;

		public const sbyte sceneEffectLayer = 7;

		public const sbyte userFeedBackLayer = 8;

		public const sbyte layerCount = 9;

		public Viewport[] mLayerArray;

		public LayerComponent()
		{
			mLayerArray = new Viewport[9];
		}

		public virtual void destruct()
		{
			mLayerArray = null;
		}

		public virtual void GetEntryPoints(Package gameScenePackage)
		{
			gameScenePackage.SetNextEntryPointIndex(45);
			for (int i = 0; i < 9; i++)
			{
				mLayerArray[i] = EntryPoint.GetViewport(gameScenePackage, -1);
			}
		}

		public virtual void Clean()
		{
			Well well = GameFactory.GetTetrisGame().GetWell();
			for (int i = 0; i < 20; i++)
			{
				WellLine line = well.GetLine(i + 20);
				if (!line.HasLockedMinos())
				{
					continue;
				}
				for (int j = 0; j < 10; j++)
				{
					if (line.IsThereLockedMino(j))
					{
						Detach(line.GetLockedMino(j).GetMinoViewport());
					}
				}
			}
			for (int k = 0; k < 9; k++)
			{
				mLayerArray[k] = null;
			}
		}

		public virtual Viewport GetLayer(sbyte layoutId)
		{
			return mLayerArray[layoutId];
		}

		public virtual void Attach(Component component, sbyte layoutId)
		{
			component.SetViewport(mLayerArray[layoutId]);
		}

		public virtual void Detach(Component component)
		{
			component.SetViewport(null);
		}

		public virtual void AttachTetrimino(Tetrimino terimino, sbyte layoutId)
		{
			terimino.SetZone(layoutId);
			for (Mino mino = terimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				Attach(mino.GetMinoViewport(), layoutId);
				mino.AttachMinoComponent();
			}
		}

		public virtual void DetachTetrimino(Tetrimino terimino)
		{
			terimino.SetZone(-1);
			for (Mino mino = terimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				Detach(mino.GetMinoViewport());
			}
		}

		public virtual void AttachAllSpecialMinosToWellIfNeeded(TetriminoList specialMinoList)
		{
			for (Tetrimino tetrimino = specialMinoList.GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				if (tetrimino.IsInZone(-1))
				{
					AttachTetrimino(tetrimino, 3);
				}
			}
		}

		public virtual void ReattachSpecialMinosToWellIfNeeded(TetriminoList specialMinoList)
		{
			for (Tetrimino tetrimino = specialMinoList.GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				sbyte zone = tetrimino.GetZone();
				AttachTetrimino(tetrimino, zone);
			}
		}

		public virtual void ReattachTetriminosToWellIfNeeded(TetriminoList tetriminoList, sbyte layoutId)
		{
			for (Tetrimino tetrimino = tetriminoList.GetRootTetrimino(); tetrimino != null; tetrimino = tetrimino.GetNextNode())
			{
				if (tetrimino.IsInZone(layoutId))
				{
					AttachTetrimino(tetrimino, layoutId);
				}
			}
		}

		public virtual void AttachTetriminoInNextHudOrHoldHud(Tetrimino tetrimino, sbyte layerId)
		{
			AttachTetrimino(tetrimino, layerId);
			PositionNextOrHoldTetriminoSprites(tetrimino);
		}

		public virtual void SetVisible(bool visible)
		{
			mLayerArray[0].SetVisible(visible);
			mLayerArray[4].SetVisible(visible);
			mLayerArray[5].SetVisible(visible);
			mLayerArray[7].SetVisible(visible);
		}

		public virtual void SetUILayersVisible(bool visible)
		{
			GetLayer(3).SetVisible(visible);
			GetLayer(2).SetVisible(visible);
			GetLayer(5).SetVisible(visible);
			GetLayer(4).SetVisible(visible);
			GetLayer(6).SetVisible(visible);
			GetLayer(1).SetVisible(visible);
			GetLayer(8).SetVisible(visible);
		}

		public virtual void AttachToNextQueueLayer(Tetrimino tetrimino, int queueIndex)
		{
			Viewport layer = GetLayer(4);
			Viewport viewport = (Viewport)layer.GetChild(queueIndex);
			tetrimino.SetZone(4);
			for (Mino mino = tetrimino.GetRootMino(); mino != null; mino = mino.GetNextNode())
			{
				mino.GetMinoViewport().SetViewport(viewport);
				mino.AttachMinoComponent();
			}
			PositionNextOrHoldTetriminoSprites(tetrimino);
		}

		public static bool IsAttached(Component component)
		{
			return component.GetViewport() != null;
		}

		public virtual void DisplayNextQueue(int queueLength)
		{
			Viewport layer = GetLayer(4);
			for (int i = 0; i < 5; i++)
			{
				Viewport viewport = (Viewport)layer.GetChild(i);
				viewport.SetVisible(i < queueLength);
			}
		}

		public virtual void PositionNextOrHoldTetriminoSprites(Tetrimino tetrimino)
		{
			Mino mino = tetrimino.GetRootMino();
			bool flag = tetrimino.GetTetriminoType() != 0 && tetrimino.GetTetriminoType() != 3;
			bool flag2 = tetrimino.GetTetriminoType() == 0;
			while (mino != null)
			{
				int baseRelativePosX = mino.GetBaseRelativePosX();
				int baseRelativePosY = mino.GetBaseRelativePosY();
				short num = 0;
				short num2 = 0;
				num = (short)((baseRelativePosX + 1) * 16 + (flag ? 8 : 0));
				num2 = (short)((baseRelativePosY + 1) * 16 + (flag2 ? (-8) : 0));
				mino.GetMinoViewport().SetTopLeft(num, num2);
				mino = mino.GetNextNode();
			}
		}

		public static LayerComponent[] InstArrayLayerComponent(int size)
		{
			LayerComponent[] array = new LayerComponent[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new LayerComponent();
			}
			return array;
		}

		public static LayerComponent[][] InstArrayLayerComponent(int size1, int size2)
		{
			LayerComponent[][] array = new LayerComponent[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LayerComponent[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LayerComponent();
				}
			}
			return array;
		}

		public static LayerComponent[][][] InstArrayLayerComponent(int size1, int size2, int size3)
		{
			LayerComponent[][][] array = new LayerComponent[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new LayerComponent[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new LayerComponent[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new LayerComponent();
					}
				}
			}
			return array;
		}
	}
}
