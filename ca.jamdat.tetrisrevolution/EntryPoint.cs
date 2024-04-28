using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class EntryPoint
	{
		public static int GetInt(Package p, int entryPoint)
		{
			return p.GetEntryPoint(entryPoint, (int[])null);
		}

		public static FlBitmap GetFlBitmap(Package p, int entryPoint)
		{
			FlBitmap flBitmap = null;
			return FlBitmap.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static FlBitmap GetFlBitmap(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetFlBitmap(preLoadedPackage, entryPoint);
		}

		public static FlBitmapMap GetFlBitmapMap(Package p, int entryPoint)
		{
			FlBitmapMap flBitmapMap = null;
			return FlBitmapMap.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static FlBitmapMap GetFlBitmapMap(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetFlBitmapMap(preLoadedPackage, entryPoint);
		}

		public static Blob GetBlob(Package p, int entryPoint)
		{
			Blob blob = null;
			return Blob.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Blob GetBlob(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetBlob(preLoadedPackage, entryPoint);
		}

		public static Color888 GetFlColor(Package p, int entryPoint)
		{
			Color888 color = null;
			return Color888.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Color888 GetFlColor(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetFlColor(preLoadedPackage, entryPoint);
		}

		public static Component GetComponent(Package p, int entryPoint)
		{
			Component component = null;
			return Component.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Component GetComponent(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetComponent(preLoadedPackage, entryPoint);
		}

		public static Controller GetController(Package p, int entryPoint)
		{
			Controller controller = null;
			return Controller.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Controller GetController(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetController(preLoadedPackage, entryPoint);
		}

		public static FlFont GetFlFont(Package p, int entryPoint)
		{
			FlFont flFont = null;
			return FlFont.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static FlFont GetFlFont(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetFlFont(preLoadedPackage, entryPoint);
		}

		public static IndexedSprite GetIndexedSprite(Package p, int entryPoint)
		{
			IndexedSprite indexedSprite = null;
			return IndexedSprite.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static IndexedSprite GetIndexedSprite(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetIndexedSprite(preLoadedPackage, entryPoint);
		}

		public static KeyFrameController GetKeyFrameController(Package p, int entryPoint)
		{
			KeyFrameController keyFrameController = null;
			return KeyFrameController.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static KeyFrameController GetKeyFrameController(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetKeyFrameController(preLoadedPackage, entryPoint);
		}

		public static KeyFrameSequence GetKeyFrameSequence(Package p, int entryPoint)
		{
			KeyFrameSequence keyFrameSequence = null;
			return KeyFrameSequence.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static KeyFrameSequence GetKeyFrameSequence(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetKeyFrameSequence(preLoadedPackage, entryPoint);
		}

		public static Scroller GetScroller(Package p, int entryPoint)
		{
			Scroller scroller = null;
			return Scroller.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Scroller GetScroller(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetScroller(preLoadedPackage, entryPoint);
		}

		public static Selection GetSelection(Package p, int entryPoint)
		{
			Selection selection = null;
			return Selection.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Selection GetSelection(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetSelection(preLoadedPackage, entryPoint);
		}

		public static Selector GetSelector(Package p, int entryPoint)
		{
			Selector selector = null;
			return Selector.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Selector GetSelector(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetSelector(preLoadedPackage, entryPoint);
		}

		public static Shape GetShape(Package p, int entryPoint)
		{
			Shape shape = null;
			return Shape.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Shape GetShape(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetShape(preLoadedPackage, entryPoint);
		}

		public static Sound GetSound(Package p, int entryPoint)
		{
			Sound sound = null;
			return Sound.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Sound GetSound(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetSound(preLoadedPackage, entryPoint);
		}

		public static Sprite GetSprite(Package p, int entryPoint)
		{
			Sprite sprite = null;
			return Sprite.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Sprite GetSprite(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetSprite(preLoadedPackage, entryPoint);
		}

		public static FlString GetFlString(Package p, int entryPoint)
		{
			FlString flString = null;
			return FlString.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static FlString GetFlString(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetFlString(preLoadedPackage, entryPoint);
		}

		public static Text GetText(Package p, int entryPoint)
		{
			Text text = null;
			return Text.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Text GetText(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetText(preLoadedPackage, entryPoint);
		}

		public static TextField GetTextField(Package p, int entryPoint)
		{
			TextField textField = null;
			return TextField.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static TextField GetTextField(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetTextField(preLoadedPackage, entryPoint);
		}

		public static TimeControlled GetTimeControlled(Package p, int entryPoint)
		{
			TimeControlled timeControlled = null;
			return TimeControlled.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static TimeControlled GetTimeControlled(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetTimeControlled(preLoadedPackage, entryPoint);
		}

		public static TimerSequence GetTimerSequence(Package p, int entryPoint)
		{
			TimerSequence timerSequence = null;
			return TimerSequence.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static TimerSequence GetTimerSequence(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetTimerSequence(preLoadedPackage, entryPoint);
		}

		public static TimeSystem GetTimeSystem(Package p, int entryPoint)
		{
			TimeSystem timeSystem = null;
			return TimeSystem.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static TimeSystem GetTimeSystem(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetTimeSystem(preLoadedPackage, entryPoint);
		}

		public static Viewport GetViewport(Package p, int entryPoint)
		{
			Viewport viewport = null;
			return Viewport.Cast(p.GetEntryPoint(entryPoint), null);
		}

		public static Viewport GetViewport(int pkgId, int entryPoint)
		{
			Package preLoadedPackage = GameLibrary.GetPreLoadedPackage(pkgId);
			return GetViewport(preLoadedPackage, entryPoint);
		}

		public static EntryPoint[] InstArrayEntryPoint(int size)
		{
			EntryPoint[] array = new EntryPoint[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new EntryPoint();
			}
			return array;
		}

		public static EntryPoint[][] InstArrayEntryPoint(int size1, int size2)
		{
			EntryPoint[][] array = new EntryPoint[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EntryPoint[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EntryPoint();
				}
			}
			return array;
		}

		public static EntryPoint[][][] InstArrayEntryPoint(int size1, int size2, int size3)
		{
			EntryPoint[][][] array = new EntryPoint[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new EntryPoint[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new EntryPoint[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new EntryPoint();
					}
				}
			}
			return array;
		}
	}
}
