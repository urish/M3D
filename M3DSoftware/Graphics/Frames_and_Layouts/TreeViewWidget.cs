﻿using M3D.Graphics.Widgets2D;
using System.Collections.Generic;

namespace M3D.Graphics.Frames_and_Layouts
{
  public class TreeViewWidget : ScrollableVerticalLayout
  {
    public TreeViewWidget()
      : this(0, null)
    {
    }

    public TreeViewWidget(int ID)
      : this(ID, null)
    {
    }

    public TreeViewWidget(int ID, Element2D parent)
      : base(ID, parent)
    {
    }

    public override void InitChildren(Element2D parent, GUIHost host, ButtonCallback MyButtonCallback)
    {
      base.InitChildren(parent, host, MyButtonCallback);
      foreach (Element2D child in ScollableChildframe.ChildList)
      {
        if (child is TreeNodeWidget)
        {
          ((TreeNodeWidget) child).TopLevel = true;
        }
      }
    }
  }
}
