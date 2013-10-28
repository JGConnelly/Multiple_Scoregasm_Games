/******************************************
 * SplashState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is the splash state
 * ****************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class SplashState : AbstractState
	{
		static readonly SplashState splashInstance = new SplashState();
		AI SomeFuckingThing;
		
		public override void Init()
		{
			Console.WriteLine("SplashState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			SomeFuckingThing = new AI("Untitled", Game1.graphics, Game1.contentManager);
		}
		
		public override void Cleanup()
		{
			Console.WriteLine("SplashState cleaned up");
			//TODO: delete things
		}
		
		public override void Pause()
		{
		}
		
		public override void Resume()
		{
		}
		
		public override void LoadContent(StateManager game)
		{
			//load the content for the splashstate
		}
		
		public override void HandleEvents(StateManager game, float dT)
		{
			//press space to change to MenuState
			if(Keyboard.GetState().IsKeyDown(Keys.Space) && (!StateManager.spaceIsDown))
			{
				StateManager.spaceIsDown = true;
				ChangeState(game, MenuState.GetInstance());
			}
			if(Keyboard.GetState().IsKeyUp(Keys.Space) && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}
			//TODO: handle events here
		}
		
		public override void Update(StateManager game, float dT)
		{
			SomeFuckingThing.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			SomeFuckingThing.Draw(Game1.spriteBatch);
		}
		
		public static SplashState GetInstance()
		{
			return splashInstance;
		}
		
		private SplashState()
		{
			//Do nothing
		}
	}
}

