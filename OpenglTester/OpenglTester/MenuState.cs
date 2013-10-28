/******************************************
 * MenuState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is the menu state
 * ****************************************/


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class MenuState : AbstractState
	{
		static readonly MenuState menuInstance = new MenuState();
		public override void Init()
		{
			Console.WriteLine ("MenuState initialized");
			//TODO: load the backgrounds and buttons and stuff here
		}
		
		public override void Cleanup ()
		{
			Console.WriteLine("MenuState cleaned up");
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

		public override void HandleEvents (StateManager game, float dT)
		{
			if (StateManager.spaceIsDown) 
			{
				ChangeState (game, PlayState.GetInstance ());
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
		
		public static MenuState GetInstance ()
		{
			return menuInstance;
		}
		
		private MenuState ()
		{
			//Do nothing
		}
	}
}

