
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
			//talk,
			//die
		};

		private int i_PlayerDisposition;
		private bool b_IsGuard;
		private int i_HitPoints;
		private float f_HitSpeed;
		private float f_HitDamage;
		private float f_DodgeSpeed;
		private float f_BlockSpeed;
		private Action currentAction;


		AnimationInfo Idle = new AnimationInfo(0,1,1), Walk = new AnimationInfo(17,6,4), 
						Run = new AnimationInfo(1,16,2), Punch = new AnimationInfo(23,6,1),
							Sneak = new AnimationInfo(30,6,4),Crouch = new AnimationInfo(29,1,1),
								Shiv = new AnimationInfo(36,5,0.5f),JumpLand = new AnimationInfo(41,0,2),
									Jumping = new AnimationInfo(42,5,1), Block = new AnimationInfo(47,1,1.5f);
		AnimationInfo CurrentAnimation;

		private string str_NoDialogueLine;


		public AI(string imagePath , int numberOfFrames , int timeToComplete,float frameSize):base ( imagePath , numberOfFrames ,  timeToComplete, frameSize)
		{
			b_IsAnimated = true;
			CurrentAnimation = Idle;
			SetAnimationStartPoint(CurrentAnimation.Start,CurrentAnimation.Frames,CurrentAnimation.TimeForCompletion);
			GenerateAlpha();
			Scale = new Vector2 (4,4);
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


		}


		//accessors and mutators

	


		public int PlayerDisposision

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
		//end of accesors and mutators


		void Update(double DeltaTime)
		{
			base.Update((float)DeltaTime);
		}
		void Draw()
		{
			base.Draw();
		}
	}
}

