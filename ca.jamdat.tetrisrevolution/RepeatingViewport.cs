using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class RepeatingViewport : Viewport
	{
		public Viewport mContentViewport;

		public new short mOffsetX;

		public new short mOffsetY;

		public RepeatingViewport(Viewport viewport)
			: base(viewport)
		{
			mOffsetX = 0;
			mOffsetY = 0;
		}

		public override void destruct()
		{
		}

		public virtual void Initialize()
		{
			SetClipChildren(true);
			mContentViewport = new Viewport(this);
			mContentViewport.SetClipChildren(true);
			SetSize(GetViewport().GetSize());
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			short clippingRectLeft = displayContext.GetClippingRectLeft();
			short clippingRectTop = displayContext.GetClippingRectTop();
			short clippingRectWidth = displayContext.GetClippingRectWidth();
			short clippingRectHeight = displayContext.GetClippingRectHeight();
			short num = GetRectLeft();
			short num2 = GetRectTop();
			short rectWidth = GetRectWidth();
			short rectHeight = GetRectHeight();
			int num3 = clippingRectLeft + clippingRectWidth;
			int num4 = num + rectWidth;
			int num5 = clippingRectTop + clippingRectHeight;
			int num6 = num2 + rectHeight;
			if (clippingRectLeft > num)
			{
				num = clippingRectLeft;
			}
			if (clippingRectTop > num2)
			{
				num2 = clippingRectTop;
			}
			if (num3 < num4)
			{
				num4 = num3;
			}
			if (num5 < num6)
			{
				num6 = num5;
			}
			rectWidth = (short)(num4 - num);
			rectHeight = (short)(num6 - num2);
			if (rectWidth <= 0 || rectHeight <= 0 || mContentViewport.GetRectHeight() <= 0 || mContentViewport.GetRectWidth() <= 0)
			{
				return;
			}
			displayContext.SetClippingRect(num, num2, rectWidth, rectHeight);
			for (short num7 = GetMinOffset(mOffsetX, mContentViewport.GetRectWidth(), (short)(-mContentViewport.GetRectWidth())); num7 < GetRectWidth(); num7 = (short)(num7 + mContentViewport.GetRectWidth()))
			{
				for (short num8 = GetMinOffset(mOffsetY, mContentViewport.GetRectHeight(), (short)(-mContentViewport.GetRectHeight())); num8 < GetRectHeight(); num8 = (short)(num8 + mContentViewport.GetRectHeight()))
				{
					DrawContentViewport(displayContext, num7, num8);
				}
			}
			displayContext.SetClippingRect(clippingRectLeft, clippingRectTop, clippingRectWidth, clippingRectHeight);
		}

		public virtual void AddComponent(Component component)
		{
			short num = (short)(component.GetRectTop() + component.GetRectHeight());
			if (num > mContentViewport.GetRectHeight())
			{
				mContentViewport.SetSize(mContentViewport.GetRectWidth(), num);
			}
			short num2 = (short)(component.GetRectLeft() + component.GetRectWidth());
			if (num2 > mContentViewport.GetRectWidth())
			{
				mContentViewport.SetSize(num2, mContentViewport.GetRectHeight());
			}
			component.SetViewport(mContentViewport);
		}

		public virtual void MoveBy(short x, short y)
		{
			mOffsetX = (short)((x + mOffsetX) % mContentViewport.GetRectWidth());
			mOffsetY = (short)((y + mOffsetY) % mContentViewport.GetRectHeight());
			Invalidate();
		}

		public virtual void MoveTo(short x, short y)
		{
			mOffsetX = x;
			mOffsetY = y;
			Invalidate();
		}

		public override void SetSize(short width, short height)
		{
			base.SetSize(width, height);
			mContentViewport.SetSize(width, mContentViewport.GetRectHeight());
		}

		public override void SetSize(Vector2_short size)
		{
			base.SetSize(size);
		}

		public virtual int GetHitComponentIdx(short posX, short posY)
		{
			bool flag = false;
			Component component = null;
			int result = -1;
			int childCount = mContentViewport.GetChildCount();
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			short num4 = 0;
			short rectHeight = mContentViewport.GetRectHeight();
			short num5 = (short)(-mOffsetY);
			short num6 = (short)(GetAbsoluteTop() + num5);
			short rectHeight2 = GetRectHeight();
			short num7 = (short)(posY + num5);
			for (int i = 0; i < childCount; i++)
			{
				if (flag)
				{
					break;
				}
				component = mContentViewport.GetChild(i);
				num = component.GetAbsoluteTop();
				num2 = (short)(num - rectHeight);
				do
				{
					num2 = (short)(num2 - rectHeight);
				}
				while (num2 >= rectHeight);
				num3 = (short)(num + rectHeight);
				do
				{
					num3 = (short)(num3 + rectHeight);
				}
				while (num3 < rectHeight);
				num4 = num2;
				while (num4 < num3 && !flag)
				{
					if (num4 >= num6 && num4 < num6 + rectHeight2 && num7 >= num4 && num7 < num4 + component.GetRectHeight())
					{
						result = i;
						flag = true;
					}
					num4 = (short)(num4 + rectHeight);
				}
			}
			return result;
		}

		public static short GetMinOffset(short toTransform, short increment, short minValue)
		{
			while (toTransform > minValue + increment)
			{
				toTransform = (short)(toTransform - increment);
			}
			return toTransform;
		}

		public virtual void DrawContentViewport(DisplayContext displayContext, short offsetX, short offsetY)
		{
			displayContext.OffsetBy((short)(offsetX + GetRectLeft()), (short)(offsetY + GetRectTop()));
			mContentViewport.OnDraw(displayContext);
			displayContext.OffsetBy((short)(-offsetX - GetRectLeft()), (short)(-offsetY - GetRectTop()));
		}
	}
}
