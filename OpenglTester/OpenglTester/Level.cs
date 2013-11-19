using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace OpenglTester
{
	public class Level
	{

		// contains a few objects(sprites ) for interaction ?
		Object obj_ThisObject;

		private List<String> str_Exits;


		private List<InteractableObject> obj_InteractableObjects;
		private List<AI> ai_Characters;

		private SpriteFont TheFont;
		private const string FontUsed = "FontyFony";

		private class TextToScreen
		{
			public string theText;
			public float StartTime;
			public float TimeOnScreen;
			public Color theColor;
			public Vector2 Position;
			public TextToScreen()
			{
				theText = "";
				StartTime = 0;
				TimeOnScreen = 0;
				theColor = Color.White;
				Position = new Vector2 (0,0);
			}
			public TextToScreen(string text, float time, Vector2 pos, Color col)
			{
				theText = text;
				StartTime = time;
				TimeOnScreen = time;
				theColor = col;
				Position = pos;
			}
		}
		private List<TextToScreen> OnScreenWords;
		// progression stats
		//private int i_
		public Level ()
		{
			obj_InteractableObjects = new List<InteractableObject>{};
			str_Exits = new List<string>{};
			ai_Characters = new List<AI>{};
			OnScreenWords = new List<TextToScreen>();
			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
		}
		public Level ( List<String> Exits , List<InteractableObject> intObjects)
		{

			str_Exits = Exits;
			OnScreenWords = new List<TextToScreen>();
			obj_InteractableObjects = intObjects;
			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
		}
		public void SetImage(string src)
		{
			obj_ThisObject = new Object(src);
			//obj_ThisObject.GenerateAlpha();
		}
		public void AddObject(InteractableObject intObj)
		{
			obj_InteractableObjects.Add(intObj);

		}
		/// <summary>
		/// Adds the exit.
		/// </summary>
		/// <param name='intObj'>
		/// Int object.
		/// </param>
		/// <param name='exit'>
		/// Exit.
		/// </param>
		/// <param name='newRoomPos'>
		/// New room position. so the position the player will be in once they enter the new room
		/// </param>
		public void AddExit(InteractableObject intObj,Vector2 newRoomPos, string exit)
		{
			intObj.SetIsExit(true,newRoomPos,exit);
			obj_InteractableObjects.Add(intObj);
			str_Exits.Add(exit);
		}
		public void AddCharacter(AI character)
		{
			ai_Characters.Add(character);
		}


		public void Update (float DeltaT)
		{
			obj_ThisObject.Update (DeltaT);

			for (int obj = 0; obj < obj_InteractableObjects.Count; obj++) {
				obj_InteractableObjects [obj].Update (DeltaT);
			}

			//ai
			for (int ais = 0; ais < ai_Characters.Count; ais++) {
				ai_Characters [ais].Update (DeltaT);
			}

			//text
			for (int t = 0; t < OnScreenWords.Count; t++) 
			{
				OnScreenWords[t].TimeOnScreen -= DeltaT;
			}
		}
		public void Draw ()
		{

			//draw interactable objects
			//draws the levels background first
			obj_ThisObject.Draw ();

			/// TEXT
			//DrawText("lel lel",new Vector2(1000,750),Color.White);
			for (int t = 0; t < OnScreenWords.Count; t++) {
				if (OnScreenWords [t].TimeOnScreen < 0)
					OnScreenWords.RemoveAt (t);
				else {
					Game1.spriteBatch.DrawString (TheFont, OnScreenWords [t].theText, OnScreenWords [t].Position, OnScreenWords [t].theColor);
					
				}
				
			}
			if (OnScreenWords.Count == 0) {
				int derp = 5;
				derp++;
			}

			for (int obj = 0; obj < obj_InteractableObjects.Count; obj++) {
				obj_InteractableObjects [obj].Draw ();
			}

			// draw ais / characters

			for (int ais = 0; ais < ai_Characters.Count; ais++) {
				ai_Characters [ais].Draw ();
			}



		}
		public void DrawText (String words, Vector2 pos, Color col , float time = 2f)
		{
			bool canAdd = true;
			TextToScreen temp = new TextToScreen(words,time,pos,col);

			// ensure there are no doubles
			for (int t = 0; t < OnScreenWords.Count; t++) 
			{
				if(temp.Position==OnScreenWords[t].Position&&temp.theText==OnScreenWords[t].theText )
				{
					OnScreenWords[t].TimeOnScreen = OnScreenWords[t].StartTime;
					canAdd = false;
				}
			}
			if(canAdd)
				OnScreenWords.Add(new TextToScreen(words,time,pos,col));	
		}

		/// <summary>
		/// Checks the can interact with a
		/// </summary>
		/// <param name='check'>
		/// Check that whatever you pass through if its close enough to interact
		/// basically check if it collides
		/// </param>
		public bool CheckCanInteract(Object check)
		{
			
			for (int obj = 0; obj < obj_InteractableObjects.Count;obj++)
			{
				if(check.CheckNearCollision(obj_InteractableObjects[obj]))
				{
					return true;
				}
			}
			return false;

		}

	}
}

