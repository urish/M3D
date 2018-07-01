﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.Widgets2D.Element2DList
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace M3D.Graphics.Widgets2D
{
  public class Element2DList : IList<Element2D>, ICollection<Element2D>, IEnumerable<Element2D>, IEnumerable, IList, ICollection, IReadOnlyList<Element2D>, IReadOnlyCollection<Element2D>
  {
    private List<Element2D> m_olStoragelist;
    private Element2D m_parent;
    private Element2DList.ReversedElement2DList m_olRevereseEnumerable;

    public Element2DList(Element2D parent)
    {
      this.m_parent = parent;
      this.m_olStoragelist = new List<Element2D>();
      this.m_olRevereseEnumerable = new Element2DList.ReversedElement2DList(this);
    }

    public Element2D Last()
    {
      lock (this.m_olStoragelist)
      {
        if (this.m_olStoragelist.Count > 0)
          return this.m_olStoragelist.Last<Element2D>();
        return (Element2D) null;
      }
    }

    public Element2DList.ReversedElement2DList Reverse()
    {
      return this.m_olRevereseEnumerable;
    }

    public Element2D[] ToArray()
    {
      return this.ThreadSafeCopy().ToArray();
    }

    public void Add(Element2D child)
    {
      child.SetParent(this.m_parent);
      lock (this.m_olStoragelist)
        this.m_olStoragelist.Add(child);
    }

    public bool Remove(Element2D child)
    {
      if (child == null)
        return true;
      child.SetParent((Element2D) null);
      lock (this.m_olStoragelist)
        return this.m_olStoragelist.Remove(child);
    }

    public void RemoveAt(int index)
    {
      lock (this.m_olStoragelist)
      {
        this.m_olStoragelist[index].SetParent((Element2D) null);
        this.m_olStoragelist.RemoveAt(index);
      }
    }

    public int IndexOf(Element2D child)
    {
      lock (this.m_olStoragelist)
        return this.m_olStoragelist.IndexOf(child);
    }

    public void Insert(int index, Element2D child)
    {
      child.SetParent(this.m_parent);
      lock (this.m_olStoragelist)
        this.m_olStoragelist.Insert(index, child);
    }

    public Element2D this[int key]
    {
      get
      {
        return this.m_olStoragelist[key];
      }
      set
      {
        this.m_olStoragelist[key] = value;
      }
    }

    public void CopyTo(Element2D[] array, int arrayIndex)
    {
      lock (this.m_olStoragelist)
        this.m_olStoragelist.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        lock (this.m_olStoragelist)
          return this.m_olStoragelist.Count;
      }
    }

    public bool Contains(Element2D child)
    {
      lock (this.m_olStoragelist)
        return this.m_olStoragelist.Contains(child);
    }

    public void Clear()
    {
      lock (this.m_olStoragelist)
        this.m_olStoragelist.Clear();
    }

    IEnumerator<Element2D> IEnumerable<Element2D>.GetEnumerator()
    {
      for (int i = 0; i < this.Count; ++i)
        yield return this[i];
    }

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      lock (this.m_olStoragelist)
        ((ICollection) this.m_olStoragelist).CopyTo(array, arrayIndex);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) ((IEnumerable<Element2D>) this).GetEnumerator();
    }

    int IList.Add(object o)
    {
      this.AssertType(o);
      this.Add((Element2D) o);
      lock (this.m_olStoragelist)
        return this.m_olStoragelist.Count - 1;
    }

    bool IList.Contains(object value)
    {
      lock (this.m_olStoragelist)
        return ((IList) this.m_olStoragelist).Contains(value);
    }

    int IList.IndexOf(object item)
    {
      lock (this.m_olStoragelist)
        return ((IList) this.m_olStoragelist).IndexOf(item);
    }

    void IList.Insert(int index, object item)
    {
      this.AssertType(item);
      this.Insert(index, (Element2D) item);
    }

    void IList.Remove(object item)
    {
      this.AssertType(item);
      this.Remove((Element2D) item);
    }

    bool ICollection<Element2D>.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return true;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        this.AssertType(value);
        this[index] = (Element2D) value;
      }
    }

    public static Element2DList operator +(Element2DList theList, Element2D element)
    {
      theList.m_parent.AddChildElement(element);
      return theList;
    }

    public static Element2DList operator -(Element2DList theList, Element2D element)
    {
      theList.m_parent.RemoveChildElement(element);
      return theList;
    }

    private void AssertType(object o)
    {
      if (o == null || !(o is Element2D))
        throw new ArgumentException("All arguments must be of type Element2D");
    }

    private List<Element2D> ThreadSafeCopy()
    {
      lock (this.m_olStoragelist)
        return new List<Element2D>((IEnumerable<Element2D>) this.m_olStoragelist);
    }

    public class ReversedElement2DList : IEnumerable<Element2D>, IEnumerable
    {
      private Element2DList m_olsourceList;

      public ReversedElement2DList(Element2DList sourceList)
      {
        this.m_olsourceList = sourceList;
      }

      IEnumerator<Element2D> IEnumerable<Element2D>.GetEnumerator()
      {
        for (int i = this.m_olsourceList.Count - 1; i >= 0; --i)
          yield return this.m_olsourceList[i];
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<Element2D>) this).GetEnumerator();
      }
    }
  }
}