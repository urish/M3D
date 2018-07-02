﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Common.SharedShutdownThread
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using System;
using System.Globalization;
using System.Threading;

namespace M3D.Spooling.Common
{
  public class SharedShutdownThread
  {
    private TimeSpan m_oMinimumDelay = new TimeSpan(5000L);
    public EventHandler<ThreadSafeVariable<bool>> OnThreadAborted;
    private ThreadSafeVariable<int> m_oSleepDelay;
    private Thread m_oInternalThread;
    private SharedShutdownThreadStart ThreadLoopBody;
    private ThreadSafeVariable<bool> m_oSharedShutdownFlag;
    private CultureInfo m_oThreadCulture;

    public SharedShutdownThread(SharedShutdownThreadStart oThreadLoopBody, ThreadSafeVariable<bool> oShutdown)
      : this(oThreadLoopBody, oShutdown, (CultureInfo) null)
    {
    }

    public SharedShutdownThread(SharedShutdownThreadStart oThreadLoopBody, ThreadSafeVariable<bool> oShutdown, CultureInfo oThreadCulture)
    {
      m_oSleepDelay = new ThreadSafeVariable<int>(-1);
      ThreadLoopBody = oThreadLoopBody;
      m_oSharedShutdownFlag = oShutdown;
      m_oThreadCulture = oThreadCulture;
      m_oInternalThread = new Thread(new ThreadStart(vInternalThreadLoop));
    }

    public string Name
    {
      get
      {
        return m_oInternalThread.Name;
      }
      set
      {
        m_oInternalThread.Name = value;
      }
    }

    public ThreadPriority Priority
    {
      get
      {
        return m_oInternalThread.Priority;
      }
      set
      {
        m_oInternalThread.Priority = value;
      }
    }

    public bool IsBackground
    {
      get
      {
        return m_oInternalThread.IsBackground;
      }
      set
      {
        m_oInternalThread.IsBackground = value;
      }
    }

    public int DelayBetweenIterations
    {
      get
      {
        return m_oSleepDelay.Value;
      }
      set
      {
        m_oSleepDelay.Value = value;
      }
    }

    public void Start()
    {
      m_oInternalThread.Start();
    }

    private void vInternalThreadLoop()
    {
      try
      {
        if (m_oThreadCulture != null)
        {
          Thread.CurrentThread.CurrentCulture = m_oThreadCulture;
        }

        while (!m_oSharedShutdownFlag.Value && ThreadLoopBody())
        {
          if (m_oSleepDelay.Value == 0)
          {
            Thread.Sleep(m_oMinimumDelay);
          }
          else if (0 < m_oSleepDelay.Value)
          {
            Thread.Sleep(m_oSleepDelay.Value);
          }
        }
      }
      catch (ThreadAbortException ex)
      {
      }
      finally
      {
        if (OnThreadAborted != null)
        {
          OnThreadAborted((object) this, m_oSharedShutdownFlag);
        }
      }
    }
  }
}
