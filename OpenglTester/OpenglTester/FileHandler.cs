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
		String Directory = "Content/Data/";
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
				new System.IO.StreamReader(Directory+FileName);
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
						Object tempObject;

						tempFileName = file.ReadLine();
						line = file.ReadLine();
						NumberOfLines++;
						string[] Positions = line.Split(',');
						tempPosition = new Vector2(Convert.ToInt32(Positions[0]),Convert.ToInt32(Positions[1]));
						NumberOfLines++;

						tempObject = new Object(tempFileName);
						tempObject.Position = tempPosition;


						//if this object is an exit
						line = file.ReadLine();
					
						if(line=="true")
						{
							line = file.ReadLine();
							ret.AddExit(tempObject,line);

						}
						else
						{
							ret.AddObject(tempObject);
							
							line = file.ReadLine();
						}
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
				new System.IO.StreamReader(Directory+"Player.txt");
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
			// Read the file and display it line by line.
			System.IO.StreamReader file = 
				new System.IO.StreamReader(Directory+CharacterName);
			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine (line);
				NumberOfLines++;
			}
			
			// close your file!
			file.Close();
			Console.WriteLine("Total number of lines : " + NumberOfLines);
			return null;

		}
	}
}

