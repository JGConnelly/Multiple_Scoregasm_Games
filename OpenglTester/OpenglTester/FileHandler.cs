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
			// Read the file and display it line by line.
			System.IO.StreamReader file = 
				new System.IO.StreamReader(LevelDirectory+Levelname+".txt");

			ret = new Level();
			while((line = file.ReadLine()) != null)
			{
				if(NumberOfLines ==0)
				{
					ImageSrc = line;
					ret.SetImage(ImageSrc);
				}
				if(NumberOfLines ==1)
				{
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
							line = file.ReadLine();
							ret.AddExit(tempObject,line);
							NumberOfLines++;

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

						ret.AddCharacter(tempChar);

					}


				}
				Console.WriteLine (line);
				NumberOfLines++;
			}
			
			// close your file!
			file.Close();
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
		public AI LoadCharacter(String CharacterName)
		{
			int NumberOfLines = 0;
			string line;
			AI ret;
			System.IO.StreamReader file;
			// Read the file and display it line by line.
			try
			{
				file = new System.IO.StreamReader(CharacterDirectory+CharacterName+".save");
			}
			catch(Exception e)
			{
				file = new System.IO.StreamReader(CharacterDirectory+CharacterName+".txt");
			}
			// temporary data that will be used to add objects to the level
			string tempFileName;
			Vector2 tempPosition;
				
				
			// reads in data for object position etc
			tempFileName = file.ReadLine();
			NumberOfLines++;
			ret = new AI(tempFileName);
			line = file.ReadLine();
			string[] Positions = line.Split(','); // get postition from file
			tempPosition = new Vector2(Convert.ToInt32(Positions[0]),Convert.ToInt32(Positions[1]));
			NumberOfLines++;

			ret.Position = tempPosition;
			// close your file!
			file.Close();
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return ret;

		}

		/// <summary>
		/// News the game.
		/// Holy fuck this deletes all the saves so be careful
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

