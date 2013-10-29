/******************************************
 * StateManager.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: The state manager has a list of the states that are currently running. The state at the back of the list is the currently active state.
 * This class handles changing states by cleaning up and removing old states, and initializing and adding new states to the list.
 * The game updates and draws this class, then this class calls the update and draw method of the current state (the back of the list)
 * ****************************************/

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class StateManager : Game
	{
		private bool isRunning;
		private List<AbstractState> states;
		public static bool spaceIsDown = false;
		
		public void Init()
		{
			Console.WriteLine ("StateManager Initialized");
			//TODO: This method should receive the parameters for title, window width and ehight, fullscreen, etc
			//TODO: then initialize the framework window
			isRunning = true;
		}
		
		public void Cleanup ()
		{
			//cleanup and delete all the states
			while (states.Count > 0) {
				states[states.Count - 1].Cleanup();
				states.RemoveAt(states.Count - 1);
			}
			//TODO: shut down the window and framework
		}
		
		public void ChangeState (AbstractState inState)
		{
			//clean up the current state
			if (states != null)
			{
				if (states.Count > 0) 
				{
					states[states.Count - 1].Cleanup();
					states.RemoveAt (states.Count - 1);
				}
			}
			//store and initialize the new state
			states.Add(inState);
			states[states.Count - 1].Init();
		}
		
		public void PushState (AbstractState inState)
		{
			//pause the current state
			if (states.Count > 0) 
			{
				states[states.Count - 1].Pause();
			}
			//store and initialize the new state
			states.Add (inState);
			states[states.Count - 1].Init ();
		}
		
		public void PopState ()
		{
			//cleanup the current state
			if (states.Count > 0) {
				states [states.Count - 1].Cleanup ();
				states.RemoveAt (states.Count - 1);
			}
			//resume the previous state
			if (states.Count > 0) {
				states [states.Count - 1].Resume ();
			}
		}
		
		public void LoadContent()
		{
			//call the LoadContent function on the current state
			states[states.Count - 1].LoadContent(this);
		}
		
		public void HandleEvents(float dT)
		{
			//call the handle events method on the current state
			states[states.Count - 1].HandleEvents(this, dT);
		}
		
		public void Update(float dT)
		{
			//check all the inputs, save them all as public static variables so other classes can use them
			//this prevents excessive GetState()
			//call the update method on the current state
			states[states.Count - 1].Update(this, dT);
		}
		
		public void Draw (float dT)
		{
			//call the draw method of the current state
			states [states.Count - 1].Draw (this, dT);
		}
		
		public bool IsRunning ()
		{
			return isRunning;
		}
		
		public void Quit ()
		{
			isRunning = false;
		}
		
		public StateManager()
		{
			states = new List<AbstractState>();
			states.Clear ();
		}
	}
}

