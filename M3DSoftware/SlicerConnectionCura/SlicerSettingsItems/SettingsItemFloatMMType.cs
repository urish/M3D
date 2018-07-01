﻿// Decompiled with JetBrains decompiler
// Type: M3D.SlicerConnectionCura.SlicerSettingsItems.SettingsItemFloatMMType
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics.Widgets2D;
using M3D.Slicer.General;
using System;
using System.Xml.Serialization;

namespace M3D.SlicerConnectionCura.SlicerSettingsItems
{
  [Serializable]
  public class SettingsItemFloatMMType : SettingsGenericBoundedNumber<float>, IReportFormat
  {
    [XmlIgnore]
    private bool formatError;

    public SettingsItemFloatMMType()
      : base(float.NaN, new Range<float>(), new Range<float>())
    {
    }

    public SettingsItemFloatMMType(float value, Range<float> warning_range, Range<float> error_range)
      : base(value, warning_range, error_range)
    {
    }

    protected override bool SetFromSlicerValue(string val)
    {
      this.formatError = false;
      try
      {
        this.value = (float) int.Parse(val) / 1000f;
        if ((double) this.value < 0.0)
          this.value = -1f;
      }
      catch (Exception ex)
      {
        this.formatError = true;
        return false;
      }
      return true;
    }

    public override SettingItemType GetItemType()
    {
      return SettingItemType.FloatMMType;
    }

    public override string TranslateToSlicerValue()
    {
      int num = -1;
      if ((double) this.value >= 0.0)
        num = (int) ((double) this.value * 1000.0);
      return num.ToString();
    }

    public override string TranslateToUserValue()
    {
      return this.value.ToString("0.000");
    }

    public override void ParseUserValue(string val)
    {
      this.formatError = false;
      try
      {
        this.value = float.Parse(val);
      }
      catch (Exception ex)
      {
        this.formatError = true;
      }
    }

    public override bool HasError
    {
      get
      {
        if (!this.formatError)
          return base.HasError;
        return true;
      }
    }

    public override bool HasWarning
    {
      get
      {
        if (!this.formatError)
          return base.HasWarning;
        return true;
      }
    }

    public override string GetErrorMsg()
    {
      if (this.formatError)
        return "Not a number";
      return base.GetErrorMsg();
    }

    public override SlicerSettingsItem Clone()
    {
      return (SlicerSettingsItem) new SettingsItemFloatMMType(this.value, this.warning_range, this.error_range);
    }

    [XmlAttribute("Number_Format")]
    public NumFormat Format { get; set; } = NumFormat.Thousands;
  }
}