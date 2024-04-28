using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public abstract class Zone
	{
		public int mCommand;

		public Component mVisualComponent;

		public Zone()
		{
			mCommand = 0;
		}

		public virtual void destruct()
		{
		}

		public abstract bool IsInside(short a3, short a2);

		public virtual void Show()
		{
			Viewport viewport = GameApp.Get();
			if (mVisualComponent == null)
			{
				mVisualComponent = CreateVisualComponent();
			}
			mVisualComponent.SetViewport(viewport);
		}

		public virtual void Hide()
		{
			if (mVisualComponent != null)
			{
				mVisualComponent.SetViewport(null);
				mVisualComponent = null;
			}
		}

		public abstract Vector2_short GetCenter();

		public virtual int GetCommand()
		{
			return mCommand;
		}

		public virtual void SetCommand(int cmd)
		{
			mCommand = cmd;
		}

		public abstract Component CreateVisualComponent();

		public virtual void Refresh()
		{
			if (mVisualComponent != null)
			{
				Hide();
				Show();
			}
		}
	}
}
