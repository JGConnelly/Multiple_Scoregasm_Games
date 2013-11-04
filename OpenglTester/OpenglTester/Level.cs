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

		public Level ()
		{
			obj_InteractableObjects = new List<Object>{};
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
	}
}

