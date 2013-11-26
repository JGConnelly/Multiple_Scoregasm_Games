/******************************************
 * MenuState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is the credits state
 * ****************************************/


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class WinState : AbstractState
	{
		static readonly WinState winInstance = new WinState();
		Object winBG;
		
		public override void Init()
		{
			Console.WriteLine ("WinState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			winBG = new Object("win");
		}
		
		public override void Cleanup ()
		{
			Console.WriteLine("WinState cleaned up");
			//TODO: delete things
		}
		
		public override void Pause()
		{
		}
		
		public override void Resume()
		{
		}
		
		public override void LoadContent (StateManager game)
		{
			//load the content for the menustate
		}
		
		public override void HandleEvents(StateManager game, float dT)
		{
			//press space to change to PlayState
			if(InputHandler.confirmPressed && (!StateManager.spaceIsDown))
			{
				AudioManager.PlaySound("Audio\\select.wav");
				StateManager.spaceIsDown = true;
				ChangeState(game, MenuState.GetInstance());
			}
			if(!InputHandler.confirmPressed && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}
		}
		
		public override void Update (StateManager game, float dT)
		{
			winBG.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			winBG.Draw();
		}
		
		public static WinState GetInstance ()
		{
			return winInstance;
		}
		
		//private constructor
		private WinState ()
		{
			//Do nothing
		}
	}
}

