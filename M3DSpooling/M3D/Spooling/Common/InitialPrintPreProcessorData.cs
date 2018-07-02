﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Common.InitialPrintPreProcessorData
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

namespace M3D.Spooling.Common
{
  public class InitialPrintPreProcessorData
  {
    public int StartingTemp;
    public int StartingTempStabilizationDelay;
    public int StartingFanValue;
    public int BedTemperature;
    public int PrimeAmount;
    public int FirstRaftLayerTemperature;
    public bool SecondRaftResetTemp;

    public InitialPrintPreProcessorData()
    {
    }

    public InitialPrintPreProcessorData(InitialPrintPreProcessorData other)
    {
      StartingTemp = other.StartingTemp;
      StartingTempStabilizationDelay = other.StartingTempStabilizationDelay;
      StartingFanValue = other.StartingFanValue;
      BedTemperature = other.BedTemperature;
      PrimeAmount = other.PrimeAmount;
      FirstRaftLayerTemperature = other.FirstRaftLayerTemperature;
      SecondRaftResetTemp = other.SecondRaftResetTemp;
    }
  }
}
