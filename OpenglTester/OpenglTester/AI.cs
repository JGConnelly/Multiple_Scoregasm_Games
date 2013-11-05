
using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace OpenglTester
{
	public class AI : Object
	{
		private class Dialogue
		{
			// the pre req for each dialogue options
			public StoryProgress EndProgressPreReq;
			int i_PlayerPreReq;

			// the responses and stats associated
			// so both lists should be of equal size
			public List<StoryProgress> ResponseEndingProg;
			public List<int> ResponseEndingPlayerStat;

		}
		public AI(string imagePath ):base ( imagePath )
		{

		}

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

