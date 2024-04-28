using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class CustomComponentUtilities
	{
		public static void Attach(Viewport customComponent, Viewport childrenContainer)
		{
			if (childrenContainer != null)
			{
				customComponent.SetRect(childrenContainer.GetRectLeft(), childrenContainer.GetRectTop(), childrenContainer.GetRectWidth(), childrenContainer.GetRectHeight());
				childrenContainer.SetTopLeft(0, 0);
				Viewport viewport = childrenContainer.GetViewport();
				if (viewport != null)
				{
					customComponent.SetViewport(viewport);
					viewport.PutComponentBehind(customComponent, childrenContainer);
				}
				childrenContainer.SetViewport(customComponent);
			}
		}

		public static void Detach(Viewport customComponent)
		{
			if (customComponent.GetChildCount() != 0)
			{
				Component child = customComponent.GetChild(0);
				Viewport viewport = customComponent.GetViewport();
				child.SetViewport(viewport);
				child.SetRect(customComponent.GetRectLeft(), customComponent.GetRectTop(), customComponent.GetRectWidth(), customComponent.GetRectHeight());
				viewport.PutComponentBehind(child, customComponent);
			}
			customComponent.SetViewport(null);
		}

		public static void DisableLockedVariantSelections(Selector containingSelector)
		{
			FeatsExpert featsExpert = FeatsExpert.Get();
			bool flag = false;
			for (int i = 0; i < 12; i++)
			{
				flag = featsExpert.IsGameVariantUnlocked(i);
				containingSelector.GetSelectionAt(i).SetEnabledState(flag);
			}
		}

		public virtual void destruct()
		{
		}

		public static CustomComponentUtilities[] InstArrayCustomComponentUtilities(int size)
		{
			CustomComponentUtilities[] array = new CustomComponentUtilities[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new CustomComponentUtilities();
			}
			return array;
		}

		public static CustomComponentUtilities[][] InstArrayCustomComponentUtilities(int size1, int size2)
		{
			CustomComponentUtilities[][] array = new CustomComponentUtilities[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CustomComponentUtilities[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CustomComponentUtilities();
				}
			}
			return array;
		}

		public static CustomComponentUtilities[][][] InstArrayCustomComponentUtilities(int size1, int size2, int size3)
		{
			CustomComponentUtilities[][][] array = new CustomComponentUtilities[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new CustomComponentUtilities[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new CustomComponentUtilities[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new CustomComponentUtilities();
					}
				}
			}
			return array;
		}
	}
}
