﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.TextLine
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

namespace M3D.Graphics
{
  public class TextLine
  {
    public string buffer;

    public int AddCharToEnd(char ch)
    {
      buffer += ch.ToString();
      return 1;
    }

    public int AddCharAt(char ch, int at)
    {
      if (at >= buffer.Length)
      {
        at = buffer.Length;
      }

      if (at < 0)
      {
        at = 0;
      }

      buffer = buffer.Insert(at, ch.ToString());
      return 1;
    }

    public int AddStringAt(string ch, int at)
    {
      if (at >= buffer.Length)
      {
        at = buffer.Length;
      }

      if (at < 0)
      {
        at = 0;
      }

      buffer = buffer.Insert(at, ch);
      return 1;
    }

    public void DeleteAt(int at)
    {
      if (at > 0 && at < buffer.Length - 1)
      {
        buffer = buffer.Substring(0, at) + buffer.Substring(at + 1);
      }
      else if (at == buffer.Length - 1)
      {
        buffer = buffer.Substring(0, at);
      }
      else
      {
        if (at != 0 || buffer.Length <= 0)
        {
          return;
        }

        buffer = buffer.Substring(1);
      }
    }

    public void DeleteRegion(int start, int end)
    {
      buffer = buffer.Remove(start, end - start);
    }

    public void SetText(string szString)
    {
      buffer = szString;
    }

    public string GetText()
    {
      return buffer;
    }

    public int GetSize()
    {
      return buffer.Length;
    }

    public void Clear()
    {
      buffer = "";
    }
  }
}
