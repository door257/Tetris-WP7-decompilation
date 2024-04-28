using System;

namespace ca.jamdat.flight
{
	public abstract class Component : TimeControlled
	{
		public new const sbyte typeNumber = 67;

		public new const sbyte typeID = 67;

		public new const bool supportsDynamicSerialization = true;

		public const sbyte focusMsg = sbyte.MinValue;

		public const sbyte selectedMsg = -127;

		public const sbyte enabledMsg = -126;

		public const sbyte pushedMsg = -125;

		public const sbyte synchronizeSelectorMsg = -124;

		public const sbyte buttonMsg = -123;

		public const sbyte attachMsg = -122;

		public const sbyte keyUpMsg = -121;

		public const sbyte keyDownMsg = -120;

		public const sbyte keyDownOrRepeatMsg = -119;

		public const sbyte penUpMsg = -118;

		public const sbyte penDownMsg = -117;

		public const sbyte penDragMsg = -116;

		public const sbyte penDragOutMsg = -115;

		public const sbyte penMgrDeactivatedMsg = -114;

		public const sbyte penDownOnTextFieldMsg = -113;

		public const sbyte returnedFromSceneMsg = -112;

		public const sbyte startTransitionMsg = -111;

		public const sbyte completedTransitionMsg = -110;

		public const sbyte quitTransitionMsg = -109;

		public const sbyte resetAncestorScrollerMsg = -108;

		public const sbyte slideMsg = -107;

		public const sbyte slideStateMsg = -106;

		public const sbyte scrollerUpdatedMsg = -105;

		public const sbyte textFieldInputMsg = -104;

		public const sbyte quitRequestMsg = -103;

		public const sbyte reservedMsg = 0;

		public const sbyte firstApplicationMsg = 1;

		public const sbyte isDetached = 0;

		public const sbyte isAttached = 1;

		public const sbyte isLeaving = 0;

		public const sbyte isEntering = 1;

		public short mRect_left;

		public short mRect_top;

		public short mRect_width;

		public short mRect_height;

		public Viewport m_pViewport;

		public bool mVisible;

		public bool mPassThrough;

		public Component m_pPreviousSibling;

		public Component()
		{
			mVisible = true;
			mPassThrough = true;
		}

		public Component(Viewport viewport)
		{
			mVisible = true;
			mPassThrough = true;
			SetViewport(viewport);
		}

		public static Component Cast(object o, Component _)
		{
			return (Component)o;
		}

		public override sbyte GetTypeID()
		{
			return 67;
		}

		public new static Type AsClass()
		{
			return null;
		}

		public override void destruct()
		{
			if (IsTrackingPen())
			{
				ReleasePen();
			}
			if (m_pViewport != null)
			{
				SetViewport(null);
			}
		}

		public virtual void SetViewport(Viewport newViewport)
		{
			Viewport pViewport = m_pViewport;
			if (pViewport != null)
			{
				UpdateAttach(false);
				pViewport.RemoveChild(this);
			}
			m_pViewport = newViewport;
			if (newViewport != null)
			{
				newViewport.AddChild(this);
				UpdateAttach(true);
			}
		}

		public virtual Viewport GetViewport()
		{
			return m_pViewport;
		}

		public virtual void TakeFocus()
		{
			FlApplication.GetInstance().SetCurrentFocus(this);
		}

		public virtual void OnFocusChange(bool entering)
		{
			SendMsg(this, -128, entering ? 1 : 0);
		}

		public virtual Component ForwardFocus()
		{
			return this;
		}

		public virtual bool DescendentOrSelfHasFocus()
		{
			Component currentFocus = FlApplication.GetInstance().GetCurrentFocus();
			return IsSelfOrAncestorOf(currentFocus);
		}

		public virtual bool IsSelfOrAncestorOf(Component component)
		{
			while (component != null)
			{
				if (component == this)
				{
					return true;
				}
				component = component.GetViewport();
			}
			return false;
		}

		public virtual int GetDepth()
		{
			Component component = this;
			int num = 0;
			do
			{
				num++;
				component = component.GetViewport();
			}
			while (component != null);
			return num;
		}

		public virtual void SendMsg(Component source, int msg, int intParam)
		{
			Component component = this;
			while (!component.OnMsg(source, msg, intParam) && !component.OnDefaultMsg(source, msg, intParam))
			{
				component = component.GetViewport();
				if (component == null)
				{
					break;
				}
			}
		}

		public virtual bool OnMsg(Component source, int msg, int intParam)
		{
			return false;
		}

		public virtual bool OnDefaultMsg(Component source, int msg, int intParam)
		{
			return false;
		}

		public virtual void SetRect(short rect_left, short rect_top, short rect_width, short rect_height)
		{
			Invalidate();
			mRect_left = rect_left;
			mRect_top = rect_top;
			mRect_width = rect_width;
			mRect_height = rect_height;
			OnRectChange();
			Invalidate();
		}

