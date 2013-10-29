/***************************************
 * Resolution.cs
 * Tash
 * Tuesday 29 October 2013
 * The resolution class resizes the game to fit into the full screen resolution of whatever size screen it is on.
 * With help from Jack and 
 * http://blog.roboblob.com/2013/07/27/solving-resolution-independent-rendering-and-2d-camera-using-monogame/
 * and http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering/
 * ***************************************/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenglTester
{
	static class Resolution
	{
		static private GraphicsDeviceManager device = null;
		static private int screenW = 0;
		static private int screenH = 0;
		static private int baseW = 1920;
		static private int baseH = 1080;
		static private Matrix scaleMatrix;
		static private bool fullScreen = true;
		static private bool dirtyMatrix = true;

		static public void Init(ref GraphicsDeviceManager inDevice)
		{
			device = inDevice;
			screenW = device.PreferredBackBufferWidth;
			screenH = device.PreferredBackBufferHeight;
			dirtyMatrix = true;
			ApplyResolutionSettings();
		}

		static public Matrix getTransformationMatric()
		{
			if (dirtyMatrix)
				RecreateScaleMatrix();
			return scaleMatrix;
		}

		static public void SetResolution(int w, int h, bool isFull)
		{
			screenW = w;
			screenH = h;
			fullScreen = isFull;
			ApplyResolutionSettings();
		}

		static public void SetBaseResolution(int w, int h)
		{
			baseW = w;
			baseH = h;
			dirtyMatrix = true;
		}

		static private void ApplyResolutionSettings()
		{
			//if we aren't using full screen, the size can be anything smaller than the actual screen size
			if(!fullScreen)
			{
				if((screenW <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) && (screenH <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
				{
					device.PreferredBackBufferWidth = screenW;
					device.PreferredBackBufferHeight = screenH;
					device.IsFullScreen = fullScreen;
					device.ApplyChanges();
				}
			}
			//if fullscreen, check that display adapter can support the display mode by looping through the supported modes and checking
			else
			{
				foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
				{
					//cehck the width and ehight of each mode against the passed values
					if ((dm.Width == screenW) && (dm.Height == screenH))
					{
						//if the mode is supported, set buffer formats, apply changes, and return
						device.PreferredBackBufferWidth = screenW;
						device.PreferredBackBufferHeight = screenH;
						device.IsFullScreen = fullScreen;
						device.ApplyChanges();
					}
				}
			}
			dirtyMatrix = true;
			screenW = device.PreferredBackBufferWidth;
			screenH = device.PreferredBackBufferHeight;
		}

		static public void BeginDraw()
		{
			//reset veiwport to (0, 0, 1, 1)
			FullViewport();
			//clear to black
			device.GraphicsDevice.Clear(Color.Black);
			//calculate proper viewport according to aspect ratio
			ResetViewport();
			//and clear it
			device.GraphicsDevice.Clear(Color.DarkOrange);
			//this will make black bars if needed, and dark orange on the game area
		}

		static private void RecreateScaleMatrix()
		{
			dirtyMatrix = false;
			scaleMatrix = Matrix.CreateScale((float)device.GraphicsDevice.Viewport.Width / baseW, (float)device.GraphicsDevice.Viewport.Height / baseH, 1f);
		}

		static public void FullViewport()
		{
			Viewport vp = new Viewport();
			vp.X = vp.Y = 0;
			vp.Width = screenW;
			vp.Height = screenH;
			device.GraphicsDevice.Viewport = vp;
		}

		static public float GetBaseAspectRatio()
		{
			return (float)baseW / (float)baseH;
		}

		static public void ResetViewport()
		{
			float targetAspectRatio = GetBaseAspectRatio();
			//find the largest area that fits in this resolution at the desired aspect ratio
			int width = device.PreferredBackBufferWidth;
			int height = (int)(width / targetAspectRatio + 0.5f);
			bool changed = false;

			if(height > device.PreferredBackBufferHeight)
			{
				height = device.PreferredBackBufferHeight;
				//pillarbox
				width = (int)(height * targetAspectRatio + 0.5f);
				changed = true;
			}

			//setup the new viewport centered in the back buffer
			Viewport vp = new Viewport();
			vp.X = (device.PreferredBackBufferWidth / 2) - (screenW / 2);
			vp.Y = (device.PreferredBackBufferHeight / 2) - (screenH / 2);
			vp.Width = screenW;
			vp.Height = screenH;
			vp.MinDepth = 0;
			vp.MaxDepth = 1;

			if(changed)
			{
				dirtyMatrix = true;
			}

			device.GraphicsDevice.Viewport = vp;
		}
	}
}

