using ca.jamdat.flight;

namespace ca.jamdat.fuser
{
	public class ObjectReader
	{
		public static object Read(Package p, sbyte typeID)
		{
			object obj = null;
			switch (typeID)
			{
			case 93:
				obj = new Array_int();
				((Array_int)obj).OnSerialize(p);
				break;
			case 41:
				obj = new Blob();
				obj = ((Blob)obj).OnSerialize(p);
				break;
			case 20:
				obj = new Color888();
				((Color888)obj).OnSerialize(p);
				break;
			case 51:
				obj = new FlBitmapFontBlob();
				obj = ((FlBitmapFontBlob)obj).OnSerialize(p);
				break;
			case 91:
				obj = new FlBitmapImplementor();
				((FlBitmapImplementor)obj).OnSerialize(p);
				break;
			case 37:
				obj = new FlBitmapMap();
				((FlBitmapMap)obj).OnSerialize(p);
				break;
			case 52:
				obj = new FlBitmapMapBlob();
				obj = ((FlBitmapMapBlob)obj).OnSerialize(p);
				break;
			case 36:
				obj = new FlFont();
				((FlFont)obj).OnSerialize(p);
				break;
			case 111:
				obj = new FlKerningPair();
				((FlKerningPair)obj).OnSerialize(p);
				break;
			case 35:
				obj = new FlString();
				((FlString)obj).OnSerialize(p);
				break;
			case 90:
				obj = new IndexedSprite();
				((IndexedSprite)obj).OnSerialize(p);
				break;
			case 88:
				obj = new KeyFrameController();
				((KeyFrameController)obj).OnSerialize(p);
				break;
			case 89:
				obj = new KeyFrameSequence();
				obj = ((KeyFrameSequence)obj).OnSerialize(p);
				break;
			case 33:
				obj = new Palette();
				obj = ((Palette)obj).OnSerialize(p);
				break;
			case 43:
				obj = new RepalettizedBitmap();
				((RepalettizedBitmap)obj).OnSerialize(p);
				break;
			case 98:
				obj = new Scroller();
				((Scroller)obj).OnSerialize(p);
				break;
			case 97:
				obj = new Selection();
				((Selection)obj).OnSerialize(p);
				break;
			case 96:
				obj = new Selector();
				((Selector)obj).OnSerialize(p);
				break;
			case 76:
				obj = new Shape();
				((Shape)obj).OnSerialize(p);
				break;
			case 83:
				obj = new Sound();
				((Sound)obj).OnSerialize(p);
				break;
			case 53:
				obj = new Sprite();
				((Sprite)obj).OnSerialize(p);
				break;
			case 71:
				obj = new Text();
				((Text)obj).OnSerialize(p);
				break;
			case 85:
				obj = new TimeSystem();
				((TimeSystem)obj).OnSerialize(p);
				break;
			case 86:
				obj = new TimerSequence();
				((TimerSequence)obj).OnSerialize(p);
				break;
			case 68:
				obj = new Viewport();
				((Viewport)obj).OnSerialize(p);
				break;
			case 1:
				obj = new bool?(p.SerializeIntrinsic(false));
				break;
			case 2:
				obj = new sbyte?(p.SerializeIntrinsic((sbyte)0));
				break;
			case 3:
				obj = new short?(p.SerializeIntrinsic((short)0));
				break;
			case 5:
				obj = new int?(p.SerializeIntrinsic(0));
				break;
			default:
				return null;
			}
			return obj;
		}
	}
}
