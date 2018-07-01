﻿// Decompiled with JetBrains decompiler
// Type: M3D.GUI.Views.Library_View.LibraryViewTab
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics;
using M3D.Graphics.Frames_and_Layouts;

namespace M3D.GUI.Views.Library_View
{
  internal interface LibraryViewTab
  {
    void Show(GUIHost host, GridLayout LibraryGrid, string filter);

    void LoadRecord(LibraryRecord record);

    void RemoveRecord(LibraryRecord record);

    void SaveRecord(LibraryRecord record);

    bool CanSaveRecords { get; }

    bool CanRemoveRecords { get; }
  }
}