﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Common.ABS_FilamentProfile
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Printer_Profiles;

namespace M3D.Spooling.Common
{
  public class ABS_FilamentProfile : FilamentProfile
  {
    private readonly float ABSWarningDim;

    public ABS_FilamentProfile(FilamentSpool spool, PrinterProfile printer_profile)
      : base(spool)
    {
      ABSWarningDim = printer_profile.PrinterSizeConstants.ABSWarningDim;
      preprocessor.initialPrint.StartingTemp = filament.filament_temperature;
      if ("Micro" == printer_profile.ProfileName)
      {
        preprocessor.initialPrint.StartingFanValue = 50;
      }
      else
      {
        preprocessor.initialPrint.StartingFanValue = 220;
      }

      preprocessor.initialPrint.StartingTempStabilizationDelay = 10;
      preprocessor.bonding.FirstLayerTemp = printer_profile.TemperatureConstants.GetBoundedTemp(filament.filament_temperature + 15);
      preprocessor.bonding.SecondLayerTemp = printer_profile.TemperatureConstants.GetBoundedTemp(filament.filament_temperature + 10);
      preprocessor.initialPrint.PrimeAmount = 19;
      preprocessor.initialPrint.FirstRaftLayerTemperature = printer_profile.TemperatureConstants.GetBoundedTemp(filament.filament_temperature + 15);
      preprocessor.initialPrint.SecondRaftResetTemp = false;
    }

    public override bool TestSizeWarning(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
      if ((double) maxX - (double) minX <= (double)ABSWarningDim && (double) maxY - (double) minY <= (double)ABSWarningDim)
      {
        return (double) maxZ - (double) minZ > (double)ABSWarningDim;
      }

      return true;
    }

    public override string ShortName
    {
      get
      {
        return "ABS";
      }
    }
  }
}
