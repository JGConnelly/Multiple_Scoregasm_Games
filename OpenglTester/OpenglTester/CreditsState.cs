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
	public class CreditsState : AbstractState
	{
		static readonly CreditsState creditsInstance = new CreditsState();
		Object creditsBG;
		
		public override void Init()
		{
			Console.WriteLine ("CreditsState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			creditsBG = new Object("creditsImg");
		}
		
		public override void Cleanup ()
		{
			Console.WriteLine("CreditsState cleaned up");
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
			creditsBG.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			creditsBG.Draw();
		}
		
		public static CreditsState GetInstance ()
		{
			return creditsInstance;
		}
		
		//private constructor
		private CreditsState ()
		{
			//Do nothing
		}
	}
}

