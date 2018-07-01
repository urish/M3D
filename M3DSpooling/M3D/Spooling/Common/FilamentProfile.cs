﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Common.FilamentProfile
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Printer_Profiles;
using System;

namespace M3D.Spooling.Common
{
  public abstract class FilamentProfile
  {
    public FilamentPreprocessorData preprocessor = new FilamentPreprocessorData();
    public SlicerProfile slicerProfile = new SlicerProfile();
    protected FilamentSpool filament;

    public FilamentProfile(FilamentSpool spool)
    {
      this.filament = spool;
      this.preprocessor.initialPrint.BedTemperature = FilamentConstants.Temperature.BedDefault(spool.filament_type);
    }

    public abstract bool TestSizeWarning(float minX, float maxX, float minY, float maxY, float minZ, float maxZ);

    public static FilamentProfile CreateFilamentProfile(FilamentSpool spool, PrinterProfile printer_profile)
    {
      switch (spool.filament_type)
      {
        case FilamentSpool.TypeEnum.ABS:
        case FilamentSpool.TypeEnum.HIPS:
          return (FilamentProfile) new ABS_FilamentProfile(spool, printer_profile);
        case FilamentSpool.TypeEnum.PLA:
        case FilamentSpool.TypeEnum.CAM:
          return (FilamentProfile) new PLA_FilamentProfile(spool, printer_profile);
        case FilamentSpool.TypeEnum.FLX:
        case FilamentSpool.TypeEnum.TGH:
          return (FilamentProfile) new TGH_FilamentProfile(spool, printer_profile);
        case FilamentSpool.TypeEnum.ABS_R:
          return (FilamentProfile) new ABS_R_FilamentProfile(spool, printer_profile);
        default:
          throw new ArgumentException("FilamentProfile.CreateFilamentProfile does not know that type :(");
      }
    }

    public string Name
    {
      get
      {
        return this.Type.ToString() + " " + this.Color.ToString();
      }
    }

    public FilamentSpool.TypeEnum Type
    {
      get
      {
        return this.filament.filament_type;
      }
    }

    public FilamentConstants.ColorsEnum Color
    {
      get
      {
        return (FilamentConstants.ColorsEnum) Enum.ToObject(typeof (FilamentConstants.ColorsEnum), this.filament.filament_color_code);
      }
    }

    public int Temperature
    {
      get
      {
        return this.filament.filament_temperature;
      }
    }

    public FilamentSpool Spool
    {
      get
      {
        return this.filament;
      }
    }

    public abstract string ShortName { get; }

    public static string GenerateSpoolName(FilamentSpool spool, bool location)
    {
      FilamentConstants.ColorsEnum color = (FilamentConstants.ColorsEnum) Enum.ToObject(typeof (FilamentConstants.ColorsEnum), spool.filament_color_code);
      if (location)
        return "My " + FilamentConstants.ColorsToString(color) + " Filament (" + spool.filament_type.ToString() + ") " + spool.filament_location.ToString();
      return "My " + FilamentConstants.ColorsToString(color) + " Filament (" + spool.filament_type.ToString() + ") ";
    }

    public override string ToString()
    {
      return this.Name;
    }

    public struct TypeColorKey
    {
      public FilamentSpool.TypeEnum type;
      public FilamentConstants.ColorsEnum color;

      public TypeColorKey(FilamentProfile.TypeColorKey other)
      {
        this.type = other.type;
        this.color = other.color;
      }

      public TypeColorKey(FilamentSpool.TypeEnum type, FilamentConstants.ColorsEnum color)
      {
        this.type = type;
        this.color = color;
      }

      public override int GetHashCode()
      {
        return 13 * (13 * 27 + this.type.GetHashCode()) + this.color.GetHashCode();
      }

      public override bool Equals(object b)
      {
        if (b is FilamentProfile.TypeColorKey)
          return this.Equals((FilamentProfile.TypeColorKey) b);
        return false;
      }

      public bool Equals(FilamentProfile.TypeColorKey b)
      {
        if ((ValueType) b == null || this.type != b.type)
          return false;
        return this.color == b.color;
      }

      public static bool operator ==(FilamentProfile.TypeColorKey a, FilamentProfile.TypeColorKey b)
      {
        if ((ValueType) a == (ValueType) b)
          return true;
        if ((ValueType) a == null || (ValueType) b == null || a.type != b.type)
          return false;
        return a.color == b.color;
      }

      public static bool operator !=(FilamentProfile.TypeColorKey a, FilamentProfile.TypeColorKey b)
      {
        return !(a == b);
      }
    }

    public struct CustomOptions
    {
      public int temperature;
    }
  }
}