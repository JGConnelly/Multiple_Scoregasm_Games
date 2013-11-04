using System;
using System.Collections.Generic;
namespace OpenglTester
{
	public class Level
	{

		// contains a few objects(sprites ) for interaction ?
		Object obj_ThisObject;
		private int i_NumberOfExits;
		private String[] str_Exits;


		private List<Object> obj_InteractableObjects;

		public Level ()
		{
			obj_InteractableObjects = new List<Object>{};
		}
		public Level (int numExits,String[] strExits , List<Object> intObjects)
		{
			i_NumberOfExits = numExits;
			str_Exits = strExits;

			obj_InteractableObjects = intObjects;
		}
		public void SetImage(string src)
		{
			obj_ThisObject = new Object(src);
		}
		public void AddObject(Object intObj)
		{
			obj_InteractableObjects.Add(intObj);

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

