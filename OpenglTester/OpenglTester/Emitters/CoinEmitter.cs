using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// Snow emitter.
/// this emitter will slowly create snow particles that will sway left and right until they hit the ground
/// </summary>
/// 
namespace OpenglTester
{
	public class CoinEmitter : Emitter
	{
		


		private int i_SkyHeight;
		private int i_SkyWidth;
		private int i_GroundHeight;
		// the height is how hight the snow will fall, and the width of how far the snow fall stretches 
		// remember origin is top left
		public CoinEmitter (int Height, int Width,int ground,int num, float time ):base( num,  time,  0, 359)
		{

			i_SkyHeight = Height;
			i_SkyWidth = Width;

			i_GroundHeight = ground;
		}
		
		public override void Initialise (int posx , int posy)
		{
			for (int i =0; i < i_ParticleNumber; i ++) 
			{

				Particles.Add(new Particle("coinstrip",9,1.5f,95));
				Particles[Particles.Count -1].SetAnimationStartPoint(0,9,1.5f);
				int pos_x = randnum.Next(0,i_SkyWidth);
				Particles[i].Position=new Vector2(pos_x,i_SkyHeight);
				
				Particles[i].Alive= true;
				float DownSpeed = randnum.Next(-250,-100);
				Particles [i].SetVelocity(new Vector2(0,DownSpeed));
			}
			i_NumActive = 1;
			//base.Initialise (posx, posy, gdm, cm);
		}
		public override void Update (float fDeltaTime)
		{
			
			for (int i =0; i < i_ParticleNumber; i ++) {
				if (i_NumActive > i) 
				{

					Particles [i].SetVelocity(new Vector2(Particles[i].GetVelocity().X,Particles[i].GetVelocity().Y));

						
					
					Particles [i].Update (fDeltaTime);
				} 
				else
					break;
				
				//if it hits the ground 
				if(Particles[i].Position.Y > i_GroundHeight)
				{
					Particles[i].SetLife(-1);
					int pos_x = randnum.Next(0,i_SkyWidth);
					Particles[i].Position=new Vector2(pos_x,i_SkyHeight);
					Particles[i].Alive= true;
					float DownSpeed = randnum.Next(-250,-100);
					Particles [i].SetVelocity(new Vector2(0,DownSpeed));
				}
			}
			float spawn = f_TotalTime / i_NumActive ;
			if (spawn < f_EmitionRate && i_NumActive < i_ParticleNumber) {
				Particles [i_NumActive].SetLife (-1);
				i_NumActive++;
				
			}
			f_TotalTime += fDeltaTime;

			
			// check whether to reset the swing timer.
						
			
			//base.Update (fDeltaTime);
		}
	}
}

