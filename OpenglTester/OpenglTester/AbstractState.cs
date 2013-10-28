/******************************************
 * AbstractState.cs
 * Author: Tash
 * Created: Tuesday 22 October 2013
 * Last Updated: Tuesday 22 October 2013 4pm
 * Comments: This is a virtual class that all the game states inherit from. Not much to see here, excpet some virtual functions and the ChangeState function.
 * ****************************************/

using System;

namespace OpenglTester
{
	public abstract class AbstractState
	{
		//virtual functions must be implemented by any derived classes
		public abstract void Init ();
		public abstract void Cleanup ();
		
		public abstract void Pause();
		public abstract void Resume();
		
		public abstract void LoadContent(StateManager game);
		public abstract void HandleEvents(StateManager game, float dT);
		public abstract void Update(StateManager game, float dT);
		public abstract void Draw(StateManager game, float dT);
		
		
		public void ChangeState (StateManager game, AbstractState state)
		{
			game.ChangeState(state);
		}
		
		//protected constructor can only be accessed by derived classes
		protected AbstractState ()
		{
			//do nothing
		}
	}
}

