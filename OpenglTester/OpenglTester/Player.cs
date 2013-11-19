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
		/// <summary>
		/// Initializes a new instance of the <see cref="OpenglTester.AnimationInfo"/> struct.
		/// </summary>
		/// <param name='NewStart'>
		/// New start.
		/// </param>
		/// <param name='NewFrames'>
		/// New frames.
		/// </param>
		/// <param name='NewTimeForCompletion'>
		/// New time for completion.
		/// </param>
		public AnimationInfo (int NewStart, int NewFrames, float NewTimeForCompletion)
		{
			Start = NewStart; Frames = NewFrames; TimeForCompletion = NewTimeForCompletion;
		}
	};
	public class Player : Object
	{
		#region Class Members
		public enum Action
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
					   Run = new AnimationInfo(1,16,1.2f), Punch = new AnimationInfo(23,6,1),
						Sneak = new AnimationInfo(30,6,4),Crouch = new AnimationInfo(29,1,1),
						 Shiv = new AnimationInfo(36,5,0.5f),JumpLand = new AnimationInfo(41,0,2),
						  Jumping = new AnimationInfo(42,5,1), Block = new AnimationInfo(47,1,1.5f);
		AnimationInfo CurrentAnimation = new AnimationInfo(0,1,1);
		bool ShivEquipped = false;
		public bool ShivFound = true;
		bool isJumping;
		bool isPunching = false;
		bool wasSwap;
		float PunchTiming = 1f;
		float Mass = 250;
		public float WalkSpeed = 40, RunSpeed = 250;
		float GroundWhileJumping;
		Action CurrentAction = Action.idle;//This will need to be accessed by other classes when in combat
		Action LastAction = Action.idle;
		Texture2D Casual, Prison;
		bool PrisonOutfit = false;
		#endregion

		readonly Vector2 gravity = new Vector2(0,9.8f);
		Vector2 velocity;

		public Player(string image1Path,string image2Path , int numberOfFrames , int timeToComplete,float frameSize):base ( image1Path , numberOfFrames ,  timeToComplete, frameSize)
		{
			b_IsAnimated = true;
			SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			Casual = Game1.contentManager.Load<Texture2D>(image2Path);
			Prison = tex_Image;

			//GenerateAlpha();
			//Scale = new Vector2 (4,4);
		}

		/// <summary>
		/// Update the player class.
		/// </summary>
		/// <param name='Elapsed'>
		/// Elapsed time since last frame
		/// </param>
		/// 
		public override void Update (float Elapsed)
		{
			if (PrisonOutfit) {
				tex_Image = Prison;
			} 
			else 
			{
				tex_Image = Casual;
			}
			// CurrentAction - Work it out based on input
			if (InputHandler.switchPressed && ShivFound &&!wasSwap)
			{
				if(ShivEquipped)
					ShivEquipped = false;
				else 
					ShivEquipped = true;

				wasSwap = true;
			}
			wasSwap = InputHandler.switchPressed;
			LastAction = CurrentAction;
			if (InputHandler.upPressed && !isJumping) 
			{
				isJumping = true;
				isPunching = false;
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
					if(InputHandler.sprintPressed)
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

					if(InputHandler.sprintPressed)
					{
						v2_Position.X-=RunSpeed*Elapsed;
					}
					else
					{
						v2_Position.X-=WalkSpeed*Elapsed;
					}

				}
				if(v2_Position.Y >= GroundWhileJumping-5)
				{
					v2_Position.Y = GroundWhileJumping;
					velocity = Vector2.Zero;
					isJumping = false;
					CurrentAction = Action.jumpland;
				}
			}
			else if (isPunching)
			{
				PunchTiming-=Elapsed;
				if(PunchTiming <=0)
				{
					isPunching = false;
				}
			}
			else if(InputHandler.punchPressed&&!isPunching)
			{
				if (InputHandler.downPressed)
					CurrentAction = Action.block;
				else if(ShivEquipped)
				{
					CurrentAction = Action.shiv;CurrentAnimation = Shiv;
					isPunching = true;
				}
				else 
				{
					CurrentAction = Action.punch;CurrentAnimation = Punch;
					isPunching = true;
				}
				PunchTiming = CurrentAnimation.TimeForCompletion;
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

		public Action GetCurrentAction()
		{
			return CurrentAction;
		}
	}
}

