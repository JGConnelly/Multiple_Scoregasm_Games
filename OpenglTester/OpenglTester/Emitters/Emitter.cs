using System;


using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace OpenglTester
{
	public class Emitter
	{
		protected int i_ParticleNumber;
		protected Vector2 v2_Position;
		protected int i_NumActive;
		protected float f_LifeTime;
		protected float f_EmitionRate;
		protected float f_TotalTime;
		protected float f_EmitionDirectionRangeHigh;
		protected float f_EmitionDirectionRangeLow; 


		// some list for storing
		protected List<Particle> Particles;
		protected Random randnum = new Random();
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

				int randnumx = randnum.Next(-50,50); //what direction on the x plane it will move
				int randnumy = randnum.Next(-50,50); //what direction on the y plane it will move
				int randDir=randnum.Next(359); // what direction it will face
				int randDirvel=randnum.Next(-10,10); // what direction and speed it will spin
				Particles[i].setRotationVel(randDir,randDirvel);
				Particles[i].SetVelocity(new Vector2(randnumx,randnumy));
				Particles[i].Alive= true;
				Particles[i].SetCenter();
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

