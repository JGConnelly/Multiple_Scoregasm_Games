using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
/// <summary>
/// File handler.
/// THe basic usage of thois
/// </summary>
namespace OpenglTester
{
	public  class FileHandler
	{
		String BaseDirectory = "Content/Data/";
		String CharacterDirectory = "Content/Data/Characters/";
		String LevelDirectory = "Content/Data/Levels/";
		public  FileHandler ()
		{

		}

		// Generic load file blah
		// just serves as an example
		public bool LoadFile(String FileName)
		{
			int NumberOfLines = 0;
			string line;

			// Read the file and display it line by line.
			System.IO.StreamReader file = 
				new System.IO.StreamReader(BaseDirectory+FileName);
			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine (line);
				NumberOfLines++;
			}

			// close your file!
			file.Close();
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return true;
		}


		/// <summary>
		/// Loads the level.
		/// </summary>
		/// <returns>
		/// The level.
		/// </returns>
		/// <param name='Levelname'>
		/// Levelname.
		/// </param>
		public Level LoadLevel (String Levelname)
		{
			// the actual level that will be returned
			Level ret;

			int NumberOfLines = 0;
			string line;

			string ImageSrc;
			int numberOfObject = 0;
			int numberOfCharacters = 0;

			bool ObjectsRead = false;
			bool CharactersRead = false;
			System.IO.StreamReader file= null ;
			// Read the file and display it line by line.
			try{
			 file = new System.IO.StreamReader(LevelDirectory+Levelname+".txt");
			}
			catch ( Exception e)
			{
				Console.Out.WriteLine(e);
			};

			ret = new Level();
			while((line = file.ReadLine()) != null)
			{
				if(NumberOfLines ==0)
				{
					ImageSrc = line;
					ret.SetImage(ImageSrc);
					line = file.ReadLine();

					if(line == "SNOW")
					{
						ret.LevelEmitter = new SnowEmitter(0,1920,1200,1000,10);
						ret.LevelEmitter.Initialise(0,0);
					}
					else if(line == "NULL")
					{
						ret.LevelEmitter = new Emitter(0,0,0,0);
						ret.LevelEmitter.Initialise(0,0);
					}



				}
				if(NumberOfLines ==1)
				{
					/// number of exits
					numberOfObject = Convert.ToInt32(line);
					for(int l =0;l<numberOfObject; l ++)
					{
						// temporary data that will be used to add objects to the level
						string tempFileName;
						Vector2 tempPosition;
						InteractableObject tempObject;

						// reads in data for object position etc
						tempFileName = file.ReadLine();
						NumberOfLines++;

						// position
						line = file.ReadLine();
						string[] Positions = line.Split(','); // get postition from file
						tempPosition = new Vector2(Convert.ToInt32(Positions[0]),Convert.ToInt32(Positions[1]));
						NumberOfLines++;

						tempObject = new InteractableObject(tempFileName);
						tempObject.Position = tempPosition;


						//if this object is an exit
						line = file.ReadLine();
					
						if(line=="true")
						{
							//read in what it exits to (file)
							string tempExit = file.ReadLine();
							NumberOfLines++;
							//where ther position is in the next level when they exit
							// position
							line = file.ReadLine();
							string[] exitsPositionStr = line.Split(','); // get postition from file
							Vector2 exitPosition = new Vector2(Convert.ToInt32(exitsPositionStr[0]),Convert.ToInt32(exitsPositionStr[1]));
							NumberOfLines++;


							ret.AddExit(tempObject,exitPosition,tempExit);

						}
						// if not load in th action associated
						// use the enum from the interactable object class
						else
						{

							line = file.ReadLine();
							NumberOfLines++;

							/// sets the action to corresponding string in the file
							///  i may actually have to list all of them
							if( line == InteractableObject.Action.EquipShiv.ToString())
							{
								tempObject.act_Action = InteractableObject.Action.EquipShiv; 
							}

							ret.AddObject(tempObject);
						}
					}
					ObjectsRead = true;
				}
			
				// read in number of characters
				if (ObjectsRead)
				{
					line = file.ReadLine();
					NumberOfLines++;

				
					numberOfCharacters = Convert.ToInt32(line);
					for(int l =0;l<numberOfCharacters; l ++)
					{
						string tempCharName;
						Vector2 tempPosition;
						AI tempChar;

						// now read in who is in the level
						line = file.ReadLine();
						tempCharName = line;

						// read in the position
						line = file.ReadLine();
						string[] Positions = line.Split(','); // get postition from file
						tempPosition = new Vector2(Convert.ToInt32(Positions[0]),Convert.ToInt32(Positions[1]));
						NumberOfLines++;


						// now we use the character file to read in the character data
						tempChar = LoadCharacter(tempCharName);
						//tempChar = new AI(tempCharName

						tempChar.Position = tempPosition;
						tempChar.Name = tempCharName;
						ret.AddCharacter(tempChar);

					}

					CharactersRead = true;
				}
				// now read all the actions 
				if(CharactersRead)
				{
					line = file.ReadLine();
					int numOfAction = Convert.ToInt32(line);
					for(int num = 0; num < numOfAction; num++)
					{
						line = file.ReadLine();
						Level.Action tempAction; 

						int tempCharStat = 0;
						int tempGameProg= 0;
						float tempTime = 0.0f;
						string tempCharName = "";
						Vector2 tempPosition=new Vector2(0,0);
						Level.Action.TypeOfCharStat enumTypeCharStat = Level.Action.TypeOfCharStat.NONE;
						Level.Action.TypeOfGameStat enumTypeGameStat = Level.Action.TypeOfGameStat.NONE;

						//types of action and reaction/affected party
						Level.Action.TypeOfAction tempTypeOfAction = Level.Action.TypeOfAction.REMOVE;
						bool tempEqual = false;
						bool tempGreater= false;
						bool tempLess= false;
						string Affected;

						string[] strFullAction = line.Split(';'); 
						string[] strFirstLine = strFullAction[0].Split(',');
						string[] strThirdLine = strFullAction[2].Split(',');
						string[] strForthLine = strFullAction[3].Split(',');

						if( strFirstLine[0] == "CHARSTAT")
						{
							// who's stat
							tempCharName = strFirstLine[1];

							// what type of stat
							if(Level.Action.TypeOfCharStat.NONE.ToString() == strFirstLine[2])
							{
								enumTypeCharStat = Level.Action.TypeOfCharStat.NONE;
								tempCharStat = Convert.ToInt32(strFirstLine[3]);
							}
							if(Level.Action.TypeOfCharStat.PLAYERDIS.ToString() == strFirstLine[2])
							{
								enumTypeCharStat = Level.Action.TypeOfCharStat.PLAYERDIS;
								tempCharStat = Convert.ToInt32(strFirstLine[3]);
							}

						}
						else if(strFirstLine[0] == "PROGSTAT")
						{
							
						}
						// the time
						tempTime =(float)(Convert.ToDouble(strFullAction[1]));

						if(strFirstLine[4] == "<")
							tempLess = true;
						else if (strFirstLine[4] == ">")
							tempGreater=true;
						else if (strFirstLine[4] == "=")
							tempEqual = true;


						// Action / reaction stuff
						
						if(strThirdLine[0] ==Level.Action.TypeOfAction.CHANGEROOM.ToString())
						{
							tempTypeOfAction = Level.Action.TypeOfAction.CHANGEROOM;
						}
						if(strThirdLine[0] ==Level.Action.TypeOfAction.REMOVE.ToString())
						{
							tempTypeOfAction = Level.Action.TypeOfAction.REMOVE;
						}
						if(strThirdLine[0] ==Level.Action.TypeOfAction.SPAWN.ToString())
						{
							tempTypeOfAction = Level.Action.TypeOfAction.SPAWN;
						}
						if(strThirdLine[0] ==Level.Action.TypeOfAction.CHANGEOUTFIT.ToString())
						{
							tempTypeOfAction = Level.Action.TypeOfAction.CHANGEOUTFIT;
						}
						Affected =strThirdLine[1];

						// the new given position
						tempPosition = new Vector2(Convert.ToInt32( strForthLine[0]),Convert.ToInt32( strForthLine[1]));
						// set up now using the data
						tempAction = new Level.Action(tempCharStat,tempGameProg,tempCharName,enumTypeCharStat,enumTypeGameStat);
						tempAction.SetupAction(tempTypeOfAction,tempEqual,tempLess,tempGreater,tempTime,Affected,tempPosition);

						// add it to the list
						ret.AddAction(tempAction);
					}
				}

				Console.WriteLine (line);
				NumberOfLines++;
			}
			
			// close your file!
			file.Close();
			file = null;
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return ret;
		}
		public string LoadPlayer()
		{
			int NumberOfLines = 0;
			string line;
			string Return = "";
			
			// Read the file and display it line by line.
			System.IO.StreamReader file = 
				new System.IO.StreamReader(BaseDirectory+"Player.txt");
			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine (line);
				Return = line;
				NumberOfLines++;
			}
			
