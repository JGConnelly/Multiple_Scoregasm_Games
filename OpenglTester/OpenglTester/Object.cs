using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class Object
	{
	
		private Texture2D tex_Image;
		private int i_NumOfFrames;

		private Vector2 v2_Position;
		private Vector2 v2_Size;
		private float f_Rotation;



		public Object (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager)
		{

			tex_Image=  contentManager.Load<Texture2D>(imagePath);
			v2_Size.Y = tex_Image.Height;
			v2_Size.X = tex_Image.Width;

			i_NumOfFrames =0;

			v2_Position.X = 0;
			v2_Position.Y = 0;

			f_Rotation = 0;
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

		virtual public void Update(double DeltaTime)
		{

		}

		virtual public void Draw(SpriteBatch spriteBatch)
		{

			spriteBatch.Draw(tex_Image,v2_Position,null, Color.White,f_Rotation,new Vector2(0,0),1f,SpriteEffects.None,0f);

		}


		
		
	}
}

