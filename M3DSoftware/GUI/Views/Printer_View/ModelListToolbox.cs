﻿// Decompiled with JetBrains decompiler
// Type: M3D.GUI.Views.Printer_View.ModelListToolbox
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics;
using M3D.Graphics.Ext3D;
using M3D.Graphics.Frames_and_Layouts;
using M3D.Graphics.Widgets2D;
using M3D.Properties;
using System.Collections.Generic;

namespace M3D.GUI.Views.Printer_View
{
  internal class ModelListToolbox : XMLFrame
  {
    private PrinterView printerview;
    private ListBoxWidget listbox;

    public ModelListToolbox(int ID, PrinterView printerview)
      : base(ID)
    {
      this.printerview = printerview;
    }

    public void Init(GUIHost host)
    {
      var modelListToolbox = Resources.ModelListToolbox;
      Init(host, modelListToolbox, new ButtonCallback(MyButtonCallback));
      SetSize(300, 170);
      listbox = FindChildElement(5031) as ListBoxWidget;
      if (listbox != null)
      {
        listbox.SetOnChangeCallback(new ListBoxWidget.OnChangeCallback(OnListboxSelectionChanged));
      }

      printerview.OnModelListChanged += new PrinterView.ModelListChanged(OnModelListChanged);
    }

    public void OnListboxSelectionChanged(ListBoxWidget listBox)
    {
      var selected = listbox.Selected;
      if (selected < 0 || selected >= listbox.Items.Count)
      {
        return;
      }

      printerview.SelectModelbyID(((ModelTransformPair.Data)listbox.Items[selected]).ID);
    }

    public void OnModelListChanged(List<ModelTransformPair.Data> modelData)
    {
      listbox.Items.Clear();
      foreach (ModelTransformPair.Data data in modelData)
      {
        listbox.Items.Add((object) data);
      }
    }

    private void MyButtonCallback(ButtonWidget button)
    {
    }

    private enum ControlIDs
    {
      ListBox = 5031, // 0x000013A7
    }
  }
}
