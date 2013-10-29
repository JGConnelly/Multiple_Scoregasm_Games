using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class SnowEmitter : Emitter
	{

		private bool b_SwingLeft;
		private float f_SwingTime;
		private float f_HorizontalSpeed;
		// the height is how hight the snow will fall, and the width of how far the snow fall stretches
		public SnowEmitter (float Height, float Width,int num, float time, float dirh,float dirl):base( num,  time,  dirh, dirl)
		{

			b_SwingLeft = true;
			f_SwingTime = 2.5f;

		}

		public override void Initialise (int posx , int posy, GraphicsDeviceManager gdm, ContentManager cm)
		{
			for (int i =0; i < i_ParticleNumber; i ++) 
			{
				Particles.Add(new Particle("par",gdm,cm));
				//FIX 
				//int posx = randnum.Next(0,Width);
				//Particles[i].Position=new Vector2(posx,Height);
			}
			//base.Initialise (posx, posy, gdm, cm);
		}
		public override void Update (float fDeltaTime)
		{

			for (int i =0; i < i_ParticleNumber; i ++) {
				if (i_NumActive > i) 
				{
					if(b_SwingLeft)
						Particles [i].SetVelocity(new Vector2(Particles[i].GetVelocity().X-f_HorizontalSpeed*fDeltaTime,Particles[i].GetVelocity().Y));
					else
						Particles [i].SetVelocity(new Vector2(Particles[i].GetVelocity().X+f_HorizontalSpeed*fDeltaTime,Particles[i].GetVelocity().Y));
					Particles [i].Update (fDeltaTime);
				} 
				else
					break;
			}
			
			if (f_TotalTime / i_NumActive > f_EmitionRate && i_NumActive < i_ParticleNumber) {
				Particles [i_NumActive].SetLife (5);
				i_NumActive++;
				
			}
			f_TotalTime += fDeltaTime;
			f_SwingTime -= fDeltaTime;

			// check whether to reset the swing timer.
			if (f_SwingTime < 0) 
			{
				f_SwingTime = 2.5f;

				// switch the swing
				if(b_SwingLeft)
					b_SwingLeft = false;
				else
					b_SwingLeft = true;
			}

			//base.Update (fDeltaTime);
		}
	}
}

