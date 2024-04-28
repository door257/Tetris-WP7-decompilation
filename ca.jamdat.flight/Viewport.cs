using System;

namespace ca.jamdat.flight
{
	public class Viewport : Component
	{
		public new const sbyte typeNumber = 68;

		public new const sbyte typeID = 68;

		public new const bool supportsDynamicSerialization = true;

		public bool mClipChildren;

		public short mOffsetX;

		public short mOffsetY;

		public sbyte mSubtype;

		public Component m_pFirstChild;

		public Component m_pLastChild;

		public Viewport()
		{
			mSubtype = -1;
		}

		public Viewport(Viewport viewport)
		{
			mSubtype = -1;
			SetViewport(viewport);
		}

		public static Viewport Cast(object o, Viewport _)
		{
			return (Viewport)o;
		}

		public override sbyte GetTypeID()
		{
			return 68;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			while (m_pFirstChild != null)
			{
				Component pFirstChild = m_pFirstChild;
				pFirstChild.SetViewport(null);
				pFirstChild = null;
			}
		}

		public virtual void Term()
		{
		}

		public override void OnDraw(DisplayContext displayContext)
		{
			short num = (short)(mRect_left - mOffsetX);
			short num2 = (short)(mRect_top - mOffsetY);
			displayContext.OffsetBy(num, num2);
			if (mClipChildren)
			{
				short clippingRectLeft = displayContext.GetClippingRectLeft();
				short clippingRectTop = displayContext.GetClippingRectTop();
				short clippingRectWidth = displayContext.GetClippingRectWidth();
				short clippingRectHeight = displayContext.GetClippingRectHeight();
				short num3 = mOffsetX;
				short num4 = mOffsetY;
				short num5 = mRect_width;
				short num6 = mRect_height;
				int num7 = clippingRectLeft + clippingRectWidth;
				int num8 = num3 + num5;
				int num9 = clippingRectTop + clippingRectHeight;
				int num10 = num4 + num6;
				if (clippingRectLeft > num3)
				{
					num3 = clippingRectLeft;
				}
				if (clippingRectTop > num4)
				{
					num4 = clippingRectTop;
				}
				if (num7 < num8)
				{
					num8 = num7;
				}
				if (num9 < num10)
				{
					num10 = num9;
				}
				num5 = (short)(num8 - num3);
				num6 = (short)(num10 - num4);
				if (num5 > 0 && num6 > 0)
				{
					displayContext.SetClippingRect(num3, num4, num5, num6);
					DrawChildList(displayContext);
					displayContext.SetClippingRect(clippingRectLeft, clippingRectTop, clippingRectWidth, clippingRectHeight);
				}
			}
			else
			{
				DrawChildList(displayContext);
			}
			displayContext.OffsetBy((short)(-num), (short)(-num2));
		}

		public virtual void OffsetTo(Vector2_short offset)
		{
			OffsetTo(offset.GetX(), offset.GetY());
		}

		public virtual void OffsetBy(Vector2_short offset)
		{
			OffsetBy(offset.GetX(), offset.GetY());
		}

		public virtual Vector2_short GetOffset()
		{
			return new Vector2_short(mOffsetX, mOffsetY);
		}

		public virtual Vector2_short GetDelta()
		{
			return new Vector2_short(GetDeltaX(), GetDeltaY());
		}

		public virtual void OffsetTo(short inPointX, short inPointY)
		{
			mOffsetX = inPointX;
			mOffsetY = inPointY;
			OnOffsetChange();
			base.Invalidate();
		}

		public virtual void OffsetBy(short inDeltaX, short inDeltaY)
		{
			mOffsetX += inDeltaX;
			mOffsetY += inDeltaY;
			OnOffsetChange();
			base.Invalidate();
		}

		public virtual void OnOffsetChange()
		{
		}

		public virtual short GetOffsetX()
		{
			return mOffsetX;
		}

		public virtual short GetOffsetY()
		{
			return mOffsetY;
		}

		public virtual short GetDeltaX()
		{
			short num = 0;
			if (m_pViewport != null)
			{
				num = m_pViewport.GetDeltaX();
			}
			return (short)(num + GetRectLeft() - mOffsetX);
		}

		public virtual short GetDeltaY()
		{
			short num = 0;
			if (m_pViewport != null)
			{
				num = m_pViewport.GetDeltaY();
			}
			return (short)(num + GetRectTop() - mOffsetY);
		}