		public virtual short GetRectLeft()
		{
			return mRect_left;
		}

		public virtual short GetRectTop()
		{
			return mRect_top;
		}

		public virtual short GetRectWidth()
		{
			return mRect_width;
		}

		public virtual short GetRectHeight()
		{
			return mRect_height;
		}

		public virtual void SetRect(FlRect r)
		{
			SetRect(r.GetLeft(), r.GetTop(), r.GetWidth(), r.GetHeight());
		}

		public virtual FlRect GetRect()
		{
			return new FlRect(mRect_left, mRect_top, mRect_width, mRect_height);
		}

		public virtual void SetTopLeft(Vector2_short topLeft)
		{
			SetTopLeft(topLeft.GetX(), topLeft.GetY());
		}

		public virtual Vector2_short GetSize()
		{
			return new Vector2_short(GetRectWidth(), GetRectHeight());
		}

		public virtual void SetSize(Vector2_short size)
		{
			SetSize(size.GetX(), size.GetY());
		}

		public virtual Vector2_short GetTopLeft()
		{
			return new Vector2_short(GetRectLeft(), GetRectTop());
		}

		public virtual Vector2_short GetAbsoluteTopLeft()
		{
			return new Vector2_short(GetAbsoluteLeft(), GetAbsoluteTop());
		}

		public virtual void SetTopLeft(short left, short top)
		{
			Invalidate();
			mRect_left = left;
			mRect_top = top;
			OnRectChange();
			Invalidate();
		}

		public virtual short GetAbsoluteTop()
		{
			Viewport pViewport = m_pViewport;
			if (pViewport != null)
			{
				return (short)(mRect_top + pViewport.GetAbsoluteTop() - pViewport.GetOffsetY());
			}
			return mRect_top;
		}

		public virtual short GetAbsoluteLeft()
		{
			Viewport pViewport = m_pViewport;
			if (pViewport != null)
			{
				return (short)(mRect_left + pViewport.GetAbsoluteLeft() - pViewport.GetOffsetX());
			}
			return mRect_left;
		}

		public virtual short GetRelativeCoordX(short absoluteCoordX)
		{
			int num = absoluteCoordX;
			for (Viewport viewport = GetViewport(); viewport != null; viewport = viewport.GetViewport())
			{
				num = num - viewport.GetRectLeft() + viewport.GetOffsetX();
			}
			return (short)(num - mRect_left);
		}

		public virtual short GetRelativeCoordY(short absoluteCoordY)
		{
			int num = absoluteCoordY;
			for (Viewport viewport = GetViewport(); viewport != null; viewport = viewport.GetViewport())
			{
				num = num - viewport.GetRectTop() + viewport.GetOffsetY();
			}
			return (short)(num - mRect_top);
		}

		public virtual void SetCenter(short x, short y)
		{
			Invalidate();
			mRect_left = (short)(x - mRect_width / 2);
			mRect_top = (short)(y - mRect_height / 2);
			OnRectChange();
			Invalidate();
		}

		public virtual void SetBottomRight(short right, short bottom)
		{
			Invalidate();
			mRect_width = (short)(right + 1 - mRect_left);
			mRect_height = (short)(bottom + 1 - mRect_top);
			OnRectChange();
			Invalidate();
		}

		public virtual short GetBottom()
		{
			return (short)(mRect_top + mRect_height - 1);
		}

		public virtual short GetRight()
		{
			return (short)(mRect_left + mRect_width - 1);
		}

		public virtual void CenterInRect(short rect_left, short rect_top, short rect_width, short rect_height)
		{
			mRect_left = (short)(rect_left + (rect_width - mRect_width >> 1));
			mRect_top = (short)(rect_top + (rect_height - mRect_height >> 1));
		}

		public virtual void SetSize(short width, short height)
		{
			Invalidate();
			mRect_width = width;
			mRect_height = height;
			OnRectChange();
			Invalidate();
		}

		public virtual void SetVisible(bool visible)
		{
			if (mVisible != visible)
			{
				Invalidate();
				mVisible = visible;
				OnVisibilityChange();
				Invalidate();
			}
		}

		public virtual bool IsVisible()
		{
			return mVisible;
		}

		public virtual bool IsGloballyVisible()
		{
			Viewport pViewport = m_pViewport;
			if (IsVisible())
			{
				if (pViewport != null)
				{
					return pViewport.IsGloballyVisible();
				}
				return true;
			}
			return false;
		}

		public virtual Component GetPreviousSiblingComponent()
		{
			return m_pPreviousSibling;
		}

		public virtual void BringToFront()
		{
			m_pViewport.BringComponentToFront(this);
			Invalidate();
		}

		public virtual void SendToBack()
		{
			m_pViewport.SendComponentToBack(this);
			Invalidate();
		}

		public virtual void PutBehind(Component inCompoment)
		{
			m_pViewport.PutComponentBehind(this, inCompoment);
			Invalidate();
		}

