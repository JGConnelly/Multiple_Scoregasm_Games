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
			punch,
			shiv
		};
		
		AnimationInfo Idle = new AnimationInfo(0,1,1), Walk = new AnimationInfo(17,6,4), 
					   Run = new AnimationInfo(1,16,2), Punch = new AnimationInfo(23,6,1),
						Sneak = new AnimationInfo(30,6,4),Crouch = new AnimationInfo(29,1,1),
						Shiv = new AnimationInfo(36,5,0.5f);
		AnimationInfo CurrentAnimation = new AnimationInfo(0,1,1);
		bool ShivEquipped = false;
		bool ShivFound = true;
		Action CurrentAction = Action.idle;
		Action LastAction = Action.idle;
#endregion
		
		public Player(string imagePath ,int numberOfFrames , int timeToComplete,float frameSize):base ( imagePath ,  numberOfFrames ,  timeToComplete, frameSize)
		{
			b_IsAnimated = true;
			SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			GenerateAlpha();
			Scale = new Vector2 (4,4);
		}
		
		public void Update (float Elapsed)
		{
			// CurrentAction - Work it out based on input
			LastAction = CurrentAction;
			
			//determine facing, as well as whether running or not.
			if (InputHandler.downPressed) 
			{
				CurrentAction = Action.crouch;
				if(InputHandler.leftPressed || InputHandler.rightPressed)
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
						v2_Position.X+=200*Elapsed;
					}
					else
					{
						v2_Position.X+=8*Elapsed;
					}
				}
				else if (InputHandler.leftPressed)
				{
					b_FlipImage = true;
					
					if(CurrentAction == Action.run)
					{
						v2_Position.X-=200*Elapsed;
					}
					else
					{
						v2_Position.X-=8*Elapsed;
					}
				}
				
			}
			else if(InputHandler.punchPressed)
			{
				if(ShivEquipped)
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
		public void Draw ()
		{
			/*if (b_IsAnimated) {
				// only using horizontal animation right now 
				/// if someone really needs vertical i can change it
				
				// anyway.. this creates the rectangle that the animation will use
				Rectangle AnimSourceRect = new Rectangle((int)((f_FrameWidth * i_CurrentFrame)+i_StartFrame*f_FrameWidth), 0,
				                                         (int)f_FrameWidth, tex_Image.Height);
				
				spriteBatch.Draw(tex_Image, v2_Position, AnimSourceRect, Color.White,
				                 f_Rotation, new Vector2(0,0), 1, SpriteEffects.None, 0f);
				Console.WriteLine("Current Animation State: " + CurrentAction.ToString());
				
			} 
			else 
			{
				spriteBatch.Draw (tex_Image, v2_Position, null, Color.White, f_Rotation, new Vector2 (0, 0), 1f, SpriteEffects.None, 0f);
			}
			*/
			base.Draw();
		}
		
		
		
	}
}