		public virtual void GetRequiredOffsetChange(Vector2_short outOffset, Component descendent, bool bottomright)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			Viewport viewport = descendent.GetViewport();
			while (viewport != null && viewport != this)
			{
				num3 += viewport.GetRectLeft() - viewport.mOffsetX;
				num4 += viewport.GetRectTop() - viewport.mOffsetY;
				viewport = viewport.GetViewport();
			}
			if (viewport == null)
			{
				outOffset.SetX(0);
				outOffset.SetY(0);
				return;
			}
			int num5 = num3 + descendent.GetRectLeft() - mOffsetX;
			int num6 = num4 + descendent.GetRectTop() - mOffsetY;
			int num7 = num5 + descendent.GetRectWidth() - mRect_width;
			int num8 = num6 + descendent.GetRectHeight() - mRect_height;
			bool flag = descendent.GetRectWidth() > mRect_width;
			bool flag2 = descendent.GetRectHeight() > mRect_height;
			if (flag)
			{
				num = (bottomright ? num7 : num5);
			}
			else
			{
				if (num5 < 0)
				{
					num = num5;
				}
				if (num7 > 0)
				{
					num = num7;
				}
			}
			if (flag2)
			{
				num2 = (bottomright ? num8 : num6);
			}
			else
			{
				if (num6 < 0)
				{
					num2 = num6;
				}
				if (num8 > 0)
				{
					num2 = num8;
				}
			}
			outOffset.SetX((short)num);
			outOffset.SetY((short)num2);
		}

		public virtual void ChangeOffsetToShow(Component descendent, bool bottomright)
		{
			Vector2_short vector2_short = new Vector2_short();
			GetRequiredOffsetChange(vector2_short, descendent, bottomright);
			OffsetBy(vector2_short.GetX(), vector2_short.GetY());
		}

		public virtual void SetClipChildren(bool clip)
		{
			mClipChildren = clip;
		}

		public virtual bool GetClipChildren()
		{
			return mClipChildren;
		}

		public virtual void SetSubtype(int subtype)
		{
			mSubtype = (sbyte)subtype;
		}

		public virtual int GetSubtype()
		{
			return mSubtype;
		}

		public override void OnAttach(bool attach)
		{
			for (Component component = m_pLastChild; component != null; component = component.m_pPreviousSibling)
			{
				component.OnAttach(attach);
			}
			base.OnAttach(attach);
			SendMsg(this, -122, attach ? 1 : 0);
		}

		public virtual void OnChildEvent(Component child)
		{
			if (GetViewport() != null)
			{
				GetViewport().OnChildEvent(child);
			}
		}

		public override void ControlValue(int valueControlCode, bool setValue, Controller controller)
		{
			if (valueControlCode == 6)
			{
				for (Component component = m_pLastChild; component != null; component = component.m_pPreviousSibling)
				{
					component.ControlValue(valueControlCode, setValue, controller);
				}
			}
			else if (setValue)
			{
				if (valueControlCode == 14)
				{
					FlRect rectValue = controller.GetRectValue();
					short left = rectValue.GetLeft();
					short top = rectValue.GetTop();
					short width = rectValue.GetWidth();
					short height = rectValue.GetHeight();
					short num = mRect_left;
					short num2 = mRect_top;
					SetRect(left, top, width, height);
					OffsetBy((short)(left - num), (short)(top - num2));
					return;
				}
			}
			else if (valueControlCode == 14)
			{
				controller.SetValue(mRect_left, mRect_top, mRect_width, mRect_height);
				return;
			}
			base.ControlValue(valueControlCode, setValue, controller);
		}

		public override Component GetHitTestComponent(short ptX, short ptY)
		{
			if (IsVisible() && HitTest(ptX, ptY))
			{
				short ptX2 = (short)(ptX - GetRectLeft() + GetOffsetX());
				short ptY2 = (short)(ptY - GetRectTop() + GetOffsetY());
				for (Component component = m_pFirstChild; component != null; component = FindNextSibling(component))
				{
					Component hitTestComponent = component.GetHitTestComponent(ptX2, ptY2);
					if (hitTestComponent != null)
					{
						return hitTestComponent;
					}
				}
				return base.GetHitTestComponent(ptX, ptY);
			}
			return null;
		}

