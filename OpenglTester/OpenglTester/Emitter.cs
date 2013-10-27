using System;


using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace OpenglTester
{
	public class Emitter
	{
		private int i_ParticleNumber;
		private Vector2 v2_Position;
		private int i_NumActive;
		private float f_LifeTime;
		private float f_EmitionRate;
		private float f_TotalTime;
		private float f_EmitionDirectionRangeHigh;
		private float f_EmitionDirectionRangeLow; 


		// some list for storing
		List<Particle> Particles;
		Random randnum = new Random();
		// the number of particles 
		public Emitter (int num, float time, float dirh,float dirl)
		{
			i_ParticleNumber = num;
			f_LifeTime = time;
			f_EmitionRate = i_ParticleNumber/time;
			f_TotalTime=0;
			f_EmitionDirectionRangeHigh = dirh;
			f_EmitionDirectionRangeLow = dirl;
			i_NumActive = 0;
			Particles = new List<Particle>();
		}

		public virtual void Initialise(int posx, int posy,GraphicsDeviceManager gdm,ContentManager cm)
		{
			//Random
			//srand( time(NULL) );
			for(int i =0 ; i < i_ParticleNumber; i ++)
			{
				// this is mainly fire based stuff
				Particles.Add(new Particle("par",gdm,cm));
				Particles[i].Position=new Vector2(posx,posy);

				int randnumx = randnum.Next(5,50);
				int randnumy = randnum.Next(5,50);
				int randDir=randnum.Next(359);
				int randDirvel=randnum.Next(100);
				Particles[i].setRotationVel(randDir,randDirvel);
				Particles[i].SetVelocity(new Vector2(randnumx,randnumy));
				Particles[i].Alive= true;
			}

		}
		public virtual void Update (float fDeltaTime)
		{
			
			for (int i =0; i < i_ParticleNumber; i ++) {
				if (i_NumActive > i)
					Particles [i].Update (fDeltaTime);
				else
					break;
			}
		
			if (f_TotalTime / i_NumActive > f_EmitionRate && i_NumActive < i_ParticleNumber) {
				Particles [i_NumActive].SetLife (5);
				i_NumActive++;
			
			}
			f_TotalTime += fDeltaTime;

		}
		public virtual void Draw (SpriteBatch spriteBatch)
		{
			for(int i =0 ; i < i_ParticleNumber; i ++)
				if (i_NumActive > i)
					Particles[i].Draw(spriteBatch);
				else
					break;
		}
	}
}

