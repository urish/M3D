﻿using M3D.GUI.Forms;
using M3D.Model;
using System;

namespace M3D.Graphics.Ext3D.ModelRendering
{
  public class OpenGLRendererObject
  {
    private int try_count;
    private GraphicsModelData graphicsModelData;
    private OpenGLRender openGLRender;
    public static OpenGLRendererObject.OpenGLRenderMode openGLRenderMode;
    private bool useTextures;

    public OpenGLRendererObject(ModelData model)
      : this(new GraphicsModelData(model, model.GetVertexCount() < 10000), false)
    {
    }

    public OpenGLRendererObject(GraphicsModelData model, bool useTextures)
    {
      this.useTextures = useTextures;
      graphicsModelData = model;
    }

    public void Reset()
    {
      try
      {
        Delete();
      }
      catch (Exception ex)
      {
      }
      try
      {
        Create();
      }
      catch (Exception ex)
      {
      }
    }

    public void Delete()
    {
      try
      {
        if (openGLRender == null)
        {
          return;
        }

        openGLRender.Dispose();
        openGLRender = null;
      }
      catch (Exception ex)
      {
      }
    }

    public void Create()
    {
      Delete();
      switch (OpenGLRendererObject.openGLRenderMode)
      {
        case OpenGLRendererObject.OpenGLRenderMode.VBOs:
          openGLRender = new OpenGLRendererVBOs(graphicsModelData);
          break;
        case OpenGLRendererObject.OpenGLRenderMode.ARBVBOs:
          openGLRender = new OpenGLRendererARBVBOs(graphicsModelData);
          break;
        default:
          openGLRender = new OpenGLRendererImmediateMode(graphicsModelData);
          break;
      }
      openGLRender.Create();
    }

    public void Draw()
    {
      try
      {
        if (openGLRender != null && openGLRender.RenderMode != OpenGLRendererObject.openGLRenderMode)
        {
          Delete();
        }

        if (openGLRender == null)
        {
          Create();
        }

        openGLRender.Draw();
      }
      catch (Exception ex)
      {
        Reset();
        if (OpenGLRendererObject.openGLRenderMode == OpenGLRendererObject.OpenGLRenderMode.ImmediateMode)
        {
          ++try_count;
          if (try_count <= 2)
          {
            return;
          }

          ExceptionForm.ShowExceptionForm(new Exception("VBOObject::Draw::Failure", ex));
        }
        else
        {
          Reset();
          OpenGLRendererObject.openGLRenderMode = OpenGLRendererObject.OpenGLRenderMode.ImmediateMode;
        }
      }
    }

    public void Translate(float x, float y, float z)
    {
      for (var index = 0; index < graphicsModelData.dataTNV.Length; ++index)
      {
        graphicsModelData.dataTNV[index].Position.X += x;
        graphicsModelData.dataTNV[index].Position.Y += y;
        graphicsModelData.dataTNV[index].Position.Z += z;
      }
    }

    public void Scale(float x, float y, float z)
    {
      for (var index = 0; index < graphicsModelData.dataTNV.Length; ++index)
      {
        graphicsModelData.dataTNV[index].Position.X *= x;
        graphicsModelData.dataTNV[index].Position.Y *= y;
        graphicsModelData.dataTNV[index].Position.Z *= z;
      }
    }

    public enum OpenGLRenderMode
    {
      VBOs,
      ARBVBOs,
      ImmediateMode,
    }
  }
}
