using System;
using System.Collections.Generic;
namespace OpenglTester
{
	public class Level
	{

		// contains a few objects(sprites ) for interaction ?

		private int i_NumberOfExits;
		private String[] str_Exits;

		private int i_NumberOfIntObjects;
		private List<Object> obj_InteractableObjects;

		public Level (int numExits,String[] strExits ,int numObjects, List<Object> intObjects)
		{
			i_NumberOfExits = numExits;
			str_Exits = strExits;
			i_NumberOfIntObjects = numObjects;
			obj_InteractableObjects = intObjects;
		}
	}
}

