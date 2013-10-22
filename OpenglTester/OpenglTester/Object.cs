using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class Object
	{
	
		protected Texture2D tex_Image;
		protected int i_NumOfFrames;
		protected int i_FramesPerSec;
		protected int i_CurrentFrame;

		protected float f_ElapsedGameTime;
		protected float f_TimePerFrame;

		protected Vector2 v2_Position;
		protected Vector2 v2_Size;
		protected float f_Rotation;

		protected bool b_IsAnimated;
		protected bool b_Paused;

		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager)
		{

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;

			i_NumOfFrames =0;
			i_FramesPerSec = 0;
			v2_Position.X = 0;
			v2_Position.Y = 0;
			b_IsAnimated = false;
			f_Rotation = 0;
			b_Paused = false;
			i_CurrentFrame = 0;
			f_ElapsedGameTime =0;
			f_TimePerFrame= 0;
		}
		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager, int numberOfFrames , int animFPS)
		{


			b_Paused = false;


			
			i_NumOfFrames =numberOfFrames;
			i_FramesPerSec = animFPS;
			f_TimePerFrame = i_FramesPerSec / numberOfFrames;
			b_IsAnimated = true;

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;
			v2_Position.X = 0;
			v2_Position.Y = 0;
			
			f_Rotation = 0;
			i_CurrentFrame = 0;
			f_ElapsedGameTime=0;
		}



		// Accessors and mutators
		Vector2 GetPosition()
		{
			return v2_Position;
		}
		Vector2 GetSize()
		{
			return v2_Size;
		}
		public void SetPosition (Vector2 newPos)
		{
			v2_Position = newPos;
		}
		public void SetSize (Vector2 newSize)
		{
			v2_Size = newSize;
		}


		// update function
		// 
		virtual public void Update(float Elapsed)
		{
			 

			// if the sprite is animated 
			if (b_IsAnimated) 
			{

				if (b_Paused)
					return;
				f_ElapsedGameTime += Elapsed;
				if (f_ElapsedGameTime > f_TimePerFrame)
				{
					i_CurrentFrame++;
					// Keep the Frame between 0 and the total frames, minus one.
					i_CurrentFrame = i_CurrentFrame % i_NumOfFrames;
					f_ElapsedGameTime -= f_TimePerFrame;
				}
				
			} 
			// otherwise
			else 
			{
				//pretty much do nothing
			}
		}

		virtual public void Draw (SpriteBatch spriteBatch)
		{
			if (b_IsAnimated) {
				// only using horizontal animation right now 
				/// if someone really needs vertical i can change it
				int FrameWidth = tex_Image.Width / i_NumOfFrames;
				Rectangle sourcerect = new Rectangle(FrameWidth * i_CurrentFrame, 0,
				                                     FrameWidth, tex_Image.Height);

				spriteBatch.Draw(tex_Image, v2_Position, sourcerect, Color.White,
				           f_Rotation, new Vector2(0,0), 1, SpriteEffects.None, 0f);

			} 
			else 
			{
				spriteBatch.Draw (tex_Image, v2_Position, null, Color.White, f_Rotation, new Vector2 (0, 0), 1f, SpriteEffects.None, 0f);
			}

		}


		
		
	}
}

