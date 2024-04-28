using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ca.jamdat.flight
{
	public class FrameworkGlobals
	{
		public FlApplication application;

		public FlKeyManager mFlKeyManager;

		public int userInputDisabled;

		public int randomState;

		public PackageLoader mPackageLoader;

		public XNAApp xnaApp;

		public SoundManager soundManager;

		public Component penTracker;

		public FlPenManager mFlPenManager;

		public FlLog mFlLog;

		public SignedInGamer gamer;

		protected GraphicsDeviceManager mGraphicsDeviceManager;

		protected ContentManager mContentManager;

		protected SpriteBatch mSpriteBatch;

		public GraphicsDeviceManager GraphicsDeviceManager
		{
			get
			{
				return mGraphicsDeviceManager;
			}
			set
			{
				mGraphicsDeviceManager = value;
			}
		}

		public ContentManager ContentManager
		{
			get
			{
				return mContentManager;
			}
			set
			{
				mContentManager = value;
			}
		}

		public SpriteBatch SpriteBatch
		{
			get
			{
				return mSpriteBatch;
			}
			set
			{
				mSpriteBatch = value;
			}
		}

		public FrameworkGlobals()
		{
			mPackageLoader = new PackageLoader();
		}

		public virtual void destruct()
		{
			Delete();
		}

		public virtual void Delete()
		{
		}

		public static FrameworkGlobals GetInstance()
		{
			return XNAApp.mFrameworkGlobals;
		}

		public static FrameworkGlobals[] InstArrayFrameworkGlobals(int size)
		{
			FrameworkGlobals[] array = new FrameworkGlobals[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new FrameworkGlobals();
			}
			return array;
		}

		public static FrameworkGlobals[][] InstArrayFrameworkGlobals(int size1, int size2)
		{
			FrameworkGlobals[][] array = new FrameworkGlobals[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FrameworkGlobals[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FrameworkGlobals();
				}
			}
			return array;
		}

		public static FrameworkGlobals[][][] InstArrayFrameworkGlobals(int size1, int size2, int size3)
		{
			FrameworkGlobals[][][] array = new FrameworkGlobals[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new FrameworkGlobals[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new FrameworkGlobals[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new FrameworkGlobals();
					}
				}
			}
			return array;
		}
	}
}
