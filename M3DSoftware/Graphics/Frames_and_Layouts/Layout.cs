﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.Frames_and_Layouts.Layout
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics.Widgets2D;
using System.Xml.Serialization;

namespace M3D.Graphics.Frames_and_Layouts
{
  public abstract class Layout : Frame
  {
    protected int border_width;
    protected int border_height;
    [XmlAttribute("layout-mode")]
    public Layout.LayoutMode layoutMode;

    public Layout(int ID, Element2D parent)
      : base(ID, parent)
    {
      border_width = 10;
      border_height = 10;
    }

    public override void Refresh()
    {
      Recalc();
    }

    public override void OnParentResize()
    {
      base.OnParentResize();
      if (layoutMode == Layout.LayoutMode.ResizeLayoutToFitChildren)
      {
        return;
      }

      Recalc();
    }

    public override void SetSize(int width, int height)
    {
      base.SetSize(width, height);
      if (layoutMode == Layout.LayoutMode.ResizeLayoutToFitChildren)
      {
        return;
      }

      Recalc();
    }

    [XmlAttribute("border-width")]
    public int BorderWidth
    {
      get
      {
        return border_width;
      }
      set
      {
        border_width = value;
        if (border_width >= 0)
        {
          return;
        }

        border_width = 0;
      }
    }

    [XmlAttribute("border-height")]
    public int BorderHeight
    {
      get
      {
        return border_height;
      }
      set
      {
        border_height = value;
        if (border_height >= 0)
        {
          return;
        }

        border_height = 0;
      }
    }

    public void Recalc()
    {
      if (layoutMode == Layout.LayoutMode.ResizeChildrenToFitLayout)
      {
        RecalcChildSizes();
      }
      else
      {
        if (layoutMode != Layout.LayoutMode.ResizeLayoutToFitChildren)
        {
          return;
        }

        ResizeToFitChildren();
      }
    }

    protected abstract void ResizeToFitChildren();

    protected abstract void RecalcChildSizes();

    public enum LayoutMode
    {
      ResizeChildrenToFitLayout,
      ResizeLayoutToFitChildren,
    }
  }
}
