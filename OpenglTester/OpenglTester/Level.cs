using System;
using System.Collections.Generic;
namespace OpenglTester
{
	public class Level
	{

		// contains a few objects(sprites ) for interaction ?
		Object obj_ThisObject;

		private List<String> str_Exits;


		private List<Object> obj_InteractableObjects;
		private List<AI> ai_Characters;

		public Level ()
		{
			obj_InteractableObjects = new List<Object>{};
			str_Exits = new List<string>{};
			ai_Characters = new List<AI>{};
		}
		public Level ( List<String> Exits , List<Object> intObjects)
		{

			str_Exits = Exits;

			obj_InteractableObjects = intObjects;
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
			obj_ThisObject.Draw();
			for (int obj = 0; obj < obj_InteractableObjects.Count;obj++)
			{
				obj_InteractableObjects[obj].Draw();
			}
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
				if(check.CheckCollision(obj_InteractableObjects[obj]))
				{
					return true;
				}
			}
			return false;

		}


	}
}

