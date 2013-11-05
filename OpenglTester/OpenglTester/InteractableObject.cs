using System;

namespace OpenglTester
{
	public class InteractableObject : Object
	{
		private bool b_isExit;
		public enum Action
		{
			Nothing,
			EquipShiv

		};
		public Action act_Action;
		public InteractableObject (string imagePath):base(imagePath)
		{
			b_isExit = false;
			act_Action = Action.Nothing;
		}

		public void SetIsExit(bool exit)
		{
			b_isExit = exit;
		}

	}
}

