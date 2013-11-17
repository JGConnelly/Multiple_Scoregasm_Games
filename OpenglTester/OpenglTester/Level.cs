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


		private List<Object> obj_InteractableObjects;
		private List<AI> ai_Characters;

		private SpriteFont TheFont;
		private const string FontUsed = "FontyFony";

		public Level ()
		{
			obj_InteractableObjects = new List<Object>{};
			str_Exits = new List<string>{};
			ai_Characters = new List<AI>{};

			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
		}
		public Level ( List<String> Exits , List<Object> intObjects)
		{

			str_Exits = Exits;

			obj_InteractableObjects = intObjects;
			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
		}
		public void SetImage(string src)
		{
			obj_ThisObject = new Object(src);
			obj_ThisObject.GenerateAlpha();
		}
		public void AddObject(Object intObj)
		{
			obj_InteractableObjects.Add(intObj);

		}
		public void AddExit(Object intObj, string exit)
		{
			obj_InteractableObjects.Add(intObj);
			str_Exits.Add(exit);
		}
		public void AddCharacter(AI character)
		{
			ai_Characters.Add(character);
		}


		public void Update(float DeltaT)
		{
			obj_ThisObject.Update(DeltaT);

			for (int obj = 0; obj < obj_InteractableObjects.Count;obj++)
			{
				obj_InteractableObjects[obj].Update(DeltaT);
			}
		}
		public void Draw()
		{
			//draw interactable objects
			//draws the levels background first
			obj_ThisObject.Draw();
			for (int obj = 0; obj < obj_InteractableObjects.Count;obj++)
			{
				obj_InteractableObjects[obj].Draw();
			}

			// draw ais / characters

			for (int ais = 0; ais < ai_Characters.Count;ais++)
			{
				ai_Characters[ais].Draw();
			}

			/// TEXT
			DrawText("lel lel",new Vector2(1000,750));

		}
		public void DrawText(String wuds,Vector2 pos)
		{
			Game1.spriteBatch.DrawString(TheFont,wuds,pos,Color.White);
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

