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
		Object playBG;
		
		AI AnAI;
		AI hooker;
		public static Player player;
		SnowEmitter SnowFall;

		FileHandler fileManager = new FileHandler();
		Level CurrentLevel;

		public override void Init()
		{
			Console.WriteLine ("PlayState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			playBG = new Object("game");


			// seting up the new player
			player = new Player("Assn7MainCharacterSpritesheet4",65,20,32f * 4);
			player.Position = new Vector2(64,630);
			AnAI = new AI("Token4", 65, 20, 32f * 4);
			AnAI.Position = new Vector2(500, 650);
			hooker = new AI("Hooker4", 11, 0, 32f * 4, true);
			hooker.Position = new Vector2(1000, 630);
			hooker.IsHooker = true;
			SnowFall = new SnowEmitter(0,1920,1200,1000,10);
			SnowFall.Initialise(0,0);
			string thisLevel = fileManager.LoadPlayer();
			CurrentLevel = fileManager.LoadLevel(thisLevel);

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
		
		public override void HandleEvents(StateManager game, float dT)
		{

			//movement of player
			if (Keyboard.GetState ().IsKeyDown (Keys.D)) 
			{
				//player.CheckCollision
			}
			if (Keyboard.GetState ().IsKeyDown (Keys.A)) 
			{

			}

			//press left bumper on the controller to go back to the splash screen
			//if(GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
			//{
			//	ChangeState(game, SplashState.GetInstance());
			//}

			//press space to change to SplashState
			if(InputHandler.confirmPressed && (!StateManager.spaceIsDown))
			{
				StateManager.spaceIsDown = true;
				//ChangeState(game, MenuState.GetInstance());
			}
			if (InputHandler.btnLeftShoulder)
				ChangeState(game, SplashState.GetInstance());
			if(!InputHandler.confirmPressed && StateManager.spaceIsDown)
			{
				StateManager.spaceIsDown = false;
			}
			//TODO: handle events here
		}

		public override void Update (StateManager game, float dT)
		{
			player.Update(dT);
			playBG.Update(dT);
			AnAI.Update(dT);
			hooker.Update(dT);
			SnowFall.Update(dT);
			CurrentLevel.Update(dT);
		}
		
		public override void Draw (StateManager game, float dT)
		{
			//draw stuff to the screen
			playBG.Draw();
			//so draw your objects etc
			//CurrentLevel.Draw ();
			AnAI.Draw();
			hooker.Draw();
			player.Draw();
			SnowFall.Draw();

		}
		
		public static PlayState GetInstance ()
		{
			return playInstance;
		}

		//private constructor
		private PlayState ()
		{
			//Do nothing
		}
	}
}

