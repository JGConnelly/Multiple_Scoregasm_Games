using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class Player : Object
	{
		Texture2D Idle, Walk, Run, Jump, Crouch;
		public Player(string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{
			
		}
		//With reference to Jacobs animation, rebuild to have multiple animation strips?
		//derp
	}
}

