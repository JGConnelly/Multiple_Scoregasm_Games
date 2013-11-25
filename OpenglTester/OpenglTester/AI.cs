
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
			//stand,
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
			//talk,
			//die
		};

		private string str_Name;
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
		private bool PlayerResponded;

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
			PlayerResponded = false;
		}


		//accessors and mutators

		public string Name
		{
			set {str_Name=value; }
			get {return str_Name;}
		}
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


		public override void Update(float DeltaTime)
		{
			DetermineAction(DeltaTime);
			DetermineAnimation();
			
			base.Update(DeltaTime);
		}

		public override void Draw ()
		{
			base.Draw ();

		}

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
			case Action.talk:
				if (b_IsHooker)
				{
					CurrentAnimation = pTalk;
				}
				else
					CurrentAnimation = JumpLand;
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

		void DetermineAction(float dT)
		{
			LastAction = CurrentAction;
			//is the ai near the player?
			if(CheckNearCollision(PlayState.player) && (CurrentAction != Action.fallen))
			{
				CurrentAction = Action.talk;
				if(PlayState.player.Position.X < Position.X )
					b_FlipImage = true;
				else
					b_FlipImage = false;

				// do text and shiz
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
						i_PlayerDisposition += Dialogues[0].ResponseEndingPlayerStat[0];
						Dialogues.RemoveAt(0);
						PlayerResponded = true;
					}
					else if(InputHandler.dlg2Pressed&& Dialogues[0].TheResponses.Count>1){
						str_TextToDraw =  Dialogues[0].TheResponses[1];
						i_PlayerDisposition += Dialogues[0].ResponseEndingPlayerStat[1];
						Dialogues.RemoveAt(0);
						PlayerResponded = true;
					}
					else if(InputHandler.dlg3Pressed&& Dialogues[0].TheResponses.Count>2){
						str_TextToDraw = Dialogues[0].TheResponses[2];
						i_PlayerDisposition += Dialogues[0].ResponseEndingPlayerStat[2];
						Dialogues.RemoveAt(0);
						PlayerResponded = true;
					}
					else if(InputHandler.dlg4Pressed&& Dialogues[0].TheResponses.Count>3){
						str_TextToDraw = Dialogues[0].TheResponses[3];
						i_PlayerDisposition += Dialogues[0].ResponseEndingPlayerStat[3];
						Dialogues.RemoveAt(0);
						PlayerResponded = true;
					}
				}
				//actually add text to the list
				if (b_CanDrawText) {
					if(PlayerResponded)
					{
						PlayerResponded = false;
						PlayState.GetInstance ().GetCurrentLevel ().DrawText (str_TextToDraw, new Vector2 ( 40,1080 - (1080 / 5)),Color.White, 2.2f);
						PlayState.GetInstance ().GetCurrentLevel ().DrawText ("\n"+str_ResponsesToDraw,new Vector2 ( 40,1080 - (1080 / 5)),new Color(0,0,255),2.2f);
					}
					else
					{
						PlayState.GetInstance ().GetCurrentLevel ().DrawText (str_TextToDraw, new Vector2 ( 40,1080 - (1080 / 5)),Color.White, 0.2f);
						PlayState.GetInstance ().GetCurrentLevel ().DrawText ("\n"+str_ResponsesToDraw,new Vector2 ( 40,1080 - (1080 / 5)),new Color(0,0,255),0.2f);
						b_CanDrawText = false;
					}
				}

			}
			else
				CurrentAction = Action.idle;

			if(CanPunch(PlayState.player) && ((PlayState.player.GetCurrentAction() == Player.Action.punch) || (PlayState.player.GetCurrentAction() == Player.Action.shiv)))
			{
				if(b_IsHooker)
					CurrentAction = Action.fall;
				else
					CurrentAction = Action.block;
			}

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

