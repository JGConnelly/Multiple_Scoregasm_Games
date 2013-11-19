#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace OpenglTester
{
	static class Program
	{
		public static Game1 game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new Game1 ();
			game.Run ();
		}
	}
}
