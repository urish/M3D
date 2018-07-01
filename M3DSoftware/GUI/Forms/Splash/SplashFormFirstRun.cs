﻿// Decompiled with JetBrains decompiler
// Type: M3D.GUI.Forms.Splash.SplashFormFirstRun
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Properties;
using M3D.Spooling.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace M3D.GUI.Forms.Splash
{
  public class SplashFormFirstRun : Form
  {
    public static bool WasRunForTheFirstTime;
    private IContainer components;
    private Timer timer1;

    public SplashFormFirstRun()
    {
      this.InitializeComponent();
      this.CenterToScreen();
      this.timer1.Start();
      SplashFormFirstRun.WasRunForTheFirstTime = true;
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void SplashForm_Load(object sender, EventArgs e)
    {
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Stop();
      this.timer1.Enabled = false;
      string publicDataFolder = Paths.PublicDataFolder;
      SplashFormFirstRun.DirectoryCopy(Path.Combine(Paths.ResourceFolder, "Data"), Path.Combine(publicDataFolder, "Data"), true);
      SplashFormFirstRun.DirectoryCopy(Path.Combine(Paths.ResourceFolder, "MyLibrary"), Path.Combine(publicDataFolder, "MyLibrary"), true);
      SplashFormFirstRun.DirectoryCopy(Path.Combine(Paths.ResourceFolder, "Utility"), Path.Combine(publicDataFolder, "Utility"), true);
      SplashFormFirstRun.DirectoryCopy(Path.Combine(Paths.ResourceFolder, "Working"), Path.Combine(publicDataFolder, "Working"), true);
      BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(Path.Combine(publicDataFolder, "version.info"), FileMode.Create));
      binaryWriter.Write(M3D.Spooling.Version.Client_Version.major);
      binaryWriter.Write(M3D.Spooling.Version.Client_Version.minor);
      binaryWriter.Write(M3D.Spooling.Version.Client_Version.build);
      binaryWriter.Write(M3D.Spooling.Version.Client_Version.hotfix);
      binaryWriter.Close();
      this.Hide();
      this.Close();
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirName);
      DirectoryInfo[] directories;
      try
      {
        directories = directoryInfo1.GetDirectories();
        if (!directoryInfo1.Exists)
          throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
      }
      catch (Exception ex)
      {
        throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName, ex);
      }
      if (!Directory.Exists(destDirName))
        Directory.CreateDirectory(destDirName);
      try
      {
        new DirectoryInfo(destDirName).Attributes &= ~FileAttributes.ReadOnly;
      }
      catch (Exception ex)
      {
      }
      foreach (FileInfo file in directoryInfo1.GetFiles())
      {
        try
        {
          file.Attributes &= ~FileAttributes.ReadOnly;
        }
        catch (Exception ex)
        {
        }
        try
        {
          string destFileName = Path.Combine(destDirName, file.Name);
          file.CopyTo(destFileName, true);
        }
        catch (Exception ex)
        {
        }
      }
      if (!copySubDirs)
        return;
      foreach (DirectoryInfo directoryInfo2 in directories)
      {
        string destDirName1 = Path.Combine(destDirName, directoryInfo2.Name);
        SplashFormFirstRun.DirectoryCopy(directoryInfo2.FullName, destDirName1, copySubDirs);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.timer1 = new Timer(this.components);
      this.SuspendLayout();
      this.timer1.Interval = 2000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.BackgroundImageLayout = ImageLayout.Center;
      if (Program.isDEBUGBUILD || VersionNumber.Stage.DEBUG == M3D.Spooling.Version.Client_Version.stage)
        this.BackgroundImage = (Image) Resources.splashscreen_debug;
      else if (VersionNumber.Stage.Alpha == M3D.Spooling.Version.Client_Version.stage)
        this.BackgroundImage = (Image) Resources.splashscreenFirstRunAlpha;
      else if (VersionNumber.Stage.Beta == M3D.Spooling.Version.Client_Version.stage)
        this.BackgroundImage = (Image) Resources.splashscreenFirstRun_beta;
      else if (M3D.Spooling.Version.Client_Version.stage == VersionNumber.Stage.Release || VersionNumber.Stage.ReleaseCandidate == M3D.Spooling.Version.Client_Version.stage)
        this.BackgroundImage = (Image) Resources.splashscreenFirstRun_real;
      this.ClientSize = new Size(500, 375);
      this.ControlBox = false;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (SplashFormFirstRun);
      this.TransparencyKey = Color.White;
      this.ResumeLayout(false);
    }
  }
}