		public virtual void PutInFront(Component inCompoment)
		{
			m_pViewport.PutComponentInFront(this, inCompoment);
			Invalidate();
		}

		public virtual bool IsAttached()
		{
			Component component = this;
			Component component2 = null;
			do
			{
				component2 = component;
				component = component2.GetViewport();
			}
			while (component != null);
			return component2 == FlApplication.GetInstance();
		}

		public virtual void OnAttach(bool attach)
		{
			if (!attach)
			{
				FlApplication instance = FlApplication.GetInstance();
				if (instance.GetCurrentFocus() == this)
				{
					instance.TakeFocus();
				}
			}
		}

		public virtual void Invalidate()
		{
			if (IsVisible())
			{
				DisplayManager.GetMainDisplayContext().AddDirtyComponent(this);
			}
		}

		public abstract void OnDraw(DisplayContext a17);

		public virtual bool IsPassThrough()
		{
			if (!mPassThrough)
			{
				return false;
			}
			return true;
		}

		public virtual void SetPassThrough(bool inIsPassThrough)
		{
			mPassThrough = inIsPassThrough;
		}

		public virtual Component GetHitTestComponent(short ptX, short ptY)
		{
			if (!IsPassThrough() && IsVisible() && HitTest(ptX, ptY))
			{
				return this;
			}
			return null;
		}

		public virtual void OnMouseRightDown(Vector2_short a15)
		{
		}

		public virtual void OnMouseOver()
		{
		}

		public virtual void OnMouseOut()
		{
		}

		public virtual void OnMouseRightUp(Vector2_short a16)
		{
		}

		public virtual void OnMouseRightDrag(Vector2_short a17)
		{
		}

		public virtual bool IsTrackingPen()
		{
			return FrameworkGlobals.GetInstance().penTracker == this;
		}

		public virtual bool IsTrackingMouse()
		{
			return false;
		}

		public override void OnSerialize(Package p)
		{
			mRect_left = p.SerializeIntrinsic(mRect_left);
			mRect_top = p.SerializeIntrinsic(mRect_top);
			mRect_width = p.SerializeIntrinsic(mRect_width);
			mRect_height = p.SerializeIntrinsic(mRect_height);
			sbyte t = 0;
			t = p.SerializeIntrinsic(t);
			mVisible = (t & 1) != 0;
			mPassThrough = (t & 2) != 0;
		}

		public override void ControlValue(int valueControlCode, bool setValue, Controller controller)
		{
			if (setValue)
			{
				short num = 0;
				short num2 = 0;
				if (valueControlCode == 1 || valueControlCode == 2 || valueControlCode == 3)
				{
					num = controller.GetCoord2ValueX();
					num2 = controller.GetCoord2ValueY();
				}
				switch (valueControlCode)
				{
				case 1:
					SetTopLeft(num, num2);
					return;
				case 2:
					SetSize(num, num2);
					return;
				case 3:
				{
					short rect_left = (short)(GetRectLeft() + (GetRectWidth() >> 1) - (num >> 1));
					short rect_top = (short)(GetRectTop() + (GetRectHeight() >> 1) - (num2 >> 1));
					SetRect(rect_left, rect_top, num, num2);
					return;
				}
				case 4:
				{
					FlRect rectValue = controller.GetRectValue();
					SetRect(rectValue.GetLeft(), rectValue.GetTop(), rectValue.GetWidth(), rectValue.GetHeight());
					return;
				}
				case 5:
					SetVisible(controller.GetBoolValue());
					return;
				}
			}
			else
			{
				switch (valueControlCode)
				{
				case 1:
					controller.SetValue(mRect_left, mRect_top);
					return;
				case 2:
				case 3:
					controller.SetValue(mRect_width, mRect_height);
					return;
				case 4:
					controller.SetValue(mRect_left, mRect_top, mRect_width, mRect_height);
					return;
				case 5:
					controller.SetValue(IsVisible());
					return;
				}
			}
			base.ControlValue(valueControlCode, setValue, controller);
		}

		public virtual void ResetAncestorScroller()
		{
			SendMsg(this, -108, 0);
		}

		public virtual void TrackPen()
		{
			FrameworkGlobals.GetInstance().penTracker = this;
		}

		public virtual void ReleasePen()
		{
			FrameworkGlobals.GetInstance().penTracker = FrameworkGlobals.GetInstance().application;
		}

		public virtual void TrackMouse()
		{
		}

		public virtual void ReleaseMouse()
		{
		}

		public virtual bool HitTest(short ptX, short ptY)
		{
			return FlRect.Contains(mRect_left, mRect_top, mRect_width, mRect_height, ptX, ptY);
		}

		public virtual void OnVisibilityChange()
		{
		}

		public virtual void OnRectChange()
		{
		}

		public virtual void UpdateAttach(bool attach)
		{
			if (IsAttached())
			{
				Invalidate();
				OnAttach(attach);
			}
		}
	}
}
