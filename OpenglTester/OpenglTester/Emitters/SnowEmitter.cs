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
	public class SnowEmitter : Emitter
	{

		private bool b_SwingLeft;
		private float f_SwingTime;
		private float f_HorizontalSpeed;
		private int i_SkyHeight;
		private int i_SkyWidth;
		private int i_GroundHeight;
		// the height is how hight the snow will fall, and the width of how far the snow fall stretches 
		// remember origin is top left
		public SnowEmitter (int Height, int Width,int ground,int num, float time ):base( num,  time,  0, 359)
		{

			b_SwingLeft = true;
			f_SwingTime = 2.5f;
			i_SkyHeight = Height;
			i_SkyWidth = Width;
			f_HorizontalSpeed = 50;
			i_GroundHeight = ground;
		}

		public override void Initialise (int posx , int posy, GraphicsDeviceManager gdm, ContentManager cm)
		{
			for (int i =0; i < i_ParticleNumber; i ++) 
			{
				int snowran = randnum.Next(1,4);
				Particles.Add(new Particle("Snow"+snowran,gdm,cm));
			
				int pos_x = randnum.Next(0,i_SkyWidth);
				Particles[i].Position=new Vector2(pos_x,i_SkyHeight);

				Particles[i].Alive= true;
				float DownSpeed = randnum.Next(-60,-10);
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
					if(b_SwingLeft)
						Particles [i].SetVelocity(new Vector2(Particles[i].GetVelocity().X-f_HorizontalSpeed*fDeltaTime,Particles[i].GetVelocity().Y));
					else
						Particles [i].SetVelocity(new Vector2(Particles[i].GetVelocity().X+f_HorizontalSpeed*fDeltaTime,Particles[i].GetVelocity().Y));

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
					float DownSpeed = randnum.Next(-60,-10);
					Particles [i].SetVelocity(new Vector2(0,DownSpeed));
				}
			}
			float spawn = f_TotalTime / i_NumActive ;
			if (spawn < f_EmitionRate && i_NumActive < i_ParticleNumber) {
				Particles [i_NumActive].SetLife (-1);
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