			// close your file!
			file.Close();
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return Return;
		}
		/// <summary>
		/// Loads the character by name passed through as an argument
		/// </summary>
		/// <returns>
		/// The character.
		/// </returns>
		/// <param name='CharacterName'>
		/// Character name.
		/// </param>
		public AI LoadCharacter (String CharacterName)
		{
			int NumberOfLines = 0;
			string line;
			AI ReturnedCharacter;
			System.IO.StreamReader file;
			// Read the file and display it line by line.
			try {
				file = new System.IO.StreamReader (CharacterDirectory + CharacterName + ".save");
			} catch (Exception e) {
				file = new System.IO.StreamReader (CharacterDirectory + CharacterName + ".txt");
			}
			// temporary data that will be used to add objects to the level
			string tempFileName;
			Vector2 tempPosition;
			int tempDisposision;
			bool tempIsGuard = false;
			bool tempIsHooker = false;


			// reads in data for object position etc
			tempFileName = file.ReadLine ();
			NumberOfLines++;
			ReturnedCharacter = new AI (tempFileName);

			// reads in the characters name
			ReturnedCharacter.Name =  ((FileStream) file.BaseStream).Name;
			ReturnedCharacter.Name = Path.GetFileName(ReturnedCharacter.Name);
			ReturnedCharacter.Name = ReturnedCharacter.Name.Remove(ReturnedCharacter.Name.Length-4,4);

			// now load in the stats
			line = file.ReadLine ();

			tempDisposision = Convert.ToInt32 (line);
			ReturnedCharacter.PlayerDisposition = tempDisposision;
			NumberOfLines++;

			//their hook
			line = file.ReadLine ();
			NumberOfLines++;

			//if guard 
			line = file.ReadLine ();
			tempIsGuard = Convert.ToBoolean (line);
			ReturnedCharacter.IsGuard = tempIsGuard;
			NumberOfLines++;

			//if hooker
			line = file.ReadLine ();
			tempIsHooker = Convert.ToBoolean (line);
			if (tempIsHooker) {
				ReturnedCharacter = new AI (tempFileName, 11, 2, 128f, true);
				ReturnedCharacter.PlayerDisposition = tempDisposision;
				ReturnedCharacter.IsGuard = tempIsGuard;
			} else {
				ReturnedCharacter = new AI (tempFileName, 65, 2, 128f, false);
				ReturnedCharacter.PlayerDisposition = tempDisposision;
				ReturnedCharacter.IsGuard = tempIsGuard;
			}

			ReturnedCharacter.IsHooker = tempIsHooker;
			NumberOfLines++;
			
			//hit points,speed,damage,dodge,block
			line = file.ReadLine();
			ReturnedCharacter.HitPoints = Convert.ToInt32(line);
			NumberOfLines++;

			line = file.ReadLine();
			ReturnedCharacter.HitSpeed =Convert.ToInt32(line);
			NumberOfLines++;

			line = file.ReadLine();
			ReturnedCharacter.HitDamage = Convert.ToInt32(line);
			NumberOfLines++;

			line = file.ReadLine();
			ReturnedCharacter.DodgeSpeed = Convert.ToInt32(line);
			NumberOfLines++;

			line = file.ReadLine();
			ReturnedCharacter.BlockSpeed = Convert.ToInt32(line);
			NumberOfLines++;

			// now all the dialogue
			line = file.ReadLine();
			ReturnedCharacter.NoDialogueLine = line;
			NumberOfLines++;

			line = file.ReadLine();
			int numDialogue = Convert.ToInt32(line );
			NumberOfLines++;

			for (int d = 0; d < numDialogue; d++)
			{
				// the temp dialogue thing
				AI.Dialogue tempDialogue = new AI.Dialogue();
				//ret.Dialogues.Add

				// this gets all the data out of the file
				line=  file.ReadLine();
				string[] DialogueData = line.Split(';'); 
				NumberOfLines++;

				// ending modifier
				if ( StoryProgress.enum_EndingProgress.NONE.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.NONE;

				}


				if ( StoryProgress.enum_EndingProgress.FOODTRUCK.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.FOODTRUCK;
					tempDialogue.EndProgressPreReq.Tunnel = Convert.ToInt32(DialogueData[1]);
				}
				if ( StoryProgress.enum_EndingProgress.INSANE.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.INSANE;
					tempDialogue.EndProgressPreReq.Insane = Convert.ToInt32(DialogueData[1]);
				}
				if ( StoryProgress.enum_EndingProgress.CRIPPLE.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.CRIPPLE;
					tempDialogue.EndProgressPreReq.Cripple = Convert.ToInt32(DialogueData[1]);
				}
				if ( StoryProgress.enum_EndingProgress.SLAYER.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.SLAYER;
					tempDialogue.EndProgressPreReq.Slayer = Convert.ToInt32(DialogueData[1]);
				}
				if ( StoryProgress.enum_EndingProgress.SUICIDE.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.SUICIDE;
					tempDialogue.EndProgressPreReq.Suicide = Convert.ToInt32(DialogueData[1]);
				}
				if ( StoryProgress.enum_EndingProgress.TUNNEL.ToString() == DialogueData[0])
				{
					tempDialogue.EndProgressPreReq.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.TUNNEL;
					tempDialogue.EndProgressPreReq.Tunnel = Convert.ToInt32(DialogueData[1]);
				}

				// pre req point
				tempDialogue.i_PlayerPreReq = Convert.ToInt32(DialogueData[2]);

				// the actual line 
				tempDialogue.Statement = DialogueData[3];

				// now the responses
				int numResponses = Convert.ToInt32(DialogueData[4]);
				line=  file.ReadLine();
				NumberOfLines++;

				string[] ResponseData = line.Split(','); 
				for (int r = 0; r < numResponses; r++)
				{
					// get the individual response's data 
					string[] responses  = ResponseData[r].Split(':');

					// the string of response
					tempDialogue.TheResponses.Add(responses[0]);

					//story prog data
					if ( StoryProgress.enum_EndingProgress.NONE.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis = StoryProgress.enum_EndingProgress.NONE;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.FOODTRUCK.ToString() ==responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.FOODTRUCK;
						tempStorydiag.Foodtruck = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.INSANE.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.INSANE;
						tempStorydiag.Insane = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.CRIPPLE.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.CRIPPLE;
						tempStorydiag.Cripple = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.SLAYER.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.SLAYER;
						tempStorydiag.Slayer = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.SUICIDE.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.SUICIDE;
						tempStorydiag.Suicide = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}
					if ( StoryProgress.enum_EndingProgress.TUNNEL.ToString() == responses[1])
					{
						StoryProgress tempStorydiag = new StoryProgress();
						tempStorydiag.enum_EndingProgressThis =  StoryProgress.enum_EndingProgress.TUNNEL;
						tempStorydiag.Tunnel = 1;
						tempDialogue.ResponseEndingProg.Add(tempStorydiag);
					}

					//player stat
					tempDialogue.ResponseEndingPlayerStat.Add(Convert.ToInt32(responses[2]));

				}
				ReturnedCharacter.Dialogues.Add(tempDialogue);
			}

			//string[] Positions = line.Split(','); // get postition from file
			// close your file!
			file.Close();
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return ReturnedCharacter;


		}

		/// <summary>
		/// News the game.
		///  this deletes all the saves so be careful
		/// </summary>
		public void NewGame()
		{
			string[] SaveList = Directory.GetFiles(BaseDirectory,"*.save");
			foreach (string f in SaveList)
			{
				File.Delete(f);
			}

		}
	}
}

