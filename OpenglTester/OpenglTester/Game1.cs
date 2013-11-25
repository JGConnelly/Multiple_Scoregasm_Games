#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

#endregion

namespace OpenglTester
{
	/// <summary>
	/// 
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		public static GraphicsDeviceManager graphics;
		public static ContentManager contentManager;
		public static SpriteBatch spriteBatch;
		public static AudioManager gameSound;
		public static StateManager gameManager = new StateManager();
		InputHandler input = new InputHandler();
		float timeDelta;
		int defaultWidth = 1920;
		int defaultHeight = 1080;
		Matrix SpriteScale;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenglTester.Game1"/> class.
		/// </summary>
		public Game1()
		{
			////set the screen resolution
			graphics = new GraphicsDeviceManager(this);
			graphics.IsFullScreen = true;

			contentManager = new ContentManager(Content.ServiceProvider);
			contentManager.RootDirectory = "Content";	   

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			gameSound = new AudioManager();

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
			
			graphics.PreferredBackBufferWidth = defaultWidth;
			graphics.PreferredBackBufferHeight = defaultHeight;
			graphics.ApplyChanges();
			graphics.GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);

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

			//scale sprites up or down based on current viewport
			//(currently the game will stretch to avoid black bars at the bottom and right side if the display isn't 16:9
			//to fix this, delete the heightscale line, and set the next line to be: SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
			float screenscale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / defaultWidth;
			float heightscale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / defaultHeight;
			SpriteScale = Matrix.CreateScale(screenscale, heightscale, 1);
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

			Game1.graphics.GraphicsDevice.Clear (Color.Black);

			//spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.NonPremultiplied);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, SpriteScale);

			//TODO: Add your drawing code here


			//so draw your objects etc
			gameManager.Draw (timeDelta);
			/// draw in the playstate manager
           

			spriteBatch.End();
			base.Draw(gameTime);

		}

		
		public void Quit()
		{
			this.Exit();
		}

		void GraphicsDevice_DeviceReset(object sender, EventArgs e)
		{
			//scale sprites up or down based on current viewport
			float screenscale = (float)graphics.GraphicsDevice.Viewport.Width / defaultWidth;

			//create the scale transformation for draw
			//do not scale the sprite depts (z = 1)
			SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
		}
	}

	public class StoryProgress
	{
		public const int i_NumberOfEndings = 6;
		private int	i_TUNNEL,i_SUICIDE,	i_FOODTRUCK,i_RAPE,i_INSANE,i_SLAYER;
		public int[] Stats;
		public enum enum_EndingProgress
		{
			NONE,
			TUNNEL,
			SUICIDE,
			FOODTRUCK,
			RAPE,
			INSANE,
			SLAYER
		};
		public enum_EndingProgress enum_EndingProgressThis;
		public StoryProgress()
		{

			enum_EndingProgressThis = new enum_EndingProgress();
			Stats = new int[i_NumberOfEndings];
		}

		public int Tunnel
		{
			set {Stats[0]=value; }
			get {return Stats[0];}
		}
		public int Suicide
		{
			set {Stats[1]=value; }
			get {return Stats[1];}
		}
		public int Foodtruck
		{
			set {Stats[2]=value; }
			get {return Stats[2];}
		}
		public int Rape
		{
			set {Stats[3]=value; }
			get {return Stats[3];}
		}
		public int Insane
		{
			set {Stats[4]=value; }
			get {return Stats[4];}
		}
		public int Slayer
		{
			set {Stats[5]=value; }
			get {return Stats[5];}
		}


	};
}