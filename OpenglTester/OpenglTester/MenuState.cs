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
		Button btnNewGame, btnLoadGame, btnQuit;
		List<Button> buttons;
		static bool changed = false;
		static int currentlySelectedButton = 0;
		
		public override void Init()
		{
			Console.WriteLine ("MenuState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			menuBG = new Object("menu");
			
			btnNewGame = new Button(760, 500, "newGameButton", "newGameButtonSelected");
			//btnLoadGame = new Button(760, 650, "loadGameButton", "loadGameButtonSelected");
			btnQuit = new Button(760, 700, "quitButton", "quitButtonSelected");
			
			buttons = new List<Button>();
			buttons.Add(btnNewGame);
			//buttons.Add(btnLoadGame);
			buttons.Add(btnQuit);
			
			buttons[currentlySelectedButton].Select();

			AudioManager.SetMusicVolume(0.3f);
			AudioManager.PlayMusic("Audio\\Hitman.wav");

			AudioManager.LoadSound("Audio\\select.wav");
			AudioManager.LoadSound("Audio\\bip.wav");
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
				AudioManager.PlaySound ("Audio\\select.wav");
				StateManager.spaceIsDown = true;
				if(btnNewGame.isSelected)
				{
					ChangeState(game, PlayState.GetInstance());
				}
				if(btnQuit.isSelected)
				{
					Program.game.Quit();
				}
			}
			if(!InputHandler.confirmPressed && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}

			//change menu buttons
			if(InputHandler.downPressed && !changed)
			{
				AudioManager.PlaySound("Audio\\bip.wav");
				buttons[currentlySelectedButton++].Deselect();
				if(currentlySelectedButton >= buttons.Count)
				{
					currentlySelectedButton = 0;
				}
				buttons[currentlySelectedButton].Select();
				changed = true;
			}
			if(InputHandler.upPressed && !changed)
			{
				AudioManager.PlaySound("Audio\\bip.wav");
				buttons[currentlySelectedButton--].Deselect();
				if (currentlySelectedButton < 0)
				{
					currentlySelectedButton = buttons.Count - 1;
				}
				buttons[currentlySelectedButton].Select();
				changed = true;
			}
			if(!InputHandler.downPressed && !InputHandler.upPressed)
			{
				changed = false;
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

