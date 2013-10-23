#region Usings
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
#endregion
namespace OpenglTester
{
	public struct AnimationInfo
	{
		public int Start;
		public int End;
		public int TimeForCompletion;
	};
	public class Player : Object
	{
		#region Class Members
		enum Action
		{
			idle,
			walk,
			run,
			sneak,
			jump,
			punch
		};

		AnimationInfo Idle = {0,1,1}, Walk = {4,4,4};
		Action CurrentAction = Action.idle;
		Action LastAction = Action.idle;
		#endregion

		public Player(string imagePath , GraphicsDeviceManager gdevman , ContentManager contentManager):base ( imagePath , gdevman, contentManager)
		{			
			b_IsAnimated = true;
		}

		public void Update (float Elapsed)
		{
			// CurrentAction - Work it out based on input
			LastAction = CurrentAction;
			if (CurrentAction == Action.idle) 
			{
				SetAnimationStartPoint(Idle.Start,Idle.End-Idle.Start,Idle.TimeForCompletion);
			}
			else if(CurrentAction == Action.walk)
			{
				SetAnimationStartPoint(Walk.Start,Walk.End-Walk.Start,Walk.TimeForCompletion);
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Right) )
			{
				CurrentAction = Action.walk;
			};

			base.Update(Elapsed);
		}



	}
}

