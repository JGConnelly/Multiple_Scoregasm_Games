#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

#endregion

namespace OpenglTester
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		public static GraphicsDeviceManager graphics;
		public static ContentManager contentManager;
		public static SpriteBatch spriteBatch;
		StateManager gameManager = new StateManager();
		InputHandler input = new InputHandler();
		float timeDelta;

		public Game1()
		{
			//set the screen resolution
			graphics = new GraphicsDeviceManager(this);
			Resolution.Init(ref graphics);
			Resolution.SetBaseResolution(1920, 1080);	 //the resolution that the game is written in
			Resolution.SetResolution(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, true);	 //the resolution of the actual screen resolution
			//graphics.IsFullScreen = true; 	//no longer necessary

			contentManager = new ContentManager(Content.ServiceProvider);
			contentManager.RootDirectory = "Content";	    

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// in this example of the animated object:
			// 4th arg: using the untitled image i only want to render through 3 of the four frames 
			// 5th arg: time to animate the entire image
			// size vertically of individual frames
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			base.Initialize();

			//initialize the game state manager and set the initial state to the splash screen
			gameManager.Init();
			gameManager.ChangeState(SplashState.GetInstance ());

				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			//Texture2D pic= new Texture2D(;
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			//get delta time
			timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

			//update the inputs (keyboard and controller)
			input.Update();

			//update the current state
			gameManager.HandleEvents (timeDelta);
			gameManager.Update (timeDelta);

			if(InputHandler.escPressed)
			{
				Console.Out.WriteLine("quitting");
				Exit ();
			}
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
				Exit ();
			}

			// TODO: Add your update logic here		
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{

			Game1.graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.NonPremultiplied);

			//TODO: Add your drawing code here


			//so draw your objects etc
			gameManager.Draw (timeDelta);
			/// draw in the playstate manager
           

			spriteBatch.End();
			base.Draw(gameTime);

		}
	}
}
