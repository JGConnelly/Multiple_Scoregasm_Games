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
		protected int i_TimeToCompleteAnim;
		protected int i_CurrentFrame;
		protected float f_FrameWidth;
		protected int i_StartFrame;


		protected float f_ElapsedGameTime;
		protected float f_TimePerFrame;

		protected Vector2 v2_Position;
		protected Vector2 v2_Size;
		protected float f_Rotation;

		protected bool b_IsAnimated;
		protected bool b_Paused;


		// create a non animated object
		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager)
		{

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;

			i_NumOfFrames =0;
			i_TimeToCompleteAnim = 0;
			v2_Position.X = 0;
			v2_Position.Y = 0;
			b_IsAnimated = false;
			f_Rotation = 0;
			b_Paused = false;
			i_CurrentFrame = 0;
			f_ElapsedGameTime =0;
			f_TimePerFrame= 0;
			i_StartFrame = 0;
		}
		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager, int numberOfFrames , int timeToComplete)
		{


			b_Paused = false;


			
			i_NumOfFrames =numberOfFrames;
			i_TimeToCompleteAnim = timeToComplete;
			f_TimePerFrame = i_TimeToCompleteAnim / numberOfFrames;
			b_IsAnimated = true;

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;
			v2_Position.X = 0;
			v2_Position.Y = 0;
			f_FrameWidth = tex_Image.Width / i_NumOfFrames; // so what the animator thinks the size of the frames are
			f_Rotation = 0;
			i_CurrentFrame = 0;
			f_ElapsedGameTime=0;
			i_StartFrame = 0;


		}

		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager, int numberOfFrames , int timeToComplete,float frameSize)
		{
			
			
			b_Paused = false;
			
			
			
			i_NumOfFrames =numberOfFrames;
			i_TimeToCompleteAnim = timeToComplete;
			f_TimePerFrame = i_TimeToCompleteAnim / i_NumOfFrames;
			b_IsAnimated = true;
			
			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;
			v2_Position.X = 0;
			v2_Position.Y = 0;

			f_FrameWidth = frameSize; // so what the animator thinks the size of the frames are

			f_Rotation = 0;
			i_CurrentFrame = 0;
			f_ElapsedGameTime=0;
			i_StartFrame = 0;


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

		virtual public void SetAnimationStartPoint (int startPoint, int numberOfFrames, int timeToComplete)
		{
			i_StartFrame = startPoint;
			i_NumOfFrames = numberOfFrames;
			i_TimeToCompleteAnim = timeToComplete;
			f_TimePerFrame = i_TimeToCompleteAnim / i_NumOfFrames;

		}
		// update function
		// 
		// just pass through the (float)gameTime.ElapsedGameTime.TotalSeconds in game class

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

				// anyway.. this creates the rectangle that the animation will use
				Rectangle AnimSourceRect = new Rectangle((int)((f_FrameWidth * i_CurrentFrame)+i_StartFrame*f_FrameWidth), 0,
				                                 (int)f_FrameWidth, tex_Image.Height);

				spriteBatch.Draw(tex_Image, v2_Position, AnimSourceRect, Color.White,
				           f_Rotation, new Vector2(0,0), 1, SpriteEffects.None, 0f);

			} 
			else 
			{
				spriteBatch.Draw (tex_Image, v2_Position, null, Color.White, f_Rotation, new Vector2 (0, 0), 1f, SpriteEffects.None, 0f);
			}

		}


		
		
	}
}

