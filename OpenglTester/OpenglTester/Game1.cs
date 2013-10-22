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
		GraphicsDeviceManager graphics;
		ContentManager contentManager;
		SpriteBatch spriteBatch;
		AI SomeFuckingThing;



		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			contentManager = new ContentManager(Content.ServiceProvider);
			contentManager.RootDirectory = "Content";	            
			graphics.IsFullScreen = true;	
			SomeFuckingThing = new AI("Untitled",graphics,contentManager );

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			//Texture2D pic= new Texture2D(;
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (Keyboard.GetState().IsKeyDown(Keys.Escape) )
			{
				Console.Out.WriteLine("quiting");
				Exit ();
			};

			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
				Exit ();
			}
			// TODO: Add your update logic here		
			SomeFuckingThing.Update(timeDelta);
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.AntiqueWhite);
			spriteBatch.Begin();
			//TODO: Add your drawing code here
            //so draw your objects etc
			SomeFuckingThing.Draw(spriteBatch);
			spriteBatch.End();
			base.Draw (gameTime);
		}
	}
}

