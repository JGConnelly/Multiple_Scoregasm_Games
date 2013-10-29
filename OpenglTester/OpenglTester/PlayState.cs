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
		Player player;
		SnowEmitter SnowFall;
		public override void Init()
		{
			Console.WriteLine ("PlayState initialized");
			//TODO: load the backgrounds and buttons and stuff here
			playBG = new Object("game", Game1.graphics, Game1.contentManager);
<<<<<<< HEAD
			player = new Player("Assn7MainCharacterSpritesheet",Game1.graphics,Game1.contentManager,65,3,32f);
=======

			// seting up the new player
			player = new Player("Assn7MainCharacterSpritesheet",Game1.graphics,Game1.contentManager,65,3,32f);
			SnowFall = new SnowEmitter(0
>>>>>>> JacobSwagStation
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
			//movement of player
			if (Keyboard.GetState ().IsKeyDown (Keys.D)) 
			{
				//player.CheckCollision
			}
			if (Keyboard.GetState ().IsKeyDown (Keys.A)) 
			{

			}
			//press space to change to SplashState
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

		public override void Update (StateManager game, float dT)
		{
			player.Update(dT);
			playBG.Update(dT);
		}
		
		public override void Draw (StateManager game, float dT)
		{
			//draw stuff to the screen
			playBG.Draw(Game1.spriteBatch);
			//so draw your objects etc
			player.Draw(Game1.spriteBatch);
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

