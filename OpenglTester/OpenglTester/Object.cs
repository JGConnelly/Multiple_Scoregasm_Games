using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class Object
	{
	
		protected Texture2D tex_Image;
		protected int i_TotalFrames;
		protected int i_NumOfFramesToAnim;
		protected float f_TimeToCompleteAnim;
		protected int i_CurrentFrame;
		protected Vector2 f_FrameSize;
		protected int i_StartFrame;


		protected float f_ElapsedGameTime;
		protected float f_TimePerFrame;

		protected Vector2 v2_Position;
		protected Vector2 v2_Size;
		protected Vector2 v2_Origin;
		protected Vector2 v2_Scale;
		protected float f_Rotation;

		protected bool b_IsAnimated;
		protected bool b_Paused;
		protected bool b_FlipImage;

		// create a non animated object
		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager)
		{

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;

			i_TotalFrames =0;
			f_TimeToCompleteAnim = 0;
			v2_Position.X = 0;
			v2_Position.Y = 0;
			b_IsAnimated = false;
			f_Rotation = 0;
			b_Paused = false;
			i_CurrentFrame = 0;
			f_ElapsedGameTime =0;
			f_TimePerFrame= 0;
			i_StartFrame = 0;
			v2_Origin = new Vector2(0,0);
			b_FlipImage = false;
		}
		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager, int numberOfFrames , float timeToComplete)
		{


			b_Paused = false;
			i_TotalFrames =numberOfFrames;
			f_TimeToCompleteAnim = timeToComplete;

			f_TimePerFrame = f_TimeToCompleteAnim / numberOfFrames;
			b_IsAnimated = true;

			tex_Image=  contentManager.Load<Texture2D>(imagePath);

			v2_Position.X = 0;
			v2_Position.Y = 0;
			f_FrameSize.X = tex_Image.Width / i_TotalFrames; // so what the animator thinks the size of the frames are
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = f_FrameSize.X;
			f_Rotation = 0;
			i_CurrentFrame = 0;
			f_ElapsedGameTime=0;
			i_StartFrame = 0;
			v2_Origin = new Vector2(0,0);
			b_FlipImage = false;

		}

		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager, int TotNumberOfFrames , float timeToComplete,float frameSize)
		{
			
			
			b_Paused = false;
			
			
			
			i_TotalFrames =TotNumberOfFrames;
			f_TimeToCompleteAnim = timeToComplete;
			f_TimePerFrame = f_TimeToCompleteAnim / i_TotalFrames;
			b_IsAnimated = true;
			f_FrameSize.X = frameSize; // so what the animator thinks the size of the frames are
			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = f_FrameSize.X;
			v2_Position.X = 0;
			v2_Position.Y = 0;



			f_Rotation = 0;
			i_CurrentFrame = 0;
			f_ElapsedGameTime=0;
			i_StartFrame = 0;
			v2_Origin = new Vector2(0,0);
			b_FlipImage = false;


		}

		// Accessors and mutators

		public Vector2 Position 
		{
			set {v2_Position=value; }
			get {return v2_Position;}
		}
		public Vector2 Size 
		{
			set {v2_Size=value; }
			get {return v2_Size;}
		}
		public float Rotation
		{
			set {f_Rotation=value;if(f_Rotation>359)f_Rotation = 0; if(f_Rotation<0)f_Rotation = 359; }
			get {return f_Rotation;}
		}
		public Vector2 Scale {
			set { v2_Scale = value;}
			get { return v2_Scale;}
		}
		public void SetCenter()
		{
			v2_Origin = v2_Size/2;
		}	
		public void SetOffCenter()
		{
			v2_Origin = new Vector2(0,0);
		}

		// end of Accessors and mutators


		// generates the alpha from the top left pixel.... i think
		public void GenerateAlpha ()
		{

			//tex_Image = Game.Content.Load<Texture2D>("mySprite");  
			Color[] mySpriteData = new Color[tex_Image.Height * tex_Image.Width];  
			tex_Image.GetData<Color>(mySpriteData); 

			byte redValue  = mySpriteData[0].R;
			byte greenValue  = mySpriteData[0].G;
			byte blueValue  = mySpriteData[0].B;


			for (int j = 0; j < tex_Image.Height * tex_Image.Width ; j++)  
			{
				if(mySpriteData[j].R == redValue && mySpriteData[j].G == greenValue && mySpriteData[j].B == blueValue)
				{
					mySpriteData[j].A = (byte)0;  
				}
			}  
			tex_Image.SetData<Color>(mySpriteData);
		}

		// flips the image horizontally
		public void FlipHorizontal ()
		{
			if(b_FlipImage)
				b_FlipImage = false;
			else
				b_FlipImage = true;
		}


		// set the starting frame 
		virtual public void SetAnimationStartPoint (int startPoint, int numberOfFrames, float timeToComplete)
		{
			i_StartFrame = startPoint;
			i_NumOfFramesToAnim = numberOfFrames;
			f_TimeToCompleteAnim = timeToComplete;
			f_TimePerFrame = f_TimeToCompleteAnim / i_NumOfFramesToAnim;

		}



		virtual public bool CheckCollision (Object other)
		{
			Rectangle thisObjectRect = new Rectangle((int)v2_Position.X,(int)v2_Position.Y,(int)v2_Size.X,(int)v2_Size.Y);
			Rectangle otherRect = new Rectangle((int)other.Position.X,(int)other.Position.Y,(int)other.Size.X,(int)other.Size.Y);

			if(thisObjectRect.Intersects(otherRect) || otherRect.Intersects(thisObjectRect))
				return true;
			else 
				return false;

		}


		// update function 
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
					i_CurrentFrame = i_CurrentFrame % i_NumOfFramesToAnim;
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


			if (b_IsAnimated) 
			{
				// only using horizontal animation right now 
				/// if someone really needs vertical i can change it

				// anyway.. this creates the rectangle that the animation will use
				Rectangle AnimSourceRect = new Rectangle((int)((f_FrameSize.X * i_CurrentFrame)+i_StartFrame*f_FrameSize.X), 0,
				                                         (int)f_FrameSize.X, tex_Image.Height);
				if(b_FlipImage)
					spriteBatch.Draw(tex_Image, v2_Position, AnimSourceRect, Color.White,
					                 f_Rotation, new Vector2(0,0), v2_Scale, SpriteEffects.FlipHorizontally, 0f);
				else
					spriteBatch.Draw(tex_Image, v2_Position, AnimSourceRect, Color.White,
					                 f_Rotation, new Vector2(0,0), v2_Scale, SpriteEffects.None, 0f);

			} 
			else 
			{
				if(b_FlipImage)
					spriteBatch.Draw (tex_Image, v2_Position,null, Color.White, f_Rotation, v2_Origin, v2_Scale, SpriteEffects.FlipHorizontally, 0f);
				else
					spriteBatch.Draw (tex_Image, v2_Position,null, Color.White, f_Rotation, v2_Origin, v2_Scale, SpriteEffects.None, 0f);
			}

		}
		

	}
}

