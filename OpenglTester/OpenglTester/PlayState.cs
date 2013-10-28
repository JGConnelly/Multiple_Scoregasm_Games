/******************************************
 * PlayState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4:15 pm
 * Comments: This is the play game state
 * ****************************************/


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class PlayState : AbstractState
	{
		static readonly PlayState playInstance = new PlayState();
		
		public override void Init()
		{
			Console.WriteLine ("PlayState initialized");
			//TODO: load the backgrounds and buttons and stuff here
		}
		
		public override void Cleanup ()
		{
			Console.WriteLine ("Playstate cleaned up");
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
			//load the content for the playstate
		}
		
		public override void HandleEvents (StateManager game, float dT)
		{
			if (StateManager.spaceIsDown) 
			{
				ChangeState (game, SplashState.GetInstance ());
			}
			//TODO: handle events here
		}
		
		public override void Update (StateManager game, float dT)
		{
			
		}
		
		public override void Draw (StateManager game, float dT)
		{
			//draw stuff to the screen
		}
		
		public static PlayState GetInstance ()
		{
			return playInstance;
		}
		
		private PlayState ()
		{
			//Do nothing
		}
	}
}

