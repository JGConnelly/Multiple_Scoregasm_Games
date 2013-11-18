using System;



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace OpenglTester
{
	public class InteractableObject : Object
	{
		private bool b_isExit;
		private string str_Exit; // the actual file that is the exit
		public enum Action
		{
			Nothing,
			EquipShiv

		};
		public Action act_Action;
		public InteractableObject (string imagePath, string ex = ""):base(imagePath)
		{
			b_isExit = false;
			if (ex != "") {
				str_Exit = ex;
				b_isExit = true;
			}

			act_Action = Action.Nothing;
		}


		public void SetIsExit(bool exit, string ex = "")
		{
			str_Exit = ex;
			b_isExit = exit;
		}

		public override void Update (float DeltaTime)
		{
			if (CheckNearCollision (PlayState.player) ) 
			{
				if(InputHandler.confirmPressed&&b_isExit)
				{
					PlayState.GetInstance().CurrentLevel = null;
					PlayState.GetInstance().CurrentLevel = PlayState.GetInstance().fileManager.LoadLevel(str_Exit);
				}
			}

		}

		virtual public bool CheckNearCollision (Object other)
		{
			Rectangle thisObjectRect = new Rectangle((int)v2_Position.X,(int)v2_Position.Y,(int)v2_Size.X + 10,(int)v2_Size.Y);
			Rectangle otherRect = new Rectangle((int)other.Position.X,(int)other.Position.Y,(int)other.Size.X + 10,(int)other.Size.Y);
			
			if(thisObjectRect.Intersects(otherRect) || otherRect.Intersects(thisObjectRect))
				return true;
			else 
				return false;
		}

	}
}

