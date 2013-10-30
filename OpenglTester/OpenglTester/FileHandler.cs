using System;
using System.IO;
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
			return null;
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
			return null;
		}
	}
}

