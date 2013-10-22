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





		public Particle (string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{
		}
	}
}

