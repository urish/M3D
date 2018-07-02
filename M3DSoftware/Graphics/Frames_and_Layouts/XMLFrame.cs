﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.Frames_and_Layouts.XMLFrame
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics.Widgets2D;
using M3D.GUI.Forms;
using System;
using System.IO;
using System.Xml.Serialization;

namespace M3D.Graphics.Frames_and_Layouts
{
  public class XMLFrame : Frame
  {
    private GUIHost host;
    protected XMLFrame childFrame;

    public XMLFrame()
      : base(0, (Element2D) null)
    {
    }

    public XMLFrame(int ID)
      : base(ID, (Element2D) null)
    {
    }

    public void Init(GUIHost host, string xmlScript, ButtonCallback MyButtonCallback)
    {
      this.host = host;
      var s = xmlScript;
      var xmlSerializer = new XmlSerializer(typeof (XMLFrame));
      Sprite.pixel_perfect = true;
      using (var textReader = (TextReader) new StringReader(s))
      {
        try
        {
          childFrame = (XMLFrame) xmlSerializer.Deserialize(textReader);
          childFrame.Init(host, MyButtonCallback);
        }
        catch (Exception ex)
        {
          ExceptionForm.ShowExceptionForm(ex);
        }
      }
      Sprite.pixel_perfect = false;
      RemoveAllChildElements();
      AddChildElement((Element2D)childFrame);
      childFrame.Refresh();
    }

    public void Init(GUIHost host, ButtonCallback MyButtonCallback)
    {
      this.host = host;
      DoOnUpdate = (ElementStandardDelegate) null;
      Sprite.texture_height_pixels = 1024;
      Sprite.texture_width_pixels = 1024;
      Sprite.pixel_perfect = true;
      Visible = true;
      Enabled = true;
      InitChildren((Element2D) null, host, MyButtonCallback);
      Sprite.pixel_perfect = false;
    }

    public GUIHost Host
    {
      get
      {
        return host;
      }
    }
  }
}
