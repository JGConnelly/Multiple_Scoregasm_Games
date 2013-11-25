using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace OpenglTester
{
	public class Level
	{

		// contains a few objects(sprites ) for interaction ?
		Object obj_ThisObject;
		public Emitter LevelEmitter;
		
		//SnowFall = new SnowEmitter(0,1920,1200,1000,10);
		//SnowFall.Initialise(0,0);
		private List<String> str_Exits;


		private List<InteractableObject> obj_InteractableObjects;
		public List<AI> ai_Characters;

		private SpriteFont TheFont;
		private const string FontUsed = "FontyFony";

		private class TextToScreen
		{
			public string theText;
			public float StartTime;
			public float TimeOnScreen;
			public Color theColor;
			public Vector2 Position;
			public TextToScreen()
			{
				theText = "";
				StartTime = 0;
				TimeOnScreen = 0;
				theColor = Color.White;
				Position = new Vector2 (0,0);
			}
			public TextToScreen(string text, float time, Vector2 pos, Color col)
			{
				theText = text;
				StartTime = time;
				TimeOnScreen = time;
				theColor = col;
				Position = pos;
			}
		}
		private List<TextToScreen> OnScreenWords;

		// the ingame actions in the game that take place
		// which are read in the FileHandler class
		public class Action
		{
			int CharStat; // a characters stat check
			int ProgStat;// a game progress stat check


			string Character; // Which character

			public enum TypeOfCharStat
			{
				NONE,
				PLAYERDIS

			}
			public TypeOfCharStat e_TypeOfCharStat;

			public enum TypeOfGameStat
			{
				NONE,
				TUNNEL,
				SUICIDE,
				FOODTRUCK,
				RAPE,
				INSANE,
				SLAYER

			}
			public TypeOfGameStat e_TypeOfGameStat;

			//equal to less then etc
			bool	comp_EQUAL;
			bool	comp_LESS;
			bool	comp_GREATER;
			public enum TypeOfAction
			{
				SPAWN,
				REMOVE,
				CHANGEROOM

			};
			public TypeOfAction e_TypeOfAction;
			string str_ObjectAffected;
			float TimeBetween;
			bool b_ConditionMet;
			Vector2 v2_NewPosition;
			bool b_Completed;

			public Action(int chara, int gameprog, string charname, TypeOfCharStat  charstat,TypeOfGameStat  gamestat)
			{
				b_ConditionMet = false;
				CharStat= chara;
				ProgStat = gameprog;
				Character = charname;
				e_TypeOfCharStat  = charstat;
				e_TypeOfGameStat = gamestat;
				v2_NewPosition= new Vector2(0,0);
				b_Completed = false;
			}
			public void  SetupAction (TypeOfAction action, bool equal, bool less, bool greater, float time,string affected,Vector2 pos)
			{
				b_ConditionMet = false;
				e_TypeOfAction = action;
				comp_EQUAL = equal;
				comp_LESS = less;
				comp_GREATER = greater;
				TimeBetween = time;
				str_ObjectAffected = affected;
				v2_NewPosition = pos;
			}
			public void Update (float deltaTime)
			{
				// check condition
				for (int ai = 0; ai < PlayState.GetInstance().CurrentLevel.ai_Characters.Count; ai++) {
					// check if its a character stat
					if (PlayState.GetInstance ().CurrentLevel.ai_Characters[ai].Name == Character) {
						if(e_TypeOfCharStat == TypeOfCharStat.PLAYERDIS){
							// now check equal to etc
							if(comp_GREATER)
							{
								if (PlayState.GetInstance ().CurrentLevel.ai_Characters[ai].PlayerDisposition > CharStat)
								{b_ConditionMet = true;}
							}
							else if(comp_EQUAL)
							{
								if (PlayState.GetInstance ().CurrentLevel.ai_Characters[ai].PlayerDisposition == CharStat)
								{b_ConditionMet = true;}
							}
							else if(comp_LESS)
							{
								if (PlayState.GetInstance ().CurrentLevel.ai_Characters[ai].PlayerDisposition < CharStat)
								{b_ConditionMet = true;}
							}

						}

					}
					// check if its a game/ story progression stat
					else if(PlayState.GetInstance().CurrentProgress.enum_EndingProgressThis.ToString()  == e_TypeOfGameStat.ToString()&&PlayState.GetInstance().CurrentProgress.enum_EndingProgressThis.ToString()!= "NONE")
					{
						int tempIndex = PlayState.GetInstance().CurrentProgress.enum_EndingProgressThis.GetHashCode();
						if(PlayState.GetInstance().CurrentProgress.Stats[tempIndex] >= ProgStat)
						{
							b_ConditionMet = true;
						}
					}

				}

				//when the condition is met do it
				if (b_ConditionMet) 
				{
					if(TimeBetween<=0)
					{
						if(e_TypeOfAction == TypeOfAction.SPAWN)
						{
							//load in and add a new character
							PlayState.GetInstance().CurrentLevel.AddCharacter(PlayState.GetInstance().fileManager.LoadCharacter(str_ObjectAffected));
							PlayState.GetInstance().CurrentLevel.ai_Characters[
								PlayState.GetInstance().CurrentLevel.ai_Characters.Count-1].Position = v2_NewPosition;
						}
						if(e_TypeOfAction == TypeOfAction.REMOVE)
						{
							for (int ai = 0; ai < PlayState.GetInstance().CurrentLevel.ai_Characters.Count; ai++)
							{
								if(PlayState.GetInstance().CurrentLevel.ai_Characters[ai].Name == str_ObjectAffected)
									PlayState.GetInstance().CurrentLevel.ai_Characters.RemoveAt(ai);
								break;
							}
						}
						if(e_TypeOfAction == TypeOfAction.CHANGEROOM)
						{
							PlayState.GetInstance().CurrentLevel = PlayState.GetInstance().fileManager.LoadLevel(str_ObjectAffected);
							PlayState.player.Position = v2_NewPosition;
						}
						b_Completed =true;

					}

					TimeBetween -= deltaTime;
				}
			}

			public bool IsCompleted()
			{
				return b_Completed;
			}
		}

		public List<Action> Actions;
		// progression stats
		//private int i_
		
		public Level ()
		{
			obj_InteractableObjects = new List<InteractableObject>{};
			str_Exits = new List<string>{};
			ai_Characters = new List<AI>{};
			OnScreenWords = new List<TextToScreen>();
			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
			Actions = new List<Action>();
		}
		public Level ( List<String> Exits , List<InteractableObject> intObjects)
		{

			str_Exits = Exits;
			OnScreenWords = new List<TextToScreen>();
			obj_InteractableObjects = intObjects;
			TheFont = Game1.contentManager.Load<SpriteFont>(FontUsed);
			Actions = new List<Action>();
		
		}
		public void SetImage(string src)
		{
			obj_ThisObject = new Object(src);
			//obj_ThisObject.GenerateAlpha();
		}
		public void AddAction (Action act)
		{
			Actions.Add(act);
		}
		public void AddObject(InteractableObject intObj)
		{
			obj_InteractableObjects.Add(intObj);
		}
		/// <summary>
		/// Adds the exit.
		/// </summary>
		/// <param name='intObj'>
		/// Int object.
		/// </param>
		/// <param name='exit'>
		/// Exit.
		/// </param>
		/// <param name='newRoomPos'>
		/// New room position. so the position the player will be in once they enter the new room
		/// </param>
		public void AddExit(InteractableObject intObj,Vector2 newRoomPos, string exit)
		{
			intObj.SetIsExit(true,newRoomPos,exit);
			obj_InteractableObjects.Add(intObj);
			str_Exits.Add(exit);
		}
		public void AddCharacter(AI character)
		{
			ai_Characters.Add(character);
		}


		public void Update (float DeltaT)
		{
			obj_ThisObject.Update (DeltaT);

			for (int obj = 0; obj < obj_InteractableObjects.Count; obj++) {
				obj_InteractableObjects [obj].Update (DeltaT);
			}

			//ai
			for (int ais = 0; ais < ai_Characters.Count; ais++) {
				ai_Characters [ais].Update (DeltaT);
			}

			//text
			for (int t = 0; t < OnScreenWords.Count; t++) {
				OnScreenWords [t].TimeOnScreen -= DeltaT;
			}

			//actions
			for (int a = 0; a < Actions.Count; a++) {
				Actions[a].Update(DeltaT);
				if(Actions[a].IsCompleted())
				{
					Actions.RemoveAt(a);
					break;
				}
			}
			LevelEmitter.Update(DeltaT);


		}
		public void Draw ()
		{

			//draw interactable objects
			//draws the levels background first
			obj_ThisObject.Draw ();

			/// TEXT
			//DrawText("lel lel",new Vector2(1000,750),Color.White);
			for (int t = 0; t < OnScreenWords.Count; t++) {
				if (OnScreenWords [t].TimeOnScreen < 0)
					OnScreenWords.RemoveAt (t);
				else {
					Game1.spriteBatch.DrawString (TheFont, OnScreenWords [t].theText, OnScreenWords [t].Position, OnScreenWords [t].theColor);
					
				}
				
			}
			if (OnScreenWords.Count == 0) {
				int derp = 5;
				derp++;
			}

			for (int obj = 0; obj < obj_InteractableObjects.Count; obj++) {
				obj_InteractableObjects [obj].Draw ();
			}

			// draw ais / characters

			for (int ais = 0; ais < ai_Characters.Count; ais++) {
				ai_Characters [ais].Draw ();
			}

			LevelEmitter.Draw();

		}
		public void DrawText (String words, Vector2 pos, Color col , float time = 2f)
		{
			bool canAdd = true;
			TextToScreen temp = new TextToScreen(words,time,pos,col);

			// ensure there are no doubles
			for (int t = 0; t < OnScreenWords.Count; t++) 
			{
				if(temp.Position==OnScreenWords[t].Position&&temp.theText==OnScreenWords[t].theText )
				{
					OnScreenWords[t].TimeOnScreen = OnScreenWords[t].StartTime;
					canAdd = false;
				}
			}
			if(canAdd)
				OnScreenWords.Add(new TextToScreen(words,time,pos,col));	
		}

		/// <summary>
		/// Checks the can interact with a
		/// </summary>
		/// <param name='check'>
		/// Check that whatever you pass through if its close enough to interact
		/// basically check if it collides
		/// </param>
		public bool CheckCanInteract(Object check)
		{
			
			for (int obj = 0; obj < obj_InteractableObjects.Count;obj++)
			{
				if(check.CheckNearCollision(obj_InteractableObjects[obj]))
				{
					return true;
				}
			}
			return false;

		}

	}
}

