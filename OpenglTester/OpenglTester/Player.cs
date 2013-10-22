using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	struct AnimationInfo
	{
		int Start, End, TimeForCompletion;
	};
	public class Player : Object
	{
		enum Action
		{
			idle,
			walk,
			run,
			sneak,
			jump,
			punch
		};



		public Player(string imagePath ,GraphicsDeviceManager gdevman,ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{
			
		}

	}
}