		public override void OnSerialize(Package p)
		{
			base.OnSerialize(p);
			mOffsetX = p.SerializeIntrinsic(mOffsetX);
			mOffsetY = p.SerializeIntrinsic(mOffsetY);
			mClipChildren = p.SerializeIntrinsic(mClipChildren);
			mSubtype = p.SerializeIntrinsic(mSubtype);
			short t = 0;
			t = p.SerializeIntrinsic(t);
			for (int i = 0; i < t; i++)
			{
				Component component = null;
				component = Component.Cast(p.SerializePointer(67, true, false), null);
				component.SetViewport(this);
			}
		}

		public virtual void DrawChildList(DisplayContext displayContext)
		{
			for (Component component = m_pLastChild; component != null; component = component.m_pPreviousSibling)
			{
				if (component.IsVisible())
				{
					component.OnDraw(displayContext);
				}
			}
		}

		public virtual void SendComponentToBack(Component component)
		{
			if (m_pFirstChild != m_pLastChild)
			{
				RemoveChild(component);
				component.m_pPreviousSibling = m_pLastChild;
				m_pLastChild = component;
			}
		}

		public virtual void BringComponentToFront(Component component)
		{
			Component pFirstChild = m_pFirstChild;
			if (pFirstChild != m_pLastChild)
			{
				RemoveChild(component);
				AddChild(component);
			}
		}

		public virtual void PutComponentBehind(Component from, Component to)
		{
			if (m_pFirstChild != m_pLastChild && from != to)
			{
				RemoveChild(from);
				Component pPreviousSibling = to.m_pPreviousSibling;
				if (pPreviousSibling == null)
				{
					m_pFirstChild = from;
				}
				from.m_pPreviousSibling = pPreviousSibling;
				to.m_pPreviousSibling = from;
			}
		}

		public virtual void PutComponentInFront(Component from, Component to)
		{
			if (m_pFirstChild != m_pLastChild && from != to)
			{
				RemoveChild(from);
				Component component = FindNextSibling(to);
				from.m_pPreviousSibling = to;
				if (component != null)
				{
					component.m_pPreviousSibling = from;
				}
				else
				{
					m_pLastChild = from;
				}
			}
		}

		public virtual short GetChildCount()
		{
			short num = 0;
			for (Component component = m_pLastChild; component != null; component = component.m_pPreviousSibling)
			{
				num = (short)(num + 1);
			}
			return num;
		}

		public virtual Component GetChild(int index)
		{
			Component component = m_pLastChild;
			for (int i = 0; i < index; i++)
			{
				component = component.m_pPreviousSibling;
			}
			return component;
		}

		public virtual void AddChild(Component child)
		{
			Component pFirstChild = m_pFirstChild;
			child.m_pPreviousSibling = null;
			if (pFirstChild != null)
			{
				pFirstChild.m_pPreviousSibling = child;
			}
			else
			{
				m_pLastChild = child;
			}
			m_pFirstChild = child;
			child.Invalidate();
		}

		public virtual void RemoveChild(Component childToRemove)
		{
			Component pPreviousSibling = childToRemove.m_pPreviousSibling;
			Component component = FindNextSibling(childToRemove);
			if (pPreviousSibling == null)
			{
				m_pFirstChild = component;
			}
			if (component != null)
			{
				component.m_pPreviousSibling = pPreviousSibling;
			}
			else
			{
				m_pLastChild = pPreviousSibling;
			}
			childToRemove.m_pPreviousSibling = null;
			childToRemove.Invalidate();
		}

		public virtual Component FindNextSibling(Component child)
		{
			Component result = null;
			Component component = m_pLastChild;
			while (component != null && child != component)
			{
				result = component;
				component = component.m_pPreviousSibling;
			}
			return result;
		}

		public static Viewport[] InstArrayViewport(int size)
		{
			Viewport[] array = new Viewport[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Viewport();
			}
			return array;
		}

		public static Viewport[][] InstArrayViewport(int size1, int size2)
		{
			Viewport[][] array = new Viewport[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Viewport[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Viewport();
				}
			}
			return array;
		}

		public static Viewport[][][] InstArrayViewport(int size1, int size2, int size3)
		{
			Viewport[][][] array = new Viewport[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Viewport[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Viewport[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Viewport();
					}
				}
			}
			return array;
		}
	}
}
