//AI.cs
//Tash
//Controls the behaviour of NPCs


using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace OpenglTester
{
	public class AI : Object
	{
		enum Action
		{
			stand,
			idle,
			walk,
			run,
			crouch,
			sneak,
			jump,
			jumpland,
			punch,
			shiv,
			block,
			fall,
			fallen,
			talk
		};

		private int i_PlayerDisposition;
		private bool b_IsGuard;
		private int i_HitPoints;
		private float f_HitSpeed;
		private float f_HitDamage;
		private float f_DodgeSpeed;
		private float f_BlockSpeed;
		private Action CurrentAction;
		private Action LastAction;
		private bool b_IsHooker;

		// text stahhhff
		bool b_CanDrawText;
		string str_TextToDraw;
		string str_ResponsesToDraw;

		//animation parameters for the AIs
		AnimationInfo Idle = new AnimationInfo(0,1,1), Walk = new AnimationInfo(17,6,4), 
						Run = new AnimationInfo(1,16,2), Punch = new AnimationInfo(23,6,1),
							Sneak = new AnimationInfo(30,6,4),Crouch = new AnimationInfo(29,1,1),
								Shiv = new AnimationInfo(36,5,0.5f),JumpLand = new AnimationInfo(41,0,2),
									Jumping = new AnimationInfo(42,5,1), Block = new AnimationInfo(47,1,1.5f);

		//the following animationInfos are only used for the prostitute, as she has different actions to the other npcs.
		AnimationInfo pFall = new AnimationInfo(8, 3, 1), pIdle = new AnimationInfo(1, 4, 4), 
						pStand = new AnimationInfo(0, 1, 1), pTalk = new AnimationInfo(5, 3, 2), 
							pFallen = new AnimationInfo(10, 1, 1);

		AnimationInfo CurrentAnimation;

		private string str_NoDialogueLine;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenglTester.AI"/> class.
		/// </summary>
		/// <param name='imagePath'>
		/// Image path.
		/// </param>
		/// <param name='numberOfFrames'>
		/// Number of frames.
		/// </param>
		/// <param name='timeToComplete'>
		/// Time to complete.
		/// </param>
		/// <param name='frameSize'>
		/// Frame size.
		/// </param>
		/// <param name='a_isHooker'>
		/// If set to <c>true</c> is hooker.
		/// </param>
		public AI(string imagePath , int numberOfFrames , int timeToComplete,float frameSize, bool a_isHooker = false):base ( imagePath , numberOfFrames ,  timeToComplete, frameSize)
		{
			b_IsHooker = a_isHooker;
			b_IsAnimated = true;
			b_CanDrawText = false;
			str_TextToDraw = "";
			str_ResponsesToDraw = "";
			CurrentAction = Action.idle;
			DetermineAnimation();
			SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			Dialogues = new List<Dialogue>();
			str_NoDialogueLine = "Go away";
			//GenerateAlpha();
			//Scale = new Vector2 (4,4);
		}

		///
		///The Dialogue class
		/// A nested class inside the AI class
		/// 
		public class Dialogue
		{
			// the pre req for each dialogue options
			public StoryProgress EndProgressPreReq;
			public int i_PlayerPreReq;

			//what the character/ ai says
			public string Statement;

			// the responses and stats associated
			// so both lists should be of equal size
			public List<string> TheResponses;
			public List<StoryProgress> ResponseEndingProg;
			public List<int> ResponseEndingPlayerStat;



			public Dialogue()
			{
				EndProgressPreReq= new StoryProgress();
				TheResponses = new List<string>();
				ResponseEndingProg = new List<StoryProgress>();
				ResponseEndingPlayerStat = new List<int>();
			}


		};
		public List<Dialogue> Dialogues;

		public AI(string imagePath ):base ( imagePath )
		{
			Dialogues = new List<Dialogue>();
			str_NoDialogueLine = "Go away";
		}


		//accessors and mutators
		public int PlayerDisposition
		{
			set {i_PlayerDisposition=value; }
			get {return i_PlayerDisposition;}
		}
		public bool IsGuard
		{
			set {b_IsGuard=value; }
			get {return b_IsGuard;}
		}
		public int HitPoints
		{
			set {i_HitPoints=value; }
			get {return i_HitPoints;}
		}
		public float HitSpeed
		{
			set {f_HitSpeed=value; }
			get {return f_HitSpeed;}
		}
		public float HitDamage
		{
			set {f_HitDamage=value; }
			get {return f_HitDamage;}
		}
		public float DodgeSpeed
		{
			set {f_DodgeSpeed=value; }
			get {return f_DodgeSpeed;}
		}
		public float BlockSpeed
		{
			set {f_BlockSpeed=value; }
			get {return f_BlockSpeed;}
		}
		public string NoDialogueLine
		{
			set {str_NoDialogueLine=value; }
			get {return str_NoDialogueLine;}

		}
		public bool IsHooker 
		{
			set { b_IsHooker = value;}
			get { return b_IsHooker;}
		}
		//end of accesors and mutators

		/// <summary>
		/// Update the specified DeltaTime.
		/// </summary>
		/// <param name='DeltaTime'>
		/// Delta time.
		/// </param>
		public override void Update(float DeltaTime)
		{
			DetermineAction(DeltaTime);
			DetermineAnimation();
			
			base.Update(DeltaTime);
		}
		/// <summary>
		/// Draw this instance.
		/// </summary>
		public override void Draw ()
		{
			base.Draw ();

		}

		/// <summary>
		/// Sets the action to block.
		/// </summary>
		public void ActionBlock ()
		{
			CurrentAction = Action.block;
		}
		/// <summary>
		/// Sets the action to crouch.
		/// </summary>
		public void ActionCrouch ()
		{
			CurrentAction = Action.crouch;
		}
		/// <summary>
		/// Sets the action to idle.
		/// </summary>
		public void ActionIdle ()
		{
			CurrentAction = Action.idle;
		}
		/// <summary>
		/// Sets the action to jump.
		/// </summary>
		public void ActionJump ()
		{
			CurrentAction = Action.jump;
		}
		/// <summary>
		/// Sets the action to punch.
		/// </summary>
		public void ActionPunch()
		{
			CurrentAction = Action.punch;
		}
		/// <summary>
		/// Sets the action to shiv.
		/// </summary>
		public void ActionShiv ()
		{
			CurrentAction = Action.shiv;
		}
		/// <summary>
		/// Sets the action to fall.
		/// </summary>
		public void ActionFall()
		{
			CurrentAction = Action.fall;
		}
		/// <summary>
		/// Sets the action to run.
		/// </summary>
		public void ActionRun ()
		{
			CurrentAction = Action.run;
		}
		/// <summary>
		/// Sets the action to walk.
		/// </summary>
		public void ActionWalk ()
		{
			CurrentAction = Action.walk;
		}
		/// <summary>
		/// Sets the action to Talk.
		/// </summary>
		public void ActionTalk()
		{
			CurrentAction = Action.talk;
		}
		/// <summary>
		/// Sets the action to sneak.
		/// </summary>
		public void ActionSneak()
		{
			CurrentAction = Action.sneak;
		}

		public void WalkTo (float xPosition)
		{
			
			//if destination is on the left of the AI
			//turn left, walk to it, until it reaches or is on the left of the destination
			//then change state to idle
			if (xPosition < v2_Position.X) {
				b_FlipImage = true;
				CurrentAction = Action.walk;
				if (v2_Position.X <= xPosition)
				{
					CurrentAction = Action.idle;
				}
			}
			//if destination is on the right of the AI
			//turn right, walk to it, until it reaches or is on the left of the destination
			//then change state to idle
			if (xPosition > v2_Position.X) {
				b_FlipImage = false;
				CurrentAction = Action.walk;
				if (v2_Position.X >= xPosition)
				{
					CurrentAction = Action.idle;
				}
			}
		}

		/// <summary>
		/// Determines the animation appropriate for the currentAction. Uses a switch as a crude state manager.
		/// </summary>
		void DetermineAnimation()
		{

			switch (CurrentAction)
			{
			case Action.idle:
				if (b_IsHooker)
				{
					CurrentAnimation = pIdle;
				}
				else
				{
					CurrentAnimation = Idle;
				}
				break;
			case Action.walk:
				CurrentAnimation = Walk;
				//keep moving until hit edge of screen
				if ((v2_Position.X > 64) && (v2_Position.X < 1920 - 64))
				{
					if (b_FlipImage)
					{
						v2_Position.X--;
					}
					else
					{
						v2_Position.X++;
					}
				}
				else //if hit edge of screen, turn around
					b_FlipImage = (b_FlipImage) ? false : true;
				break;
			case Action.run:
				CurrentAnimation = Run;
				//keep moving until hit edge of screen
				if ((v2_Position.X > 64) && (v2_Position.X < 1920 - 64))
				{
					if (b_FlipImage)
					{
						v2_Position.X -= 2;
					}
					else
					{
						v2_Position.X += 2;
					}
				}
				else //if hit edge of screen, turn around
					b_FlipImage = (b_FlipImage) ? false : true;
				break;
			case Action.crouch:
				CurrentAnimation = Crouch;
				break;
			case Action.sneak:
				CurrentAnimation = Sneak;
				break;
			case Action.punch:
				CurrentAnimation = Punch;
				//when to stop punching? after 1 punch?
				break;
			case Action.shiv:
				CurrentAnimation = Shiv;
				//when to stop shivving? ater 1 shiv?
				break;
			case Action.jump:
				CurrentAnimation = Jumping;
				//has to actually jump up
				//has to swap to jumpland after how long? when Y = floor level?
				break;
			case Action.jumpland:
				CurrentAnimation = JumpLand;
				break;
			case Action.block:
				CurrentAnimation = Block;
				break;
			case Action.talk:
				if (b_IsHooker)
				{
					CurrentAnimation = pTalk;
				}
				else
					CurrentAnimation = Idle; //non-prostitute AI has no talk animation, so will just stand still
				break;
			case Action.fall:
				if (b_IsHooker)
				{
					CurrentAnimation = pFall;
					if (PlayState.player.Position.X < Position.X)
					{
						v2_Position.X++;
					}
					else
					{
						v2_Position.X--;
					}
				}
				else
					CurrentAnimation = Block;
				break;
			case Action.fallen:
				if (b_IsHooker)
				{
					CurrentAnimation = pFallen;
				}
				break;
			default:
				if (b_IsHooker)
				{
					CurrentAnimation = pIdle;
				}
				else
				{
					CurrentAnimation = Idle;
				}
				break;
			}
			//Update the current animation. if the animation is different from the last animation, change the animation
			if(LastAction != CurrentAction)
			{
				SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			}
		}

		/// <summary>
		/// Determines the action the AI should be doing based on the player's interaction, or based on previous states
		/// </summary>
		/// <param name='dT'>
		/// delta time
		/// </param>
		void DetermineAction(float dT)
		{
			LastAction = CurrentAction;
			//is the ai near the player?
			if(CheckNearCollision(PlayState.player) && (CurrentAction != Action.fallen)) //do nothing if AI is dead
			{
				CurrentAction = Action.talk;
				if(PlayState.player.Position.X < Position.X )
					b_FlipImage = true;
				else
					b_FlipImage = false;

				// do text
				if(Dialogues.Count >0)
				{
					b_CanDrawText = true;
					str_TextToDraw = Dialogues[0].Statement;
					str_ResponsesToDraw = null;
					for(int r = 0; r < Dialogues[0].TheResponses.Count; r++)
					{
						str_ResponsesToDraw += Convert.ToString(r+1)+". "+ Dialogues[0].TheResponses[r]+"\n";
					}

					// if the player selects an option
					if(InputHandler.dlg1Pressed && Dialogues[0].TheResponses.Count>0)
					{
						str_TextToDraw = Dialogues[0].TheResponses[0];
						Dialogues.RemoveAt(0);
					}
					else if(InputHandler.dlg2Pressed&& Dialogues[0].TheResponses.Count>1){
						str_TextToDraw =  Dialogues[0].TheResponses[1];
						Dialogues.RemoveAt(0);
					}
					else if(InputHandler.dlg3Pressed&& Dialogues[0].TheResponses.Count>2){
						str_TextToDraw = Dialogues[0].TheResponses[2];
						Dialogues.RemoveAt(0);
					}
					else if(InputHandler.dlg4Pressed&& Dialogues[0].TheResponses.Count>3){
						str_TextToDraw = Dialogues[0].TheResponses[3];
						Dialogues.RemoveAt(0);
					}
				}
				//actually add text to the list
				if (b_CanDrawText) {
					PlayState.GetInstance ().GetCurrentLevel ().DrawText (str_TextToDraw, new Vector2 ( 40,1080 - (1080 / 5)),Color.White);
					PlayState.GetInstance ().GetCurrentLevel ().DrawText ("\n"+str_ResponsesToDraw,new Vector2 ( 40,1080 - (1080 / 5)),new Color(0,0,255));
					b_CanDrawText = false;
				}
			}
			else
				CurrentAction = Action.idle;

			//AI will always block if the player punches, except hooker will fall
			if(CanPunch(PlayState.player) && ((PlayState.player.GetCurrentAction() == Player.Action.punch) || (PlayState.player.GetCurrentAction() == Player.Action.shiv)))
			{
				if(b_IsHooker)
					CurrentAction = Action.fall;
				else
					CurrentAction = Action.block;
			}

			//if the player was just falling, and is far enough from the player, change to fallen state
			if(LastAction == Action.fall && !CheckExactCollision(PlayState.player))
			{
				CurrentAction = Action.fallen;
			}
			if (LastAction == Action.fallen)
			{
				CurrentAction = Action.fallen;
				v2_Position.Y = 660;
			}

	
		}
	}
}

