/******************************************
 * MenuState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is the how to play state
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
	public class HowState : AbstractState
	{
		static readonly HowState howInstance = new HowState();
		Object howBG;
		
		public override void Init()
		{
			Console.WriteLine ("HowState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			howBG = new Object("howImg");
		}
		
		public override void Cleanup ()
		{
			Console.WriteLine("HowState cleaned up");
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
			howBG.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			howBG.Draw();
		}
		
		public static HowState GetInstance ()
		{
			return howInstance;
		}
		
		//private constructor
		private HowState ()
		{
			//Do nothing
		}
	}
}

