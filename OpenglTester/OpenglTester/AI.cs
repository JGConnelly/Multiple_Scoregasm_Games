
using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace OpenglTester
{
	public class AI : Object
	{
		public AI(string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{

		}

		void Update(double DeltaTime)
		{
			base.Update((float)DeltaTime);
		}
		void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}

