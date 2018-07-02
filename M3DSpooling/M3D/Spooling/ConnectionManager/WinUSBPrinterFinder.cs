﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.ConnectionManager.WinUSBPrinterFinder
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Common;
using System.Collections.Generic;
using System.Diagnostics;
using USBClassLibrary;

namespace M3D.Spooling.ConnectionManager
{
  internal class WinUSBPrinterFinder : IUSBPrinterFinder
  {
    private static ThreadSafeVariable<int> _deviceInstallDetectedCount = new ThreadSafeVariable<int>(0);
    private Stopwatch installDetectedTimer = new Stopwatch();
    private List<string> com_ports;

    public WinUSBPrinterFinder()
    {
      com_ports = new List<string>();
    }

    public override List<string> UsbAttached(uint MyDeviceVID, uint MyDevicePID)
    {
      var num = 0;
      var DPList = new List<USBClass.DeviceProperties>();
      lock (com_ports)
      {
        com_ports.Clear();
        if (USBClass.GetUSBDevice(MyDeviceVID, MyDevicePID, DPList, true, new uint?()))
        {
          foreach (USBClass.DeviceProperties deviceProperties in DPList)
          {
            if (!string.IsNullOrEmpty(deviceProperties.COMPort))
            {
              com_ports.Add(deviceProperties.COMPort);
            }
            else
            {
              ++num;
            }
          }
        }
      }
      if (WinUSBPrinterFinder._deviceInstallDetectedCount.Value > num)
      {
        if (installDetectedTimer.IsRunning)
        {
          if (installDetectedTimer.ElapsedMilliseconds > 2000L)
          {
            installDetectedTimer.Stop();
            WinUSBPrinterFinder._deviceInstallDetectedCount.Value = num;
          }
        }
        else
        {
          installDetectedTimer.Restart();
        }
      }
      else
      {
        WinUSBPrinterFinder._deviceInstallDetectedCount.Value = num;
      }

      return new List<string>((IEnumerable<string>)com_ports);
    }

    public static int DeviceInstallDetected
    {
      get
      {
        return WinUSBPrinterFinder._deviceInstallDetectedCount.Value;
      }
    }
  }
}
