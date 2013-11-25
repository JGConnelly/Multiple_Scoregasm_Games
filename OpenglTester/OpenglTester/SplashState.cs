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
		Object splashBG;

		CoinEmitter CoinFall;
		
		public override void Init()
		{
			Console.WriteLine("SplashState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			splashBG = new Object("newSplash");

			CoinFall = new CoinEmitter(-50,1920,1200,1000,10);
			CoinFall.Initialise(0,0);
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
			if(InputHandler.confirmPressed && (!StateManager.spaceIsDown))
			{
				StateManager.spaceIsDown = true;
				ChangeState(game, MenuState.GetInstance());
			}
			if(!InputHandler.confirmPressed && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}
			//TODO: handle events here
		}
		
		public override void Update(StateManager game, float dT)
		{
			splashBG.Update(dT);
			CoinFall.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			splashBG.Draw();
			CoinFall.Draw();
		}
		
		public static SplashState GetInstance()
		{
			return splashInstance;
		}


		//private constructor
		private SplashState()
		{
			//Do nothing
		}
	}
}

