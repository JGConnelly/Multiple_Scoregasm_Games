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
		public int Frames;
		public float TimeForCompletion;
		public AnimationInfo (int a, int b, float c)
		{
			Start = a; Frames = b; TimeForCompletion = c;
		}
	};
	public class Player : Object
	{
		#region Class Members
		enum Action
		{
			idle,
			walk,
			run,
			crouch,
			sneak,
			jump,
			jumpland,
			punch,
			shiv,
			block
		};
		
		AnimationInfo Idle = new AnimationInfo(0,1,1), Walk = new AnimationInfo(17,6,3), 
					   Run = new AnimationInfo(1,16,2), Punch = new AnimationInfo(23,6,1),
						Sneak = new AnimationInfo(30,6,4),Crouch = new AnimationInfo(29,1,1),
						 Shiv = new AnimationInfo(36,5,0.5f),JumpLand = new AnimationInfo(41,0,2),
						  Jumping = new AnimationInfo(42,5,1), Block = new AnimationInfo(47,1,1.5f);
		AnimationInfo CurrentAnimation = new AnimationInfo(0,1,1);
		bool ShivEquipped = false;
		public bool ShivFound = true;
		bool isJumping;
		float Mass = 250;
		public float WalkSpeed = 20, RunSpeed = 250;
		float GroundWhileJumping;
		Action CurrentAction = Action.idle;//This will need to be accessed by other classes when in combat
		Action LastAction = Action.idle;
		#endregion

		readonly Vector2 gravity = new Vector2(0,9.8f);
		Vector2 velocity;

		public Player(string imagePath , int numberOfFrames , int timeToComplete,float frameSize):base ( imagePath , numberOfFrames ,  timeToComplete, frameSize)
		{
			b_IsAnimated = true;
			SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			GenerateAlpha();
			Scale = new Vector2 (4,4);
		}
		// 
	
		/// <summary>
		/// Update the player class.
		/// </summary>
		/// <param name='Elapsed'>
		/// Elapsed time since last frame
		/// </param>
		/// 
		public void Update (float Elapsed)
		{
			// CurrentAction - Work it out based on input
			LastAction = CurrentAction;
			if (InputHandler.upPressed && !isJumping) 
			{
				isJumping = true;
				GroundWhileJumping = v2_Position.Y;
				velocity.Y = -8;
				v2_Position.Y -=5;

				CurrentAction = Action.jumpland;
			}
			else if (isJumping)
			{
				float time = (float) Elapsed;
				Vector2 Acceleration;
				Acceleration = ((velocity)/Mass + gravity);
				//velocity+= gravity*time;
				velocity += Acceleration * Elapsed;
				v2_Position.Y+=velocity.Y;//*time;
				CurrentAction = Action.jump;
				if(InputHandler.rightPressed)
				{
					b_FlipImage = false;
					v2_Position.X+=100*Elapsed;

				}
				else if (InputHandler.leftPressed)
				{
					b_FlipImage = true;

					v2_Position.X-=100*Elapsed;

				}
				if(v2_Position.Y >= GroundWhileJumping-5)
				{
					v2_Position.Y = GroundWhileJumping;
					velocity = Vector2.Zero;
					isJumping = false;
					CurrentAction = Action.jumpland;
				}
			}
			else if (InputHandler.downPressed) 
			{
				CurrentAction = Action.crouch;
				if(InputHandler.punchPressed)
				{
					CurrentAction = Action.block;
				}
				else if(InputHandler.leftPressed || InputHandler.rightPressed)
				{
					CurrentAction = Action.sneak;
					if(InputHandler.rightPressed)
					{
						b_FlipImage = false;
						v2_Position.X+=5*Elapsed;

					}
					else if (InputHandler.leftPressed)
					{
						b_FlipImage = true;
						v2_Position.X-=5*Elapsed;

					}
				}
			}
			//determine facing, as well as whether running or not.
			else if (InputHandler.leftPressed || InputHandler.rightPressed)
			{
				CurrentAction = Action.walk;
				if (InputHandler.sprintPressed)
				{
					CurrentAction = Action.run;
				}
				if(InputHandler.rightPressed)
				{
					b_FlipImage = false;
					
					if(CurrentAction == Action.run)
					{
						v2_Position.X+=RunSpeed*Elapsed;
					}
					else
					{
						v2_Position.X+=WalkSpeed*Elapsed;
					}
				}
				else if (InputHandler.leftPressed)
				{
					b_FlipImage = true;
					
					if(CurrentAction == Action.run)
					{
						v2_Position.X-=RunSpeed*Elapsed;
					}
					else
					{
						v2_Position.X-=WalkSpeed*Elapsed;
					}
				}
				
			}
			else if(InputHandler.punchPressed)
			{
				if (InputHandler.downPressed)
					CurrentAction = Action.block;
				else if(ShivEquipped)
					CurrentAction = Action.shiv;
				else 
					CurrentAction = Action.punch;
			}
			else if (InputHandler.switchPressed && ShivFound)
			{
				if(ShivEquipped)
					ShivEquipped = false;
				else 
					ShivEquipped = true;
			}
			/*//Work out if jumping
			//Also need to work out if grounded
			else if()
			{
				//if right key or left key is also down, move in that direction midair
			}*/
			else
			{
				CurrentAction = Action.idle;
			}
			switch (CurrentAction)
			{
			case Action.idle:
				CurrentAnimation = Idle;
				break;
			case Action.walk:
				CurrentAnimation = Walk;
				break;
			case Action.run:
				CurrentAnimation = Run;
				break;
			case Action.crouch:
				CurrentAnimation = Crouch;
				break;
			case Action.sneak:
				CurrentAnimation = Sneak;
				break;
			case Action.punch:
				CurrentAnimation = Punch;
				break;
			case Action.shiv:
				CurrentAnimation = Shiv;
				break;
			case Action.jump:
				CurrentAnimation = Jumping;
				break;
			case Action.jumpland:
				CurrentAnimation = JumpLand;
				break;
			case Action.block:
				CurrentAnimation = Block;
				break;
			default:
				CurrentAnimation = Idle;
				break;
			}
			//Update the current animation. if the animation is different from the last animation, change the animation
			if(LastAction != CurrentAction)
			{
				SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			}


			base.Update(Elapsed);
		}		
	}
}

