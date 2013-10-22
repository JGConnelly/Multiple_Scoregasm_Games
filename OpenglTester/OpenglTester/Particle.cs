using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;




namespace OpenglTester
{
	public class Particle:Object
	{
		private bool b_Alive;
		private Vector2 v2_LinearVelocity;
		private float f_RotationalVelocity;
		private bool b_FollowRotation;

		private float f_LifeRemaining;




		public Particle (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{


		}
		public override void Update (float Elapsed)
		{
			// now move the sprite to appropriate position
			f_Rotation += f_RotationalVelocity * Elapsed;
			if (f_Rotation > 360)
				f_Rotation-= 360;
			if (f_Rotation < 0)
				f_Rotation+= 360;
			if(b_FollowRotation)
			{
				v2_Position.X += v2_LinearVelocity.X * Math.Cos(f_Rotation* 0.0174532925) *Elapsed;
				if(Math.Sin(f_Rotation * 0.0174532925) !=0)
					v2_Position.Y -= v2_LinearVelocity.Y * Math.Sin(f_Rotation * 0.0174532925) *Elapsed;
				else
					v2_Position.Y -= v2_LinearVelocity.Y  *Elapsed;
			}
			else
			{
				v2_Position.X += v2_LinearVelocity.X * Elapsed;
				v2_Position.Y -= v2_LinearVelocity.Y  *Elapsed;
			}
			

			
			f_LifeRemaining -=Elapsed;
			if(f_LifeRemaining < 0 )
				b_Alive = false;

			base.Update (Elapsed);

		}
		public override void Draw (SpriteBatch spriteBatch)
		{
			if (b_Alive)
				base.Draw (spriteBatch);
		}
	}
}

