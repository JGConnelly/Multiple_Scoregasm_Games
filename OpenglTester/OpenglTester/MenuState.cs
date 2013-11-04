/******************************************
 * MenuState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is the menu state
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
	public class MenuState : AbstractState
	{
		static readonly MenuState menuInstance = new MenuState();
		Object menuBG;
		List<Button> buttons;
		Button btnNewGame, btnLoadGame, btnQuit;
		int currentSelectedButton = 0;
		static bool selectionChanged = false;

		public override void Init()
		{
			Console.WriteLine ("MenuState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			menuBG = new Object("menu");

			buttons = new List<Button>();

			Button btnNewGame = new Button(760, 500, "newGameButton", "newGameButtonSelected");
			Button btnLoadGame = new Button(760, 650, "loadGameButton", "loadGameButtonSelected");
			Button btnQuit = new Button(760, 800, "quitButton", "quitButtonSelected");

			buttons.Add(btnNewGame);
			buttons.Add(btnLoadGame);
			buttons.Add(btnQuit);

			buttons[currentSelectedButton].Select();
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

		public override void HandleEvents(StateManager game, float dT)
		{
			//press space to change to PlayState
			if(InputHandler.confirmPressed && (!StateManager.spaceIsDown))
			{
				StateManager.spaceIsDown = true;
				if (btnNewGame.IsSelected())
				{
					ChangeState(game, PlayState.GetInstance());
				}
				if (btnQuit.IsSelected())
				{
					//wuit the game
				}
			}
			if(!InputHandler.confirmPressed && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}

			//select buttons
			if(InputHandler.downPressed && !selectionChanged)
			{
				buttons[currentSelectedButton++].Deselect();
				if(currentSelectedButton >= buttons.Count)
				{
					currentSelectedButton = 0;
				}
				buttons[currentSelectedButton].Select();
				selectionChanged = true;
			}

			if(InputHandler.upPressed && !selectionChanged)
			{
				buttons[currentSelectedButton--].Deselect();
				if (currentSelectedButton < 0)
				{
					currentSelectedButton = buttons.Count - 1;
				}
				buttons[currentSelectedButton].Select();
				selectionChanged = true;
			}

			if (!InputHandler.downPressed && !InputHandler.upPressed)
			{
				selectionChanged = false;
			}
			//TODO: handle events here
		}
		
		public override void Update (StateManager game, float dT)
		{
			menuBG.Update(dT);
		}
		
		public override void Draw(StateManager game, float dT)
		{
			//draw stuff to the screen
			menuBG.Draw();
			foreach(Button btn in buttons)
			{
				btn.Draw();
			}
		}
		
		public static MenuState GetInstance ()
		{
			return menuInstance;
		}

		//private constructor
		private MenuState ()
		{
			//Do nothing
		}
	}
}

