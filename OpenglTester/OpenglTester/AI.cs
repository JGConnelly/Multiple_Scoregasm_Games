
using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace OpenglTester
{
	public class AI : Object
	{
		private int i_PlayerDisposision;
		private bool b_IsGuard;
		private int i_HitPoints;
		private float f_HitSpeed;
		private float f_HitDamage;
		private float f_DodgeSpeed;
		private float f_BlockSpeed;

		private string str_NoDialogueLine;


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
			set {i_PlayerDisposision=value; }
			get {return i_PlayerDisposision;}
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

